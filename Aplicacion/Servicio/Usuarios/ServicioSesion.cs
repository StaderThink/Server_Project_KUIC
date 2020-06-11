using Aplicacion.Modelo.Sesiones;
using Aplicacion.Seguridad;

using Dominio.Modelo;
using Dominio.Repositorio;

using System;

namespace Aplicacion.Servicio.Usuarios {
    public sealed class ServicioSesion : Traductor<Sesion, string> {
        private bool ValidarSesion(Sesion sesion) {
            int dias = (DateTime.Now - sesion.Fecha).Days;
            if (dias > 3) return false;

            Credencial credencial = sesion.Credencial;

            RepoUsuario repo = new RepoUsuario();
            Usuario usuario = repo.PorDocumento(sesion.Credencial.Documento);

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
                    ProveedorJWT criptografo = new ProveedorJWT();
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
                ProveedorJWT criptografo = new ProveedorJWT();
                Sesion sesion = criptografo.Traduccir<Sesion>(carga);

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
