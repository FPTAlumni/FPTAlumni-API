using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace UniAlumni.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    // Port for http & https
                    webBuilder.UseUrls("http://*;https://*");
                });
    }
}