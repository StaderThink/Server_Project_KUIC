using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using dotenv.net;

namespace Infraestructura {
	public class Program {
		public static void Main(string[] args) {
			DotEnv.Config(); // injectar el .env globalmente
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) {
			var host = Host.CreateDefaultBuilder(args);
			var builder = host.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

			return builder;
		}
	}
}
