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
    public class SalidaController : Controller
    {
        private readonly RepoSalida repo = new RepoSalida();

        [HttpGet]
        public IEnumerable<Salida> Listar()
        {
            return repo.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<Salida> Obtener(int id)
        {
            var salida = repo.PorId(id);

            if (salida is Salida)
            {
                return salida;
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody]Salida datos)
        {
            if (repo.Insertar(datos))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody]Salida datos)
        {
            if (repo.PorId(id) is Salida)
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
            var salida = repo.PorId(id);
            if (salida is Salida)
            {
                if (repo.Eliminar(salida))
                {
                    return Accepted();
                }
            }
            return BadRequest();
        }
    }
}
