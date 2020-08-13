using System.Collections.Generic;
using System.Linq;
using Aplicacion.Clientes;
using Dominio.Clientes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Clientes.Controladores
{
    [Authorize(Roles = "clientes, solicitar")]
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly RepositorioCliente repositorio;

        public ClienteController()
        {
            repositorio = new RepositorioCliente();
        }

        private IEnumerable<Cliente> Busqueda(string criterio = "")
        {
            IEnumerable<Cliente> lista = repositorio.Listar();

            criterio = criterio?.ToLower() ?? "";

            return
                from cliente in lista
                where
                    cliente.Rut.Contains(criterio) ||
                    cliente.Nombre.Contains(criterio)
                select cliente;
        }

        [HttpGet]
        public IActionResult Listar([FromQuery] string buscar)
        {
            try
            {
                IEnumerable<Cliente> resultado = Busqueda(buscar);
                return Ok(resultado);
            }

            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> Obtener(int id)
        {
            if (repositorio.PorId(id) is Cliente cliente)
            {
                return cliente;
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] Cliente cliente)
        {
            ServicioRegistradorCliente servicio = new ServicioRegistradorCliente();

            if (servicio.Registrar(cliente))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Cliente cliente)
        {
            if (repositorio.PorId(id) is Cliente)
            {
                cliente.Id = id;

                if (repositorio.Editar(cliente))
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
            if (repositorio.PorId(id) is Cliente cliente)
            {
                if (repositorio.Eliminar(cliente))
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
