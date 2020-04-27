using Centaurus.Seguridad;
using Corvus.Caso.Proceso;
using Corvus.Modelo.Sesiones;
using Microsoft.AspNetCore.Mvc;
using Pegasus.Extension;

namespace Pegasus.Controlador.Usuarios {
	[Route("api/[controller]")]
	public class SesionController: Controller {
		[HttpPost]
		public IActionResult Post([FromBody] Credencial credencial) {
			var proceso = new ProcesoSesion();

			if (proceso.Generar(credencial) is Sesion sesion) {
				var cookies = HttpContext.Response.Cookies;
				var proveedor = new ProveedorToken();

				cookies.Append("token", proveedor.Encriptar(sesion));

				return Ok("sesion iniciada");
			}

			return BadRequest("revisa tus credenciales");
		}

		[HttpDelete]
		[Autenticado]
		public IActionResult Delete() {
			HttpContext.Response.Cookies.Delete("token");
			return Ok("sesion finalizada");
		}
	}
}
