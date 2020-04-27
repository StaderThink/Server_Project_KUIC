using Centaurus.Modelo;
using Centaurus.Repositorio;
using System.Collections.Generic;

namespace Corvus.Caso.Crud {
	public abstract class Crud<Modelo> where Modelo: IEntidad {
		public abstract bool Editar(Modelo entidad);
		public abstract bool Eliminar(Modelo entidad);
		public abstract bool Insertar(Modelo entidad);
		public abstract IEnumerable<Modelo> Listar();
		public abstract Modelo PorId(int id);
	}
}
