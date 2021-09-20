using System;
using System.Threading.Tasks;

namespace SignalRDemo.Communication.Clients
{
    internal interface IChatClient
    {
        internal void Send(String user, String msg);
    }
}