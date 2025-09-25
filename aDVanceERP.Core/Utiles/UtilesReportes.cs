using System.Diagnostics;
using System.Globalization;

using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace aDVanceERP.Core.Utiles;

public static class UtilesReportes {
    #region Inventario por almacenes

    public static void GenerarReporteInventarioAlmacenes(bool mostrar = true) {
        // Obtener datos de la base de datos  
        var almacenes = ObtenerAlmacenes();
        var inventario = ObtenerInventarioPorAlmacenes();

        // Crear documento PDF  
        var documento = new PdfDocument();
        documento.Info.Title = "Reporte de Inventario por Almacenes";
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
                        margenIzquierdo, margenDerecho, ref yPoint);
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
                            margenIzquierdo, margenDerecho, ref yPoint);
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
                            margenIzquierdo, margenDerecho, ref yPoint);
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
        var nombreArchivo = $"inventario-almacenes-{DateTime.Now:yyyyMMdd-HHmmss}.pdf";

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
                                            int margenIzquierdo, int margenDerecho, ref double yPoint) {
        // Agregar título
        gfx.DrawString("INVENTARIO POR ALMACENES", fontTitulo, XBrushes.Black,
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

    private static Dictionary<int, List<Dictionary<string, string>>> ObtenerInventarioPorAlmacenes() {
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
                    ELSE p.precio_compra
                END as precio_costo,
                p.precio_venta_base as precio_venta,
                pa.cantidad,
                um.abreviatura as unidad_medida
            FROM adv__inventario pa
            JOIN adv__producto p ON pa.id_producto = p.id_producto
            JOIN adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
            JOIN adv__unidad_medida um ON dp.id_unidad_medida = um.id_unidad_medida
            WHERE pa.cantidad > 0
            ORDER BY pa.id_almacen, p.nombre
            """;

            using (var command = new MySqlCommand(query, connection)) {
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int idAlmacen = reader.GetInt32("id_almacen");

                        if (!inventario.ContainsKey(idAlmacen)) {
                            inventario[idAlmacen] = new List<Dictionary<string, string>>();
                        }

                        var producto = new Dictionary<string, string> {
                            ["codigo"] = reader["codigo"].ToString(),
                            ["nombre"] = reader["nombre"].ToString(),
                            ["categoria"] = reader["categoria"].ToString(),
                            ["precio_costo"] = reader.GetDecimal("precio_costo").ToString("N2", CultureInfo.InvariantCulture),
                            ["precio_venta"] = reader.GetDecimal("precio_venta").ToString("N2", CultureInfo.InvariantCulture),
                            ["cantidad"] = reader.GetDecimal("cantidad").ToString("N2", CultureInfo.InvariantCulture),
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

    #endregion
    #region Mayor de inventario [Incompleto]

    public static void GenerarReporteMayor(DateTime fechaInicio, DateTime fechaFin,
                                long idAlmacen = 0, string categoria = "Todas",
                                bool mostrar = true) {
        // Obtener datos de la base de datos
        var movimientos = ObtenerMovimientosInventario(fechaInicio, fechaFin, idAlmacen, categoria);
        var saldosIniciales = ObtenerSaldosIniciales(fechaInicio, idAlmacen, categoria);
        var productos = ObtenerProductosConUnidadMedida(idAlmacen, categoria);

        // Procesar datos para el reporte
        var filas = ProcesarDatosParaMayor(movimientos, saldosIniciales, productos, fechaInicio, fechaFin);

        // Crear documento PDF
        var documento = new PdfDocument();
        documento.Info.Title = "Reporte de Mayor de Inventario";
        documento.Info.Author = "aDVanceERP";

        // Configuración de márgenes y fuentes
        const int margenIzquierdo = 40;
        const int margenDerecho = 40;
        const int margenSuperior = 40;
        const int margenInferior = 40;
        const int alturaEncabezado = 120;
        const int alturaPie = 30;
        const int alturaFila = 18;
        const int maxFilasPorPagina = 23;

        var fontTitulo = new XFont("Arial", 14, XFontStyleEx.Bold);
        var fontSubtitulo = new XFont("Arial", 10, XFontStyleEx.Regular);
        var fontContenido = new XFont("Arial", 12, XFontStyleEx.Regular);
        var fontEncabezado = new XFont("Arial", 12, XFontStyleEx.Bold);

        int paginaActual = 1;
        int filasProcesadas = 0;
        PdfPage pagina = null;
        XGraphics gfx = null;
        double yPoint = 0;

        while (filasProcesadas < filas.Count) {
            if (pagina == null || filasProcesadas % maxFilasPorPagina == 0) {
                // Crear página en orientación horizontal
                pagina = documento.AddPage();
                pagina.Orientation = PageOrientation.Landscape;
                pagina.Size = PageSize.A4;
                gfx = XGraphics.FromPdfPage(pagina);
                yPoint = margenSuperior;

                // Dibujar encabezado
                DibujarEncabezadoMayor(gfx, pagina, fontTitulo, fontSubtitulo,
                    fechaInicio, fechaFin, idAlmacen, categoria,
                    margenIzquierdo, margenDerecho, ref yPoint);

                // Dibujar encabezados de tabla
                DibujarEncabezadosTablaMayor(gfx, pagina, fontEncabezado,
                    margenIzquierdo, margenDerecho, ref yPoint);
            }

            // Dibujar filas de datos
            int filasEnPagina = Math.Min(maxFilasPorPagina, filas.Count - filasProcesadas);
            for (int i = 0; i < filasEnPagina; i++) {
                var fila = filas[filasProcesadas];
                DibujarFilaMayor(gfx, fontContenido, fila,
                    margenIzquierdo, margenDerecho, ref yPoint, alturaFila);
                filasProcesadas++;
            }

            // Dibujar pie de página
            if (filasProcesadas == filas.Count || filasProcesadas % maxFilasPorPagina == 0) {
                DibujarPiePaginaMayor(gfx, pagina, fontSubtitulo,
                    margenIzquierdo, margenDerecho,
                    margenInferior, ref yPoint, paginaActual,
                    documento.Pages.Count);
                paginaActual++;
            }
        }

        // Guardar el documento
        var nombreArchivo = $"mayor-inventario-{fechaInicio:yyyy-MM-dd}-{fechaFin:yyyy-MM-dd}.pdf";

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

    private static void DibujarEncabezadoMayor(XGraphics gfx, PdfPage pagina, XFont fontTitulo, XFont fontSubtitulo,
                                        DateTime fechaInicio, DateTime fechaFin, long idAlmacen, string categoria,
                                        int margenIzquierdo, int margenDerecho, ref double yPoint) {
        // Obtener nombre del almacén
        string nombreAlmacen = idAlmacen == 0 ? "Todos los almacenes" : ObtenerNombreAlmacen(idAlmacen);

        // Agregar título
        gfx.DrawString("MAYOR DE INVENTARIO", fontTitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width - margenIzquierdo - margenDerecho, 20),
            XStringFormats.TopLeft);
        yPoint += 25;

        // Agregar información del reporte
        gfx.DrawString($"Período: {fechaInicio.ToShortDateString()} - {fechaFin.ToShortDateString()}", fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width, 15), XStringFormats.TopLeft);
        yPoint += 15;

        gfx.DrawString($"Almacén: {nombreAlmacen}", fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width, 15), XStringFormats.TopLeft);
        yPoint += 15;

        categoria =
            categoria == "Todas" ? "Todas las categorías" :
            categoria == "ProductoTerminado" ? "Productos Terminados" :
            categoria == "MateriaPrima" ? "Materias Primas" :
            "Mercancías";
        gfx.DrawString($"Categoría: {categoria}", fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width, 15), XStringFormats.TopLeft);
        yPoint += 20;
    }

    private static void DibujarEncabezadosTablaMayor(XGraphics gfx, PdfPage pagina, XFont fontEncabezado,
                                            int margenIzquierdo, int margenDerecho, ref double yPoint) {
        // Anchuras fijas para las columnas
        var anchoCodigo = 100;
        var anchoUM = 30;
        var anchoSaldoInicial = 70;
        var anchoDetalle = 120;
        var anchoEntradas = 60;
        var anchoSalidas = 60;
        var anchoSaldoFinal = 70;
        var anchoCosto = 60;

        // Calcular ancho disponible para producto (dinámico)
        double anchoTotalFijo = anchoCodigo + anchoUM + anchoSaldoInicial + anchoDetalle +
                              anchoEntradas + anchoSalidas + anchoSaldoFinal + anchoCosto;
        double anchoDisponible = pagina.Width - margenIzquierdo - margenDerecho;
        double anchoProducto = anchoDisponible - anchoTotalFijo;

        // Posición X inicial
        double xPos = margenIzquierdo;

        // Encabezados de columna
        gfx.DrawString("Código", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoCodigo, 18), XStringFormats.TopLeft);
        xPos += anchoCodigo;

        gfx.DrawString("Producto", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoProducto, 18), XStringFormats.TopLeft);
        xPos += anchoProducto;

        gfx.DrawString("UM", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoUM, 18), XStringFormats.TopCenter);
        xPos += anchoUM;

        gfx.DrawString("Saldo Ini.", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoSaldoInicial, 18), XStringFormats.TopRight);
        xPos += anchoSaldoInicial;

        gfx.DrawString("Detalle Movimiento", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoDetalle, 18), XStringFormats.TopLeft);
        xPos += anchoDetalle;

        gfx.DrawString("Entradas", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoEntradas, 18), XStringFormats.TopRight);
        xPos += anchoEntradas;

        gfx.DrawString("Salidas", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoSalidas, 18), XStringFormats.TopRight);
        xPos += anchoSalidas;

        gfx.DrawString("Saldo Fin.", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoSaldoFinal, 18), XStringFormats.TopRight);
        xPos += anchoSaldoFinal;

        gfx.DrawString("Costo", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoCosto, 18), XStringFormats.TopRight);

        // Línea divisoria
        yPoint += 18;
        gfx.DrawLine(XPens.Black, margenIzquierdo, yPoint, pagina.Width - margenDerecho, yPoint);
        yPoint += 5;
    }

    private static void DibujarFilaMayor(XGraphics gfx, XFont fontContenido, string[] fila,
                                int margenIzquierdo, int margenDerecho, ref double yPoint, int alturaFila) {
        // Anchuras fijas (consistentes con encabezados)
        var anchoCodigo = 100;
        var anchoUM = 30;
        var anchoSaldoInicial = 70;
        var anchoDetalle = 120;
        var anchoEntradas = 60;
        var anchoSalidas = 60;
        var anchoSaldoFinal = 70;
        var anchoCosto = 60;

        // Calcular ancho para producto (dinámico)
        double anchoTotalFijo = anchoCodigo + anchoUM + anchoSaldoInicial + anchoDetalle +
                              anchoEntradas + anchoSalidas + anchoSaldoFinal + anchoCosto;
        double anchoDisponible = gfx.PageSize.Width - margenIzquierdo - margenDerecho;
        double anchoProducto = anchoDisponible - anchoTotalFijo;

        // Posición X inicial
        double xPos = margenIzquierdo;

        // Código del producto (solo en la primera fila del producto)
        if (fila[0] != "") {
            gfx.DrawString(fila[0], fontContenido, XBrushes.Black,
                new XRect(xPos, yPoint, anchoCodigo, alturaFila), XStringFormats.TopLeft);
        }
        xPos += anchoCodigo;

        // Nombre del producto (solo en la primera fila del producto)
        if (fila[1] != "") {
            gfx.DrawString(fila[1], fontContenido, XBrushes.Black,
                new XRect(xPos, yPoint, anchoProducto, alturaFila), XStringFormats.TopLeft);
        }
        xPos += anchoProducto;

        // Unidad de medida (solo en la primera fila del producto)
        if (fila[2] != "") {
            gfx.DrawString(fila[2], fontContenido, XBrushes.Black,
                new XRect(xPos, yPoint, anchoUM, alturaFila), XStringFormats.TopCenter);
        }
        xPos += anchoUM;

        // Saldo inicial (solo en la primera fila del producto)
        if (fila[3] != "") {
            gfx.DrawString(fila[3], fontContenido, XBrushes.Black,
                new XRect(xPos, yPoint, anchoSaldoInicial, alturaFila), XStringFormats.TopRight);
        }
        xPos += anchoSaldoInicial;

        // Detalle del movimiento
        gfx.DrawString(fila[4], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoDetalle, alturaFila), XStringFormats.TopLeft);
        xPos += anchoDetalle;

        // Entradas
        gfx.DrawString(fila[5], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoEntradas, alturaFila), XStringFormats.TopRight);
        xPos += anchoEntradas;

        // Salidas
        gfx.DrawString(fila[6], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoSalidas, alturaFila), XStringFormats.TopRight);
        xPos += anchoSalidas;

        // Saldo final
        gfx.DrawString(fila[7], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoSaldoFinal, alturaFila), XStringFormats.TopRight);
        xPos += anchoSaldoFinal;

        // Costo
        gfx.DrawString(fila[8], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoCosto, alturaFila), XStringFormats.TopRight);

        yPoint += alturaFila;
    }

    private static void DibujarPiePaginaMayor(XGraphics gfx, PdfPage pagina, XFont fontSubtitulo,
                                    int margenIzquierdo, int margenDerecho,
                                    int margenInferior, ref double yPoint, int paginaActual,
                                    int totalPaginas) {
        // Pie de página
        var fechaGeneracion = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        var textoPie = $"{fechaGeneracion} - Página {paginaActual} de {totalPaginas}";

        gfx.DrawString(textoPie, fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, pagina.Height - margenInferior, pagina.Width - margenIzquierdo - margenDerecho, 20),
            XStringFormats.BottomRight);
    }

    private static List<string[]> ProcesarDatosParaMayor(List<Dictionary<string, object>> movimientos,
                                                Dictionary<int, decimal> saldosIniciales,
                                                Dictionary<int, Dictionary<string, string>> productos,
                                                DateTime fechaInicio, DateTime fechaFin) {
        var filas = new List<string[]>();

        // Agrupar movimientos por producto
        var movimientosPorProducto = movimientos
            .GroupBy(m => m["id_producto"])
            .ToDictionary(g => (int) g.Key, g => g.OrderBy(m => m["fecha"]).ToList());

        // Procesar cada producto
        foreach (var productoId in productos.Keys) {
            var productoInfo = productos[productoId];
            decimal saldoActual = saldosIniciales.ContainsKey(productoId) ? saldosIniciales[productoId] : 0;
            bool primeraFila = true;

            // Agregar fila de saldo inicial
            filas.Add(new string[] {
            primeraFila ? productoInfo["codigo"] : "",
            primeraFila ? productoInfo["nombre"] : "",
            primeraFila ? productoInfo["unidad_medida"] : "",
            saldoActual.ToString("N2"),
            "Saldo inicial",
            "",
            "",
            saldoActual.ToString("N2"),
            productoInfo["costo_unitario"]
        });
            primeraFila = false;

            // Procesar movimientos del producto
            if (movimientosPorProducto.ContainsKey(productoId)) {
                foreach (var movimiento in movimientosPorProducto[productoId]) {
                    decimal cantidad = Convert.ToDecimal(movimiento["cantidad_movida"]);
                    string tipoMovimiento = movimiento["tipo_movimiento"].ToString();
                    string efecto = movimiento["efecto"].ToString();
                    DateTime fechaMovimiento = Convert.ToDateTime(movimiento["fecha"]);
                    string detalle = $"{fechaMovimiento:dd/MM/yyyy} - {tipoMovimiento}";

                    decimal entradas = 0;
                    decimal salidas = 0;

                    if (efecto == "Carga") {
                        entradas = cantidad;
                        saldoActual += cantidad;
                    } else if (efecto == "Descarga") {
                        salidas = cantidad;
                        saldoActual -= cantidad;
                    } else if (efecto == "Transferencia") {
                        // Determinar si es entrada o salida según el almacén
                        int idAlmacenOrigen = Convert.ToInt32(movimiento["id_almacen_origen"]);
                        int idAlmacenDestino = Convert.ToInt32(movimiento["id_almacen_destino"]);

                        // Asumimos que el reporte es para un almacén específico o todos
                        // Si es transferencia de otro almacén a este, es entrada
                        // Si es transferencia de este a otro almacén, es salida
                        // En el caso de "todos los almacenes", mostramos ambas
                        entradas = cantidad;
                        salidas = cantidad;
                        saldoActual = saldoActual; // No cambia el saldo total
                    }

                    filas.Add(new string[] {
                    "", // Código vacío para filas subsiguientes
                    "", // Nombre vacío para filas subsiguientes
                    "", // UM vacía para filas subsiguientes
                    "",
                    detalle,
                    entradas.ToString("N2"),
                    salidas.ToString("N2"),
                    saldoActual.ToString("N2"),
                    productoInfo["costo_unitario"]
                });
                }
            }

            // Agregar línea divisoria entre productos
            filas.Add(new string[] { "", "", "", "", "", "", "", "", "" });
        }

        return filas;
    }

    #endregion
    #region Submayor de inventario

    public static void GenerarReporteSubmayor(DateTime fechaInicio, DateTime fechaFin,
                                        long idAlmacen = 0, string categoria = "Todas",
                                        bool mostrar = true) {
        // Obtener datos de la base de datos
        var movimientos = ObtenerMovimientosInventario(fechaInicio, fechaFin, idAlmacen, categoria);
        var saldosIniciales = ObtenerSaldosIniciales(fechaInicio, idAlmacen, categoria);
        var productos = ObtenerProductosConUnidadMedida(idAlmacen, categoria);

        // Procesar datos para el reporte
        var filas = ProcesarDatosParaReporte(movimientos, saldosIniciales, productos, fechaInicio, fechaFin);

        // Crear documento PDF
        var documento = new PdfDocument();
        documento.Info.Title = "Reporte de Submayor de Inventario";
        documento.Info.Author = "aDVanceERP";

        // Configuración de márgenes y fuentes
        const int margenIzquierdo = 40;
        const int margenDerecho = 40;
        const int margenSuperior = 40;
        const int margenInferior = 40;
        const int alturaEncabezado = 120;
        const int alturaPie = 30;
        const int alturaFila = 18; // Aumentado para Arial 12
        const int maxFilasPorPagina = 23; // Ajustado por el mayor tamaño de fuente

        var fontTitulo = new XFont("Arial", 14, XFontStyleEx.Bold);
        var fontSubtitulo = new XFont("Arial", 10, XFontStyleEx.Regular);
        var fontContenido = new XFont("Arial", 12, XFontStyleEx.Regular); // Tamaño 12 como solicitado
        var fontEncabezado = new XFont("Arial", 12, XFontStyleEx.Bold);

        int paginaActual = 1;
        int filasProcesadas = 0;
        PdfPage pagina = null;
        XGraphics gfx = null;
        double yPoint = 0;

        while (filasProcesadas < filas.Count) {
            if (pagina == null || filasProcesadas % maxFilasPorPagina == 0) {
                // Crear página en orientación horizontal
                pagina = documento.AddPage();
                pagina.Orientation = PageOrientation.Landscape;
                pagina.Size = PageSize.A4;
                gfx = XGraphics.FromPdfPage(pagina);
                yPoint = margenSuperior;

                // Dibujar encabezado (según formato original)
                DibujarEncabezadoSubmayorOriginal(gfx, pagina, fontTitulo, fontSubtitulo,
                    fechaInicio, fechaFin, idAlmacen, categoria,
                    margenIzquierdo, margenDerecho, ref yPoint);

                // Dibujar encabezados de tabla con columna de número
                DibujarEncabezadosTablaSubmayorConNumeros(gfx, pagina, fontEncabezado,
                    margenIzquierdo, margenDerecho, ref yPoint);
            }

            // Dibujar filas de datos
            int filasEnPagina = Math.Min(maxFilasPorPagina, filas.Count - filasProcesadas);
            for (int i = 0; i < filasEnPagina; i++) {
                var fila = filas[filasProcesadas];
                DibujarFilaSubmayorConNumeros(gfx, fontContenido, fila, filasProcesadas + 1,
                                  margenIzquierdo, margenDerecho, ref yPoint, alturaFila);
                filasProcesadas++;
            }

            // Dibujar pie de página
            if (filasProcesadas == filas.Count || filasProcesadas % maxFilasPorPagina == 0) {
                DibujarPiePaginaSubmayorOriginal(gfx, pagina, fontSubtitulo,
                                        margenIzquierdo, margenDerecho,
                                        margenInferior, ref yPoint, paginaActual,
                                        documento.Pages.Count);
                paginaActual++;
            }
        }

        // Guardar el documento
        var nombreArchivo = $"submayor-inventario-{fechaInicio:yyyy-MM-dd}-{fechaFin:yyyy-MM-dd}.pdf";

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

    // Métodos auxiliares actualizados
    private static void DibujarEncabezadoSubmayorOriginal(XGraphics gfx, PdfPage pagina, XFont fontTitulo, XFont fontSubtitulo,
                                                DateTime fechaInicio, DateTime fechaFin, long idAlmacen, string categoria,
                                                int margenIzquierdo, int margenDerecho, ref double yPoint) {
        // Obtener nombre del almacén
        string nombreAlmacen = idAlmacen == 0 ? "Todos los almacenes" : ObtenerNombreAlmacen(idAlmacen);

        // Agregar título
        gfx.DrawString("SUBMAYOR DE INVENTARIO", fontTitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width - margenIzquierdo - margenDerecho, 20),
            XStringFormats.TopLeft);
        yPoint += 25;

        // Agregar información del reporte
        gfx.DrawString($"Período: {fechaInicio.ToShortDateString()} - {fechaFin.ToShortDateString()}", fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width, 15), XStringFormats.TopLeft);
        yPoint += 15;

        gfx.DrawString($"Almacén: {nombreAlmacen}", fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width, 15), XStringFormats.TopLeft);
        yPoint += 15;

        categoria = 
            categoria == "Todas" ? "Todas las categorías" : 
            categoria == "ProductoTerminado" ? "Productos Terminados" :
            categoria == "MateriaPrima" ? "Materias Primas" :
            "Mercancías";
        gfx.DrawString($"Categoría: {categoria}", fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width, 15), XStringFormats.TopLeft);
        yPoint += 20;
    }

    private static void DibujarEncabezadosTablaSubmayorConNumeros(XGraphics gfx, PdfPage pagina, XFont fontEncabezado,
                                                      int margenIzquierdo, int margenDerecho, ref double yPoint) {
        // Anchuras fijas para todas las columnas excepto producto
        var anchoNumero = 40;
        var anchoCodigo = 100;
        var anchoUM = 30;
        var anchoSaldoInicial = 60;
        var anchoEntradas = 60;
        var anchoSalidas = 60;
        var anchoSaldoFinal = 70;
        var anchoCostoPromedio = 80;

        // Calcular ancho disponible para producto (dinámico)
        double anchoTotalFijo = anchoNumero + anchoCodigo + anchoUM + anchoSaldoInicial +
                              anchoEntradas + anchoSalidas + anchoSaldoFinal + anchoCostoPromedio;
        double anchoDisponible = pagina.Width - margenIzquierdo - margenDerecho;
        double anchoProducto = anchoDisponible - anchoTotalFijo;

        // Posición X inicial
        double xPos = margenIzquierdo;

        // Encabezados de columna
        gfx.DrawString("#", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoNumero, 18), XStringFormats.TopCenter);
        xPos += anchoNumero;

        gfx.DrawString("Código", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoCodigo, 18), XStringFormats.TopLeft);
        xPos += anchoCodigo;

        gfx.DrawString("Producto", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoProducto, 18), XStringFormats.TopLeft);
        xPos += anchoProducto;

        gfx.DrawString("UM", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoUM, 18), XStringFormats.TopCenter);
        xPos += anchoUM;

        gfx.DrawString("Saldo Ini.", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoSaldoInicial, 18), XStringFormats.TopRight);
        xPos += anchoSaldoInicial;

        gfx.DrawString("Entradas", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoEntradas, 18), XStringFormats.TopRight);
        xPos += anchoEntradas;

        gfx.DrawString("Salidas", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoSalidas, 18), XStringFormats.TopRight);
        xPos += anchoSalidas;

        gfx.DrawString("Saldo Fin.", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoSaldoFinal, 18), XStringFormats.TopRight);
        xPos += anchoSaldoFinal;

        gfx.DrawString("Costo Prom.", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchoCostoPromedio, 18), XStringFormats.TopRight);

        // Línea divisoria
        yPoint += 18;
        gfx.DrawLine(XPens.Black, margenIzquierdo, yPoint, pagina.Width - margenDerecho, yPoint);
        yPoint += 5;
    }

    private static void DibujarFilaSubmayorConNumeros(XGraphics gfx, XFont fontContenido, string[] fila, int numeroFila,
                                          int margenIzquierdo, int margenDerecho, ref double yPoint, int alturaFila) {
        // Anchuras fijas (consistentes con encabezados)
        var anchoNumero = 40;
        var anchoCodigo = 100;
        var anchoUM = 30;
        var anchoSaldoInicial = 60;
        var anchoEntradas = 60;
        var anchoSalidas = 60;
        var anchoSaldoFinal = 70;
        var anchoCostoPromedio = 80;

        // Calcular ancho para producto (dinámico)
        double anchoTotalFijo = anchoNumero + anchoCodigo + anchoUM + anchoSaldoInicial +
                              anchoEntradas + anchoSalidas + anchoSaldoFinal + anchoCostoPromedio;
        double anchoDisponible = gfx.PageSize.Width - margenIzquierdo - margenDerecho;
        double anchoProducto = anchoDisponible - anchoTotalFijo;

        // Posición X inicial
        double xPos = margenIzquierdo;

        // Número de fila
        gfx.DrawString(numeroFila.ToString(), fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoNumero, alturaFila), XStringFormats.TopCenter);
        xPos += anchoNumero;

        // Código del producto
        gfx.DrawString(fila[0], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoCodigo, alturaFila), XStringFormats.TopLeft);
        xPos += anchoCodigo;

        // Nombre del producto (usa todo el ancho disponible)
        gfx.DrawString(fila[1], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoProducto, alturaFila), XStringFormats.TopLeft);
        xPos += anchoProducto;

        // Unidad de medida
        gfx.DrawString(fila[2], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoUM, alturaFila), XStringFormats.TopCenter);
        xPos += anchoUM;

        // Saldo inicial
        gfx.DrawString(fila[3], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoSaldoInicial, alturaFila), XStringFormats.TopRight);
        xPos += anchoSaldoInicial;

        // Entradas
        gfx.DrawString(fila[4], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoEntradas, alturaFila), XStringFormats.TopRight);
        xPos += anchoEntradas;

        // Salidas
        gfx.DrawString(fila[5], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoSalidas, alturaFila), XStringFormats.TopRight);
        xPos += anchoSalidas;

        // Saldo final
        gfx.DrawString(fila[6], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoSaldoFinal, alturaFila), XStringFormats.TopRight);
        xPos += anchoSaldoFinal;

        // Costo promedio
        gfx.DrawString(fila[7], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchoCostoPromedio, alturaFila), XStringFormats.TopRight);

        yPoint += alturaFila;
    }

    private static void DibujarPiePaginaSubmayorOriginal(XGraphics gfx, PdfPage pagina, XFont fontSubtitulo,
                                               int margenIzquierdo, int margenDerecho,
                                               int margenInferior, ref double yPoint, int paginaActual,
                                               int totalPaginas) {
        // Pie de página original
        var fechaGeneracion = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        var textoPie = $"{fechaGeneracion} - Página {paginaActual} de {totalPaginas}";

        gfx.DrawString(textoPie, fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, pagina.Height - margenInferior, pagina.Width - margenIzquierdo - margenDerecho, 20),
            XStringFormats.BottomRight);
    }

    // Métodos para obtener datos de la base de datos
    private static List<Dictionary<string, object>> ObtenerMovimientosInventario(DateTime fechaInicio, DateTime fechaFin, long idAlmacen, string categoria) {
        var movimientos = new List<Dictionary<string, object>>();

        using (var connection = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            connection.Open();

            var query = @"
                SELECT 
                    m.id_movimiento,
                    m.id_producto,
                    p.codigo,
                    p.nombre,
                    p.categoria,
                    m.id_almacen_origen,
                    m.id_almacen_destino,
                    m.fecha,
                    m.cantidad_movida,
                    tm.nombre as tipo_movimiento,
                    tm.efecto,
                    pa.cantidad as saldo_actual
                FROM adv__movimiento m
                JOIN adv__producto p ON m.id_producto = p.id_producto
                JOIN adv__tipo_movimiento tm ON m.id_tipo_movimiento = tm.id_tipo_movimiento
                JOIN adv__inventario pa ON m.id_producto = pa.id_producto 
                    AND (m.id_almacen_origen = pa.id_almacen OR m.id_almacen_destino = pa.id_almacen)
                WHERE m.fecha BETWEEN @fechaInicio AND @fechaFin
            ";

            if (idAlmacen > 0) {
                query += " AND (m.id_almacen_origen = @idAlmacen OR m.id_almacen_destino = @idAlmacen)";
            }

            if (categoria != "Todas") {
                query += " AND p.categoria = @categoria";
            }

            query += " ORDER BY p.codigo, m.fecha";

            using (var command = new MySqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                command.Parameters.AddWithValue("@fechaFin", fechaFin);

                if (idAlmacen > 0) {
                    command.Parameters.AddWithValue("@idAlmacen", idAlmacen);
                }

                if (categoria != "Todas") {
                    command.Parameters.AddWithValue("@categoria", categoria);
                }

                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        var movimiento = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++) {
                            movimiento[reader.GetName(i)] = reader.GetValue(i);
                        }
                        movimientos.Add(movimiento);
                    }
                }
            }
        }

        return movimientos;
    }

    private static Dictionary<int, decimal> ObtenerSaldosIniciales(DateTime fechaInicio, long idAlmacen, string categoria) {
        var saldos = new Dictionary<int, decimal>();

        using (var connection = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            connection.Open();

            var query = @"
                SELECT 
                    m.id_producto,
                    SUM(CASE 
                        WHEN tm.efecto = 'Carga' THEN m.cantidad_movida
                        WHEN tm.efecto = 'Descarga' THEN -m.cantidad_movida
                        WHEN tm.efecto = 'Transferencia' AND m.id_almacen_destino = @idAlmacenParam THEN m.cantidad_movida
                        WHEN tm.efecto = 'Transferencia' AND m.id_almacen_origen = @idAlmacenParam THEN -m.cantidad_movida
                        ELSE 0
                    END) as saldo_inicial
                FROM adv__movimiento m
                JOIN adv__tipo_movimiento tm ON m.id_tipo_movimiento = tm.id_tipo_movimiento
                JOIN adv__producto p ON m.id_producto = p.id_producto
                WHERE m.fecha < @fechaInicio
            ";

            if (idAlmacen > 0) {
                query += " AND (m.id_almacen_origen = @idAlmacenParam OR m.id_almacen_destino = @idAlmacenParam)";
            }

            if (categoria != "Todas") {
                query += " AND p.categoria = @categoria";
            }

            query += " GROUP BY m.id_producto";

            using (var command = new MySqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                command.Parameters.AddWithValue("@idAlmacenParam", idAlmacen);

                if (categoria != "Todas") {
                    command.Parameters.AddWithValue("@categoria", categoria);
                }

                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int idProducto = reader.GetInt32("id_producto");
                        decimal saldo = reader.GetDecimal("saldo_inicial");
                        saldos[idProducto] = saldo;
                    }
                }
            }
        }

        return saldos;
    }

    private static Dictionary<int, Dictionary<string, string>> ObtenerProductosConUnidadMedida(long idAlmacen, string categoria) {
        var productos = new Dictionary<int, Dictionary<string, string>>();

        using (var connection = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            connection.Open();

            var query = """
                SELECT 
                p.id_producto,
                p.codigo,
                p.nombre,
                p.categoria,
                um.abreviatura as unidad_medida,
                CASE 
                    WHEN p.categoria = 'ProductoTerminado' THEN p.costo_produccion_unitario
                    ELSE p.precio_compra
                END as costo_unitario
                FROM adv__producto p
                JOIN adv__inventario pa ON p.id_producto = pa.id_producto
                JOIN adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
                JOIN adv__unidad_medida um ON dp.id_unidad_medida = um.id_unidad_medida
                """;

            if (idAlmacen > 0) {
                query += " WHERE pa.id_almacen = @idAlmacen";
            }

            if (categoria != "Todas") {
                query += $" {(idAlmacen > 0 ? " AND" : " WHERE")} p.categoria = @categoria";
            }

            using (var command = new MySqlCommand(query, connection)) {
                if (idAlmacen > 0) {
                    command.Parameters.AddWithValue("@idAlmacen", idAlmacen);
                }

                if (categoria != "Todas") {
                    command.Parameters.AddWithValue("@categoria", categoria);
                }

                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        var producto = new Dictionary<string, string>();
                        producto["codigo"] = reader["codigo"].ToString();
                        producto["nombre"] = reader["nombre"].ToString();
                        producto["unidad_medida"] = reader["unidad_medida"].ToString();
                        producto["costo_unitario"] = reader["costo_unitario"].ToString();
                        producto["categoria"] = reader["categoria"].ToString();

                        productos[reader.GetInt32("id_producto")] = producto;
                    }
                }
            }
        }

        return productos;
    }

    private static string ObtenerNombreAlmacen(long idAlmacen) {
        using (var connection = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            connection.Open();

            var query = "SELECT nombre FROM adv__almacen WHERE id_almacen = @idAlmacen";

            using (var command = new MySqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@idAlmacen", idAlmacen);
                return command.ExecuteScalar()?.ToString() ?? "Almacén desconocido";
            }
        }
    }

    private static List<string[]> ProcesarDatosParaReporte(List<Dictionary<string, object>> movimientos,
                                                     Dictionary<int, decimal> saldosIniciales,
                                                     Dictionary<int, Dictionary<string, string>> productos,
                                                     DateTime fechaInicio, DateTime fechaFin) {
        var filas = new List<string[]>();

        // Agrupar movimientos por producto
        var movimientosPorProducto = movimientos
            .GroupBy(m => m["id_producto"])
            .ToDictionary(g => (int) g.Key, g => g.ToList());

        // Procesar cada producto
        foreach (var productoId in productos.Keys) {
            var productoInfo = productos[productoId];
            decimal saldoInicial = saldosIniciales.ContainsKey(productoId) ? saldosIniciales[productoId] : 0;
            decimal entradas = 0;
            decimal salidas = 0;

            if (movimientosPorProducto.ContainsKey(productoId)) {
                foreach (var movimiento in movimientosPorProducto[productoId]) {
                    decimal cantidad = Convert.ToDecimal(movimiento["cantidad_movida"]);
                    string efecto = movimiento["efecto"].ToString();

                    if (efecto == "Carga") {
                        entradas += cantidad;
                    } else if (efecto == "Descarga") {
                        salidas += cantidad;
                    }
                    // Para transferencias, dependemos de si es origen o destino
                }
            }

            decimal saldoFinal = saldoInicial + entradas - salidas;
            decimal costoUnitario = decimal.Parse(productoInfo["costo_unitario"]);

            // Crear fila para el reporte
            var fila = new string[]
            {
            productoInfo["codigo"],
            productoInfo["nombre"],
            productoInfo["unidad_medida"],
            saldoInicial.ToString("N2"),
            entradas.ToString("N2"),
            salidas.ToString("N2"),
            saldoFinal.ToString("N2"),
            costoUnitario.ToString("N2")
            };

            filas.Add(fila);
        }

        return filas;
    }

    #endregion

    public static void GenerarReporteVentas(DateTime fecha, List<string[]> filas,
                                      string cliente = "Todos los clientes",
                                      string usuario = "Todos los usuarios",
                                      string producto = "Todos los productos",
                                      bool mostrar = true) {
        // Crear un nuevo documento PDF
        var documento = new PdfDocument();
        documento.Info.Title = "Reporte de Ventas";
        documento.Info.Author = "aDVanceERP";

        // Configurar márgenes (en puntos, 1 punto = 1/72 pulgadas)
        const int margenIzquierdo = 40;
        const int margenDerecho = 40;
        const int margenSuperior = 40;
        const int margenInferior = 40;
        const int alturaEncabezado = 120;
        const int alturaPie = 30;
        const int alturaFila = 15;
        const int maxFilasPorPagina = 30;

        // Fuentes
        var fontTitulo = new XFont("Arial", 14, XFontStyleEx.Bold);
        var fontSubtitulo = new XFont("Arial", 10, XFontStyleEx.Regular);
        var fontContenido = new XFont("Arial", 9, XFontStyleEx.Regular);
        var fontEncabezado = new XFont("Arial", 9, XFontStyleEx.Bold);

        var culture = new CultureInfo("es-ES");
        decimal sumaTotal = 0;
        int paginaActual = 1;
        int filasProcesadas = 0;
        PdfPage pagina = null;
        XGraphics gfx = null;
        double yPoint = 0;

        while (filasProcesadas < filas.Count) {
            // Crear nueva página si es necesario
            if (pagina == null || filasProcesadas % maxFilasPorPagina == 0) {
                pagina = documento.AddPage();
                pagina.Size = PageSize.Letter;
                gfx = XGraphics.FromPdfPage(pagina);
                yPoint = margenSuperior;

                // Dibujar encabezado en cada página
                DibujarEncabezadoVentas(gfx, pagina, fontTitulo, fontSubtitulo,
                    fecha, cliente, usuario, producto,
                    margenIzquierdo, margenDerecho, ref yPoint);

                // Dibujar encabezados de tabla
                DibujarEncabezadosTablaVentas(gfx, pagina, fontEncabezado,
                    margenIzquierdo, margenDerecho, ref yPoint);
            }

            // Dibujar filas de datos
            int filasEnPagina = Math.Min(maxFilasPorPagina, filas.Count - filasProcesadas);
            for (int i = 0; i < filasEnPagina; i++) {
                var fila = filas[filasProcesadas];
                DibujarFilaVenta(gfx, fontContenido, fila, filasProcesadas + 1,
                                margenIzquierdo, ref yPoint, alturaFila, culture, ref sumaTotal);
                filasProcesadas++;
            }

            // Dibujar total parcial si es última página o hay muchas filas
            if (filasProcesadas == filas.Count || filasProcesadas % maxFilasPorPagina == 0) {
                DibujarPiePaginaVentas(gfx, pagina, fontEncabezado, fontSubtitulo,
                                      sumaTotal, margenIzquierdo, margenDerecho,
                                      margenInferior, ref yPoint, paginaActual,
                                      documento.Pages.Count, culture);
                paginaActual++;
            }
        }

        // Guardar el documento
        var nombreArchivo = $"ventas-productos-{fecha:yyyy-MM-dd}.pdf";

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

    // Métodos auxiliares para GenerarReporteVentas
    private static void DibujarEncabezadoVentas(XGraphics gfx, PdfPage pagina, XFont fontTitulo, XFont fontSubtitulo,
                                              DateTime fecha, string cliente, string usuario, string producto,
                                              int margenIzquierdo, int margenDerecho, ref double yPoint) {
        // Agregar título
        gfx.DrawString("VENTAS POR ARTÍCULO", fontTitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width - margenIzquierdo - margenDerecho, 20),
            XStringFormats.TopLeft);
        yPoint += 25;

        // Agregar información del reporte
        gfx.DrawString($"Fecha: {fecha.ToShortDateString()}", fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width, 15), XStringFormats.TopLeft);
        yPoint += 15;

        gfx.DrawString($"Cliente: {cliente}", fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width, 15), XStringFormats.TopLeft);
        yPoint += 15;

        gfx.DrawString($"Usuario: {usuario}", fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width, 15), XStringFormats.TopLeft);
        yPoint += 15;

        gfx.DrawString($"Producto: {producto}", fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width, 15), XStringFormats.TopLeft);
        yPoint += 20;
    }

    private static void DibujarEncabezadosTablaVentas(XGraphics gfx, PdfPage pagina, XFont fontEncabezado, int margenIzquierdo, int margenDerecho, ref double yPoint) {
        // Definir anchos de columnas
        var anchoCodigo = 50;
        var anchoProducto = 200;
        var anchoUM = 40;
        var anchoPrecioVentaFinal = 90;
        var anchoCantidad = 60;
        var anchoTotal = 90;

        // Crear encabezados de la tabla
        gfx.DrawString("Código", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, anchoCodigo, 15), XStringFormats.TopLeft);

        gfx.DrawString("Producto", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo + anchoCodigo, yPoint, anchoProducto, 15), XStringFormats.TopLeft);

        gfx.DrawString("UM", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo + anchoCodigo + anchoProducto, yPoint, anchoUM, 15), XStringFormats.TopCenter);

        gfx.DrawString("Precio Venta Final", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo + anchoCodigo + anchoProducto + anchoUM, yPoint, anchoPrecioVentaFinal, 15), XStringFormats.TopRight);

        gfx.DrawString("Cantidad", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo + anchoCodigo + anchoProducto + anchoUM + anchoPrecioVentaFinal, yPoint, anchoCantidad, 15), XStringFormats.TopRight);

        gfx.DrawString("Total", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo + anchoCodigo + anchoProducto + anchoUM + anchoPrecioVentaFinal + anchoCantidad, yPoint, anchoTotal, 15), XStringFormats.TopRight);

        yPoint += 15;
        gfx.DrawLine(XPens.Black, margenIzquierdo, yPoint, pagina.Width - margenDerecho, yPoint);
        yPoint += 5;
    }

    private static void DibujarFilaVenta(XGraphics gfx, XFont fontContenido, string[] fila, int numeroFila,
                                       int margenIzquierdo, ref double yPoint, int alturaFila,
                                       CultureInfo culture, ref decimal sumaTotal) {
        // Definir anchos de columnas (consistentes con los encabezados)
        var anchoCodigo = 50;
        var anchoProducto = 200;
        var anchoUM = 40;
        var anchoPrecioVentaFinal = 90;
        var anchoCantidad = 60;
        var anchoTotal = 90;

        // Número de fila
        gfx.DrawString(numeroFila.ToString(), fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, anchoCodigo, alturaFila), XStringFormats.TopLeft);

        // Producto
        gfx.DrawString(fila[1], fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoCodigo, yPoint, anchoProducto, alturaFila), XStringFormats.TopLeft);

        // UM
        gfx.DrawString(fila.Length > 2 ? fila[2] : "", fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoCodigo + anchoProducto, yPoint, anchoUM, alturaFila), XStringFormats.TopCenter);

        // Precio Venta Final
        var precioBase = decimal.TryParse(fila.Length > 3 ? fila[3] : "0", NumberStyles.Any, CultureInfo.InvariantCulture, out var valorBase)
            ? valorBase
            : 0.00m;
        gfx.DrawString(precioBase.ToString("N2", CultureInfo.InvariantCulture), fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoCodigo + anchoProducto + anchoUM, yPoint, anchoPrecioVentaFinal, alturaFila), XStringFormats.TopRight);

        // Cantidad
        gfx.DrawString(fila.Length > 4 ? fila[4] : "0", fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoCodigo + anchoProducto + anchoUM + anchoPrecioVentaFinal, yPoint, anchoCantidad, alturaFila), XStringFormats.TopRight);

        // Total
        var total = decimal.TryParse(fila.Length > 5 ? fila[5] : "0", NumberStyles.Any, CultureInfo.InvariantCulture, out var valorTotal)
            ? valorTotal
            : precioBase * (fila.Length > 4 ? decimal.Parse(fila[4]) : 0);
        gfx.DrawString(total.ToString("N2", CultureInfo.InvariantCulture), fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoCodigo + anchoProducto + anchoUM + anchoPrecioVentaFinal + anchoCantidad, yPoint, anchoTotal, alturaFila), XStringFormats.TopRight);

        sumaTotal += total;
        yPoint += alturaFila;
    }

    private static void DibujarPiePaginaVentas(XGraphics gfx, PdfPage pagina, XFont fontEncabezado, XFont fontSubtitulo,
                                             decimal sumaTotal, int margenIzquierdo, int margenDerecho,
                                             int margenInferior, ref double yPoint, int paginaActual,
                                             int totalPaginas, CultureInfo culture) {
        // Definir anchos de columnas (consistentes con los encabezados)
        var anchoCodigo = 50;
        var anchoProducto = 200;
        var anchoUM = 40;
        var anchoPrecioVentaFinal = 90;
        var anchoCantidad = 60;
        var anchoTotal = 90;

        // Dibujar línea antes del total
        yPoint += 5;
        gfx.DrawLine(XPens.Black, margenIzquierdo, yPoint, pagina.Width - margenDerecho, yPoint);
        yPoint += 5;

        // Agregar fila con los totales
        gfx.DrawString("Total", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo + anchoCodigo + anchoProducto + anchoUM + anchoPrecioVentaFinal, yPoint, anchoCantidad, 15), XStringFormats.TopRight);

        gfx.DrawString(sumaTotal.ToString("N2", CultureInfo.InvariantCulture) + " $", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo + anchoCodigo + anchoProducto + anchoUM + anchoPrecioVentaFinal + anchoCantidad, yPoint, anchoTotal, 15), XStringFormats.TopRight);
        yPoint += 15;

        // Pie de página
        var fechaGeneracion = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        var textoPie = $"{fechaGeneracion} - Página {paginaActual} de {totalPaginas}";

        gfx.DrawString(textoPie, fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, pagina.Height - margenInferior, pagina.Width - margenIzquierdo - margenDerecho, 20),
            XStringFormats.BottomRight);
    }

    public static void GenerarEntradaMercancias(DateTime fecha, List<string[]> filas,
                                          string proveedor = "",
                                          string documentoExterno = "",
                                          string direccion = "",
                                          string nif = "") {
        // Crear un nuevo documento PDF
        var documento = new PdfDocument();
        documento.Info.Title = "Entrada de Mercancías";
        documento.Info.Author = "aDVanceERP";

        // Configurar márgenes
        const int margenIzquierdo = 40;
        const int margenDerecho = 40;
        const int margenSuperior = 40;
        const int margenInferior = 40;
        const int alturaEncabezado = 150;
        const int alturaPie = 30;
        const int alturaFila = 15;
        const int maxFilasPorPagina = 25;

        // Fuentes
        var fontTitulo = new XFont("Arial", 16, XFontStyleEx.Bold);
        var fontSubtitulo = new XFont("Arial", 10, XFontStyleEx.Regular);
        var fontContenido = new XFont("Arial", 9, XFontStyleEx.Regular);
        var fontEncabezado = new XFont("Arial", 9, XFontStyleEx.Bold);
        var fontNegrita = new XFont("Arial", 9, XFontStyleEx.Bold);

        var culture = new CultureInfo("es-ES");
        decimal subtotal = 0;
        int paginaActual = 1;
        int filasProcesadas = 0;
        PdfPage pagina = null;
        XGraphics gfx = null;
        double yPoint = 0;

        while (filasProcesadas < filas.Count) {
            // Crear nueva página si es necesario
            if (pagina == null || filasProcesadas % maxFilasPorPagina == 0) {
                pagina = documento.AddPage();
                pagina.Size = PageSize.Letter;
                gfx = XGraphics.FromPdfPage(pagina);
                yPoint = margenSuperior;

                // Dibujar encabezado en cada página
                DibujarEncabezadoEntradaMercancias(gfx, pagina, fontTitulo, fontContenido, fontNegrita,
                    fecha, proveedor, documentoExterno, direccion, nif,
                    margenIzquierdo, margenDerecho, ref yPoint);

                // Dibujar encabezados de tabla
                DibujarEncabezadosTablaEntrada(gfx, pagina, fontEncabezado,
                    margenIzquierdo, margenDerecho, ref yPoint);
            }

            // Dibujar filas de datos
            int filasEnPagina = Math.Min(maxFilasPorPagina, filas.Count - filasProcesadas);
            for (int i = 0; i < filasEnPagina; i++) {
                var fila = filas[filasProcesadas];
                DibujarFilaEntrada(gfx, fontContenido, fila, filasProcesadas + 1,
                                 margenIzquierdo, ref yPoint, alturaFila, culture, ref subtotal);
                filasProcesadas++;
            }

            // Dibujar total parcial si es última página o hay muchas filas
            if (filasProcesadas == filas.Count || filasProcesadas % maxFilasPorPagina == 0) {
                DibujarPiePaginaEntrada(gfx, pagina, fontNegrita, fontContenido, fontSubtitulo,
                                       subtotal, margenIzquierdo, margenDerecho,
                                       margenInferior, ref yPoint, paginaActual,
                                       documento.Pages.Count, culture);
                paginaActual++;
            }
        }

        // Guardar el documento
        var nombreArchivo = $"entrada-mercancias-{fecha:yyyyMMdd}-{documento}.pdf";

        if (documento.Pages.Count <= 0)
            return;

        documento.Save(nombreArchivo);

        // Mostrar el documento PDF al usuario
        Process.Start(new ProcessStartInfo {
            FileName = nombreArchivo,
            UseShellExecute = true
        });
    }

    // Métodos auxiliares para GenerarEntradaMercancias
    private static void DibujarEncabezadoEntradaMercancias(XGraphics gfx, PdfPage pagina, XFont fontTitulo,
                                                         XFont fontContenido, XFont fontNegrita,
                                                         DateTime fecha, string proveedor,
                                                         string documento, string direccion, string nif,
                                                         int margenIzquierdo, int margenDerecho, ref double yPoint) {
        // Agregar título
        gfx.DrawString("ENTRADA DE MERCANCÍAS", fontTitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width - margenIzquierdo - margenDerecho, 20),
            XStringFormats.TopLeft);
        yPoint += 25;

        // Información de la empresa
        gfx.DrawString("Empresa:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, 100, 15), XStringFormats.TopLeft);
        yPoint += 15;

        gfx.DrawString("Dirección:", fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width, 15), XStringFormats.TopLeft);
        yPoint += 20;

        // Encabezado de información del proveedor
        var anchoColumna1 = 100;
        var anchoColumna2 = 150;
        var anchoColumna3 = 100;
        var anchoColumna4 = 150;

        // Fila 1
        gfx.DrawString("Proveedor:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, anchoColumna1, 15), XStringFormats.TopLeft);

        gfx.DrawString(proveedor, fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1, yPoint, anchoColumna2, 15), XStringFormats.TopLeft);

        gfx.DrawString("Número de documento:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2, yPoint, anchoColumna3, 15), XStringFormats.TopLeft);

        gfx.DrawString(documento, fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2 + anchoColumna3, yPoint, anchoColumna4, 15), XStringFormats.TopLeft);
        yPoint += 15;

        // Fila 2
        gfx.DrawString("Dirección:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, anchoColumna1, 15), XStringFormats.TopLeft);

        gfx.DrawString(direccion, fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1, yPoint, anchoColumna2, 15), XStringFormats.TopLeft);

        gfx.DrawString("Documento externo:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2, yPoint, anchoColumna3, 15), XStringFormats.TopLeft);
        yPoint += 15;

        // Fila 3
        gfx.DrawString("NIT:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, anchoColumna1, 15), XStringFormats.TopLeft);

        gfx.DrawString(nif, fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1, yPoint, anchoColumna2, 15), XStringFormats.TopLeft);

        gfx.DrawString("Fecha:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2, yPoint, anchoColumna3, 15), XStringFormats.TopLeft);

        gfx.DrawString(fecha.ToShortDateString(), fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2 + anchoColumna3, yPoint, anchoColumna4, 15), XStringFormats.TopLeft);
        yPoint += 15;

        // Fila 4
        gfx.DrawString("Vencimiento:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2, yPoint, anchoColumna3, 15), XStringFormats.TopLeft);

        gfx.DrawString(fecha.ToShortDateString(), fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2 + anchoColumna3, yPoint, anchoColumna4, 15), XStringFormats.TopLeft);
        yPoint += 20;
    }

    private static void DibujarEncabezadosTablaEntrada(XGraphics gfx, PdfPage pagina, XFont fontEncabezado, int margenIzquierdo, int margenDerecho, ref double yPoint) {
        // Definir anchos de columnas para la tabla de productos
        var anchoNumero = 30;
        var anchoProducto = 220;
        var anchoCantidad = 50;
        var anchoPrecio = 60;
        var anchoImpuesto = 50;
        var anchoDescuento = 60;
        var anchoTotal = 60;

        // Verificar y ajustar anchos si es necesario
        var anchoDisponible = pagina.Width - margenIzquierdo - margenDerecho;
        var sumaAnchos = anchoNumero + anchoProducto + anchoCantidad + anchoPrecio +
                         anchoImpuesto + anchoDescuento + anchoTotal;

        if (sumaAnchos > anchoDisponible) {
            // Reducir el ancho del producto proporcionalmente
            anchoProducto -= (sumaAnchos - (int) anchoDisponible.Value);
            anchoProducto = Math.Max(anchoProducto, 100); // Mínimo 100 puntos
        }

        // Encabezados de la tabla de productos
        gfx.DrawString("#", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, anchoNumero, 15), XStringFormats.TopCenter);

        gfx.DrawString("Producto", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero, yPoint, anchoProducto, 15), XStringFormats.TopLeft);

        gfx.DrawString("Cantidad", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto, yPoint, anchoCantidad, 15), XStringFormats.TopRight);

        gfx.DrawString("Precio", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad, yPoint, anchoPrecio, 15), XStringFormats.TopRight);

        gfx.DrawString("Impuesto", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad + anchoPrecio, yPoint, anchoImpuesto, 15), XStringFormats.TopRight);

        gfx.DrawString("Descuento", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad + anchoPrecio + anchoImpuesto, yPoint, anchoDescuento, 15), XStringFormats.TopRight);

        gfx.DrawString("Total", fontEncabezado, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad + anchoPrecio + anchoImpuesto + anchoDescuento, yPoint, anchoTotal, 15), XStringFormats.TopRight);

        yPoint += 15;
        gfx.DrawLine(XPens.Black, margenIzquierdo, yPoint, pagina.Width - margenDerecho, yPoint);
        yPoint += 5;
    }

    private static void DibujarFilaEntrada(XGraphics gfx, XFont fontContenido, string[] fila, int numeroFila,
                                         int margenIzquierdo, ref double yPoint, int alturaFila,
                                         CultureInfo culture, ref decimal subtotal) {
        // Definir anchos de columnas (consistentes con los encabezados)
        var anchoNumero = 30;
        var anchoProducto = 220;
        var anchoCantidad = 50;
        var anchoPrecio = 60;
        var anchoImpuesto = 50;
        var anchoDescuento = 60;
        var anchoTotal = 60;

        // Número
        gfx.DrawString(numeroFila.ToString(), fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, anchoNumero, alturaFila), XStringFormats.TopCenter);

        // Producto
        gfx.DrawString(fila[1], fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero, yPoint, anchoProducto, alturaFila), XStringFormats.TopLeft);

        // Cantidad
        gfx.DrawString(fila[2], fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto, yPoint, anchoCantidad, alturaFila), XStringFormats.TopRight);

        // Precio
        var precio = decimal.TryParse(fila.Length > 3 ? fila[3] : "0", NumberStyles.Any, CultureInfo.InvariantCulture, out var valorPrecio)
            ? valorPrecio
            : 0.00m;
        gfx.DrawString(precio.ToString("N2", CultureInfo.InvariantCulture), fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad, yPoint, anchoPrecio, alturaFila), XStringFormats.TopRight);

        // Impuesto
        var impuesto = decimal.TryParse(fila.Length > 4 ? fila[4] : "0", NumberStyles.Any, CultureInfo.InvariantCulture, out var valorImpuesto)
            ? valorImpuesto
            : 0.00m;
        gfx.DrawString(impuesto.ToString("N2", CultureInfo.InvariantCulture), fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad + anchoPrecio, yPoint, anchoImpuesto, alturaFila), XStringFormats.TopRight);

        // Descuento
        var descuento = fila.Length > 5 ? fila[5] : "0.00%";
        gfx.DrawString(descuento, fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad + anchoPrecio + anchoImpuesto, yPoint, anchoDescuento, alturaFila), XStringFormats.TopRight);

        // Total
        var total = decimal.TryParse(fila.Length > 6 ? fila[6] : "0", NumberStyles.Any, CultureInfo.InvariantCulture, out var valorTotal)
            ? valorTotal
            : precio * decimal.Parse(fila[2]);
        gfx.DrawString(total.ToString("N2", CultureInfo.InvariantCulture), fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad + anchoPrecio + anchoImpuesto + anchoDescuento, yPoint, anchoTotal, alturaFila), XStringFormats.TopRight);

        subtotal += total;
        yPoint += alturaFila;
    }

    private static void DibujarPiePaginaEntrada(XGraphics gfx, PdfPage pagina, XFont fontNegrita, XFont fontContenido,
                                              XFont fontSubtitulo, decimal subtotal, int margenIzquierdo,
                                              int margenDerecho, int margenInferior, ref double yPoint,
                                              int paginaActual, int totalPaginas, CultureInfo culture) {
        // Definir anchos de columnas (consistentes con los encabezados)
        var anchoNumero = 30;
        var anchoProducto = 220;
        var anchoCantidad = 50;
        var anchoPrecio = 60;
        var anchoImpuesto = 50;
        var anchoDescuento = 60;
        var anchoTotal = 60;

        // Totales
        yPoint += 10;
        gfx.DrawLine(XPens.Black, margenIzquierdo, yPoint, pagina.Width - margenDerecho, yPoint);
        yPoint += 10;

        // Descuento general
        gfx.DrawString("Descuento:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad + anchoPrecio + anchoImpuesto, yPoint, anchoDescuento, 15), XStringFormats.TopRight);

        gfx.DrawString("0.00 $", fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad + anchoPrecio + anchoImpuesto + anchoDescuento, yPoint, anchoTotal, 15), XStringFormats.TopRight);
        yPoint += 15;

        // Subtotal
        gfx.DrawString("Subtotal:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad + anchoPrecio + anchoImpuesto, yPoint, anchoDescuento, 15), XStringFormats.TopRight);

        gfx.DrawString(subtotal.ToString("N2", CultureInfo.InvariantCulture) + " $", fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad + anchoPrecio + anchoImpuesto + anchoDescuento, yPoint, anchoTotal, 15), XStringFormats.TopRight);
        yPoint += 15;

        // Total
        gfx.DrawString("Total:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad + anchoPrecio + anchoImpuesto, yPoint, anchoDescuento, 15), XStringFormats.TopRight);

        gfx.DrawString(subtotal.ToString("N2", CultureInfo.InvariantCulture) + " $", fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoNumero + anchoProducto + anchoCantidad + anchoPrecio + anchoImpuesto + anchoDescuento, yPoint, anchoTotal, 15), XStringFormats.TopRight);
        yPoint += 20;

        // Pie de página
        var fechaGeneracion = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        var textoPie = $"{fechaGeneracion} - Página {paginaActual} de {totalPaginas}";

        gfx.DrawString(textoPie, fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, pagina.Height - margenInferior, pagina.Width - margenIzquierdo - margenDerecho, 20),
            XStringFormats.BottomRight);
    }

    public static void GenerarFacturaVenta(DateTime fecha, List<string[]> productos,
                                     string[] datosCliente, string numeroFactura,
                                     string estadoPago, string metodoPago,
                                     decimal cantidadPagada) {
        // Crear un nuevo documento PDF
        var documento = new PdfDocument();
        documento.Info.Title = $"Factura {numeroFactura}";
        documento.Info.Author = "aDVanceERP";

        // Configurar márgenes
        const int margenIzquierdo = 40;
        const int margenDerecho = 40;
        const int margenSuperior = 40;
        const int margenInferior = 40;
        const int alturaEncabezado = 150;
        const int alturaPie = 30;
        const int alturaFila = 15;
        const int maxFilasPorPagina = 20; // Máximo de productos por página

        // Fuentes
        var fontTitulo = new XFont("Arial", 16, XFontStyleEx.Bold);
        var fontSubtitulo = new XFont("Arial", 10, XFontStyleEx.Regular);
        var fontContenido = new XFont("Arial", 9, XFontStyleEx.Regular);
        var fontEncabezado = new XFont("Arial", 9, XFontStyleEx.Bold);
        var fontNegrita = new XFont("Arial", 9, XFontStyleEx.Bold);

        var culture = new CultureInfo("es-ES");
        decimal totalFactura = 0;
        int paginaActual = 1;
        int productosProcesados = 0;
        PdfPage pagina = null;
        XGraphics gfx = null;
        double yPoint = 0;

        while (productosProcesados < productos.Count) {
            // Crear nueva página si es necesario
            if (pagina == null || productosProcesados % maxFilasPorPagina == 0) {
                pagina = documento.AddPage();
                pagina.Size = PageSize.Letter;
                gfx = XGraphics.FromPdfPage(pagina);
                yPoint = margenSuperior;

                // Dibujar encabezado en cada página
                DibujarEncabezadoFactura(gfx, pagina, fontTitulo, fontContenido, fontNegrita,
                    fecha, datosCliente, numeroFactura, estadoPago,
                    margenIzquierdo, margenDerecho, ref yPoint);

                // Dibujar encabezados de tabla
                DibujarEncabezadosTablaFactura(gfx, pagina, fontEncabezado,
                    margenIzquierdo, margenDerecho, ref yPoint);
            }

            // Dibujar filas de productos
            int productosEnPagina = Math.Min(maxFilasPorPagina, productos.Count - productosProcesados);
            for (int i = 0; i < productosEnPagina; i++) {
                var producto = productos[productosProcesados];
                decimal totalLinea = DibujarFilaProducto(gfx, pagina, fontContenido, producto, productosProcesados + 1,
                                                       margenIzquierdo, margenDerecho, ref yPoint, alturaFila, culture);
                totalFactura += totalLinea;
                productosProcesados++;
            }

            // Dibujar totales si es última página o hay muchos productos
            if (productosProcesados == productos.Count || productosProcesados % maxFilasPorPagina == 0) {
                DibujarPiePaginaFactura(gfx, pagina, fontNegrita, fontContenido, fontSubtitulo,
                                      totalFactura, metodoPago, cantidadPagada,
                                      margenIzquierdo, margenDerecho, margenInferior,
                                      ref yPoint, paginaActual, documento.Pages.Count, culture);
                paginaActual++;
            }
        }

        // Guardar el documento
        var nombreArchivo = $"factura-venta-{numeroFactura}.pdf";

        if (documento.Pages.Count <= 0)
            return;

        documento.Save(nombreArchivo);

        // Mostrar el documento PDF al usuario
        Process.Start(new ProcessStartInfo {
            FileName = nombreArchivo,
            UseShellExecute = true
        });
    }

    // Métodos auxiliares para GenerarFacturaVenta
    private static void DibujarEncabezadoFactura(XGraphics gfx, PdfPage pagina, XFont fontTitulo,
                                               XFont fontContenido, XFont fontNegrita,
                                               DateTime fecha, string[] datosCliente,
                                               string numeroFactura, string estadoPago,
                                               int margenIzquierdo, int margenDerecho, ref double yPoint) {
        var anchoColumna1 = 90;
        var anchoColumna2 = 90;
        var anchoColumna3 = 60;
        var anchoColumna4 = 200;

        gfx.DrawString("Factura N°:", fontNegrita, XBrushes.Black,
            new XRect(pagina.Width - margenDerecho - anchoColumna1 - anchoColumna2 + 30, yPoint, anchoColumna1, 15), XStringFormats.TopLeft);
        gfx.DrawString(numeroFactura, fontContenido, XBrushes.Black,
            new XRect(pagina.Width - margenDerecho - anchoColumna2 - 10, yPoint, anchoColumna2, 15), XStringFormats.TopLeft);
        yPoint += 30;

        // Agregar título
        gfx.DrawString("FACTURA", fontTitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width - margenIzquierdo - margenDerecho, 20),
            XStringFormats.TopCenter);
        yPoint += 50;

        // Fila 1 - Fecha, Vencimiento y estado del pago
        gfx.DrawString("Fecha:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, anchoColumna1, 15), XStringFormats.TopLeft);

        gfx.DrawString(fecha.ToShortDateString(), fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1, yPoint, anchoColumna2, 15), XStringFormats.TopLeft);
        yPoint += 15;

        gfx.DrawString("Vencimiento:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, anchoColumna1, 15), XStringFormats.TopLeft);

        gfx.DrawString(fecha.ToShortDateString(), fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1, yPoint, anchoColumna2, 15), XStringFormats.TopLeft);
        yPoint += 15;

        gfx.DrawString("Estado del pago:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, anchoColumna1, 15), XStringFormats.TopLeft);

        gfx.DrawString(estadoPago, fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1, yPoint, anchoColumna2, 15), XStringFormats.TopLeft);
        yPoint += 30;

        // Fila 2 - Cliente, Dirección y CI o NIT
        gfx.DrawString("Cliente:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2, yPoint - 60, anchoColumna3, 15), XStringFormats.TopLeft);

        gfx.DrawString(datosCliente.Length > 0 ? datosCliente[0] : "Anónimo", fontContenido, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2 + anchoColumna3, yPoint - 60, anchoColumna4, 15), XStringFormats.TopLeft);

        if (datosCliente.Length > 1) {
            gfx.DrawString("Dirección:", fontNegrita, XBrushes.Black,
                new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2, yPoint - 45, anchoColumna3, 15), XStringFormats.TopLeft);

            gfx.DrawString(datosCliente[1], fontContenido, XBrushes.Black,
                new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2 + anchoColumna3, yPoint - 45, anchoColumna2, 15), XStringFormats.TopLeft);
        }

        if (datosCliente.Length > 2) {
            gfx.DrawString("CI o NIT:", fontNegrita, XBrushes.Black,
                new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2, yPoint - 30, anchoColumna3, 15), XStringFormats.TopLeft);

            gfx.DrawString(datosCliente[2], fontContenido, XBrushes.Black,
                new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2 + anchoColumna3, yPoint - 30, anchoColumna2, 15), XStringFormats.TopLeft);
        }
    }

    private static void DibujarEncabezadosTablaFactura(XGraphics gfx, PdfPage pagina, XFont fontEncabezado,
    int margenIzquierdo, int margenDerecho, ref double yPoint) {
        // Definir anchos de columnas para la tabla de productos
        var anchos = CalcularAnchosColumnas(pagina.Width - margenIzquierdo - margenDerecho);

        // Encabezados de la tabla de productos
        double xPos = margenIzquierdo - 11;

        // Columna #
        gfx.DrawString("#", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoNumero, 15), XStringFormats.TopCenter);
        xPos += anchos.AnchoNumero;

        // Columna Descripción
        gfx.DrawString("Descripción de producto", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoDescripcion, 15), XStringFormats.TopLeft);
        xPos += anchos.AnchoDescripcion;

        // Columna Cantidad
        gfx.DrawString("Cantidad", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoCantidad, 15), XStringFormats.TopRight);
        xPos += anchos.AnchoCantidad;

        // Columna Precio
        gfx.DrawString("Precio", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoPrecio, 15), XStringFormats.TopRight);
        xPos += anchos.AnchoPrecio;

        // Columna Impuesto
        gfx.DrawString("Impuesto", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoImpuesto, 15), XStringFormats.TopRight);
        xPos += anchos.AnchoImpuesto;

        // Columna Descuento
        gfx.DrawString("Descuento", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoDescuento, 15), XStringFormats.TopRight);
        xPos += anchos.AnchoDescuento;

        // Columna Total
        gfx.DrawString("Total", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoTotal, 15), XStringFormats.TopRight);

        yPoint += 15;
        gfx.DrawLine(XPens.Black, margenIzquierdo, yPoint, pagina.Width - margenDerecho, yPoint);
        yPoint += 5;
    }

    private static decimal DibujarFilaProducto(XGraphics gfx, PdfPage pagina, XFont fontContenido, string[] producto,
                                             int numeroFila, int margenIzquierdo, int margenDerecho, ref double yPoint,
                                             int alturaFila, CultureInfo culture) {
        var anchos = CalcularAnchosColumnas(pagina.Width - margenIzquierdo - margenDerecho);
        double xPos = margenIzquierdo - 11;

        // Número
        gfx.DrawString(numeroFila.ToString(), fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoNumero, alturaFila), XStringFormats.TopCenter);
        xPos += anchos.AnchoNumero;

        // Descripción
        gfx.DrawString(producto[1], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoDescripcion, alturaFila), XStringFormats.TopLeft);
        xPos += anchos.AnchoDescripcion;

        // Cantidad
        gfx.DrawString(producto[2], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoCantidad, alturaFila), XStringFormats.TopRight);
        xPos += anchos.AnchoCantidad;

        // Precio
        var precio = decimal.TryParse(producto.Length > 3 ? producto[3] : "0", NumberStyles.Any, CultureInfo.InvariantCulture, out var valorPrecio)
            ? valorPrecio
            : 0.00m;
        gfx.DrawString(precio.ToString("N2", CultureInfo.InvariantCulture), fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoPrecio, alturaFila), XStringFormats.TopRight);
        xPos += anchos.AnchoPrecio;

        // Impuesto
        gfx.DrawString(producto.Length > 4 ? producto[4] : "—", fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoImpuesto, alturaFila), XStringFormats.TopCenter);
        xPos += anchos.AnchoImpuesto;

        // Descuento
        var descuento = producto.Length > 5 ? producto[5] : "0.00%";
        gfx.DrawString(descuento, fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoDescuento, alturaFila), XStringFormats.TopRight);
        xPos += anchos.AnchoDescuento;

        // Total
        var total = decimal.TryParse(producto.Length > 6 ? producto[6] : "0", NumberStyles.Any, CultureInfo.InvariantCulture, out var valorTotal)
            ? valorTotal
            : precio * decimal.Parse(producto[2]);
        gfx.DrawString(total.ToString("N2", CultureInfo.InvariantCulture), fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoTotal, alturaFila), XStringFormats.TopRight);

        yPoint += alturaFila;
        return total;
    }

    // Estructura para mantener consistentes los anchos de columna
    private static (int AnchoNumero, int AnchoDescripcion, int AnchoCantidad,
                   int AnchoPrecio, int AnchoImpuesto, int AnchoDescuento,
                   int AnchoTotal) CalcularAnchosColumnas(double anchoDisponible) {
        // Valores base para los anchos
        int anchoNumero = 30;
        int anchoDescripcion = 250;
        int anchoCantidad = 60;
        int anchoPrecio = 70;
        int anchoImpuesto = 60;
        int anchoDescuento = 70;
        int anchoTotal = 80;

        // Calcular suma total de anchos
        int sumaAnchos = anchoNumero + anchoDescripcion + anchoCantidad +
                        anchoPrecio + anchoImpuesto + anchoDescuento + anchoTotal;

        // Ajustar si excede el ancho disponible
        if (sumaAnchos > anchoDisponible) {
            // Calcular factor de reducción
            double factor = anchoDisponible / sumaAnchos;

            // Aplicar factor a cada columna (excepto número y cantidades)
            anchoDescripcion = (int) (anchoDescripcion * factor);
            anchoPrecio = (int) (anchoPrecio * factor);
            anchoImpuesto = (int) (anchoImpuesto * factor);
            anchoDescuento = (int) (anchoDescuento * factor);
            anchoTotal = (int) (anchoTotal * factor);

            // Asegurar mínimos
            anchoDescripcion = Math.Max(anchoDescripcion, 150);
            anchoPrecio = Math.Max(anchoPrecio, 50);
            anchoImpuesto = Math.Max(anchoImpuesto, 50);
            anchoDescuento = Math.Max(anchoDescuento, 50);
            anchoTotal = Math.Max(anchoTotal, 60);
        }

        return (anchoNumero, anchoDescripcion, anchoCantidad,
                anchoPrecio, anchoImpuesto, anchoDescuento,
                anchoTotal);
    }

    private static void DibujarPiePaginaFactura(XGraphics gfx, PdfPage pagina, XFont fontNegrita,
                                          XFont fontContenido, XFont fontSubtitulo,
                                          decimal totalFactura, string metodoPago,
                                          decimal cantidadPagada, int margenIzquierdo,
                                          int margenDerecho, int margenInferior,
                                          ref double yPoint, int paginaActual,
                                          int totalPaginas, CultureInfo culture) {
        var anchos = CalcularAnchosColumnas(pagina.Width - margenIzquierdo - margenDerecho);
        double xPos = margenIzquierdo + anchos.AnchoNumero + anchos.AnchoDescripcion;

        // Línea separadora antes de los totales
        yPoint += 5;
        gfx.DrawLine(XPens.Black, margenIzquierdo, yPoint, pagina.Width - margenDerecho, yPoint);
        yPoint += 10;

        // Total factura - Alineado con la columna "Total" de los productos
        double xPosTotales = margenIzquierdo + anchos.AnchoNumero + anchos.AnchoDescripcion +
                            anchos.AnchoCantidad + anchos.AnchoPrecio + anchos.AnchoImpuesto - 11;

        // Etiqueta "Total"
        gfx.DrawString("Total", fontNegrita, XBrushes.Black,
            new XRect(xPosTotales, yPoint, anchos.AnchoDescuento, 15), XStringFormats.TopRight);

        // Valor total - Alineado con la columna "Total" de los productos
        double xPosValorTotal = xPosTotales + anchos.AnchoDescuento;
        gfx.DrawString(totalFactura.ToString("N2", CultureInfo.InvariantCulture) + " $", fontNegrita, XBrushes.Black,
            new XRect(xPosValorTotal, yPoint, anchos.AnchoTotal, 15), XStringFormats.TopRight);

        yPoint += 20;

        // Información de pago (solo en la última página)
        if (paginaActual == totalPaginas) {
            // Restablecer posición X para la información de pago
            xPos = margenIzquierdo;

            // Método de pago
            gfx.DrawString("Método de pago:", fontNegrita, XBrushes.Black,
                new XRect(xPos, yPoint, 120, 15), XStringFormats.TopLeft);

            gfx.DrawString(metodoPago, fontContenido, XBrushes.Black,
                new XRect(xPos + 120, yPoint, 150, 15), XStringFormats.TopLeft);
            yPoint += 15;

            // Efectivo si aplica
            if (metodoPago.Equals("Efectivo", StringComparison.OrdinalIgnoreCase)) {
                gfx.DrawString("Efectivo:", fontNegrita, XBrushes.Black,
                    new XRect(xPos, yPoint, 120, 15), XStringFormats.TopLeft);

                gfx.DrawString(cantidadPagada.ToString("N2", CultureInfo.InvariantCulture) + " $", fontContenido, XBrushes.Black,
                    new XRect(xPos + 120, yPoint, 150, 15), XStringFormats.TopLeft);
                yPoint += 15;
            }

            // Cantidad pagada
            gfx.DrawString("Cantidad pagada:", fontNegrita, XBrushes.Black,
                new XRect(xPos, yPoint, 120, 15), XStringFormats.TopLeft);

            gfx.DrawString(cantidadPagada.ToString("N2", CultureInfo.InvariantCulture) + " $", fontContenido, XBrushes.Black,
                new XRect(xPos + 120, yPoint, 150, 15), XStringFormats.TopLeft);
            yPoint += 15;

            // Cantidad adeudada
            decimal cantidadAdeudada = totalFactura - cantidadPagada;
            gfx.DrawString("Cantidad adeudada:", fontNegrita, XBrushes.Black,
                new XRect(xPos, yPoint, 120, 15), XStringFormats.TopLeft);

            gfx.DrawString(cantidadAdeudada.ToString("N2", CultureInfo.InvariantCulture) + " $", fontContenido, XBrushes.Black,
                new XRect(xPos + 120, yPoint, 150, 15), XStringFormats.TopLeft);
            yPoint += 20;
        }

        // Pie de página
        var fechaGeneracion = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        var textoPie = $"Generado con aDVanceERP - {fechaGeneracion} - Página {paginaActual} de {totalPaginas}";

        gfx.DrawString(textoPie, fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, pagina.Height - margenInferior, pagina.Width - margenIzquierdo - margenDerecho, 20),
            XStringFormats.BottomLeft);
    }

    #region Reporte de cierre de caja

    public static void GenerarReporteCierreCaja(DateTime fechaApertura, DateTime fechaCierre, decimal saldoInicial, decimal saldoFinal, int idCaja, List<string[]> movimientos, string estadoCaja = "Cerrada", bool mostrar = true) {
        // Crear un nuevo documento PDF
        var documento = new PdfDocument();
        documento.Info.Title = "Reporte de Cierre de Caja";
        documento.Info.Author = "aDVanceERP";

        // Configurar márgenes (en puntos, 1 punto = 1/72 pulgadas)
        const int margenIzquierdo = 40;
        const int margenDerecho = 40;
        const int margenSuperior = 40;
        const int margenInferior = 40;
        const int alturaEncabezado = 120;
        const int alturaPie = 30;
        const int alturaFila = 15;
        const int maxFilasPorPagina = 30;

        // Fuentes
        var fontTitulo = new XFont("Arial", 14, XFontStyleEx.Bold);
        var fontSubtitulo = new XFont("Arial", 10, XFontStyleEx.Regular);
        var fontContenido = new XFont("Arial", 9, XFontStyleEx.Regular);
        var fontEncabezado = new XFont("Arial", 9, XFontStyleEx.Bold);
        var fontNegrita = new XFont("Arial", 9, XFontStyleEx.Bold);

        var culture = new CultureInfo("es-ES");
        decimal totalIngresos = 0;
        decimal totalEgresos = 0;
        int paginaActual = 1;
        int filasProcesadas = 0;
        PdfPage pagina = null;
        XGraphics gfx = null;
        double yPoint = 0;

        while (filasProcesadas < movimientos.Count) {
            // Crear nueva página si es necesario
            if (pagina == null || filasProcesadas % maxFilasPorPagina == 0) {
                pagina = documento.AddPage();
                pagina.Size = PageSize.Letter;
                gfx = XGraphics.FromPdfPage(pagina);
                yPoint = margenSuperior;

                // Dibujar encabezado en cada página
                DibujarEncabezadoCierreCaja(gfx, pagina, fontTitulo, fontSubtitulo, fontNegrita,
                    idCaja, estadoCaja, fechaApertura, fechaCierre, saldoInicial, saldoFinal,
                    margenIzquierdo, margenDerecho, ref yPoint);

                // Dibujar encabezados de tabla
                DibujarEncabezadosTablaCierreCaja(gfx, pagina, fontEncabezado,
                    margenIzquierdo, margenDerecho, ref yPoint);
            }

            // Dibujar filas de datos
            int filasEnPagina = Math.Min(maxFilasPorPagina, movimientos.Count - filasProcesadas);
            for (int i = 0; i < filasEnPagina; i++) {
                var fila = movimientos[filasProcesadas];
                DibujarFilaMovimiento(gfx, fontContenido, fila, filasProcesadas + 1,
                                    margenIzquierdo, ref yPoint, alturaFila, culture,
                                    ref totalIngresos, ref totalEgresos);
                filasProcesadas++;
            }

            // Dibujar total parcial si es última página o hay muchas filas
            if (filasProcesadas == movimientos.Count || filasProcesadas % maxFilasPorPagina == 0) {
                DibujarPiePaginaCierreCaja(gfx, pagina, fontNegrita, fontContenido, fontSubtitulo,
                                          totalIngresos, totalEgresos, saldoInicial, saldoFinal,
                                          margenIzquierdo, margenDerecho, margenInferior,
                                          ref yPoint, paginaActual, documento.Pages.Count, culture);
                paginaActual++;
            }
        }

        // Guardar el documento
        var nombreArchivo = $"cierre-caja-{idCaja}-{fechaCierre:yyyyMMdd}.pdf";

        if (documento.Pages.Count > 0)
            documento.Save(nombreArchivo);

        // Mostrar el documento PDF al usuario
        if (mostrar)
            Process.Start(new ProcessStartInfo {
                FileName = nombreArchivo,
                UseShellExecute = true
            });
    }

    // Métodos auxiliares para GenerarReporteCierreCaja
    private static void DibujarEncabezadoCierreCaja(XGraphics gfx, PdfPage pagina, XFont fontTitulo,
        XFont fontSubtitulo, XFont fontNegrita, int idCaja, string estadoCaja,
        DateTime fechaApertura, DateTime fechaCierre, decimal saldoInicial, decimal saldoFinal,
        int margenIzquierdo, int margenDerecho, ref double yPoint) {
        // Agregar título
        gfx.DrawString("REPORTE DE CIERRE DE CAJA", fontTitulo, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, pagina.Width - margenIzquierdo - margenDerecho, 20),
            XStringFormats.TopLeft);
        yPoint += 25;

        // Información de la caja
        var anchoColumna1 = 100;
        var anchoColumna2 = 150;
        var anchoColumna3 = 100;
        var anchoColumna4 = 150;

        // Fila 1 - ID Caja y Estado
        gfx.DrawString("Caja N°:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, anchoColumna1, 15), XStringFormats.TopLeft);
        gfx.DrawString(idCaja.ToString(), fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1, yPoint, anchoColumna2, 15), XStringFormats.TopLeft);

        gfx.DrawString("Estado:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2, yPoint, anchoColumna3, 15), XStringFormats.TopLeft);
        gfx.DrawString(estadoCaja, fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2 + anchoColumna3, yPoint, anchoColumna4, 15), XStringFormats.TopLeft);
        yPoint += 15;

        // Fila 2 - Fecha Apertura y Fecha Cierre
        gfx.DrawString("Fecha Apertura:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, anchoColumna1, 15), XStringFormats.TopLeft);
        gfx.DrawString(fechaApertura.ToString("dd/MM/yyyy HH:mm:ss"), fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1, yPoint, anchoColumna2, 15), XStringFormats.TopLeft);

        gfx.DrawString("Fecha Cierre:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2, yPoint, anchoColumna3, 15), XStringFormats.TopLeft);
        gfx.DrawString(fechaCierre.ToString("dd/MM/yyyy HH:mm:ss"), fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2 + anchoColumna3, yPoint, anchoColumna4, 15), XStringFormats.TopLeft);
        yPoint += 15;

        // Fila 3 - Saldo Inicial y Saldo Final
        gfx.DrawString("Saldo Inicial:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo, yPoint, anchoColumna1, 15), XStringFormats.TopLeft);
        gfx.DrawString(saldoInicial.ToString("N2", CultureInfo.InvariantCulture) + " $", fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1, yPoint, anchoColumna2, 15), XStringFormats.TopLeft);

        gfx.DrawString("Saldo Final:", fontNegrita, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2, yPoint, anchoColumna3, 15), XStringFormats.TopLeft);
        gfx.DrawString(saldoFinal.ToString("N2", CultureInfo.InvariantCulture) + " $", fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo + anchoColumna1 + anchoColumna2 + anchoColumna3, yPoint, anchoColumna4, 15), XStringFormats.TopLeft);
        yPoint += 30;
    }

    private static void DibujarEncabezadosTablaCierreCaja(XGraphics gfx, PdfPage pagina, XFont fontEncabezado,
    int margenIzquierdo, int margenDerecho, ref double yPoint) {
        // Calcular ancho disponible
        double anchoDisponible = pagina.Width - margenIzquierdo - margenDerecho;

        // Definir anchos base de columnas
        var anchos = CalcularAnchosColumnasCierreCaja(anchoDisponible);

        // Encabezados de la tabla
        double xPos = margenIzquierdo;

        // Columna #
        gfx.DrawString("#", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoNumero, 15), XStringFormats.TopCenter);
        xPos += anchos.AnchoNumero;

        // Columna Fecha
        gfx.DrawString("Fecha/Hora", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoFecha, 15), XStringFormats.TopLeft);
        xPos += anchos.AnchoFecha;

        // Columna Concepto
        gfx.DrawString("Concepto", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoConcepto, 15), XStringFormats.TopLeft);
        xPos += anchos.AnchoConcepto;

        // Columna Tipo
        gfx.DrawString("Tipo", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoTipo, 15), XStringFormats.TopCenter);
        xPos += anchos.AnchoTipo;

        // Columna Monto
        gfx.DrawString("Monto", fontEncabezado, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoMonto, 15), XStringFormats.TopRight);
        xPos += anchos.AnchoMonto;

        // Columna Observaciones
        gfx.DrawString("Observaciones", fontEncabezado, XBrushes.Black,
            new XRect(xPos + 10, yPoint, anchos.AnchoObservaciones, 15), XStringFormats.TopLeft);

        yPoint += 15;
        gfx.DrawLine(XPens.Black, margenIzquierdo, yPoint, pagina.Width - margenDerecho, yPoint);
        yPoint += 5;
    }

    private static (int AnchoNumero, int AnchoFecha, int AnchoConcepto,
                   int AnchoTipo, int AnchoMonto, int AnchoObservaciones)
        CalcularAnchosColumnasCierreCaja(double anchoDisponible) {
        // Valores base para los anchos (en puntos)
        int anchoNumero = 30;      // Columna #
        int anchoFecha = 100;      // Columna Fecha
        int anchoConcepto = 100;   // Columna Concepto (ajustable)
        int anchoTipo = 30;        // Columna Tipo
        int anchoMonto = 90;       // Columna Monto
        int anchoObservaciones = 150; // Columna Observaciones (ajustable)

        // Calcular suma total de anchos
        int sumaAnchos = anchoNumero + anchoFecha + anchoConcepto +
                        anchoTipo + anchoMonto + anchoObservaciones;

        // Si los anchos exceden el espacio disponible, ajustar las columnas de texto
        if (sumaAnchos > anchoDisponible) {
            // Calcular el exceso
            int exceso = (int) (sumaAnchos - anchoDisponible);

            // Reducir proporcionalmente las columnas de texto largo
            int reduccionConcepto = (int) (exceso * 0.4); // 60% del ajuste a Concepto
            int reduccionObservaciones = (int) (exceso * 0.6); // 40% del ajuste a Observaciones

            // Aplicar reducción asegurando un mínimo
            anchoConcepto = Math.Max(anchoConcepto - reduccionConcepto, 120);
            anchoObservaciones = Math.Max(anchoObservaciones - reduccionObservaciones, 80);
        }

        return (anchoNumero, anchoFecha, anchoConcepto, anchoTipo, anchoMonto, anchoObservaciones);
    }

    private static void DibujarFilaMovimiento(XGraphics gfx, XFont fontContenido, string[] fila, int numeroFila,
        int margenIzquierdo, ref double yPoint, int alturaFila, CultureInfo culture,
        ref decimal totalIngresos, ref decimal totalEgresos) {
        // Calcular anchos de columnas
        var anchos = CalcularAnchosColumnasCierreCaja(gfx.PageSize.Width - margenIzquierdo * 2);

        double xPos = margenIzquierdo;

        // Número
        gfx.DrawString(numeroFila.ToString(), fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoNumero, alturaFila), XStringFormats.TopCenter);
        xPos += anchos.AnchoNumero;

        // Fecha
        gfx.DrawString(fila[0], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoFecha, alturaFila), XStringFormats.TopLeft);
        xPos += anchos.AnchoFecha;

        // Concepto
        gfx.DrawString(fila[1], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoConcepto, alturaFila), XStringFormats.TopLeft);
        xPos += anchos.AnchoConcepto;

        // Tipo
        gfx.DrawString(fila[2], fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoTipo, alturaFila), XStringFormats.TopCenter);
        xPos += anchos.AnchoTipo;

        // Monto
        var monto = decimal.TryParse(fila[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var valorMonto)
            ? valorMonto : 0.00m;
        gfx.DrawString(monto.ToString("N2", CultureInfo.InvariantCulture), fontContenido, XBrushes.Black,
            new XRect(xPos, yPoint, anchos.AnchoMonto, alturaFila), XStringFormats.TopRight);
        xPos += anchos.AnchoMonto;

        // Observaciones
        string observaciones = fila.Length > 4 ? fila[4] : "";
        // Truncar texto si es muy largo para la columna
        if (observaciones.Length > 40)
            observaciones = observaciones.Substring(0, 38) + "...";

        gfx.DrawString(observaciones, fontContenido, XBrushes.Black,
            new XRect(xPos + 10, yPoint, anchos.AnchoObservaciones, alturaFila), XStringFormats.TopLeft);

        // Actualizar totales
        if (fila[2] == "Ingreso")
            totalIngresos += monto;
        else
            totalEgresos += monto;

        yPoint += alturaFila;
    }

    private static void DibujarPiePaginaCierreCaja(XGraphics gfx, PdfPage pagina, XFont fontNegrita,
    XFont fontContenido, XFont fontSubtitulo, decimal totalIngresos, decimal totalEgresos,
    decimal saldoInicial, decimal saldoFinal, int margenIzquierdo, int margenDerecho,
    int margenInferior, ref double yPoint, int paginaActual, int totalPaginas, CultureInfo culture) {
        // Calcular anchos de columnas (consistentes con el resto del reporte)
        var anchos = CalcularAnchosColumnasCierreCaja(pagina.Width - margenIzquierdo - margenDerecho);

        // Línea separadora
        yPoint += 10;
        gfx.DrawLine(XPens.Black, margenIzquierdo, yPoint, pagina.Width - margenDerecho, yPoint);
        yPoint += 10;

        // Posición X para las etiquetas (alineado con la columna Concepto)
        double xPosEtiquetas1 = margenIzquierdo + anchos.AnchoNumero + anchos.AnchoFecha;
        double xPosEtiquetas2 = margenIzquierdo + anchos.AnchoNumero + anchos.AnchoFecha;
        double anchoEtiquetas = anchos.AnchoConcepto + anchos.AnchoTipo;

        // Posición X para los valores (alineado con la columna Monto)
        double xPosValores1 = xPosEtiquetas1 + anchoEtiquetas;
        double xPosValores2 = xPosEtiquetas2 + anchoEtiquetas;

        // Total Ingresos
        gfx.DrawString("Total Ingresos:", fontNegrita, XBrushes.Black,
            new XRect(xPosEtiquetas1, yPoint, anchoEtiquetas, 15), XStringFormats.TopRight);
        gfx.DrawString(totalIngresos.ToString("N2", culture) + " $", fontContenido, XBrushes.Black,
            new XRect(xPosValores1, yPoint, anchos.AnchoMonto, 15), XStringFormats.TopRight);
        yPoint += 15;

        // Total Egresos
        gfx.DrawString("Total Egresos:", fontNegrita, XBrushes.Black,
            new XRect(xPosEtiquetas1, yPoint, anchoEtiquetas, 15), XStringFormats.TopRight);
        gfx.DrawString(totalEgresos.ToString("N2", culture) + " $", fontContenido, XBrushes.Black,
            new XRect(xPosValores1, yPoint, anchos.AnchoMonto, 15), XStringFormats.TopRight);
        yPoint += 15;

        // Saldo Esperado (solo en la última página)
        if (paginaActual == totalPaginas) {
            decimal saldoEsperado = saldoInicial + totalIngresos - totalEgresos;

            gfx.DrawString("Saldo Esperado:", fontNegrita, XBrushes.Black,
                new XRect(xPosEtiquetas1, yPoint, anchoEtiquetas, 15), XStringFormats.TopRight);
            gfx.DrawString(saldoEsperado.ToString("N2", culture) + " $", fontContenido, XBrushes.Black,
                new XRect(xPosValores1, yPoint, anchos.AnchoMonto, 15), XStringFormats.TopRight);
            yPoint += 25;

            // Saldo Final (campo abierto para completar manualmente)
            gfx.DrawString("Saldo Final:", fontNegrita, XBrushes.Black,
                new XRect(xPosEtiquetas2, yPoint, anchoEtiquetas, 15), XStringFormats.TopRight);

            // Dibujar línea para completar manualmente
            gfx.DrawLine(XPens.Black, xPosValores1, yPoint + 12, xPosValores1 + anchos.AnchoMonto, yPoint + 12);
            yPoint += 15;

            // Diferencia (campo abierto para completar manualmente)
            gfx.DrawString("Diferencia:", fontNegrita, XBrushes.Black,
                new XRect(xPosEtiquetas2, yPoint, anchoEtiquetas, 15), XStringFormats.TopRight);

            // Dibujar línea para completar manualmente
            gfx.DrawLine(XPens.Black, xPosValores1, yPoint + 12, xPosValores1 + anchos.AnchoMonto, yPoint + 12);
            yPoint += 15;

            // Espacio para firmas
            double anchoFirma = (pagina.Width - margenIzquierdo - margenDerecho) / 2 - 20;
            yPoint = pagina.Height - margenInferior - 80;

            // Firma "Contabilizado por"
            gfx.DrawString("Contabilizado por:", fontContenido, XBrushes.Black,
                new XRect(margenIzquierdo, yPoint, anchoFirma, 15), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.Black, margenIzquierdo, yPoint + 20, margenIzquierdo + anchoFirma, yPoint + 20);
            gfx.DrawString("Firma", fontSubtitulo, XBrushes.Gray,
                new XRect(margenIzquierdo, yPoint + 22, anchoFirma, 15), XStringFormats.TopCenter);

            // Firma "Revisado por"
            gfx.DrawString("Revisado por:", fontContenido, XBrushes.Black,
                new XRect(pagina.Width - margenDerecho - anchoFirma, yPoint, anchoFirma, 15), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.Black, pagina.Width - margenDerecho - anchoFirma, yPoint + 20,
                        pagina.Width - margenDerecho, yPoint + 20);
            gfx.DrawString("Firma", fontSubtitulo, XBrushes.Gray,
                new XRect(pagina.Width - margenDerecho - anchoFirma, yPoint + 22, anchoFirma, 15), XStringFormats.TopCenter);

            yPoint += 40;
        }

        // Pie de página (alineado a la derecha)
        var fechaGeneracion = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        var textoPie = $"{fechaGeneracion} - Página {paginaActual} de {totalPaginas}";

        gfx.DrawString(textoPie, fontSubtitulo, XBrushes.Black,
            new XRect(margenIzquierdo, pagina.Height - margenInferior,
                     pagina.Width - margenIzquierdo - margenDerecho, 20),
            XStringFormats.BottomRight);
    }

    #endregion
}