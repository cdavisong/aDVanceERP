using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Pago;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos.Repositorios;

using aDVancePOS.Desktop.Utiles;

namespace aDVancePOS.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorContenedorModulos {
    private PresentadorRegistroPago? _registroPago;

    private void InicializarVistaRegistroPago() {
        _registroPago = new PresentadorRegistroPago(new VistaRegistroPago());
        _registroPago.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroPago.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroPago.Vista.PagoEliminado += delegate (object? sender, EventArgs e) {
            if (sender is not string[] metodoPago || !metodoPago[0].Contains("Transferencia"))
                return;

            _registroPago.Vista.CargarMetodosPago();

            Transferencia = Array.Empty<string>();
        };
        _registroPago.DatosRegistradosActualizados += delegate (object? sender, EventArgs args) {
            if (_terminalVenta == null)
                return;

            _terminalVenta.Vista.PagoEfectuado = true;

            ActualizarSeguimientoEntrega();
            RegistrarMovimientoCaja(sender as List<Pago?>);
        };
    }

    private void MostrarVistaRegistroPago(object? sender, EventArgs e) {
        InicializarVistaRegistroPago();

        if (_registroPago != null && _terminalVenta != null) {
            _registroPago.Vista.IdVenta = _proximoIdVenta;
            _registroPago.Vista.Total = _terminalVenta.Vista.Total;
            _registroPago.Vista.EfectuarTransferencia += delegate {
                MostrarVistaRegistroDetallePagoTransferencia(sender, e);
            };

            _registroPago.Vista.Mostrar();
        }

        _registroPago?.Dispose();
    }

    //TODO: Trabajar en la edicion de pagos
    private void MostrarVistaEdicionPago(object? sender, EventArgs e) {
        InicializarVistaRegistroPago();

        if (sender is Venta venta) {
            if (_registroPago != null && _terminalVenta != null) {
                _registroPago.PopularVistaDesdeObjeto(new Pago(0, venta.Id, string.Empty, venta.Total));
                _registroPago.Vista.EfectuarTransferencia += delegate {
                    MostrarVistaEdicionDetallePagoTransferencia(sender, e);
                };
                _registroPago.Vista.Mostrar();
            }
        }

        _registroPago?.Dispose();
    }

    private void ActualizarSeguimientoEntrega() {
        using (var datosSeguimiento = new DatosSeguimientoEntrega()) {
            var objetoSeguimiento = datosSeguimiento
                .Obtener(CriterioBusquedaSeguimientoEntrega.IdVenta, _registroPago?.Vista.IdVenta.ToString())
                .FirstOrDefault();

            if (objetoSeguimiento == null)
                return;

            objetoSeguimiento.FechaPago = DateTime.Now;
            datosSeguimiento.Editar(objetoSeguimiento);
        }
    }

    private void RegistrarMovimientoCaja(List<Pago?> pagos) {
        using (var datos = new DatosMovimientoCaja())
            foreach (var pago in pagos) {
                if (pago?.MetodoPago != "Efectivo")
                    continue;

                var movimientoCaja = new MovimientoCaja(0,
                    UtilesCaja.ObtenerIdCajaActiva(),
                    DateTime.Now,
                    pago.Monto,
                    TipoMovimientoCaja.Ingreso,
                    $"Venta",
                    pago.Id,
                    UtilesCuentaUsuario.UsuarioAutenticado?.Id ?? 0,
                    $"Pago de venta #{_registroPago?.Vista.IdVenta} realizado por {UtilesCuentaUsuario.UsuarioAutenticado?.Nombre}"
                );

                datos.Adicionar(movimientoCaja);
            }
    }
}