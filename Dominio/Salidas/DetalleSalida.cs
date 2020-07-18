using System.ComponentModel.DataAnnotations;

namespace Dominio.Salidas
{
    public sealed class DetalleSalida : IDetalle
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public int Cantidad { get; set; }

        [Required]
        public int Salida { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Producto { get; set; }
    }
}
