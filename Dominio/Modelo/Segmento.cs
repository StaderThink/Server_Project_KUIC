namespace Dominio.Modelo
{
    public sealed class Segmento : IEntidad
    {
        public int Id { get; set; }
        public int Notificacion { get; set; }
        public int Cargo { get; set; }
    }
}
