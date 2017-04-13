﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMv1.ViewModels
{
    public class MessageViewModel
    {
        public int user_id { get; set; }

        public string user_email { get; set; }

        public string conversation_id { get; set; }

        public string created_at { get; set; }
        
        public string body { get; set; }
    }
}
