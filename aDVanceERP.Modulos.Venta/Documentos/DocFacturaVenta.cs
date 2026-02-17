using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Infraestructura.Extensiones.BD;
using aDVanceERP.Core.Infraestructura.Globales;

using ClosedXML.Excel;

using MySql.Data.MySqlClient;

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

using System.Diagnostics;
using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Documentos {
    public class DocFacturaVenta : DocumentoBase {
        private long _idVenta;
        private Dictionary<string, string> _datosVenta;
        private Dictionary<string, string> _datosCliente;
        private List<Dictionary<string, string>> _detallesProductos;
        private int _alturaTotalInfoClienteYVenta = 110;

        #region Constructor

        public DocFacturaVenta(long idVenta) {
            _idVenta = idVenta;

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
            if (parametros.Length > 0 && parametros[0] is int idVenta) {
                _idVenta = idVenta;
            }

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
                _datosVenta = ObtenerDatosVenta(_idVenta);
                _datosCliente = ObtenerDatosCliente(_datosVenta["id_cliente"]);
                _detallesProductos = ObtenerDetallesProductos(_idVenta);

                if (_datosVenta.Count == 0) {
                    CentroNotificaciones.MostrarNotificacion(
                        "No se encontraron datos para la factura.",
                        Core.Modelos.Comun.TipoNotificacion.Advertencia);
                    return;
                }

                // Crear documento PDF
                var documento = new PdfDocument();
                documento.Info.Title = $"Factura {_datosVenta["numero_factura_ticket"]}";
                documento.Info.Author = NombreEmpresa;
                documento.Info.Creator = "aDVance ERP";

                // Crear página
                var pagina = documento.AddPage();
                pagina.Size = PageSize.Letter;
                var gfx = XGraphics.FromPdfPage(pagina);

                // Dibujar banner profesional
                DibujarBannerProfesional(
                    gfx,
                    pagina,
                    "FACTURA DE VENTA",
                    _datosVenta["numero_factura_ticket"],
                    DateTime.Parse(_datosVenta["fecha_venta"])
                );

                double yPos = ObtenerInicioPosicionContenido();

                // Información del cliente y venta en dos columnas
                DibujarInformacionClienteYVenta(gfx, yPos);
                yPos += _alturaTotalInfoClienteYVenta;

                // Tabla de productos
                double anchoDisponible = gfx.PageSize.Width - MargenIzquierdo - MargenDerecho;
                var columnas = new Dictionary<string, double> {
                    ["#"] = anchoDisponible * 0.05,            // 5%
                    ["Código"] = anchoDisponible * 0.15,       // 15%
                    ["Producto"] = anchoDisponible * 0.30,     // 30%
                    ["Cant."] = anchoDisponible * 0.10,        // 10%
                    ["P. Unit."] = anchoDisponible * 0.13,     // 13%
                    ["Desc."] = anchoDisponible * 0.12,        // 12%
                    ["Subtotal"] = anchoDisponible * 0.15      // 15%
                };

                DibujarEncabezadoTabla(gfx, yPos, columnas);
                yPos += 25;

                // Dibujar productos
                int numeroFila = 0;
                decimal subtotalGeneral = 0;

                foreach (var producto in _detallesProductos) {
                    // Verificar si se necesita nueva página
                    if (NecesitaNuevaPagina(yPos, 20, gfx.PageSize.Height)) {
                        // Nueva página
                        DibujarPiePagina(gfx, pagina, 1, 2, $"Factura: {_datosVenta["numero_factura_ticket"]}");

                        pagina = documento.AddPage();
                        pagina.Size = PageSize.Letter;
                        gfx = XGraphics.FromPdfPage(pagina);

                        DibujarBannerProfesional(
                            gfx,
                            pagina,
                            "FACTURA DE VENTA",
                            _datosVenta["numero_factura_ticket"],
                            DateTime.Parse(_datosVenta["fecha_venta"])
                        );

                        yPos = ObtenerInicioPosicionContenido() + 10;
                        DibujarEncabezadoTabla(gfx, yPos, columnas);
                        yPos += 25;
                    }

                    numeroFila++;
                    decimal cantidad = decimal.Parse(producto["cantidad"], CultureInfo.InvariantCulture);
                    decimal precioUnitario = decimal.Parse(producto["precio_venta_unitario"], CultureInfo.InvariantCulture);
                    decimal descuento = decimal.Parse(producto["descuento_item"], CultureInfo.InvariantCulture);
                    decimal subtotal = decimal.Parse(producto["subtotal"], CultureInfo.InvariantCulture);

                    subtotalGeneral += subtotal;

                    var datosProducto = new Dictionary<string, (double ancho, string valor, XStringFormat formato)> {
                        ["#"] = (columnas["#"], numeroFila.ToString(), XStringFormats.Center),
                        ["Código"] = (columnas["Código"], producto["codigo"], XStringFormats.CenterLeft),
                        ["Producto"] = (columnas["Producto"], TruncarTexto(producto["nombre"], FontContenido, gfx, columnas["Producto"] - 15), XStringFormats.CenterLeft),
                        ["Cant."] = (columnas["Cant."], cantidad.ToString("N2"), XStringFormats.CenterRight),
                        ["P. Unit."] = (columnas["P. Unit."], precioUnitario.ToString("C2"), XStringFormats.CenterRight),
                        ["Desc."] = (columnas["Desc."], descuento.ToString("C2"), XStringFormats.CenterRight),
                        ["Subtotal"] = (columnas["Subtotal"], subtotal.ToString("C2"), XStringFormats.CenterRight)
                    };

                    DibujarFilaTabla(gfx, yPos, datosProducto, numeroFila % 2 == 0, 20);
                    yPos += 20;
                }

                // Línea separadora antes de totales
                yPos += 10;
                gfx.DrawLine(new XPen(ColorSecundario, 2),
                    MargenIzquierdo, yPos,
                    gfx.PageSize.Width - MargenDerecho, yPos);
                yPos += 15;

                // Sección de totales (derecha)
                decimal descuentoTotal = decimal.Parse(_datosVenta["descuento_total"], CultureInfo.InvariantCulture);
                decimal impuestoTotal = decimal.Parse(_datosVenta["impuesto_total"], CultureInfo.InvariantCulture);
                decimal totalBruto = decimal.Parse(_datosVenta["total_bruto"], CultureInfo.InvariantCulture);
                decimal importeTotal = decimal.Parse(_datosVenta["importe_total"], CultureInfo.InvariantCulture);

                var totales = new Dictionary<string, string> {
                    ["SUBTOTAL"] = totalBruto.ToString("C2"),
                    ["DESCUENTO"] = descuentoTotal.ToString("C2"),
                    ["BASE IMPONIBLE"] = (totalBruto - descuentoTotal).ToString("C2"),
                    ["IMPUESTOS"] = impuestoTotal.ToString("C2"),
                    ["TOTAL A PAGAR"] = importeTotal.ToString("C2")
                };

                DibujarSeccionTotales(gfx, yPos, totales, 350);

                // Información adicional (izquierda, debajo de productos)
                if (!string.IsNullOrEmpty(_datosVenta["observaciones_venta"])) {
                    double yPosObservaciones = yPos;
                    gfx.DrawString("OBSERVACIONES:", FontContenidoNegrita, new XSolidBrush(ColorSecundario),
                        new XPoint(MargenIzquierdo, yPosObservaciones));
                    yPosObservaciones += 15;

                    var rectObservaciones = new XRect(MargenIzquierdo, yPosObservaciones, 300, 60);
                    gfx.DrawRectangle(new XPen(ColorTextoSecundario, 0.5), rectObservaciones);

                    gfx.DrawString(_datosVenta["observaciones_venta"], FontPequeno,
                        new XSolidBrush(ColorTexto),
                        new XRect(MargenIzquierdo + 5, yPosObservaciones + 5, 290, 50),
                        XStringFormats.TopLeft);
                }

                // Método de pago
                yPos += 130;
                gfx.DrawString($"Método de Pago: {_datosVenta["metodo_pago_principal"]}", FontContenido,
                    new XSolidBrush(ColorTexto),
                    new XPoint(MargenIzquierdo, yPos));

                // Estado de la venta
                yPos += 15;
                XColor colorEstado = _datosVenta["estado_venta"] switch {
                    "Completada" => XColor.FromArgb(34, 139, 34),
                    "Entregada" => XColor.FromArgb(0, 128, 0),
                    "Pendiente" => XColor.FromArgb(255, 140, 0),
                    "Anulada" => XColor.FromArgb(178, 34, 34),
                    _ => ColorTexto
                };

                gfx.DrawString($"Estado: {_datosVenta["estado_venta"]}", FontContenidoNegrita,
                    new XSolidBrush(colorEstado),
                    new XPoint(MargenIzquierdo, yPos));

                // Pie de página
                DibujarPiePagina(gfx, pagina, 1, 1, $"Factura: {_datosVenta["numero_factura_ticket"]}");

                // Guardar archivo
                string rutaDocumento = Path.Combine(
                    Path.GetTempPath(),
                    $"Factura_{_datosVenta["numero_factura_ticket"].Replace("/", "-")}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                );

                documento.Save(rutaDocumento);

                if (mostrar) {
                    Process.Start(new ProcessStartInfo {
                        FileName = rutaDocumento,
                        UseShellExecute = true
                    });
                }

                CentroNotificaciones.MostrarNotificacion(
                    "Factura generada exitosamente.",
                    Core.Modelos.Comun.TipoNotificacion.Info
                );

            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error al generar la factura: {ex.Message}",
                    Core.Modelos.Comun.TipoNotificacion.Error
                );
            }
        }

        #endregion

        #region Generación de Excel

        private void GenerarDocumentoExcel(bool mostrar) {
            try {
                // Cargar datos
                _datosVenta = ObtenerDatosVenta(_idVenta);
                _datosCliente = ObtenerDatosCliente(_datosVenta["id_cliente"]);
                _detallesProductos = ObtenerDetallesProductos(_idVenta);

                if (_datosVenta.Count == 0) {
                    CentroNotificaciones.MostrarNotificacion(
                        "No se encontraron datos para la factura.",
                        Core.Modelos.Comun.TipoNotificacion.Advertencia);
                    return;
                }

                // Crear libro de Excel
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Factura");

                int currentRow = 1;

                // Título principal
                worksheet.Cell(currentRow, 1).Value = "FACTURA DE VENTA";
                worksheet.Range(currentRow, 1, currentRow, 7).Merge();
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 1).Style.Font.FontSize = 16;
                worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(currentRow, 1).Style.Fill.BackgroundColor = XLColor.FromArgb(178, 34, 34);
                worksheet.Cell(currentRow, 1).Style.Font.FontColor = XLColor.White;
                currentRow += 2;

                // Información de la empresa
                worksheet.Cell(currentRow, 1).Value = NombreEmpresa;
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 1).Style.Font.FontSize = 12;
                currentRow++;

                if (!string.IsNullOrEmpty(RifEmpresa)) {
                    worksheet.Cell(currentRow, 1).Value = $"RIF: {RifEmpresa}";
                    currentRow++;
                }
                if (!string.IsNullOrEmpty(DireccionEmpresa)) {
                    worksheet.Cell(currentRow, 1).Value = DireccionEmpresa;
                    currentRow++;
                }
                if (!string.IsNullOrEmpty(TelefonoEmpresa)) {
                    worksheet.Cell(currentRow, 1).Value = $"Tel: {TelefonoEmpresa}";
                    currentRow++;
                }
                currentRow += 2;

                // Información de la factura
                worksheet.Cell(currentRow, 1).Value = "Número de Factura:";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = _datosVenta["numero_factura_ticket"];
                worksheet.Cell(currentRow, 4).Value = "Fecha:";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 5).Value = DateTime.Parse(_datosVenta["fecha_venta"]).ToString("dd/MM/yyyy HH:mm");
                currentRow++;

                worksheet.Cell(currentRow, 1).Value = "Estado:";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = _datosVenta["estado_venta"];
                worksheet.Cell(currentRow, 4).Value = "Almacén:";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 5).Value = _datosVenta["nombre_almacen"];
                currentRow += 2;

                // Información del cliente
                worksheet.Cell(currentRow, 1).Value = "DATOS DEL CLIENTE";
                worksheet.Range(currentRow, 1, currentRow, 7).Merge();
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                currentRow++;

                worksheet.Cell(currentRow, 1).Value = "Cliente:";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = _datosCliente["nombre_completo"];
                worksheet.Range(currentRow, 2, currentRow, 4).Merge();
                currentRow++;

                worksheet.Cell(currentRow, 1).Value = "Documento:";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = $"{_datosCliente["tipo_documento"]}: {_datosCliente["numero_documento"]}";
                currentRow++;

                if (!string.IsNullOrEmpty(_datosCliente["direccion_principal"])) {
                    worksheet.Cell(currentRow, 1).Value = "Dirección:";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = _datosCliente["direccion_principal"];
                    worksheet.Range(currentRow, 2, currentRow, 7).Merge();
                    currentRow++;
                }
                currentRow += 2;

                // Encabezados de productos
                worksheet.Cell(currentRow, 1).Value = "#";
                worksheet.Cell(currentRow, 2).Value = "Código";
                worksheet.Cell(currentRow, 3).Value = "Producto";
                worksheet.Cell(currentRow, 4).Value = "Cantidad";
                worksheet.Cell(currentRow, 5).Value = "Precio Unit.";
                worksheet.Cell(currentRow, 6).Value = "Descuento";
                worksheet.Cell(currentRow, 7).Value = "Subtotal";

                var rangoEncabezado = worksheet.Range(currentRow, 1, currentRow, 7);
                rangoEncabezado.Style.Font.Bold = true;
                rangoEncabezado.Style.Fill.BackgroundColor = XLColor.FromArgb(178, 34, 34);
                rangoEncabezado.Style.Font.FontColor = XLColor.White;
                rangoEncabezado.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                currentRow++;

                // Detalles de productos
                int numeroFila = 0;
                foreach (var producto in _detallesProductos) {
                    numeroFila++;

                    worksheet.Cell(currentRow, 1).Value = numeroFila;
                    worksheet.Cell(currentRow, 2).Value = producto["codigo"];
                    worksheet.Cell(currentRow, 3).Value = producto["nombre"];
                    worksheet.Cell(currentRow, 4).Value = decimal.Parse(producto["cantidad"], CultureInfo.InvariantCulture);
                    worksheet.Cell(currentRow, 5).Value = decimal.Parse(producto["precio_venta_unitario"], CultureInfo.InvariantCulture);
                    worksheet.Cell(currentRow, 6).Value = decimal.Parse(producto["descuento_item"], CultureInfo.InvariantCulture);
                    worksheet.Cell(currentRow, 7).Value = decimal.Parse(producto["subtotal"], CultureInfo.InvariantCulture);

                    // Formato numérico
                    worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = "#,##0.00";
                    worksheet.Cell(currentRow, 5).Style.NumberFormat.Format = "$#,##0.00";
                    worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = "$#,##0.00";
                    worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "$#,##0.00";

                    // Borde
                    var rangoFila = worksheet.Range(currentRow, 1, currentRow, 7);
                    rangoFila.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    currentRow++;
                }

                currentRow++;

                // Totales
                decimal totalBruto = decimal.Parse(_datosVenta["total_bruto"], CultureInfo.InvariantCulture);
                decimal descuentoTotal = decimal.Parse(_datosVenta["descuento_total"], CultureInfo.InvariantCulture);
                decimal impuestoTotal = decimal.Parse(_datosVenta["impuesto_total"], CultureInfo.InvariantCulture);
                decimal importeTotal = decimal.Parse(_datosVenta["importe_total"], CultureInfo.InvariantCulture);

                worksheet.Cell(currentRow, 6).Value = "SUBTOTAL:";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Value = totalBruto;
                worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "$#,##0.00";
                currentRow++;

                worksheet.Cell(currentRow, 6).Value = "DESCUENTO:";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Value = descuentoTotal;
                worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "$#,##0.00";
                currentRow++;

                worksheet.Cell(currentRow, 6).Value = "BASE IMPONIBLE:";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Value = totalBruto - descuentoTotal;
                worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "$#,##0.00";
                currentRow++;

                worksheet.Cell(currentRow, 6).Value = "IMPUESTOS:";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Value = impuestoTotal;
                worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "$#,##0.00";
                currentRow++;

                worksheet.Cell(currentRow, 6).Value = "TOTAL A PAGAR:";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 6).Style.Font.FontSize = 12;
                worksheet.Cell(currentRow, 7).Value = importeTotal;
                worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "$#,##0.00";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Style.Font.FontSize = 12;
                worksheet.Cell(currentRow, 7).Style.Fill.BackgroundColor = XLColor.LightGreen;

                var rangoTotales = worksheet.Range(currentRow, 6, currentRow, 7);
                rangoTotales.Style.Border.OutsideBorder = XLBorderStyleValues.Medium;

                currentRow += 2;

                // Observaciones
                if (!string.IsNullOrEmpty(_datosVenta["observaciones_venta"])) {
                    worksheet.Cell(currentRow, 1).Value = "OBSERVACIONES:";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = _datosVenta["observaciones_venta"];
                    worksheet.Range(currentRow, 1, currentRow, 7).Merge();
                    currentRow++;
                }

                // Método de pago
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Método de Pago:";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = _datosVenta["metodo_pago_principal"];

                // Ajustar anchos de columna
                worksheet.Column(1).Width = 8;
                worksheet.Column(2).Width = 12;
                worksheet.Column(3).Width = 35;
                worksheet.Column(4).Width = 12;
                worksheet.Column(5).Width = 14;
                worksheet.Column(6).Width = 14;
                worksheet.Column(7).Width = 16;

                // Guardar archivo
                string rutaDocumento = Path.Combine(
                    Path.GetTempPath(),
                    $"Factura_{_datosVenta["numero_factura_ticket"].Replace("/", "-")}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                );

                workbook.SaveAs(rutaDocumento);

                if (mostrar) {
                    Process.Start(new ProcessStartInfo {
                        FileName = rutaDocumento,
                        UseShellExecute = true
                    });
                }

                CentroNotificaciones.MostrarNotificacion(
                    "Factura en Excel generada exitosamente.",
                    Core.Modelos.Comun.TipoNotificacion.Info
                );

            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error al generar la factura en Excel: {ex.Message}",
                    Core.Modelos.Comun.TipoNotificacion.Error
                );
            }
        }

        #endregion

        #region Métodos de Dibujo Específicos

        /// <summary>
        /// Dibuja la información del cliente y datos de venta en dos columnas
        /// </summary>
        private void DibujarInformacionClienteYVenta(XGraphics gfx, double yPos) {
            double anchoDisponible = gfx.PageSize.Width - MargenIzquierdo - MargenDerecho;
            double anchoColumna = (anchoDisponible - 20) / 2;

            // Columna izquierda - Cliente
            var datosCliente = new Dictionary<string, string> {
                ["Cliente"] = _datosCliente["nombre_completo"],
                ["Documento"] = $"{_datosCliente["tipo_documento"]}: {_datosCliente["numero_documento"]}",
                ["Código Cliente"] = _datosCliente["codigo_cliente"]
            };

            if (!string.IsNullOrEmpty(_datosCliente["direccion_principal"])) {
                var lineasDireccion = ObtenerLineasTexto(_datosCliente["direccion_principal"], 
                    FontContenidoNegrita, gfx, anchoColumna / 2);

                datosCliente["Dirección"] = lineasDireccion[0];

                if (lineasDireccion.Length > 1) {
                    for (int i = 1; i < lineasDireccion.Length; i++)
                        datosCliente[$"Dirección L{i + 1}"] = lineasDireccion[i];
                }
            }

            DibujarCuadroInformacion(gfx, MargenIzquierdo, yPos, anchoColumna,
                "INFORMACIÓN DEL CLIENTE", datosCliente);

            // Columna derecha - Venta
            var datosVentaInfo = new Dictionary<string, string> {
                ["Almacén"] = _datosVenta["nombre_almacen"],
                ["Vendedor"] = _datosVenta["nombre_vendedor"] ?? "N/A",
                ["Fecha Venta"] = DateTime.Parse(_datosVenta["fecha_venta"]).ToString("dd/MM/yyyy HH:mm")
            };

            if (!string.IsNullOrEmpty(_datosVenta["metodo_pago_principal"])) {
                datosVentaInfo["Método Pago"] = _datosVenta["metodo_pago_principal"];
            }

            DibujarCuadroInformacion(gfx, MargenIzquierdo + anchoColumna + 20, yPos, anchoColumna,
                "INFORMACIÓN DE LA VENTA", datosVentaInfo);

            _alturaTotalInfoClienteYVenta = Math.Max(datosCliente.Count, datosVentaInfo.Count) * 15 + 40;
        }

        #endregion

        #region Métodos Auxiliares

        private string[] ObtenerLineasTexto(string texto, XFont fuente, XGraphics gfx, double anchoMaximo   ) {
            var palabras = texto.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var lineas = new List<string>();
            var lineaActual = string.Empty;

            foreach (var palabra in palabras) {
                var pruebaLinea = string.IsNullOrEmpty(lineaActual) ? palabra : lineaActual + " " + palabra;
                var tamanoPrueba = gfx.MeasureString(pruebaLinea, fuente);

                if (tamanoPrueba.Width <= anchoMaximo) {
                    lineaActual = pruebaLinea;
                } else {
                    if (!string.IsNullOrEmpty(lineaActual)) {
                        lineas.Add(lineaActual);
                    }
                    lineaActual = palabra; // Empezar nueva línea con la palabra actual
                }
            }

            if (!string.IsNullOrEmpty(lineaActual)) {
                lineas.Add(lineaActual);
            }

            return lineas.ToArray();
        }

        /// <summary>
        /// Trunca texto para que quepa en el ancho especificado
        /// </summary>
        private string TruncarTexto(string texto, XFont fuente, XGraphics gfx, double anchoMaximo) {
            var tamano = gfx.MeasureString(texto, fuente);

            if (tamano.Width <= anchoMaximo) {
                return texto;
            }

            var textoTruncado = texto;

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
            ConfigurarEmpresa(
                nombre: "aDVance ERP",
                direccion: "Tu dirección comercial",
                telefono: "+58 (XXX) XXX-XXXX",
                email: "info@advanceerp.com",
                web: "www.advanceerp.com",
                rif: "J-XXXXXXXXX-X"
            );
        }

        /// <summary>
        /// Obtiene los datos principales de la venta
        /// </summary>
        private static Dictionary<string, string> ObtenerDatosVenta(long idVenta) {
            var datos = new Dictionary<string, string>();

            using (var connection = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                connection.Open();

                var query = """
                SELECT 
                    v.id_venta,
                    v.id_cliente,
                    v.id_empleado_vendedor,
                    v.id_almacen,
                    v.numero_factura_ticket,
                    v.fecha_venta,
                    v.total_bruto,
                    v.descuento_total,
                    v.impuesto_total,
                    v.importe_total,
                    v.metodo_pago_principal,
                    v.estado_venta,
                    v.observaciones_venta,
                    a.nombre as nombre_almacen,
                    CONCAT(p.nombre_completo) as nombre_vendedor
                FROM adv__venta v
                INNER JOIN adv__almacen a ON v.id_almacen = a.id_almacen
                LEFT JOIN adv__empleado e ON v.id_empleado_vendedor = e.id_empleado
                LEFT JOIN adv__persona p ON e.id_persona = p.id_persona
                WHERE v.id_venta = @idVenta
                """;

                using (var command = new MySqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@idVenta", idVenta);

                    using (var reader = command.ExecuteReader()) {
                        if (reader.Read()) {
                            datos["id_venta"] = reader["id_venta"].ToString();
                            datos["id_cliente"] = reader["id_cliente"].ToString();
                            datos["numero_factura_ticket"] = reader["numero_factura_ticket"]?.ToString() ?? "N/A";
                            datos["fecha_venta"] = reader["fecha_venta"].ToString();
                            datos["total_bruto"] = reader["total_bruto"].ToString();
                            datos["descuento_total"] = reader["descuento_total"].ToString();
                            datos["impuesto_total"] = reader["impuesto_total"].ToString();
                            datos["importe_total"] = reader["importe_total"].ToString();
                            datos["metodo_pago_principal"] = reader["metodo_pago_principal"]?.ToString() ?? "N/A";
                            datos["estado_venta"] = reader["estado_venta"].ToString();
                            datos["observaciones_venta"] = reader["observaciones_venta"]?.ToString() ?? "";
                            datos["nombre_almacen"] = reader["nombre_almacen"].ToString();
                            datos["nombre_vendedor"] = reader.IsDBNull(reader.GetOrdinal("nombre_vendedor"))
                                ? "N/A"
                                : reader["nombre_vendedor"].ToString();
                        }
                    }
                }
            }

            return datos;
        }

        /// <summary>
        /// Obtiene los datos del cliente
        /// </summary>
        private static Dictionary<string, string> ObtenerDatosCliente(string idCliente) {
            var datos = new Dictionary<string, string>();

            using (var connection = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                connection.Open();

                var query = """
                SELECT 
                    c.codigo_cliente,
                    c.limite_credito,
                    p.nombre_completo,
                    p.tipo_documento,
                    p.numero_documento,
                    p.direccion_principal
                FROM adv__cliente c
                INNER JOIN adv__persona p ON c.id_persona = p.id_persona
                WHERE c.id_cliente = @idCliente
                """;

                using (var command = new MySqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@idCliente", idCliente);

                    using (var reader = command.ExecuteReader()) {
                        if (reader.Read()) {
                            datos["codigo_cliente"] = reader["codigo_cliente"].ToString();
                            datos["limite_credito"] = reader["limite_credito"].ToString();
                            datos["nombre_completo"] = reader["nombre_completo"].ToString();
                            datos["tipo_documento"] = reader["tipo_documento"]?.ToString() ?? "N/A";
                            datos["numero_documento"] = reader["numero_documento"]?.ToString() ?? "N/A";
                            datos["direccion_principal"] = reader["direccion_principal"]?.ToString() ?? "";
                        }
                    }
                }
            }

            return datos;
        }

        /// <summary>
        /// Obtiene los detalles de productos de la venta
        /// </summary>
        private static List<Dictionary<string, string>> ObtenerDetallesProductos(long idVenta) {
            var detalles = new List<Dictionary<string, string>>();

            using (var connection = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                connection.Open();

                var query = """
                SELECT 
                    p.codigo,
                    p.nombre,
                    dvp.cantidad,
                    dvp.precio_venta_unitario,
                    dvp.descuento_item,
                    dvp.subtotal,
                    um.abreviatura as unidad_medida
                FROM adv__detalle_venta_producto dvp
                INNER JOIN adv__producto p ON dvp.id_producto = p.id_producto
                LEFT JOIN adv__unidad_medida um ON p.id_unidad_medida = um.id_unidad_medida
                WHERE dvp.id_venta = @idVenta
                ORDER BY dvp.id_detalle_venta_producto
                """;

                using (var command = new MySqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@idVenta", idVenta);

                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var detalle = new Dictionary<string, string> {
                                ["codigo"] = reader["codigo"].ToString(),
                                ["nombre"] = reader["nombre"].ToString(),
                                ["cantidad"] = reader.GetDecimal("cantidad").ToString("N2", CultureInfo.InvariantCulture),
                                ["precio_venta_unitario"] = reader.GetDecimal("precio_venta_unitario").ToString("N2", CultureInfo.InvariantCulture),
                                ["descuento_item"] = reader.GetDecimal("descuento_item").ToString("N2", CultureInfo.InvariantCulture),
                                ["subtotal"] = reader.GetDecimal("subtotal").ToString("N2", CultureInfo.InvariantCulture),
                                ["unidad_medida"] = reader["unidad_medida"]?.ToString() ?? "N/A"
                            };

                            detalles.Add(detalle);
                        }
                    }
                }
            }

            return detalles;
        }

        #endregion
    }
}