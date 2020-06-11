﻿namespace Dominio.Modelo {
    public sealed class Producto : IEntidad {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public double Precio { get; set; }
        public string MinCantidad { get; set; }
        public double MinPeso { get; set; }
        public double MaxPeso { get; set; }
        public Magnitud Magnitud { get; set; }
        public Presentacion Presentacion { get; set; }
        public int Categoria { get; set; }
    }
}
