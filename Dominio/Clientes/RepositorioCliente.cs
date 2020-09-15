using System;
using System.Collections.Generic;

namespace Dominio.Clientes
{
    public sealed class RepositorioCliente
    {
        public bool Editar(Cliente entidad)
        {
            using Conexion conexion = new Conexion();

            string consulta = $@"
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
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(Cliente entidad)
        {
            using Conexion conexion = new Conexion();

            string consulta = "delete from cliente where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Insertar(Cliente entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = $@"
                insert into cliente (nombre, encargado, rut, correo, direccion, telefono, creado, actualizado, activo) 
                values (@Nombre, @Encargado, @Rut, @Correo, @Direccion, @Telefono, @Creado, @Actualizado, @Activo)";

            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public IEnumerable<Cliente> Listar()
        {
            using Conexion conexion = new Conexion();
            return conexion.Seleccionar<Cliente>("select * from cliente order by nombre desc");
        }

        public Cliente PorId(int id)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from cliente where id = @id";

            return conexion.Obtener<Cliente>(consulta, new { id });
        }
        public Cliente PorRut(string rut)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from cliente where rut = @rut";
            return conexion.Obtener<Cliente>(consulta, new { rut });
        }
    }

}