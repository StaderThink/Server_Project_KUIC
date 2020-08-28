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
            IHostBuilder host = Host.CreateDefaultBuilder(args);
            IHostBuilder builder = host.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

            return builder;
        }
    }
}
