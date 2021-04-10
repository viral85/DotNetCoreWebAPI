using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MyTestProjectAPI
{
    /// <summary>
    /// Program Class
    /// </summary>
    public class Program
    {
        static String env = "";
        /// <summary>
        /// Main Method
        /// </summary>
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            env = config.GetSection("Environment").Value;
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// CreateHostBuilder 
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((a, config) =>
                {
                    config.AddJsonFile("appsettings.json");
                    config.AddJsonFile($"appsettings.{env}.json");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
