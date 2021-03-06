using System.Collections.Generic;
using System.Linq;
using Aplicacion.Productos;
using Dominio.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Productos.Controladores
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProductoController : Controller
    {
        private readonly RepositorioProducto repositorio;

        public ProductoController()
        {
            repositorio = new RepositorioProducto();
        }

        [HttpGet]
        public IEnumerable<Producto> Listar()
        {
            return repositorio.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> Obtener(int id)
        {
            Producto producto = repositorio.PorId(id);

            if (producto is Producto)
            {
                return producto;
            }
            return NotFound();
        }

        [HttpGet("buscar")]
        public ActionResult<Producto> Buscar([FromQuery] int id, [FromQuery] string codigo)
        {
            IEnumerable<Producto> lista = repositorio.Listar();

            Producto consulta = lista.First(producto => producto.Id == id || producto.Codigo == codigo);

            if (consulta is Producto producto)
            {
                return producto;
            }

            else
            {
                return NotFound();
            }
        }

        [HttpGet("codigo/{codigo}")]
        public ActionResult<Producto> PorCodigo (string codigo)
        {
            if (repositorio.PorCodigo(codigo) is Producto producto)
            {
                return producto;
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] Producto datos)
        {
            var servicio = new ServicioRegistradorProducto();

            if (servicio.Registrar(datos))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Producto datos)
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
            Producto producto = repositorio.PorId(id);
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
