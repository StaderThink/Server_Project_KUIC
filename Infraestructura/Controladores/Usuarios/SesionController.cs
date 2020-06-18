using Aplicacion.Modelo.Sesiones;
using Aplicacion.Seguridad;
using Aplicacion.Servicio.Usuarios;

using Dominio.Modelo;
using Dominio.Repositorio;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Security.Claims;

namespace Infraestructura.Controladores.Usuarios
{
    [Route("api/[controller]")]
    public class SesionController : ControllerBase
    {
        [HttpPost]
        public IActionResult IniciarSesion([FromBody] Credencial credencial)
        {
            var servicio = new ServicioSesion();

            if (servicio.ValidarCredencial(credencial) is Usuario usuario)
            {
                var criptografo = new ProveedorJWT();
                var identidad = servicio.GenerarIdentidad(usuario);

                return Ok(criptografo.GenerarToken(identidad));
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        public IActionResult VerificarSesion()
        {
            var cargaDocumento = HttpContext.User.Claims.First(carga => carga.Type == ClaimTypes.Dns);
            var documento = cargaDocumento.Value;

            var repo = new RepoUsuario();

            if (repo.PorDocumento(documento) is Usuario usuario)
            {
                return Ok(usuario);
            }

            return BadRequest();
        }
    }
}
