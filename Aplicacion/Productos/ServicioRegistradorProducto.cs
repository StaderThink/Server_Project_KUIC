using System;
using Dominio.Productos;

namespace Aplicacion.Productos
{
    public sealed class ServicioRegistradorProducto
    {
        private readonly RepositorioProducto repoProducto;

        public ServicioRegistradorProducto()
        {
            repoProducto = new RepositorioProducto();
        }

        public bool Registrar(Producto producto)
        {
            try
            {
                producto.Existencias = 0;

                repoProducto.Insertar(producto);
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
