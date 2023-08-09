using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using OwnYITCSAT.DataAccessLayer;
using OwnYITCSAT.Models;
using OwnYITCSAT.Utility;

namespace OwnYITCSAT
{
    //This is OwnYITCSAT_V3 Project
    public class Startup
    {
        //private readonly IConfigurationRoot _appConfiguration;
        private readonly IHostingEnvironment _hostingEnvironment;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _hostingEnvironment = env;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {      
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //var type= OSPlatform.Linux;
                if (OwnYITConstant.LINUX_WWW_PATH == "")
                    OwnYITConstant.LINUX_WWW_PATH = _hostingEnvironment.WebRootPath;
                //var architectureFolder = (IntPtr.Size == 8) ? "64 bit" : "32 bit";
                var wkHtmlToPdfPath = Path.Combine(_hostingEnvironment.ContentRootPath, $"wwwroot/Utility/v0.12.4/64 bit/libwkhtmltox");
                OwnYITConstant.PDF_LOGO_PATH_LEFT = Path.Combine(_hostingEnvironment.ContentRootPath, $"wwwroot/images/Setting/NSGLogoLeft.png");
                OwnYITConstant.PDF_LOGO_PATH_RIGHT = Path.Combine(_hostingEnvironment.ContentRootPath, $"wwwroot/images/Setting/NSGLogoRight.png");
                CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
                context.LoadUnmanagedLibrary(wkHtmlToPdfPath);
                services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var type = OSPlatform.Windows;
                var architectureFolder = (IntPtr.Size == 8) ? "64 bit" : "32 bit";
                var wkHtmlToPdfPath = Path.Combine(_hostingEnvironment.ContentRootPath, $"wwwroot\\Utility\\v0.12.4\\{architectureFolder}\\libwkhtmltox");
                OwnYITConstant.PDF_LOGO_PATH_LEFT = Path.Combine(_hostingEnvironment.ContentRootPath, $"wwwroot\\images\\Setting\\NSGLogoLeft.png");
                OwnYITConstant.PDF_LOGO_PATH_RIGHT = Path.Combine(_hostingEnvironment.ContentRootPath, $"wwwroot\\images\\Setting\\NSGLogoRight.png");
                CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
                context.LoadUnmanagedLibrary(wkHtmlToPdfPath);
                services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            }            
            //Other codes...           
            services.AddDirectoryBrowser();
            services.AddMvc();           
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);//You can set Time   
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        //public void Configure(IApplicationBuilder app, IHostingEnvironment env) // for windows
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) // for linix
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            // for Linux
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            // for Linux end
            app.UseStaticFiles(); // For the wwwroot folder
            app.UseSession();
            
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                RequestPath = "/wwwroot"
            });

            if (OwnYITConstant.LINUX_ROOT_PATH == "")
                OwnYITConstant.LINUX_ROOT_PATH = env.ContentRootPath;

            if (OwnYITConstant.LINUX_WWW_PATH == "")
                OwnYITConstant.LINUX_WWW_PATH = env.WebRootPath;
            // string sAppPath = env.ContentRootPath; //Application Base Path
            //string swwwRootPath = env.WebRootPath;  //wwwroot folder path
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=csat_login}/{action=csat_login}/{id?}");
            });

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                context.Response.Headers.Add("Feature-Policy",
                "vibrate 'self' ; " +
                "camera 'self' ; " +
                "microphone 'self' ; " +
                "speaker 'self' ;" +
                "geolocation 'self' ; " +
                "gyroscope 'self' ; " +
                "magnetometer 'self' ; " +
                "midi 'self' ; " +
                "sync-xhr 'self' ; " +
                "push 'self' ; " +
                "notifications 'self' ; " +
                "fullscreen '*' ; " +
                "payment 'self' ; ");

                context.Response.Headers.Add(
                "Content-Security-Policy-Report-Only",
                "default-src 'self'; " +
                "script-src-elem 'self'" +
                "style-src-elem 'self'; " +
                "img-src 'self'; " +
                "font-src 'self'" +
                "media-src 'self'" +
                "frame-src 'self'" +
                "connect-src "

                );
                await next();
            });

            app.UseMvc();

            app.UseCookiePolicy(
            new CookiePolicyOptions
            {
                Secure = CookieSecurePolicy.Always
            });
        }
    }
}
