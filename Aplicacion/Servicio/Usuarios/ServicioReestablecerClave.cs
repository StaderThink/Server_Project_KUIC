using Dominio.Modelo;
using Dominio.Repositorio;
using Aplicacion.Modelo.Formularios;

namespace Aplicacion.Servicio.Usuarios {
	public sealed class ServicioReestablecerClave {
		private Usuario UsuarioDesdeFormulario(FormularioReestablecerClave formulario) {
			var repo = new RepoUsuario();
			var usuario = repo.PorDocumento(formulario.Documento);

			if (usuario is Usuario) {
				if (usuario.Expedicion == formulario.Expedicion) {
					return usuario;
				}
			}

			return null;
		}

		public bool ReestablecerClave(FormularioReestablecerClave formulario) {
			var repo = new RepoUsuario();

			if (UsuarioDesdeFormulario(formulario) is Usuario usuario) {
				usuario.Clave = formulario.Clave;
				return repo.Editar(usuario);
			}

			return false;
		}
	}
}
