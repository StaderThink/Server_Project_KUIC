using Centaurus.Modelo;
using Centaurus.Repositorio;
using Corvus.Modelo.Formularios;

namespace Corvus.Servicio.Usuarios {
	public sealed class ServicioReestablecerClave {
		public Usuario UsuarioDesdeFormulario(FormularioReestablecerClave formulario) {
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
