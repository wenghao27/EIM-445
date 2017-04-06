using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMv1.Models
{
    public interface IUserRepository
    {
        Task<UserList> usersAsync(string token);
        Task<string> createConversation(string token, string email);
        Task<MessageList> getMessages(string token, string id);
    }
}
