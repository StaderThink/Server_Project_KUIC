using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Aplicacion.Usuarios;
using Dominio.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Usuarios
{
    [Authorize(Roles = "usuarios")]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly RepositorioUsuario repositorio;

        public UsuarioController()
        {
            repositorio = new RepositorioUsuario();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var list = repositorio.Listar();

                // remover el usuario en sesion

                string documento = HttpContext.User.FindFirstValue(ClaimTypes.Dns);
                return Ok(list.Where(u => u.Documento != documento));
            }

            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> Obtener(int id)
        {
            if (repositorio.PorId(id) is Usuario usuario)
            {
                return usuario;
            }

            return NotFound();
        }

        [HttpGet("documento/{documento}")]
        public ActionResult<Usuario> PorDocumento(string documento)
        {
            if (repositorio.PorDocumento(documento) is Usuario usuario)
            {
                return usuario;
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] Usuario usuario)
        {
            var servicio = new ServicioRegistradorUsuario();

            if (servicio.Registrar(usuario))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Usuario usuario)
        {
            if (repositorio.PorId(id) is Usuario)
            {
                usuario.Id = id;

                if (repositorio.Editar(usuario))
                {
                    return Ok();
                }

                return BadRequest();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            if (repositorio.PorId(id) is Usuario usuario)
            {
                if (repositorio.Eliminar(usuario))
                {
                    return Ok();
                }

                else
                    return BadRequest();
            }

            return NotFound();
        }
    }
}
