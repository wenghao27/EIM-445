﻿using EIMv1.Models;
using EIMv1.ViewModels;
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
        private readonly IUserRepository _UserRepository;

        public MessageController(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        //supposed to be authorized, not sure if it is a good practice
        //to set up a local DB to just check authentication 
        //[Authorize]
        public IActionResult Message()
        {


            var messageViewModel = new MessageViewModel
            {
                Users = _UserRepository.Users
            };

            return View(messageViewModel);
        }




        


    }
}