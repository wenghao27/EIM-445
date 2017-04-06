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

        //// GET: api/values
        //[HttpGet]
        //public async Task<IEnumerable<UserViewModel>> LoadUsers() 
        //{
        //    string token = Request.Cookies["ACCESS_TOKEN"];

        //    UserList userList = await _userRepository.usersAsync(token);
        //    List<UserViewModel> users = new List<UserViewModel>();

        //    foreach (var user in userList.users)
        //    {
        //        users.Add(MapRepoUsersToUserViewModel(user));
        //    }
        //    return users;
        //}

        //private UserViewModel MapRepoUsersToUserViewModel(User user)
        //{   
            
        //    return new UserViewModel()
        //    {
        //        first_name = user.first_name,
        //        last_name = user.last_name,
        //        email = user.email
        //    };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public async Task<IEnumerable<MessageViewModel>> GetMessages(string id)
        //{
            
        //    string token = Request.Cookies["ACCESS_TOKEN"];

        //    UserList userList = await _userRepository.usersAsync(token);
        //    User user = userList.users.Find(x => x.last_name.Equals(id));
        //    string conversationID = await _userRepository.createConversation(token, user.email);
        

        //    MessageList messageList = await _userRepository.getMessages(token, conversationID);

        //    List<MessageViewModel> messageViewModels = new List<MessageViewModel>();

        //    foreach(var message in messageList.messages)
        //    {
        //        messageViewModels.Add(MapRepoMessagesToMessageViewModels(message));
        //    }
        //    return messageViewModels;
        //    //id is the user id
        //    //serach our users to
        //    //find the right user using the id
        //    //    creat a convo
        //    //    get the convo id
        //    //    get the messagesand returnthem
        //}

        //private MessageViewModel MapRepoMessagesToMessageViewModels(Message message)
        //{
        //    return new MessageViewModel()
        //    {
        //        //user_email = message.user_email,
        //        //conversation_id = message.conversation_id,
        //        //created_at = message.created_at,
        //        //user_id = message.user_id
        //    };
        //}

        // POST api/values
        [HttpPost]
        public void Post(SendMessageViewModel value)
        {
            //TODO: currently not giving the right values
            Debug.WriteLine("ss" + value.body + " In Post\n\n" + value.to);
            //encrypt and call api
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    
}
