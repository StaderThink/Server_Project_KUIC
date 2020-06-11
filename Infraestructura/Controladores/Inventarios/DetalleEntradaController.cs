using Dominio.Modelo;
using Dominio.Repositorio;

using Infraestructura.Extensiones;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;

namespace Infraestructura.Controladores.Inventarios {
    [Route("api/[controller]")]
    [Autenticado]
    public class DetalleEntradaController : Controller {
        private readonly RepoDetalleEntrada repo = new RepoDetalleEntrada();

        [HttpGet]
        public IEnumerable<DetalleEntrada> Listar() {
            return repo.Listar();
        }

        [HttpGet("{id}")] // GET /api/detalleEntrada/1
        public ActionResult<DetalleEntrada> Obtener(int id) {
            DetalleEntrada detalle = repo.PorId(id);

            if (detalle is DetalleEntrada) {
                return detalle;
            }
            return NotFound();
        }

        [HttpGet("entrada/{entradaId}")] // GET /api/detalleEntrada/entrada/5
        public IEnumerable<DetalleEntrada> ListarPorEntrada(int entradaId) {
            IEnumerable<DetalleEntrada> lista = repo.Listar();

            return
                from detalle in lista
                where detalle.Entrada == entradaId
                select detalle;
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] DetalleEntrada datos) {
            if (repo.Insertar(datos)) {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] DetalleEntrada datos) {
            if (repo.PorId(id) is DetalleEntrada) {
                datos.Id = id;
                if (repo.Editar(datos)) {
                    return Accepted();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            DetalleEntrada detalle = repo.PorId(id);
            if (detalle is DetalleEntrada) {
                if (repo.Eliminar(detalle)) {
                    return Accepted();
                }
            }
            return BadRequest();
        }
    }
}
