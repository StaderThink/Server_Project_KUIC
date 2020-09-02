using System;
using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;

namespace Dominio
{
    internal sealed class Conexion : IDisposable
    {
        private readonly MySqlConnection connection;

        public Conexion()
        {
            string host = Environment.GetEnvironmentVariable("DATABASE_HOST");
            string name = Environment.GetEnvironmentVariable("DATABASE_NAME");
            string user = Environment.GetEnvironmentVariable("DATABASE_USER");
            string password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");

            string url = $"server={host}; database={name}; user={user}; password={password};";

            connection = new MySqlConnection(url);
        }

        public void Dispose()
        {
            connection.Close();
            connection.Dispose();
        }

        public int Ejecutar(string consulta, object carga = null)
        {
            return connection.Execute(consulta, carga);
        }

        public IEnumerable<T> Seleccionar<T>(string consulta, object carga = null)
        {
            return connection.Query<T>(consulta, carga);
        }

        public T Obtener<T>(string consulta, object carga = null)
        {
            return connection.QuerySingleOrDefault<T>(consulta, carga);
        }
    }
}
