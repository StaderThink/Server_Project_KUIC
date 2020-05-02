using Centaurus.Modelo;
using Centaurus.Repositorio;

namespace Corvus.Caso.Crud {
	public sealed class CrudUsuario: Crud<Usuario> {
		private RepoUsuario Repo { get; set; }

		public CrudUsuario() {
			Repo = new RepoUsuario();
		}

		public override bool Editar(Usuario entidad)
			=> Repo.Editar(entidad);

		public override bool Eliminar(Usuario entidad)
			=> Repo.Eliminar(entidad);

		public override bool Insertar(Usuario entidad)
			=> Repo.Insertar(entidad);
	}
}
