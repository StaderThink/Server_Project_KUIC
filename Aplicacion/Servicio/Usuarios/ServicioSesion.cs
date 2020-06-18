using Aplicacion.Modelo.Sesiones;

using Dominio.Modelo;
using Dominio.Repositorio;

using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aplicacion.Servicio.Usuarios
{
    public sealed class ServicioSesion
    {
        private readonly RepoUsuario _repo;

        public ServicioSesion()
        {
            _repo = new RepoUsuario();
        }

        public string? GenerarToken(Credencial credencial)
        {
            if (ValidarCredencial(credencial) is Usuario usuario)
            {
                var repoCargo = new RepoCargo();
                var cargo = repoCargo.PorId(usuario.Cargo);

                // generacion del token

                string tokenSecreto = Environment.GetEnvironmentVariable("TOKEN") ?? "Aurelia";

                SymmetricSecurityKey llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecreto));
                SigningCredentials credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

                var carga = new List<Claim>
                {
                    new Claim(ClaimTypes.Dns, usuario.Documento),
                    new Claim(ClaimTypes.Email, usuario.Correo),
                };

                carga.AddRange(GenerarRoles(cargo));

                JwtSecurityToken token = new JwtSecurityToken
                (
                    issuer: tokenSecreto,
                    audience: tokenSecreto,
                    claims: carga,
                    expires: DateTime.Now.AddDays(3),
                    signingCredentials: credenciales
                );

                JwtSecurityTokenHandler criptografo = new JwtSecurityTokenHandler();
                return criptografo.WriteToken(token);
            }

            return null;
        }

        private IEnumerable<Claim> GenerarRoles(Cargo cargo)
        {
            var roles = new List<Claim>();

            if (cargo.Logistica)
                roles.Add(new Claim(ClaimTypes.Role, "logistica"));
            if (cargo.Pedidos)
                roles.Add(new Claim(ClaimTypes.Role, "pedidos"));
            if (cargo.Solicitar)
                roles.Add(new Claim(ClaimTypes.Role, "solicitar"));
            if (cargo.Usuarios)
                roles.Add(new Claim(ClaimTypes.Role, "usuarios"));
            if (cargo.Clientes)
                roles.Add(new Claim(ClaimTypes.Role, "clientes"));

            return roles;
        }

        private Usuario? ValidarCredencial(Credencial credencial)
        {
            var usuario = _repo.PorDocumento(credencial.Documento);

            if (usuario is Usuario)
            {
                if (usuario.Clave == credencial.Clave && usuario.Activo)
                {
                    return usuario;
                }
            }

            return null;
        }
    }
}
