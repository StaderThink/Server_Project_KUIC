using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dominio.Usuarios
{
    public sealed class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Apellido { get; set; }

        [StringLength(10, MinimumLength = 6, ErrorMessage = "El documento debe tener entre {2} y {1} carateres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public TipoDocumento TipoDocumento { get; set; }

        [EmailAddress(ErrorMessage = "El campo no concuerda con una dirección de correo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Correo { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Este campo debe tener minimo {2} caracteres")]
        [JsonIgnore]
        public string Clave { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime Nacimiento { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime Expedicion { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Cargo { get; set; }

        [Phone]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Telefono { get; set; }

        public DateTime Creado { get; set; }

        public DateTime Actualizado { get; set; }

        public bool Activo { get; set; }
    }
}
