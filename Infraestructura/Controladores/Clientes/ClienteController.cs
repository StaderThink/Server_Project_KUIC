using Aplicacion.Servicio.Usuarios;
using Dominio.Modelo;
using Dominio.Repositorio;
using Infraestructura.Extensiones;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Infraestructura.Controladores.Clientes {
    [Route("api/[controller]")]
	[Autenticado(Permiso.Clientes)]
	public class ClienteController: Controller {
		private readonly RepoCliente repositorio = new RepoCliente();

		[HttpGet]
		public IEnumerable<Cliente> Listar() {
			var todos = repositorio.Listar();
			return todos
				.Where(cliente => cliente.Activo)
				.OrderBy(cliente => cliente.Id);
		}

		[HttpGet("todos")]
		public IEnumerable<Cliente> ListarTodos() {
			return repositorio
				.Listar()
				.OrderBy(cliente => cliente.Id);
        }

		[HttpGet("{id}")]
		public ActionResult<Cliente> Obtener(int id) {
			if (repositorio.PorId(id) is Cliente cliente) {
				return cliente;
			}

			return NotFound();
		}

		[HttpPost]
		public IActionResult Insertar([FromBody] Cliente cliente) {
			if (repositorio.Insertar(cliente))
			{
				return Accepted();
			}
			return BadRequest();
		}

		[HttpPut("{id}")]
		public IActionResult Editar(int id, [FromBody] Cliente cliente) {
			if (repositorio.PorId(id) is Cliente) {
				cliente.Id = id;

				if (repositorio.Editar(cliente)) {
					return Ok();
				}

				return BadRequest();
			}

			return NotFound();
		}

		[HttpDelete("{id}")]
		public IActionResult Eliminar(int id) {
			if (repositorio.PorId(id) is Cliente cliente) {
				if (repositorio.Eliminar(cliente)) {
					return Ok();
				}

				else return BadRequest();
			}

			return NotFound();
		}
		[HttpGet("existe")]
		public ActionResult<Cliente> Existe([FromQuery] string rut)
		{
			var lista = repositorio.Listar();
			try
			{
				var busqueda = lista.First(cliente => cliente.Rut == rut);

				if (busqueda is Cliente)
				{
					return busqueda;
				}

				return NotFound();
			}
			catch
			{
				return NotFound();
			}
		}
	}
}
