using System;
using Dominio.Clientes;

namespace Aplicacion.Clientes
{
    public sealed class ServicioRegistradorCliente
    {
        public bool Registrar(Cliente cliente)
        {
            RepositorioCliente repoCliente = new RepositorioCliente();

            // valores por defecto
            cliente.Activo = true;
            cliente.Actualizado = DateTime.Now;
            cliente.Creado = DateTime.Now;

            return repoCliente.Insertar(cliente);
        }
    }
}
