using Dominio.Modelo;
using Dominio.Repositorio;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

namespace Infraestructura.Controladores.Inventarios
{
    [Route("api/[controller]")]
    [Authorize]
    public class NotificacionController : Controller
    {
        private readonly RepoNotificacion repo = new RepoNotificacion();

        [HttpGet]
        public IEnumerable<Notificacion> Listar()
        {
            return repo.Listar();
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

        [HttpPost]
        public IActionResult Insertar([FromBody] Notificacion datos)
        {
            if (repo.Insertar(datos))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Notificacion datos)
        {
            if (repo.PorId(id) is Notificacion)
            {
                datos.Id = id;
                if (repo.Editar(datos))
                {
                    return Accepted();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Notificacion notificacion = repo.PorId(id);
            if (notificacion is Notificacion)
            {
                if (repo.Eliminar(notificacion))
                {
                    return Accepted();
                }
            }
            return BadRequest();
        }
    }
}
