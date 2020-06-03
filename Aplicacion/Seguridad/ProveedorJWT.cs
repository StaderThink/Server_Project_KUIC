using Jose;

namespace Aplicacion.Seguridad {
	public sealed class ProveedorJWT: Proveedor {
		public override string Encriptar(object carga)
			=> JWT.Encode(carga, _llave, JwsAlgorithm.HS256);

		public T Traduccir<T>(string encriptado)
			=> JWT.Decode<T>(encriptado, _llave, JwsAlgorithm.HS256);

		public override object Traduccir(string encriptado)
			=> Traduccir<object>(encriptado);
	}
}
