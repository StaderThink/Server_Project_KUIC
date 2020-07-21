using System;
using System.Linq;
using System.Security.Claims;
using Aplicacion.Sesiones;
using Aplicacion.Sesiones.Formularios;
using Dominio.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Sesiones.Controladores
{
    [Route("api/[controller]")]
    public class SesionController : ControllerBase
    {
        [HttpPost]
        public IActionResult IniciarSesion([FromBody] FormularioIniciarSesion credencial)
        {
            ServicioSesion servicio = new ServicioSesion();

            if (servicio.ValidarCredencial(credencial) is Usuario usuario)
            {
                ProveedorTokenSesion criptografo = new ProveedorTokenSesion();
                ClaimsPrincipal identidad = servicio.GenerarIdentidad(usuario);

                return Ok(criptografo.GenerarToken(identidad));
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        public IActionResult VerificarSesion()
        {
            try
            {
                Claim cargaDocumento = HttpContext.User.Claims.First(carga => carga.Type == ClaimTypes.Dns);
                string documento = cargaDocumento.Value;

                RepositorioUsuario repo = new RepositorioUsuario();

                if (repo.PorDocumento(documento) is Usuario usuario)
                {
                    return Ok(usuario);
                }

                return BadRequest();
            }

            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut("cambiar_clave")]
        public IActionResult CambiarClave([FromBody] FormularioCambiarClave formulario)
        {
            try
            {
                ServicioCambiarClave servicioCambiarClave = new ServicioCambiarClave();

                var id = HttpContext.User.FindFirst(ClaimTypes.SerialNumber);

                if (id is null)
                {
                    throw new NullReferenceException(nameof(id));
                }

                else
                {
                    formulario.Usuario = int.Parse(id.Value);

                    if (servicioCambiarClave.CambiarClave(formulario))
                    {
                        return Ok();
                    }
                }

                return BadRequest();
            }

            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut("reestablecer_clave")]
        public IActionResult ReestablecerClave([FromBody] FormularioReestablecerClave formulario)
        {
            try
            {
                ServicioReestablecerClave servicioReestablecerClave = new ServicioReestablecerClave();

                if (servicioReestablecerClave.ReestablecerClave(formulario))
                {
                    return Ok();
                }

                return BadRequest();
            }

            catch
            {
                return BadRequest();
            }
        }
    }
}
