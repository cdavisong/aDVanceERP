using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Infraestructura.Extensiones.BD;
using aDVanceERP.Core.Infraestructura.Globales;

using ClosedXML.Excel;

using MySql.Data.MySqlClient;

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

using System.Data;
using System.Diagnostics;
using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Documentos {
    public class DocInventarioAlmacen : DocumentoBase {
        private int? _idAlmacenEspecifico;
        private Dictionary<int, string> _almacenes;
        private Dictionary<int, List<Dictionary<string, string>>> _inventario;

        #region Constructor

        public DocInventarioAlmacen(int? idAlmacen = null) {
            _idAlmacenEspecifico = idAlmacen;

            // Configurar información de la empresa
            CargarInformacionEmpresa();

            // Intentar cargar el logo (ajusta la ruta según tu aplicación)
            string rutaLogo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo.png");
            if (File.Exists(rutaLogo)) {
                CargarLogo(rutaLogo);
            }
        }

        #endregion

        #region Implementación de IGeneradorDocumento

        public void GenerarDocumento(bool mostrar = true, FormatoDocumento formato = FormatoDocumento.PDF) {
            if (formato == FormatoDocumento.Excel) {
                GenerarDocumentoExcel(mostrar);
            } else {
                GenerarDocumentoPDF(mostrar);
            }
        }

        public void GenerarDocumentoConParametros(FormatoDocumento formato, params object[] parametros) {
            int? idAlmacen = null;

            if (parametros.Length > 0 && parametros[0] is int id) {
                idAlmacen = id;
            }

            _idAlmacenEspecifico = idAlmacen;
            GenerarDocumento(true, formato);
        }

        #endregion

        #region Override de Método Abstracto

        public override void GenerarDocumento(bool mostrar = true) {
            GenerarDocumentoPDF(mostrar);
        }

        #endregion

        #region Generación de PDF

        private void GenerarDocumentoPDF(bool mostrar) {
            try {
                // Cargar datos
                _almacenes = _idAlmacenEspecifico.HasValue
                    ? ObtenerAlmacenEspecifico(_idAlmacenEspecifico.Value)
                    : ObtenerAlmacenes();

                _inventario = ObtenerInventarioPorAlmacenes(_idAlmacenEspecifico);

                if (_almacenes.Count == 0 || _inventario.Count == 0) {
                    CentroNotificaciones.MostrarNotificacion(
                        "No se encontraron datos para el reporte.",
                        Core.Modelos.Comun.TipoNotificacion.Advertencia);
                    return;
                }

                // Crear documento PDF
                var documento = new PdfDocument();
                documento.Info.Title = _idAlmacenEspecifico.HasValue
                    ? $"Inventario - {_almacenes.First().Value}"
                    : "Inventario por Almacenes";
                documento.Info.Author = NombreEmpresa;
                documento.Info.Creator = "aDVance ERP";

                int numeroPagina = 1;
                PdfPage paginaActual = null;
                XGraphics gfx = null;
                double yPos = 0;

                // Procesar cada almacén
                foreach (var almacen in _almacenes) {
                    var productos = _inventario.ContainsKey(almacen.Key)
                        ? _inventario[almacen.Key]
                        : new List<Dictionary<string, string>>();

                    if (productos.Count == 0) continue;

                    // Crear nueva página para cada almacén
                    paginaActual = documento.AddPage();
                    paginaActual.Size = PageSize.Letter;
                    gfx = XGraphics.FromPdfPage(paginaActual);

                    // Dibujar banner profesional
                    DibujarBannerProfesional(
                        gfx,
                        paginaActual,
                        "INVENTARIO POR ALMACÉN",
                        $"ALM-{almacen.Key:D4}",
                        DateTime.Now
                    );

                    // CORRECCIÓN: Iniciar después del banner con más espacio
                    yPos = ObtenerInicioPosicionContenido();

                    // Información del almacén en cuadro destacado
                    var infoAlmacen = new Dictionary<string, string> {
                        ["Almacén"] = almacen.Value.ToUpper(),
                        ["Total Productos"] = productos.Count.ToString("N0"),
                        ["Fecha Reporte"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                    };

                    DibujarCuadroInformacion(gfx, MargenIzquierdo, yPos,
                        gfx.PageSize.Width - MargenIzquierdo - MargenDerecho,
                        "INFORMACIÓN DEL ALMACÉN", infoAlmacen);

                    // CORRECCIÓN: Espacio adecuado después del cuadro de información
                    yPos += 95;

                    // CORRECCIÓN: Calcular anchos de columna proporcionalmente al ancho disponible
                    double anchoDisponible = gfx.PageSize.Width - MargenIzquierdo - MargenDerecho;
                    var columnas = new Dictionary<string, double> {
                        ["Código"] = anchoDisponible * 0.15,      // 15%
                        ["Producto"] = anchoDisponible * 0.25,    // 25%
                        ["UM"] = anchoDisponible * 0.08,          // 8%
                        ["Stock"] = anchoDisponible * 0.10,       // 10%
                        ["Costo"] = anchoDisponible * 0.12,       // 12%
                        ["P. Venta"] = anchoDisponible * 0.12,    // 12%
                        ["Categoría"] = anchoDisponible * 0.18    // 18%
                    };

                    // Dibujar encabezado de tabla
                    DibujarEncabezadoTabla(gfx, yPos, columnas);
                    yPos += 25;

                    // Variables para totales
                    int totalProductos = productos.Count;
                    decimal valorTotalCosto = 0;
                    decimal valorTotalVenta = 0;
                    int numeroFila = 0;

                    // Dibujar productos
                    foreach (var producto in productos) {
                        // Verificar si se necesita nueva página
                        if (NecesitaNuevaPagina(yPos, 20, gfx.PageSize.Height)) {
                            // Pie de página actual
                            DibujarPiePagina(gfx, paginaActual, numeroPagina,
                                documento.PageCount + 1,
                                $"Almacén: {almacen.Value}");

                            // Nueva página
                            paginaActual = documento.AddPage();
                            paginaActual.Size = PageSize.Letter;
                            gfx = XGraphics.FromPdfPage(paginaActual);
                            numeroPagina++;

                            // Dibujar banner y encabezado en nueva página
                            DibujarBannerProfesional(
                                gfx,
                                paginaActual,
                                "INVENTARIO POR ALMACÉN",
                                $"ALM-{almacen.Key:D4}",
                                DateTime.Now
                            );

                            yPos = ObtenerInicioPosicionContenido() + 10;
                            DibujarEncabezadoTabla(gfx, yPos, columnas);
                            yPos += 25;
                        }

                        // Parsear valores
                        var cantidad = decimal.Parse(producto["cantidad"], CultureInfo.InvariantCulture);
                        var precioCosto = decimal.Parse(producto["precio_costo"], CultureInfo.InvariantCulture);
                        var precioVenta = decimal.Parse(producto["precio_venta"], CultureInfo.InvariantCulture);

                        // Calcular totales
                        valorTotalCosto += cantidad * precioCosto;
                        valorTotalVenta += cantidad * precioVenta;

                        // CORRECCIÓN: Acortar categoría para que quepa
                        string categoriaTexto = producto["categoria"] switch {
                            "MateriaPrima" => "M. Prima",
                            "ProductoTerminado" => "P. Terminado",
                            "Mercancia" => "Mercancía",
                            _ => producto["categoria"]
                        };

                        // CORRECCIÓN: Truncar texto largo si es necesario
                        string nombreProducto = TruncarTexto(producto["nombre"], FontContenido, gfx, columnas["Producto"] - 10);

                        // Dibujar fila
                        var datosColumnas = new Dictionary<string, (double, string, XStringFormat)> {
                            ["Código"] = (columnas["Código"], producto["codigo"], XStringFormats.CenterLeft),
                            ["Producto"] = (columnas["Producto"], nombreProducto, XStringFormats.CenterLeft),
                            ["UM"] = (columnas["UM"], producto["unidad_medida"], XStringFormats.Center),
                            ["Stock"] = (columnas["Stock"], cantidad.ToString("N2"), XStringFormats.CenterRight),
                            ["Costo"] = (columnas["Costo"], "$" + precioCosto.ToString("N2"), XStringFormats.CenterRight),
                            ["P. Venta"] = (columnas["P. Venta"], "$" + precioVenta.ToString("N2"), XStringFormats.CenterRight),
                            ["Categoría"] = (columnas["Categoría"], categoriaTexto, XStringFormats.CenterLeft)
                        };

                        DibujarFilaTabla(gfx, yPos, datosColumnas, numeroFila % 2 == 0);
                        yPos += 18;
                        numeroFila++;
                    }

                    // CORRECCIÓN: Más espacio antes de los totales
                    yPos += 15;

                    // Dibujar totales
                    var totales = new Dictionary<string, string> {
                        ["Total Productos"] = totalProductos.ToString("N0"),
                        ["Valor Total Costo"] = "$" + valorTotalCosto.ToString("N2"),
                        ["Valor Total Venta"] = "$" + valorTotalVenta.ToString("N2"),
                        ["Margen Potencial"] = "$" + (valorTotalVenta - valorTotalCosto).ToString("N2")
                    };

                    DibujarSeccionTotales(gfx, yPos, totales, 300);

                    // Pie de página
                    DibujarPiePagina(gfx, paginaActual, numeroPagina,
                        documento.PageCount, $"Almacén: {almacen.Value}");
                }

                // Guardar documento
                string rutaDocumento = Path.Combine(
                    Path.GetTempPath(),
                    $"Inventario_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                );

                documento.Save(rutaDocumento);

                if (mostrar) {
                    Process.Start(new ProcessStartInfo {
                        FileName = rutaDocumento,
                        UseShellExecute = true
                    });
                }

                CentroNotificaciones.MostrarNotificacion(
                    "Documento PDF generado exitosamente.",
                    Core.Modelos.Comun.TipoNotificacion.Info
                );

            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error al generar el documento: {ex.Message}",
                    Core.Modelos.Comun.TipoNotificacion.Error
                );
            }
        }

        #endregion

        #region Generación de Excel

        private void GenerarDocumentoExcel(bool mostrar) {
            try {
                // Cargar datos
                _almacenes = _idAlmacenEspecifico.HasValue
                    ? ObtenerAlmacenEspecifico(_idAlmacenEspecifico.Value)
                    : ObtenerAlmacenes();

                _inventario = ObtenerInventarioPorAlmacenes(_idAlmacenEspecifico);

                if (_almacenes.Count == 0 || _inventario.Count == 0) {
                    CentroNotificaciones.MostrarNotificacion(
                        "No se encontraron datos para el reporte.",
                        Core.Modelos.Comun.TipoNotificacion.Advertencia);
                    return;
                }

                var workbook = new XLWorkbook();

                foreach (var almacen in _almacenes) {
                    var productos = _inventario.ContainsKey(almacen.Key)
                        ? _inventario[almacen.Key]
                        : new List<Dictionary<string, string>>();

                    if (productos.Count == 0) continue;

                    var worksheet = workbook.Worksheets.Add($"ALM-{almacen.Key:D4}");

                    // Encabezado
                    worksheet.Cell(1, 1).Value = "INVENTARIO POR ALMACÉN";
                    worksheet.Range(1, 1, 1, 7).Merge().Style.Font.Bold = true;
                    worksheet.Range(1, 1, 1, 7).Style.Font.FontSize = 14;
                    worksheet.Range(1, 1, 1, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    // Información del almacén
                    worksheet.Cell(3, 1).Value = "Almacén:";
                    worksheet.Cell(3, 2).Value = almacen.Value;
                    worksheet.Cell(4, 1).Value = "Fecha:";
                    worksheet.Cell(4, 2).Value = DateTime.Now;
                    worksheet.Cell(4, 2).Style.DateFormat.Format = "dd/MM/yyyy HH:mm:ss";

                    // Encabezados de tabla
                    int headerRow = 6;
                    worksheet.Cell(headerRow, 1).Value = "Código";
                    worksheet.Cell(headerRow, 2).Value = "Producto";
                    worksheet.Cell(headerRow, 3).Value = "UM";
                    worksheet.Cell(headerRow, 4).Value = "Stock";
                    worksheet.Cell(headerRow, 5).Value = "Costo";
                    worksheet.Cell(headerRow, 6).Value = "P. Venta";
                    worksheet.Cell(headerRow, 7).Value = "Categoría";

                    var rangoEncabezado = worksheet.Range(headerRow, 1, headerRow, 7);
                    rangoEncabezado.Style.Font.Bold = true;
                    rangoEncabezado.Style.Fill.BackgroundColor = XLColor.LightGray;
                    rangoEncabezado.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                    // Datos
                    int currentRow = headerRow + 1;
                    decimal valorTotalCosto = 0;
                    decimal valorTotalVenta = 0;

                    foreach (var producto in productos) {
                        var cantidad = decimal.Parse(producto["cantidad"], CultureInfo.InvariantCulture);
                        var precioCosto = decimal.Parse(producto["precio_costo"], CultureInfo.InvariantCulture);
                        var precioVenta = decimal.Parse(producto["precio_venta"], CultureInfo.InvariantCulture);

                        valorTotalCosto += cantidad * precioCosto;
                        valorTotalVenta += cantidad * precioVenta;

                        string categoriaTexto = producto["categoria"] switch {
                            "MateriaPrima" => "Materia Prima",
                            "ProductoTerminado" => "Producto Terminado",
                            "Mercancia" => "Mercancía",
                            _ => producto["categoria"]
                        };

                        worksheet.Cell(currentRow, 1).Value = producto["codigo"];
                        worksheet.Cell(currentRow, 2).Value = producto["nombre"];
                        worksheet.Cell(currentRow, 3).Value = producto["unidad_medida"];
                        worksheet.Cell(currentRow, 4).Value = cantidad;
                        worksheet.Cell(currentRow, 5).Value = precioCosto;
                        worksheet.Cell(currentRow, 6).Value = precioVenta;
                        worksheet.Cell(currentRow, 7).Value = categoriaTexto;

                        // Formato de números
                        worksheet.Cell(currentRow, 4).Style.NumberFormat.NumberFormatId = 4;
                        worksheet.Cell(currentRow, 5).Style.NumberFormat.NumberFormatId = 4;
                        worksheet.Cell(currentRow, 6).Style.NumberFormat.NumberFormatId = 4;

                        currentRow++;
                    }

                    // Fila de totales
                    worksheet.Cell(currentRow, 1).Value = "TOTAL ALMACÉN:";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = productos.Count;
                    worksheet.Cell(currentRow, 5).Value = valorTotalCosto;
                    worksheet.Cell(currentRow, 6).Value = valorTotalVenta;

                    var rangoTotales = worksheet.Range(currentRow, 1, currentRow, 7);
                    rangoTotales.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    rangoTotales.Style.Border.BottomBorder = XLBorderStyleValues.Double;
                    rangoTotales.Style.Font.Bold = true;

                    currentRow += 2;
                }

                // Guardar archivo
                string rutaDocumento = Path.Combine(
                    Path.GetTempPath(),
                    $"Inventario_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                );

                workbook.SaveAs(rutaDocumento);

                if (mostrar) {
                    Process.Start(new ProcessStartInfo {
                        FileName = rutaDocumento,
                        UseShellExecute = true
                    });
                }

                CentroNotificaciones.MostrarNotificacion(
                    "Documento Excel generado exitosamente.",
                    Core.Modelos.Comun.TipoNotificacion.Info
                );

            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error al generar el documento: {ex.Message}",
                    Core.Modelos.Comun.TipoNotificacion.Error
                );
            }
        }

        #endregion

        #region Métodos auxiliares

        /// <summary>
        /// Trunca texto para que quepa en el ancho especificado
        /// </summary>
        private string TruncarTexto(string texto, XFont fuente, XGraphics gfx, double anchoMaximo) {
            var tamano = gfx.MeasureString(texto, fuente);

            if (tamano.Width <= anchoMaximo) {
                return texto;
            }

            // Ir quitando caracteres hasta que quepa
            string textoTruncado = texto;
            while (textoTruncado.Length > 0) {
                textoTruncado = textoTruncado.Substring(0, textoTruncado.Length - 1);
                var nuevoTamano = gfx.MeasureString(textoTruncado + "...", fuente);
                if (nuevoTamano.Width <= anchoMaximo) {
                    return textoTruncado + "...";
                }
            }

            return "...";
        }

        #endregion

        #region Métodos para obtener datos de BD

        protected override void CargarInformacionEmpresa() {
            // Implementa la lógica para cargar desde tu BD
            // Por ahora, datos de ejemplo
            ConfigurarEmpresa(
                nombre: "aDVance ERP",
                direccion: "Tu dirección comercial",
                telefono: "+58 (XXX) XXX-XXXX",
                email: "info@advanceerp.com",
                web: "www.advanceerp.com",
                rif: "J-XXXXXXXXX-X"
            );
        }

        private static Dictionary<int, string> ObtenerAlmacenes() {
            var almacenes = new Dictionary<int, string>();

            using (var connection = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                connection.Open();
                var query = "SELECT id_almacen, nombre FROM adv__almacen ORDER BY nombre";

                using (var command = new MySqlCommand(query, connection)) {
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            almacenes[reader.GetInt32("id_almacen")] = reader.GetString("nombre");
                        }
                    }
                }
            }

            return almacenes;
        }

        private static Dictionary<int, string> ObtenerAlmacenEspecifico(int idAlmacen) {
            var almacenes = new Dictionary<int, string>();

            using (var connection = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                connection.Open();
                var query = "SELECT id_almacen, nombre FROM adv__almacen WHERE id_almacen = @idAlmacen";

                using (var command = new MySqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@idAlmacen", idAlmacen);

                    using (var reader = command.ExecuteReader()) {
                        if (reader.Read()) {
                            almacenes[reader.GetInt32("id_almacen")] = reader.GetString("nombre");
                        }
                    }
                }
            }

            return almacenes;
        }

        private static Dictionary<int, List<Dictionary<string, string>>> ObtenerInventarioPorAlmacenes(int? idAlmacenEspecifico = null) {
            var inventario = new Dictionary<int, List<Dictionary<string, string>>>();

            using (var connection = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                connection.Open();

                var query = """
                SELECT 
                    pa.id_almacen,
                    p.id_producto,
                    p.codigo,
                    p.nombre,
                    p.categoria,
                    CASE 
                        WHEN p.categoria = 'ProductoTerminado' THEN p.costo_produccion_unitario
                        ELSE p.costo_adquisicion_unitario
                    END as precio_costo,
                    p.precio_venta_base as precio_venta,
                    COALESCE(pa.cantidad, 0) as cantidad,
                    COALESCE(um.abreviatura, 'N/A') as unidad_medida
                FROM adv__producto p
                LEFT JOIN adv__inventario pa ON p.id_producto = pa.id_producto
                LEFT JOIN adv__unidad_medida um ON p.id_unidad_medida = um.id_unidad_medida
                WHERE 1=1
                """;

                if (idAlmacenEspecifico.HasValue) {
                    query += " AND pa.id_almacen = @idAlmacen";
                }

                query += " ORDER BY pa.id_almacen, p.nombre";

                using (var command = new MySqlCommand(query, connection)) {
                    if (idAlmacenEspecifico.HasValue) {
                        command.Parameters.AddWithValue("@idAlmacen", idAlmacenEspecifico.Value);
                    }

                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            int idAlmacen = reader.IsDBNull("id_almacen") ? 0 : reader.GetInt32("id_almacen");

                            if (!inventario.ContainsKey(idAlmacen)) {
                                inventario[idAlmacen] = new List<Dictionary<string, string>>();
                            }

                            var producto = new Dictionary<string, string> {
                                ["codigo"] = reader["codigo"].ToString(),
                                ["nombre"] = reader["nombre"].ToString(),
                                ["categoria"] = reader["categoria"].ToString(),
                                ["precio_costo"] = reader.IsDBNull("precio_costo") ? "0.00" :
                                    reader.GetDecimal("precio_costo").ToString("N2", CultureInfo.InvariantCulture),
                                ["precio_venta"] = reader.IsDBNull("precio_venta") ? "0.00" :
                                    reader.GetDecimal("precio_venta").ToString("N2", CultureInfo.InvariantCulture),
                                ["cantidad"] = reader.IsDBNull("cantidad") ? "0.00" :
                                    reader.GetDecimal("cantidad").ToString("N2", CultureInfo.InvariantCulture),
                                ["unidad_medida"] = reader["unidad_medida"].ToString()
                            };

                            inventario[idAlmacen].Add(producto);
                        }
                    }
                }
            }

            return inventario;
        }

        #endregion
    }
}