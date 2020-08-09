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
        [HttpPut]
        public IActionResult ActualizarDatos([FromBody] Usuario formulario)
        {
            var repositorio = new RepositorioUsuario();
            var sesion = HttpContext.User.FindFirst(ClaimTypes.SerialNumber);

            if (sesion is Claim)
            {
                bool resultadoConversion = int.TryParse(sesion.Value, out int id);

                if (resultadoConversion)
                {
                    if (repositorio.PorId(id) is Usuario entidad)
                    {
                        entidad.Nombre = formulario.Nombre;
                        entidad.Apellido = formulario.Apellido;
                        entidad.Correo = formulario.Correo;
                        entidad.Telefono = formulario.Telefono;

                        if (repositorio.Editar(entidad))
                        {
                            return Ok();
                        }
                    }
                }
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPut("cambiar_clave")]
        public IActionResult CambiarClave([FromBody] FormularioCambiarClave formulario)
        {
            ServicioCambiarClave servicioCambiarClave = new ServicioCambiarClave();

            var id = HttpContext.User.FindFirst(ClaimTypes.SerialNumber);

            if (id is Claim)
            {
                formulario.Usuario = int.Parse(id.Value);

                if (servicioCambiarClave.CambiarClave(formulario))
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPut("reestablecer_clave")]
        public IActionResult ReestablecerClave([FromBody] FormularioReestablecerClave formulario)
        {
            ServicioReestablecerClave servicioReestablecerClave = new ServicioReestablecerClave();

            if (servicioReestablecerClave.ReestablecerClave(formulario))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
