﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMv1.Models
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
    }
}