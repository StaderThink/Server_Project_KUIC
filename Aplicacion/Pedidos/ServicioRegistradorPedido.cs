using System;
using Aplicacion.Pedidos.Formularios;
using Dominio.Productos;
using Dominio.Pedidos;

namespace Aplicacion.Pedidos
{
    public sealed class ServicioRegistradorPedido
    {
        public bool Registrar(FormularioRegistrarPedido formulario)
        {
            RepositorioPedido repoPedido = new RepositorioPedido();

            try
            {
                Pedido pedido = formulario.Pedido;
                System.Collections.Generic.IEnumerable<DetallePedido> detalles = formulario.Detalles;

                if (repoPedido.Insertar(pedido))
                {
                    RepositorioDetallePedido repoDetalle = new RepositorioDetallePedido();
                    RepositorioProducto repoProducto = new RepositorioProducto();

                    int id = repoPedido.UltimoPorId();

                    foreach (DetallePedido detalle in detalles)
                    {
                        detalle.Pedido = id;

                        if (repoProducto.PorId(detalle.Producto) is Producto producto)
                        {
                            if (producto.Existencias > 0 && producto.Existencias >= detalle.Cantidad)
                            {
                                producto.Existencias -= detalle.Cantidad;

                                repoProducto.Editar(producto);
                                repoDetalle.Insertar(detalle);
                            }
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