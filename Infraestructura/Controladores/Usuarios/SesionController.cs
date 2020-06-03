using Dominio.Modelo;
using Aplicacion.Modelo.Formularios;
using Aplicacion.Modelo.Sesiones;
using Aplicacion.Servicio.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Infraestructura.Extensiones;
using System;

namespace Infraestructura.Controladores.Usuarios {
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

		[HttpPost("reestablecer")]
		public IActionResult ReestablecerClave([FromBody] FormularioReestablecerClave formulario) {
			var servicio = new ServicioReestablecerClave();
			
			if (servicio.ReestablecerClave(formulario)) return Ok();
			else return BadRequest();
		}
	}
}
