using System;
using System.Text;

namespace Centaurus.Seguridad {
    public abstract class Proveedor {
        internal readonly byte[] _llave;

        public Proveedor() {
            var texto = Environment.GetEnvironmentVariable("SECRET_TOKEN");
            _llave = Encoding.UTF8.GetBytes(texto);
        }

        public abstract string Encriptar(object carga);
        public abstract object Traduccir(string encriptado);
    }
}
