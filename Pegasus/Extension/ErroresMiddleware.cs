using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Pegasus.Extension {
	public class ErroresMiddleware {
		private readonly RequestDelegate _next;

		private string EscribirLog(Exception error) {
			var rutaDirectorio = Path.GetFullPath(".log");

			Directory.CreateDirectory(rutaDirectorio);

			var rutaArchivo = rutaDirectorio + $"/{error.GetHashCode()}.log";
			var rutaFinal = Path.Combine(rutaDirectorio, rutaArchivo);

			string contenido = $"{DateTime.Now}\n{error}";

			File.AppendAllText(rutaFinal, contenido);
			return error.GetHashCode().ToString();
		}

		public ErroresMiddleware(RequestDelegate next) {
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context) {
			try {
				await _next(context); // esperar al controlador
			}

			catch (Exception ex) {
				string codigo = EscribirLog(ex);
				string mensaje = $"falla registrada #{codigo}";

				#if RELEASE
				mensaje = "hubo un error al procesar la petición";
				#endif

				context.Response.StatusCode = 500;
				await context.Response.WriteAsync(mensaje);
			}
		}
	}
}
