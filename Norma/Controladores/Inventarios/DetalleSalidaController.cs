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
    public class DetalleSalidaController : Controller
    {
        private readonly RepoDetalleSalida repo = new RepoDetalleSalida();

        [HttpGet]
        public IEnumerable<DetalleSalida> Listar()
        {
            return repo.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<DetalleSalida> Obtener(int id)
        {
            var detalle = repo.PorId(id);

            if (detalle is DetalleSalida)
            {
                return detalle;
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody]DetalleSalida datos)
        {
            if (repo.Insertar(datos))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody]DetalleSalida datos)
        {
            if (repo.PorId(id) is DetalleSalida)
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
            var detalle = repo.PorId(id);
            if (detalle is DetalleSalida)
            {
                if (repo.Eliminar(detalle))
                {
                    return Accepted();
                }
            }
            return BadRequest();
        }
    }
}
