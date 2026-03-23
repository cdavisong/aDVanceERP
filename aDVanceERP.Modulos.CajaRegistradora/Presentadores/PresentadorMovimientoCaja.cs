using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Caja;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Presentadores {
    internal class PresentadorMovimientoCaja : PresentadorVistaRegistro<IVistaMovimientoCaja, CajaMovimiento, RepoCajaMovimiento, FiltroBusquedaCajaMovimiento> {
        private long _idTurno;

        public PresentadorMovimientoCaja(IVistaMovimientoCaja vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaMovimientoCaja", OnMostrarVistaMovimientoCaja);
        }

        private void OnMostrarVistaMovimientoCaja(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            var turno = AgregadorEventos.DeserializarPayload<CajaTurno>(obj);
            var almacen = RepoAlmacen.Instancia.ObtenerPorId(turno.IdAlmacen);

            _idTurno = turno.Id;

            Vista.Codigo = turno.Codigo;
            Vista.IdAlmacen = almacen!.Id;
            Vista.NombreAlmacen = almacen!.Nombre;

            Vista.Mostrar();
        }

        protected override CajaMovimiento? ObtenerEntidadDesdeVista() {
            var idUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0;

            // El monto es negativo para salidas
            var montoFinal = Vista.Tipo == TipoMovimientoCajaEnum.SalidaManual
                ? -Math.Abs(Vista.Monto)
                : Math.Abs(Vista.Monto);

            return new CajaMovimiento {
                IdTurno = _idTurno,
                Tipo = Vista.Tipo,
                CanalPago = Vista.CanalPago,
                IdVenta = null,
                Monto = montoFinal,
                Descripcion = Vista.Descripcion,
                IdCuentaUsuario = idUsuario,
                FechaMovimiento = DateTime.UtcNow
            };
        }

        protected override bool EntidadCorrecta() {
            if (Vista.Monto <= 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "El monto debe ser mayor a cero.",
                    TipoNotificacionEnum.Advertencia);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Vista.Descripcion)) {
                CentroNotificaciones.MostrarNotificacion(
                    "Ingrese una descripción para el movimiento.",
                    TipoNotificacionEnum.Advertencia);
                return false;
            }

            return true;
        }
    }
}
