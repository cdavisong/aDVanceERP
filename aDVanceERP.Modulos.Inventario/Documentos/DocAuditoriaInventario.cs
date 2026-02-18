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

    /// <summary>
    /// Modo de reporte de auditoría:
    /// - LogCambios: historial automático de adv__auditoria_inventario (trigger)
    /// - ConteoFisico: resultado de una sesión de conteo físico (adv__sesion_auditoria)
    /// </summary>
    public enum ModoAuditoria {
        LogCambios,
        ConteoFisico
    }

    public class DocAuditoriaInventario : DocumentoBase {

        #region Campos privados

        private readonly ModoAuditoria _modo;

        // Para LogCambios
        private readonly int? _idAlmacen;
        private readonly int? _idProducto;
        private readonly DateTime _fechaDesde;
        private readonly DateTime _fechaHasta;

        // Para ConteoFisico
        private readonly int? _idSesionAuditoria;

        // Datos cargados
        private string _nombreAlmacen = "Todos los almacenes";
        private string _nombreProducto = string.Empty;
        private List<Dictionary<string, string>> _registros = new();
        private Dictionary<string, string> _resumenSesion = new();

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor para el modo LogCambios (historial por trigger/movimientos).
        /// Todos los filtros son opcionales.
        /// </summary>
        public DocAuditoriaInventario(
            DateTime fechaDesde,
            DateTime fechaHasta,
            int? idAlmacen = null,
            int? idProducto = null) {

            _modo = ModoAuditoria.LogCambios;
            _fechaDesde = fechaDesde;
            _fechaHasta = fechaHasta;
            _idAlmacen = idAlmacen;
            _idProducto = idProducto;

            CargarInformacionEmpresa();
            string rutaLogo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo.png");
            if (File.Exists(rutaLogo)) CargarLogo(rutaLogo);
        }

        /// <summary>
        /// Constructor para el modo ConteoFisico (sesión de auditoría física).
        /// </summary>
        public DocAuditoriaInventario(int idSesionAuditoria) {
            _modo = ModoAuditoria.ConteoFisico;
            _idSesionAuditoria = idSesionAuditoria;

            // Valores no usados en este modo, pero requeridos por el compilador
            _fechaDesde = DateTime.MinValue;
            _fechaHasta = DateTime.MaxValue;

            CargarInformacionEmpresa();
            string rutaLogo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo.png");
            if (File.Exists(rutaLogo)) CargarLogo(rutaLogo);
        }

        #endregion

        #region Override de método abstracto

        public override void GenerarDocumento(bool mostrar = true) {
            GenerarDocumentoPDF(mostrar);
        }

        #endregion

        #region Método público principal

        public void GenerarDocumento(bool mostrar = true, FormatoDocumento formato = FormatoDocumento.PDF) {
            if (formato == FormatoDocumento.Excel) {
                GenerarDocumentoExcel(mostrar);
            } else {
                GenerarDocumentoPDF(mostrar);
            }
        }

        #endregion

        #region Generación de PDF

        private void GenerarDocumentoPDF(bool mostrar) {
            try {
                CargarDatos();

                if (_registros.Count == 0) {
                    CentroNotificaciones.MostrarNotificacion(
                        "No se encontraron registros para los filtros indicados.",
                        Core.Modelos.Comun.TipoNotificacion.Advertencia);
                    return;
                }

                var documento = new PdfDocument();
                documento.Info.Title = ObtenerTituloDocumento();
                documento.Info.Author = NombreEmpresa;
                documento.Info.Creator = "aDVance ERP";

                int numeroPagina = 1;
                PdfPage paginaActual;
                XGraphics gfx;
                double yPos;

                // --- Primera página ---
                paginaActual = documento.AddPage();
                paginaActual.Size = PageSize.Letter;
                gfx = XGraphics.FromPdfPage(paginaActual);
                yPos = ObtenerInicioPosicionContenido();

                DibujarBannerProfesional(gfx, paginaActual,
                    _modo == ModoAuditoria.LogCambios ? "AUDITORÍA DE INVENTARIO" : "CONTEO FÍSICO DE INVENTARIO",
                    ObtenerNumeroDocumento(), DateTime.Now);

                // Cuadro de información del reporte
                DibujarCuadroInformacion(gfx, MargenIzquierdo, yPos,
                    gfx.PageSize.Width - MargenIzquierdo - MargenDerecho,
                    "INFORMACIÓN DEL REPORTE",
                    ObtenerDatosEncabezado());

                yPos += 120;

                // Encabezado de tabla según modo
                var columnas = _modo == ModoAuditoria.LogCambios
                    ? ObtenerColumnasLogCambios(gfx)
                    : ObtenerColumnasConteoFisico(gfx);

                DibujarEncabezadoTabla(gfx, yPos, columnas);
                yPos += 25;

                // Contadores para totales
                int filaNum = 0;
                decimal totalEntradas = 0;
                decimal totalSalidas = 0;
                decimal totalImpacto = 0;
                int totalFaltantes = 0;
                int totalSobrantes = 0;
                int totalExactos = 0;

                foreach (var registro in _registros) {
                    if (NecesitaNuevaPagina(yPos, 20, gfx.PageSize.Height)) {
                        DibujarPiePagina(gfx, paginaActual, numeroPagina,
                            documento.PageCount + 1, ObtenerTituloDocumento());

                        paginaActual = documento.AddPage();
                        paginaActual.Size = PageSize.Letter;
                        gfx = XGraphics.FromPdfPage(paginaActual);
                        numeroPagina++;

                        DibujarBannerProfesional(gfx, paginaActual,
                            _modo == ModoAuditoria.LogCambios ? "AUDITORÍA DE INVENTARIO" : "CONTEO FÍSICO DE INVENTARIO",
                            ObtenerNumeroDocumento(), DateTime.Now);

                        yPos = ObtenerInicioPosicionContenido() + 10;
                        DibujarEncabezadoTabla(gfx, yPos, columnas);
                        yPos += 25;
                    }

                    bool esAlternada = filaNum % 2 != 0;

                    if (_modo == ModoAuditoria.LogCambios) {
                        // Colorear fila según tipo de diferencia
                        decimal diferencia = decimal.TryParse(registro["diferencia_cantidad"],
                            NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : 0;

                        DibujarFilaLogCambios(gfx, yPos, registro, columnas, esAlternada, diferencia);

                        if (diferencia > 0) totalEntradas += diferencia;
                        else totalSalidas += Math.Abs(diferencia);
                        totalImpacto += decimal.TryParse(registro["diferencia_valor"],
                            NumberStyles.Any, CultureInfo.InvariantCulture, out var dv) ? dv : 0;
                    } else {
                        decimal diferencia = decimal.TryParse(registro["diferencia"],
                            NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : 0;

                        DibujarFilaConteoFisico(gfx, yPos, registro, columnas, esAlternada);

                        totalImpacto += decimal.TryParse(registro["valor_diferencia"],
                            NumberStyles.Any, CultureInfo.InvariantCulture, out var dv) ? dv : 0;

                        if (diferencia > 0) totalSobrantes++;
                        else if (diferencia < 0) totalFaltantes++;
                        else totalExactos++;
                    }

                    yPos += 18;
                    filaNum++;
                }

                // Sección de totales
                yPos += 10;
                if (NecesitaNuevaPagina(yPos, 80, gfx.PageSize.Height)) {
                    DibujarPiePagina(gfx, paginaActual, numeroPagina, documento.PageCount + 1);
                    paginaActual = documento.AddPage();
                    paginaActual.Size = PageSize.Letter;
                    gfx = XGraphics.FromPdfPage(paginaActual);
                    numeroPagina++;
                    yPos = ObtenerInicioPosicionContenido();
                }

                var totales = _modo == ModoAuditoria.LogCambios
                    ? new Dictionary<string, string> {
                        ["Total registros"] = filaNum.ToString("N0"),
                        ["Total entradas (+)"] = totalEntradas.ToString("N2", CultureInfo.InvariantCulture),
                        ["Total salidas (-)"] = totalSalidas.ToString("N2", CultureInfo.InvariantCulture),
                        ["Impacto económico"] = totalImpacto.ToString("N2", CultureInfo.InvariantCulture)
                    }
                    : new Dictionary<string, string> {
                        ["Total productos"] = filaNum.ToString("N0"),
                        ["Exactos ✓"] = totalExactos.ToString("N0"),
                        ["Sobrantes (+)"] = totalSobrantes.ToString("N0"),
                        ["Faltantes (-)"] = totalFaltantes.ToString("N0"),
                        ["Impacto económico"] = totalImpacto.ToString("N2", CultureInfo.InvariantCulture)
                    };

                DibujarSeccionTotales(gfx, yPos, totales, 300);

                // Pie de última página
                DibujarPiePagina(gfx, paginaActual, numeroPagina, numeroPagina, ObtenerTituloDocumento());

                // Guardar y abrir
                string rutaDocumento = Path.Combine(Path.GetTempPath(),
                    $"Auditoria_Inventario_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

                documento.Save(rutaDocumento);

                if (mostrar) {
                    Process.Start(new ProcessStartInfo {
                        FileName = rutaDocumento,
                        UseShellExecute = true
                    });
                }

                CentroNotificaciones.MostrarNotificacion(
                    "Documento de auditoría generado exitosamente.",
                    Core.Modelos.Comun.TipoNotificacion.Info);

            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error al generar el documento: {ex.Message}",
                    Core.Modelos.Comun.TipoNotificacion.Error);
            }
        }

        #endregion

        #region Helpers de dibujo específicos

        /// <summary>
        /// Fila del log de cambios: colorea en verde/rojo según si fue entrada o salida.
        /// </summary>
        private void DibujarFilaLogCambios(XGraphics gfx, double yPos,
            Dictionary<string, string> registro,
            Dictionary<string, double> columnas,
            bool esAlternada,
            decimal diferencia) {

            double xPos = MargenIzquierdo;
            double alturaFila = 18;

            // Fondo de fila
            XColor colorFondo = esAlternada
                ? XColor.FromArgb(250, 250, 250)
                : XColors.White;

            gfx.DrawRectangle(new XSolidBrush(colorFondo),
                new XRect(xPos, yPos, gfx.PageSize.Width - MargenIzquierdo - MargenDerecho, alturaFila));

            // Indicador lateral de color según tipo de cambio
            if (diferencia != 0) {
                XColor colorIndicador = diferencia > 0
                    ? XColor.FromArgb(76, 175, 80)   // Verde: entrada
                    : XColor.FromArgb(229, 57, 53);   // Rojo: salida

                gfx.DrawRectangle(new XSolidBrush(colorIndicador),
                    new XRect(MargenIzquierdo, yPos, 3, alturaFila));
            }

            var valores = new Dictionary<string, (double ancho, string valor, XStringFormat formato)> {
                ["fecha"] = (columnas["Fecha"], registro["fecha_registro"], XStringFormats.CenterLeft),
                ["producto"] = (columnas["Producto"], TruncarTexto(registro["producto"], gfx, columnas["Producto"] - 10), XStringFormats.CenterLeft),
                ["almacen"] = (columnas["Almacén"], registro["almacen"], XStringFormats.CenterLeft),
                ["ant"] = (columnas["Ant."], registro["cantidad_anterior"], XStringFormats.CenterRight),
                ["nuevo"] = (columnas["Nuevo"], registro["cantidad_nueva"], XStringFormats.CenterRight),
                ["diferencia"] = (columnas["Diferencia"], (diferencia >= 0 ? "+" : "") + registro["diferencia_cantidad"], XStringFormats.CenterRight),
                ["origen"] = (columnas["Origen"], registro["origen"], XStringFormats.CenterLeft),
                ["usuario"] = (columnas["Usuario"], registro["usuario"], XStringFormats.CenterLeft),
            };

            DibujarFilaTabla(gfx, yPos, valores, false, alturaFila);
        }

        /// <summary>
        /// Fila del conteo físico: colorea según diferencia (sobrante/faltante/exacto).
        /// </summary>
        private void DibujarFilaConteoFisico(XGraphics gfx, double yPos,
            Dictionary<string, string> registro,
            Dictionary<string, double> columnas,
            bool esAlternada) {

            double alturaFila = 18;

            decimal diferencia = decimal.TryParse(registro["diferencia"],
                NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : 0;

            XColor colorFondo = esAlternada
                ? XColor.FromArgb(250, 250, 250)
                : XColors.White;

            gfx.DrawRectangle(new XSolidBrush(colorFondo),
                new XRect(MargenIzquierdo, yPos,
                    gfx.PageSize.Width - MargenIzquierdo - MargenDerecho, alturaFila));

            // Indicador lateral
            XColor colorIndicador = diferencia > 0
                ? XColor.FromArgb(76, 175, 80)   // Verde: sobrante
                : diferencia < 0
                    ? XColor.FromArgb(229, 57, 53)  // Rojo: faltante
                    : XColor.FromArgb(117, 117, 117); // Gris: exacto

            gfx.DrawRectangle(new XSolidBrush(colorIndicador),
                new XRect(MargenIzquierdo, yPos, 3, alturaFila));

            string estadoTexto = diferencia > 0 ? "Sobrante"
                               : diferencia < 0 ? "Faltante"
                               : "Exacto";

            var valores = new Dictionary<string, (double ancho, string valor, XStringFormat formato)> {
                ["codigo"] = (columnas["Código"], registro["codigo_producto"], XStringFormats.CenterLeft),
                ["producto"] = (columnas["Producto"], TruncarTexto(registro["producto"], gfx, columnas["Producto"] - 10), XStringFormats.CenterLeft),
                ["sistema"] = (columnas["Sistema"], registro["cantidad_sistema"], XStringFormats.CenterRight),
                ["contado"] = (columnas["Contado"], registro["cantidad_contada"], XStringFormats.CenterRight),
                ["diferencia"] = (columnas["Diferencia"], (diferencia >= 0 ? "+" : "") + registro["diferencia"], XStringFormats.CenterRight),
                ["val_dif"] = (columnas["Val. Dif."], registro["valor_diferencia"], XStringFormats.CenterRight),
                ["estado"] = (columnas["Estado"], estadoTexto, XStringFormats.Center),
                ["ajustado"] = (columnas["Ajustado"], registro["ajuste_aplicado"] == "1" ? "Sí" : "No", XStringFormats.Center),
            };

            DibujarFilaTabla(gfx, yPos, valores, false, alturaFila);
        }

        private Dictionary<string, double> ObtenerColumnasLogCambios(XGraphics gfx) {
            double w = gfx.PageSize.Width - MargenIzquierdo - MargenDerecho;
            return new Dictionary<string, double> {
                ["Fecha"] = w * 0.14,
                ["Producto"] = w * 0.20,
                ["Almacén"] = w * 0.13,
                ["Ant."] = w * 0.08,
                ["Nuevo"] = w * 0.08,
                ["Diferencia"] = w * 0.10,
                ["Origen"] = w * 0.13,
                ["Usuario"] = w * 0.14,
            };
        }

        private Dictionary<string, double> ObtenerColumnasConteoFisico(XGraphics gfx) {
            double w = gfx.PageSize.Width - MargenIzquierdo - MargenDerecho;
            return new Dictionary<string, double> {
                ["Código"] = w * 0.10,
                ["Producto"] = w * 0.22,
                ["Sistema"] = w * 0.10,
                ["Contado"] = w * 0.10,
                ["Diferencia"] = w * 0.10,
                ["Val. Dif."] = w * 0.13,
                ["Estado"] = w * 0.13,
                ["Ajustado"] = w * 0.12,
            };
        }

        private string TruncarTexto(string texto, XGraphics gfx, double anchoMaximo) {
            var tamano = gfx.MeasureString(texto, FontContenido);
            if (tamano.Width <= anchoMaximo) return texto;

            string truncado = texto;
            while (truncado.Length > 0) {
                truncado = truncado[..^1];
                if (gfx.MeasureString(truncado + "...", FontContenido).Width <= anchoMaximo)
                    return truncado + "...";
            }
            return "...";
        }

        #endregion

        #region Generación de Excel

        private void GenerarDocumentoExcel(bool mostrar) {
            try {
                CargarDatos();

                if (_registros.Count == 0) {
                    CentroNotificaciones.MostrarNotificacion(
                        "No se encontraron registros para los filtros indicados.",
                        Core.Modelos.Comun.TipoNotificacion.Advertencia);
                    return;
                }

                using var workbook = new XLWorkbook();
                var ws = workbook.Worksheets.Add(
                    _modo == ModoAuditoria.LogCambios ? "Log de Cambios" : "Conteo Físico");

                // Estilo de encabezado
                var estiloEncabezado = workbook.Style;
                int col = 1;

                // Título
                ws.Cell(1, 1).Value = ObtenerTituloDocumento();
                ws.Range(1, 1, 1, _modo == ModoAuditoria.LogCambios ? 8 : 8).Merge();
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 14;
                ws.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#B22222");
                ws.Cell(1, 1).Style.Font.FontColor = XLColor.White;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Fecha del reporte
                ws.Cell(2, 1).Value = $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                ws.Range(2, 1, 2, _modo == ModoAuditoria.LogCambios ? 8 : 8).Merge();
                ws.Cell(2, 1).Style.Font.Italic = true;
                ws.Cell(2, 1).Style.Font.FontColor = XLColor.Gray;

                int currentRow = 4;

                // Encabezados de columna
                List<string> headers = _modo == ModoAuditoria.LogCambios
                    ? new() { "Fecha", "Producto", "Almacén", "Cant. Anterior", "Cant. Nueva", "Diferencia", "Origen", "Usuario" }
                    : new() { "Código", "Producto", "Sistema", "Contado", "Diferencia", "Val. Diferencia", "Estado", "Ajustado" };

                col = 1;
                foreach (var header in headers) {
                    ws.Cell(currentRow, col).Value = header;
                    ws.Cell(currentRow, col).Style.Font.Bold = true;
                    ws.Cell(currentRow, col).Style.Fill.BackgroundColor = XLColor.FromHtml("#333333");
                    ws.Cell(currentRow, col).Style.Font.FontColor = XLColor.White;
                    ws.Cell(currentRow, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    col++;
                }

                currentRow++;

                // Datos
                int fila = 0;
                foreach (var registro in _registros) {
                    col = 1;
                    bool esAlternada = fila % 2 != 0;

                    List<string> valores = _modo == ModoAuditoria.LogCambios
                        ? new() {
                            registro["fecha_registro"],
                            registro["producto"],
                            registro["almacen"],
                            registro["cantidad_anterior"],
                            registro["cantidad_nueva"],
                            registro["diferencia_cantidad"],
                            registro["origen"],
                            registro["usuario"]
                        }
                        : new() {
                            registro["codigo_producto"],
                            registro["producto"],
                            registro["cantidad_sistema"],
                            registro["cantidad_contada"],
                            registro["diferencia"],
                            registro["valor_diferencia"],
                            decimal.TryParse(registro["diferencia"], NumberStyles.Any, CultureInfo.InvariantCulture, out var dif)
                                ? (dif > 0 ? "Sobrante" : dif < 0 ? "Faltante" : "Exacto")
                                : "-",
                            registro["ajuste_aplicado"] == "1" ? "Sí" : "No"
                        };

                    foreach (var valor in valores) {
                        ws.Cell(currentRow, col).Value = valor;
                        if (esAlternada)
                            ws.Cell(currentRow, col).Style.Fill.BackgroundColor = XLColor.FromHtml("#FAFAFA");
                        col++;
                    }

                    // Colorear columna Diferencia según signo
                    int colDif = _modo == ModoAuditoria.LogCambios ? 6 : 5;
                    if (decimal.TryParse(registro[_modo == ModoAuditoria.LogCambios ? "diferencia_cantidad" : "diferencia"],
                        NumberStyles.Any, CultureInfo.InvariantCulture, out decimal difVal)) {
                        ws.Cell(currentRow, colDif).Style.Font.FontColor =
                            difVal > 0 ? XLColor.FromHtml("#2E7D32") :
                            difVal < 0 ? XLColor.FromHtml("#C62828") :
                            XLColor.Gray;
                        ws.Cell(currentRow, colDif).Style.Font.Bold = true;
                    }

                    currentRow++;
                    fila++;
                }

                // Ajustar ancho de columnas
                ws.Columns().AdjustToContents();

                // Guardar y abrir
                string rutaDocumento = Path.Combine(Path.GetTempPath(),
                    $"Auditoria_Inventario_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");

                workbook.SaveAs(rutaDocumento);

                if (mostrar) {
                    Process.Start(new ProcessStartInfo {
                        FileName = rutaDocumento,
                        UseShellExecute = true
                    });
                }

                CentroNotificaciones.MostrarNotificacion(
                    "Documento Excel de auditoría generado exitosamente.",
                    Core.Modelos.Comun.TipoNotificacion.Info);

            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error al generar el Excel: {ex.Message}",
                    Core.Modelos.Comun.TipoNotificacion.Error);
            }
        }

        #endregion

        #region Carga de datos

        private void CargarDatos() {
            if (_modo == ModoAuditoria.LogCambios) {
                CargarLogCambios();
            } else {
                CargarConteoFisico();
            }
        }

        /// <summary>
        /// Carga el historial de la tabla adv__auditoria_inventario.
        /// Usa la vista v_auditoria_inventario si está disponible.
        /// </summary>
        private void CargarLogCambios() {
            _registros.Clear();

            using var connection = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());
            connection.Open();

            var query = """
                SELECT
                    DATE_FORMAT(a.fecha_registro, '%d/%m/%Y %H:%i') AS fecha_registro,
                    COALESCE(p.nombre, 'N/A')                        AS producto,
                    COALESCE(al.nombre, 'N/A')                       AS almacen,
                    a.cantidad_anterior,
                    a.cantidad_nueva,
                    a.diferencia_cantidad,
                    a.costo_prom_anterior,
                    a.costo_prom_nuevo,
                    a.diferencia_valor,
                    a.origen,
                    COALESCE(cu.nombre, 'Sistema')                   AS usuario,
                    a.observacion
                FROM adv__auditoria_inventario a
                LEFT JOIN adv__inventario      inv ON a.id_inventario       = inv.id_inventario
                LEFT JOIN adv__producto        p   ON a.id_producto         = p.id_producto
                LEFT JOIN adv__almacen         al  ON a.id_almacen          = al.id_almacen
                LEFT JOIN adv__cuenta_usuario  cu  ON a.id_cuenta_usuario   = cu.id_cuenta_usuario
                WHERE a.fecha_registro BETWEEN @desde AND @hasta
                """;

            if (_idAlmacen.HasValue) query += " AND a.id_almacen  = @idAlmacen";
            if (_idProducto.HasValue) query += " AND a.id_producto = @idProducto";

            query += " ORDER BY a.fecha_registro DESC";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@desde", _fechaDesde.Date);
            command.Parameters.AddWithValue("@hasta", _fechaHasta.Date.AddDays(1).AddSeconds(-1));
            if (_idAlmacen.HasValue) command.Parameters.AddWithValue("@idAlmacen", _idAlmacen.Value);
            if (_idProducto.HasValue) command.Parameters.AddWithValue("@idProducto", _idProducto.Value);

            using var reader = command.ExecuteReader();
            while (reader.Read()) {
                _registros.Add(new Dictionary<string, string> {
                    ["fecha_registro"] = reader["fecha_registro"].ToString() ?? string.Empty,
                    ["producto"] = reader["producto"].ToString() ?? string.Empty,
                    ["almacen"] = reader["almacen"].ToString() ?? string.Empty,
                    ["cantidad_anterior"] = reader.IsDBNull("cantidad_anterior") ? "0.00"
                                                : reader.GetDecimal("cantidad_anterior").ToString("N2", CultureInfo.InvariantCulture),
                    ["cantidad_nueva"] = reader.IsDBNull("cantidad_nueva") ? "0.00"
                                                : reader.GetDecimal("cantidad_nueva").ToString("N2", CultureInfo.InvariantCulture),
                    ["diferencia_cantidad"] = reader.IsDBNull("diferencia_cantidad") ? "0.00"
                                                : reader.GetDecimal("diferencia_cantidad").ToString("N2", CultureInfo.InvariantCulture),
                    ["diferencia_valor"] = reader.IsDBNull("diferencia_valor") ? "0.00"
                                                : reader.GetDecimal("diferencia_valor").ToString("N2", CultureInfo.InvariantCulture),
                    ["origen"] = reader["origen"].ToString() ?? string.Empty,
                    ["usuario"] = reader["usuario"].ToString() ?? string.Empty,
                    ["observacion"] = reader["observacion"].ToString() ?? string.Empty,
                });
            }

            if (_idAlmacen.HasValue) {
                _nombreAlmacen = _registros.FirstOrDefault()?["almacen"] ?? "N/A";
            }
        }

        /// <summary>
        /// Carga el detalle de una sesión de conteo físico desde adv__detalle_auditoria.
        /// </summary>
        private void CargarConteoFisico() {
            _registros.Clear();
            _resumenSesion.Clear();

            if (!_idSesionAuditoria.HasValue) return;

            using var connection = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());
            connection.Open();

            // Cargar datos de la sesión para el encabezado
            var querySesion = """
                SELECT s.nombre, s.fecha_inicio, s.fecha_cierre, s.estado,
                       al.nombre AS almacen, cu.nombre AS responsable
                FROM adv__sesion_auditoria s
                LEFT JOIN adv__almacen        al ON s.id_almacen        = al.id_almacen
                LEFT JOIN adv__cuenta_usuario cu ON s.id_cuenta_usuario = cu.id_cuenta_usuario
                WHERE s.id_sesion_auditoria = @idSesion
                """;

            using (var cmd = new MySqlCommand(querySesion, connection)) {
                cmd.Parameters.AddWithValue("@idSesion", _idSesionAuditoria.Value);
                using var r = cmd.ExecuteReader();
                if (r.Read()) {
                    _nombreAlmacen = r["almacen"].ToString() ?? "N/A";
                    _resumenSesion["Sesión"] = r["nombre"].ToString() ?? string.Empty;
                    _resumenSesion["Almacén"] = _nombreAlmacen;
                    _resumenSesion["Responsable"] = r["responsable"].ToString() ?? "N/A";
                    _resumenSesion["Inicio"] = r.IsDBNull("fecha_inicio") ? "-"
                                                      : r.GetDateTime("fecha_inicio").ToString("dd/MM/yyyy HH:mm");
                    _resumenSesion["Cierre"] = r.IsDBNull("fecha_cierre") ? "En proceso"
                                                      : r.GetDateTime("fecha_cierre").ToString("dd/MM/yyyy HH:mm");
                    _resumenSesion["Estado"] = r["estado"].ToString() ?? string.Empty;
                }
            }

            // Cargar detalle
            var queryDetalle = """
                SELECT
                    p.codigo                                         AS codigo_producto,
                    p.nombre                                         AS producto,
                    d.cantidad_sistema,
                    COALESCE(d.cantidad_contada, 0)                  AS cantidad_contada,
                    COALESCE(d.diferencia, 0)                        AS diferencia,
                    COALESCE(d.valor_diferencia, 0)                  AS valor_diferencia,
                    d.ajuste_aplicado,
                    COALESCE(cu.nombre, 'N/A')                       AS contado_por,
                    d.observacion
                FROM adv__detalle_auditoria d
                JOIN adv__producto         p   ON d.id_producto  = p.id_producto
                LEFT JOIN adv__cuenta_usuario cu ON d.contado_por = cu.id_cuenta_usuario
                WHERE d.id_sesion_auditoria = @idSesion
                ORDER BY p.nombre
                """;

            using var cmdDetalle = new MySqlCommand(queryDetalle, connection);
            cmdDetalle.Parameters.AddWithValue("@idSesion", _idSesionAuditoria.Value);

            using var reader = cmdDetalle.ExecuteReader();
            while (reader.Read()) {
                _registros.Add(new Dictionary<string, string> {
                    ["codigo_producto"] = reader["codigo_producto"].ToString() ?? string.Empty,
                    ["producto"] = reader["producto"].ToString() ?? string.Empty,
                    ["cantidad_sistema"] = reader.GetDecimal("cantidad_sistema").ToString("N2", CultureInfo.InvariantCulture),
                    ["cantidad_contada"] = reader.GetDecimal("cantidad_contada").ToString("N2", CultureInfo.InvariantCulture),
                    ["diferencia"] = reader.GetDecimal("diferencia").ToString("N2", CultureInfo.InvariantCulture),
                    ["valor_diferencia"] = reader.GetDecimal("valor_diferencia").ToString("N2", CultureInfo.InvariantCulture),
                    ["ajuste_aplicado"] = reader.GetInt32("ajuste_aplicado").ToString(),
                    ["contado_por"] = reader["contado_por"].ToString() ?? string.Empty,
                    ["observacion"] = reader["observacion"].ToString() ?? string.Empty,
                });
            }
        }

        #endregion

        #region Helpers de texto

        private string ObtenerTituloDocumento() {
            return _modo == ModoAuditoria.LogCambios
                ? "AUDITORÍA DE INVENTARIO"
                : "CONTEO FÍSICO DE INVENTARIO";
        }

        private string ObtenerNumeroDocumento() {
            return _modo == ModoAuditoria.LogCambios
                ? $"AUD-{DateTime.Now:yyyyMMdd}"
                : $"CFI-{_idSesionAuditoria:D4}";
        }

        private Dictionary<string, string> ObtenerDatosEncabezado() {
            if (_modo == ModoAuditoria.LogCambios) {
                return new Dictionary<string, string> {
                    ["Almacén"] = _nombreAlmacen,
                    ["Período"] = $"{_fechaDesde:dd/MM/yyyy} - {_fechaHasta:dd/MM/yyyy}",
                    ["Producto"] = string.IsNullOrEmpty(_nombreProducto) ? "Todos" : _nombreProducto,
                    ["Registros"] = _registros.Count.ToString("N0"),
                    ["Generado"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                };
            } else {
                var datos = new Dictionary<string, string>(_resumenSesion) {
                    ["Total Items"] = _registros.Count.ToString("N0"),
                    ["Generado"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                };
                return datos;
            }
        }

        #endregion

        #region CargarInformacionEmpresa

        protected override void CargarInformacionEmpresa() {
            ConfigurarEmpresa(
                nombre: "aDVance ERP",
                direccion: "Tu dirección comercial",
                telefono: "+58 (XXX) XXX-XXXX",
                email: "info@advanceerp.com",
                web: "www.advanceerp.com",
                rif: "J-XXXXXXXXX-X"
            );
        }

        #endregion
    }
}