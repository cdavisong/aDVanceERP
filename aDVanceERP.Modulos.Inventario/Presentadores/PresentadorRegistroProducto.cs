using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Helpers.Comun;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Monedas;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Monedas;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorRegistroProducto : PresentadorVistaRegistro<IVistaRegistroProducto, Producto, RepoProducto, FiltroBusquedaProducto> {
        private Moneda? _monedaBase;

        public PresentadorRegistroProducto(IVistaRegistroProducto vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroProducto", OnMostrarVistaRegistroProducto);
            AgregadorEventos.Suscribir("MostrarVistaEdicionProducto", OnMostrarVistaEdicionProducto);
        }

        public Almacen? Almacen { get; set; }

        private void OnMostrarVistaRegistroProducto(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            CargarDatosComunes();

            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionProducto(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var datos = AgregadorEventos.DeserializarPayload<object[]>(obj);
            var idProducto = AgregadorEventos.DeserializarPayload<long>(datos[0]?.ToString() ?? string.Empty);
            var producto = RepoProducto.Instancia.ObtenerPorId(idProducto);
            var almacen = !string.IsNullOrEmpty(datos[1]?.ToString())
                ? AgregadorEventos.DeserializarPayload<Almacen>(datos[1].ToString())
                : null;

            if (producto == null)
                return;

            CargarDatosComunes();

            if (almacen != null)
                Almacen = almacen;

            PopularVistaDesdeEntidad(producto);

            Vista.Mostrar();
        }

        private void CargarDatosComunes() {
            var monedas = RepoMoneda.Instancia.ObtenerActivas();

            Vista.CargarProveedores([.. RepoProveedor.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarUnidadesMedida([.. RepoUnidadMedida.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarClasificaciones([.. RepoClasificacionProducto.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarMonedas([.. monedas]);

            // Cachear la moneda base para usar en la conversión
            _monedaBase = monedas.FirstOrDefault(m => m.EsBase);

            // Símbolo inicial = moneda base
            if (_monedaBase != null)
                Vista.ActualizarSimboloMoneda(_monedaBase.Simbolo);
        }

        public override void PopularVistaDesdeEntidad(Producto entidad) {
            base.PopularVistaDesdeEntidad(entidad);

            var proveedor = RepoProveedor.Instancia.ObtenerPorId(entidad.IdProveedor);
            var unidadMedida = RepoUnidadMedida.Instancia.ObtenerPorId(entidad.IdUnidadMedida);
            var clasificacion = RepoClasificacionProducto.Instancia.ObtenerPorId(entidad.IdClasificacionProducto);

            try {
                if (!string.IsNullOrWhiteSpace(entidad.RutaImagen) && File.Exists(entidad.RutaImagen)) {
                    using var fs = File.OpenRead(entidad.RutaImagen);
                    using var img = Image.FromStream(fs);
                    Vista.Imagen = new Bitmap(img);
                } else {
                    Vista.Imagen = null;
                }
            } catch {
                Vista.Imagen = null;
            }

            Vista.Categoria = entidad.Categoria;
            Vista.NombreProducto = entidad.Nombre;
            Vista.Codigo = entidad.Codigo;
            Vista.Descripcion = entidad.Descripcion;
            Vista.Proveedor = proveedor;
            Vista.UnidadMedida = unidadMedida;
            Vista.ClasificacionProducto = clasificacion;
            Vista.EsVendible = entidad.EsVendible;

            // En edición, el costo almacenado ya está en CUP base.
            // Se muestra en la moneda base y el combo se deja en base.
            Vista.CostoUnitario = entidad.Categoria == CategoriaProductoEnum.Mercancia
                                  || entidad.Categoria == CategoriaProductoEnum.MateriaPrima
                ? entidad.CostoAdquisicionUnitario
                : entidad.Categoria == CategoriaProductoEnum.ProductoTerminado
                    ? entidad.CostoProduccionUnitario
                    : 0m;

            Vista.MonedaCosto = _monedaBase; // siempre base en edición
            Vista.ImpuestoVentaPorcentaje = entidad.ImpuestoVentaPorcentaje;
            Vista.MargenGananciaDeseado = entidad.MargenGananciaDeseado;
            Vista.PrecioVentaBase = entidad.PrecioVentaBase;
            Vista.Almacen = Almacen;
        }

        protected override Producto? ObtenerEntidadDesdeVista() {
            Vista.SalvarImagenEnDirectorioLocal();

            var costoIngresado = Vista.CostoAdquisicionUnitario;
            var costoProduccion = Vista.CostoProduccionUnitario;
            var monedaElegida = Vista.MonedaCosto;

            if (monedaElegida != null && _monedaBase != null
                && monedaElegida.Id != _monedaBase.Id) {
                // Conversión: monto_en_moneda_elegida × tasa(elegida→base) = monto_en_base
                var tasa = RepoTasaCambio.Instancia
                    .ObtenerTasaVigente(monedaElegida.Id, _monedaBase.Id);

                if (tasa == 1m) {
                    CentroNotificaciones.MostrarNotificacion(
                        $"No existe una tasa de cambio vigente para {monedaElegida.Codigo} → {_monedaBase.Codigo}. " +
                        "Registre la tasa antes de guardar el producto.",
                        TipoNotificacionEnum.Advertencia);
                    return null;
                }

                costoIngresado = Math.Round(costoIngresado * tasa, 2, MidpointRounding.AwayFromZero);
                costoProduccion = Math.Round(costoProduccion * tasa, 2, MidpointRounding.AwayFromZero);
            }

            return new Producto {
                Id = _entidad?.Id ?? 0,
                Categoria = Vista.Categoria,
                Nombre = Vista.NombreProducto,
                Codigo = string.IsNullOrEmpty(Vista.Codigo)
                    ? CodigoHelper.GenerarEan13($"{Vista.Categoria}.{Vista.NombreProducto}")
                    : Vista.Codigo,
                Descripcion = Vista.Descripcion,
                IdProveedor = Vista.Proveedor?.Id ?? 0,
                IdUnidadMedida = Vista.UnidadMedida?.Id ?? 0,
                IdClasificacionProducto = Vista.ClasificacionProducto?.Id ?? 1,
                EsVendible = Vista.EsVendible,
                CostoAdquisicionUnitario = costoIngresado,
                CostoProduccionUnitario = costoProduccion,
                ImpuestoVentaPorcentaje = Vista.ImpuestoVentaPorcentaje,
                MargenGananciaDeseado = Vista.MargenGananciaDeseado,
                PrecioVentaBase = Vista.PrecioVentaBase,
                RutaImagen = Vista.RutaImagen,
                Activo = true
            };
        }

        protected override async void RegistroEdicionAuxiliar(RepoProducto repositorio, long id) {
            if (!Vista.ModoEdicion)
                AgregadorEventos.Publicar("ProductoRegistrado", AgregadorEventos.SerializarPayload(new object[] { Entidad!, Vista.Almacen, Vista.CantidadInicial }));
        }

        protected override bool EntidadCorrecta() {
            var productosConNombreRepetido = RepoProducto.Instancia
                .Buscar(FiltroBusquedaProducto.Nombre, Vista.NombreProducto)
                .cantidad;

            var nombreRepetido = !Vista.ModoEdicion && productosConNombreRepetido > 0;
            var nombreOk = !string.IsNullOrEmpty(Vista.NombreProducto) && !nombreRepetido;
            var codigoOk = !string.IsNullOrEmpty(Vista.Codigo);
            var unidadMedidaOk = Vista.UnidadMedida != null;
            var almacenOk = Vista.ModoEdicion || Vista.CantidadInicial > 0 && Vista.Almacen != null;
            var monedaOk = Vista.MonedaCosto != null;

            if (nombreRepetido)
                CentroNotificaciones.MostrarNotificacion(
                    "Ya existe un producto con el mismo nombre registrado en el sistema, los nombres de productos deben ser únicos.",
                    TipoNotificacionEnum.Advertencia);
            if (!nombreOk)
                CentroNotificaciones.MostrarNotificacion(
                    "El campo de nombre es obligatorio para el producto, por favor, corrija los datos entrados.",
                    TipoNotificacionEnum.Advertencia);
            if (!codigoOk)
                CentroNotificaciones.MostrarNotificacion(
                    "El campo de código es obligatorio para el producto, por favor, corrija los datos entrados.",
                    TipoNotificacionEnum.Advertencia);
            if (!unidadMedidaOk)
                CentroNotificaciones.MostrarNotificacion(
                    "El campo de unidad de medida es obligatorio para el producto, por favor, corrija los datos entrados.",
                    TipoNotificacionEnum.Advertencia);
            if (!almacenOk)
                CentroNotificaciones.MostrarNotificacion(
                    "El campo de almacén es obligatorio para el producto cuando se especifica una cantidad inicial, por favor, corrija los datos entrados.",
                    TipoNotificacionEnum.Advertencia);
            if (!monedaOk)
                CentroNotificaciones.MostrarNotificacion(
                    "Debe seleccionar la moneda en que se expresa el costo del producto.",
                    TipoNotificacionEnum.Advertencia);

            return nombreOk && codigoOk && unidadMedidaOk && almacenOk && monedaOk;
        }
    }
}