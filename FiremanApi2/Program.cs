using System;

using FiremanApi2.DataBase;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FiremanApi2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<FireContext>();
                    DataBaseInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while seeding database.");
                }
            }

            host.Run();

        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var webhost =  WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .Build();
            return webhost;
        }
            
    }
}
