using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ClientSignalRDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var conn = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/chatHub")
                .Build();

            conn.StartAsync().Wait();
            conn.InvokeCoreAsync("Send", args: new[] { "Luka", "HI!" });
            conn.On("Recieve", (String user, String msg) =>
            {
                Console.WriteLine(user + " " + msg);
            });

            Console.ReadKey();
        }
    }
}
