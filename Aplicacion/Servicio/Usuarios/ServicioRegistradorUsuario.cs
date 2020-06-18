using Dominio.Modelo;
using Dominio.Repositorio;

using System;

namespace Aplicacion.Servicio.Usuarios
{
    public sealed class ServicioRegistradorUsuario
    {
        private readonly IRepo<Usuario> repo;

        public ServicioRegistradorUsuario(IRepo<Usuario> repo)
        {
            this.repo = repo;
        }

        public bool Registrar(Usuario usuario)
        {
            // valores por defecto

            usuario.Clave = Guid.NewGuid().ToString()[0..8];

            usuario.Activo = true;
            usuario.Actualizado = DateTime.Now;
            usuario.Creado = DateTime.Now;

            return repo.Insertar(usuario);
        }
    }
}
