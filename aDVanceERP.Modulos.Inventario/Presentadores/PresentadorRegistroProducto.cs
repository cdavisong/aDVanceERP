using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Helpers.Comun;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorRegistroProducto : PresentadorVistaRegistro<IVistaRegistroProducto, Producto, RepoProducto, FiltroBusquedaProducto> {
        public PresentadorRegistroProducto(IVistaRegistroProducto vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroProducto", OnMostrarVistaRegistroProducto);
            AgregadorEventos.Suscribir("MostrarVistaEdicionProducto", OnMostrarVistaEdicionProducto);
        }

        public string? NombreAlmacen { get; set; }

        private void OnMostrarVistaRegistroProducto(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            Vista.CargarProveedores([.. RepoProveedor.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarUnidadesMedida([.. RepoUnidadMedida.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarClasificaciones([.. RepoClasificacionProducto.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);

            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionProducto(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var datos = AgregadorEventos.DeserializarPayload<object[]>(obj);
            var datosExtra = datos != null ? AgregadorEventos.DeserializarPayload<object[]>(datos[1].ToString()) : null;
            var producto = datos != null ? AgregadorEventos.DeserializarPayload<Producto>(datos[0].ToString()) : null;

            if (producto == null)
                return;

            // Carga inicial de datos
            Vista.CargarProveedores([.. RepoProveedor.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarUnidadesMedida([.. RepoUnidadMedida.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarClasificaciones([.. RepoClasificacionProducto.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);

            // Carga de datos extra
            if (datosExtra != null) {
                NombreAlmacen = datosExtra[0].ToString();
            }

            PopularVistaDesdeEntidad(producto);

            Vista.Mostrar();
        }

        public override void PopularVistaDesdeEntidad(Producto entidad) {
            base.PopularVistaDesdeEntidad(entidad);

            // Variables auxiliares
            var proveedor = RepoProveedor.Instancia.ObtenerPorId(entidad.IdProveedor);
            var unidadMedida = RepoUnidadMedida.Instancia.ObtenerPorId(entidad.IdUnidadMedida);
            var clasificacion = RepoClasificacionProducto.Instancia.ObtenerPorId(entidad.IdClasificacionProducto);

            // Cargar la imagen sin bloquear el archivo: crear una copia en memoria (Bitmap).
            try {
                if (!string.IsNullOrWhiteSpace(entidad?.RutaImagen) && File.Exists(entidad.RutaImagen)) {
                    using var fs = File.OpenRead(entidad.RutaImagen);
                    using var img = Image.FromStream(fs);
                    Vista.Imagen = new Bitmap(img); // copia desconectada del stream/archivo
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
            Vista.CostoUnitario = entidad.Categoria == CategoriaProductoEnum.Mercancia || entidad.Categoria == CategoriaProductoEnum.MateriaPrima
                    ? entidad.CostoAdquisicionUnitario
                    : entidad.Categoria == CategoriaProductoEnum.ProductoTerminado
                        ? entidad.CostoProduccionUnitario
                        : 0m;
            Vista.ImpuestoVentaPorcentaje = entidad.ImpuestoVentaPorcentaje;
            Vista.MargenGananciaDeseado = entidad.MargenGananciaDeseado;
            Vista.PrecioVentaBase = entidad.PrecioVentaBase;
        }

        protected override Producto? ObtenerEntidadDesdeVista() {
            Vista.SalvarImagenEnDirectorioLocal();

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
                CostoAdquisicionUnitario = Vista.CostoAdquisicionUnitario,
                CostoProduccionUnitario = Vista.CostoProduccionUnitario,
                ImpuestoVentaPorcentaje = Vista.ImpuestoVentaPorcentaje,
                MargenGananciaDeseado = Vista.MargenGananciaDeseado,
                PrecioVentaBase = Vista.PrecioVentaBase,
                RutaImagen = Vista.RutaImagen,
                Activo = true
            };
        }

        protected override async void RegistroEdicionAuxiliar(RepoProducto repositorio, long id) {
            if (!Vista.ModoEdicion)
                AgregadorEventos.Publicar("ProductoRegistrado", AgregadorEventos.SerializarPayload((Entidad, Vista.Almacen, Vista.CantidadInicial)));
        }

        protected override bool EntidadCorrecta() {
            var productosConNombreRepetido = RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, Vista.NombreProducto).cantidad;
            var nombreRepetido = !Vista.ModoEdicion && productosConNombreRepetido > 0;
            var nombreOk = !string.IsNullOrEmpty(Vista.NombreProducto) && !nombreRepetido;
            var codigoOk = !string.IsNullOrEmpty(Vista.Codigo);
            var unidadMedidaOk = Vista.UnidadMedida != null;

            if (nombreRepetido)
                CentroNotificaciones.MostrarNotificacion("Ye existe un producto con el mismo nombre registrado en el sistema, los nombres de productos deben ser únicos.", TipoNotificacionEnum.Advertencia);
            if (!nombreOk)
                CentroNotificaciones.MostrarNotificacion("El campo de nombre es obligatorio para el producto, por favor, corrija los datos entrados", TipoNotificacionEnum.Advertencia);
            if (!codigoOk)
                CentroNotificaciones.MostrarNotificacion("El campo de código es obligatorio para el producto, por favor, corrija los datos entrados", TipoNotificacionEnum.Advertencia);
            if (!unidadMedidaOk)
                CentroNotificaciones.MostrarNotificacion("El campo de unidad de medida es obligatorio para el producto, por favor, corrija los datos entrados", TipoNotificacionEnum.Advertencia);

            return nombreOk && codigoOk && unidadMedidaOk;
        }
    }
}