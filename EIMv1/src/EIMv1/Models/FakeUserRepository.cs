        using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMv1.Models
{
    public class FakeUserRepository
    {
        public List<User> usersAsync
        {

            //in real repo we would make the call to api to get list of users
            get
            {
                return new List<User>
                {
                    new User{first_name = "Hao", last_name = "Weng", email="sample1@email.com"},
                    new User{first_name = "Byran", last_name="Castaneda", email="sample2@email.com"},
                    new User{first_name = "Alex", last_name = "Bastille", email="sample3@email.com"}
                };
            }
        }
    }
}
