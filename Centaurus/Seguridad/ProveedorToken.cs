using Jose;

namespace Centaurus.Seguridad {
    public sealed class ProveedorToken: Proveedor {
        public override string Encriptar(object carga)
            => JWT.Encode(carga, _llave, JwsAlgorithm.HS256);

        public T Traduccir<T>(string encriptado)
            => JWT.Decode<T>(encriptado, _llave, JwsAlgorithm.HS256);

        public override object Traduccir(string encriptado)
            => Traduccir<object>(encriptado);
    }
}
