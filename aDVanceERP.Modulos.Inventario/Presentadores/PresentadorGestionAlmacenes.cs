using aDVanceERP.Core.Controladores;
using aDVanceERP.Core.Documentos.Interfaces;
using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Helpers.Comun;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun;
using aDVanceERP.Modulos.Inventario.Documentos;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

using ClosedXML.Excel;

using System.Data;
using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorGestionAlmacenes : PresentadorVistaGestion<PresentadorTuplaAlmacen, IVistaGestionAlmacenes, IVistaTuplaAlmacen, Almacen, RepoAlmacen, FiltroBusquedaAlmacen> {
        private ControladorArchivosAndroid _androidFileManager = new ControladorArchivosAndroid(Application.StartupPath);
        private DocInventarioAlmacen _docInventarioAlmacen = new DocInventarioAlmacen();
        private bool _dispositivoConectado;

        public PresentadorGestionAlmacenes(IVistaGestionAlmacenes vista) : base(vista) {
            vista.ImportarInventarioVersat += OnImportarInventarioVersat;
            vista.ExportarDocumentoInventario += OnExportarDocumentoInventarioAlmacenes;

            RegistrarEntidad += OnRegistrarAlmacen;
            EditarEntidad += OnEditarAlmacen;

            AgregadorEventos.Suscribir("MostrarVistaGestionAlmacenes", OnMostrarVistaGestionAlmacenes);
        }

        private void OnRegistrarAlmacen(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("MostrarVistaRegistroAlmacen", string.Empty);
            Vista.MostrarBtnImportarInventarioVersat = false;
        }

        private void OnEditarAlmacen(object? sender, Almacen e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionAlmacen", AgregadorEventos.SerializarPayload(e));
            Vista.MostrarBtnImportarInventarioVersat = false;
        }

        private void OnMostrarVistaGestionAlmacenes(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaAlmacen.FiltroBusquedaAlmacen);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaAlmacen ObtenerValoresTupla(Almacen entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaAlmacen(new VistaTuplaAlmacen(), entidad);

            presentadorTupla.Vista.Id = entidad.Id.ToString();
            presentadorTupla.Vista.NombreAlmacen = entidad.Nombre;
            presentadorTupla.Vista.Tipo = entidad.Tipo.ToString();
            presentadorTupla.Vista.Direccion = entidad.Direccion ?? "No hay dirección disponible";
            presentadorTupla.Vista.CoordenadasGeograficas = entidad.Coordenadas ?? new CoordenadasGeograficas { Latitud = 0, Longitud = 0 };
            presentadorTupla.Vista.Estado = entidad.Estado;
            presentadorTupla.Vista.Descripcion = entidad.Descripcion ?? "No hay descripción disponible";
            presentadorTupla.Vista.MostrarBotonExportarProductos = _dispositivoConectado;
            presentadorTupla.Vista.ExportarDocumentoInventario += OnExportarDocumentoInventarioAlmacen;
            presentadorTupla.Vista.DescargarProductos += OnDescargarProductos;
            presentadorTupla.EntidadSeleccionada += CambiarVisibilidadBtnImportarInvntarioVersat;
            presentadorTupla.EntidadDeseleccionada += CambiarVisibilidadBtnImportarInvntarioVersat;

            return presentadorTupla;
        }

        public override void ActualizarResultadosBusqueda() {
            _dispositivoConectado = VerificarConexionDispositivo();

            base.ActualizarResultadosBusqueda();
        }

        private async void OnImportarInventarioVersat(object? sender, string rutaArchivo) {
            // Mostrar mensaje de advertencia antes de la importación
            if (CentroNotificaciones.MostrarMensaje(
                "La importación desde Excel actualizará los datos existentes y agregará nuevos productos si no existen. ¿Desea continuar?",
                TipoMensaje.Info,
                BotonesMensaje.ContinuarAbortar) != DialogResult.Yes) {
                return;
            }

            // Filtrar primero las tuplas seleccionadas para evitar procesamiento innecesario
            if (!TuplasSeleccionadas.Any()) {
                Vista.MostrarBtnImportarInventarioVersat = false;
                return;
            }

            var idAlmacen = TuplasSeleccionadas.FirstOrDefault()?.Entidad.Id ?? 0;

            if (idAlmacen != 0) {
                _cargaDatos.TextoProgreso = "Importando y actualizando inventario...";
                _cargaDatos.Mostrar();

                var resultado = await Task.Run(() => ImportarDesdeExcel(rutaArchivo, idAlmacen));

                if (resultado.exito) {
                    CentroNotificaciones.MostrarNotificacion($"Se ha importado el archivo correctamente. Se han actualizado {resultado.registrosProcesados} registros.");

                    Vista.MostrarBtnImportarInventarioVersat = false;
                    ActualizarResultadosBusqueda();
                } else {
                    CentroNotificaciones.MostrarNotificacion($"Error al importar el archivo: {resultado.mensaje}", TipoNotificacion.Error);
                }
            }
        }

        private void OnExportarDocumentoInventarioAlmacen(object? sender, (int id, FormatoDocumento formato) e) {
            _docInventarioAlmacen.GenerarDocumentoConParametros(e.formato, e.id);
        }

        private void OnExportarDocumentoInventarioAlmacenes(object? sender, FormatoDocumento e) {
            _docInventarioAlmacen.GenerarDocumento(true, e);
        }

        private void OnDescargarProductos(object? sender, EventArgs e) {
            var existeDirectorio = false;

            try {
                // Verificar conexión del dispositivo
                if (!VerificarConexionDispositivo()) {
                    CentroNotificaciones.MostrarNotificacion("Conecte un dispositivo Android con depuración USB activada", TipoNotificacion.Advertencia);
                } else {
                    existeDirectorio = _androidFileManager.EnsureDirectoryExists();

                    if (!existeDirectorio) {
                        CentroNotificaciones.MostrarNotificacion("No se pudo crear el directorio en el dispositivo Android", TipoNotificacion.Error);
                        return;
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            var id = sender as string;

            if (string.IsNullOrEmpty(id)) {
                CentroNotificaciones.MostrarNotificacion("ID del almacén no proporcionado", TipoNotificacion.Error);
                return;
            }

            var productos = RepoProducto.Instancia.ObtenerProductosAlmacenJson(long.Parse(id));
            var rutaArchivoProductos = Path.Combine(Application.StartupPath, "productos_almacen.json");

            using (var fileStream = new FileStream(rutaArchivoProductos, FileMode.Create))
            using (var writer = new StreamWriter(fileStream)) {
                writer.Write(productos);
            }

            if (_androidFileManager.PushFileToDevice(rutaArchivoProductos, "productos_almacen.json")) {
                CentroNotificaciones.MostrarNotificacion($"Productos del almacén {id} descargados correctamente", TipoNotificacion.Info);
            } else {
                CentroNotificaciones.MostrarNotificacion($"Error al descargar productos del almacén {id}", TipoNotificacion.Error);
            }

            // Limpiar archivo temporal
            try { File.Delete(rutaArchivoProductos); } catch { }
        }

        public (bool exito, string mensaje, int registrosProcesados) ImportarDesdeExcel(string rutaArchivo, long idAlmacen) {
            try {
                // 1. Leer archivo Excel con ClosedXML
                var datosInventario = LeerExcel(rutaArchivo);

                if (datosInventario == null || datosInventario.Rows.Count == 0)
                    return (false, "El archivo Excel no contiene datos válidos.", 0);

                // 2. Procesar datos
                try {
                    int registrosProcesados = 0;

                    var repoAlmacen = RepoAlmacen.Instancia;
                    var almacen = repoAlmacen.ObtenerPorId(idAlmacen);

                    foreach (DataRow fila in datosInventario.Rows) {
                        // Renombrar el almacén con el nombre del archivo
                        var infoArchivo = new FileInfo(rutaArchivo);

                        almacen.Nombre = infoArchivo.Name.Substring(0, infoArchivo.Name.IndexOf('.'));

                        repoAlmacen.Editar(almacen);

                        // Procesar cada fila del inventario
                        if (ProcesarFilaInventario(fila, almacen))
                            registrosProcesados++;
                    }

                    _cargaDatos.Ocultar();

                    // Obtener la lista de productos que no se encuentran en el Excel y eliminarlos completamente del sistema
                    var productosEnExcel = new HashSet<string>(datosInventario.AsEnumerable().Select(r => r["Código"]?.ToString() ?? "N/A").Where(c => !string.IsNullOrEmpty(c)));
                    var inventarioActual = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdAlmacen, idAlmacen.ToString()).resultadosBusqueda.Select(i => i.entidadBase);
                    var productosAEliminar = inventarioActual.Where(i => !productosEnExcel.Contains(RepoProducto.Instancia.ObtenerPorId(i.IdProducto)?.Codigo ?? "N/A")).ToList();

                    if (productosAEliminar.Count > 0) {
                        if (CentroNotificaciones.MostrarMensaje(
                            $"Se encontraron {productosAEliminar.Count} productos en el inventario actual que no están presentes en el archivo Excel. ¿Desea eliminarlos del sistema?",
                            TipoMensaje.Info,
                            BotonesMensaje.SiNo) == DialogResult.Yes) {
                            _cargaDatos.Mostrar();

                            foreach (var inventario in productosAEliminar) {
                                _cargaDatos.TextoProgreso = $"Eliminando el producto ID:{inventario.IdProducto}";
                                Thread.Sleep(100);

                                RepoProducto.Instancia.Eliminar(inventario.IdProducto);
                            }
                        }
                    }

                    return (true, $"Importación completada exitosamente. {registrosProcesados} registros procesados.", registrosProcesados);
                } catch (Exception ex) {
                    return (false, $"Error durante la importación: {ex.Message}", 0);
                }
            } catch (Exception ex) {
                return (false, $"Error general: {ex.Message}", 0);
            } finally {
                _cargaDatos.Ocultar();
            }
        }

        private bool ProcesarFilaInventario(DataRow fila, Almacen almacen) {
            var repoProducto = RepoProducto.Instancia;
            var repoInventario = RepoInventario.Instancia;

            try {
                // Extraer datos de la fila
                string codigo = fila["Código"]?.ToString();
                string descripcion = fila["Descripción"]?.ToString();
                string cantidad = fila["cantidad"]?.ToString();
                string precio = fila["Precio"]?.ToString();

                // Mostrar mensaje
                _cargaDatos.TextoProgreso = $"Procesando el producto COD:{codigo}";
                Thread.Sleep(100);

                if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.Equals(",")) {
                    cantidad = cantidad.Replace('.', ' ').Replace(',', '.');
                    precio = precio.Replace('.', ' ').Replace(',', '.');
                }

                if (!decimal.TryParse(cantidad, CultureInfo.InvariantCulture, out decimal cantidadDec))
                    cantidadDec = 0;

                if (!decimal.TryParse(precio, CultureInfo.InvariantCulture, out decimal precioDec))
                    precioDec = 0;

                // Obtener el producto 
                var productosPorCodigo = repoProducto.Buscar(FiltroBusquedaProducto.Codigo, codigo).resultadosBusqueda.Select(p => p.entidadBase).ToList();

                // Verificar que no existan productos con código duplicado, en cuyo caso se eliminarán todos
                // para registrar como nuevo el producto entrante.
                if (productosPorCodigo.Count > 1) {
                    productosPorCodigo.ForEach(p => {
                        repoProducto.Eliminar(p.Id);
                    });

                    productosPorCodigo.Clear();
                }

                var producto = productosPorCodigo.FirstOrDefault();

                // Verificar coincidencias para productos con el mismo nombre pero códigos diferentes y
                // eliminar los demás códigos de la base de datos
                var productosPorNombre = repoProducto.Buscar(FiltroBusquedaProducto.Nombre, descripcion).resultadosBusqueda.Select(p => p.entidadBase).ToList();

                if (productosPorNombre != null && productosPorNombre.Count() > 0)
                    productosPorNombre.FindAll(p => !p.Codigo?.Equals(codigo) ?? false).ForEach(p => {
                        repoProducto.Eliminar(p.Id);
                    });

                if (producto != null) {
                    // Editar nombre y precio del producto o costo unitario segun categoria
                    if (!string.IsNullOrEmpty(descripcion)) {
                        producto.Nombre = descripcion;
                    }
                    if (producto.Categoria == CategoriaProducto.ProductoTerminado)
                        producto.CostoProduccionUnitario = precioDec;
                    else producto.CostoAdquisicionUnitario = precioDec;

                    repoProducto.Editar(producto);

                    // Modificar datos de inventario del producto
                    var inventario = repoInventario.Buscar(FiltroBusquedaInventario.IdProducto, producto.Id.ToString()).resultadosBusqueda.FirstOrDefault(i => i.entidadBase.IdAlmacen.Equals(almacen.Id)).entidadBase;

                    if (inventario == null) {
                        // Crear nuevo registro de inventario si no existe
                        inventario = new Core.Modelos.Modulos.Inventario.Inventario {
                            IdAlmacen = almacen.Id,
                            IdProducto = producto.Id,
                            Cantidad = cantidadDec,
                            CostoPromedio = precioDec,
                            ValorTotal = precioDec * cantidadDec
                        };

                        repoInventario.Adicionar(inventario);

                        return true;
                    }

                    inventario.Cantidad = cantidadDec;
                    inventario.CostoPromedio = precioDec;
                    inventario.ValorTotal = precioDec * cantidadDec;

                    repoInventario.Editar(inventario);

                    return true;
                } else {
                    // Crear nuevo producto
                    producto = new Producto {
                        Categoria = CategoriaProducto.Mercancia,
                        Nombre = descripcion ?? "Producto sin descripción",
                        Codigo = codigo ?? CodigoHelper.GenerarEan13(descripcion),
                        Descripcion = descripcion ?? "Producto sin descripción",
                        IdProveedor = 0,
                        IdUnidadMedida = 1, // Unidad
                        EsVendible = true,
                        ImpuestoVentaPorcentaje = 10,
                        MargenGananciaDeseado = 25,
                        CostoAdquisicionUnitario = precioDec,
                        PrecioVentaBase = precioDec + (precioDec * 0.1m) + (precioDec * 0.25m),
                    };

                    producto.Id = repoProducto.Adicionar(producto);

                    // Crear registro de inventario
                    var inventario = new Core.Modelos.Modulos.Inventario.Inventario {
                        IdAlmacen = almacen.Id,
                        IdProducto = producto.Id,
                        Cantidad = cantidadDec,
                        CostoPromedio = precioDec,
                        ValorTotal = precioDec * cantidadDec
                    };

                    repoInventario.Adicionar(inventario);

                    return true;
                }
            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion($"Error al procesar fila: {ex.Message}", TipoNotificacion.Error);

                return false;
            }
        }

        private DataTable LeerExcel(string rutaArchivo) {
            try {
                // Usar ClosedXML para leer el archivo Excel
                using (var workbook = new XLWorkbook(rutaArchivo)) {
                    // Obtener la primera hoja de cálculo
                    var worksheet = workbook.Worksheet(1);

                    if (worksheet == null)
                        return null;

                    DataTable dt = new DataTable();

                    // Obtener el rango usado
                    var range = worksheet.RangeUsed();

                    // Leer encabezados (primera fila)
                    foreach (var cell in range.Row(1).Cells()) {
                        string nombreColumna = cell.GetString().Trim();
                        if (!string.IsNullOrEmpty(nombreColumna))
                            dt.Columns.Add(nombreColumna);
                    }

                    // Leer filas de datos (omitir la primera fila que son los encabezados)
                    foreach (var row in range.Rows().Skip(1)) {
                        DataRow dr = dt.NewRow();

                        for (int i = 0; i < dt.Columns.Count; i++) {
                            if (i < row.CellCount()) {
                                var cell = row.Cell(i + 1);
                                dr[i] = cell.GetString(); // GetString maneja valores nulos correctamente
                            }
                        }

                        dt.Rows.Add(dr);
                    }

                    return dt;
                }
            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion($"Error al leer el archivo Excel: {ex.Message}", TipoNotificacion.Error);
                return null;
            }
        }

        public bool VerificarConexionDispositivo() {
            var conexionOk = true;

            try {
                // Verificar conexión del dispositivo
                if (!_androidFileManager.CheckDeviceConnection())
                    conexionOk = false;
            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion($"Error al verificar conexión del dispositivo: {ex.Message}", TipoNotificacion.Error);
            }

            return conexionOk;
        }

        private void CambiarVisibilidadBtnImportarInvntarioVersat(object? sender, Almacen e) {
            Vista.MostrarBtnImportarInventarioVersat = _tuplasEntidades.Any(t => t.EstadoSeleccion);
        }
    }
}