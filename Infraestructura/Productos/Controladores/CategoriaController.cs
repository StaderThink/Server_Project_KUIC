using System.Collections.Generic;
using Dominio.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Productos.Controladores
{
    [Route("api/[controller]")]
    [Authorize(Roles = "logistica")]
    public class CategoriaController : Controller
    {
        private readonly RepositorioCategoria repositorio;

        public CategoriaController()
        {
            repositorio = new RepositorioCategoria();
        }

        [HttpGet]
        public IEnumerable<Categoria> Listar()
        {
            return repositorio.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<Categoria> Obtener(int id)
        {
            Categoria categoria = repositorio.PorId(id);

            if (categoria is Categoria)
            {
                return categoria;
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] Categoria datos)
        {
            if (repositorio.Insertar(datos))
            {
                return Accepted();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            Categoria categoria = repositorio.PorId(id);

            if (categoria is Categoria)
            {
                if (repositorio.Eliminar(categoria))
                {
                    return Accepted();
                }
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Categoria datos)
        {
            if (repositorio.PorId(id) is Categoria)
            {
                datos.Id = id;

                if (repositorio.Editar(datos))
                {
                    return Accepted();
                }
            }

            return BadRequest();
        }
    }
}
