using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    public interface IVistaTuplaCarrito : IVistaTupla {
        long IdProducto { get; set; }
        public string Codigo { get; set; }
        public string NombreProducto { get; set; }
        public decimal CostoGeneral { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Descuento { get; set; }
        public decimal ImpuestoAdicional { get; set; }
        public UnidadMedida? UnidadMedida { get; set; }
    }
}