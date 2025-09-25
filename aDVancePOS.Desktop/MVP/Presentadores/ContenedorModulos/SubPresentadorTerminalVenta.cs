using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.Inventario.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.Inventario.MVP.Modelos;

using aDVancePOS.Modulos.TerminalVenta.MVP.Presentadores;
using aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta;

using System.Globalization;

namespace aDVancePOS.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorContenedorModulos {
    private PresentadorTerminalVenta? _terminalVenta;
    private long _proximoIdVenta = 0;

    private List<string[]>? ProductosVenta { get; set; } = new();

    private async void InicializarVistaTerminalVenta() {
        _terminalVenta = new PresentadorTerminalVenta(new VistaTerminalVenta());
        _terminalVenta.Vista.CargarNombresAlmacenes(UtilesAlmacen.ObtenerNombresAlmacenes(true));
        _terminalVenta.Vista.IdTipoEntrega = await UtilesEntrega.ObtenerIdTipoEntrega("Presencial");
        _terminalVenta.Vista.ModificarCantidadProductos += MostrarVistaModificadorCantidadProducto;
        _terminalVenta.Vista.RegistrarDatos += delegate {
            ProductosVenta = _terminalVenta.Vista.Productos;

            RegistrarDetallesVentaProducto();
            RegistrarTransferenciaVenta();
        };
        //_terminalVenta.Vista.CancelarVenta += delegate {
        //    //TODO: Verificar cancelación de la venta
        //    if (!_terminalVenta.Vista.ModoEdicionDatos && !UtilesVenta.ExisteVenta(_proximoIdVenta))
        //        CancelarVenta();
        //};
        
        Vista.Vistas?.Registrar("terminalVenta", _terminalVenta.Vista);
    }

    private void CancelarVenta() {
        // Eliminar pagos registrados si se cancela la venta
        var pagosVenta = UtilesVenta.ObtenerPagosPorVenta(_proximoIdVenta);

        if (pagosVenta.Count > 0) {
            using (var datosPago = new DatosPago())
                foreach (var pago in pagosVenta) {
                    var pagoSplit = pago.Split("|");

                    datosPago.Eliminar(long.Parse(pagoSplit[0]));
                }
        }

        // Eliminar seguimientos de entrega registrados si se cancela la venta
        var idSeguimientoEntrega = UtilesEntrega.ObtenerIdSeguimientoEntrega(_proximoIdVenta).Result;

        if (idSeguimientoEntrega != 0)
            using (var datosSeguimientoEntrega = new DatosSeguimientoEntrega())
                datosSeguimientoEntrega.Eliminar(idSeguimientoEntrega);
    }

    private void MostrarVistaTerminalVenta(object? sender, EventArgs e) { 
        if (_terminalVenta?.Vista == null)
            return;

        _proximoIdVenta = UtilesBD.ObtenerUltimoIdTabla("venta") + 1;
        _terminalVenta.Vista.EfectuarPago += delegate {
            MostrarVistaRegistroPago(sender, e);
        };
        _terminalVenta.Vista.AsignarMensajeria += delegate {
            MostrarVistaRegistroMensajeria(sender, e);
        };

        _terminalVenta.Vista.Restaurar();
        _terminalVenta.Vista.Mostrar();
    }

    private void RegistrarDetallesVentaProducto() {
        if (ProductosVenta == null || ProductosVenta.Count == 0)
            return;

        var ultimoIdVenta = UtilesBD.ObtenerUltimoIdTabla("venta");

        foreach (var producto in ProductosVenta) {
            var detalleVentaProducto = new DetalleVentaProducto(
                0,
                ultimoIdVenta,
                long.Parse(producto[0]),
                decimal.TryParse(producto[2], NumberStyles.Any, CultureInfo.InvariantCulture,
                    out var precioCompraVigente)
                    ? precioCompraVigente
                    : 0.00m,
                decimal.TryParse(producto[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var precioVentaFinal)
                    ? precioVentaFinal
                    : 0.00m,
                decimal.TryParse(producto[4], NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad)
                    ? cantidad
                    : 0.00m
            );

            using (var datosProducto = new DatosDetalleVentaProducto()) {
                datosProducto.Adicionar(detalleVentaProducto);
            }

            RegistrarMovimientoVentaProducto(detalleVentaProducto, producto);
            ModificarStockVentaProducto(detalleVentaProducto, producto);

            // Actualizar precio de venta en tabla producto
            UtilesProducto.ActualizarPrecioVentaBase(
                detalleVentaProducto.IdProducto,
                detalleVentaProducto.PrecioVentaFinal
            );
        }
    }

    private static void RegistrarMovimientoVentaProducto(DetalleVentaProducto detalleVentaProducto,
        IReadOnlyList<string> producto) {
        using (var datosMovimiento = new DatosMovimiento()) {
            datosMovimiento.Adicionar(new Movimiento(
                0,
                detalleVentaProducto.IdProducto,
                long.Parse(producto[5]),
                0,
                DateTime.Now,
                detalleVentaProducto.Cantidad,
                UtilesMovimiento.ObtenerIdTipoMovimiento("Venta")
            ));
        }
    }

    private static void ModificarStockVentaProducto(DetalleVentaProducto detalleVentaProducto,
        IReadOnlyList<string> producto) {
        UtilesMovimiento.ModificarStockProductoAlmacen(
            detalleVentaProducto.IdProducto,
            long.Parse(producto[5]),
            0,
            detalleVentaProducto.Cantidad
        );
    }
}
