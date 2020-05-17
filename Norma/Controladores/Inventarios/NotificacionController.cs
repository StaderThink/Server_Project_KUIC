using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Centaurus.Modelo;
using Centaurus.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Norma.Extensiones;

namespace Norma.Controladores.Inventarios
{
    [Route("api/[controller]")]
    [Autenticado]
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
            var notificacion = repo.PorId(id);

            if (notificacion is Notificacion)
            {
                return notificacion;
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody]Notificacion datos)
        {
            if (repo.Insertar(datos))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody]Notificacion datos)
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
            var notificacion = repo.PorId(id);
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
