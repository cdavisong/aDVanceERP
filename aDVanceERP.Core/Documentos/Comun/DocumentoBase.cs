using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace aDVanceERP.Core.Documentos.Comun {
    public abstract class DocumentoBase {
        #region Propiedades de Configuración

        // Márgenes
        protected int MargenIzquierdo { get; set; } = 40;
        protected int MargenDerecho { get; set; } = 40;
        protected int MargenSuperior { get; set; } = 20;
        protected int MargenInferior { get; set; } = 40;

        // Altura del banner de encabezado
        protected int AlturaBanner { get; set; } = 120;

        // Colores corporativos (Basado en aDVance ERP)
        protected XColor ColorPrimario { get; set; } = XColor.FromKnownColor(XKnownColor.Firebrick); // Rojo ladrillo
        protected XColor ColorSecundario { get; set; } = XColor.FromArgb(51, 51, 51); // Gris oscuro
        protected XColor ColorFondo { get; set; } = XColor.FromArgb(245, 245, 245); // Gris claro
        protected XColor ColorTexto { get; set; } = XColor.FromArgb(33, 33, 33);
        protected XColor ColorTextoSecundario { get; set; } = XColor.FromArgb(117, 117, 117);

        // Fuentes
        protected XFont FontTitulo { get; set; }
        protected XFont FontSubtitulo { get; set; }
        protected XFont FontEncabezado { get; set; }
        protected XFont FontContenido { get; set; }
        protected XFont FontContenidoNegrita { get; set; }
        protected XFont FontPequeno { get; set; }

        // Información de la empresa
        protected string NombreEmpresa { get; set; } = "aDVance ERP";
        protected string DireccionEmpresa { get; set; }
        protected string TelefonoEmpresa { get; set; }
        protected string EmailEmpresa { get; set; }
        protected string WebEmpresa { get; set; }
        protected string RifEmpresa { get; set; }
        protected XImage LogoEmpresa { get; set; }

        #endregion

        #region Constructor

        protected DocumentoBase() {
            InicializarFuentes();
        }

        private void InicializarFuentes() {
            var options = new XPdfFontOptions(PdfFontEncoding.Unicode);

            FontTitulo = new XFont("Arial", 18, XFontStyleEx.Bold, options);
            FontSubtitulo = new XFont("Arial", 12, XFontStyleEx.Bold, options);
            FontEncabezado = new XFont("Arial", 9, XFontStyleEx.Bold, options);
            FontContenido = new XFont("Arial", 9, XFontStyleEx.Regular, options);
            FontContenidoNegrita = new XFont("Arial", 9, XFontStyleEx.Bold, options);
            FontPequeno = new XFont("Arial", 7, XFontStyleEx.Regular, options);
        }

        #endregion

        #region Métodos para Cargar Configuración de Empresa

        /// <summary>
        /// Carga la información de la empresa desde la base de datos
        /// </summary>
        protected virtual void CargarInformacionEmpresa() {
            // Implementar en clase derivada para cargar desde BD
            // Este es un método que cada aplicación puede sobrescribir
        }

        /// <summary>
        /// Carga el logo de la empresa
        /// </summary>
        /// <param name="rutaLogo">Ruta al archivo del logo</param>
        protected void CargarLogo(string rutaLogo) {
            if (File.Exists(rutaLogo)) {
                LogoEmpresa = XImage.FromFile(rutaLogo);
            }
        }

        /// <summary>
        /// Configura los datos de la empresa manualmente
        /// </summary>
        protected void ConfigurarEmpresa(string nombre, string direccion, string telefono,
                                        string email, string web, string rif) {
            NombreEmpresa = nombre;
            DireccionEmpresa = direccion;
            TelefonoEmpresa = telefono;
            EmailEmpresa = email;
            WebEmpresa = web;
            RifEmpresa = rif;
        }

        #endregion

        #region Banner y Encabezado Principal

        /// <summary>
        /// Dibuja el banner profesional en el encabezado del documento
        /// </summary>
        protected virtual void DibujarBannerProfesional(XGraphics gfx, PdfPage pagina,
                                                       string tipoDocumento,
                                                       string numeroDocumento = null,
                                                       DateTime? fechaDocumento = null) {
            double yPos = MargenSuperior;

            // Fondo del banner con gradiente simulado
            var rectBanner = new XRect(0, 0, pagina.Width, AlturaBanner);
            gfx.DrawRectangle(new XSolidBrush(ColorFondo), rectBanner);

            // Línea superior decorativa en color corporativo
            gfx.DrawRectangle(new XSolidBrush(ColorPrimario),
                new XRect(0, 0, pagina.Width, 4));

            // Logo de la empresa (izquierda)
            double logoX = MargenIzquierdo;
            double logoY = yPos;
            double logoWidth = 80;
            double logoHeight = 80;

            if (LogoEmpresa != null) {
                gfx.DrawImage(LogoEmpresa, logoX, logoY, logoWidth, logoHeight);
            } else {
                // Dibujar placeholder si no hay logo
                gfx.DrawRectangle(XPens.Gray, new XSolidBrush(XColor.FromArgb(230, 230, 230)),
                    logoX, logoY, logoWidth, logoHeight);

                var fontPlaceholder = new XFont("Arial", 8, XFontStyleEx.Bold);
                gfx.DrawString("LOGO", fontPlaceholder, new XSolidBrush(ColorTextoSecundario),
                    new XRect(logoX, logoY, logoWidth, logoHeight),
                    XStringFormats.Center);
            }

            // Información de la empresa (centro-izquierda)
            double infoX = logoX + logoWidth + 20;
            double infoY = logoY + 20;

            gfx.DrawString(NombreEmpresa, FontSubtitulo, new XSolidBrush(ColorSecundario),
                new XPoint(infoX, infoY));
            infoY += 15;

            if (!string.IsNullOrEmpty(RifEmpresa)) {
                gfx.DrawString($"RIF: {RifEmpresa}", FontPequeno, new XSolidBrush(ColorTextoSecundario),
                    new XPoint(infoX, infoY));
                infoY += 12;
            }

            if (!string.IsNullOrEmpty(DireccionEmpresa)) {
                gfx.DrawString(DireccionEmpresa, FontPequeno, new XSolidBrush(ColorTextoSecundario),
                    new XPoint(infoX, infoY));
                infoY += 10;
            }

            if (!string.IsNullOrEmpty(TelefonoEmpresa)) {
                gfx.DrawString($"Tel: {TelefonoEmpresa}", FontPequeno, new XSolidBrush(ColorTextoSecundario),
                    new XPoint(infoX, infoY));
                infoY += 10;
            }

            if (!string.IsNullOrEmpty(EmailEmpresa)) {
                gfx.DrawString(EmailEmpresa, FontPequeno, new XSolidBrush(ColorTextoSecundario),
                    new XPoint(infoX, infoY));
            }

            // Información del documento (derecha)
            double docInfoX = pagina.Width - MargenDerecho - 180;
            double docInfoY = logoY;

            // Tipo de documento con fondo de color
            var rectTipoDoc = new XRect(docInfoX, docInfoY, 180, 25);
            gfx.DrawRectangle(new XSolidBrush(ColorPrimario), rectTipoDoc);
            gfx.DrawString(tipoDocumento.ToUpper(), FontSubtitulo, XBrushes.White,
                new XRect(docInfoX, docInfoY + 2, 180, 20),
                XStringFormats.Center);

            docInfoY += 30;

            // Número de documento
            if (!string.IsNullOrEmpty(numeroDocumento)) {
                gfx.DrawString("Documento:", FontPequeno, new XSolidBrush(ColorTextoSecundario),
                    new XPoint(docInfoX, docInfoY + 10));
                gfx.DrawString(numeroDocumento, FontContenidoNegrita, new XSolidBrush(ColorSecundario),
                    new XPoint(docInfoX, docInfoY + 20));
                docInfoY += 25;
            }

            // Fecha del documento
            if (fechaDocumento.HasValue) {
                gfx.DrawString("Fecha:", FontPequeno, new XSolidBrush(ColorTextoSecundario),
                    new XPoint(docInfoX, docInfoY + 10));
                gfx.DrawString(fechaDocumento.Value.ToString("dd/MM/yyyy HH:mm"),
                    FontContenidoNegrita, new XSolidBrush(ColorSecundario),
                    new XPoint(docInfoX, docInfoY + 20));
            }

            // Línea separadora inferior
            gfx.DrawLine(new XPen(ColorPrimario, 2),
                MargenIzquierdo, AlturaBanner - 5,
                pagina.Width - MargenDerecho, AlturaBanner - 5);
        }

        #endregion

        #region Encabezado de Tabla

        /// <summary>
        /// Dibuja el encabezado de una tabla con estilo profesional
        /// </summary>
        protected void DibujarEncabezadoTabla(XGraphics gfx, double yPos,
                                             Dictionary<string, double> columnas,
                                             double alturaEncabezado = 25) {
            double xPos = MargenIzquierdo;

            // Fondo del encabezado
            gfx.DrawRectangle(new XSolidBrush(ColorSecundario),
                new XRect(xPos, yPos,
                    gfx.PageSize.Width - MargenIzquierdo - MargenDerecho,
                    alturaEncabezado));

            // Dibujar títulos de columnas
            foreach (var columna in columnas) {
                var rect = new XRect(xPos + 5, yPos + 7, columna.Value - 10, alturaEncabezado - 10);
                gfx.DrawString(columna.Key, FontEncabezado, XBrushes.White, rect,
                    XStringFormats.TopLeft);

                // Línea separadora vertical (excepto la última)
                if (columna.Key != columnas.Keys.Last()) {
                    gfx.DrawLine(new XPen(XColor.FromArgb(80, 255, 255, 255), 1),
                        xPos + columna.Value, yPos + 3,
                        xPos + columna.Value, yPos + alturaEncabezado - 3);
                }

                xPos += columna.Value;
            }
        }

        #endregion

        #region Fila de Tabla

        /// <summary>
        /// Dibuja una fila de datos en una tabla
        /// </summary>
        protected void DibujarFilaTabla(XGraphics gfx, double yPos,
                                       Dictionary<string, (double ancho, string valor, XStringFormat formato)> datos,
                                       bool esAlternada = false,
                                       double alturaFila = 18) {
            double xPos = MargenIzquierdo;

            // Fondo alternado para mejor legibilidad
            if (esAlternada) {
                gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(250, 250, 250)),
                    new XRect(xPos, yPos,
                        gfx.PageSize.Width - MargenIzquierdo - MargenDerecho,
                        alturaFila));
            }

            // Dibujar datos de cada columna
            foreach (var dato in datos.Values) {
                var rect = new XRect(xPos + 5, yPos + 2, dato.ancho - 10, alturaFila - 4);
                gfx.DrawString(dato.valor, FontContenido, new XSolidBrush(ColorTexto),
                    rect, dato.formato);
                xPos += dato.ancho;
            }

            // Línea separadora inferior sutil
            gfx.DrawLine(new XPen(XColor.FromArgb(230, 230, 230), 1),
                MargenIzquierdo, yPos + alturaFila,
                gfx.PageSize.Width - MargenDerecho, (yPos % 2 == 0 ? yPos - 1 : yPos - 0.5) + alturaFila);
        }

        #endregion

        #region Sección de Totales

        /// <summary>
        /// Dibuja una sección de totales al final de un documento
        /// </summary>
        protected void DibujarSeccionTotales(XGraphics gfx, double yPos,
                                            Dictionary<string, string> totales,
                                            double anchoSeccion = 300) {
            double xPos = gfx.PageSize.Width - MargenDerecho - anchoSeccion;
            double alturaFila = 20;

            // Fondo de la sección de totales
            double alturaTotal = totales.Count * alturaFila + 10;
            gfx.DrawRectangle(new XSolidBrush(ColorFondo),
                new XRect(xPos, yPos, anchoSeccion, alturaTotal));

            // Borde de la sección
            gfx.DrawRectangle(new XPen(ColorSecundario, 1),
                new XRect(xPos, yPos, anchoSeccion, alturaTotal));

            yPos += 5;

            foreach (var total in totales) {
                // Etiqueta
                gfx.DrawString(total.Key + ":", FontContenidoNegrita, new XSolidBrush(ColorSecundario),
                    new XRect(xPos + 10, yPos, anchoSeccion / 2, alturaFila),
                    XStringFormats.CenterLeft);

                // Valor
                gfx.DrawString(total.Value, FontContenidoNegrita, new XSolidBrush(ColorSecundario),
                    new XRect(xPos + anchoSeccion / 2, yPos, anchoSeccion / 2 - 10, alturaFila),
                    XStringFormats.CenterRight);

                yPos += alturaFila;
            }
        }

        #endregion

        #region Pie de Página

        /// <summary>
        /// Dibuja el pie de página con número de página y fecha
        /// </summary>
        protected virtual void DibujarPiePagina(XGraphics gfx, PdfPage pagina,
                                               int paginaActual, int totalPaginas,
                                               string textoAdicional = null) {
            double yPos = pagina.Height - MargenInferior + 10;

            // Línea superior del pie
            gfx.DrawLine(new XPen(ColorTextoSecundario, 0.5),
                MargenIzquierdo, yPos - 5,
                pagina.Width - MargenDerecho, yPos - 5);

            // Texto izquierdo (opcional)
            if (!string.IsNullOrEmpty(textoAdicional)) {
                gfx.DrawString(textoAdicional, FontPequeno, new XSolidBrush(ColorTextoSecundario),
                    new XRect(MargenIzquierdo, yPos, 200, 15),
                    XStringFormats.CenterLeft);
            }

            // Centro: Nombre del sistema
            gfx.DrawString($"Generado por {NombreEmpresa}", FontPequeno,
                new XSolidBrush(ColorTextoSecundario),
                new XRect(0, yPos, pagina.Width, 15),
                XStringFormats.Center);

            // Derecha: Paginación y fecha
            string textoDerecha = $"Página {paginaActual} de {totalPaginas} | {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
            gfx.DrawString(textoDerecha, FontPequeno, new XSolidBrush(ColorTextoSecundario),
                new XRect(pagina.Width - MargenDerecho - 200, yPos, 200, 15),
                XStringFormats.CenterRight);
        }

        #endregion

        #region Métodos Auxiliares

        /// <summary>
        /// Verifica si se necesita una nueva página
        /// </summary>
        protected bool NecesitaNuevaPagina(double yPosActual, double espacioNecesario, double alturaPagina) {
            return yPosActual + espacioNecesario > alturaPagina - MargenInferior - 30;
        }

        /// <summary>
        /// Calcula el inicio del área de contenido (después del banner)
        /// </summary>
        protected double ObtenerInicioPosicionContenido() {
            return AlturaBanner + 10;
        }

        /// <summary>
        /// Dibuja un cuadro de información con título y contenido
        /// </summary>
        protected void DibujarCuadroInformacion(XGraphics gfx, double x, double y,
                                               double ancho, string titulo,
                                               Dictionary<string, string> datos) {
            double alturaFila = 15;
            double alturaTotal = (datos.Count + 1) * alturaFila + 10;

            // Fondo
            gfx.DrawRectangle(new XSolidBrush(ColorFondo),
                new XRect(x, y, ancho, alturaTotal));

            // Borde
            gfx.DrawRectangle(new XPen(ColorSecundario, 1),
                new XRect(x, y, ancho, alturaTotal));

            // Título con fondo de color
            gfx.DrawRectangle(new XSolidBrush(ColorPrimario),
                new XRect(x, y, ancho, 20));
            gfx.DrawString(titulo, FontEncabezado, XBrushes.White,
                new XRect(x + 5, y, ancho - 10, 20),
                XStringFormats.CenterLeft);

            y += 32;

            // Datos
            foreach (var dato in datos) {
                gfx.DrawString(dato.Key + ":", FontContenido, new XSolidBrush(ColorTextoSecundario),
                    new XPoint(x + 10, y));
                gfx.DrawString(dato.Value, FontContenidoNegrita, new XSolidBrush(ColorTexto),
                    new XPoint(x + ancho / 2, y));
                y += alturaFila;
            }
        }

        #endregion

        #region Método Abstracto

        /// <summary>
        /// Método abstracto que cada documento debe implementar para generar su contenido
        /// </summary>
        public abstract void GenerarDocumento(bool mostrar = true);

        #endregion
    }
}
