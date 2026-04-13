using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Documentos;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

using System.Drawing.Imaging;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorGestionProductos : PresentadorVistaGestion<PresentadorTuplaProducto, IVistaGestionProductos, IVistaTuplaProducto, Producto, RepoProducto, FiltroBusquedaProducto> {
        private string _directorioImagen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "res", "imagenes", "productos");
        private string _rutaImagen = string.Empty;

        public PresentadorGestionProductos(IVistaGestionProductos vista) : base(vista) {
            vista.GenerarCatalogoProductos += OnGenerarCatalogoProductos;

            RegistrarEntidad += OnRegistrarProducto;
            EditarEntidad += OnEditarProducto;

            AgregadorEventos.Suscribir("MostrarVistaGestionProductos", OnMostrarVistaGestionProductos);
        }

        private void OnRegistrarProducto(object? sender, EventArgs e) {
            if (RepoAlmacen.Instancia.Cantidad() == 0) {
                CentroNotificaciones.MostrarNotificacion("No es posible registrar un nuevo producto porque no hay almacenes registrados en el sistema. Por favor, registre al menos un almacén antes de continuar.", TipoNotificacionEnum.Advertencia);
                return;
            }

            AgregadorEventos.Publicar("MostrarVistaRegistroProducto", string.Empty);
        }

        private void OnEditarProducto(object? sender, Producto e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionProducto", AgregadorEventos.SerializarPayload(new object[] { e, sender }));
        }

        private void OnMostrarVistaGestionProductos(string obj) {
            Vista.CargarFiltroAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.entidadBase.Nombre).Prepend("Todos los almacenes")]);
            Vista.CargarFiltrosBusqueda([.. EnumExt.ObtenerNombresDescripciones<FiltroBusquedaProducto>()]);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void OnGenerarCatalogoProductos(object? sender, EventArgs e) {
            var docCatalogoProductos = new DocCatalogoComercial();

            docCatalogoProductos.GenerarDocumento();
        }

        protected override PresentadorTuplaProducto ObtenerValoresTupla(Producto entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaProducto(new VistaTuplaProducto(), entidad);
            var unidadMedidaProducto = RepoUnidadMedida.Instancia.ObtenerPorId(entidad.IdUnidadMedida);
            var (cantidad, resultadosBusqueda) = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, entidad.Id.ToString());
            var presentaciones = RepoPrecioPresentacion.Instancia.Buscar(FiltroBusquedaPrecioPresentacion.IdProducto, entidad.Id.ToString());

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.Codigo = entidad.Codigo ?? string.Empty;
            presentadorTupla.Vista.FechaUltimoMovimiento = cantidad > 0 
                    ? resultadosBusqueda.Min(inv => inv.entidadBase.UltimaActualizacion) 
                    : DateTime.MinValue;
            presentadorTupla.Vista.NombreAlmacen = string.IsNullOrEmpty(Vista.NombreAlmacen) || Vista.NombreAlmacen.Contains("Todos") 
                    ? "-" 
                    : Vista.NombreAlmacen;
            presentadorTupla.Vista.NombreDescripcion = string.IsNullOrEmpty(entidad.Nombre)
                ? string.IsNullOrEmpty(entidad.Descripcion)
                    ? "No existe un nombre o descripción disponible para el producto"
                    : entidad.Descripcion
                : $"{entidad.Nombre}{(string.IsNullOrEmpty(entidad.Descripcion) ? "" : $", {entidad.Descripcion}")}";
            presentadorTupla.Vista.CostoUnitario = entidad.Categoria == CategoriaProductoEnum.ProductoTerminado 
                ? entidad.CostoProduccionUnitario 
                : entidad.CostoAdquisicionUnitario;
            presentadorTupla.Vista.PrecioVentaBase = entidad.PrecioVentaBase;
            presentadorTupla.Vista.Presentaciones = presentaciones.cantidad;
            presentadorTupla.Vista.Stock = string.IsNullOrEmpty(Vista.NombreAlmacen) || Vista.NombreAlmacen.Contains("Todos")
                ? resultadosBusqueda.Sum(inv => inv.entidadBase.Cantidad)
                : resultadosBusqueda
                    .Find(inv => RepoAlmacen.Instancia
                    .ObtenerPorId(inv.entidadBase.IdAlmacen)?
                    .Nombre
                    .Equals(Vista.NombreAlmacen) ?? false)
                    .entidadBase?
                    .Cantidad ?? 0;
            presentadorTupla.Vista.UnidadMedida = unidadMedidaProducto?.Abreviatura ?? "u";
            
            return presentadorTupla;
        }

        public void SalvarImagenEnDirectorioLocal() {
            if (string.IsNullOrEmpty(_rutaImagen))
                return;

            var rutaImagen = Path.Combine(_directorioImagen, Path.GetFileName(_rutaImagen));

            if (File.Exists(rutaImagen))
                File.Delete(rutaImagen);

            // Convertir la imagen original del producto a un formato compatible con el guardado (por ejemplo, JPEG o PNG)
            var formatoImagen = Path.GetExtension(_rutaImagen).ToLower() switch {
                ".jpg" or ".jpeg" => ImageFormat.Jpeg,
                ".png" => ImageFormat.Png,
                _ => ImageFormat.Png
            };

            // Cargar la imagen sin bloquear el archivo y guardarla en la ruta destino
            using (var bitmap = CargarBitmapSinBloquear(_rutaImagen)) {
                bitmap.Save(rutaImagen, formatoImagen);
            }
        }

        // Método auxiliar que carga un Bitmap desde archivo sin bloquear el archivo en disco.
        // Lee todos los bytes, crea un MemoryStream, obtiene una Image desde el stream y
        // devuelve un nuevo Bitmap copiado en memoria. Esto permite cerrar el stream y
        // liberar el archivo original inmediatamente.
        private static Bitmap CargarBitmapSinBloquear(string ruta) {
            var bytes = File.ReadAllBytes(ruta);
            using (var ms = new MemoryStream(bytes)) {
                using (var img = Image.FromStream(ms)) {
                    return new Bitmap(img);
                }
            }
        }
    }
}