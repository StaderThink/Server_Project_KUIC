using Dominio.Modelo;
using Dominio.Repositorio;

using Infraestructura.Extensiones;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

namespace Infraestructura.Controladores.Inventarios {
    [Route("api/[controller]")]
    [Autenticado]
    public class DetalleSalidaController : Controller {
        private readonly RepoDetalleSalida repo = new RepoDetalleSalida();

        [HttpGet]
        public IEnumerable<DetalleSalida> Listar() {
            return repo.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<DetalleSalida> Obtener(int id) {
            DetalleSalida detalle = repo.PorId(id);

            if (detalle is DetalleSalida) {
                return detalle;
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] DetalleSalida datos) {
            if (repo.Insertar(datos)) {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] DetalleSalida datos) {
            if (repo.PorId(id) is DetalleSalida) {
                datos.Id = id;
                if (repo.Editar(datos)) {
                    return Accepted();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            DetalleSalida detalle = repo.PorId(id);
            if (detalle is DetalleSalida) {
                if (repo.Eliminar(detalle)) {
                    return Accepted();
                }
            }
            return BadRequest();
        }
    }
}
