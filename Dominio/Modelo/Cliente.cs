using System;
namespace Dominio.Modelo
{
	public sealed class Cliente: IEntidad
    {
		public int Id { get; set; }
		public string Nombre {get; set;}
		public string Encargado {get; set;}
		public string Rut {get; set;}
		public string Correo {get; set;}
		public string Direccion {get; set;}
		public string Telelfono {get; set;}
		public DateTime Creado { get; set; }
        public DateTime Actualizado { get; set; }
        public bool Activo { get; set; }
    }
}
