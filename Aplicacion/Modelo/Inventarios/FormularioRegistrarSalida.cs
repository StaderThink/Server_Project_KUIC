using System.Collections.Generic;
using Dominio.Modelo;

namespace Aplicacion.Modelo.Inventarios
{
    public sealed class FormularioRegistrarSalida
    {
        public Salida Salida { get; set; }
        public IEnumerable<DetalleSalida> Detalles { get; set; }
    }
}
