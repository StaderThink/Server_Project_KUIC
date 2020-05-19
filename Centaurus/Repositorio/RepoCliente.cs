using Centaurus.Modelo;
using System.Collections.Generic;

namespace Centaurus.Repositorio
{
    public sealed class RepoCliente : IRepo<Cliente>
    {
        public bool Editar(Cliente entidad)
        {
            using var conexion = new Conexion();

            var consulta = $@"
                update cliente set 
                    nombre = @Nombre,
                    encargado = @Encargado,
                    rut = @Rut,
                    correo = @Correo,
                    direccion = @Direccion,
                    telefono = @Telefono,
                    actualizado = curdate(),
					activo = @Activo
                    where id = @Id
           ";
            var filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(Cliente entidad)
        {
            using var conexion = new Conexion();

            var consulta = "delete from cliente where id = @Id";
            var filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Insertar(Cliente entidad)
        {
            using var conexion = new Conexion();
            var consulta = $@"insert into (nombre, encargado, rut, correo, direccion, telefono) 
                            values (@Nombre, @Encargado, @Rut, @Correo, @Direccion, @Telefono)";
            var filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public IEnumerable<Cliente> Listar()
        {
            using var conexion = new Conexion();
            return conexion.Seleccionar<Cliente>("select * from cliente");
        }

        public Cliente PorId(int id)
        {
            using var conexion = new Conexion();
            var consulta = "select * from cliente where id = @id";

            return conexion.Obtener<Cliente>(consulta, new { id });
        }
        public Cliente PorRut(string rut)
        {
            using var conexion = new Conexion();
            var consulta = "slect * from cliente where rut = @Rut";
            return conexion.Obtener<Cliente>(consulta, new { rut });
        }
    }
}
