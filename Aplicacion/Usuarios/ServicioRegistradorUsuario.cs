using System;
using Dominio.Usuarios;

namespace Aplicacion.Usuarios
{
    public sealed class ServicioRegistradorUsuario
    {
        public bool Registrar(Usuario usuario)
        {
            RepositorioUsuario repoUsuario = new RepositorioUsuario();

            // valores por defecto

            usuario.Clave = Guid.NewGuid().ToString()[0..8];

            usuario.Activo = true;
            usuario.Actualizado = DateTime.Now;
            usuario.Creado = DateTime.Now;

            return repoUsuario.Insertar(usuario);
        }
    }
}
