using Centaurus.Modelo;
using Centaurus.Repositorio;
using Corvus.Modelo.Sesiones;
using Corvus.Seguridad;
using Corvus.Servicio.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pegasus.Extension;
using System;

namespace Pegasus.Controlador.Usuarios {
	using FormularioReestablecer = Corvus.Modelo.Formularios.FormularioReestablecerClave;

	[Route("api/[controller]")]
	public class SesionController: Controller {
		[HttpPost]
		public IActionResult Iniciar([FromBody] Credencial credencial) {
			var servicio = new ServicioSesion();

			if (servicio.Generar(credencial) is Sesion sesion) {
				var cookies = HttpContext.Response.Cookies;
				var proveedor = new ProveedorJWT();

				cookies.Append("token", proveedor.Encriptar(sesion));

				return Accepted();
			}

			return BadRequest();
		}

		#region Reestablecer clave

		[HttpGet("reestablecer")]
		public IActionResult PuedeReestablecer([FromBody] FormularioReestablecer formulario) {
			var servicio = new ServicioReestablecerClave();
			
			if (servicio.UsuarioDesdeFormulario(formulario) is Usuario) {
				return Accepted();
			}

			return BadRequest();
		}

		[HttpPost("reestablecer")]
		public IActionResult ReestablecerClave([FromBody] FormularioReestablecer formulario) {
			var servicio = new ServicioReestablecerClave();

			if (servicio.ReestablecerClave(formulario)) {
				return Accepted();
			}

			return BadRequest();
		}
		#endregion

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
