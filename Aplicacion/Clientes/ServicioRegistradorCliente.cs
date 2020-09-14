using System;
using Aplicacion.Mailer;
using Dominio.Clientes;

namespace Aplicacion.Clientes
{
    public sealed class ServicioRegistradorCliente
    {
        public bool Registrar(Cliente cliente)
        {
            RepositorioCliente repoCliente = new RepositorioCliente();

            // valores por defecto
            cliente.Activo = false;
            cliente.Actualizado = DateTime.Now;
            cliente.Creado = DateTime.Now;


            if (repoCliente.Insertar(cliente))
            {
                var correspondencia = new Correspondence();
                correspondencia.SendMessageClient(cliente);

                return true;
            }

            return false;
        }
    }
}
