using System;
using System.Text;

namespace Aplicacion.Seguridad {
    public abstract class Proveedor {
        protected readonly byte[] _llave;

        public Proveedor() {
            string texto = Environment.GetEnvironmentVariable("SECRET_TOKEN");
            _llave = Encoding.UTF8.GetBytes(texto);
        }

        public abstract string Encriptar(object carga);
        public abstract object Traduccir(string encriptado);
    }
}
