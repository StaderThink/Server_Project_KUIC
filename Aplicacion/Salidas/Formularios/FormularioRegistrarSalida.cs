using System.Collections.Generic;
using Dominio.Salidas;

namespace Aplicacion.Salidas.Formularios
{
    public sealed class FormularioRegistrarSalida
    {
        public Salida Salida { get; set; }
        public IEnumerable<DetalleSalida> Detalles { get; set; }
    }
}
