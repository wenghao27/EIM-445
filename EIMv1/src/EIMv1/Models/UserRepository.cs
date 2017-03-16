using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl;
using System.Diagnostics;

namespace EIMv1.Models
{
    public class UserRepository : IUserRepository
    {

        private static string baseUrl = "https://chronoschat.co/";
        private string _token;

        public UserRepository()
        {
        }

        public async Task<UserList> usersAsync(string token)
        {
            Debug.WriteLine("USER REPOSITORY b");
             var userList = await baseUrl
                .AppendPathSegment("conversations")
                .AppendPathSegment("index")
                .WithHeader("Authorization", token)
                .GetAsync()
                .ReceiveJson<UserList>();
            return userList;
            
        }

       

    }
}
