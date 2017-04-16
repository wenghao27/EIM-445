using EIMv1.Models;
using EIMv1.ViewModels;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Flurl;
using System.Net.Http.Headers;
using System.Runtime.Serialization;

namespace EIMv1.Controllers.Web
{
    public class AuthController : Controller
    {
        private static string chronoskeysUrl = "https://chronoskeys.co/";

        public class KeyPost
        {
            [JsonProperty("user")]
            public string user { get; set; }

            [JsonProperty("key")]
            public string key { get; set; }
        }

        public IActionResult Signup()
        {
            //ADD Todo: if user been authenticated, redirect to messaging page
            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Message", "Message");
            //}
            return View();
        }

        [HttpGet]
        public async Task<bool> CheckUsernameAvailability(SignupViewModel model)
        {
            try
            {
                var response = await chronoskeysUrl
                    .AppendPathSegment("api")
                    .AppendPathSegment("PublicKey")
                    .AppendPathSegment("byUser")
                    .SetQueryParam("user", model.Username.ToString())
                    .GetAsync();
               
                if (response.StatusCode.Equals(200))
                {
                    return false;
                }
            }
            catch (FlurlHttpException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return true;
          }

        [HttpGet]
        public async Task<bool> PKDWakeUp()
        {
            try
            {
                var response = await chronoskeysUrl
                    .GetAsync();

                if (response.StatusCode.Equals(200))
                {
                    return true;
                }
            }
            catch (FlurlHttpException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return false;
        }

        [HttpPost]
        public async Task<bool> PublishPublicKey(SignupViewModel model)
        {
            byte[] keyPair = Encryption.RSAService.RSAKeyGeneration();
            byte[] publicKey = Encryption.RSAService.RSAPublicKeyOnly(keyPair);
            String publicKeyStr = Encryption.RSAService.convertToString(publicKey);
            //Debug.WriteLine("Public key: " + publicKeyStr);
            var postParams = new KeyPost();
            postParams.user = model.Username.ToString();
            postParams.key = publicKeyStr;

            var jsonString = JsonConvert.SerializeObject(postParams);
            var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync("https://chronoskeys.co/api/PublicKey", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }


            //try
            //{
            //    var responseMessage = await "https://chronoskeys.co/api/PublicKey".PostUrlEncodedAsync(new
            //    {
            //        user = model.Username.ToString(),
            //        key = publicKeyStr
            //    });
            //    if (responseMessage.StatusCode.Equals(200))
            //    {
            //        return true;
            //    }
            //}
            //catch (FlurlHttpException ex)
            //{
            //    Debug.WriteLine(ex.ToString());
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.ToString());
            //}
            return false;
        }

            
        

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                bool userNameCheck = await CheckUsernameAvailability(model);
                if (userNameCheck == true)
                {
                    bool publishKeyCheck = await PublishPublicKey(model);
                    if (publishKeyCheck == true)
                    {
                        try
                        {
                            HttpResponseMessage responseMessage = await "https://chronoschat.co/registration".PostUrlEncodedAsync(new
                            {
                                email = model.Username.ToString(),
                                password = model.Password.ToString(),
                                first_name = model.FirstName,
                                last_name = model.LastName

                            });
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Login", "Auth");
                            }
                        }
                        catch (FlurlHttpException ex)
                        {
                            Debug.WriteLine(ex.ToString());
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Public key did not post for " + model.Username + "." );
                    }
                }
                else
                {
                    Debug.WriteLine("Username is taken on chronoskeys.co.");
                }

            }

            return View();
        }


        public IActionResult Login()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Message", "Message");
            //}
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage responseMessage =
                             await "https://chronoschat.co/authenticate".PostUrlEncodedAsync(new
                             {

                                 email = vModel.Username,
                                 password = vModel.Password

                             });
                string auth = await responseMessage.Content.ReadAsStringAsync();
                UserToken ut = JsonConvert.DeserializeObject<UserToken>(auth);

                Response.Cookies.Append("ACCESS_TOKEN", ut.auth_token);
               
               
              
                if (responseMessage.IsSuccessStatusCode)
                {
             
                    return RedirectToAction("Message", "Message");

                }
            }
            return View();
        }
    }

    //public async Task<IActionResult> Logout()
    //{
    //    if (User.Identity.IsAuthenticated)
    //    {
    //        await _signInManager.SignOutAsync();
    //    }
    //    return RedirectToAction("Login", "Auth");
    //}

}
