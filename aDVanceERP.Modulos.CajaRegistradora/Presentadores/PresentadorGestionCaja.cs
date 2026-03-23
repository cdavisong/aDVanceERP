using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Caja;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;
using aDVanceERP.Modulos.CajaRegistradora.Vistas;

namespace aDVanceERP.Modulos.CajaRegistradora.Presentadores {
    internal class PresentadorGestionCaja : PresentadorVistaGestion<PresentadorTuplaTurno, IVistaGestionCaja, IVistaTuplaTurno, CajaTurno, RepoCajaTurno, FiltroBusquedaCajaTurno> {
        public PresentadorGestionCaja(IVistaGestionCaja vista) : base(vista) {
            vista.AbrirTurno += OnAbrirTurno;
            vista.CerrarTurno += OnCerrarTurno;
            vista.RegistrarMovimiento += OnRegistrarMovimiento;
            
            AgregadorEventos.Suscribir("MostrarVistaGestionCaja", OnMostrarVistaGestionCaja);
        }

        private void OnAbrirTurno(object? sender, EventArgs e) {
            var idAlmacen = Vista.IdAlmacenSeleccionado;

            if (idAlmacen <= 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "Seleccione un almacén antes de abrir un turno de caja.",
                    TipoNotificacionEnum.Advertencia);
                return;
            }

            // Regla: solo 1 turno activo por almacén
            if (RepoCajaTurno.Instancia.ExisteTurnoAbierto(idAlmacen)) {
                CentroNotificaciones.MostrarNotificacion(
                    "Ya existe un turno abierto para este almacén. Ciérrelo antes de abrir uno nuevo.",
                    TipoNotificacionEnum.Advertencia);
                return;
            }

            var almacen = RepoAlmacen.Instancia.ObtenerPorId(idAlmacen);
            
            if (almacen == null) 
                return;

            AgregadorEventos.Publicar("MostrarVistaAperturaTurno", AgregadorEventos.SerializarPayload(almacen));
        }

        private void OnCerrarTurno(object? sender, EventArgs e) {
            var codigo = sender as string;
            
            if (string.IsNullOrEmpty(codigo) || !codigo.StartsWith("TRN-")) 
                return;

            var turno = RepoCajaTurno.Instancia.Buscar(FiltroBusquedaCajaTurno.Codigo, codigo).resultadosBusqueda.Select(r => r.entidadBase).FirstOrDefault();

            if (turno == null || turno.Estado != EstadoCajaTurnoEnum.Abierto) {
                CentroNotificaciones.MostrarNotificacion(
                    "El turno ya no está abierto.",
                    TipoNotificacionEnum.Advertencia);
                return;
            }

            var almacen = RepoAlmacen.Instancia.ObtenerPorId(turno.IdAlmacen);

            AgregadorEventos.Publicar("MostrarVistaCierreTurno", AgregadorEventos.SerializarPayload(turno));
        }

        private void OnRegistrarMovimiento(object? sender, EventArgs e) {
            var codigo = sender as string;

            if (string.IsNullOrEmpty(codigo) || !codigo.StartsWith("TRN-"))
                return;

            var turno = RepoCajaTurno.Instancia.Buscar(FiltroBusquedaCajaTurno.Codigo, codigo).resultadosBusqueda.Select(r => r.entidadBase).FirstOrDefault();

            if (turno == null || turno.Estado != EstadoCajaTurnoEnum.Abierto) {
                CentroNotificaciones.MostrarNotificacion(
                    "No se pueden registrar movimientos en un turno cerrado o anulado.",
                    TipoNotificacionEnum.Advertencia);
                return;
            }
                
            AgregadorEventos.Publicar("MostrarVistaMovimientoCaja", AgregadorEventos.SerializarPayload(turno));
        }

        private void OnMostrarVistaGestionCaja(string obj) {
            Vista.CargarFiltroAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(resultado => resultado.entidadBase)]);
            Vista.CargarFiltrosBusqueda(UtilesBusquedaCajaTurno.Filtros);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaTurno ObtenerValoresTupla(CajaTurno entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaTurno(new VistaTuplaTurno(), entidad);

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.Codigo = entidad.Codigo;
            presentadorTupla.Vista.NombreAlmacen = entidad.NombreAlmacen ?? "-";
            presentadorTupla.Vista.NombreUsuarioApertura = entidad.NombreUsuarioApertura ?? "-";
            presentadorTupla.Vista.FechaApertura = entidad.FechaApertura;
            presentadorTupla.Vista.FechaCierre = entidad.FechaCierre;
            presentadorTupla.Vista.MontoApertura = entidad.MontoApertura;
            presentadorTupla.Vista.MontoEfectivoCalculado = entidad.MontoEfectivoCalculado;
            presentadorTupla.Vista.MontoEfectivoDeclarado = entidad.MontoEfectivoDeclarado;
            presentadorTupla.Vista.DiferenciaEfectivo = entidad.DiferenciaEfectivo;
            presentadorTupla.Vista.MontoTransferenciasCalculado = entidad.MontoTransferenciasCalculado;
            presentadorTupla.Vista.MontoTransferenciasDeclarado = entidad.MontoTransferenciasDeclarado;
            presentadorTupla.Vista.DiferenciaTransferencias = entidad.DiferenciaTransferencias;
            presentadorTupla.Vista.Estado = entidad.Estado;

            presentadorTupla.Vista.VerDetalleTurno += OnVerDetalleTurnoDesde;
            presentadorTupla.Vista.AnularTurno += OnAnularTurnoDesde;

            return presentadorTupla;
        }

        public override void ActualizarResultadosBusqueda() {
            // Filtro por defecto: turno del día de hoy
            if (FiltroBusqueda == FiltroBusquedaCajaTurno.Todos &&
               (CriteriosBusqueda == null || CriteriosBusqueda.Length == 0)) {
                CriteriosBusqueda = [
                    "0",
                    DateTime.Today.ToString("yyyy-MM-dd 00:00:00"),
                    DateTime.Today.ToString("yyyy-MM-dd 23:59:59"),
                    string.Empty
                ];
            }

            base.ActualizarResultadosBusqueda();
            Vista.RefrescarEstadoTurnoActivo();
        }

        private void OnVerDetalleTurnoDesde(object? sender, long idTurno) {
            MostrarDetalleTurno(idTurno);
        }

        private void MostrarDetalleTurno(long idTurno) {
            var turno = RepoCajaTurno.Instancia.ObtenerPorId(idTurno);

            AgregadorEventos.Publicar("MostrarVistaDetalleTurno", AgregadorEventos.SerializarPayload(turno));
        }

        private void OnAnularTurnoDesde(object? sender, long idTurno) {
            EjecutarAnulacion(idTurno);
        }

        private void EjecutarAnulacion(long idTurno) {
            // Guardia: no anular si tiene movimientos
            if (RepoCajaMovimiento.Instancia.TurnoTieneMovimientos(idTurno)) {
                CentroNotificaciones.MostrarNotificacion(
                    "No es posible anular un turno que ya tiene movimientos registrados.",
                    TipoNotificacionEnum.Advertencia);
                return;
            }

            var anulado = RepoCajaTurno.Instancia.AnularTurno(idTurno, "Anulado manualmente por el operador.");

            if (anulado) {
                CentroNotificaciones.MostrarNotificacion(
                    "El turno fue anulado correctamente.",
                    TipoNotificacionEnum.Ok);

                ActualizarResultadosBusqueda();
            } else {
                CentroNotificaciones.MostrarNotificacion(
                    "No fue posible anular el turno. Verifique que siga abierto.",
                    TipoNotificacionEnum.Error);
            }
        }
    }
}
