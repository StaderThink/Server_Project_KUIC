﻿namespace Dominio.Modelo
{
    public sealed class DetalleEntrada : IEntidad, IDetalle
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int Entrada { get; set; }
        public int Producto { get; set; }
    }
}
