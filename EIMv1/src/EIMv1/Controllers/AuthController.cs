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

namespace EIMv1.Controllers.Web
{
    public class AuthController : Controller
    {
      
        public IActionResult Signup()
        {
            //ADD Todo: if user been authenticated, redirect to messaging page
            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Message", "Message");
            //}
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupViewModel model)
        {
           
            if (ModelState.IsValid)
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
            string cookie = Request.Cookies["myTest"];
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
