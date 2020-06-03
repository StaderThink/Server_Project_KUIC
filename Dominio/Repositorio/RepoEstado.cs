using Dominio.Modelo;
using System.Collections.Generic;

namespace Dominio.Repositorio
{
    public sealed class RepoEstado : IRepo<Estado>
    {
        public bool Editar(Estado entidad)
        {
            using var conexion = new Conexion();

            var consulta = $@"
                update estado set 
                    nombre = @Nombre,
                    orden = @Orden,
                    cancelable = @Cancelable
                    
                    where id = @Id
           ";
            var filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(Estado entidad)
        {
            using var conexion = new Conexion();

            var consulta = "delete from estado where id = @Id";
            var filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Insertar(Estado entidad)
        {
            using var conexion = new Conexion();
            var consulta = $@"insert into (nombre, orden, cancelable) 
                            values (@Nombre, @Orden, @Cancelable)";
            var filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public IEnumerable<Estado> Listar()
        {
            using var conexion = new Conexion();
            return conexion.Seleccionar<Estado>("select * from estado");
        }

        public Estado PorId(int id)
        {
            using var conexion = new Conexion();
            var consulta = "select * from estado where id = @id";

            return conexion.Obtener<Estado>(consulta, new { id });
        }
        public Estado PorOrden(int orden)
        {
            using var conexion = new Conexion();
            var consulta = "select * from estado where orden = @Orden";
            return conexion.Obtener<Estado>(consulta, new { orden });
        }
    }
}
