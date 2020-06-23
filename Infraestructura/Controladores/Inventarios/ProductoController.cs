using Aplicacion.Servicio.Inventarios.Productos;
using Dominio.Modelo;
using Dominio.Repositorio;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;

namespace Infraestructura.Controladores.Inventarios
{
    [Route("api/[controller]")]
    [Authorize(Roles = "logistica")]
    public class ProductoController : Controller
    {
        private readonly RepoProducto repositorio = new RepoProducto();

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

            Producto consulta = lista.First(producto => {
                return producto.Id == id || producto.Codigo == codigo;
            });

            if (consulta is Producto producto)
            {
                return producto;
            }

            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] Producto datos)
        {
            var repoExistencia = new RepoExistencia();

            var servicio = new ServicioRegistradorProducto(repositorio, repoExistencia);
            
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
