using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EIMv1.Models;

namespace EIMv1.ViewModels
{
    public class UserToken
    {
        public string auth_token { get; set; }
        public User user { get; set; }
    }
}
