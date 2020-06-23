using Aplicacion.Modelo.Inventarios;
using Dominio.Modelo;
using Dominio.Repositorio;

using System;
using System.Collections.Generic;

namespace Aplicacion.Servicio.Inventarios
{
    public sealed class ServicioRegistradorEntrada
    {
        public bool Registrar(FormularioRegistrarEntrada formulario)
        {
            var repoEntrada = new RepoEntrada();
            
            try
            {
                var entrada = formulario.Entrada;
                var detalles = formulario.Detalle;

                entrada.Fecha = DateTime.Now;

                if (repoEntrada.Insertar(entrada))
                {
                    var repoDetalle = new RepoDetalleEntrada();
                    var repoExistencia = new RepoExistencia();

                    int id = repoEntrada.UltimoPorId();

                    foreach (var detalle in detalles)
                    {
                        detalle.Entrada = id;

                        if (repoExistencia.PorProducto(detalle.Producto) is Existencia existencia)
                        {
                            existencia.Cantidad += detalle.Cantidad;
                            repoExistencia.Editar(existencia);
                        }

                        repoDetalle.Insertar(detalle);
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
