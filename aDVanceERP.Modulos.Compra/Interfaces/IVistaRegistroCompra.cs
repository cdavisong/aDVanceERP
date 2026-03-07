using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Compra.Vistas;

namespace aDVanceERP.Modulos.Compra.Interfaces {
    internal interface IVistaRegistroCompra : IVistaRegistro {
        DateTime FechaCompra { get; set; }
        string CodigoSolicitud { get; set; }
        string NombreProveedor { get; set; }
        string NombreAlmacenDestino { get; set; }
        string ObservacionesCompra { get; set; }
        decimal TotalBruto { get; }
        decimal ImpuestoTotal { get; }
        decimal MontoPagado { get; }
        decimal ImporteTotal { get; }
        Dictionary<Pago, DetallePagoTransferencia> Pagos { get; }
        Dictionary<long, VistaTuplaCarrito> Carrito { get; }

        event EventHandler? RegistrarNuevoProducto;

        void CargarCodigosSolicitudesCompra(object[] codigosSolicitudes);
        void CargarNombresProveedores(string[] nombresProveedores);
        void CargarNombresAlmacenes(object[] nombresAlmacenes);
        void CargarNombresProductos(string[] nombresProductos);
    }
}
