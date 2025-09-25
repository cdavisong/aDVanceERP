using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.Finanzas.MVP.Presentadores;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos {
    public partial class PresentadorModulos {
        private PresentadorRegistroAperturaCaja? _registroAperturaCaja;

        private void InicializarVistaRegistroAperturaCaja() {
            _registroAperturaCaja = new PresentadorRegistroAperturaCaja(new VistaRegistroAperturaCaja());
            _registroAperturaCaja.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
            _registroAperturaCaja.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
            _registroAperturaCaja.EntidadRegistradaActualizada += delegate {
                if (_gestionCajas == null)
                    return;

                _gestionCajas.ActualizarResultadosBusqueda();
            };
        }

        private void MostrarVistaRegistroAperturaCaja(object? sender, EventArgs e) {
            if (UtilesCaja.ExisteCajaActiva()) {
                CentroNotificaciones.Mostrar("Sólo puede existir una caja abierta por sesión. Para realizar nuevas aperturas de caja realice el cierre correspondiente a la caja activa actualmente", TipoNotificacion.Advertencia);

                return;
            }

            InicializarVistaRegistroAperturaCaja();

            if (_registroAperturaCaja == null)
                return;

            _registroAperturaCaja.Vista.Restaurar();
            _registroAperturaCaja.Vista.Mostrar();
            _registroAperturaCaja.Dispose();
        }

        private void MostrarVistaEdicionAperturaCaja(object? sender, EventArgs e) {
            InicializarVistaRegistroAperturaCaja();

            if (sender is Caja caja) {
                if (_registroAperturaCaja != null) {
                    _registroAperturaCaja.PopularVistaDesdeEntidad(caja);
                    _registroAperturaCaja.Vista.Mostrar();
                }
            }

            _registroAperturaCaja?.Dispose();
        }

        private void ActualizarMontoCaja(long idCaja, RepoMovimientoCaja? datosMovimientoCaja = null) {
            if (datosMovimientoCaja == null) {
                using (var datos = new RepoMovimientoCaja())
                    ActualizarMontoCaja(idCaja, datos);

                return;
            }

            var movimientosCaja = datosMovimientoCaja.Buscar(FiltroBusquedaMovimientoCaja.IdCaja, idCaja.ToString()).resultados;
            decimal saldoActual = 0;

            foreach (var movimiento in movimientosCaja) {
                if (movimiento.Tipo == TipoMovimientoCaja.Ingreso)
                    saldoActual += movimiento.Monto;
                else if (movimiento.Tipo == TipoMovimientoCaja.Egreso)
                    saldoActual -= movimiento.Monto;                
            }

            UtilesCaja.ActualizarMontoCaja(idCaja, saldoActual);
        }
    }
}
