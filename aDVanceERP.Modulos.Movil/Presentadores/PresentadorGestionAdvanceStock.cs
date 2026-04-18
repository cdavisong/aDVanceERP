using aDVanceERP.Core.Controladores;
using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Movil.Interfaces;

using System.Text.Json;

namespace aDVanceERP.Modulos.Movil.Presentadores {
    internal class PresentadorGestionAdvanceStock : PresentadorVistaBase<IVistaGestionAdvanceStock> {
        private readonly ControladorArchivosAndroidStock _controladorStock;
        private readonly ExportadorCatalogosStock _exportador;

        public PresentadorGestionAdvanceStock(IVistaGestionAdvanceStock vista) : base(vista) {
            _controladorStock = new ControladorArchivosAndroidStock(Application.StartupPath);
            _exportador = new ExportadorCatalogosStock(CarpetaExportacion);

            Vista.VerificarConexion += OnVerificarConexion;
            Vista.EnviarCatalogos += OnEnviarCatalogos;
            Vista.EliminarCatalogos += OnEliminarCatalogos;
            Vista.ImportarSesiones += OnImportarSesiones;

            AgregadorEventos.Suscribir("MostrarVistaGestionStock", OnMostrarVistaGestionStock);
        }

        private string CarpetaExportacion => Path.Combine(Application.StartupPath, "exports", "stock");

        private string CarpetaImportacion => Path.Combine(Application.StartupPath, "imports", "stock");

        private void OnMostrarVistaGestionStock(string obj) {
            Vista.Restaurar();
            ActualizarEstadoConexion();
            Vista.Mostrar();
        }

        private void OnVerificarConexion(object? sender, EventArgs e)
            => ActualizarEstadoConexion(mostrarAdvertencia: true);

        private void ActualizarEstadoConexion(bool mostrarAdvertencia = false) {
            bool conectado = _controladorStock.CheckDeviceConnection(mostrarAdvertencia);
            bool appInstalada = conectado && _controladorStock.CheckAppInstalada();

            Vista.DispositivoConectado = conectado;
            Vista.AppInstalada = appInstalada;
            Vista.MostrarBotonEnviarCatalogos = appInstalada;
            Vista.MostrarBotonImportarSesiones = appInstalada;

            if (conectado && appInstalada) {
                // ── Estado de catálogos (ls -la sobre el archivo principal) ──
                var (existen, fecha) = _controladorStock.ObtenerInfoCatalogos();
                Vista.CatalogosExistenEnDispositivo = existen;
                Vista.FechaActualizacionCatalogos = fecha;
                Vista.MostrarBotonEliminarCatalogos = existen;

                // ── Archivos de sesiones disponibles ──
                var sesiones = _controladorStock.ListarArchivosStock();
                Vista.ArchivosDisponiblesDispositivo = sesiones.Count;
                Vista.ActualizarArchivosSesion(sesiones);
            } else {
                Vista.CatalogosExistenEnDispositivo = false;
                Vista.FechaActualizacionCatalogos = null;
                Vista.MostrarBotonEliminarCatalogos = false;
                Vista.ActualizarArchivosSesion(
                    new List<(string fileName, DateTime fechaHora, double tamanoKb)>());
            }
        }

        private void OnEnviarCatalogos(object? sender, EventArgs e) {
            if (!_controladorStock.CheckDeviceConnection(mostrarAdvertencia: true))
                return;

            GenerarCatalogosStock();

            _controladorStock.FlujoPreparacion(
                _exportador.RutaProductos,
                _exportador.RutaProveedores,
                _exportador.RutaUnidades,
                _exportador.RutaClasificaciones,
                _exportador.RutaAlmacenes);

            // Refrescar para actualizar el estado de catálogos y el botón eliminar
            ActualizarEstadoConexion();
        }

        private void OnEliminarCatalogos(object? sender, EventArgs e) {
            var respuesta = CentroNotificaciones.MostrarMensaje(
                "¿Está seguro de que desea eliminar todos los catálogos del dispositivo? " +
                "aDVance.STOCK no podrá registrar inventario hasta que se envíen nuevos catálogos.",
                TipoMensaje.Advertencia,
                BotonesMensaje.SiNo);

            if (respuesta != DialogResult.Yes)
                return;

            _controladorStock.LimpiarCatalogos();
            ActualizarEstadoConexion();
        }

