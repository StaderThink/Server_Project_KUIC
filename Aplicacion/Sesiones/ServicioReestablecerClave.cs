using Aplicacion.Sesiones.Formularios;
using Dominio.Usuarios;

namespace Aplicacion.Sesiones
{
    public class ServicioReestablecerClave
    {
        public bool ReestablecerClave(FormularioReestablecerClave formulario)
        {
            try
            {
                RepositorioUsuario repoUsuario = new RepositorioUsuario();

                if (repoUsuario.PorDocumento(formulario.Documento) is Usuario usuario)
                {
                    if (usuario.Expedicion == formulario.FechaExpedicion)
                    {
                        usuario.Clave = formulario.NuevaClave;
                        repoUsuario.Editar(usuario);

                        return true;
                    }
                }

                return false;
            }

            catch
            {
                return false;
            }
        }
    }
}
