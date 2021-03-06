﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMv1.ViewModels
{
    public class SignupViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        
        public string Password { get; set; }

        [Required]
        public string PasswordConfirm { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
