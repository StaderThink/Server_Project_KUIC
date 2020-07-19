using System;
using System.Collections.Generic;
using Aplicacion.Salidas.Formularios;
using Dominio.Productos;
using Dominio.Salidas;

namespace Aplicacion.Salidas
{
    public sealed class ServicioRegistradorSalida
    {
        public bool Registrar(FormularioRegistrarSalida formulario)
        {
            RepositorioSalida repoSalida = new RepositorioSalida();

            try
            {
                Salida salida = formulario.Salida;
                IEnumerable<DetalleSalida> detalles = formulario.Detalles;

                salida.Fecha = DateTime.Now;

                if (repoSalida.Insertar(salida))
                {
                    RepositorioDetalleSalida repoDetalle = new RepositorioDetalleSalida();
                    RepositorioProducto repoProducto = new RepositorioProducto();

                    int id = repoSalida.UltimoPorId();

                    foreach (DetalleSalida detalle in detalles)
                    {
                        detalle.Salida = id;

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
