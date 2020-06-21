using System;
using Dominio.Modelo;
using Dominio.Repositorio;

namespace Aplicacion.Servicio.Inventarios.Productos
{
    public sealed class ServicioRegistradorProducto
    {
        private readonly IRepo<Producto> repoProducto;
        private readonly IRepo<Existencia> repoExistencia;

        public ServicioRegistradorProducto(IRepo<Producto> repoProducto, IRepo<Existencia> repoExistencia)
        {
            this.repoProducto = repoProducto;
            this.repoExistencia = repoExistencia;
        }

        public bool Registrar(Producto producto)
        {
            try
            {
                repoProducto.Insertar(producto);
                int id = repoProducto.UltimoPorId();

                var existencia = new Existencia()
                {
                    Producto = id,
                    Cantidad = 0
                };

                repoExistencia.Insertar(existencia);
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
