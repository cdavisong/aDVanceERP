using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    internal interface IVistaTuplaPedido : IVistaTupla {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaPedido { get; set; }
        public string NombreCliente { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public decimal ImporteTotal { get; set; }
        public EstadoPedidoEnum EstadoPedido { get; set; }
        public bool Activo { get; set; }
    }
}