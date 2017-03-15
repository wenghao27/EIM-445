using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMv1.Controllers
{
    public class MessageController : Controller
    {
        //supposed to be authorized, not sure if it is a good practice
        //to set up a local DB to just check authentication 
        //[Authorize]
        public IActionResult Message()
        {
            string token = Request.Cookies["ACCESS_TOKEN"];
            return View();
        }

      
    }
}
