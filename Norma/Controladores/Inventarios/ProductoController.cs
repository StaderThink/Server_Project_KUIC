using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Centaurus.Modelo;
using Centaurus.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Norma.Extensiones;

namespace Norma.Controladores.Inventarios
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

        [HttpGet("{id}")]
        public ActionResult<Producto> Obtener(int id)
        {
            var producto = repositorio.PorId(id);
            if (producto is Producto)
            {
                return producto;
            }
            return NotFound();
        }

        [HttpGet("{codigo}")]
        public ActionResult<Producto> Codigo(string codigo)
        {
            var producto = repositorio.PorCodigo(codigo);
            if (producto is Producto)
            {
                return producto;
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody]Producto datos)
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
