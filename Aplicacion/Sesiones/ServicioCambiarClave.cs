using Aplicacion.Mailer;
using Aplicacion.Sesiones.Formularios;
using Dominio.Usuarios;

namespace Aplicacion.Sesiones
{
    public class ServicioCambiarClave
    {
        public bool CambiarClave(FormularioCambiarClave formulario)
        {
            RepositorioUsuario repository = new RepositorioUsuario();

            if (repository.PorId(formulario.Usuario) is Usuario entity)
            {
                if (entity.Clave == formulario.ClaveAnterior)
                {
                    entity.Clave = formulario.NuevaClave;

                    bool response = repository.Editar(entity);

                    if (response)
                    {
                        var correspondence = new Correspondence();
                        correspondence.SendPasswordChange(entity);
                    }

                    return response;
                } 
            }

            return false;
        }
    }
}
