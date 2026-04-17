using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    internal interface IVistaTuplaVentaPresentacion : IVistaTupla {
        long Id { get; set; }
        string NombreUM {  get; set; }
        string AbreviaturaUM { get; set; }
        decimal Cantidad { get; set; }
        decimal PrecioVenta {  get; set; }
        decimal PrecioPorUnidad { get; set; }
        decimal Descuento { get; set; }         
        bool Estado {  get; set; }
    }
}
