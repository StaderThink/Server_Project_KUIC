using System;
using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Sesiones.Formularios
{
    public sealed class FormularioReestablecerClave
    {
        [StringLength(10, ErrorMessage = "El documento debe tener entre {2} y {1} carateres", MinimumLength = 7)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime FechaExpedicion { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Este campo debe tener minimo {2} caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string NuevaClave { get; set; }
    }
}
