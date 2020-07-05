namespace Dominio.Salidas
{
    public sealed class DetalleSalida : IDetalle
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int Salida { get; set; }
        public int Producto { get; set; }
    }
}
