using System.Collections.Generic;
using System.Security.Claims;
using Aplicacion.Notificaciones;
using Dominio.Notificaciones;
using Dominio.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Controladores.Inventarios
{
    [Route("api/[controller]")]
    public class NotificacionController : Controller
    {
        private readonly RepositorioNotificacion repo;

        public NotificacionController()
        {
            repo = new RepositorioNotificacion();
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var sesion = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.SerialNumber);
                var idUsuario = int.Parse(sesion.Value);

                var repositorioUsuario = new RepositorioUsuario();

                if (repositorioUsuario.PorId(idUsuario) is Usuario usuario)
                {
                    var listado = repo.PorCargo(usuario.Cargo);
                    return Ok(listado);
                }

                else
                {
                    return NotFound();
                }
            }

            catch
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpGet("autor")]
        public IActionResult ListarPorAutor()
        {
            try
            {
                var sesion = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.SerialNumber);
                var idUsuario = int.Parse(sesion.Value);

                var repositorioUsuario = new RepositorioUsuario();

                if (repositorioUsuario.PorId(idUsuario) is Usuario usuario)
                {
                    var listado = repo.PorAutor(usuario.Id);
                    return Ok(listado);
                }

                else
                {
                    return NotFound();
                }
            }

            catch
            {
                return Unauthorized();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Notificacion> Obtener(int id)
        {
            Notificacion notificacion = repo.PorId(id);

            if (notificacion is Notificacion)
            {
                return notificacion;
            }
            return NotFound();
        }

        [HttpGet("autor/{idAutor}")]
        public IEnumerable<Notificacion> ObtenerAutor(int idAutor)
        {
            return repo.PorAutor(idAutor);
        }

        [Authorize(Roles = "usuarios")]
        [HttpPost]
        public IActionResult Insertar([FromBody] FormularioRegistrarNotificacion formulario)
        {
            ServicioRegistradorNotificacion servicio = new ServicioRegistradorNotificacion();

            if (servicio.Registrar(formulario))
            {
                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = "usuarios")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Notificacion notificacion = repo.PorId(id);
            if (notificacion is Notificacion)
            {
                ServicioEliminadorNotificacion servicio = new ServicioEliminadorNotificacion();
                if (servicio.Eliminar(notificacion))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
