namespace Corvus.Caso {
	public interface ITraductor<Ingreso, Salida> {
		Salida Generar(Ingreso datos);
		Ingreso Traducir(Salida datos);
	}
}
