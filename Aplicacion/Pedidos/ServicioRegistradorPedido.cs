using System;
using Aplicacion.Pedidos.Formularios;
using Dominio.Productos;
using Dominio.Pedidos;
using Aplicacion.Salidas;
using System.Collections.Generic;
using Dominio;
using Dominio.Salidas;
using Aplicacion.Salidas.Formularios;
using System.Linq;

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
                IEnumerable<DetallePedido> detalles = formulario.Detalles;
                pedido.Activo = true;
                pedido.Estado = Estado.Pendiente;
                if (repoPedido.Insertar(pedido))
                {
                    var repoDetalle = new RepositorioDetallePedido();
                    var listaSalida = new List<IDetalle>();

                    pedido.Id = repoPedido.UltimoPorId();

                    foreach (DetallePedido detalle in detalles)
                    {
                        detalle.Pedido = pedido.Id;

                        repoDetalle.Insertar(detalle);
                    }

                    return RegistrarSalida(pedido, detalles);
                }

                return false;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        private bool RegistrarSalida(Pedido pedido, IEnumerable<IDetalle> detalles)
        {
            List<DetalleSalida> salidas = new List<DetalleSalida>();

            foreach (var detalle in detalles)
            {
                salidas.Add(new DetalleSalida { Cantidad = detalle.Cantidad, Producto = detalle.Producto });
            }

            // registrar la salida

            var registradorSalida = new ServicioRegistradorSalida();
            var formularioSalida = new FormularioRegistrarSalida
            {
                Salida = new Salida
                {
                    Fecha = pedido.Fecha,
                    Observacion = $"Salida para pedido #{pedido.Id}",
                    Pedido = pedido.Id
                },

                Detalles = salidas
            };

            return registradorSalida.Registrar(formularioSalida);
        }
    }
}