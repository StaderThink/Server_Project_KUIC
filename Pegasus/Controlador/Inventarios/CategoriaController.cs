using Centaurus.Modelo;
using Centaurus.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Pegasus.Extension;
using System.Collections.Generic;

namespace Pegasus.Controlador.Inventarios {
	[Route("api/[controller]")]
	[Autenticado(Permiso.Logistica)]
	public class CategoriaController: Controller {
		private readonly RepoCategoria repo = new RepoCategoria();

		[HttpGet]
		public IEnumerable<Categoria> Listar() {
			return repo.Listar();
		}

		[HttpGet("{id}")]
		public ActionResult<Categoria> Obtener(int id) {
			var categoria = repo.PorId(id);

			if (categoria is Categoria) {
				return categoria;
			}

			return NotFound();
		}

		[HttpPost]
		public IActionResult Insertar([FromBody] Categoria datos) {
			if (repo.Insertar(datos)) {
				return Accepted();
			}

			return BadRequest();
		}

		[HttpDelete("{id}")]
		public IActionResult Eliminar(int id) {
			var categoria = repo.PorId(id);

			if (categoria is Categoria) {
				if (repo.Eliminar(categoria)) {
					return Accepted();
				}
			}

			return BadRequest();
		}

		[HttpPut("{id}")]
		public IActionResult Editar(int id, [FromBody] Categoria datos) {
			if (repo.PorId(id) is Categoria) {
				datos.Id = id;

				if (repo.Editar(datos)) {
					return Accepted();
				}
			}

			return BadRequest();
		}
	}
}
