using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Pedidos
{
    public sealed class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un valor valido")]
        [Required(ErrorMessage = "Este campo es obligatorio")]

        public int Cliente { get; set; }
        public int Asesor { get; set; }
        public Estado Estado { get; set; }
        public bool Activo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public double Descuento { get; set; }
        public string Observacion { get; set; }
    }
}