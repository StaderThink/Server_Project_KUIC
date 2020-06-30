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
    public class PedidoController : Controller
    {
        private readonly RepoPedido repositorio = new RepoPedido();

        [HttpGet]
        public IEnumerable<Pedido>Listar()
        {
            return repositorio.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<Pedido> Obtener(int id)
        {
            if (repositorio.PorId(id) is Pedido pedido)
            {
                return pedido;
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] Pedido pedido)
        {
            if (repositorio.Insertar(pedido))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Pedido pedido)
        {
            if (repositorio.PorId(id) is Pedido)
            {
                pedido.Id = id;

                if (repositorio.Editar(pedido))
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
            if (repositorio.PorId(id) is Pedido pedido)
            {
                if (repositorio.Eliminar(pedido))
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
