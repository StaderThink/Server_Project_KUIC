using Centaurus.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Pegasus.Extension;

namespace Pegasus.Controlador.Usuarios {
	[Autenticado(Permiso.Usuarios)]
	[Route("api/[controller]")]
	public class CargoController: Controller {
		[HttpGet]
		public IActionResult Listar() {
			var repo = new RepoCargo();
			var lista = repo.Listar();

			return Ok(lista);
		}
	}
}
