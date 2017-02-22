using EIMv1.ViewModels;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                        password = model.Password.ToString()

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
            if (ModelState.IsValid)
            {
                HttpResponseMessage responseMessage =
                             await "https://chronoschat.co/authenticate".PostUrlEncodedAsync(new
                             {

                                 email = vModel.Username,
                                 password = vModel.Password

                             });
                //var signInResult = await _signInManager
                //                  .PasswordSignInAsync(vModel.Username, vModel.Password, true, false);
                if (responseMessage.IsSuccessStatusCode)
                {
                    // var result = await _signInManager.PasswordSignInAsync(vModel.Username, vModel.Password, true, false);
                    // if (result.Succeeded)
                    //  {
                    return RedirectToAction("Message", "Message");
                    //  }

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
