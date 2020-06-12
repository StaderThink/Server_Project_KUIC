using Aplicacion.Servicio.Usuarios;

using Dominio.Modelo;
using Dominio.Repositorio;

using Infraestructura.Extensiones;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;

namespace Infraestructura.Controladores.Usuarios {
    [Route("api/[controller]")]
    [Autenticado(Permiso.Usuarios)]
    public class UsuarioController : Controller {
        private readonly RepoUsuario repo = new RepoUsuario();

        private IEnumerable<Usuario> Busqueda(string criterio = "") {
            IEnumerable<Usuario> lista = repo.Listar();

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
        public IActionResult Listar([FromQuery] string buscar) {
            try {
                var resultado = Busqueda(buscar);
                return Ok(resultado);
            }

            catch {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> Obtener(int id) {
            if (repo.PorId(id) is Usuario usuario) {
                return usuario;
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] Usuario usuario) {
            ServicioRegistradorUsuario servicio = new ServicioRegistradorUsuario(repo);

            if (servicio.Registrar(usuario)) return Ok();
            else return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Usuario usuario) {
            if (repo.PorId(id) is Usuario) {
                usuario.Id = id;

                if (repo.Editar(usuario)) {
                    return Ok();
                }

                return BadRequest();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id) {
            if (repo.PorId(id) is Usuario usuario) {
                if (repo.Eliminar(usuario)) {
                    return Ok();
                }

                else return BadRequest();
            }

            return NotFound();
        }
    }
}
