using EIMv1.Models;
using EIMv1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMv1.Components
{
    public class UserList : ViewComponent
    {
        private readonly IUserRepository _userRepository;

        public UserList(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IViewComponentResult> Invoke()
        {
            string token = Request.Cookies["ACCESS_TOKEN"];

            var messageViewModel = new ContentViewModel
            {
                Users = await _userRepository.usersAsync(token)
            };
            return View(messageViewModel);
        }

    }
}
