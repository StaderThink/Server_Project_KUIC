namespace Aplicacion.Servicio
{
    public abstract class Traductor<Entrada, Salida>
    {
        public abstract Salida Generar(Entrada carga);
        public abstract Entrada Traducir(Salida carga);
    }
}
