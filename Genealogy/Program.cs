
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SpaPrerendering
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //DataBaseAccess.CreateDB();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
    }
}