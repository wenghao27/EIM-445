using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl;
using System.Diagnostics;
using EIMv1.ViewModels;

namespace EIMv1.Models
{
    public class UserRepository : IUserRepository
    {

        private static string baseUrl = "https://chronoschat.co/";

        private UserList _users;
    

        public async Task<UserList> usersAsync(string token)
        {
             var userList = await baseUrl
                .AppendPathSegment("conversations")
                .AppendPathSegment("index")
                .WithHeader("Authorization", token)
                .GetAsync()
                .ReceiveJson<UserList>();
            return userList;
            
        }


        public async Task <string> createConversation(string token, string id)
        {
            string email = await conversationIdHelper(token, id);
            var postBody = new { recipient_email = email };
            var conversationId = await baseUrl
                .AppendPathSegment("conversations")
                .AppendPathSegment("create")
                .WithHeader("Authorization", token)
                .PostJsonAsync(postBody)
                .ReceiveJson<ConversationID>();
            return conversationId.id;
        }

        //id currently last name
        public async Task<MessageList> getMessages(string token, string id)
        {
            string convoId = await createConversation(token, id);

            MessageList messageList = await baseUrl
                .AppendPathSegment("messages")
                .AppendPathSegment("index")
                .SetQueryParam("conversation_id", convoId)
                .WithHeader("Authorization", token)
                .GetAsync()
                .ReceiveJson<MessageList>();

            
            if(!messageList.messages.Any())
            {
                messageList = new MessageList();
                messageList.messages = Enumerable.Empty<Message>().ToList();
                Debug.WriteLine("no messages" + messageList.messages.Any());
            }
            else
            {
                Debug.WriteLine("yes messages " + messageList.messages.Any());
            }
            return messageList;
        }

        private async Task<string> conversationIdHelper(string token, string id)
        {
            Debug.WriteLine("in conversation helper");
            
            _users = await usersAsync(token);
            Debug.WriteLine("in conversion helper " + _users.users == null);
            User user = _users.users.Find(x => x.last_name == id);
            Debug.WriteLine("last name is " + user.last_name);

            //_users.users.Where(x => x.last_name.Equals(id)).FirstOrDefault();
            return user.email;


        } 

        public async void SendMessage(string token, SendMessage messsage)
        {
            Debug.WriteLine("SendMessage() id is " + messsage.to);

            string convoId = await createConversation(token, messsage.to);
            Debug.WriteLine("convoid is " + convoId);
            Debug.WriteLine("body is " + messsage.body);
            var jsonMessage = new { body = messsage.body, conversation_id = convoId };
            var returnString = await baseUrl
                .AppendPathSegment("messages")
                .AppendPathSegment("create")
                .WithHeader("Authorization", token)
                .PostJsonAsync(jsonMessage)
                .ReceiveString();
            Debug.WriteLine(returnString);
        }


    }
}
