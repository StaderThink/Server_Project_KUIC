namespace Dominio.Modelo
{
    public sealed class DetalleSalida : IEntidad, IDetalle
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int Salida { get; set; }
        public int Producto { get; set; }
    }
}
