using Centaurus.Modelo;
using Centaurus.Repositorio;
using Corvus.Modelo.Sesiones;
using Corvus.Seguridad;
using System;

namespace Corvus.Servicio.Usuarios {
	public sealed class ServicioSesion: Traductor<Sesion, string> {
		private bool ValidarSesion(Sesion sesion) {
			var dias = (DateTime.Now - sesion.Fecha).Days;
			if (dias > 3) return false;

			var credencial = sesion.Credencial;

			var repo = new RepoUsuario();
			var usuario = repo.PorDocumento(sesion.Credencial.Documento);

			if (usuario is Usuario) {
				if (usuario.Activo && usuario.Clave == credencial.Clave) {
					return true;
				}
			}

			return false;
		}

		public override string Generar(Sesion carga) {
			if (ValidarSesion(carga)) {
				try {
					var criptografo = new ProveedorJWT();
					return criptografo.Encriptar(carga);
				}

				catch {
					return null;
				}
			}

			return null;
		}

		public override Sesion Traducir(string carga) {
			try {
				var criptografo = new ProveedorJWT();
				var sesion = criptografo.Traduccir<Sesion>(carga);

				if (sesion is Sesion) {
					if (ValidarSesion(sesion)) return sesion;
				}
			}

			catch {
				return null;
			}

			return null;
		}
	}
}
