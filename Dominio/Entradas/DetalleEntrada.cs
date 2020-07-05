namespace Dominio.Entradas
{
    public sealed class DetalleEntrada : IDetalle
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int Entrada { get; set; }
        public int Producto { get; set; }
    }
}
