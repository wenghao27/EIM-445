using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EIMv1.Models;
using EIMv1.ViewModels;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EIMv1.Controllers
{
    [Route("api/[controller]")]
    public class MessageDataController : Controller
    {
        private readonly IUserRepository _userRepository;

        public MessageDataController(IUserRepository userRepository)
        {
            _userRepository = userRepository;  
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<MessageViewModel>> GetMessages(string id)
        {
            string token = Request.Cookies["ACCESS_TOKEN"];

            UserList userList = await _userRepository.usersAsync(token);
            User user = userList.users.Find(x => x.last_name == id);

            MessageList messageList = await _userRepository.getMessages(token, user.last_name);

            List<MessageViewModel> messageViewModels = new List<MessageViewModel>();

            foreach (var message in messageList.messages)
            {
                messageViewModels.Add(MapRepoMessagesToMessageViewModels(message));
            }
            return messageViewModels;
        }

        private MessageViewModel MapRepoMessagesToMessageViewModels(Message message)
        {
            return new MessageViewModel()
            {
                user_email = message.user_email,
                conversation_id = message.conversation_id,
                created_at = message.created_at,
                user_id = message.user_id,
                body = message.body
            };
        }

        // POST api/values
        [HttpPost]
        public async Task<String> Post([FromBody]SendMessageViewModel message)
        {
            string token = Request.Cookies["ACCESS_TOKEN"];
            SendMessage sendMessage = new SendMessage();
            sendMessage.body = message.body;
            sendMessage.to = message.to;
            string response = await _userRepository.SendMessage(token, sendMessage);
            return response;
        }
    }
    
}
