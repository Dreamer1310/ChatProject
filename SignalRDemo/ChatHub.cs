using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRDemo
{
    class ChatHub : Hub
    {
        public Task Send(String user, String msg)
        {
            Console.WriteLine(user + " " + msg);
            return Clients.All.SendAsync("Recieve", user, msg);
        }

        public override Task OnConnectedAsync()
        {
            Send("Luka", "Hi There");
            return base.OnConnectedAsync();
        }
    }
}
