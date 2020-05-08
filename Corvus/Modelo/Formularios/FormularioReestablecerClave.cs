using System;

namespace Corvus.Modelo.Formularios {
	public sealed class FormularioReestablecerClave {
		public string Documento { get; set; }
		public DateTime Expedicion { get; set; }
		public string Clave { get; set; }
	}
}