        private void OnImportarSesiones(object? sender, EventArgs e) {
            if (!_controladorStock.CheckDeviceConnection(mostrarAdvertencia: true))
                return;

            // Si se dispara desde la tupla individual, sender es el nombre del archivo
            string archivoIndividual = sender as string ?? string.Empty;

            ResultadoPullStock resultado;

            if (!string.IsNullOrEmpty(archivoIndividual) && archivoIndividual.StartsWith("stock_")) {
                // Importar sólo el archivo seleccionado
                string destino = Path.Combine(CarpetaImportacion, archivoIndividual);
                resultado = _controladorStock.PullSesionIndividual(archivoIndividual, destino);
            } else {
                // Importar todas las sesiones pendientes y limpiar el dispositivo
                resultado = _controladorStock.FlujoImportacion(
                    CarpetaImportacion,
                    limpiarDispositivoTrasImportar: true);
            }

            if (!resultado.Exitoso || resultado.JsonDescargados.Count == 0)
                return;

            ProcesarSesionesStock(resultado.JsonDescargados);
            ActualizarEstadoConexion(); // refrescar tabla
        }

        /// <summary>
        /// Deserializa cada archivo de sesión descargado y actualiza el
        /// inventario del ERP con los conteos realizados en campo.
        /// Publica <c>SesionesStockImportadas</c> al terminar para que
        /// MOD_INVENTARIO refresque su vista si está abierta.
        /// </summary>
        private void ProcesarSesionesStock(List<string> archivosJson) {
            int productosActualizados = 0;
            int productosNuevos = 0;
            var errores = new List<string>();

            var repoProducto = RepoProducto.Instancia;
            var repoMovimiento = RepoMovimiento.Instancia;
            var repoInventario = RepoInventario.Instancia;

            var idTipoMovimientoCompra = RepoTipoMovimiento.Instancia
                .Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Compra")
                .resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0;
            var idTipoMovimientoCargaInicial = RepoTipoMovimiento.Instancia
                .Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Carga Inicial")
                .resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0;

            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            foreach (var archivo in archivosJson) {
                try {
                    if (!File.Exists(archivo)) continue;

                    var sesion = JsonSerializer.Deserialize<SesionStockJson>(
                        File.ReadAllText(archivo), opciones);

                    if (sesion?.Items == null || sesion.Items.Count == 0)
                        continue;

                    foreach (var item in sesion.Items) {
                        try {
                            Producto? producto = item.IdProducto > 0
                                ? repoProducto.ObtenerPorId(item.IdProducto)
                                : null;

                            // Producto existente no encontrado en BD → error
                            if (producto == null && item.IdProducto > 0) {
                                errores.Add($"'{Path.GetFileName(archivo)}' — producto Id={item.IdProducto} no encontrado.");
                                continue;
                            }

                            // Producto nuevo registrado en campo
                            if (producto == null) {
                                producto = new Producto {
                                    Id = 0,
                                    Codigo = item.Codigo ?? $"STOCK-{DateTime.Now.Ticks}",
                                    Nombre = item.Nombre ?? "Producto sin nombre",
                                    Categoria = CategoriaProductoEnum.MateriaPrima,
                                    IdClasificacionProducto = item.IdClasificacion,
                                    IdUnidadMedida = item.IdUnidadMedida,
                                    CostoAdquisicionUnitario = item.CostoUnitario,
                                    CostoProduccionUnitario = 0,
                                    PrecioVentaBase = item.CostoUnitario,
                                    Activo = true
                                };
                                producto.Id = repoProducto.Adicionar(producto);
                                productosNuevos++;

                                AgregadorEventos.Publicar("NuevoProductoRegistrado", string.Empty);
                            }

                            // ── Actualizar inventario ──
                            var inventarioExistente = repoInventario
                                .Buscar(FiltroBusquedaInventario.IdProducto, producto.Id.ToString())
                                .resultadosBusqueda
                                .FirstOrDefault(r => r.entidadBase.IdAlmacen == item.IdAlmacen)
                                .entidadBase;

                            if (inventarioExistente != null) {
                                repoMovimiento.Adicionar(new Movimiento {
                                    Id = 0,
                                    IdProducto = producto.Id,
                                    CostoUnitario = item.CostoUnitario,
                                    IdAlmacenOrigen = 0,
                                    IdAlmacenDestino = item.IdAlmacen,
                                    Estado = EstadoMovimientoEnum.Completado,
                                    FechaCreacion = sesion.FechaSesion,
                                    SaldoInicial = inventarioExistente.Cantidad,
                                    FechaTermino = sesion.FechaSesion,
                                    CantidadMovida = item.CantidadRegistrada,
                                    SaldoFinal = inventarioExistente.Cantidad + item.CantidadRegistrada,
                                    IdTipoMovimiento = idTipoMovimientoCompra,
                                    IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                    Notas = $"Compra desde sesión aDVance.STOCK — {sesion.NombreSesion}."
                                });

                                inventarioExistente.Cantidad = item.CantidadRegistrada;
                                repoInventario.Editar(inventarioExistente);
                            } else {
                                repoMovimiento.Adicionar(new Movimiento {
                                    Id = 0,
                                    IdProducto = producto.Id,
                                    CostoUnitario = item.CostoUnitario,
                                    IdAlmacenOrigen = 0,
                                    IdAlmacenDestino = item.IdAlmacen,
                                    Estado = EstadoMovimientoEnum.Completado,
                                    FechaCreacion = sesion.FechaSesion,
                                    SaldoInicial = 0,
                                    FechaTermino = sesion.FechaSesion,
                                    CantidadMovida = item.CantidadRegistrada,
                                    SaldoFinal = item.CantidadRegistrada,   // ajuste absoluto
                                    IdTipoMovimiento = idTipoMovimientoCargaInicial,
                                    IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                    Notas = $"Carga inicial desde sesión aDVance.STOCK — {sesion.NombreSesion}."
                                });

                                repoInventario.Adicionar(new Inventario {
                                    Id = 0,
                                    IdProducto = producto.Id,
                                    IdAlmacen = item.IdAlmacen,
                                    Cantidad = item.CantidadRegistrada,
                                    CostoPromedio = item.CostoUnitario,
                                    ValorTotal = item.CostoUnitario * item.CantidadRegistrada,
                                    UltimaActualizacion = DateTime.Now
                                });
                            }

                            productosActualizados++;

                        } catch (Exception itemEx) {
                            errores.Add($"'{Path.GetFileName(archivo)}' — " +
                                        $"item {item.Codigo ?? item.IdProducto.ToString()}: {itemEx.Message}");
                        }
                    }

                    // Archivar el JSON procesado
                    try {
                        string carpetaProcesados = Path.Combine(CarpetaImportacion, "procesados");
                        Directory.CreateDirectory(carpetaProcesados);
                        File.Move(archivo,
                            Path.Combine(carpetaProcesados, Path.GetFileName(archivo)),
                            overwrite: true);
                    } catch { /* no crítico */ }

                } catch (Exception archivoEx) {
                    errores.Add($"Error procesando '{Path.GetFileName(archivo)}': {archivoEx.Message}");
                }
            }

            // ── Notificación resumen ──
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Productos actualizados: {productosActualizados}");
            if (productosNuevos > 0)
                sb.AppendLine($"Productos nuevos registrados: {productosNuevos}");
            if (errores.Count > 0) {
                sb.AppendLine();
                sb.AppendLine("Errores (primeros 5):");
                foreach (var err in errores.Take(5))
                    sb.AppendLine($"• {err}");
            }

            CentroNotificaciones.MostrarNotificacion(
                sb.ToString().TrimEnd(),
                errores.Count == 0 ? TipoNotificacionEnum.Ok : TipoNotificacionEnum.Advertencia);

            if (productosActualizados > 0)
                AgregadorEventos.Publicar("SesionesStockImportadas", string.Empty);
        }

