using System;
namespace Dominio.Clientes
{
    public sealed class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Encargado { get; set; }
        public string Rut { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Actualizado { get; set; }
        public bool Activo { get; set; }
    }
}
