using Dominio.Modelo;

using System.Collections.Generic;

namespace Dominio.Repositorio
{
    public sealed class RepoEstado : IRepo<Estado>
    {
        public bool Editar(Estado entidad)
        {
            using Conexion conexion = new Conexion();

            string consulta = $@"
                update estado set 
                    nombre = @Nombre,
                    orden = @Orden,
                    cancelable = @Cancelable
                    
                    where id = @Id
           ";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(Estado entidad)
        {
            using Conexion conexion = new Conexion();

            string consulta = "delete from estado where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Insertar(Estado entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = $@"insert into estado (nombre, orden, cancelable) 
                            values (@Nombre, @Orden, @Cancelable)";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public IEnumerable<Estado> Listar()
        {
            using Conexion conexion = new Conexion();
            return conexion.Seleccionar<Estado>("select * from estado");
        }

        public Estado PorId(int id)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from estado where id = @id";

            return conexion.Obtener<Estado>(consulta, new { id });
        }
        public Estado PorOrden(int orden)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from estado where orden = @orden";
            return conexion.Obtener<Estado>(consulta, new { orden });
        }
    }
}
