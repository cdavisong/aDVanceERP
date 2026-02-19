using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Infraestructura.Extensiones.BD;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

using System.Diagnostics;
using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Documentos {

    public class DocCatalogoComercial : DocumentoBase {

        #region Campos privados

        // Filtros opcionales
        private readonly int? _idCategoria;
        private readonly string _busqueda;

        // Datos cargados
        private List<Dictionary<string, string>> _productos = new();

        // Colores adicionales para el catálogo
        private readonly XColor _colorTarjeta = XColor.FromArgb(255, 255, 255);
        private readonly XColor _colorPrecio = XColor.FromArgb(178, 34, 34);   // Firebrick igual que corporativo
        private readonly XColor _colorEtiqueta = XColor.FromArgb(245, 245, 245);
        private readonly XColor _colorSeparador = XColor.FromArgb(224, 224, 224);
        private readonly XColor _colorStockOk = XColor.FromArgb(46, 125, 50);   // Verde
        private readonly XColor _colorStockBajo = XColor.FromArgb(230, 81, 0);    // Naranja

        // Fuente extra grande para precio
        private XFont _fontPrecio;
        private XFont _fontCategoria;

        #endregion

        #region Constructor

        /// <summary>
        /// Genera el catálogo comercial con los productos vendibles que tienen stock.
        /// </summary>
        /// <param name="idCategoria">Opcional: filtrar por clasificación de producto.</param>
        /// <param name="busqueda">Opcional: filtrar por nombre o código.</param>
        public DocCatalogoComercial(int? idCategoria = null, string busqueda = null) {
            _idCategoria = idCategoria;
            _busqueda = busqueda;

            CargarInformacionEmpresa();

            string rutaLogo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo.png");
            if (File.Exists(rutaLogo)) CargarLogo(rutaLogo);

            InicializarFuentesExtra();
        }

        private void InicializarFuentesExtra() {
            var options = new XPdfFontOptions(PdfFontEncoding.Unicode);
            _fontPrecio = new XFont("Arial", 22, XFontStyleEx.Bold, options);
            _fontCategoria = new XFont("Arial", 8, XFontStyleEx.Regular, options);
        }

        #endregion

        #region Override de método abstracto

        public override void GenerarDocumento(bool mostrar = true) {
            GenerarDocumentoPDF(mostrar);
        }

        #endregion

        #region Método público principal

        public void GenerarDocumento(bool mostrar = true, FormatoDocumento formato = FormatoDocumento.PDF) {
            // El catálogo solo tiene sentido en PDF para compartir por WhatsApp
            GenerarDocumentoPDF(mostrar);
        }

        #endregion

        #region Generación de PDF

        private void GenerarDocumentoPDF(bool mostrar) {
            try {
                CargarProductos();

                if (_productos.Count == 0) {
                    CentroNotificaciones.MostrarNotificacion(
                        "No hay productos vendibles con stock disponible para el catálogo.",
                        Core.Modelos.Comun.TipoNotificacion.Advertencia);
                    return;
                }

                var documento = new PdfDocument();
                documento.Info.Title = "Catálogo de Productos";
                documento.Info.Author = NombreEmpresa;
                documento.Info.Creator = "aDVance ERP";
                documento.Info.Subject = $"Catálogo generado el {DateTime.Now:dd/MM/yyyy}";

                // Página de portada
                AgregarPortada(documento);

                // Una página por producto
                int numeroPagina = 2;
                int totalPaginas = _productos.Count + 1; // +1 por portada

                foreach (var producto in _productos) {
                    var pagina = documento.AddPage();
                    pagina.Size = PageSize.Letter;
                    var gfx = XGraphics.FromPdfPage(pagina);

                    DibujarPaginaProducto(gfx, pagina, producto, numeroPagina, totalPaginas);
                    numeroPagina++;
                }

                // Guardar
                string rutaDocumento = Path.Combine(Path.GetTempPath(),
                    $"Catalogo_{NombreEmpresa.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

                documento.Save(rutaDocumento);

                if (mostrar) {
                    Process.Start(new ProcessStartInfo {
                        FileName = rutaDocumento,
                        UseShellExecute = true
                    });
                }

                CentroNotificaciones.MostrarNotificacion(
                    $"Catálogo generado con {_productos.Count} productos.",
                    Core.Modelos.Comun.TipoNotificacion.Info);

            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error al generar el catálogo: {ex.Message}",
                    Core.Modelos.Comun.TipoNotificacion.Error);
            }
        }

        #endregion

        #region Portada

        private void AgregarPortada(PdfDocument documento) {
            var pagina = documento.AddPage();
            pagina.Size = PageSize.Letter;
            var gfx = XGraphics.FromPdfPage(pagina);

            double w = pagina.Width;
            double h = pagina.Height;

            // Fondo completo gris claro
            gfx.DrawRectangle(new XSolidBrush(ColorFondo),
                new XRect(0, 0, w, h));

            // Banda superior en color corporativo (40% de la página)
            double altoBanda = h * 0.40;
            gfx.DrawRectangle(new XSolidBrush(ColorPrimario),
                new XRect(0, 0, w, altoBanda));

            // Línea decorativa inferior de la banda
            gfx.DrawRectangle(new XSolidBrush(ColorSecundario),
                new XRect(0, altoBanda, w, 5));

            // Logo centrado en la banda superior
            double logoSize = 100;
            double logoX = (w - logoSize) / 2;
            double logoY = (altoBanda - logoSize) / 2 - 20;

            if (LogoEmpresa != null) {
                gfx.DrawImage(LogoEmpresa, logoX, logoY, logoSize, logoSize);
            } else {
                gfx.DrawRectangle(XPens.White,
                    new XSolidBrush(XColor.FromArgb(200, 255, 255, 255)),
                    logoX, logoY, logoSize, logoSize);
                gfx.DrawString("LOGO", FontSubtitulo, XBrushes.White,
                    new XRect(logoX, logoY, logoSize, logoSize),
                    XStringFormats.Center);
            }

            // Nombre de la empresa bajo el logo (aún en banda)
            gfx.DrawString(NombreEmpresa.ToUpper(), FontSubtitulo, XBrushes.White,
                new XRect(0, logoY + logoSize + 10, w, 20),
                XStringFormats.Center);

            // Título del catálogo
            double yTexto = altoBanda + 60;
            gfx.DrawString("CATÁLOGO DE PRODUCTOS", _fontPrecio, new XSolidBrush(ColorSecundario),
                new XRect(0, yTexto, w, 35),
                XStringFormats.Center);

            yTexto += 45;

            // Línea decorativa bajo el título
            double lineaX = w * 0.25;
            gfx.DrawLine(new XPen(ColorPrimario, 2), lineaX, yTexto, w - lineaX, yTexto);
            yTexto += 20;

            // Fecha y cantidad de productos
            gfx.DrawString($"{_productos.Count} productos disponibles", FontSubtitulo,
                new XSolidBrush(ColorTextoSecundario),
                new XRect(0, yTexto, w, 20),
                XStringFormats.Center);

            yTexto += 20;
            gfx.DrawString($"Actualizado al {DateTime.Now:dd/MM/yyyy}", FontContenido,
                new XSolidBrush(ColorTextoSecundario),
                new XRect(0, yTexto, w, 16),
                XStringFormats.Center);

            // Datos de contacto en la parte inferior
            double yContacto = h - 120;
            gfx.DrawRectangle(new XSolidBrush(ColorSecundario),
                new XRect(0, yContacto, w, 120));

            yContacto += 20;
            gfx.DrawString("CONTACTO", FontEncabezado, XBrushes.White,
                new XRect(0, yContacto, w, 15),
                XStringFormats.Center);

            yContacto += 20;

            var datosContacto = new List<(string icono, string valor)>();
            if (!string.IsNullOrEmpty(TelefonoEmpresa)) datosContacto.Add(("Tel:", TelefonoEmpresa));
            if (!string.IsNullOrEmpty(EmailEmpresa)) datosContacto.Add(("Email:", EmailEmpresa));
            if (!string.IsNullOrEmpty(WebEmpresa)) datosContacto.Add(("Web:", WebEmpresa));
            if (!string.IsNullOrEmpty(DireccionEmpresa)) datosContacto.Add(("Dir:", DireccionEmpresa));

            foreach (var (icono, valor) in datosContacto) {
                gfx.DrawString($"{icono} {valor}", FontContenido,
                    new XSolidBrush(XColor.FromArgb(200, 200, 200)),
                    new XRect(0, yContacto, w, 14),
                    XStringFormats.Center);
                yContacto += 15;
            }
        }

        #endregion

        #region Página de producto

        private void DibujarPaginaProducto(XGraphics gfx, PdfPage pagina,
            Dictionary<string, string> producto,
            int numeroPagina, int totalPaginas) {

            double w = pagina.Width;
            double h = pagina.Height;

            // Fondo general
            gfx.DrawRectangle(new XSolidBrush(ColorFondo),
                new XRect(0, 0, w, h));

            // --- BANNER SUPERIOR (heredado de DocumentoBase) ---
            DibujarBannerProfesional(gfx, pagina, "CATÁLOGO DE PRODUCTOS",
                producto["codigo"], DateTime.Now);

            double yPos = ObtenerInicioPosicionContenido() + 15;

            // =====================================================
            // SECCIÓN SUPERIOR: Imagen (izquierda) + Info principal (derecha)
            // =====================================================

            double margenContenido = MargenIzquierdo;
            double anchoContenido = w - margenContenido - MargenDerecho;

            // Proporciones: imagen 30%, info 70%
            double anchoImagen = anchoContenido * 0.30;
            double anchoInfo = anchoContenido * 0.67;
            double xInfo = margenContenido + anchoImagen + (anchoContenido * 0.03);
            double altoSeccionSuperior = 180;

            // -- Contenedor imagen --
            var rectImagen = new XRect(margenContenido, yPos, anchoImagen, altoSeccionSuperior);
            gfx.DrawRectangle(new XPen(ColorSeparador, 1), new XSolidBrush(_colorTarjeta), rectImagen);

            string rutaImagen = producto["ruta_imagen"];
            if (!string.IsNullOrEmpty(rutaImagen) && File.Exists(rutaImagen)) {
                try {
                    var imgProducto = XImage.FromFile(rutaImagen);

                    // Escalar manteniendo aspecto dentro del contenedor
                    double escala = Math.Min(anchoImagen / imgProducto.PixelWidth,
                                               altoSeccionSuperior / imgProducto.PixelHeight) * 0.85;
                    double imgW = imgProducto.PixelWidth * escala;
                    double imgH = imgProducto.PixelHeight * escala;
                    double imgX = margenContenido + (anchoImagen - imgW) / 2;
                    double imgY = yPos + (altoSeccionSuperior - imgH) / 2;

                    gfx.DrawImage(imgProducto, imgX, imgY, imgW, imgH);
                } catch {
                    DibujarPlaceholderImagen(gfx, rectImagen);
                }
            } else {
                DibujarPlaceholderImagen(gfx, rectImagen);
            }

            // -- Info principal (derecha de la imagen) --

            double yInfo = yPos;

            // Categoría (etiqueta pequeña)
            string categoria = FormatearCategoria(producto["categoria"]);
            var rectCategoria = new XRect(xInfo, yInfo, anchoInfo, 18);
            gfx.DrawRectangle(new XSolidBrush(ColorPrimario), rectCategoria);
            gfx.DrawString(categoria.ToUpper(), _fontCategoria, XBrushes.White,
                new XRect(xInfo + 8, yInfo, anchoInfo - 8, 18),
                XStringFormats.CenterLeft);

            yInfo += 24;

            // Nombre del producto
            gfx.DrawString(producto["nombre"], FontSubtitulo, new XSolidBrush(ColorSecundario),
                new XRect(xInfo, yInfo, anchoInfo, 20),
                XStringFormats.CenterLeft);

            yInfo += 22;

            // Código
            gfx.DrawString($"Código: {producto["codigo"]}", _fontCategoria,
                new XSolidBrush(ColorTextoSecundario),
                new XRect(xInfo, yInfo, anchoInfo, 14),
                XStringFormats.CenterLeft);

            yInfo += 20;

            // Línea separadora
            gfx.DrawLine(new XPen(_colorSeparador, 0.5), xInfo, yInfo, xInfo + anchoInfo, yInfo);
            yInfo += 12;

            // Precio (grande y destacado)
            string precioTexto = $"$ {producto["precio_venta"]}";
            gfx.DrawString(precioTexto, _fontPrecio, new XSolidBrush(_colorPrecio),
                new XRect(xInfo, yInfo, anchoInfo, 35),
                XStringFormats.CenterLeft);

            yInfo += 40;

            // Stock y unidad de medida
            decimal stock = decimal.TryParse(producto["stock_total"],
                NumberStyles.Any, CultureInfo.InvariantCulture, out var s) ? s : 0;
            decimal stockMin = decimal.TryParse(producto["cantidad_minima"],
                NumberStyles.Any, CultureInfo.InvariantCulture, out var sm) ? sm : 0;

            bool stockBajo = stock <= stockMin && stockMin > 0;

            XColor colorStock = stock > 0
                ? (stockBajo ? _colorStockBajo : _colorStockOk)
                : XColor.FromArgb(229, 57, 53); // rojo: sin stock

            string textoStock = stock > 0
                ? $"Disponible: {stock:N2} {producto["unidad_medida"]}"
                : "Sin stock";

            // Pastilla de stock
            double anchoPastilla = 180;
            double altoPastilla = 22;
            gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(30,
                colorStock.R, colorStock.G, colorStock.B)),
                new XRect(xInfo, yInfo, anchoPastilla, altoPastilla));
            gfx.DrawRectangle(new XPen(colorStock, 1),
                new XRect(xInfo, yInfo, anchoPastilla, altoPastilla));
            gfx.DrawString(textoStock, FontContenidoNegrita, new XSolidBrush(colorStock),
                new XRect(xInfo + 8, yInfo, anchoPastilla - 8, altoPastilla),
                XStringFormats.CenterLeft);

            // =====================================================
            // SECCIÓN DESCRIPCIÓN
            // =====================================================

            double yDesc = yPos + altoSeccionSuperior + 20;

            // Título de sección
            DibujarTituloSeccion(gfx, margenContenido, yDesc, anchoContenido, "DESCRIPCIÓN DEL PRODUCTO");
            yDesc += 28;

            // Texto de descripción con wrap manual
            string descripcion = string.IsNullOrWhiteSpace(producto["descripcion"])
                ? "Sin descripción disponible."
                : producto["descripcion"];

            yDesc = DibujarTextoConWrap(gfx, descripcion, FontContenido,
                new XSolidBrush(ColorTexto),
                margenContenido + 10, yDesc, anchoContenido - 20, 14);

            yDesc += 20;

            // =====================================================
            // SECCIÓN FICHA TÉCNICA (tabla de dos columnas)
            // =====================================================

            DibujarTituloSeccion(gfx, margenContenido, yDesc, anchoContenido, "FICHA TÉCNICA");
            yDesc += 28;

            var ficha = new Dictionary<string, string> {
                ["Código"] = producto["codigo"],
                ["Categoría"] = categoria,
                ["Unidad de Medida"] = producto["unidad_medida"],
                ["Clasificación"] = producto["clasificacion"],
                ["Precio de Venta"] = $"$ {producto["precio_venta"]}",
                ["Stock Total"] = $"{stock:N2} {producto["unidad_medida"]}",
            };

            yDesc = DibujarFichaTecnica(gfx, margenContenido, yDesc, anchoContenido, ficha);

            // =====================================================
            // PIE DE PÁGINA
            // =====================================================

            DibujarPiePagina(gfx, pagina, numeroPagina, totalPaginas,
                $"Precios sujetos a cambio sin previo aviso · {NombreEmpresa}");
        }

        #endregion

        #region Helpers de dibujo

        private void DibujarPlaceholderImagen(XGraphics gfx, XRect rect) {
            gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(235, 235, 235)), rect);
            gfx.DrawString("Sin imagen", FontPequeno, new XSolidBrush(ColorTextoSecundario),
                rect, XStringFormats.Center);
        }

        /// <summary>
        /// Dibuja un título de sección con barra lateral en color corporativo.
        /// </summary>
        private void DibujarTituloSeccion(XGraphics gfx, double x, double y,
            double ancho, string titulo) {

            // Barra lateral
            gfx.DrawRectangle(new XSolidBrush(ColorPrimario),
                new XRect(x, y, 4, 20));

            // Fondo suave
            gfx.DrawRectangle(new XSolidBrush(_colorEtiqueta),
                new XRect(x + 4, y, ancho - 4, 20));

            // Texto
            gfx.DrawString(titulo, FontEncabezado, new XSolidBrush(ColorSecundario),
                new XRect(x + 12, y, ancho - 12, 20),
                XStringFormats.CenterLeft);
        }

        /// <summary>
        /// Dibuja texto con salto de línea automático. Devuelve la Y final.
        /// </summary>
        private double DibujarTextoConWrap(XGraphics gfx, string texto, XFont fuente,
            XBrush pincel, double x, double y, double anchoMax, double alturaLinea) {

            var palabras = texto.Split(' ');
            string linea = string.Empty;

            foreach (var palabra in palabras) {
                string candidata = string.IsNullOrEmpty(linea) ? palabra : linea + " " + palabra;
                double ancho = gfx.MeasureString(candidata, fuente).Width;

                if (ancho > anchoMax && !string.IsNullOrEmpty(linea)) {
                    gfx.DrawString(linea, fuente, pincel, new XPoint(x, y));
                    y += alturaLinea;
                    linea = palabra;
                } else {
                    linea = candidata;
                }
            }

            if (!string.IsNullOrEmpty(linea)) {
                gfx.DrawString(linea, fuente, pincel, new XPoint(x, y));
                y += alturaLinea;
            }

            return y;
        }

        /// <summary>
        /// Dibuja una tabla de dos columnas para la ficha técnica.
        /// Devuelve la Y final.
        /// </summary>
        private double DibujarFichaTecnica(XGraphics gfx, double x, double y,
            double ancho, Dictionary<string, string> datos) {

            double alturaFila = 22;
            double anchoClave = ancho * 0.38;
            double anchoValor = ancho * 0.62;
            int indice = 0;

            foreach (var par in datos) {
                bool esAlternada = indice % 2 != 0;

                XColor colorFila = esAlternada
                    ? XColor.FromArgb(248, 248, 248)
                    : _colorTarjeta;

                // Fondo fila
                gfx.DrawRectangle(new XSolidBrush(colorFila),
                    new XRect(x, y, ancho, alturaFila));

                // Borde inferior
                gfx.DrawLine(new XPen(_colorSeparador, 0.5),
                    x, y + alturaFila, x + ancho, y + alturaFila);

                // Clave
                gfx.DrawString(par.Key, FontContenidoNegrita, new XSolidBrush(ColorSecundario),
                    new XRect(x + 10, y, anchoClave - 10, alturaFila),
                    XStringFormats.CenterLeft);

                // Separador vertical
                gfx.DrawLine(new XPen(_colorSeparador, 0.5),
                    x + anchoClave, y + 3, x + anchoClave, y + alturaFila - 3);

                // Valor
                gfx.DrawString(par.Value, FontContenido, new XSolidBrush(ColorTexto),
                    new XRect(x + anchoClave + 10, y, anchoValor - 10, alturaFila),
                    XStringFormats.CenterLeft);

                y += alturaFila;
                indice++;
            }

            // Borde exterior de la tabla
            gfx.DrawRectangle(new XPen(_colorSeparador, 1),
                new XRect(x, y - (datos.Count * alturaFila), ancho, datos.Count * alturaFila));

            return y + 10;
        }

        private string FormatearCategoria(string categoria) => categoria switch {
            "Mercancia" => "Mercancía",
            "ProductoTerminado" => "Producto Terminado",
            "MateriaPrima" => "Materia Prima",
            _ => categoria
        };

        private XColor ColorSeparador => _colorSeparador;

        #endregion

        #region Carga de datos

        private void CargarProductos() {
            _productos.Clear();

            using var connection = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());
            connection.Open();

            var query = """
                SELECT
                    p.id_producto,
                    p.codigo,
                    p.nombre,
                    p.descripcion,
                    p.categoria,
                    p.ruta_imagen,
                    p.precio_venta_base                         AS precio_venta,
                    COALESCE(um.nombre, 'Unidad')               AS unidad_medida,
                    COALESCE(cp.nombre, 'General')              AS clasificacion,
                    COALESCE(SUM(inv.cantidad), 0)              AS stock_total,
                    COALESCE(MAX(inv.cantidad_minima), 0)       AS cantidad_minima
                FROM adv__producto p
                LEFT JOIN adv__unidad_medida          um  ON p.id_unidad_medida          = um.id_unidad_medida
                LEFT JOIN adv__clasificacion_producto cp  ON p.id_clasificacion_producto = cp.id_clasificacion_producto
                LEFT JOIN adv__inventario             inv ON p.id_producto               = inv.id_producto
                WHERE p.es_vendible = 1
                  AND p.activo      = 1
                """;

            if (_idCategoria.HasValue)
                query += " AND p.id_clasificacion_producto = @idCategoria";

            if (!string.IsNullOrWhiteSpace(_busqueda))
                query += " AND (p.nombre LIKE @busqueda OR p.codigo LIKE @busqueda)";

            query += """

                GROUP BY
                    p.id_producto, p.codigo, p.nombre, p.descripcion,
                    p.categoria, p.ruta_imagen, p.precio_venta_base,
                    um.nombre, cp.nombre
                HAVING stock_total > 0
                ORDER BY p.nombre
                """;

            using var command = new MySqlCommand(query, connection);

            if (_idCategoria.HasValue)
                command.Parameters.AddWithValue("@idCategoria", _idCategoria.Value);

            if (!string.IsNullOrWhiteSpace(_busqueda))
                command.Parameters.AddWithValue("@busqueda", $"%{_busqueda}%");

            using var reader = command.ExecuteReader();
            while (reader.Read()) {
                _productos.Add(new Dictionary<string, string> {
                    ["codigo"] = reader["codigo"].ToString() ?? string.Empty,
                    ["nombre"] = reader["nombre"].ToString() ?? string.Empty,
                    ["descripcion"] = reader["descripcion"].ToString() ?? string.Empty,
                    ["categoria"] = reader["categoria"].ToString() ?? string.Empty,
                    ["ruta_imagen"] = reader["ruta_imagen"].ToString() ?? string.Empty,
                    ["precio_venta"] = reader.IsDBNull(reader.GetOrdinal("precio_venta")) ? "0.00"
                                            : reader.GetDecimal("precio_venta").ToString("N2", CultureInfo.InvariantCulture),
                    ["unidad_medida"] = reader["unidad_medida"].ToString() ?? "Unidad",
                    ["clasificacion"] = reader["clasificacion"].ToString() ?? "General",
                    ["stock_total"] = reader.GetDecimal("stock_total").ToString("N2", CultureInfo.InvariantCulture),
                    ["cantidad_minima"] = reader.GetDecimal("cantidad_minima").ToString("N2", CultureInfo.InvariantCulture),
                });
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