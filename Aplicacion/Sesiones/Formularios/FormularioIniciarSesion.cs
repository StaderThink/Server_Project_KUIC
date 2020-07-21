using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Sesiones.Formularios
{
    public sealed class FormularioIniciarSesion
    {
        [StringLength(10, MinimumLength = 6, ErrorMessage = "El documento debe tener entre {2} y {1} caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Documento { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Este campo debe tener minimo {2} caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio")]
        public string Clave { get; set; }

        public FormularioIniciarSesion() { }

        public FormularioIniciarSesion(string documento, string clave)
        {
            Documento = documento;
            Clave = clave;
        }
    }
}
