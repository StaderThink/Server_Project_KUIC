using Dominio.Notificaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Notificaciones
{
    public sealed class ServicioEliminadorNotificacion
    {
        private readonly RepositorioNotificacion repositorio;

        public ServicioEliminadorNotificacion()
        {
            repositorio = new RepositorioNotificacion();
        }

        public bool Eliminar(Notificacion notificacion)
        {
            try
            {
                RepositorioNotificacion repoNotificacion = new RepositorioNotificacion();
                RepositorioSegmento repoSegmento = new RepositorioSegmento();

                int id = notificacion.Id;

                if (repoSegmento.EliminarPorNotificacion(notificacion))
                {
                    repoNotificacion.Eliminar(notificacion);

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
