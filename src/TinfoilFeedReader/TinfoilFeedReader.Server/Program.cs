using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TinfoilFeedReader.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile("appsettings.{Environment}.json", true)
                    .Build())
                .UseStartup<Startup>()
                .Build();
    }
}
