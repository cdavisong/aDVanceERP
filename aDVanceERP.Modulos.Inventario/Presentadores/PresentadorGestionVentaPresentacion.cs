using aDVanceERP.Core.Eventos;
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
    internal class PresentadorGestionVentaPresentacion
        : PresentadorVistaGestion<PresentadorTuplaVentaPresentacion, IVistaGestionVentaPresentacion,
                                  IVistaTuplaVentaPresentacion, PrecioPresentacion,
                                  RepoPrecioPresentacion, FiltroBusquedaPrecioPresentacion> {

        // ── Estado ────────────────────────────────────────────────────────────

        /// <summary>Moneda base del sistema. Se resuelve al abrir la vista.</summary>
        private Moneda? _monedaBase;

        public PresentadorGestionVentaPresentacion(IVistaGestionVentaPresentacion vista) : base(vista) {
            RegistrarEntidad += OnRegistrarVentaPresentacion;
            EditarEntidad += OnEditarVentaPresentacion;

            AgregadorEventos.Suscribir("MostrarVistaGestionPrecioPresentacion",
                OnMostrarVistaGestionVentaPresentacion);
        }

        // ── Handlers ─────────────────────────────────────────────────────────

        private void OnRegistrarVentaPresentacion(object? sender, EventArgs e) {
            if (sender is not PrecioPresentacion precioPresentacion)
                return;

            var precioConvertido = AplicarConversionMoneda(precioPresentacion);

            if (precioConvertido.PrecioVenta <= 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "El precio de venta de la presentación debe ser mayor que cero.",
                    TipoNotificacionEnum.Advertencia);
                return;
            }

            RepoPrecioPresentacion.Instancia.Adicionar(precioConvertido);
            ActualizarResultadosBusqueda();
        }

        private void OnEditarVentaPresentacion(object? sender, PrecioPresentacion e) {
            if (sender is not PrecioPresentacion)
                return;

            ActualizarResultadosBusqueda();
        }

        private void OnMostrarVistaGestionVentaPresentacion(string obj) {
            Vista.Restaurar();

            // Datos del producto
            Vista.CargarDatosProducto(AgregadorEventos.DeserializarPayload<Producto>(obj));
            Vista.CargarUnidadesMedida([.. RepoUnidadMedida.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);

            // ── Monedas ──────────────────────────────────────────────────────
            var monedas = RepoMoneda.Instancia.ObtenerActivas().ToArray();
            Vista.CargarMonedas(monedas);

            _monedaBase = monedas.FirstOrDefault(m => m.EsBase);

            if (_monedaBase != null)
                Vista.ActualizarSimboloMoneda(_monedaBase.Simbolo);

            Vista.Mostrar();
            ActualizarResultadosBusqueda();
        }

        // ── RegistrarEntidad desde vista: construir PrecioPresentacion ────────
        //
        // La vista ya envuelve la creación del objeto en su Inicializar():
        //   RegistrarEntidad?.Invoke(new PrecioPresentacion { … PrecioVenta = PrecioVenta … }, e)
        //
        // Aquí interceptamos OnRegistrarVentaPresentacion para convertir el precio
        // a moneda base ANTES de persistir, si la moneda elegida ≠ base.
        //
        // ⚠️  Para acceder a la moneda elegida necesitamos un override del flujo.
        //     El patrón más limpio es sobrescribir el evento en la vista y que
        //     el presentador construya el objeto final.  Como la vista actual
        //     ya construye el PrecioPresentacion internamente, aplicamos la
        //     conversión en el handler recibiendo el objeto y ajustando PrecioVenta.

        private PrecioPresentacion AplicarConversionMoneda(PrecioPresentacion original) {
            var monedaElegida = Vista.MonedaPrecioVenta;

            if (monedaElegida == null || _monedaBase == null
                || monedaElegida.Id == _monedaBase.Id)
                return original;   // ya en base, sin conversión

            var tasa = RepoTasaCambio.Instancia
                .ObtenerTasaVigente(monedaElegida.Id, _monedaBase.Id);

            if (tasa == 1m) {
                CentroNotificaciones.MostrarNotificacion(
                    $"No existe tasa de cambio vigente para {monedaElegida.Codigo} → {_monedaBase.Codigo}. " +
                    "Registre la tasa antes de guardar la presentación.",
                    TipoNotificacionEnum.Advertencia);
                return original; // el presentador base rechazará la entidad por precio 0
            }

            return new PrecioPresentacion {
                Id = original.Id,
                IdProducto = original.IdProducto,
                IdUnidadMedida = original.IdUnidadMedida,
                Cantidad = original.Cantidad,
                // Precio convertido a moneda base
                PrecioVenta = Math.Round(original.PrecioVenta * tasa, 2, MidpointRounding.AwayFromZero),
                Activo = original.Activo
            };
        }

        // ── Construcción de tuplas ────────────────────────────────────────────

        protected override PresentadorTuplaVentaPresentacion ObtenerValoresTupla(
                PrecioPresentacion entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaVentaPresentacion(new VistaTuplaVentaPresentacion(), entidad);
            var producto = RepoProducto.Instancia.ObtenerPorId(entidad.IdProducto)!;
            var unidadMedida = RepoUnidadMedida.Instancia.ObtenerPorId(entidad.IdUnidadMedida)!;

            presentadorTupla.Vista.NombreUM = unidadMedida.Nombre;
            presentadorTupla.Vista.AbreviaturaUM = unidadMedida.Abreviatura;
            presentadorTupla.Vista.Cantidad = entidad.Cantidad;

            // Los precios almacenados están en CUP base → mostrar con símbolo base
            var simboloBase = _monedaBase?.Simbolo ?? "$";
            presentadorTupla.Vista.PrecioVenta = entidad.PrecioVenta;
            presentadorTupla.Vista.PrecioPorUnidad = entidad.PrecioPorUnidad;
            presentadorTupla.Vista.Descuento =
                producto.PrecioVentaBase > 0
                    ? ((producto.PrecioVentaBase - entidad.PrecioPorUnidad) / producto.PrecioVentaBase) * 100
                    : 0m;
            presentadorTupla.Vista.Estado = entidad.Activo;

            return presentadorTupla;
        }

        // ── Búsqueda ──────────────────────────────────────────────────────────

        public override void ActualizarResultadosBusqueda() {
            FiltroBusqueda = Vista.FiltroBusqueda;
            CriteriosBusqueda = Vista.CriteriosBusqueda;

            base.ActualizarResultadosBusqueda();
        }
    }
}