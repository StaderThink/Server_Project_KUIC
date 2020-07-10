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

                    int id = repositorio.PorUltimoId();

                    foreach (Segmento segmento in segmentos)
                    {
                        segmento.Notificacion = id;
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
