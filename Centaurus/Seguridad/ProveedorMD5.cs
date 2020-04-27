using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace Centaurus.Seguridad {
    public sealed class ProveedorMD5: Proveedor {
        private TripleDESCryptoServiceProvider GenerarCriptografo() {
            var md5 = new MD5CryptoServiceProvider();

            return new TripleDESCryptoServiceProvider() {
                Key = md5.ComputeHash(_llave),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
        }

        public override string Encriptar(object carga) {
            using var proveedor = GenerarCriptografo();

            var texto = carga.ToString();
            var buffer = Encoding.UTF8.GetBytes(texto);

            var transformador = proveedor.CreateEncryptor();
            var resultado = transformador.TransformFinalBlock(buffer, 0, buffer.Length);

            return Convert.ToBase64String(resultado);
        }

        public override object Traduccir(string encriptado) {
            using var proveedor = GenerarCriptografo();

            var buffer = Convert.FromBase64String(encriptado);
            var transformador = proveedor.CreateDecryptor();
            var resultado = transformador.TransformFinalBlock(buffer, 0, buffer.Length);

            return Encoding.UTF8.GetString(resultado);
        }
    }
}
