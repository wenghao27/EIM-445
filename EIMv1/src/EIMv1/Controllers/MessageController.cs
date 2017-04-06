using EIMv1.Models;
using EIMv1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMv1.Controllers
{
    public class MessageController : Controller
    {
        private readonly IUserRepository _UserRepository;

        public MessageController(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        //supposed to be authorized, not sure if it is a good practice
        //to set up a local DB to just check authentication 
        //[Authorize]
        public async Task<IActionResult> Message()
        {
            string token = Request.Cookies["ACCESS_TOKEN"];

            var messageViewModel = new ContentViewModel
            {
                Users = await _UserRepository.usersAsync(token),
                Friend = null
            };
            
            return View(messageViewModel);
        }

        [Route("Message/Message/{id}")]
        public async Task<IActionResult> Message(string id)
        {
            string token = Request.Cookies["ACCESS_TOKEN"];
            Debug.WriteLine("in Messages() id is " + id);

            var contentViewModel = new ContentViewModel
            {
                Users = await _UserRepository.usersAsync(token),
                Friend = id,
                Messages = await _UserRepository.getMessages(token, id)
            };
            return View(contentViewModel);
        }

        public ViewResult UserList()
        {
            return View();
        }

        public ViewResult Messages()
        {
            return View();
        }
    }

   
}
