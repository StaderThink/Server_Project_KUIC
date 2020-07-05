using System;
using System.Collections.Generic;
using Aplicacion.Salidas;
using Aplicacion.Salidas.Formularios;
using Dominio.Entradas;
using Dominio.Salidas;

namespace Aplicacion.Entradas
{
    public sealed class ServicioEliminadorEntrada
    {
        public IEnumerable<DetalleSalida> GenerarDetalleSalida(IEnumerable<DetalleEntrada> detalleEntrada)
        {
            List<DetalleSalida> detalleSalida = new List<DetalleSalida>();

            foreach (DetalleEntrada detalle in detalleEntrada)
            {
                detalleSalida.Add(new DetalleSalida
                {
                    Cantidad = detalle.Cantidad,
                    Producto = detalle.Producto,
                });
            }

            return detalleSalida;
        }

        public bool Eliminar(Entrada entrada)
        {
            ServicioRegistradorSalida servicioSalida = new ServicioRegistradorSalida();

            try
            {
                // generar salida

                RepositorioDetallEntrada repoDetalle = new RepositorioDetallEntrada();
                IEnumerable<DetalleEntrada> detalles = repoDetalle.ListarPorEntrada(entrada);

                FormularioRegistrarSalida formulario = new FormularioRegistrarSalida()
                {
                    Salida = new Salida
                    {
                        Fecha = DateTime.Now,
                        Observacion = $"Correccion para entrada #{entrada.Id}"
                    },

                    Detalles = GenerarDetalleSalida(detalles)
                };

                servicioSalida.Registrar(formulario);

                // eliminar entrada

                repoDetalle.EliminarPorEntrada(entrada);

                RepositorioEntrada repoEntrada = new RepositorioEntrada();
                return repoEntrada.Eliminar(entrada);
            }

            catch
            {
                throw;
            }
        }
    }
}
