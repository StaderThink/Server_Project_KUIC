using System.ComponentModel.DataAnnotations;

namespace Dominio.Cargos
{
    public sealed class Cargo
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }

        [Required]
        public bool Pedidos { get; set; } = false;

        [Required]
        public bool Usuarios { get; set; } = false;

        [Required]
        public bool Logistica { get; set; } = false;

        [Required]
        public bool Clientes { get; set; } = false;

        [Required]
        public bool Solicitar { get; set; } = false;
    }
}
