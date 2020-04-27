using Centaurus.Modelo;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Centaurus {
	internal sealed class Conexion: IDisposable {
		private readonly MySqlConnection _conexion;

		public Conexion() {
			var url = Environment.GetEnvironmentVariable("DATABASE_URL");
			_conexion = new MySqlConnection(url);
		}

		public void Dispose() {
			_conexion.Close();
			_conexion.Dispose();
		}

		#region Base
		public int Ejecutar(string consulta, object carga = null)
			=>  _conexion.Execute(consulta, carga);

		public IEnumerable<T> Seleccionar<T>(string consulta, object carga = null)
			=> _conexion.Query<T>(consulta, carga);

		public T Obtener<T>(string consulta, object carga = null)
			=> _conexion.QuerySingleOrDefault<T>(consulta, carga);
		#endregion

		#region CRUD
		public bool Insertar<T>(T entidad) where T : IEntidad {
			var id = _conexion.Insert(entidad);
			return id is int;
		}
		public bool Editar<T>(T entidad) where T : IEntidad {
			var filasAfectadas = _conexion.Update(entidad);
			return filasAfectadas > 0;
		}

		public bool Eliminar<T>(T entidad) where T : IEntidad {
			var id = _conexion.Insert(entidad);
			return id is int;
		}
		#endregion
	}
}
