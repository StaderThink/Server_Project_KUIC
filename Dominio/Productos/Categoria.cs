using System.ComponentModel.DataAnnotations;

namespace Dominio.Productos
{
    public sealed class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
