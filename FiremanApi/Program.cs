using System.IO;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace FiremanApi
{
    public static class Program
    {
        public static void Main(string[] args) => BuildWebHost(args).Start();

        public static IWebHost BuildWebHost(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                        .UseStartup<Startup>()
                        .ConfigureAppConfiguration((hostContext, config) =>
                        {
                            // delete all default configuration providers
                            config.Sources.Clear();
                        })
                        .Build();
    }
}
