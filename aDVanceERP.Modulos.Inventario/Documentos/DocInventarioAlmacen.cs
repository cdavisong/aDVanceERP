using aDVanceERP.Core.Documentos.Interfaces;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp;

using ClosedXML.Excel;

using System.Diagnostics;
using System.Globalization;
using System.Data;
using aDVanceERP.Core.Infraestructura.Extensiones.BD;

namespace aDVanceERP.Modulos.Inventario.Documentos {
    public class DocInventarioAlmacen : IGeneradorDocumento {
        private int? _idAlmacenEspecifico;

        public DocInventarioAlmacen(int? idAlmacen = null) {
            _idAlmacenEspecifico = idAlmacen;
        }

        public void GenerarDocumento(bool mostrar = true, FormatoDocumento formato = FormatoDocumento.PDF) {
            if (formato == FormatoDocumento.Excel) {
                GenerarDocumentoInventarioAlmacenesExcel(mostrar, _idAlmacenEspecifico);
            }
            else {
                GenerarDocumentoInventarioAlmacenesPdf(mostrar, _idAlmacenEspecifico);
            }
        }

        public void GenerarDocumentoConParametros(FormatoDocumento formato, params object[] parametros) {
            int? idAlmacen = null;

            if (parametros.Length > 0 && parametros[0] is int id) {
                idAlmacen = id;
            }

            if (formato == FormatoDocumento.Excel) {
                GenerarDocumentoInventarioAlmacenesExcel(true, idAlmacen);
            }
            else {
                GenerarDocumentoInventarioAlmacenesPdf(true, idAlmacen);
            }
        }

