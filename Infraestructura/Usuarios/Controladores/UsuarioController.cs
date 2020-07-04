using System.Collections.Generic;
using System.Linq;
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

        private IEnumerable<Usuario> Busqueda(string criterio = "")
        {
            IEnumerable<Usuario> lista = repositorio.Listar();

            criterio = criterio?.ToLower() ?? "";

            return
                from usuario in lista
                where
                    usuario.Documento.Contains(criterio) ||
                    usuario.Nombre.Contains(criterio) ||
                    usuario.Apellido.Contains(criterio)
                select usuario;
        }

        [HttpGet]
        public IActionResult Listar([FromQuery] string buscar)
        {
            try
            {
                IEnumerable<Usuario> resultado = Busqueda(buscar);
                return Ok(resultado);
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

        [HttpPost]
        public IActionResult Insertar([FromBody] Usuario usuario)
        {
            ServicioRegistradorUsuario servicio = new ServicioRegistradorUsuario();

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
