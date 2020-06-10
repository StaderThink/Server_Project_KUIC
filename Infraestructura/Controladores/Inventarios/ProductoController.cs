using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Modelo;
using Dominio.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Infraestructura.Extensiones;

namespace Infraestructura.Controladores.Inventarios
{
    [Route("api/[controller]")]
    [Autenticado(Permiso.Logistica)]
    public class ProductoController : Controller
    {
        private readonly RepoProducto repositorio = new RepoProducto();
        [HttpGet]
        public IEnumerable<Producto> Listar()
        {
            return repositorio.Listar();
        }

        [HttpGet("buscar")] // GET api/producto/buscar
        public ActionResult<Producto> Buscar([FromQuery] int id, [FromQuery] string codigo) {
            IEnumerable<Producto> lista = repositorio.Listar();

            var consulta = lista.First(producto => {
                return producto.Id == id || producto.Codigo == codigo;
            });

            if (consulta is Producto producto) {
                return producto;
            }

            else {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] Producto datos)
        {
            if (repositorio.Insertar(datos))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody]Producto datos)
        {
            if (repositorio.PorId(id) is Producto)
            {
                datos.Id = id;
                if (repositorio.Editar(datos))
                {
                    return Accepted();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var producto = repositorio.PorId(id);
            if (producto is Producto)
            {
                if (repositorio.Eliminar(producto))
                {
                    return Accepted();
                }
            }
            return BadRequest();
        }
    }
}
