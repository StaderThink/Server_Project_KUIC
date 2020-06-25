using Dominio.Modelo;

using System.Collections.Generic;

namespace Aplicacion.Modelo.Inventarios
{
    public sealed class FormularioRegistrarEntrada
    {
        public Entrada Entrada { get; set; }
        public List<DetalleEntrada> Detalles { get; set; }
    }
}
