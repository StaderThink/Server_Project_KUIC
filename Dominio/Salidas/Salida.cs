using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Salidas
{
    public sealed class Salida
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Observacion { get; set; }
        public int Pedido { get; set; }
    }
}