        public static void GenerarDocumentoInventarioAlmacenesExcel(bool mostrar = true, int? idAlmacenEspecifico = null) {
            // Obtener datos de la base de datos  
            var almacenes = idAlmacenEspecifico.HasValue
                ? ObtenerAlmacenEspecifico(idAlmacenEspecifico.Value)
                : ObtenerAlmacenes();

            var inventario = ObtenerInventarioPorAlmacenes(idAlmacenEspecifico);

            // Si no hay datos, salir
            if (almacenes.Count == 0 || inventario.Count == 0) {
                CentroNotificaciones.Mostrar("No se encontraron datos para el reporte.", Core.Modelos.Comun.TipoNotificacion.Advertencia);
                return;
            }

            // Crear un nuevo libro de trabajo
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Inventario");

            // Configurar cultura para números (usar CultureInfo.InvariantCulture)
            var culture = CultureInfo.InvariantCulture;

            // Configurar anchos de columna FIJOS
            worksheet.Column(1).Width = 22;  // Código
            worksheet.Column(2).Width = 40;  // Producto
            worksheet.Column(3).Width = 8;   // UM
            worksheet.Column(4).Width = 12;  // Stock
            worksheet.Column(5).Width = 15;  // Costo
            worksheet.Column(6).Width = 15;  // P. Venta
            worksheet.Column(7).Width = 18;  // Categoría

            // Configurar estilos
            var titleStyle = workbook.Style;
            titleStyle.Font.FontSize = 14;
            titleStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var headerStyle = workbook.Style;
            headerStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerStyle.Border.BottomBorder = XLBorderStyleValues.Thin;

            // IMPORTANTE: Usar formato de número invariante para evitar problemas culturales
            var numberStyle = workbook.Style;
            numberStyle.NumberFormat.NumberFormatId = 4; // Formato numérico estándar
            numberStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            var textStyle = workbook.Style;
            textStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            var totalStyle = workbook.Style;
            totalStyle.Border.TopBorder = XLBorderStyleValues.Thin;
            totalStyle.Border.BottomBorder = XLBorderStyleValues.Double;

            // Escribir título
            string titulo = idAlmacenEspecifico.HasValue
                ? $"INVENTARIO POR ALMACÉN - {almacenes.First().Value.ToUpper()}"
                : "INVENTARIO POR ALMACENES";

            worksheet.Cell(1, 1).Value = titulo;
            worksheet.Range(1, 1, 1, 7).Merge().Style = titleStyle;

            // Escribir fecha
            worksheet.Cell(2, 1).Value = $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
            worksheet.Range(2, 1, 2, 7).Merge().Style = textStyle;

            int currentRow = 4;

            foreach (var almacen in almacenes) {
                var productosEnAlmacen = inventario.ContainsKey(almacen.Key) ? inventario[almacen.Key] : new List<Dictionary<string, string>>();

                if (productosEnAlmacen.Count == 0)
                    continue;

                // Escribir encabezado de almacén
                worksheet.Cell(currentRow, 1).Value = $"ALMACÉN: {almacen.Value.ToUpper()}";
                worksheet.Range(currentRow, 1, currentRow, 7).Merge().Style = headerStyle;
                currentRow++;

                // Escribir encabezados de columna
                worksheet.Cell(currentRow, 1).Value = "Código";
                worksheet.Cell(currentRow, 2).Value = "Producto";
                worksheet.Cell(currentRow, 3).Value = "UM";
                worksheet.Cell(currentRow, 4).Value = "Stock";
                worksheet.Cell(currentRow, 5).Value = "Costo";
                worksheet.Cell(currentRow, 6).Value = "P. Venta";
                worksheet.Cell(currentRow, 7).Value = "Categoría";

                // Aplicar estilo de encabezado a todas las celdas
                worksheet.Range(currentRow, 1, currentRow, 7).Style = headerStyle;

                currentRow++;

                // Escribir productos
                foreach (var producto in productosEnAlmacen) {
                    worksheet.Cell(currentRow, 1).Value = producto["codigo"];
                    worksheet.Cell(currentRow, 1).Style = textStyle;

                    worksheet.Cell(currentRow, 2).Value = producto["nombre"];
                    worksheet.Cell(currentRow, 2).Style = textStyle;

                    worksheet.Cell(currentRow, 3).Value = producto["unidad_medida"];
                    worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    // IMPORTANTE: Parsear usando CultureInfo.InvariantCulture
                    if (decimal.TryParse(producto["cantidad"], NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad)) {
                        worksheet.Cell(currentRow, 4).Value = cantidad;
                        worksheet.Cell(currentRow, 4).Style = numberStyle;
                    }

                    if (decimal.TryParse(producto["precio_costo"], NumberStyles.Any, CultureInfo.InvariantCulture, out var precioCosto)) {
                        worksheet.Cell(currentRow, 5).Value = precioCosto;
                        worksheet.Cell(currentRow, 5).Style = numberStyle;
                    }

                    if (decimal.TryParse(producto["precio_venta"], NumberStyles.Any, CultureInfo.InvariantCulture, out var precioVenta)) {
                        worksheet.Cell(currentRow, 6).Value = precioVenta;
                        worksheet.Cell(currentRow, 6).Style = numberStyle;
                    }

                    string categoria = producto["categoria"] switch {
                        "ProductoTerminado" => "PROD. TERMINADO",
                        "MateriaPrima" => "MATERIA PRIMA",
                        _ => "MERCANCÍA"
                    };
                    worksheet.Cell(currentRow, 7).Value = categoria;
                    worksheet.Cell(currentRow, 7).Style = textStyle;

                    currentRow++;
                }

                // Calcular y escribir totales del almacén
                int totalProductos = productosEnAlmacen.Count;
                decimal valorTotalCosto = productosEnAlmacen.Sum(p =>
                {
                    if (decimal.TryParse(p["cantidad"], NumberStyles.Any, CultureInfo.InvariantCulture, out var cant) &&
                        decimal.TryParse(p["precio_costo"], NumberStyles.Any, CultureInfo.InvariantCulture, out var costo)) {
                        return cant * costo;
                    }
                    return 0;
                });

                worksheet.Cell(currentRow, 1).Value = "TOTAL ALMACÉN:";
                worksheet.Range(currentRow, 1, currentRow, 3).Merge().Style = totalStyle;

                worksheet.Cell(currentRow, 4).Value = totalProductos;
                worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 5).Value = valorTotalCosto;
                worksheet.Cell(currentRow, 5).Style = numberStyle;
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;

                // Aplicar estilo de total a toda la fila
                worksheet.Range(currentRow, 1, currentRow, 7).Style = totalStyle;

                currentRow += 2;
            }

