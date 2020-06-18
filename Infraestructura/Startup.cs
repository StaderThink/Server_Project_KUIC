using Blazored.LocalStorage;

using Infraestructura.Autenticacion;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Net.Http;
using System.Text;

namespace Infraestructura
{
    public class Startup
    {
        public const string CookieScheme = "CookieScheme";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void ConfigurarAutenticacion(JwtBearerOptions opciones)
        {
            SymmetricSecurityKey llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TOKEN")));

            opciones.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Environment.GetEnvironmentVariable("TOKEN"),
                ValidAudience = Environment.GetEnvironmentVariable("TOKEN"),
                IssuerSigningKey = llave
            };
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRazorPages().WithRazorPagesRoot("/Vistas");
            services.AddServerSideBlazor();
            services.AddBlazoredLocalStorage();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(ConfigurarAutenticacion);

            services.AddSingleton(service => // agregar cliente http
                new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:5001")
                }
            );

            services.AddScoped<AuthenticationStateProvider, BlazorAuthenticationProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
