using Dominio.Modelo;

using System.Collections.Generic;

namespace Dominio.Repositorio
{
    public sealed class RepoPedido : IRepo<Pedido>
    {
        public bool Editar(Pedido entidad)
        {
            using Conexion conexion = new Conexion();

            string consulta = $@"
                update pedido set 
                    fecha = @Fecha,
                    cliente = @Cliente,
                    asesor = @Asesor,
                    estado = @Estado,
                    cancelado = @Cancelado,
                    descuento = @Descuento,
                    observacion = @Observacion
                    where id = @Id
           ";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Eliminar(Pedido entidad)
        {
            using Conexion conexion = new Conexion();

            string consulta = "delete from pedido where id = @Id";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public bool Insertar(Pedido entidad)
        {
            using Conexion conexion = new Conexion();
            string consulta = $@"insert into (fecha, cliente, asesor, estado, cancelado, descuento, observacion) 
                            values (@Fecha, @Cliente, @Asesor, @Estado, @Cancelado, @Descuento, @Observacion)";
            int filasAfectadas = conexion.Ejecutar(consulta, entidad);
            return filasAfectadas > 0;
        }
        public IEnumerable<Pedido> Listar()
        {
            using Conexion conexion = new Conexion();
            return conexion.Seleccionar<Pedido>("select * from pedido");
        }

        public Pedido PorId(int id)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from pedido where id = @id";
            return conexion.Obtener<Pedido>(consulta, new { id });
        }
        public Pedido PorAsesor(int asesor)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from pedido where asesor = @asesor";
            return conexion.Obtener<Pedido>(consulta, new { asesor });
        }
        public Pedido PorCliente(int cliente)
        {
            using Conexion conexion = new Conexion();
            string consulta = "select * from pedido where cliente = @cliente";
            return conexion.Obtener<Pedido>(consulta, new { cliente });
        }
    }
}
