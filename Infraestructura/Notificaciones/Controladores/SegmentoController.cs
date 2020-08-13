using System.Collections.Generic;
using System.Linq;
using Dominio.Notificaciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Controladores.Inventarios
{
    [Route("api/[controller]")]
    [Authorize]
    public class SegmentoController : Controller
    {
        private readonly RepositorioSegmento repositorio;

        public SegmentoController()
        {
            repositorio = new RepositorioSegmento();
        }

        [HttpGet]
        public IEnumerable<Segmento> Listar()
        {
            return repositorio.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<Segmento> Obtener(int id)
        {
            Segmento segmento = repositorio.PorId(id);

            if (segmento is Segmento)
            {
                return segmento;
            }
            return NotFound();
        }

        [HttpGet("notificacion/{notificacionId}")]
        public IEnumerable<Segmento> ListarPorNotificacion(int notificacionId)
        {
            var lista = repositorio.PorNotificacion(notificacionId);
            return lista.Where(s => s.Notificacion == notificacionId);
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] Segmento datos)
        {
            if (repositorio.Insertar(datos))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Segmento datos)
        {
            if (repositorio.PorId(id) is Segmento)
            {
                datos.Id = id;
                if (repositorio.Editar(datos))
                {
                    return Accepted();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Segmento segmento = repositorio.PorId(id);
            if (segmento is Segmento)
            {
                if (repositorio.Eliminar(segmento))
                {
                    return Accepted();
                }
            }
            return BadRequest();
        }
    }
}
