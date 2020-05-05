using Centaurus.Modelo;
using Centaurus.Repositorio;
using Corvus.Modelo.Sesiones;
using System;

namespace Corvus.Caso.Proceso {
	public sealed class ProcesoSesion: ITraductor<Credencial, Sesion> {
		private bool ValidarCredencial(Credencial credencial) {
			var repo = new RepoUsuario();
			var consulta = repo.PorDocumento(credencial.Documento);

			if (consulta is Usuario usuario) {
				if (usuario.Activo && usuario.Clave == credencial.Clave) {
					return true;
				}
			}

			return false;
		}

		public Sesion Generar(Credencial datos) {
			if (ValidarCredencial(datos)) {
				return new Sesion {
					Credencial = datos,
					Fecha = DateTime.Now
				};
			}

			return null;
		}

		public Credencial Traducir(Sesion datos) {
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
