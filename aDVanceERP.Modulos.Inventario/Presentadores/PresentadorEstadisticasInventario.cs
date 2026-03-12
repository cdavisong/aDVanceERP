using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Estadisticas;
using aDVanceERP.Modulos.Inventario.Interfaces;

using System.Drawing.Drawing2D;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorEstadisticasInventario : PresentadorVistaBase<IVistaEstadisticasInventario> {
        private readonly RepoEstadisticasInventario _repo = new();

        private List<MovimientoDiario> _movimientos = [];
        private List<StockPorAlmacen> _stockAlmacen = [];

        // ── Paleta del sistema de diseño aDVance ────────────────────────────
        private static readonly Color ColorEntradas = Color.FromArgb(255, 218, 185); // peachpuff
        private static readonly Color ColorEntradasLine = Color.FromArgb(240, 192, 144); // tono más saturado
        private static readonly Color ColorSalidas = Color.FromArgb(178, 34, 34); // firebrick
        private static readonly Color ColorEjeTexto = Color.FromArgb(136, 136, 136); // text-light
        private static readonly Color ColorGridLine = Color.FromArgb(228, 228, 228); // border
        private static readonly Color ColorFondo = Color.White;

        public PresentadorEstadisticasInventario(IVistaEstadisticasInventario vista) : base(vista) {
            vista.ActualizarTodo += OnActualizarTodo;
            vista.ActualizarEvolucionMovimientos += OnActualizarEvolucionMovimientos;
            vista.ActualizarValorAlmacen += OnActualizarValorAlmacen;

            AgregadorEventos.Suscribir("MostrarVistaEstadisticasInventario", OnMostrarVistaEstadisticasInventario);
        }

        private void OnMostrarVistaEstadisticasInventario(string obj) {
            Vista.Restaurar();

            CargarDatos();

            Vista.Mostrar();
        }

        private void CargarDatos() {
            Task.Run(() => _repo.ObtenerMetricas())
                .ContinueWith(t => {
                    if (t.IsFaulted)
                        return;

                    var m = t.Result;

                    // Todas las actualizaciones de UI deben ir al hilo principal
                    (Vista as Form)?.Invoke(() => {
                        Vista.TotalProductos = m.TotalProductos;
                        Vista.ProductosBajoStockMinimo = m.ProductosBajoStockMinimo;
                        Vista.ProductosSinStock = m.ProductosSinStock;
                        Vista.ValorTotalInventario = m.ValorTotalInventario;
                        Vista.TotalAlmacenes = m.TotalAlmacenes;
                        Vista.MovimientosHoy = m.MovimientosHoy;

                        Vista.CargarTopProductosValor(m.TopProductosValor);

                        _movimientos = m.EvolucionMovimientos ?? [];
                        _stockAlmacen = m.StockPorAlmacen ?? [];

                        Vista.RenderizarGraficosMovimientosAlmacenes();
                    });
                });
        }

        private void OnActualizarTodo(object? sender, EventArgs e) {
            CargarDatos();
        }

        private void OnActualizarEvolucionMovimientos(object? sender, PaintEventArgs e) {
            var g = e.Graphics;
            var rect = e.ClipRectangle;

            // Si el ClipRectangle es vacío (primer Paint antes de layout),
            // usar el tamaño del control emisor.
            if (sender is Control ctrl && (rect.Width == 0 || rect.Height == 0))
                rect = ctrl.ClientRectangle;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(ColorFondo);

            if (_movimientos.Count == 0) {
                DibujarEstadoVacio(g, rect, "Sin datos de movimientos");
                return;
            }

            // ── Márgenes internos ──────────────────────────────────────────
            const int margenIzq = 52;  // espacio para etiquetas del eje Y
            const int margenDer = 12;
            const int margenSup = 10;
            const int margenInf = 52;  // espacio para etiquetas del eje X

            var areaGraf = new RectangleF(
                margenIzq,
                margenSup,
                rect.Width - margenIzq - margenDer,
                rect.Height - margenSup - margenInf);

            if (areaGraf.Width <= 0 || areaGraf.Height <= 0) return;

            // ── Calcular escala ────────────────────────────────────────────
            decimal maxVal = _movimientos
                .SelectMany(m => new[] { m.Entradas, m.Salidas })
                .DefaultIfEmpty(1m)
                .Max();

            if (maxVal == 0) maxVal = 1m;

            decimal valorTecho = RedondeadoArriba(maxVal);
            int pasos = 4; // líneas de grilla horizontales

            // ── Grilla horizontal ──────────────────────────────────────────
            using var penGrid = new Pen(ColorGridLine, 1f) { DashStyle = DashStyle.Dot };
            using var fuenteEje = new Font("Segoe UI", 7.5f, FontStyle.Regular);
            using var brushTexto = new SolidBrush(ColorEjeTexto);

            for (int i = 0; i <= pasos; i++) {
                float y = areaGraf.Top + areaGraf.Height * (1f - (float) i / pasos);
                float valEje = (float) (valorTecho * i / pasos);

                g.DrawLine(penGrid,
                    areaGraf.Left, y,
                    areaGraf.Right, y);

                string etiqueta = FormatearNumeroCorto(valEje);
                var tamTexto = g.MeasureString(etiqueta, fuenteEje);
                g.DrawString(etiqueta, fuenteEje, brushTexto,
                    areaGraf.Left - tamTexto.Width - 4,
                    y - tamTexto.Height / 2f);
            }

            // ── Calcular puntos de las dos series ─────────────────────────
            int n = _movimientos.Count;
            float paso = n > 1 ? areaGraf.Width / (n - 1) : areaGraf.Width;

            PointF[] ptsEntradas = new PointF[n];
            PointF[] ptsSalidas = new PointF[n];

            for (int i = 0; i < n; i++) {
                float x = areaGraf.Left + paso * i;

                float yE = areaGraf.Bottom
                    - (float) (_movimientos[i].Entradas / valorTecho) * areaGraf.Height;
                float yS = areaGraf.Bottom
                    - (float) (_movimientos[i].Salidas / valorTecho) * areaGraf.Height;

                ptsEntradas[i] = new PointF(x, yE);
                ptsSalidas[i] = new PointF(x, yS);
            }

            // ── Área rellena — Entradas ────────────────────────────────────
            if (n >= 2) {
                using var pathEntradas = new GraphicsPath();
                pathEntradas.AddLines(ptsEntradas);
                pathEntradas.AddLine(ptsEntradas[^1], new PointF(areaGraf.Right, areaGraf.Bottom));
                pathEntradas.AddLine(new PointF(areaGraf.Right, areaGraf.Bottom),
                                     new PointF(areaGraf.Left, areaGraf.Bottom));
                pathEntradas.CloseFigure();

                using var brushEntradas = new LinearGradientBrush(
                    new PointF(0, areaGraf.Top),
                    new PointF(0, areaGraf.Bottom),
                    Color.FromArgb(140, ColorEntradas),
                    Color.FromArgb(0, ColorEntradas));

                g.FillPath(brushEntradas, pathEntradas);
            }

            // ── Línea — Entradas ───────────────────────────────────────────
            if (n >= 2) {
                using var penEntradas = new Pen(ColorEntradasLine, 2.0f);
                g.DrawLines(penEntradas, ptsEntradas);
            }

            // ── Línea — Salidas (punteada, semitransparente) ───────────────
            if (n >= 2) {
                using var penSalidas = new Pen(Color.FromArgb(160, ColorSalidas), 1.5f) {
                    DashStyle = DashStyle.Custom,
                    DashPattern = new float[] { 4f, 3f }
                };
                g.DrawLines(penSalidas, ptsSalidas);
            }

            // ── Etiquetas eje X (mostrar cada ~7 días) ─────────────────────
            int intervaloX = Math.Max(1, n / 6);
            for (int i = 0; i < n; i += intervaloX) {
                float x = areaGraf.Left + paso * i;
                string etiq = _movimientos[i].Fecha.ToString("dd/MM");
                var tam = g.MeasureString(etiq, fuenteEje);
                g.DrawString(etiq, fuenteEje, brushTexto,
                    x - tam.Width / 2f,
                    areaGraf.Bottom + 5f);
            }

            // ── Leyenda ────────────────────────────────────────────────────
            DibujarLeyendaLineas(g, rect, fuenteEje, brushTexto);
        }

        private void OnActualizarValorAlmacen(object? sender, PaintEventArgs e) {
            var g = e.Graphics;
            var rect = e.ClipRectangle;

            if (sender is Control ctrl && (rect.Width == 0 || rect.Height == 0))
                rect = ctrl.ClientRectangle;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(ColorFondo);

            if (_stockAlmacen.Count == 0) {
                DibujarEstadoVacio(g, rect, "Sin datos de almacenes");
                return;
            }

            // ── Márgenes ───────────────────────────────────────────────────
            const int margenIzq = 8;
            const int margenDer = 8;
            const int margenSup = 10;
            const int margenInf = 52;  // etiquetas eje X

            var areaGraf = new RectangleF(
                margenIzq,
                margenSup,
                rect.Width - margenIzq - margenDer,
                rect.Height - margenSup - margenInf);

            if (areaGraf.Width <= 0 || areaGraf.Height <= 0) return;

            // ── Datos ──────────────────────────────────────────────────────
            int n = _stockAlmacen.Count;
            decimal maxVal = _stockAlmacen.Max(a => a.ValorTotal);
            if (maxVal == 0) maxVal = 1m;

            // ── Dimensiones de barras ──────────────────────────────────────
            const float gapRatio = 0.25f; // 25% del ancho de columna como gap
            float totalWidth = areaGraf.Width;
            float colWidth = totalWidth / n;
            float barWidth = colWidth * (1f - gapRatio);
            float gapWidth = colWidth * gapRatio / 2f;

            using var fuenteEtiq = new Font("Segoe UI", 7.5f, FontStyle.Regular);
            using var fuenteVal = new Font("Segoe UI", 7f, FontStyle.Bold);
            using var brushTexto = new SolidBrush(ColorEjeTexto);
            using var penGrid = new Pen(ColorGridLine, 1f) { DashStyle = DashStyle.Dot };

            // ── Grilla horizontal ──────────────────────────────────────────
            for (int i = 0; i <= 4; i++) {
                float y = areaGraf.Top + areaGraf.Height * (1f - (float) i / 4);
                g.DrawLine(penGrid, areaGraf.Left, y, areaGraf.Right, y);
            }

            // ── Barras ────────────────────────────────────────────────────
            // Opacidades decrecientes para reflejar ranking (1ro=100%, último≈40%)
            // siguiendo el diseño HTML: opacity 1 → .75 → .55 → .40
            float[] opacidades = CalcularOpacidades(n);

            for (int i = 0; i < n; i++) {
                var almacen = _stockAlmacen[i];
                float proporcion = (float) (almacen.ValorTotal / maxVal);
                float barHeight = areaGraf.Height * proporcion;
                float x = areaGraf.Left + colWidth * i + gapWidth;
                float y = areaGraf.Bottom - barHeight;

                // Color peachpuff con opacidad decreciente
                int alpha = (int) (255 * opacidades[i]);
                using var brushBarra = new SolidBrush(Color.FromArgb(alpha, ColorEntradas));

                // Barra con esquinas superiores redondeadas
                using var pathBarra = CrearRectanguloRedondeado(
                    new RectangleF(x, y, barWidth, barHeight),
                    radio: 3f, soloArriba: true);

                g.FillPath(brushBarra, pathBarra);

                // ── Etiqueta nombre abajo ──────────────────────────────────
                string nombre = AcortarTexto(almacen.NombreAlmacen, 6);
                var tamNom = g.MeasureString(nombre, fuenteEtiq);
                float xCentro = x + barWidth / 2f;

                g.DrawString(nombre, fuenteEtiq, brushTexto,
                    xCentro - tamNom.Width / 2f,
                    areaGraf.Bottom + 5f);

                // ── Valor encima de la barra ───────────────────────────────
                if (barHeight > 18f) {
                    string valStr = FormatearNumeroCorto((float) almacen.ValorTotal);
                    var tamVal = g.MeasureString(valStr, fuenteVal);
                    using var brushValBlanco = new SolidBrush(Color.FromArgb(alpha, ColorEjeTexto));

                    g.DrawString(valStr, fuenteVal, brushValBlanco,
                        xCentro - tamVal.Width / 2f,
                        Math.Max(areaGraf.Top, y - tamVal.Height - 2f));
                }
            }
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir("MostrarVistaEstadisticasInventario", OnMostrarVistaEstadisticasInventario);

            Vista.Cerrar();
        }

        #region HELPERS:

        private static void DibujarEstadoVacio(Graphics g, Rectangle rect, string mensaje) {
            using var f = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            using var b = new SolidBrush(ColorEjeTexto);
            var tam = g.MeasureString(mensaje, f);
            g.DrawString(mensaje, f, b,
                (rect.Width - tam.Width) / 2f,
                (rect.Height - tam.Height) / 2f);
        }

        private static void DibujarLeyendaLineas(
            Graphics g, Rectangle rect,
            Font fuente, SolidBrush brushTexto) {
            float xBase = 52f;
            float yBase = rect.Height - 16f;

            // Entradas
            using var penE = new Pen(ColorEntradasLine, 2f);
            g.DrawLine(penE, xBase, yBase + 4f, xBase + 18f, yBase + 4f);
            g.DrawString("Entradas", fuente, brushTexto, xBase + 22f, yBase);

            // Salidas
            float xS = xBase + 90f;
            using var penS = new Pen(Color.FromArgb(160, ColorSalidas), 1.5f) {
                DashStyle = DashStyle.Custom,
                DashPattern = new float[] { 4f, 3f }
            };
            g.DrawLine(penS, xS, yBase + 4f, xS + 18f, yBase + 4f);
            g.DrawString("Salidas", fuente, brushTexto, xS + 22f, yBase);
        }

        private static decimal RedondeadoArriba(decimal valor) {
            if (valor <= 0) return 10m;
            double v = (double) valor;
            double magnitud = Math.Pow(10, Math.Floor(Math.Log10(v)));
            double techo = Math.Ceiling(v / magnitud) * magnitud;
            return (decimal) techo;
        }

        private static string FormatearNumeroCorto(float valor) {
            if (valor >= 1_000_000) return $"{valor / 1_000_000:F1}M";
            if (valor >= 1_000) return $"{valor / 1_000:F1}k";
            return $"{valor:F0}";
        }

        private static float[] CalcularOpacidades(int n) {
            if (n <= 0) return [];
            var result = new float[n];
            for (int i = 0; i < n; i++) {
                // Interpolación lineal entre 1.0 (posición 0) y 0.35 (posición n-1)
                result[i] = n == 1
                    ? 1f
                    : 1f - (0.65f * i / (n - 1));
            }
            return result;
        }

        private static string AcortarTexto(string texto, int maxChars) {
            if (string.IsNullOrEmpty(texto)) return string.Empty;
            if (texto.Length <= maxChars) return texto;
            return texto[..maxChars] + "…";
        }

        private static GraphicsPath CrearRectanguloRedondeado(
            RectangleF rect, float radio, bool soloArriba = false) {
            var path = new GraphicsPath();
            float r2 = radio * 2;

            if (soloArriba) {
                // Esquina sup-izq redondeada
                path.AddArc(rect.Left, rect.Top, r2, r2, 180, 90);
                // Esquina sup-der redondeada
                path.AddArc(rect.Right - r2, rect.Top, r2, r2, 270, 90);
                // Lado derecho → inf-der (recto)
                path.AddLine(rect.Right, rect.Top + radio, rect.Right, rect.Bottom);
                // Fondo (recto)
                path.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom);
                // Lado izq → sup-izq
                path.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top + radio);
            } else {
                path.AddArc(rect.Left, rect.Top, r2, r2, 180, 90);
                path.AddArc(rect.Right - r2, rect.Top, r2, r2, 270, 90);
                path.AddArc(rect.Right - r2, rect.Bottom - r2, r2, r2, 0, 90);
                path.AddArc(rect.Left, rect.Bottom - r2, r2, r2, 90, 90);
            }

            path.CloseFigure();
            return path;
        }

        #endregion
    }
}
