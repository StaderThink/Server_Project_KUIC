using System.Collections.Generic;

namespace Dominio.Cargos
{
    public sealed class RepositorioCargo
    {
        public bool Editar(Cargo entidad)
        {
            using Conexion conexion = new Conexion();

            string consulta = @"
				update cargo set
					nombre = @Nombre, pedidos = @Pedidos,
					usuarios = @Usuarios, logistica = @Logistica,
					clientes = @Clientes, solicitar = @Solicitar
				where id = @Id
			";

            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }

        public bool Insertar(Cargo entidad)
        {
            using Conexion conexion = new Conexion();

            string consulta = @"
				insert into cargo(nombre, pedidos, usuarios, logistica, clientes, solicitar)
				values (@Nombre, @Pedidos, @Usuarios, @Logistica, @Clientes, @Solicitar)
			";

            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }

        public IEnumerable<Cargo> Listar()
        {
            using Conexion conexion = new Conexion();
            return conexion.Seleccionar<Cargo>("select * from cargo order by nombre desc");
        }

        public Cargo PorId(int id)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from cargo where id = @id";

            return conexion.Obtener<Cargo>(consulta, new { id });
        }
    }
}
