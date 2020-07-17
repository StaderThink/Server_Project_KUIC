using Dominio.Cargos;
using Dominio.Notificaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Notificaciones
{
    public sealed class ServicioRegistradorNotificacion
    {
        private readonly RepositorioNotificacion repositorio;

        public ServicioRegistradorNotificacion()
        {
            repositorio = new RepositorioNotificacion();
        }

        public bool Registrar(FormularioRegistrarNotificacion formulario)
        {
            try
            {
                Notificacion notificacion = formulario.Notificacion;
                IEnumerable<Segmento> segmentos = formulario.Segmentos;

                notificacion.FechaInicio = DateTime.Now;

                if (repositorio.Insertar(notificacion))
                {
                    RepositorioSegmento repoSegmento = new RepositorioSegmento();
                    RepositorioCargo repoCargo = new RepositorioCargo();

                    int id = repositorio.PorUltimoId();

                    foreach (Segmento segmento in segmentos)
                    {
                        segmento.Notificacion = id;

                        if (repoCargo.PorId(segmento.Cargo) is Cargo cargo)
                        {
                            repoSegmento.Insertar(segmento);
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
