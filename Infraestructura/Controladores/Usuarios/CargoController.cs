using Dominio.Modelo;
using Dominio.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Infraestructura.Extensiones;
using System.Collections.Generic;

namespace Infraestructura.Controladores.Usuarios {
	[Autenticado(Permiso.Usuarios)]
	[Route("api/[controller]")]
	public class CargoController: Controller {
		private readonly RepoCargo repo;

		public CargoController() {
			repo = new RepoCargo();
		}

		[HttpGet]
		public IEnumerable<Cargo> Listar() {
			return repo.Listar();
		}

		[HttpPost]
		public IActionResult Insertar([FromBody] Cargo datos) {
			if (repo.Insertar(datos)) {
				return Ok();
			}

			return BadRequest();
		}

		[HttpGet("{id}")]
		public ActionResult<Cargo> Obtener(int id) {
			if (repo.PorId(id) is Cargo cargo) {
				return cargo;
			}

			return NotFound();
		}

		[HttpPut("{id}")]
		public IActionResult Editar(int id, [FromBody] Cargo datos) {
			if (repo.PorId(id) is Cargo) {
				datos.Id = id;

				if (repo.Editar(datos)) return Ok();
				else return BadRequest();
			}

			return NotFound();
		}

		[HttpDelete("{id}")]
		public IActionResult Eliminar(int id) {
			if (repo.PorId(id) is Cargo cargo) {
				if (repo.Eliminar(cargo)) return Ok();
				else return BadRequest();
			}

			return NotFound();
		}
	}
}
