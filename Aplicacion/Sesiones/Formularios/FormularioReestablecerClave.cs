using System;

namespace Aplicacion.Sesiones.Formularios
{
    public sealed class FormularioReestablecerClave
    {
        public string Documento { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public string NuevaClave { get; set; }
    }
}
