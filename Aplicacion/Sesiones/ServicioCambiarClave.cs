using Aplicacion.Sesiones.Formularios;
using Dominio.Usuarios;

namespace Aplicacion.Sesiones
{
    public class ServicioCambiarClave
    {
        public bool CambiarClave(FormularioCambiarClave formulario)
        {
            RepositorioUsuario repositorio = new RepositorioUsuario();

            if (repositorio.PorId(formulario.Usuario) is Usuario entidad)
            {
                if (entidad.Clave == formulario.ClaveAnterior)
                {
                    entidad.Clave = formulario.NuevaClave;
                    return repositorio.Editar(entidad);
                } 
            }

            return false;
        }
    }
}
