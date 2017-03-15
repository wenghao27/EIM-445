using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMv1.Models
{
    public class FakeUserRepository : IUserRepository
    {
        public IEnumerable<User> Users
        {

            //in real repo we would make the call to api to get list of users
            get
            {
                return new List<User>
                {
                    new User{Name = "Hao W.", Email="sample1@email.com"},
                    new User{Name = "Byran C.", Email="sample2@email.com"},
                    new User{Name = "Alex B.", Email="sample3@email.com"}
                };
            }
        }
    }
}
