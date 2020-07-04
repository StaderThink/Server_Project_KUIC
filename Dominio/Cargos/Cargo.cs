namespace Dominio.Cargos
{
    public sealed class Cargo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Pedidos { get; set; }
        public bool Usuarios { get; set; }
        public bool Logistica { get; set; }
        public bool Clientes { get; set; }
        public bool Solicitar { get; set; }
    }
}
