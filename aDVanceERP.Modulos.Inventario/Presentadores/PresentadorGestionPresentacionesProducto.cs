using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Monedas;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Monedas;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorGestionPresentacionesProducto : PresentadorVistaGestion<PresentadorTuplaPresentacionProducto, IVistaGestionPresentacionProducto, IVistaTuplaVentaPresentacion, PresentacionProducto, RepoPresentacionProducto, FiltroBusquedaPresentacionProducto> {
        private PresentacionProducto? _entidad = null!;
        private Moneda? _monedaBase;
        

        public PresentadorGestionPresentacionesProducto(IVistaGestionPresentacionProducto vista) : base(vista) {
            vista.RegistrarEntidad += OnRegistrarPresentacionProducto;
            vista.EditarEntidad += OnEditarPresentacionProducto;

            AgregadorEventos.Suscribir<EventoMostrarVistaGestionPresentacionProducto>(OnMostrarVistaGestionVistaGestionPresentacionProducto);
            AgregadorEventos.Suscribir<EventoMostrarVistaEdicionPresentacionProducto>(OnMostrarVistaEdicionPresentacionProducto);
        }

        private void OnMostrarVistaGestionVistaGestionPresentacionProducto(EventoMostrarVistaGestionPresentacionProducto e) {
            CargarDatosComunes(e.Producto);

            Vista.ModoEdicion = false;
            Vista.Restaurar();            
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void OnMostrarVistaEdicionPresentacionProducto(EventoMostrarVistaEdicionPresentacionProducto e) {
            Vista.ModoEdicion = true;

            CargarDatosComunes(e.PresentacionProducto);
            PopularVistaDesdeEntidad(e.PresentacionProducto);
        }

        private void CargarDatosComunes(Producto producto = null!) {
            var monedas = RepoMoneda.Instancia.ObtenerActivas();

            _monedaBase = monedas.FirstOrDefault(m => m.EsBase);

            Vista.CargarDatosProducto(producto);
            Vista.CargarUnidadesMedida([.. RepoUnidadMedida.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarMonedas([.. monedas]);
        }

        private void CargarDatosComunes(PresentacionProducto presentacionProducto) {
            _entidad = presentacionProducto;
        }

        protected override PresentadorTuplaPresentacionProducto ObtenerValoresTupla(PresentacionProducto entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaPresentacionProducto(new VistaTuplaPresentacionProducto(), entidad);
            var producto = RepoProducto.Instancia.ObtenerPorId(entidad.IdProducto)!;
            var unidadMedida = RepoUnidadMedida.Instancia.ObtenerPorId(entidad.IdUnidadMedida)!;

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.NombreUM = unidadMedida.Nombre;
            presentadorTupla.Vista.AbreviaturaUM = unidadMedida.Abreviatura;
            presentadorTupla.Vista.Cantidad = entidad.Cantidad;
            presentadorTupla.Vista.PrecioVenta = entidad.PrecioVenta;
            presentadorTupla.Vista.PrecioPorUnidad = entidad.PrecioPorUnidad;
            presentadorTupla.Vista.Descuento =
                producto.PrecioVentaBase > 0
                    ? ((producto.PrecioVentaBase - entidad.PrecioPorUnidad) / producto.PrecioVentaBase) * 100
                    : 0m;
            presentadorTupla.Vista.Estado = entidad.Activo;

            return presentadorTupla;
        }

        public override void ActualizarResultadosBusqueda() {
            FiltroBusqueda = FiltroBusquedaPresentacionProducto.IdProducto;
            CriteriosBusqueda = [Vista.IdProducto.ToString()];

            base.ActualizarResultadosBusqueda();
        }

        private void OnRegistrarPresentacionProducto(object? sender, EventArgs e) {
            _entidad = ObtenerEntidadDesdeVista();

            if (_entidad == null)
                return;

            var entidadConConversion = AplicarConversionMoneda(_entidad);
            var producto = RepoProducto.Instancia.ObtenerPorId(_entidad.IdProducto);

            RepoPresentacionProducto.Instancia.Adicionar(entidadConConversion);

            Vista.Restaurar();
            Vista.CargarDatosProducto(producto);
            Vista.ModoEdicion = false;

            ActualizarResultadosBusqueda();
        }

        private void OnEditarPresentacionProducto(object? sender, EventArgs e) {
            _entidad = ObtenerEntidadDesdeVista();

            if (_entidad == null) 
                return;

            var entidadConConversion = AplicarConversionMoneda(_entidad);
            var producto = RepoProducto.Instancia.ObtenerPorId(_entidad.IdProducto);

            RepoPresentacionProducto.Instancia.Editar(entidadConConversion);

            Vista.Restaurar();
            Vista.CargarDatosProducto(producto);
            Vista.ModoEdicion = false;

            ActualizarResultadosBusqueda();
        }

        public void PopularVistaDesdeEntidad(PresentacionProducto entidad) {
            var producto = RepoProducto.Instancia.ObtenerPorId(entidad.IdProducto);
            var unidadMedida = RepoUnidadMedida.Instancia.ObtenerPorId(entidad.IdUnidadMedida);

            Vista.IdProducto = entidad.IdProducto;
            Vista.UnidadMedida = unidadMedida;
            Vista.Cantidad = entidad.Cantidad;
            Vista.PrecioVenta = entidad.PrecioVenta;
        }

        protected PresentacionProducto? ObtenerEntidadDesdeVista() {
            return new PresentacionProducto {
                Id = _entidad?.Id ?? 0,
                IdProducto = Vista.IdProducto,
                IdUnidadMedida = Vista.UnidadMedida?.Id ?? throw new InvalidOperationException("Debe seleccionar una unidad de medida."),
                Cantidad = Vista.Cantidad,
                PrecioVenta = Vista.PrecioVenta,
                Activo = true
            };
        }

        private PresentacionProducto AplicarConversionMoneda(PresentacionProducto original) {
            var monedaElegida = Vista.MonedaPrecioVenta;

            if (monedaElegida == null || _monedaBase == null
                || monedaElegida.Id == _monedaBase.Id)
                return original;

            var tasa = RepoTasaCambio.Instancia
                .ObtenerTasaVigente(monedaElegida.Id, _monedaBase.Id);

            if (tasa == 1m) {
                CentroNotificaciones.MostrarNotificacion(
                    $"No existe tasa de cambio vigente para {monedaElegida.Codigo} → {_monedaBase.Codigo}. " +
                    "Registre la tasa antes de guardar la presentación.",
                    TipoNotificacionEnum.Advertencia);
                return original;
            }

            return new PresentacionProducto {
                Id = original.Id,
                IdProducto = original.IdProducto,
                IdUnidadMedida = original.IdUnidadMedida,
                Cantidad = original.Cantidad,
                PrecioVenta = Math.Round(original.PrecioVenta * tasa, 2, MidpointRounding.AwayFromZero),
                Activo = original.Activo
            };
        }

        public override void Dispose() {
            Vista.RegistrarEntidad -= OnRegistrarPresentacionProducto;
            Vista.EditarEntidad -= OnEditarPresentacionProducto;

            AgregadorEventos.Desuscribir<EventoMostrarVistaGestionPresentacionProducto>(OnMostrarVistaGestionVistaGestionPresentacionProducto);
            AgregadorEventos.Desuscribir<EventoMostrarVistaEdicionPresentacionProducto>(OnMostrarVistaEdicionPresentacionProducto);

            base.Dispose();
        }
    }
}