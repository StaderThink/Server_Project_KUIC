using Aplicacion.Modelo.Sesiones;
using Aplicacion.Servicio.Usuarios;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Controladores.Usuarios
{
    [Route("api/[controller]")]
    public class SesionController : ControllerBase
    {
        [HttpPost]
        public IActionResult IniciarSesion([FromBody] Credencial credencial)
        {
            ServicioSesion servicio = new ServicioSesion();

            if (servicio.GenerarToken(credencial) is string token)
            {
                return Ok(token);
            }

            return BadRequest();
        }

        [Authorize(Roles = "usuarios")]
        [HttpGet]
        public IActionResult VerificarSesion()
        {
            return Ok("Hola!");
        }
    }
}
