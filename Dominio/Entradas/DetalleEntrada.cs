using System.ComponentModel.DataAnnotations;

namespace Dominio.Entradas
{
    public sealed class DetalleEntrada : IDetalle
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Entrada { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Producto { get; set; }
    }
}
