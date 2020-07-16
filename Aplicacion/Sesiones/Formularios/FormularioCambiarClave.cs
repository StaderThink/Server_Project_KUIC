using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Sesiones.Formularios
{
    public sealed class FormularioCambiarClave
    {
        public int Usuario { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Este campo debe tener minimo {2} caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string ClaveAnterior { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Este campo debe tener minimo {2} caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string NuevaClave { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Este campo debe tener minimo {2} caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string ConfirmacionClave { get; set; }
    }
}
