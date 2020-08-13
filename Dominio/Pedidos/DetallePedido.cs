using System.ComponentModel.DataAnnotations;
namespace Dominio.Pedidos
{
    public sealed class DetallePedido : IDetalle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Pedido { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Producto { get; set; }
    }
}