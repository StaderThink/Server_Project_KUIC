using System;
using Aplicacion.Entradas.Formularios;
using Dominio.Entradas;
using Dominio.Productos;

namespace Aplicacion.Entradas
{
    public sealed class ServicioRegistradorEntrada
    {
        private readonly RepositorioEntrada repositorio;

        public ServicioRegistradorEntrada()
        {
            repositorio = new RepositorioEntrada();
        }

        public bool Registrar(FormularioRegistrarEntrada formulario)
        {
            try
            {
                Entrada entrada = formulario.Entrada;
                System.Collections.Generic.IEnumerable<DetalleEntrada> detalles = formulario.Detalles;

                entrada.Fecha = DateTime.Now;

                if (repositorio.Insertar(entrada))
                {
                    RepositorioDetallEntrada repoDetalle = new RepositorioDetallEntrada();
                    RepositorioProducto repoProducto = new RepositorioProducto();

                    int id = repositorio.UltimoPorId();

                    foreach (DetalleEntrada detalle in detalles)
                    {
                        detalle.Entrada = id;

                        if (repoProducto.PorId(detalle.Producto) is Producto producto)
                        {
                            producto.Existencias += detalle.Cantidad;

                            repoDetalle.Insertar(detalle);
                            repoProducto.Editar(producto);
                        }
                    }

                    return true;
                }

                return false;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
