using System.Collections.Generic;
using Dominio.Pedidos;

namespace Aplicacion.Pedidos.Formularios
{
    public sealed class FormularioRegistrarPedido
    {
        public Pedido Pedido { get; set; }
        public IEnumerable<DetallePedido> Detalles { get; set; }
    }
}

