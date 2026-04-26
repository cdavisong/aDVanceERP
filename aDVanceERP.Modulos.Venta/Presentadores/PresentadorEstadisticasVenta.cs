using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Estadisticas;
using aDVanceERP.Modulos.Venta.Interfaces;

using System.Drawing.Drawing2D;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorEstadisticasVenta : PresentadorVistaBase<IVistaEstadisticasVenta> {
        private readonly RepoEstadisticasVenta _repo = new();

        private List<IngresoDiario> _ingresoDiario = [];
        private List<VentasPorMetodoPago> _metodosPago = [];

        // ── Paleta del sistema de diseño aDVance ─────────────────────────
        private static readonly Color ColorAreaRelleno = Color.FromArgb(255, 218, 185); // peachpuff
        private static readonly Color ColorLinea = Color.FromArgb(240, 192, 144); // peachpuff saturado

        // Donut métodos de pago
        private static readonly Color ColorEfectivo = Color.FromArgb(255, 218, 185); // peachpuff
        private static readonly Color ColorTransferencia = Color.FromArgb(173, 216, 230); // lightblue
        private static readonly Color ColorTarjeta = Color.FromArgb(232, 149, 74); // ámbar
        private static readonly Color ColorOtro = Color.FromArgb(178, 34, 34); // firebrick

        // Neutros
        private static readonly Color ColorEjeTexto = Color.FromArgb(136, 136, 136);
        private static readonly Color ColorGridLine = Color.FromArgb(228, 228, 228);
        private static readonly Color ColorFondo = Color.White;

        // Mapa método de pago → color (insensible a mayúsculas)
        private static readonly Dictionary<string, Color> MapaColoresPago = new() {
            { "Efectivo",               ColorEfectivo      },
            { "Cash",                   ColorEfectivo      },
            { "Transferencia",          ColorTransferencia },
            { "Transferencia bancaria", ColorTransferencia },
            { "Tarjeta",                ColorTarjeta       },
            { "Tarjeta de crédito",     ColorTarjeta       },
            { "Tarjeta de débito",      ColorTarjeta       },
        };

        public PresentadorEstadisticasVenta(IVistaEstadisticasVenta vista) : base(vista) {
            vista.ActualizarTodo += OnActualizarTodo;
            vista.ActualizarIngresosDiarios += OnActualizarIngresosDiarios;
            vista.ActualizarMetodosPago += OnActualizarMetodosPago;

            AgregadorEventos.Suscribir("MostrarVistaEstadisticasVentas", OnMostrarVistaEstadisticasVentas);
        }

        private void OnMostrarVistaEstadisticasVentas(string obj) {
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
                        Vista.VentasHoy = m.VentasHoy;
                        Vista.IngresosHoy = m.IngresosHoy;
                        Vista.IngresosMes = m.IngresosMes;
                        Vista.IngresosMesAnterior = m.IngresosMesAnterior;
                        Vista.VentasPendientes = m.VentasPendientes;
                        Vista.PagosPendientesConfirmacion = m.PagosPendientesConfirmacion;
                        Vista.PedidosActivos = m.PedidosActivos;

                        Vista.CargarTopProductos(m.TopProductos);

                        _ingresoDiario = m.EvolucionIngresos;
                        _metodosPago = m.DistribucionMetodosPago;

                        //Vista.ActualizarPorcentajeVentasHoyVsAyer(m.VentasHoy, m.VentasAyer);
                        Vista.ActualizarPorcentajeIngresosVsMesAnterior(m.IngresosMes, m.IngresosMesAnterior);
                        Vista.RenderizarGraficosGastosDistribuciones();
                    });
                });
        }

        private void OnActualizarTodo(object? sender, EventArgs e) {
            CargarDatos();
        }

        private void OnActualizarIngresosDiarios(object? sender, PaintEventArgs e) {
            var g = e.Graphics;
            var rect = e.ClipRectangle;

            if (sender is Control ctrl && (rect.Width == 0 || rect.Height == 0))
                rect = ctrl.ClientRectangle;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(ColorFondo);

            if (_ingresoDiario.Count == 0) {
                DibujarEstadoVacio(g, rect, "Sin datos de ingresos diarios");
                return;
            }

            // ── Márgenes ──────────────────────────────────────────────────
            const int margenIzq = 52;
            const int margenDer = 12;
            const int margenSup = 10;
            const int margenInf = 52;

            var area = new RectangleF(
                margenIzq,
                margenSup,
                rect.Width - margenIzq - margenDer,
                rect.Height - margenSup - margenInf);

            if (area.Width <= 0 || area.Height <= 0) return;

            // ── Escala ────────────────────────────────────────────────────
            decimal maxVal = _ingresoDiario.Max(d => d.Ingresos);
            if (maxVal == 0) maxVal = 1m;
            decimal techo = RedondeadoArriba(maxVal);

            // ── Grilla horizontal ─────────────────────────────────────────
            using var penGrid = new Pen(ColorGridLine, 1f) { DashStyle = DashStyle.Dot };
            using var fuenteEje = new Font("Segoe UI", 7.5f);
            using var brushTexto = new SolidBrush(ColorEjeTexto);
            const int pasos = 4;

            for (int i = 0; i <= pasos; i++) {
                float y = area.Top + area.Height * (1f - (float) i / pasos);
                float valEje = (float) (techo * i / pasos);

                g.DrawLine(penGrid, area.Left, y, area.Right, y);

                string etiq = FormatearNumeroCorto(valEje);
                var tam = g.MeasureString(etiq, fuenteEje);
                g.DrawString(etiq, fuenteEje, brushTexto,
                    area.Left - tam.Width - 4f,
                    y - tam.Height / 2f);
            }

            // ── Puntos de la serie ────────────────────────────────────────
            int n = _ingresoDiario.Count;
            float paso = n > 1 ? area.Width / (n - 1) : area.Width;
            var pts = new PointF[n];

            for (int i = 0; i < n; i++) {
                pts[i] = new PointF(
                    area.Left + paso * i,
                    area.Bottom - (float) (_ingresoDiario[i].Ingresos / techo) * area.Height);
            }

            // ── Área rellena ──────────────────────────────────────────────
            if (n >= 2) {
                using var pathArea = new GraphicsPath();
                pathArea.AddLines(pts);
                pathArea.AddLine(pts[^1], new PointF(area.Right, area.Bottom));
                pathArea.AddLine(new PointF(area.Right, area.Bottom),
                                 new PointF(area.Left, area.Bottom));
                pathArea.CloseFigure();

                using var brushArea = new LinearGradientBrush(
                    new PointF(0, area.Top),
                    new PointF(0, area.Bottom),
                    Color.FromArgb(150, ColorAreaRelleno),
                    Color.FromArgb(0, ColorAreaRelleno));

                g.FillPath(brushArea, pathArea);
            }

            // ── Línea ─────────────────────────────────────────────────────
            if (n >= 2) {
                using var penLinea = new Pen(ColorLinea, 2f);
                g.DrawLines(penLinea, pts);
            }

            // ── Etiquetas eje X (cada ~7 días) ────────────────────────────
            int intervaloX = Math.Max(1, n / 6);
            for (int i = 0; i < n; i += intervaloX) {
                float x = area.Left + paso * i;
                string etiq = _ingresoDiario[i].Fecha.ToString("dd/MM");
                var tam = g.MeasureString(etiq, fuenteEje);
                g.DrawString(etiq, fuenteEje, brushTexto,
                    x - tam.Width / 2f,
                    area.Bottom + 5f);
            }

            // ── Etiqueta de leyenda (una sola serie) ──────────────────────
            float xLey = 52f, yLey = rect.Height - 16f;
            using var penLey = new Pen(ColorLinea, 2f);
            g.DrawLine(penLey, xLey, yLey + 4f, xLey + 18f, yLey + 4f);
            g.DrawString("Ingresos diarios", fuenteEje, brushTexto, xLey + 22f, yLey);
        }

        private void OnActualizarMetodosPago(object? sender, PaintEventArgs e) {
            var g = e.Graphics;
            var rect = e.ClipRectangle;

            if (sender is Control ctrl && (rect.Width == 0 || rect.Height == 0))
                rect = ctrl.ClientRectangle;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(ColorFondo);

            if (_metodosPago.Count == 0) {
                DibujarEstadoVacio(g, rect, "Sin datos de métodos de pago");
                return;
            }

            // ── Totales ───────────────────────────────────────────────────
            int total = _metodosPago.Sum(m => m.Cantidad);
            if (total == 0) total = 1;

            var segmentos = _metodosPago
                .OrderByDescending(m => m.Cantidad)
                .Select(m => (
                    Metodo: m.MetodoPago,
                    Cantidad: m.Cantidad,
                    Color: ResolverColorMetodo(m.MetodoPago)
                ))
                .ToList();

            // ── Layout vertical: donut arriba, leyenda abajo ──────────────
            int n = segmentos.Count;
            const int alturaItemLey = 20;
            const int margenLey = 6;
            int zonaLeyenda = n * alturaItemLey + margenLey;

            int zonaDonut = rect.Height - zonaLeyenda;
            int donutSize = Math.Min(rect.Width - 24, zonaDonut - 10);
            donutSize = Math.Max(donutSize, 60);
            int donutX = (rect.Width - donutSize) / 2;
            int donutY = (zonaDonut - donutSize) / 2;

            float grosorAnillo = donutSize * 0.175f;
            float radio = (donutSize - grosorAnillo) / 2f;
            var centro = new PointF(donutX + donutSize / 2f, donutY + donutSize / 2f);

            // ── Track de fondo ────────────────────────────────────────────
            using var penTrack = new Pen(ColorGridLine, grosorAnillo);
            g.DrawEllipse(penTrack,
                centro.X - radio, centro.Y - radio,
                radio * 2, radio * 2);

            // ── Segmentos ─────────────────────────────────────────────────
            float anguloActual = -90f;
            const float gapGrados = 1.5f;

            foreach (var seg in segmentos) {
                float porcion = (float) seg.Cantidad / total;
                float barrido = porcion * 360f - gapGrados;
                if (barrido <= 0f) { anguloActual += porcion * 360f; continue; }

                using var penSeg = new Pen(seg.Color, grosorAnillo) {
                    StartCap = LineCap.Flat,
                    EndCap = LineCap.Flat
                };

                g.DrawArc(penSeg,
                    centro.X - radio, centro.Y - radio,
                    radio * 2, radio * 2,
                    anguloActual + gapGrados / 2f, barrido);

                anguloActual += porcion * 360f;
            }

            // ── Texto central: porcentaje del método principal ────────────
            var principal = segmentos[0];
            float pctPrincipal = (float) principal.Cantidad / total * 100f;

            using var fuentePct = new Font("Segoe UI", donutSize * 0.155f, FontStyle.Bold);
            using var fuenteSub = new Font("Segoe UI", donutSize * 0.085f);
            using var brushCentro = new SolidBrush(Color.FromArgb(64, 64, 64));
            using var brushCentroSub = new SolidBrush(ColorEjeTexto);

            string txtPct = $"{pctPrincipal:F0}%";
            string txtSub = AcortarTexto(principal.Metodo, 9);
            var tamPct = g.MeasureString(txtPct, fuentePct);
            var tamSub = g.MeasureString(txtSub, fuenteSub);
            float bloqueH = tamPct.Height + 2f + tamSub.Height;

            g.DrawString(txtPct, fuentePct, brushCentro,
                centro.X - tamPct.Width / 2f,
                centro.Y - bloqueH / 2f);
            g.DrawString(txtSub, fuenteSub, brushCentroSub,
                centro.X - tamSub.Width / 2f,
                centro.Y - bloqueH / 2f + tamPct.Height + 2f);

            // ── Leyenda abajo — dos columnas ──────────────────────────────
            using var fuenteLey = new Font("Segoe UI", 8.5f);
            using var fuenteVal = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            using var brushTexto = new SolidBrush(ColorEjeTexto);
            using var brushVal = new SolidBrush(Color.FromArgb(64, 64, 64));

            int leyX = 10;
            int leyAncho = rect.Width - leyX - 8;
            int columnas = n > 2 ? 2 : 1;
            float colW = leyAncho / (float) columnas;
            float leyYBase = rect.Height - zonaLeyenda + margenLey;
            const int dotSize = 8;

            for (int i = 0; i < n; i++) {
                var seg = segmentos[i];
                int col = i % columnas;
                int fila = i / columnas;
                float itemX = leyX + col * colW;
                float itemY = leyYBase + fila * alturaItemLey + (alturaItemLey - dotSize) / 2f;
                float yText = leyYBase + fila * alturaItemLey + (alturaItemLey - fuenteLey.Height) / 2f;

                using var brushDot = new SolidBrush(seg.Color);
                g.FillEllipse(brushDot, itemX, itemY, dotSize, dotSize);

                g.DrawString(AcortarTexto(seg.Metodo, 10), fuenteLey, brushTexto,
                    itemX + dotSize + 5f, yText);

                float pct = (float) seg.Cantidad / total * 100f;
                string valStr = $"{seg.Cantidad} ({pct:F0}%)";
                var tamVal = g.MeasureString(valStr, fuenteVal);
                g.DrawString(valStr, fuenteVal, brushVal,
                    itemX + colW - tamVal.Width - 4f, yText);
            }
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir("MostrarVistaEstadisticasVentas", OnMostrarVistaEstadisticasVentas);

            Vista.Cerrar();
        }

        #region HELPERS

        private static Color ResolverColorMetodo(string metodo) {
            if (MapaColoresPago.TryGetValue(metodo?.Trim() ?? "", out var c)) return c;
            return ColorOtro;
        }

        private static void DibujarEstadoVacio(Graphics g, Rectangle rect, string mensaje) {
            using var f = new Font("Segoe UI", 9.5f);
            using var b = new SolidBrush(ColorEjeTexto);
            var tam = g.MeasureString(mensaje, f);
            g.DrawString(mensaje, f, b,
                (rect.Width - tam.Width) / 2f,
                (rect.Height - tam.Height) / 2f);
        }

        private static decimal RedondeadoArriba(decimal valor) {
            if (valor <= 0) return 10m;
            double v = (double) valor;
            double mag = Math.Pow(10, Math.Floor(Math.Log10(v)));
            return (decimal) (Math.Ceiling(v / mag) * mag);
        }

        private static string FormatearNumeroCorto(float valor) {
            if (valor >= 1_000_000) return $"{valor / 1_000_000:F1}M";
            if (valor >= 1_000) return $"{valor / 1_000:F1}k";
            return $"{valor:F0}";
        }

        private static string AcortarTexto(string texto, int max) {
            if (string.IsNullOrEmpty(texto) || texto.Length <= max) return texto ?? "";
            return texto[..max] + "…";
        }

        #endregion
    }
}
