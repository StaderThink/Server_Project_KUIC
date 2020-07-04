using System.Collections.Generic;
using System.Security.Claims;
using Aplicacion.Sesiones.Formularios;
using Dominio.Usuarios;
using Dominio.Cargos;

namespace Aplicacion.Sesiones
{
    public sealed class ServicioSesion
    {
        private readonly RepositorioUsuario repositorio;

        public ServicioSesion()
        {
            repositorio = new RepositorioUsuario();
        }

        public Usuario ValidarCredencial(FormularioCredencial credencial)
        {
            var usuario = repositorio.PorDocumento(credencial.Documento);

            if (usuario is Usuario)
            {
                if (usuario.Clave == credencial.Clave && usuario.Activo)
                {
                    return usuario;
                }
            }

            return null;
        }

        public ClaimsPrincipal GenerarIdentidad(Usuario usuario)
        {
            var repositorioCargo = new RepositorioCargo();
            var cargo = repositorioCargo.PorId(usuario.Cargo);

            var listado = new List<Claim>
            {
                new Claim(ClaimTypes.SerialNumber, usuario.Id.ToString()),
                new Claim(ClaimTypes.Dns, usuario.Documento),
                new Claim(ClaimTypes.Email, usuario.Correo),
            };

            // permisos

            if (cargo.Logistica)
                listado.Add(new Claim(ClaimTypes.Role, "logistica"));
            if (cargo.Pedidos)
                listado.Add(new Claim(ClaimTypes.Role, "pedidos"));
            if (cargo.Solicitar)
                listado.Add(new Claim(ClaimTypes.Role, "solicitar"));
            if (cargo.Usuarios)
                listado.Add(new Claim(ClaimTypes.Role, "usuarios"));
            if (cargo.Clientes)
                listado.Add(new Claim(ClaimTypes.Role, "clientes"));

            var identidad = new ClaimsIdentity(listado, "bearerToken");
            return new ClaimsPrincipal(identidad);
        }
    }
}
