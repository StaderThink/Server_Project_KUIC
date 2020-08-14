using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Clientes
{
    public sealed class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]

        public string Encargado { get; set; }

        [StringLength(11, MinimumLength = 7, ErrorMessage = "El documento debe tener entre {2} y {1} carateres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]

        public string Rut { get; set; }

        [EmailAddress(ErrorMessage = "El campo no concuerda con una dirección de correo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]

        public string Correo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]

        public string Direccion { get; set; }

        [Phone]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Telefono { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Actualizado { get; set; }
        public bool Activo { get; set; }
    }
}
