using System.Collections.Generic;
using Aplicacion.Salidas;
using Aplicacion.Salidas.Formularios;
using Dominio.Salidas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Controladores.Inventarios
{
    [Route("api/[controller]")]
    [Authorize(Roles = "logistica")]
    public class SalidaController : Controller
    {
        private readonly RepositorioSalida repositorio;

        public SalidaController()
        {
            repositorio = new RepositorioSalida();
        }

        [HttpGet]
        public IEnumerable<Salida> Listar()
        {
            return repositorio.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<Salida> Obtener(int id)
        {
            Salida salida = repositorio.PorId(id);

            if (salida is Salida)
            {
                return salida;
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] FormularioRegistrarSalida formulario)
        {
            ServicioRegistradorSalida servicio = new ServicioRegistradorSalida();

            if (servicio.Registrar(formulario))
            {
                return Accepted();
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Salida datos)
        {
            if (repositorio.PorId(id) is Salida)
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
            Salida salida = repositorio.PorId(id);
            if (salida is Salida)
            {
                if (repositorio.Eliminar(salida))
                {
                    return Accepted();
                }
            }
            return BadRequest();
        }
    }
}
