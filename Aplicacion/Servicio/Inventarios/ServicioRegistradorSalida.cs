using System;
using Aplicacion.Modelo.Inventarios;
using Dominio.Modelo;
using Dominio.Repositorio;

namespace Aplicacion.Servicio.Inventarios
{
    public sealed class ServicioRegistradorSalida
    {
        public bool Registrar(FormularioRegistrarSalida formulario)
        {
            var repoSalida = new RepoSalida();

            try
            {
                var entrada = formulario.Salida;
                var detalles = formulario.Detalles;

                entrada.Fecha = DateTime.Now;

                if (repoSalida.Insertar(entrada))
                {
                    var repoDetalle = new RepoDetalleSalida();
                    var repoExistencia = new RepoExistencia();

                    int id = repoSalida.UltimoPorId();

                    foreach (var detalle in detalles)
                    {
                        detalle.Salida = id;

                        if (repoExistencia.PorProducto(detalle.Producto) is Existencia existencia)
                        {
                            if (existencia.Cantidad > 0 && existencia.Cantidad >= detalle.Cantidad)
                            {
                                existencia.Cantidad -= detalle.Cantidad;
                                repoExistencia.Editar(existencia);
                            }
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
