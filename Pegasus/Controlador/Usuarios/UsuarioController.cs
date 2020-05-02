using Microsoft.AspNetCore.Mvc;
using Pegasus.Extension;

namespace Pegasus.Controlador.Usuarios {
	[Route("api/[controller]")]
	[Autenticado(Permiso.Usuarios)]
	public class UsuarioController: Controller {
		[HttpGet]
		public IActionResult Listar() {
			return NotFound("seguimos trabajando por aquí");
		}

		[HttpGet("{id}")]
		public IActionResult Obtener(int id) {
			return NotFound("seguimos trabajando por aquí");
		}

		[HttpPost]
		public IActionResult Insertar([FromBody] string value) {
			return NotFound("seguimos trabajando por aquí");
		}

		[HttpPut("{id}")]
		public IActionResult Editar(int id, [FromBody]string value) {
			return NotFound("seguimos trabajando por aquí");
		}

		[HttpDelete("{id}")]
		public IActionResult Eliminar(int id) {
			return NotFound("seguimos trabajando por aquí");
		}
	}
}
