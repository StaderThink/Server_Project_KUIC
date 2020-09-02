using dotenv.net;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Infraestructura
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            DotEnv.Config(); // injectar el .env globalmente
#endif
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args);
            var builder = host.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

            return builder;
        }
    }
}
