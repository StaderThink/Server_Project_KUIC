using System.Collections.Generic;
using System.Linq;
using Dominio.Entradas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Entradas.Controladores
{
    [Route("api/[controller]")]
    [Authorize(Roles = "logistica")]
    public class DetalleEntradaController : Controller
    {
        private readonly RepositorioDetallEntrada repositorio = new RepositorioDetallEntrada();

        public DetalleEntradaController()
        {
            repositorio = new RepositorioDetallEntrada();
        }

        [HttpGet]
        public IEnumerable<DetalleEntrada> Listar()
        {
            return repositorio.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<DetalleEntrada> Obtener(int id)
        {
            DetalleEntrada detalle = repositorio.PorId(id);

            if (detalle is DetalleEntrada)
            {
                return detalle;
            }
            return NotFound();
        }

        [HttpGet("entrada/{entradaId}")]
        public IEnumerable<DetalleEntrada> ListarPorEntrada(int entradaId)
        {
            IEnumerable<DetalleEntrada> lista = repositorio.Listar();

            return
                from detalle in lista
                where detalle.Entrada == entradaId
                select detalle;
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] DetalleEntrada datos)
        {
            if (repositorio.Insertar(datos))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] DetalleEntrada datos)
        {
            if (repositorio.PorId(id) is DetalleEntrada)
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
            DetalleEntrada detalle = repositorio.PorId(id);
            if (detalle is DetalleEntrada)
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
