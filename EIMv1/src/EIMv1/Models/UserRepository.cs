using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using System.Diagnostics;

namespace EIMv1.Models
{
    public class UserRepository : IUserRepository
    {

        private static string baseUrl = "https://chronoschat.co/";

        public async Task<UserList> usersAsync()
        {
            Debug.WriteLine("USER REPOSITORY b");
            var userList = await "https://chronoschat.co/conversations/index/"
                    .WithHeader("Authorization", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjozfQ.boMqb1bTwOD9tIwUdyw65SsKVBS62b8M7Kd126uBPNU")
                    .GetAsync()
                    .ReceiveJson<UserList>();
            return userList;
        }

       

    }
}
