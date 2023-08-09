using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OwnYITCSAT.DataAccessLayer;
namespace OwnYITCSAT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .CaptureStartupErrors(true) // the default
                //.UseSetting("detailedErrors", "true")
                .UseIISIntegration()
                .UseStartup<Startup>()
                //.UseUrls("http://localhost:5002") // for linux
                .Build();


        //public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        //{
        //   return WebHost.CreateDefaultBuilder(args)
        //         .UseKestrel(opt =>
        //         {
        //             opt.AddServerHeader = false;
        //             opt.ConfigureHttpsDefaults(s =>
        //             {
        //                 s.SslProtocols = SslProtocols.Tls12;
        //             });
        //         })
        //         .ConfigureLogging(builder =>
        //         {
        //             builder.ClearProviders();
        //             builder.AddSerilog();
        //         })
        //        .UseStartup();
        //}
    }
}
