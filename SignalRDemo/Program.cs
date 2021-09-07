using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace SignalRDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<StartUp>();
        }
    }
}
