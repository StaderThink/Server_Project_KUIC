using Dapper;
using System;
using System.Text.Json.Serialization;

namespace Centaurus.Modelo {
    public sealed class Usuario: IEntidad {
        public int Id { get; set; }
        [Required] public string Nombre { get; set; }
        [Required] public string Apellido { get; set; }
        [Required] public string Documento { get; set; }
        [Required] public TipoDocumento TipoDocumento { get; set; }
        [Required] public string Correo { get; set; }
        [Required][JsonIgnore] public string Clave { get; set; }
        [Required] public DateTime Nacimiento { get; set; }
        [Required] public DateTime Expedicion { get; set; }
        [Required] public int Cargo { get; set; }
        [Required] public string Telefono { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Actualizado { get; set; }
        public bool Activo { get; set; }
    }
}
