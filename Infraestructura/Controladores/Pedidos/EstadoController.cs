using Dominio.Modelo;
using Dominio.Repositorio;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;

namespace Infraestructura.Controladores.Pedidos
{
    [Authorize(Roles = "pedidos")]
    [Route("api/[controller]")]
    public class EstadoController : Controller
    {
        private readonly RepoEstado repositorio = new RepoEstado();

        [HttpGet]
        public IEnumerable<Estado> Listar()
        {
            return repositorio.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<Estado> Obtener(int id)
        {
            if (repositorio.PorId(id) is Estado estado)
            {
                return estado;
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] Estado estado)
        {
            if (repositorio.Insertar(estado))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Estado estado)
        {
            if (repositorio.PorId(id) is Estado)
            {
                estado.Id = id;

                if (repositorio.Editar(estado))
                {
                    return Ok();
                }

                return BadRequest();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            if (repositorio.PorId(id) is Estado estado)
            {
                if (repositorio.Eliminar(estado))
                {
                    return Ok();
                }

                else
                    return BadRequest();
            }

            return NotFound();
        }
    }
}
