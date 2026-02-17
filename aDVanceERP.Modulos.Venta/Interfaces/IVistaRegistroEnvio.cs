using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    internal interface IVistaRegistroEnvio : IVistaRegistro {
        string NumeroFacturaVenta { get; set; }
        (Persona persona, Cliente cliente, TelefonoContacto telefono) Cliente { get; }
        (Persona persona, Mensajero mensajero, TelefonoContacto telefono) Mensajero { get; }
        TipoEnvioEnum TipoEnvio { get; set; }
        DateTime FechaAsignacion { get; set; }
        string? ObservacionesEntrega { get; set; }
        decimal MontoCobradoAlCliente { get; set; }

        void CargarFacturasVentasPendientes(string[] numerosFacturasPendientes);
        void CargarTiposEnvio(string[] tiposEnvio);
        void CargarNombresClientes(string[] nombresClientes);
        void CargarNombresMensajeros(string[] nombresMensajeros);
    }
}
