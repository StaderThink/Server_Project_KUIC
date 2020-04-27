using Centaurus.Modelo;
using Centaurus.Repositorio;
using Centaurus.Seguridad;
using System;
using System.Collections.Generic;

namespace Corvus.Caso.Crud {
	public sealed class CrudUsuario: Crud<Usuario> {
		protected override IRepo<Usuario> Repo { get; set; }

		public CrudUsuario() {
			Repo = new RepoUsuario();
		}

		public override bool Editar(Usuario entidad)
			=> Repo.Editar(entidad);

		public override bool Eliminar(Usuario entidad)
			=> Repo.Eliminar(entidad);

		public override bool Insertar(Usuario entidad) {
			// valores por defecto

			entidad.Creado = DateTime.Now;
			entidad.Actualizado = DateTime.Now;
			entidad.Activo = false;

			return Repo.Insertar(entidad);
		}

		public override IEnumerable<Usuario> Listar()
			=> Repo.Listar();
	}
}
