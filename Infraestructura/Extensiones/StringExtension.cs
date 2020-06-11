namespace Infraestructura.Extensiones {
    public static class StringExtension {
        public static string ToCapitalize(this string texto) {
            if (string.IsNullOrEmpty(texto)) {
                return texto;
            }

            else {
                string primeraLetra = texto[0].ToString().ToUpper();
                return primeraLetra + texto[1..];
            }
        }
    }
}
