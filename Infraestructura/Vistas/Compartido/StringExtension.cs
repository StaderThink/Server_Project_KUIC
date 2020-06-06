namespace Infraestructura.Vistas.Compartido {
    public static class StringExtension {
        public static string ToCapitalize(this string texto) {
            string primeraLetra = texto[0].ToString().ToUpper();
            return primeraLetra + texto[1..];
        }
    }
}
