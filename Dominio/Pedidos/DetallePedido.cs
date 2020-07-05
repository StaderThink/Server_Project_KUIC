namespace Dominio.Pedidos
{
    public sealed class DetallePedido : IDetalle
    {
        public int Id { get; set; }
        public int Pedido { get; set; }
        public int Cantidad { get; set; }
        public int Producto { get; set; }
    }
}
