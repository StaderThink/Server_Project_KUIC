using Dapper;

using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;

namespace Dominio {
    public sealed class Conexion : IDisposable {
        private readonly MySqlConnection _conexion;

        public Conexion() {
            string url = Environment.GetEnvironmentVariable("DATABASE_URL");
            _conexion = new MySqlConnection(url);
        }

        public void Dispose() {
            _conexion.Close();
            _conexion.Dispose();
        }

        public int Ejecutar(string consulta, object carga = null) {
            return _conexion.Execute(consulta, carga);
        }

        public IEnumerable<T> Seleccionar<T>(string consulta, object carga = null) {
            return _conexion.Query<T>(consulta, carga);
        }

        public T Obtener<T>(string consulta, object carga = null) {
            return _conexion.QuerySingleOrDefault<T>(consulta, carga);
        }
    }
}
