using Centaurus.Modelo;
using System.Collections.Generic;

namespace Centaurus.Repositorio {
	public interface IRepo<Modelo> where Modelo: IEntidad {
		bool Insertar(Modelo entidad);
		bool Editar(Modelo entidad);
		bool Eliminar(Modelo entidad);
		IEnumerable<Modelo> Listar();
	}
}
