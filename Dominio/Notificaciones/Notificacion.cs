using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Notificaciones
{
    public sealed class Notificacion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Texto { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; } = DateTime.Now;

        [Required]
        public DateTime FechaFin { get; set; } = DateTime.Now.AddDays(10);

        [Required]
        public int Autor { get; set; }
    }
}
