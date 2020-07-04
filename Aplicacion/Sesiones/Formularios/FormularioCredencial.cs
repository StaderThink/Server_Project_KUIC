using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Sesiones.Formularios
{
    public sealed class FormularioCredencial
    {
        public string Documento { get; set; }
        public string Clave { get; set; }

        public FormularioCredencial() { }

        public FormularioCredencial(string documento, string clave)
        {
            Documento = documento;
            Clave = clave;
        }
    }
}
