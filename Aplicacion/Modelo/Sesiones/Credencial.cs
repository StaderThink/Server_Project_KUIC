namespace Aplicacion.Modelo.Sesiones
{
    public sealed class Credencial
    {
        public string Documento { get; set; }
        public string Clave { get; set; }

        public Credencial() { }

        public Credencial(string documento, string clave)
        {
            Documento = documento;
            Clave = clave;
        }
    }
}
