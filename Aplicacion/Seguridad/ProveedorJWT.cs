using Jose;

namespace Aplicacion.Seguridad {
    public sealed class ProveedorJWT : Proveedor {
        public override string Encriptar(object carga) {
            return JWT.Encode(carga, _llave, JwsAlgorithm.HS256);
        }

        public T Traduccir<T>(string encriptado) {
            return JWT.Decode<T>(encriptado, _llave, JwsAlgorithm.HS256);
        }

        public override object Traduccir(string encriptado) {
            return Traduccir<object>(encriptado);
        }
    }
}
