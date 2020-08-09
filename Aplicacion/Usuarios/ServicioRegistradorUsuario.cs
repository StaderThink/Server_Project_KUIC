using System;
using Aplicacion.Mailer;
using Dominio.Usuarios;

namespace Aplicacion.Usuarios
{
    public sealed class ServicioRegistradorUsuario
    {
        public bool Registrar(Usuario usuario)
        {
            RepositorioUsuario repositorio = new RepositorioUsuario();

            // valores por defecto

            usuario.Clave = Guid.NewGuid().ToString()[0..8];

            usuario.Actualizado = DateTime.Now;
            usuario.Creado = DateTime.Now;

            if (repositorio.Insertar(usuario))
            {
                var correspondencia = new Correspondence();
                correspondencia.SendDefaultPassword(usuario);

                return true;
            }

            return false;
        }
    }
}
