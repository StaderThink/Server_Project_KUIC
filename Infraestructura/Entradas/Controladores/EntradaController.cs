using System.Collections.Generic;
using Aplicacion.Entradas;
using Aplicacion.Entradas.Formularios;
using Dominio.Entradas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Entradas.Controladores
{
    [Route("api/[controller]")]
    [Authorize(Roles = "logistica")]
    public class EntradaController : Controller
    {
        private readonly RepositorioEntrada repositorio;

        public EntradaController()
        {
            repositorio = new RepositorioEntrada();
        }

        [HttpGet]
        public IEnumerable<Entrada> Listar()
        {
            return repositorio.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<Entrada> Obtener(int id)
        {
            Entrada entrada = repositorio.PorId(id);
            if (entrada is Entrada)
            {
                return entrada;
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] FormularioRegistrarEntrada formulario)
        {
            ServicioRegistradorEntrada servicio = new ServicioRegistradorEntrada();

            if (servicio.Registrar(formulario))
            {
                return Accepted();
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Entrada informacion)
        {
            if (repositorio.PorId(id) is Entrada)
            {
                informacion.Id = id;

                if (repositorio.Editar(informacion))
                {
                    return Accepted();
                }
            }

            return BadRequest();
        }
    }
}
