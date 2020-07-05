using System.Collections.Generic;
using Dominio.Entradas;

namespace Aplicacion.Entradas.Formularios
{
    public sealed class FormularioRegistrarEntrada
    {
        public Entrada Entrada { get; set; }
        public IEnumerable<DetalleEntrada> Detalles { get; set; }
    }
}
