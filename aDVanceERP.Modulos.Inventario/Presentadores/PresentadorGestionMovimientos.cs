using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorGestionMovimientos : PresentadorVistaGestion<PresentadorTuplaMovimiento, IVistaGestionMovimientos, IVistaTuplaMovimiento, Movimiento, RepoMovimiento, FiltroBusquedaMovimiento> {
        public PresentadorGestionMovimientos(IVistaGestionMovimientos vista) : base(vista) {
            vista.RegistrarEntidad += OnRegistrarMovimiento;

            AgregadorEventos.Suscribir<EventoMostrarVistaGestionMovimientos>(OnMostrarVistaGestionMovimientos);
        }

        private void OnMostrarVistaGestionMovimientos(EventoMostrarVistaGestionMovimientos e) {
            CargarDatosComunes();
            
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void CargarDatosComunes() {
            Vista.CargarFiltrosBusqueda([.. EnumExt.ObtenerNombresDescripciones<FiltroBusquedaMovimiento>()]);
        }

        public override void ActualizarResultadosBusqueda() {
            if (FiltroBusqueda == FiltroBusquedaMovimiento.Todos && (CriteriosBusqueda == null || CriteriosBusqueda.Length == 0))
                CriteriosBusqueda = [DateTime.Today.ToString("yyyy-MM-dd 00:00:00"), DateTime.Today.ToString("yyyy-MM-dd 23:59:59"), string.Empty];

            // Actualizar totales
            var fechaDesde = DateTime.ParseExact(CriteriosBusqueda[0], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            var fechaHasta = DateTime.ParseExact(CriteriosBusqueda[1], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            var resumen = RepoMovimiento.Instancia.ObtenerTotalesPorRangoFechas(fechaDesde, fechaHasta);

            Vista.TotalEntradas = resumen.TotalEntradas;
            Vista.TotalSalidas = resumen.TotalSalidas;
            Vista.Balance = resumen.Balance;

            base.ActualizarResultadosBusqueda();
        }

        private void OnRegistrarMovimiento(object? sender, EventArgs e) {
            if (RepoProducto.Instancia.Cantidad() == 0) {
                CentroNotificaciones.MostrarNotificacion("No es posible registrar un nuevo movimiento porque no hay productos registrados en el sistema. Por favor, registre al menos un producto antes de continuar.", TipoNotificacionEnum.Advertencia);
                return;
            }

            AgregadorEventos.Publicar(new EventoMostrarVistaRegistroMovimiento());
        }

        protected override PresentadorTuplaMovimiento ObtenerValoresTupla(Movimiento entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaMovimiento(new VistaTuplaMovimiento(), entidad);

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.NombreProducto = entidad.NombreProducto;
            presentadorTupla.Vista.NombreAlmacenOrigen = entidad.NombreAlmacenOrigen;
            presentadorTupla.Vista.ActualizarIconoStock(entidad.EfectoMovimiento);
            presentadorTupla.Vista.NombreAlmacenDestino = entidad.NombreAlmacenDestino;
            presentadorTupla.Vista.SaldoInicial = entidad.SaldoInicial.ToString("N2", CultureInfo.InvariantCulture);
            presentadorTupla.Vista.CantidadMovida = entidad.CantidadMovida.ToString("N2", CultureInfo.InvariantCulture);
            presentadorTupla.Vista.SaldoFinal = entidad.SaldoFinal.ToString("N2", CultureInfo.InvariantCulture);
            presentadorTupla.Vista.TipoMovimiento = entidad.NombreTipoMovimiento;
            presentadorTupla.Vista.EstadoMovimiento = entidad.Estado;
            presentadorTupla.Vista.Fecha = entidad.FechaCreacion.ToString("yyyy-MM-dd");

            return presentadorTupla;
        }

        public override void Dispose() {
            Vista.RegistrarEntidad -= OnRegistrarMovimiento;

            AgregadorEventos.Desuscribir<EventoMostrarVistaGestionMovimientos>(OnMostrarVistaGestionMovimientos);

            base.Dispose();
        }
    }
}