using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace BusinessAdministration.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            IWebHostEnvironment? hostEnvironment = null;

            return WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureServices(services =>
                {
                    hostEnvironment = services
                    .Where(x => x.ServiceType == typeof(IWebHostEnvironment))
                    .Select(x => (IWebHostEnvironment)x.ImplementationInstance)
                    .First();
                })
                .UseStartup<Startup>();
        }
    }
}
