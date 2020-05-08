using Centaurus.Modelo;
using Centaurus.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Pegasus.Extension;
using System.Linq;

namespace Pegasus.Controlador.Usuarios {
	[Route("api/[controller]")]
	[Autenticado(Permiso.Usuarios)]
	public class UsuarioController: Controller {
		private readonly RepoUsuario repo = new RepoUsuario();

		[HttpGet]
		public IActionResult Listar() {
			var todos = repo.Listar();
			return Ok(todos.Where(usuario => usuario.Activo));
		}

		[HttpGet("todos")]
		public IActionResult ListarTodos() => Ok(repo.Listar());

		[HttpGet("{id}")]
		public ActionResult<Usuario> Obtener(int id) {
			if (repo.PorId(id) is Usuario usuario) {
				return usuario;
			}

			return NotFound();
		}

		[HttpPost]
		public IActionResult Insertar([FromBody] Usuario usuario) {
			if (repo.Insertar(usuario)) return Accepted();
			else return BadRequest();
		}

		[HttpPut("{id}")]
		public IActionResult Editar(int id, [FromBody] Usuario usuario) {
			if (repo.PorId(id) is Usuario) {
				usuario.Id = id;

				if (repo.Editar(usuario)) {
					return Accepted();
				}

				return BadRequest();
			}

			return NotFound();
		}

		[HttpDelete("{id}")]
		public IActionResult Eliminar(int id) {
			if (repo.PorId(id) is Usuario usuario) {
				if (repo.Eliminar(usuario)) {
					return Accepted();
				}

				else return BadRequest();
			}

			return NotFound();
		}
	}
}
