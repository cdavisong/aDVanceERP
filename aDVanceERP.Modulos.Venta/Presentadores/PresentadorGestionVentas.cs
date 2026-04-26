using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;
using aDVanceERP.Modulos.Venta.Vistas;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorGestionVentas : PresentadorVistaGestion<PresentadorTuplaVenta, IVistaGestionVentas, IVistaTuplaVenta, Core.Modelos.Modulos.Venta.Venta, RepoVenta, FiltroBusquedaVenta> {
        public PresentadorGestionVentas(IVistaGestionVentas vista) : base(vista) {
            vista.RegistrarEntidad += OnRegistrarVenta;

            AgregadorEventos.Suscribir("MostrarVistaGestionVentas", OnMostrarVistaGestionVentas);
        }

        private void OnMostrarVistaGestionVentas(string obj) {
            CargarDatosComunes();

            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void CargarDatosComunes() {
            Vista.CargarFiltrosBusqueda([.. EnumExt.ObtenerNombresDescripciones<FiltroBusquedaVenta>()]);
        }

        public override void ActualizarResultadosBusqueda() {
            if (FiltroBusqueda == FiltroBusquedaVenta.Todas && (CriteriosBusqueda == null || CriteriosBusqueda.Length == 0))
                CriteriosBusqueda = [DateTime.Today.ToString("yyyy-MM-dd 00:00:00"), DateTime.Today.ToString("yyyy-MM-dd 23:59:59"), string.Empty];

            // Actualizar totales
            var fechaDesde = DateTime.ParseExact(CriteriosBusqueda[0], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            var fechaHasta = DateTime.ParseExact(CriteriosBusqueda[1], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            var totalRecaudado = RepoVenta.Instancia.ObtenerTotalRecaudadoPorRangoFechas(fechaDesde, fechaHasta);

            Vista.TotalRecaudado = totalRecaudado;

            base.ActualizarResultadosBusqueda();
        }

        private void OnRegistrarVenta(object? sender, EventArgs e) {
            if (RepoProducto.Instancia.Cantidad() == 0) {
                CentroNotificaciones.MostrarNotificacion("No es posible registrar una venta manual porque no hay productos registrados en el sistema. Por favor, registre al menos un producto antes de continuar.", TipoNotificacionEnum.Advertencia);
                return;
            }

            AgregadorEventos.Publicar("MostrarVistaRegistroVenta", string.Empty);
        }

        protected override PresentadorTuplaVenta ObtenerValoresTupla(Core.Modelos.Modulos.Venta.Venta entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaVenta(new VistaTuplaVenta(), entidad);
            var cliente = RepoCliente.Instancia.ObtenerPorId(entidad.IdCliente);
            var persona = RepoPersona.Instancia.Buscar(FiltroBusquedaPersona.Id, cliente?.IdPersona.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.NumeroFacturaVenta = entidad.NumeroFacturaTicket ?? "-";
            presentadorTupla.Vista.FechaVenta = entidad.FechaVenta;
            presentadorTupla.Vista.NombreCliente = string.IsNullOrEmpty(persona?.NombreCompleto) 
                ? "Anónimo" 
                : persona.NombreCompleto;
            presentadorTupla.Vista.CanalPagoPrincipal = string.IsNullOrEmpty(entidad.CanalPagoPrincipal) || entidad.CanalPagoPrincipal.Equals("N/A")
                ? CanalPagoEnum.NA
                : Enum.Parse<CanalPagoEnum>(entidad.CanalPagoPrincipal);
            presentadorTupla.Vista.TotalBruto = entidad.TotalBruto;
            presentadorTupla.Vista.DescuentoTotal = entidad.DescuentoTotal;
            presentadorTupla.Vista.ImpuestoTotal = entidad.ImpuestoTotal;
            presentadorTupla.Vista.ImporteTotal = entidad.ImporteTotal;
            presentadorTupla.Vista.Activo = entidad.Activo;
            presentadorTupla.Vista.EstadoVenta = entidad.EstadoVenta;

            return presentadorTupla;
        }
    }
}