using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace School_Management_System_Backend
{
    public class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            logger.Trace("This is a trace message");
            logger.Debug("This is a debug message");
            logger.Info("This is an info message");
            logger.Warn("This is a warning message");
            logger.Error("This is an error message");
            logger.Fatal("This is a fatal message");

            // ...

            LogManager.Shutdown(); // Optional: Clean up and flush log messages
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
       
    }
}
