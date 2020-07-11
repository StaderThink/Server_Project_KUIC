using System.Collections.Generic;
using Aplicacion.Pedidos;
using Aplicacion.Pedidos.Formularios;
using Dominio.Pedidos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Controladores.Pedidos
{
    [Authorize(Roles = "pedidos")]
    [Route("api/[controller]")]
    public class PedidoController : Controller
    {
        private readonly RepositorioPedido repositorio;

        public PedidoController()
        {
            repositorio = new RepositorioPedido();
        }

        [HttpGet]
        public IEnumerable<Pedido> Listar()
        {
            return repositorio.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<Pedido> Obtener(int id)
        {
            Pedido pedido = repositorio.PorId(id);

            if (pedido is Pedido)
            {
                return pedido;
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] FormularioRegistrarPedido formulario)
        {
            ServicioRegistradorPedido servicio = new ServicioRegistradorPedido();

            if (servicio.Registrar(formulario))
            {
                return Accepted();
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Pedido datos)
        {
            if (repositorio.PorId(id) is Pedido)
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
            Pedido pedido = repositorio.PorId(id);
            if (pedido is Pedido)
            {
                if (repositorio.Eliminar(pedido))
                {
                    return Accepted();
                }
            }
            return BadRequest();
        }
    }
}
