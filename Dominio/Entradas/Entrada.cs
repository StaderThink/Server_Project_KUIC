using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entradas
{
    public sealed class Entrada
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Observacion { get; set; }
    }
}
