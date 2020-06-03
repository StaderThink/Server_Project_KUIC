using Dominio.Modelo;
using System.Collections.Generic;

namespace Dominio.Repositorio {
	public interface IRepo<Modelo> where Modelo : IEntidad {
		bool Insertar(Modelo entidad);
		bool Editar(Modelo entidad);
		bool Eliminar(Modelo entidad);
		IEnumerable<Modelo> Listar();
		Modelo PorId(int id);
	}
}
