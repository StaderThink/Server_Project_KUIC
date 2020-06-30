﻿using Dominio.Modelo;
using Dominio.Repositorio;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;

namespace Infraestructura.Controladores.Pedidos
{
    [Authorize(Roles = "pedidos")]
    [Route("api/[controller]")]
    public class DetallePedidoController : Controller
    {
        private readonly RepoDetallePedido repositorio = new RepoDetallePedido();

        [HttpGet]
        public IEnumerable<DetallePedido> Listar()
        {
            return repositorio.Listar();
        }

        [HttpGet("{id}")] 
        public ActionResult<DetallePedido> Obtener(int id)
        {
            DetallePedido detalle = repositorio.PorId(id);

            if (detalle is DetallePedido)
            {
                return detalle;
            }
            return NotFound();
        }

        [HttpGet("pedido/{pedidoId}")] 
        public IEnumerable<DetallePedido> ListarPorPedido(int pedidoId)
        {
            IEnumerable<DetallePedido> lista = repositorio.Listar();

            return
                from detalle in lista
                where detalle.Pedido == pedidoId
                select detalle;
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] DetallePedido datos)
        {
            if (repositorio.Insertar(datos))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] DetallePedido datos)
        {
            if (repositorio.PorId(id) is DetallePedido)
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
            DetallePedido detalle = repositorio.PorId(id);
            if (detalle is DetallePedido)
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