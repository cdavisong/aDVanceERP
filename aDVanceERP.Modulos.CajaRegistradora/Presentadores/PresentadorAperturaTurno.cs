using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Caja;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Presentadores {
    internal class PresentadorAperturaTurno : PresentadorVistaRegistro<IVistaAperturaTurno, CajaTurno, RepoCajaTurno, FiltroBusquedaCajaTurno> {
        public PresentadorAperturaTurno(IVistaAperturaTurno vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaAperturaTurno", OnMostrarVistaAperturaTurno);
        }

        private void OnMostrarVistaAperturaTurno(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            var almacen = AgregadorEventos.DeserializarPayload<Almacen>(obj);
            
            Vista.IdAlmacen = almacen.Id;
            Vista.NombreAlmacen = almacen.ToString();

            Vista.Mostrar();
        }

        protected override CajaTurno? ObtenerEntidadDesdeVista() {
            var idUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0;
            var codigo = RepoCajaTurno.Instancia.GenerarCodigoTurno(Vista.IdAlmacen);

            var turno = new CajaTurno {
                Codigo = codigo,
                IdAlmacen = Vista.IdAlmacen,
                IdCuentaApertura = idUsuario,
                FechaApertura = DateTime.Now,
                MontoApertura = Vista.MontoApertura,
                Estado = EstadoCajaTurnoEnum.Abierto,
                ObservacionesApertura = Vista.Observaciones
            };

            return turno;
        }

        protected override bool EntidadCorrecta() {
            if (Vista.MontoApertura < 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "El monto de apertura no puede ser negativo.",
                    TipoNotificacionEnum.Advertencia);
                return false;
            }

            // Segunda barrera: verificar turno activo antes de insertar
            if (RepoCajaTurno.Instancia.ExisteTurnoAbierto(Vista.IdAlmacen)) {
                CentroNotificaciones.MostrarNotificacion(
                    "Ya existe un turno abierto para este almacén.",
                    TipoNotificacionEnum.Advertencia);
                return false;
            }

            return true;
        }
    }
}
