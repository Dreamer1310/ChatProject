using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Chat;
using SignalRDemo.Communication.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRDemo.Communication
{
    internal class ChatHub : Hub<IChatClient>
    {
        public void Send(String user, String msg)
        {
            Console.WriteLine(user + " " + msg);
            Clients.All.Send(user, msg);
        }

        public override Task OnConnectedAsync()
        {
            HandleConnectedUser();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var ID = Int64.Parse(Context.UserIdentifier);
                ChatManager.RemoveUserById(ID);
            }
            catch (Exception)
            {

                throw new Exception("Incorrect User ID... ChatHub.OnDisconnectedAsync");
            }
            return base.OnDisconnectedAsync(exception);
        }

        private void HandleConnectedUser()
        {

            try
            {
                var ID = Int64.Parse(Context.UserIdentifier);

                var user = ChatManager.GetUserById(ID);
                if (user != null)
                {
                    user.ConnectionID = Context.ConnectionId;
                    user.Client = Clients.Caller;
                }
                else
                {
                    var newUser = new ChatUser<IChatClient>
                    {
                        ID = ID,
                        Username = "SmokeChungas",
                        Client = Clients.Caller,
                        ConnectionID = Context.ConnectionId
                    };

                    ChatManager.AddOnlineUser(newUser);
                }
            }
            catch (Exception)
            {

                throw new Exception("Incorrect User ID... ChatHub.OnConnectedAsync.HandleConnectedUser");
            }
        }
    }
}
