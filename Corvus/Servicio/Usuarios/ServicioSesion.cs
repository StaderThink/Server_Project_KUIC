using Centaurus.Repositorio;
using Corvus.Modelo.Sesiones;
using System;

namespace Corvus.Servicio.Usuarios {
	using ModeloUsuario = Centaurus.Modelo.Usuario;

	public sealed class ServicioSesion: Traductor<Credencial, Sesion> {
		#region Validacion
		private bool ValidarCredencial(Credencial credencial) {
			var repo = new RepoUsuario();
			var consulta = repo.PorDocumento(credencial.Documento);

			if (consulta is ModeloUsuario usuario) {
				if (usuario.Activo && usuario.Clave == credencial.Clave) {
					return true;
				}
			}

			return false;
		}
		#endregion

		public override Sesion Generar(Credencial datos) {
			if (ValidarCredencial(datos)) {
				return new Sesion {
					Credencial = datos,
					Fecha = DateTime.Now
				};
			}

			return null;
		}

		public override Credencial Traducir(Sesion datos) {
			if (ValidarCredencial(datos.Credencial)) {
				var diasUso = (DateTime.Now - datos.Fecha).Days;

				if (diasUso < 3) {
					return datos.Credencial;
				}
			}

			return null;
		}
	}
}
