using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Venta.Vistas;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    public interface IVistaRegistroVenta : IVistaRegistro {
        DateTime FechaVenta {  get; set; }
        string NumeroPedido { get; set; }
        string NombreCliente { get; set; }
        string NombreAlmacenOrigen { get; set; }
        string ObservacionesVenta { get; set; }
        Dictionary<long, VistaTuplaCarrito> Carrito { get; }
        decimal TotalBruto { get; }
        decimal DescuentoTotal {  get; }
        decimal ImpuestoTotal { get; }
        decimal ImporteTotal { get; }
        Dictionary<Pago, DetallePagoTransferencia> Pagos { get; }

        void CargarNumerosPedidos(object[] numerosPedidos);
        void CargarNombresClientes(string[] nombresClientes);
        void CargarNombresAlmacenes(object[] nombresAlmacenes);
        void CargarNombresProductos(string[] nombresProductos);
    }
}