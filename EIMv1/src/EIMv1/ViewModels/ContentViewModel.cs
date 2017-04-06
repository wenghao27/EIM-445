using EIMv1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMv1.ViewModels
{
    public class ContentViewModel
    {
        public UserList Users { get; set; }
        
        public String Friend { get; set; }

        public MessageList Messages { get; set; }
        
        public string Description { get; set; }
    }
}