            // Agregar bordes a toda la tabla
            if (currentRow > 4) {
                var dataRange = worksheet.Range(4, 1, currentRow - 3, 7);
                dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            }

            // Guardar el archivo
            var nombreArchivo = idAlmacenEspecifico.HasValue
                ? $"inventario-almacen-{idAlmacenEspecifico.Value}-{DateTime.Now:yyyyMMdd-HHmmss}.xlsx"
                : $"inventario-almacenes-{DateTime.Now:yyyyMMdd-HHmmss}.xlsx";

            workbook.SaveAs(nombreArchivo);

            // Mostrar el archivo Excel al usuario  
            if (mostrar) {
                try {
                    Process.Start(new ProcessStartInfo {
                        FileName = nombreArchivo,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex) {
                    CentroNotificaciones.Mostrar($"Error al abrir el archivo: {ex.Message}", Core.Modelos.Comun.TipoNotificacion.Error);
                }
            }
        }

        public static void GenerarDocumentoInventarioAlmacenesPdf(bool mostrar = true, int? idAlmacenEspecifico = null) {
            // Obtener datos de la base de datos  
            var almacenes = idAlmacenEspecifico.HasValue
                ? ObtenerAlmacenEspecifico(idAlmacenEspecifico.Value)
                : ObtenerAlmacenes();

            var inventario = ObtenerInventarioPorAlmacenes(idAlmacenEspecifico);

            // Si no hay datos, salir
            if (almacenes.Count == 0 || inventario.Count == 0) {
                CentroNotificaciones.Mostrar("No se encontraron datos para el reporte.", Core.Modelos.Comun.TipoNotificacion.Advertencia);
                return;
            }

            // Crear documento PDF  
            var documento = new PdfDocument();
            documento.Info.Title = idAlmacenEspecifico.HasValue
                ? $"Reporte de Inventario - Almacén {almacenes.First().Value}"
                : "Reporte de Inventario por Almacenes";
            documento.Info.Author = "aDVanceERP";

            // Configuración de márgenes y fuentes  
            const int margenIzquierdo = 40;
            const int margenDerecho = 40;
            const int margenSuperior = 40;
            const int margenInferior = 40;
            const int alturaFila = 18;
            const int maxFilasPorPagina = 26;

            var fontTitulo = new XFont("Arial", 14, XFontStyleEx.Bold);
            var fontSubtitulo = new XFont("Arial", 10, XFontStyleEx.Regular);
            var fontContenido = new XFont("Arial", 12, XFontStyleEx.Regular);
            var fontEncabezado = new XFont("Arial", 12, XFontStyleEx.Bold);
            var fontAlmacen = new XFont("Arial", 12, XFontStyleEx.Bold);

            // Primera pasada: Generar todo el contenido
            List<PdfPage> paginasGeneradas = new List<PdfPage>();
            PdfPage paginaActual = null;
            XGraphics gfx = null;
            double yPoint = 0;
            int filasEnPaginaActual = 0;

            foreach (var almacen in almacenes) {
                var productosEnAlmacen = inventario.ContainsKey(almacen.Key) ? inventario[almacen.Key] : new List<Dictionary<string, string>>();

                if (productosEnAlmacen.Count == 0)
                    continue;

                int productosProcesados = 0;

                while (productosProcesados < productosEnAlmacen.Count) {
                    // Crear nueva página si es necesario
                    if (paginaActual == null || filasEnPaginaActual >= maxFilasPorPagina - 3) {
                        // Cerrar el contexto gráfico anterior si existe
                        if (gfx != null) {
                            gfx.Dispose();
                        }

                        paginaActual = documento.AddPage();
                        paginaActual.Orientation = PageOrientation.Landscape;
                        paginaActual.Size = PageSize.A4;
                        paginasGeneradas.Add(paginaActual);
                        gfx = XGraphics.FromPdfPage(paginaActual);
                        yPoint = margenSuperior;
                        filasEnPaginaActual = 0;

                        // Dibujar encabezado
                        DibujarEncabezadoInventario(gfx, paginaActual, fontTitulo, fontSubtitulo,
                            margenIzquierdo, margenDerecho, ref yPoint, idAlmacenEspecifico.HasValue);
                        filasEnPaginaActual += 2;
                    }

                    // Encabezado de almacén
                    if (productosProcesados == 0) {
                        if (filasEnPaginaActual > maxFilasPorPagina - 3) {
                            // Cerrar el contexto gráfico actual
                            gfx.Dispose();

                            paginaActual = documento.AddPage();
                            paginaActual.Orientation = PageOrientation.Landscape;
                            paginasGeneradas.Add(paginaActual);
                            gfx = XGraphics.FromPdfPage(paginaActual);
                            yPoint = margenSuperior;
                            filasEnPaginaActual = 0;

                            DibujarEncabezadoInventario(gfx, paginaActual, fontTitulo, fontSubtitulo,
                                margenIzquierdo, margenDerecho, ref yPoint, idAlmacenEspecifico.HasValue);
                            filasEnPaginaActual += 2;
                        }

                        gfx.DrawString($"ALMACÉN: {almacen.Value.ToUpper()}", fontAlmacen, XBrushes.Black,
                            new XRect(margenIzquierdo, yPoint, paginaActual.Width - margenIzquierdo - margenDerecho, 20),
                            XStringFormats.TopLeft);
                        yPoint += 25;
                        filasEnPaginaActual += 1;

                        DibujarEncabezadosTablaInventario(gfx, paginaActual, fontEncabezado,
                            margenIzquierdo, margenDerecho, ref yPoint);
                        filasEnPaginaActual += 1;
                    }

                    // Calcular cuántas filas podemos poner en esta página
                    int filasDisponibles = maxFilasPorPagina - filasEnPaginaActual;
                    int filasAProcesar = Math.Min(filasDisponibles, productosEnAlmacen.Count - productosProcesados);

                    for (int i = 0; i < filasAProcesar; i++) {
                        var producto = productosEnAlmacen[productosProcesados];
                        DibujarFilaInventario(gfx, fontContenido, producto,
                            margenIzquierdo, margenDerecho, ref yPoint, alturaFila);

                        productosProcesados++;
                        filasEnPaginaActual++;
                    }

                    // Totales del almacén
                    if (productosProcesados == productosEnAlmacen.Count) {
                        if (filasEnPaginaActual > maxFilasPorPagina - 2) {
                            // Cerrar el contexto gráfico actual
                            gfx.Dispose();

                            paginaActual = documento.AddPage();
                            paginaActual.Orientation = PageOrientation.Landscape;
                            paginasGeneradas.Add(paginaActual);
                            gfx = XGraphics.FromPdfPage(paginaActual);
                            yPoint = margenSuperior;
                            filasEnPaginaActual = 0;

                            DibujarEncabezadoInventario(gfx, paginaActual, fontTitulo, fontSubtitulo,
                                margenIzquierdo, margenDerecho, ref yPoint, idAlmacenEspecifico.HasValue);
                            filasEnPaginaActual += 2;
                        }

                        DibujarTotalesAlmacen(gfx, fontContenido, productosEnAlmacen,
                            margenIzquierdo, margenDerecho, ref yPoint, alturaFila);
                        filasEnPaginaActual += 2;
                    }
                }
            }

            // Cerrar el último contexto gráfico si existe
            if (gfx != null) {
                gfx.Dispose();
            }

            // Segunda pasada: Numerar las páginas correctamente usando el mismo contexto gráfico
            for (int i = 0; i < documento.Pages.Count; i++) {
                using (var gfxUnificado = XGraphics.FromPdfPage(documento.Pages[i])) {
                    DibujarPiePaginaInventario(gfxUnificado, documento.Pages[i], fontSubtitulo,
                        margenIzquierdo, margenDerecho,
                        margenInferior, i + 1, documento.Pages.Count);
                }
            }

            // Guardar el documento
            var nombreArchivo = idAlmacenEspecifico.HasValue
                ? $"inventario-almacen-{idAlmacenEspecifico.Value}-{DateTime.Now:yyyyMMdd-HHmmss}.pdf"
                : $"inventario-almacenes-{DateTime.Now:yyyyMMdd-HHmmss}.pdf";

            if (documento.Pages.Count <= 0)
                return;

            documento.Save(nombreArchivo);

            // Mostrar el documento PDF al usuario  
            if (mostrar)
                Process.Start(new ProcessStartInfo {
                    FileName = nombreArchivo,
                    UseShellExecute = true
                });
        }

        private static void DibujarEncabezadoInventario(XGraphics gfx, PdfPage pagina, XFont fontTitulo, XFont fontSubtitulo,
                                                int margenIzquierdo, int margenDerecho, ref double yPoint, bool esAlmacenEspecifico = false) {
            // Agregar título
            string titulo = esAlmacenEspecifico ? "INVENTARIO POR ALMACÉN" : "INVENTARIO POR ALMACENES";
            gfx.DrawString(titulo, fontTitulo, XBrushes.Black,
                new XRect(margenIzquierdo, yPoint, pagina.Width - margenIzquierdo - margenDerecho, 20),
                XStringFormats.TopCenter);
            yPoint += 25;

            // Agregar información del reporte
            gfx.DrawString($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fontSubtitulo, XBrushes.Black,
                new XRect(margenIzquierdo, yPoint, pagina.Width, 15), XStringFormats.TopLeft);
            yPoint += 20;
        }

        private static void DibujarEncabezadosTablaInventario(XGraphics gfx, PdfPage pagina, XFont fontEncabezado,
                                                      int margenIzquierdo, int margenDerecho, ref double yPoint) {
            // Anchuras de columnas ajustadas para hoja horizontal
            var anchoCodigo = 100;
            var anchoUM = 30;
            var anchoStock = 45;
            var anchoPrecioCosto = 80;
            var anchoPrecioVenta = 70;
            var anchoCategoria = 150;

            // Calcular ancho disponible para producto (dinámico)
            double anchoTotalFijo = anchoCodigo + anchoUM + anchoStock +
                                  anchoPrecioCosto + anchoPrecioVenta + anchoCategoria;
            double anchoDisponible = pagina.Width - margenIzquierdo - margenDerecho;
            double anchoProducto = anchoDisponible - anchoTotalFijo;

            // Posición X inicial
            double xPos = margenIzquierdo;

            // Encabezados de columna
            gfx.DrawString("Código", fontEncabezado, XBrushes.Black,
                new XRect(xPos, yPoint, anchoCodigo, 20), XStringFormats.TopLeft);
            xPos += anchoCodigo;

            gfx.DrawString("Producto", fontEncabezado, XBrushes.Black,
                new XRect(xPos, yPoint, anchoProducto, 20), XStringFormats.TopLeft);
            xPos += anchoProducto;

            gfx.DrawString("UM", fontEncabezado, XBrushes.Black,
                new XRect(xPos, yPoint, anchoUM, 20), XStringFormats.TopCenter);
            xPos += anchoUM;

            gfx.DrawString("Stock", fontEncabezado, XBrushes.Black,
                new XRect(xPos, yPoint, anchoStock, 20), XStringFormats.TopRight);
            xPos += anchoStock;

            gfx.DrawString("Costo", fontEncabezado, XBrushes.Black,
                new XRect(xPos, yPoint, anchoPrecioCosto, 20), XStringFormats.TopRight);
            xPos += anchoPrecioCosto;

            gfx.DrawString("P. Venta", fontEncabezado, XBrushes.Black,
                new XRect(xPos, yPoint, anchoPrecioVenta, 20), XStringFormats.TopRight);
            xPos += anchoPrecioVenta;

            gfx.DrawString("Categoría", fontEncabezado, XBrushes.Black,
                new XRect(xPos + 10, yPoint, anchoCategoria, 20), XStringFormats.TopLeft);

            // Línea divisoria
            yPoint += 20;
            gfx.DrawLine(XPens.Black, margenIzquierdo, yPoint, pagina.Width - margenDerecho, yPoint);
            yPoint += 5;
        }

        private static void DibujarFilaInventario(XGraphics gfx, XFont fontContenido, Dictionary<string, string> producto,
                                          int margenIzquierdo, int margenDerecho, ref double yPoint, int alturaFila) {
            // Anchuras de columnas (consistentes con encabezados)
            var anchoCodigo = 100;
            var anchoUM = 30;
            var anchoStock = 45;
            var anchoPrecioCosto = 80;
            var anchoPrecioVenta = 70;
            var anchoCategoria = 150;

            // Calcular ancho disponible para producto (dinámico)
            double anchoTotalFijo = anchoCodigo + anchoUM + anchoStock +
                                  anchoPrecioCosto + anchoPrecioVenta + anchoCategoria;
            double anchoDisponible = gfx.PageSize.Width - margenIzquierdo - margenDerecho;
            double anchoProducto = anchoDisponible - anchoTotalFijo;

            // Posición X inicial
            double xPos = margenIzquierdo;

            // Código del producto
            gfx.DrawString(producto["codigo"], fontContenido, XBrushes.Black,
                new XRect(xPos, yPoint, anchoCodigo, alturaFila), XStringFormats.TopLeft);
            xPos += anchoCodigo;

            // Nombre del producto
            gfx.DrawString(producto["nombre"], fontContenido, XBrushes.Black,
                new XRect(xPos, yPoint, anchoProducto, alturaFila), XStringFormats.TopLeft);
            xPos += anchoProducto;

            // Unidad de medida
            gfx.DrawString(producto["unidad_medida"], fontContenido, XBrushes.Black,
                new XRect(xPos, yPoint, anchoUM, alturaFila), XStringFormats.TopCenter);
            xPos += anchoUM;

            // Stock
            gfx.DrawString(producto["cantidad"], fontContenido, XBrushes.Black,
                new XRect(xPos, yPoint, anchoStock, alturaFila), XStringFormats.TopRight);
            xPos += anchoStock;

            // Precio costo (compra o producción)
            gfx.DrawString(producto["precio_costo"], fontContenido, XBrushes.Black,
                new XRect(xPos, yPoint, anchoPrecioCosto, alturaFila), XStringFormats.TopRight);
            xPos += anchoPrecioCosto;

            // Precio venta
            gfx.DrawString(producto["precio_venta"], fontContenido, XBrushes.Black,
                new XRect(xPos, yPoint, anchoPrecioVenta, alturaFila), XStringFormats.TopRight);
            xPos += anchoPrecioVenta;

            // Categoría
            string categoria = producto["categoria"] switch {
                "ProductoTerminado" => "PROD. TERMINADO",
                "MateriaPrima" => "MATERIA PRIMA",
                _ => "MERCANCÍA"
            };
            gfx.DrawString(categoria, fontContenido, XBrushes.Black,
                new XRect(xPos + 10, yPoint, anchoCategoria, alturaFila), XStringFormats.TopLeft);

            yPoint += alturaFila;
        }

        private static void DibujarTotalesAlmacen(XGraphics gfx, XFont fontContenido, List<Dictionary<string, string>> productos,
                                          int margenIzquierdo, int margenDerecho, ref double yPoint, int alturaFila) {
            // Calcular totales
            int totalProductos = productos.Count;
            decimal valorTotalCosto = productos.Sum(p => decimal.Parse(p["cantidad"], NumberStyles.Any, CultureInfo.InvariantCulture) * decimal.Parse(p["precio_costo"], NumberStyles.Any, CultureInfo.InvariantCulture));

            // Dibujar línea divisoria
            gfx.DrawLine(XPens.Black, margenIzquierdo, yPoint, gfx.PageSize.Width - margenDerecho, yPoint);
            yPoint += 5;

            // Anchuras de columnas (consistentes con encabezados)
            var anchoCodigo = 100;
            var anchoUM = 30;
            var anchoStock = 45;
            var anchoPrecioCosto = 80;
            var anchoPrecioVenta = 70;
            var anchoCategoria = 150;

            // Calcular ancho disponible para producto (dinámico)
            double anchoTotalFijo = anchoCodigo + anchoUM + anchoStock +
                                  anchoPrecioCosto + anchoPrecioVenta + anchoCategoria;
            double anchoDisponible = gfx.PageSize.Width - margenIzquierdo - margenDerecho;
            double anchoProducto = anchoDisponible - anchoTotalFijo;

            // Posición X inicial
            double xPos = margenIzquierdo;

            // Total productos
            gfx.DrawString("TOTAL ALMACÉN:", fontContenido, XBrushes.Black,
                new XRect(xPos, yPoint, anchoCodigo + anchoProducto, alturaFila), XStringFormats.TopRight);
            xPos += anchoCodigo + anchoProducto;

            gfx.DrawString(totalProductos.ToString("N0", CultureInfo.InvariantCulture), fontContenido, XBrushes.Black,
                new XRect(xPos, yPoint, anchoUM + anchoStock, alturaFila), XStringFormats.TopRight);
            xPos += anchoUM + anchoStock;

            // Valor total costo/compra
            gfx.DrawString($"${valorTotalCosto.ToString("N2", CultureInfo.InvariantCulture)}", fontContenido, XBrushes.Black,
                new XRect(xPos, yPoint, anchoPrecioCosto, alturaFila), XStringFormats.TopRight);
            xPos += anchoPrecioCosto;

            yPoint += alturaFila;
        }

        private static void DibujarPiePaginaInventario(XGraphics gfx, PdfPage pagina, XFont fontSubtitulo,
                                               int margenIzquierdo, int margenDerecho,
                                               int margenInferior, int paginaActual,
                                               int totalPaginas) {
            var textoPie = $"Página {paginaActual} de {totalPaginas} - {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

            gfx.DrawString(textoPie, fontSubtitulo, XBrushes.Black,
                new XRect(margenIzquierdo, pagina.Height - margenInferior,
                    pagina.Width - margenIzquierdo - margenDerecho, 20),
                XStringFormats.BottomRight);
        }

        #region Métodos para obtener datos de la base de datos

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
                    COALESCE(pa.cantidad, 0) as cantidad,  -- Mostrar 0 si no hay inventario
                    COALESCE(um.abreviatura, 'N/A') as unidad_medida  -- Valor por defecto
                FROM adv__producto p
                LEFT JOIN adv__inventario pa ON p.id_producto = pa.id_producto
                LEFT JOIN adv__unidad_medida um ON p.id_unidad_medida = um.id_unidad_medida
                WHERE 1=1  -- Condición siempre verdadera para facilitar filtros
                """;

                // Agregar filtro por almacén si se especifica
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
                                ["precio_costo"] = reader.IsDBNull("precio_costo") ? "0.00" : reader.GetDecimal("precio_costo").ToString("N2", CultureInfo.InvariantCulture),
                                ["precio_venta"] = reader.IsDBNull("precio_venta") ? "0.00" : reader.GetDecimal("precio_venta").ToString("N2", CultureInfo.InvariantCulture),
                                ["cantidad"] = reader.IsDBNull("cantidad") ? "0.00" : reader.GetDecimal("cantidad").ToString("N2", CultureInfo.InvariantCulture),
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
