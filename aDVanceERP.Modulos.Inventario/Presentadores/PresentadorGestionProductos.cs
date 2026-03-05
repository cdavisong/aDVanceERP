using aDVanceERP.Core.Controladores;
using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Documentos;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

using DocumentFormat.OpenXml.Office2010.Excel;

using System.Drawing.Imaging;
using System.Text.Json;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorGestionProductos : PresentadorVistaGestion<PresentadorTuplaProducto, IVistaGestionProductos, IVistaTuplaProducto, Producto, RepoProducto, FiltroBusquedaProducto> {
        private ControladorArchivosAndroidStock _androidStock = new ControladorArchivosAndroidStock(Application.StartupPath);
        private string _directorioImagen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "res", "imagenes", "productos");
        private string _rutaImagen = string.Empty;

        public PresentadorGestionProductos(IVistaGestionProductos vista) : base(vista) {
            vista.ActualizarCatalogoApp += OnActualizarCatalogoApp;
            vista.ImportarProductosDispositivo += OnImportarProductosDesdeDispositivo;
            vista.GenerarCatalogoProductos += OnGenerarCatalogoProductos;

            RegistrarEntidad += OnRegistrarProducto;
            EditarEntidad += OnEditarProducto;

            AgregadorEventos.Suscribir("MostrarVistaGestionProductos", OnMostrarVistaGestionProductos);
            AgregadorEventos.Suscribir("HabilitarDeshabilitarProducto", OnHabilitarDeshabilitarProducto);
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
            Vista.CargarFiltrosBusqueda(UtilesBusquedaProducto.FiltroBusquedaProducto);
            Vista.Restaurar();

            // Cambiar visibilidad del botón para importar ventas desde dispositivo según conexión con dispositivo Android
            Vista.MostrarBotonesAplicacionStock = _androidStock.CheckDeviceConnection() && _androidStock.CheckAppInstalada();

            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void OnActualizarCatalogoApp(object? sender, EventArgs e) {
            var exportador = new ExportadorCatalogosStock(@"exports\stock\");
            var productos = RepoProducto.Instancia.ObtenerTodos().Select(p => p.entidadBase);
            var proveedores = RepoProveedor.Instancia.ObtenerTodos().Select(p => p.entidadBase);
            var unidadesMedida = RepoUnidadMedida.Instancia.ObtenerTodos().Select(um => um.entidadBase);
            var clasificacionesProducto = RepoClasificacionProducto.Instancia.ObtenerTodos().Select(c => c.entidadBase);
            var almacenes = RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.entidadBase);

            // Exportar todos los catálogos necesarios para la app de Android
            exportador.ExportarTodo(
                productos,
                proveedores,
                unidadesMedida,
                clasificacionesProducto,
                almacenes
                );

            // Enviar catálogos la teléfono
            _androidStock.FlujoPreparacion(
                exportador.RutaProductos,
                exportador.RutaProveedores,
                exportador.RutaUnidades,
                exportador.RutaClasificaciones,
                exportador.RutaAlmacenes
                );

            CentroNotificaciones.MostrarNotificacion("El catálogo de productos y datos relacionados ha sido actualizado exitosamente en la aplicación móvil.", TipoNotificacionEnum.Info);
        }

        private void OnImportarProductosDesdeDispositivo(object? sender, EventArgs e) {
            var rutaDirectorioProductos = Path.Combine(Application.StartupPath, "imports", "stock");
            var archivosDescargados = _androidStock.FlujoImportacion(rutaDirectorioProductos, true);

            if (archivosDescargados.Exitoso) {
                var repoAlmacen = RepoAlmacen.Instancia;
                var repoProducto = RepoProducto.Instancia;
                var repoinventario = RepoInventario.Instancia;
                var repoMovimiento = RepoMovimiento.Instancia;
                var repoTipoMovimiento = RepoTipoMovimiento.Instancia;

                try {
                    foreach (var archivo in archivosDescargados.JsonDescargados) {
                        var contenido = File.ReadAllText(archivo);
                        var root = JsonSerializer.Deserialize<ExportacionSesion>(contenido);

                        if (root?.Productos == null || root.Productos.Count == 0) {
                            // Archivo válido pero sin productos
                            continue;
                        }

                        foreach (var productoApp in root.Productos) {
                            var idProducto = 0L;

                            if (productoApp.Tipo.Equals("nuevo")) {
                                var producto = new Producto {
                                    Id = 0,
                                    Categoria = Enum.TryParse<CategoriaProducto>(productoApp.Categoria, out var categoria) ? categoria : CategoriaProducto.Mercancia,
                                    Nombre = productoApp.Nombre!,
                                    Codigo = productoApp.Codigo,
                                    Descripcion = productoApp.Descripcion!,
                                    IdProveedor = productoApp?.IdProveedor ?? 0,
                                    IdUnidadMedida = productoApp?.IdUnidadMedida ?? 0,
                                    IdClasificacionProducto = productoApp?.IdClasificacion ?? 1,
                                    EsVendible = productoApp!.EsVendible ?? false,
                                    CostoAdquisicionUnitario = productoApp.CostoAdquisicionUnitario ?? 0,
                                    CostoProduccionUnitario = 0,
                                    ImpuestoVentaPorcentaje = 10,
                                    MargenGananciaDeseado = ((productoApp.PrecioVentaBase ?? 0) - ((productoApp.CostoAdquisicionUnitario ?? 0) + ((productoApp.CostoAdquisicionUnitario ?? 0) * (10 / 100m)))) * 100 / (productoApp.CostoAdquisicionUnitario ?? 0),
                                    PrecioVentaBase = productoApp.PrecioVentaBase ?? 0,
                                    RutaImagen = productoApp.TieneImagen ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "res", "imagenes", "productos", $"{productoApp.Codigo}.jpg") : string.Empty,
                                    Activo = true
                                };

                                idProducto = repoProducto.Adicionar(producto);

                                // Crear movimiento de inventario inicial
                                var movimiento = new Movimiento() {
                                    Id = 0,
                                    IdProducto = idProducto,
                                    CostoUnitario = productoApp.CostoAdquisicionUnitario ?? 0,
                                    IdAlmacenOrigen = 0,
                                    IdAlmacenDestino = root.IdAlmacen,
                                    Estado = EstadoMovimiento.Completado,
                                    FechaCreacion = DateTime.Now,
                                    SaldoInicial = 0m,
                                    FechaTermino = DateTime.Now,
                                    CantidadMovida = productoApp.Cantidad,
                                    SaldoFinal = productoApp.Cantidad,
                                    IdTipoMovimiento = repoTipoMovimiento.Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Carga Inicial").resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0,
                                    IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                    Notas = "Movimiento de inventario inicial al registrar el producto. Producto importado desde aplicación móvil",
                                };

                                // Adicionar a la base de datos local
                                repoMovimiento.Adicionar(movimiento);

                                // Crear inventario inicial en el almacén seleccionado
                                var inventario = new Core.Modelos.Modulos.Inventario.Inventario() {
                                    Id = 0,
                                    IdProducto = idProducto,
                                    IdAlmacen = root.IdAlmacen,
                                    Cantidad = productoApp.Cantidad,
                                    CostoPromedio = productoApp.CostoAdquisicionUnitario ?? 0,
                                    ValorTotal = (productoApp.CostoAdquisicionUnitario ?? 0) * productoApp.Cantidad,
                                    UltimaActualizacion = DateTime.Now
                                };

                                repoinventario.Adicionar(inventario);

                                // Almacenar imagen si existe
                                if (productoApp.TieneImagen) {
                                    _rutaImagen = Path.Combine(rutaDirectorioProductos, "imagenes", $"{productoApp.Codigo}.jpg");

                                    if (File.Exists(_rutaImagen))
                                        SalvarImagenEnDirectorioLocal();
                                }
                            } else {
                                var producto = repoProducto.Buscar(FiltroBusquedaProducto.Codigo, productoApp.Codigo).resultadosBusqueda.Select(p => p.entidadBase).FirstOrDefault();
                                var inventario = repoinventario.Buscar(FiltroBusquedaInventario.IdProducto, producto?.Id.ToString()).resultadosBusqueda.Select(i => i.entidadBase).FirstOrDefault(i => i.IdAlmacen == root.IdAlmacen);

                                // Crear movimiento de inventario
                                var movimiento = new Movimiento() {
                                    Id = 0,
                                    IdProducto = producto.Id,
                                    CostoUnitario = productoApp.CostoAdquisicionUnitario ?? 0,
                                    IdAlmacenOrigen = 0,
                                    IdAlmacenDestino = root.IdAlmacen,
                                    Estado = EstadoMovimiento.Completado,
                                    FechaCreacion = DateTime.Now,
                                    SaldoInicial = inventario?.Cantidad ?? 0m,
                                    FechaTermino = DateTime.Now,
                                    CantidadMovida = productoApp.Cantidad,
                                    SaldoFinal = (inventario?.Cantidad ?? 0m) + productoApp.Cantidad,
                                    IdTipoMovimiento = repoTipoMovimiento.Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Compra").resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0,
                                    IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                    Notas = "Movimiento de inventario por nueva compra de producto existente. Producto importado desde aplicación móvil",
                                };

                                // Adicionar a la base de datos local
                                repoMovimiento.Adicionar(movimiento);

                                // Realizar movimiento de inventario
                                repoinventario.ModificarInventario(
                                    producto.Nombre,
                                    string.Empty,
                                    repoAlmacen.ObtenerPorId(root.IdAlmacen)?.Nombre,
                                    movimiento.CantidadMovida
                                );
                            }
                        }
                    }

                    CentroNotificaciones.MostrarNotificacion("Los productos y movimientos de entrada han sido importados exitosamente desde la aplicación móvil.", TipoNotificacionEnum.Info);

                    ActualizarResultadosBusqueda();
                } catch (Exception) {
                    CentroNotificaciones.MostrarNotificacion($"Ocurrieron errores durante la importación.", TipoNotificacionEnum.Error);
                }
            } else {
                CentroNotificaciones.MostrarNotificacion("No se pudieron descargar los archivos de productos desde el dispositivo móvil. Por favor, asegúrese de que el dispositivo esté conectado correctamente y que la aplicación móvil esté instalada.", TipoNotificacionEnum.Error);
            }
        }

        private void OnGenerarCatalogoProductos(object? sender, EventArgs e) {
            var docCatalogoProductos = new DocCatalogoComercial();

            docCatalogoProductos.GenerarDocumento();
        }

        private void OnHabilitarDeshabilitarProducto(string obj) {
            var idProductoSeleccionado = _tuplasEntidades.FirstOrDefault(t => t.EstadoSeleccion)?.Vista.Id ?? 0;

            if (idProductoSeleccionado != 0) {
                var estado = RepoProducto.Instancia.HabilitarDeshabilitarProducto(idProductoSeleccionado);

                ActualizarResultadosBusqueda();

                CentroNotificaciones.MostrarNotificacion($"El producto ha sido {(estado ? "habilitado" : "deshabilitado")} satisfactoriamente.", TipoNotificacionEnum.Info);
            }
        }

        protected override PresentadorTuplaProducto ObtenerValoresTupla(Producto entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaProducto(new VistaTuplaProducto(), entidad);
            var unidadMedidaProducto = RepoUnidadMedida.Instancia.ObtenerPorId(entidad.IdUnidadMedida);
            var inventarioProducto = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, entidad.Id.ToString());

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.Codigo = entidad.Codigo ?? string.Empty;
            presentadorTupla.Vista.FechaUltimoMovimiento = inventarioProducto.cantidad > 0 ? inventarioProducto.resultadosBusqueda.Min(inv => inv.entidadBase.UltimaActualizacion) : DateTime.MinValue;
            presentadorTupla.Vista.NombreAlmacen = string.IsNullOrEmpty(Vista.NombreAlmacen) || Vista.NombreAlmacen.Contains("Todos") ? "-" : Vista.NombreAlmacen;
            presentadorTupla.Vista.NombreProducto = entidad.Nombre ?? string.Empty;
            presentadorTupla.Vista.Descripcion = entidad.Descripcion ?? "No hay descripción disponible";
            presentadorTupla.Vista.CostoUnitario = entidad.Categoria == CategoriaProducto.ProductoTerminado ? entidad.CostoProduccionUnitario : entidad.CostoAdquisicionUnitario;
            presentadorTupla.Vista.PrecioVentaBase = entidad.PrecioVentaBase;
            presentadorTupla.Vista.UnidadMedida = unidadMedidaProducto?.Abreviatura ?? "U";
            presentadorTupla.Vista.Stock = string.IsNullOrEmpty(Vista.NombreAlmacen) || Vista.NombreAlmacen.Contains("Todos") ? inventarioProducto.resultadosBusqueda.Sum(inv => inv.entidadBase.Cantidad) : inventarioProducto.resultadosBusqueda.Find(inv => RepoAlmacen.Instancia.ObtenerPorId(inv.entidadBase.IdAlmacen)?.Nombre.Equals(Vista.NombreAlmacen) ?? false).entidadBase?.Cantidad ?? 0;
            presentadorTupla.Vista.MovimientoPositivoStock += delegate (object? sender, EventArgs args) {
                var nombreAlmacen = sender as string;
                var objetoPos = new object[] { entidad, "+" };

                AgregadorEventos.Publicar("MostrarVistaRegistroMovimiento", AgregadorEventos.SerializarPayload(objetoPos));
            };
            presentadorTupla.Vista.MovimientoNegativoStock += delegate (object? sender, EventArgs args) {
                var nombreAlmacen = sender as string;
                var objetoNeg = new object[] { entidad, "-" };

                AgregadorEventos.Publicar("MostrarVistaRegistroMovimiento", AgregadorEventos.SerializarPayload(objetoNeg));
            };

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