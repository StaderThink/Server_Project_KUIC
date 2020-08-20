using System.ComponentModel.DataAnnotations;

namespace Dominio.Notificaciones
{
    public sealed class Segmento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Notificacion { get; set; }
        public int Cargo { get; set; }
    }
}
