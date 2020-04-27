using Centaurus.Modelo;
using Centaurus.Repositorio;
using System;
using System.Collections.Generic;

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

		public override bool Insertar(Usuario entidad) {
			// valores por defecto

			entidad.Creado = DateTime.Now;
			entidad.Actualizado = DateTime.Now;
			entidad.Activo = false;

			return Repo.Insertar(entidad);
		}

		public override IEnumerable<Usuario> Listar()
			=> Repo.Listar();

		public override Usuario PorId(int id)
			=> Repo.PorId(id);

		public Usuario PorDocumento(string documento)
			=> Repo.PorDocumento(documento);
	}
}
