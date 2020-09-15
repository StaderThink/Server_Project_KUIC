using System.Collections.Generic;

namespace Dominio.Entradas
{
    public sealed class RepositorioEntrada
    {
        public bool Insertar(Entrada entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = $@"
                insert into entrada (fecha, observacion)
                values (@Fecha, @Observacion)
            ";

            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }

        public bool Editar(Entrada entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = @$"
				update entrada set fecha = @Fecha, observacion = @Observacion
				where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(Entrada entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = "delete from entrada where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public IEnumerable<Entrada> Listar()
        {
            using Conexion conexion = new Conexion();
            return conexion.Seleccionar<Entrada>("select * from entrada order by fecha desc");
        }
        public Entrada PorId(int id)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from entrada where id = @id";
            return conexion.Obtener<Entrada>(consulta, new { id });
        }

        public int UltimoPorId()
        {
            using Conexion conexion = new Conexion();

            string consulta = "select * from entrada order by id desc limit 0, 1";
            Entrada entrada = conexion.Obtener<Entrada>(consulta);

            return entrada.Id;
        }
    }
}
