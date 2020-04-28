using Centaurus.Modelo;
using Centaurus.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Pegasus.Extension;

namespace Pegasus.Controlador.Usuarios {
	[Autenticado(Permiso.Usuarios)]
	[Route("api/[controller]")]
	public class CargoController: Controller {
		private RepoCargo _repo;

		public CargoController() {
			_repo = new RepoCargo();
		}

		[HttpGet]
		public IActionResult Listar() {
			var lista = _repo.Listar();

			return Ok(lista);
		}

		[HttpPost]
		public IActionResult Insertar([FromBody] Cargo datos) {
			if (_repo.Insertar(datos)) {
				return Ok("el cargo fue creado con exito");
			}

			return BadRequest("no fue insertado el cargo");
		}

		[HttpGet("{id}")]
		public IActionResult Obtener(int id) {
			if (_repo.PorId(id) is Cargo cargo) {
				return Ok(cargo);
			}

			return NotFound("no se encontro el cargo");
		}

		[HttpPut("{id}")]
		public IActionResult Editar(int id, [FromBody] Cargo datos) {
			if (_repo.PorId(id) is Cargo) {
				datos.Id = id;

				if (_repo.Editar(datos)) {
					return Ok("editado correctamente");
				}

				else return BadRequest("no fue posible editar el cargo");
			}

			return NotFound("no se encontro el cargo");
		}

		[HttpDelete("{id}")]
		public IActionResult Eliminar(int id) {
			if (_repo.PorId(id) is Cargo cargo) {
				if (_repo.Eliminar(cargo)) {
					return Ok("eliminado correctamente");
				}

				else return BadRequest("no fue posible eliminar el cargo");
			}

			return NotFound("no se encontro el cargo");
		}
	}
}
