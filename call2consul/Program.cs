using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Consul4NetCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace call2consul
{
    public class Program
    {
        private static string defaultHost = "http://*:5000";
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddConsulFile("consulsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            var host = configuration["Host"] ?? defaultHost;
            return WebHost.CreateDefaultBuilder(args)
                  .UseStartup<Startup>()
                  .UseUrls(host)
                  .Build();
        }
    }
}