        private void GenerarCatalogosStock() {
            var productos = RepoProducto.Instancia.ObtenerTodos().Select(r => r.entidadBase);
            var proveedores = RepoProveedor.Instancia.ObtenerTodos().Select(r => r.entidadBase);
            var unidades = RepoUnidadMedida.Instancia.ObtenerTodos().Select(r => r.entidadBase);
            var clasificaciones = RepoClasificacionProducto.Instancia.ObtenerTodos().Select(r => r.entidadBase);
            var almacenes = RepoAlmacen.Instancia.ObtenerTodos().Select(r => r.entidadBase);

            _exportador.ExportarTodo(productos, proveedores, unidades, clasificaciones, almacenes);
        }

        public override void Dispose() {
            Vista.VerificarConexion -= OnVerificarConexion;
            Vista.EnviarCatalogos -= OnEnviarCatalogos;
            Vista.EliminarCatalogos -= OnEliminarCatalogos;
            Vista.ImportarSesiones -= OnImportarSesiones;

            AgregadorEventos.Desuscribir("MostrarVistaGestionStock", OnMostrarVistaGestionStock);

            Vista.Cerrar();
        }
    }

    file class SesionStockJson {
        public string NombreSesion { get; set; } = string.Empty;
        public DateTime FechaSesion { get; set; }
        public List<ItemStockJson> Items { get; set; } = [];
    }

    file class ItemStockJson {
        public long IdProducto { get; set; }   // 0 si es producto nuevo
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public long IdClasificacion { get; set; }
        public long IdUnidadMedida { get; set; }
        public long IdAlmacen { get; set; }
        public decimal CantidadRegistrada { get; set; }
        public decimal CostoUnitario { get; set; }
        public string? ImagenNombreArchivo { get; set; }
    }
}