using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    public interface IVistaTuplaVenta : IVistaTupla {
        public long Id { get; set; }
        public DateTime FechaVenta { get; set; }
        public string NombreCliente { get; set; }
        public string? MetodoPagoPrincipal { get; set; }
        public decimal TotalBruto { get; set; }
        public decimal DescuentoTotal { get; set; }
        public decimal ImpuestoTotal { get; set; }
        public decimal ImporteTotal { get; set; }
        public EstadoVenta EstadoVenta { get; set; }
        public bool Activo { get; set; }
    }
}