using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pegasus.Extension;

namespace Pegasus {
	public class Startup {
		public void ConfigureServices(IServiceCollection services)
			=> services.AddControllers();

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseMiddleware<ErroresMiddleware>();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
