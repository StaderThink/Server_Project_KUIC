using System.ComponentModel.DataAnnotations;

namespace Dominio.Productos
{
    public sealed class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [StringLength(5, MinimumLength = 5, ErrorMessage = "El codigo debe tener {1} carateres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Codigo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public double Precio { get; set; } = 50;

        [Required]
        public int Existencias { get; set; } = 0;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public int MinCantidad { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public double MinPeso { get; set; } = 1;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public double MaxPeso { get; set; } = 0;

        [Required]
        public Magnitud Magnitud { get; set; } = Magnitud.Kg;

        [Required]
        public Presentacion Presentacion { get; set; } = Presentacion.Bandeja;

        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un valor valido")]
        [Required(ErrorMessage = "Elige una categoria")]
        public int Categoria { get; set; }
    }
}
