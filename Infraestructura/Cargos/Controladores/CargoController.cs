using System.Collections.Generic;
using Dominio.Cargos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Cargos.Controladores
{
    [Authorize]
    [Route("api/[controller]")]
    public class CargoController : Controller
    {
        private readonly RepositorioCargo repo;

        public CargoController()
        {
            repo = new RepositorioCargo();
        }

        [HttpGet]
        public IEnumerable<Cargo> Listar()
        {
            return repo.Listar();
        }

        [HttpPost]
        [Authorize(Roles = "usuarios")]
        public IActionResult Insertar([FromBody] Cargo datos)
        {
            if (repo.Insertar(datos))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public ActionResult<Cargo> Obtener(int id)
        {
            if (repo.PorId(id) is Cargo cargo)
            {
                return cargo;
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "usuarios")]
        public IActionResult Editar(int id, [FromBody] Cargo datos)
        {
            if (repo.PorId(id) is Cargo)
            {
                datos.Id = id;

                if (repo.Editar(datos))
                    return Ok();
                else
                    return BadRequest();
            }

            return NotFound();
        }
    }
}
