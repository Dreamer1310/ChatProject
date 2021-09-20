using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRDemo
{
    internal class ChatUser<IChatClient>
    {
        internal Int64 ID { get; set; }
        internal String Username { get; set; }
        internal IChatClient Client { get; set; }
        internal String ConnectionID { get; set; }
    }
}
