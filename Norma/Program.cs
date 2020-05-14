using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using dotenv.net;

namespace Norma {
	public class Program {
		public static void Main(string[] args) {
			DotEnv.Config();
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => {
					webBuilder.UseStartup<Startup>();
				});
	}
}
