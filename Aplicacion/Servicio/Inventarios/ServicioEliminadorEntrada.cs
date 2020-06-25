using System;
using System.Collections.Generic;
using Aplicacion.Modelo.Inventarios;
using Dominio.Modelo;
using Dominio.Repositorio;

namespace Aplicacion.Servicio.Inventarios
{
    public sealed class ServicioEliminadorEntrada
    {
        public IEnumerable<DetalleSalida> GenerarDetalleSalida(IEnumerable<DetalleEntrada> detalleEntrada)
        {
            var detalleSalida = new List<DetalleSalida>();

            foreach (var detalle in detalleEntrada)
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
            var servicioSalida = new ServicioRegistradorSalida();

            try
            {
                // generar salida
                
                var repoDetalle = new RepoDetalleEntrada();
                var detalles = repoDetalle.ListarPorEntrada(entrada);

                var formulario = new FormularioRegistrarSalida()
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

                var repoEntrada = new RepoEntrada();
                return repoEntrada.Eliminar(entrada);
            }

            catch
            {
                throw;
            }
        }
    }
}
