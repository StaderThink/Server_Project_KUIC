using Dominio.Modelo;
using Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infraestructura.Extensiones;

namespace Infraestructura.Controladores.Inventarios
{
    [Route("api/[controller]")]
    [Autenticado(Permiso.Logistica)]
    public class ExistenciaController : Controller
    {
        private readonly RepoExistencia existencia = new RepoExistencia();

        [HttpGet]
        public IEnumerable<Existencia> Listar()
        {
            return existencia.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<Existencia> Obtener(int id)
        {
            var existe = existencia.PorId(id);

            if(existe is Existencia)
            {
                return existe;
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody]Existencia datos)
        {
            if (existencia.Insertar(datos))
            {
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody]Existencia datos)
        {
            if (existencia.PorId(id) is Existencia)
            {
                datos.Id = id;
                if (existencia.Editar(datos))
                {
                    return Accepted();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existe = existencia.PorId(id);
            if (existe is Existencia)
            {
                if (existencia.Eliminar(existe))
                {
                    return Accepted();
                }
            }
            return BadRequest();
        }
    }
}
