using Centaurus.Modelo;
using Corvus.Modelo.Sesiones;
using Corvus.Servicio.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Norma.Extensiones;
using System;

namespace Norma.Controladores.Usuarios {
	[Route("api/[controller]")]
	public class SesionController: Controller {
		[HttpPost]
		public IActionResult IniciarSesion([FromBody] Credencial credencial) {
			var sesion = new Sesion {
				Fecha = DateTime.Now,
				Credencial = credencial
			};

			var servicio = new ServicioSesion();

			if (servicio.Generar(sesion) is string token) {
				return Ok(token);
			}

			return BadRequest();
		}

		[Autenticado]
		[HttpGet]
		public ActionResult<Usuario> QuienSoy() {
			object carga = HttpContext.Items["usuario"];

			if (carga is Usuario usuario) return usuario;
			else return NoContent();
		}
	}
}
