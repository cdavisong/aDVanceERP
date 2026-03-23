using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Caja;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Presentadores {
    internal class PresentadorCierreTurno : PresentadorVistaRegistro<IVistaCierreTurno, CajaTurno, RepoCajaTurno, FiltroBusquedaCajaTurno> {
        private CajaTurno _turno = null!;
        private TotalesCierreCaja? _totalesCalculados = null!;

        public PresentadorCierreTurno(IVistaCierreTurno vista) : base(vista) {
            vista.ArqueoModificado += OnArqueoModificado;

            AgregadorEventos.Suscribir("MostrarVistaCierreTurno", OnMostrarVistaCierreTurno);
        }

        private void OnMostrarVistaCierreTurno(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            _turno = AgregadorEventos.DeserializarPayload<CajaTurno>(obj);
            _totalesCalculados = RepoCajaMovimiento.Instancia.ObtenerTotalesPorCanal(_turno.Id);

            Vista.CodigoTurno = _turno.Codigo;
            Vista.NombreAlmacen = _turno.NombreAlmacen ?? string.Empty;
            Vista.FechaApertura = _turno.FechaApertura;
            Vista.MontoApertura = _turno.MontoApertura;
            Vista.TotalEfectivoCalculado = _totalesCalculados.TotalEfectivo;
            Vista.TotalTransferenciasCalculado = _totalesCalculados.TotalTransferencias;
            Vista.MontoEfectivoDeclarado = 0m;
            Vista.MontoTransferenciasDeclarado = 0m;
            Vista.DiferenciaEfectivo = 0m - _totalesCalculados.TotalEfectivo;
            Vista.DiferenciaTransferencias = 0m - _totalesCalculados.TotalTransferencias;

            Vista.ActualizarTotalArqueo(0m);
            Vista.Mostrar();
        }

        protected override CajaTurno? ObtenerEntidadDesdeVista() {
            return null;
        }

        protected override bool EntidadCorrecta() {
            if (_totalesCalculados == null) {
                CentroNotificaciones.MostrarNotificacion(
                    "Error interno: no se pudieron obtener los totales del turno.",
                    TipoNotificacionEnum.Error);
                return false;
            }

            return true;
        }

        protected override void RegistroEdicionAuxiliar(RepoCajaTurno repositorio, long id) {
            var idUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0;
            var arqueo = Vista.ObtenerArqueo().ToList();

            // Guardar arqueo de denominaciones (transacción interna en el repo)
            RepoCajaArqueo.Instancia.GuardarArqueoCompleto(_turno.Id, arqueo);

            // Registrar AjusteArqueo si hay diferencia en efectivo
            var difEfectivo = Vista.MontoEfectivoDeclarado - _totalesCalculados?.TotalEfectivo ?? 0m;

            if (difEfectivo != 0m) {
                RepoCajaMovimiento.Instancia.Adicionar(new CajaMovimiento {
                    IdTurno = _turno.Id,
                    Tipo = TipoMovimientoCajaEnum.AjusteArqueo,
                    CanalPago = CanalPagoCajaEnum.Efectivo,
                    IdVenta = null,
                    Monto = difEfectivo,
                    Descripcion = difEfectivo > 0
                                        ? "Sobrante de efectivo en arqueo de cierre"
                                        : "Faltante de efectivo en arqueo de cierre",
                    IdCuentaUsuario = idUsuario,
                    FechaMovimiento = DateTime.Now
                });
            }

            // Cerrar el turno - el repo escribe los 4 montos y cambia el estado
            var cerrado = RepoCajaTurno.Instancia.CerrarTurno(
                idTurno: _turno.Id,
                idCuentaCierre: idUsuario,
                montoEfectivoCalculado: _totalesCalculados!.TotalEfectivo,
                montoEfectivoDeclarado: Vista.MontoEfectivoDeclarado,
                montoTransferenciasCalculado: _totalesCalculados.TotalTransferencias,
                montoTransferenciasDeclarado: Vista.MontoTransferenciasDeclarado,
                observacionesCierre: Vista.Observaciones);

            if (cerrado) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Turno {_turno.Codigo} cerrado correctamente.",
                    TipoNotificacionEnum.Info);

                AgregadorEventos.Publicar("TurnoCajaCerrado", _turno.Id.ToString());
                Vista.Cerrar();
            } else {
                CentroNotificaciones.MostrarNotificacion(
                    "No fue posible cerrar el turno. Es posible que ya haya sido cerrado en otra sesión.",
                    TipoNotificacionEnum.Error);
            }
        }

        private void OnArqueoModificado(object? sender, EventArgs e) {
            var arqueo = Vista.ObtenerArqueo().ToList();
            var totalContado = arqueo.Sum(a => a.Subtotal);

            Vista.ActualizarTotalArqueo(totalContado);

            // Actualizar el declarado de efectivo con el total del arqueo
            Vista.MontoEfectivoDeclarado = totalContado;

            RecalcularDiferencias();
        }

        private void RecalcularDiferencias() {
            if (_totalesCalculados == null) 
                return;

            Vista.DiferenciaEfectivo = Vista.MontoEfectivoDeclarado - _totalesCalculados.TotalEfectivo;
            Vista.DiferenciaTransferencias = Vista.MontoTransferenciasDeclarado - _totalesCalculados.TotalTransferencias;
        }
    }
}
