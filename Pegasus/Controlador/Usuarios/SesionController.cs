using Centaurus.Modelo;
using Corvus.Caso.Proceso;
using Corvus.Modelo.Sesiones;
using Corvus.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Pegasus.Extension;

namespace Pegasus.Controlador.Usuarios {
	[Route("api/[controller]")]
	public class SesionController: Controller {
		[HttpPost]
		public IActionResult Iniciar([FromBody] Credencial credencial) {
			var proceso = new ProcesoSesion();

			if (proceso.Generar(credencial) is Sesion sesion) {
				var cookies = HttpContext.Response.Cookies;
				var proveedor = new ProveedorJWT();

				cookies.Append("token", proveedor.Encriptar(sesion));

				return Accepted();
			}

			return BadRequest();
		}

		[HttpDelete]
		[Autenticado]
		public IActionResult Cerrar() {
			HttpContext.Response.Cookies.Delete("token");
			return Accepted();
		}

		[HttpGet]
		[Autenticado]
		public ActionResult<Usuario> QuienSoy() {
			var carga = HttpContext.Items["usuario"];

			if (carga is Usuario usuario) return usuario;
			else return NoContent();
		}
	}
}
