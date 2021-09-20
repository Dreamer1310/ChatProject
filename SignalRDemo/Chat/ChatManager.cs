using SignalRDemo.Communication.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRDemo.Chat
{
    class ChatManager
    {
        private static readonly List<ChatUser<IChatClient>> _onlineUsers = new List<ChatUser<IChatClient>>();
        internal static List<ChatUser<IChatClient>> OnlineUsers => _onlineUsers;

        internal static void AddOnlineUser(ChatUser<IChatClient> user)
        {
            _onlineUsers.Add(user);
        }

        internal static void RemoveUserById(Int64 ID)
        {
            var user = GetUserById(ID);
            if (user != null) _onlineUsers.Remove(user);
        }

        internal static ChatUser<IChatClient> GetUserById(Int64 userID)
        {
            return _onlineUsers.FirstOrDefault(x => x.ID == userID);
        }
    }
}
