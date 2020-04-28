using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pegasus.Formulario {
	public sealed class FormularioCargo {
		public string Nombre { get; set; }
		public bool Pedidos { get; set; }
		public bool Usuarios { get; set; }
		public bool Logistica { get; set; }
		public bool Clientes { get; set; }
		public bool Solicitar { get; set; }
	}
}
