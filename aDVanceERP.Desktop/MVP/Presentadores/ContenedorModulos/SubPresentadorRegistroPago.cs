using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Pago;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos.Repositorios;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
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
        _registroPago.EntidadRegistradaActualizada += delegate (object? sender, EventArgs args) {
            if (_registroVentaProducto == null)
                return;

            _registroVentaProducto.Vista.PagoEfectuado = true;

            ActualizarSeguimientoEntrega();
            ActualizarMovimientoCaja(sender as List<Pago?>);
        };
    }

    private void MostrarVistaRegistroPago(object? sender, EventArgs e) {
        InicializarVistaRegistroPago();

        if (_registroPago != null && _registroVentaProducto != null) {
            _registroPago.Vista.IdVenta = _proximoIdVenta;
            _registroPago.Vista.Total = _registroVentaProducto.Vista.Total;
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
            if (_registroPago != null && _registroVentaProducto != null) {
                _registroPago.PopularVistaDesdeEntidad(new Pago(0, venta.Id, string.Empty, venta.Total));
                _registroPago.Vista.EfectuarTransferencia += delegate {
                    MostrarVistaEdicionDetallePagoTransferencia(sender, e);
                };
                _registroPago.Vista.Mostrar();
            }
        }

        _registroPago?.Dispose();
    }

    private void ActualizarSeguimientoEntrega(long idVenta = 0) {
        using (var datosSeguimiento = new RepoSeguimientoEntrega()) {
            var objetoSeguimiento = datosSeguimiento
                .Buscar(FiltroBusquedaSeguimientoEntrega.IdVenta, (idVenta != 0 ? idVenta : _registroPago?.Vista.IdVenta).ToString())
                .resultados
                .FirstOrDefault();

            if (objetoSeguimiento == null)
                return;

            objetoSeguimiento.FechaPago = DateTime.Now;
            datosSeguimiento.Editar(objetoSeguimiento);
        }
    }

    private void ActualizarMovimientoCaja(List<Pago?> pagos) {
        using (var datos = new RepoMovimientoCaja()) {
            foreach (var pago in pagos) {
                if (pago?.MetodoPago != "Efectivo")
                    continue;

                var movimientoCaja = datos
                            .Buscar(FiltroBusquedaMovimientoCaja.IdPago, pago.Id.ToString())
                            .resultados
                            .FirstOrDefault();

                if (movimientoCaja == null) {
                    movimientoCaja = new MovimientoCaja(0,
                        UtilesCaja.ObtenerIdCajaActiva(),
                        DateTime.Now,
                        pago.Monto,
                        TipoMovimientoCaja.Ingreso,
                        $"Venta",
                        pago.Id,
                        UtilesCuentaUsuario.UsuarioAutenticado?.Id ?? 0,
                        $"Pago de venta #{(pago.IdVenta > 0 ? pago.IdVenta : _registroPago?.Vista.IdVenta)} realizado por {UtilesCuentaUsuario.UsuarioAutenticado?.Nombre}"
                    );

                    datos.Adicionar(movimientoCaja);
                } else {
                    movimientoCaja.Monto = pago.Monto;

                    datos.Editar(movimientoCaja);
                }
            }

            // Actualizar el monto actual de la caja activa
            ActualizarMontoCaja(UtilesCaja.ObtenerIdCajaActiva(), datos);
        }
    }
}