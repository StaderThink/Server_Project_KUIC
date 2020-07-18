using System.ComponentModel.DataAnnotations;

namespace Dominio.Notificaciones
{
    public sealed class Segmento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Notificacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Cargo { get; set; }
    }
}
