using Dominio.Modelo;

using System.Collections.Generic;

namespace Dominio.Repositorio
{
    public sealed class RepoSalida : IRepo<Salida>
    {
        public bool Insertar(Salida entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = "insert into salida (fecha, observacion, pedido) values (@Fecha, @Observacion, @Pedido)";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Editar(Salida entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = @"
				update salida set fecha = @Fecha, observacion = @Observacion, pedido = @Pedido
				where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(Salida entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = "delete from salida where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public IEnumerable<Salida> Listar()
        {
            using Conexion conexion = new Conexion();
            return conexion.Seleccionar<Salida>("select * from salida");
        }
        public Salida PorId(int id)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from salida where id = @id";
            return conexion.Obtener<Salida>(consulta, new { id });
        }

        public int UltimoPorId()
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from salida order by id desc limit 0, 1";

            var salida = conexion.Obtener<Salida>(consulta);
            return salida.Id;
        }
    }
}
