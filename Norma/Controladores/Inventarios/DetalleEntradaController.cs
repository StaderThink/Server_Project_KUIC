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
    public class DetalleEntradaController : Controller
    {
        private readonly RepoDetalleEntrada repo = new RepoDetalleEntrada();

        [HttpGet]
        public IEnumerable<DetalleEntrada> Listar()
        {
            return repo.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<DetalleEntrada> Obtener(int id)
        {
            var detalle = repo.PorId(id);

            if (detalle is DetalleEntrada)
            {
                return detalle;
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody]DetalleEntrada datos)
        {
            if (repo.Insertar(datos))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody]DetalleEntrada datos)
        {
            if (repo.PorId(id) is DetalleEntrada)
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
            if (detalle is DetalleEntrada)
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
