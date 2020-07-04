using System;
using System.Text.Json.Serialization;

namespace Dominio.Usuarios
{
    public sealed class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string Correo { get; set; }
        [JsonIgnore] public string Clave { get; set; }
        public DateTime Nacimiento { get; set; }
        public DateTime Expedicion { get; set; }
        public int Cargo { get; set; }
        public string Telefono { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Actualizado { get; set; }
        public bool Activo { get; set; }
    }
}
