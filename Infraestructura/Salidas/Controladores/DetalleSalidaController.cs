using System.Collections.Generic;
using System.Linq;
using Dominio.Salidas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Controladores.Inventarios
{
    [Route("api/[controller]")]
    [Authorize(Roles = "logistica")]
    public class DetalleSalidaController : Controller
    {
        private readonly RepositorioDetalleSalida repositorio;

        public DetalleSalidaController()
        {
            repositorio = new RepositorioDetalleSalida();
        }

        [HttpGet]
        public IEnumerable<DetalleSalida> Listar()
        {
            return repositorio.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<DetalleSalida> Obtener(int id)
        {
            DetalleSalida detalle = repositorio.PorId(id);

            if (detalle is DetalleSalida)
            {
                return detalle;
            }
            return NotFound();
        }

        [HttpGet("salida/{salidaId}")] // GET /api/detalleEntrada/entrada/5
        public IEnumerable<DetalleSalida> ListarPorSalida(int salidaId)
        {
            IEnumerable<DetalleSalida> lista = repositorio.Listar();

            return
                from detalle in lista
                where detalle.Salida == salidaId
                select detalle;
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] DetalleSalida datos)
        {
            if (repositorio.Insertar(datos))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] DetalleSalida datos)
        {
            if (repositorio.PorId(id) is DetalleSalida)
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
            DetalleSalida detalle = repositorio.PorId(id);
            if (detalle is DetalleSalida)
            {
                if (repositorio.Eliminar(detalle))
                {
                    return Accepted();
                }
            }
            return BadRequest();
        }
    }
}
