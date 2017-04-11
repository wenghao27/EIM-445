using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMv1.ViewModels;

namespace EIMv1.Models
{
    public interface IUserRepository
    {
        Task<UserList> usersAsync(string token);
        Task<string> createConversation(string token, string email);
        Task<MessageList> getMessages(string token, string id);
        Task<string> SendMessage(string token, SendMessage message);
    }
}
