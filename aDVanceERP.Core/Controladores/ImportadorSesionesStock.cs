using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;

using System.Text.Json;

namespace aDVanceERP.Core.Controladores {

    /// <summary>
    /// Procesa los archivos JSON de sesión descargados desde aDVance Stock Mobile
    /// y actualiza el inventario del ERP.
    ///
    /// Responsabilidades:
    ///   - Deserializar los JSON de sesión (<see cref="SesionStockJson"/>)
    ///   - Crear productos nuevos registrados en campo si no existen en BD
    ///   - Actualizar stock existente o crear nuevo registro de inventario
    ///   - Registrar el movimiento correspondiente (Compra o Carga Inicial)
    ///   - Archivar los JSON procesados en subcarpeta "procesados"
    ///   - Publicar eventos "NuevoProductoRegistrado" y "SesionesStockImportadas"
    ///
    /// El presenter solo construye el importador, llama a Procesar()
    /// y muestra el resumen devuelto al usuario.
    /// </summary>
    public class ImportadorSesionesStock {

        // ══════════════════════════════════════════════════════
        //  RESULTADO
        // ══════════════════════════════════════════════════════

        /// <summary>Resumen de la operación de importación.</summary>
        public record ResultadoImportacion(
            int          ProductosActualizados,
            int          ProductosNuevos,
            List<string> Errores);

        // ══════════════════════════════════════════════════════
        //  PUNTO DE ENTRADA
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Procesa una colección de archivos JSON de sesión descargados del dispositivo.
        /// </summary>
        /// <param name="archivosJson">Rutas locales de los archivos stock_*.json.</param>
        /// <param name="carpetaImportacion">
        ///   Carpeta base de importación. Los archivos procesados se mueven a
        ///   <c>{carpetaImportacion}/procesados/</c>.
        /// </param>
        public ResultadoImportacion Procesar(
            IEnumerable<string> archivosJson,
            string              carpetaImportacion) {

            int productosActualizados = 0;
            int productosNuevos       = 0;
            var errores               = new List<string>();

            var repoProducto   = RepoProducto.Instancia;
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
                                errores.Add($"'{Path.GetFileName(archivo)}' " +
                                            $"— producto Id={item.IdProducto} no encontrado.");
                                continue;
                            }

                            // Producto nuevo registrado en campo → crear
                            if (producto == null) {
                                producto = new Producto {
                                    Id                       = 0,
                                    Codigo                   = item.Codigo
                                                               ?? $"STOCK-{DateTime.Now.Ticks}",
                                    Nombre                   = item.Nombre ?? "Producto sin nombre",
                                    Categoria                = CategoriaProductoEnum.MateriaPrima,
                                    IdClasificacionProducto  = item.IdClasificacion,
                                    IdUnidadMedida           = item.IdUnidadMedida,
                                    CostoAdquisicionUnitario = item.CostoUnitario,
                                    CostoProduccionUnitario  = 0,
                                    PrecioVentaBase          = item.CostoUnitario,
                                    Activo                   = true
                                };
                                producto.Id = repoProducto.Adicionar(producto);
                                productosNuevos++;

                                AgregadorEventos.Publicar("NuevoProductoRegistrado", string.Empty);
                            }

                            // ── Actualizar inventario ──────────────────────────

                            var inventarioExistente = repoInventario
                                .Buscar(FiltroBusquedaInventario.IdProducto,
                                        producto.Id.ToString())
                                .resultadosBusqueda
                                .FirstOrDefault(r => r.entidadBase.IdAlmacen == item.IdAlmacen)
                                .entidadBase;

                            if (inventarioExistente != null) {
                                // Stock ya existe: entrada de compra
                                repoMovimiento.Adicionar(new Movimiento {
                                    Id               = 0,
                                    IdProducto       = producto.Id,
                                    CostoUnitario    = item.CostoUnitario,
                                    IdAlmacenOrigen  = 0,
                                    IdAlmacenDestino = item.IdAlmacen,
                                    Estado           = EstadoMovimientoEnum.Completado,
                                    FechaCreacion    = sesion.FechaSesion,
                                    SaldoInicial     = inventarioExistente.Cantidad,
                                    FechaTermino     = sesion.FechaSesion,
                                    CantidadMovida   = item.CantidadRegistrada,
                                    SaldoFinal       = inventarioExistente.Cantidad
                                                       + item.CantidadRegistrada,
                                    IdTipoMovimiento = idTipoMovimientoCompra,
                                    IdCuentaUsuario  = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                    Notas            = $"Compra desde sesión aDVance.STOCK" +
                                                       $" — {sesion.NombreSesion}."
                                });

                                inventarioExistente.Cantidad = item.CantidadRegistrada;
                                repoInventario.Editar(inventarioExistente);

                            } else {
                                // Stock no existe: carga inicial
                                repoMovimiento.Adicionar(new Movimiento {
                                    Id               = 0,
                                    IdProducto       = producto.Id,
                                    CostoUnitario    = item.CostoUnitario,
                                    IdAlmacenOrigen  = 0,
                                    IdAlmacenDestino = item.IdAlmacen,
                                    Estado           = EstadoMovimientoEnum.Completado,
                                    FechaCreacion    = sesion.FechaSesion,
                                    SaldoInicial     = 0,
                                    FechaTermino     = sesion.FechaSesion,
                                    CantidadMovida   = item.CantidadRegistrada,
                                    SaldoFinal       = item.CantidadRegistrada,
                                    IdTipoMovimiento = idTipoMovimientoCargaInicial,
                                    IdCuentaUsuario  = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                    Notas            = $"Carga inicial desde sesión aDVance.STOCK" +
                                                       $" — {sesion.NombreSesion}."
                                });

                                repoInventario.Adicionar(new Inventario {
                                    Id                  = 0,
                                    IdProducto          = producto.Id,
                                    IdAlmacen           = item.IdAlmacen,
                                    Cantidad            = item.CantidadRegistrada,
                                    CostoPromedio       = item.CostoUnitario,
                                    ValorTotal          = item.CostoUnitario * item.CantidadRegistrada,
                                    UltimaActualizacion = DateTime.Now
                                });
                            }

                            productosActualizados++;

                        } catch (Exception itemEx) {
                            errores.Add($"'{Path.GetFileName(archivo)}' " +
                                        $"— item {item.Codigo ?? item.IdProducto.ToString()}: " +
                                        $"{itemEx.Message}");
                        }
                    }

                    // ── Archivar el JSON procesado ─────────────────────────────
                    try {
                        string carpetaProcesados = Path.Combine(carpetaImportacion, "procesados");
                        Directory.CreateDirectory(carpetaProcesados);
                        File.Move(
                            archivo,
                            Path.Combine(carpetaProcesados, Path.GetFileName(archivo)),
                            overwrite: true);
                    } catch { /* no crítico */ }

                } catch (Exception archivoEx) {
                    errores.Add($"Error procesando '{Path.GetFileName(archivo)}': " +
                                $"{archivoEx.Message}");
                }
            }

            if (productosActualizados > 0)
                AgregadorEventos.Publicar("SesionesStockImportadas", string.Empty);

            return new ResultadoImportacion(
                productosActualizados,
                productosNuevos,
                errores);
        }
    }
}
