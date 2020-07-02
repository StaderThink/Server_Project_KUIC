using Dominio.Modelo;
using Dominio.Repositorio;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;

namespace Infraestructura.Controladores.Inventarios
{
    [Route("api/[controller]")]
    [Authorize(Roles = "logistica")]
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
            DetalleSalida detalle = repo.PorId(id);

            if (detalle is DetalleSalida)
            {
                return detalle;
            }
            return NotFound();
        }

        [HttpGet("salida/{salidaId}")] // GET /api/detalleEntrada/entrada/5
        public IEnumerable<DetalleSalida> ListarPorSalida(int salidaId)
        {
            IEnumerable<DetalleSalida> lista = repo.Listar();

            return
                from detalle in lista
                where detalle.Salida == salidaId
                select detalle;
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] DetalleSalida datos)
        {
            if (repo.Insertar(datos))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] DetalleSalida datos)
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
            DetalleSalida detalle = repo.PorId(id);
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
