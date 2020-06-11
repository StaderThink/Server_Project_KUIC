using System;
using System.Security.Cryptography;
using System.Text;

namespace Aplicacion.Seguridad {
    public sealed class ProveedorMD5 : Proveedor {
        private TripleDESCryptoServiceProvider GenerarCriptografo() {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            return new TripleDESCryptoServiceProvider() {
                Key = md5.ComputeHash(_llave),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
        }

        public override string Encriptar(object carga) {
            using TripleDESCryptoServiceProvider proveedor = GenerarCriptografo();

            string texto = carga.ToString();
            byte[] buffer = Encoding.UTF8.GetBytes(texto);

            ICryptoTransform transformador = proveedor.CreateEncryptor();
            byte[] resultado = transformador.TransformFinalBlock(buffer, 0, buffer.Length);

            return Convert.ToBase64String(resultado);
        }

        public override object Traduccir(string encriptado) {
            using TripleDESCryptoServiceProvider proveedor = GenerarCriptografo();

            byte[] buffer = Convert.FromBase64String(encriptado);
            ICryptoTransform transformador = proveedor.CreateDecryptor();
            byte[] resultado = transformador.TransformFinalBlock(buffer, 0, buffer.Length);

            return Encoding.UTF8.GetString(resultado);
        }
    }
}
