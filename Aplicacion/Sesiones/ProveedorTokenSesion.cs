using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Aplicacion.Sesiones
{
    public sealed class ProveedorTokenSesion
    {
        public string GenerarToken(ClaimsPrincipal identidad)
        {
            string tokenSecreto = Environment.GetEnvironmentVariable("TOKEN");

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecreto));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: tokenSecreto,
                audience: tokenSecreto,
                claims: identidad.Claims,
                expires: DateTime.Now.AddDays(3),
                signingCredentials: credenciales
            );

            var criptografo = new JwtSecurityTokenHandler();
            return criptografo.WriteToken(token);
        }
    }
}
