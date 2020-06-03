using System;
using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Modelo.Formularios {
	public sealed class FormularioReestablecerClave {
		[Required]
		public string Documento { get; set; }
		public DateTime Expedicion { get; set; }
		public string Clave { get; set; }
	}
}
