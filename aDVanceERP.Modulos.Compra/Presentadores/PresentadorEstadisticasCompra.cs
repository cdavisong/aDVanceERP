using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Estadisticas;
using aDVanceERP.Modulos.Compra.Interfaces;

using System.Drawing.Drawing2D;
using System.Globalization;

namespace aDVanceERP.Modulos.Compra.Presentadores {
    internal class PresentadorEstadisticasCompra : PresentadorVistaBase<IVistaEstadisticasCompra> {
        private readonly RepoEstadisticasCompra _repo = new();

        private List<GastoMensual> _gastoMensual = [];
        private List<ComprasPorEstado> _comprasPorEstado = [];

        // ── Paleta del sistema de diseño aDVance ─────────────────────────
        // Barras de gasto
        private static readonly Color ColorBarraPasada = Color.FromArgb(173, 216, 230); // lightblue  (#ADD8E6)
        private static readonly Color ColorBarraActual = Color.FromArgb(255, 218, 185); // peachpuff  (#FFDAB9)

        // Donut — colores por estado (mismo orden que el HTML de referencia)
        private static readonly Color ColorAprobada = Color.FromArgb(173, 216, 230); // lightblue
        private static readonly Color ColorParcial = Color.FromArgb(255, 218, 185); // peachpuff
        private static readonly Color ColorPendiente = Color.FromArgb(232, 149, 74); // ámbar #e8954a
        private static readonly Color ColorBorrador = Color.FromArgb(178, 34, 34); // firebrick

        // Neutros
        private static readonly Color ColorEjeTexto = Color.FromArgb(136, 136, 136);
        private static readonly Color ColorGridLine = Color.FromArgb(228, 228, 228);
        private static readonly Color ColorFondo = Color.White;

        // Mapa estado → color para el donut
        // Las claves deben coincidir con los valores de ComprasPorEstado.Estado
        // que devuelva el repositorio (ignorando mayúsculas).
        private static readonly Dictionary<EstadoCompraEnum, Color> MapaColoresEstado = new() {
            { EstadoCompraEnum.Aprobada,                ColorAprobada  },
            { EstadoCompraEnum.Recibida_Parcial,        ColorParcial   },
            { EstadoCompraEnum.Recibida_Completa,       ColorParcial   },
            { EstadoCompraEnum.Pendiente_Aprobacion,    ColorPendiente },
            { EstadoCompraEnum.Borrador,                ColorBorrador  },
            { EstadoCompraEnum.Cancelada,               ColorBorrador  },
        };

        public PresentadorEstadisticasCompra(IVistaEstadisticasCompra vista) : base(vista) {
            vista.ActualizarTodo += OnActualizarTodo;
            vista.ActualizarEvolucionGasto6Meses += OnActualizarEvolucionGasto6Meses;
            vista.ActualizarDistribucionPorEstado += OnActualizarDistribucionPorEstado;

            AgregadorEventos.Suscribir("MostrarVistaEstadisticasCompras", OnMostrarVistaEstadisticasCompras);
        }

        private void OnMostrarVistaEstadisticasCompras(string obj) {
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
                        Vista.OrdenesPendientesAprobacion = m.OrdenesPendientesAprobacion;
                        Vista.OrdenesAprobadas = m.OrdenesAprobadas;
                        Vista.OrdenesRecibidasParcial = m.OrdenesRecibidasParcial;
                        Vista.SolicitudesPendientes = m.SolicitudesPendientes;
                        Vista.GastoMesActual = m.GastoMesActual;
                        Vista.GastoMesAnterior = m.GastoMesAnterior;

                        Vista.CargarTopProveedores(m.TopProveedores);

                        _gastoMensual = m.EvolucionGasto;
                        _comprasPorEstado = m.DistribucionEstados;

                        Vista.ActualizarPorcentajeVsMesAnterior(m.GastoMesActual, m.GastoMesAnterior);
                        Vista.RenderizarGraficosGastosDistribuciones();
                    });
                });
        }

        private void OnActualizarTodo(object? sender, EventArgs e) {
            CargarDatos();
        }

        private void OnActualizarEvolucionGasto6Meses(object? sender, PaintEventArgs e) {
            var g = e.Graphics;
            var rect = e.ClipRectangle;

            if (sender is Control ctrl && (rect.Width == 0 || rect.Height == 0))
                rect = ctrl.ClientRectangle;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(ColorFondo);

            if (_gastoMensual.Count == 0) {
                DibujarEstadoVacio(g, rect, "Sin datos de gasto mensual");
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
            decimal maxVal = _gastoMensual.Max(g2 => g2.Gasto);
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

            // ── Dimensiones de barras ─────────────────────────────────────
            int n = _gastoMensual.Count;
            float colWidth = area.Width / n;
            float barWidth = colWidth * 0.60f;          // 60% del ancho de columna
            float gapLeft = colWidth * 0.20f;           // centrado

            // ── Dibujar barras ────────────────────────────────────────────
            // El último mes (índice n-1) se resalta en peachpuff; los anteriores en lightblue.
            for (int i = 0; i < n; i++) {
                var mes = _gastoMensual[i];
                bool esMesActual = (i == n - 1);

                float proporcion = (float) (mes.Gasto / techo);
                float barH = area.Height * proporcion;
                float x = area.Left + colWidth * i + gapLeft;
                float y = area.Bottom - barH;

                Color colorBarra = esMesActual ? ColorBarraActual : ColorBarraPasada;

                // Ajustar opacidad para meses pasados (efecto visual del HTML: opacity .75-.85)
                if (!esMesActual) {
                    float opacidad = 0.75f + 0.10f * ((float) i / Math.Max(1, n - 2));
                    colorBarra = Color.FromArgb((int) (255 * opacidad), colorBarra);
                }

                using var path = CrearRectanguloRedondeado(
                    new RectangleF(x, y, barWidth, barH), radio: 4f, soloArriba: true);
                using var brush = new SolidBrush(colorBarra);
                g.FillPath(brush, path);

                // ── Etiqueta mes abajo ─────────────────────────────────────
                string etiqMes = ((Mes) mes.Mes).ObtenerNombreDescripcion();
                etiqMes = char.ToUpper(etiqMes[0]) + etiqMes[1..]; // "oct" → "Oct"
                var tam = g.MeasureString(etiqMes, fuenteEje);
                float xCentro = x + barWidth / 2f;

                g.DrawString(etiqMes, fuenteEje, brushTexto,
                    xCentro - tam.Width / 2f,
                    area.Bottom + 5f);

                // ── Valor encima ───────────────────────────────────────────
                if (barH > 18f) {
                    string valStr = FormatearNumeroCorto((float) mes.Gasto);
                    var tamV = g.MeasureString(valStr, fuenteEje);
                    g.DrawString(valStr, fuenteEje, brushTexto,
                        xCentro - tamV.Width / 2f,
                        Math.Max(area.Top, y - tamV.Height - 2f));
                }
            }

            // ── Leyenda ────────────────────────────────────────────────────
            DibujarLeyendaBarras(g, rect, fuenteEje, brushTexto);
        }

        private void OnActualizarDistribucionPorEstado(object? sender, PaintEventArgs e) {
            var g = e.Graphics;
            var rect = e.ClipRectangle;

            if (sender is Control ctrl && (rect.Width == 0 || rect.Height == 0))
                rect = ctrl.ClientRectangle;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(ColorFondo);

            if (_comprasPorEstado.Count == 0) {
                DibujarEstadoVacio(g, rect, "Sin datos de distribución");
                return;
            }

            // ── Layout: donut arriba, leyenda abajo ───────────────────────
            int total = _comprasPorEstado.Sum(c => c.Cantidad);
            if (total == 0) total = 1;

            // La leyenda necesita ~20px por ítem; reservamos ese espacio abajo.
            var segmentosConteo = _comprasPorEstado.Count;
            const int alturaItemLey = 20;
            const int margenLey = 6;
            int zonaLeyenda = segmentosConteo * alturaItemLey + margenLey;

            // Donut — ocupa el ancho completo menos márgenes, acotado al espacio disponible arriba
            int zonaDonut = rect.Height - zonaLeyenda;
            int donutSize = Math.Min(rect.Width - 24, zonaDonut - 10);
            donutSize = Math.Max(donutSize, 60);
            int donutX = (rect.Width - donutSize) / 2;
            int donutY = (zonaDonut - donutSize) / 2;

            float grosorAnillo = donutSize * 0.175f;
            float radio = (donutSize - grosorAnillo) / 2f;
            var centroDonut = new PointF(donutX + donutSize / 2f, donutY + donutSize / 2f);

            // Zona de leyenda (coordenadas Y base)
            int leyX = 10;
            int leyAncho = rect.Width - leyX - 8;

            using var fuenteLey = new Font("Segoe UI", 8.5f);
            using var fuenteVal = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            using var brushTexto = new SolidBrush(ColorEjeTexto);
            using var brushOscuro = new SolidBrush(Color.FromArgb(64, 64, 64));

            // ── Círculo de fondo (track) ───────────────────────────────────
            using var penTrack = new Pen(ColorGridLine, grosorAnillo);
            g.DrawEllipse(penTrack,
                centroDonut.X - radio,
                centroDonut.Y - radio,
                radio * 2, radio * 2);

            // ── Segmentos del donut ───────────────────────────────────────
            // GDI+ dibuja arcos desde la derecha (0°) y va en sentido horario.
            // Empezamos desde arriba (-90°) para que el primer segmento salga en la parte superior.
            float anguloActual = -90f;
            float gapGrados = 1.5f;   // pequeño hueco entre segmentos para legibilidad

            // Asignar colores a cada estado
            var segmentos = _comprasPorEstado
                .OrderByDescending(c => c.Cantidad)
                .Select(c => (
                    Estado: c.Estado,
                    Cantidad: c.Cantidad,
                    Color: ResolverColorEstado(Enum.TryParse<EstadoCompraEnum>(c.Estado, out var estado) ? estado : EstadoCompraEnum.Borrador)
                ))
                .ToList();

            foreach (var seg in segmentos) {
                float porcion = (float) seg.Cantidad / total;
                float barrido = porcion * 360f - gapGrados;

                if (barrido <= 0f) {
                    anguloActual += porcion * 360f;
                    continue;
                }

                using var penSeg = new Pen(seg.Color, grosorAnillo) {
                    StartCap = LineCap.Flat,
                    EndCap = LineCap.Flat
                };

                g.DrawArc(penSeg,
                    centroDonut.X - radio,
                    centroDonut.Y - radio,
                    radio * 2, radio * 2,
                    anguloActual + gapGrados / 2f,
                    barrido);

                anguloActual += porcion * 360f;
            }

            // ── Total centrado dentro del donut ───────────────────────────
            using var fuenteTotal = new Font("Segoe UI", donutSize * 0.155f, FontStyle.Bold);
            using var fuenteSubTotal = new Font("Segoe UI", donutSize * 0.085f);
            using var brushCentro = new SolidBrush(Color.FromArgb(64, 64, 64));
            using var brushCentroSub = new SolidBrush(ColorEjeTexto);

            string textoTotal = total.ToString();
            var tamTotal = g.MeasureString(textoTotal, fuenteTotal);
            string textoSubTotal = "órdenes";
            var tamSubTotal = g.MeasureString(textoSubTotal, fuenteSubTotal);

            float bloqueH = tamTotal.Height + 2f + tamSubTotal.Height;
            g.DrawString(textoTotal, fuenteTotal, brushCentro,
                centroDonut.X - tamTotal.Width / 2f,
                centroDonut.Y - bloqueH / 2f);
            g.DrawString(textoSubTotal, fuenteSubTotal, brushCentroSub,
                centroDonut.X - tamSubTotal.Width / 2f,
                centroDonut.Y - bloqueH / 2f + tamTotal.Height + 2f);

            // ── Leyenda abajo — dos columnas si hay más de 2 estados ──────
            // Cada ítem: [dot] Nombre ············ N (XX%)
            const int dotSize = 8;
            int columnas = segmentos.Count > 2 ? 2 : 1;
            int filas = (int) Math.Ceiling(segmentos.Count / (float) columnas);
            float colW = leyAncho / columnas;
            float leyYBase = rect.Height - zonaLeyenda + margenLey;

            for (int i = 0; i < segmentos.Count; i++) {
                var seg = segmentos[i];
                int col = i % columnas;
                int fila = i / columnas;
                float itemX = leyX + col * colW;
                float itemY = leyYBase + fila * alturaItemLey + (alturaItemLey - dotSize) / 2f;

                // Dot de color
                using var brushDot = new SolidBrush(seg.Color);
                g.FillEllipse(brushDot, itemX, itemY, dotSize, dotSize);

                // Nombre del estado
                string nombre = AcortarTexto(seg.Estado, 10);
                float xNombre = itemX + dotSize + 5f;
                float yTexto = leyYBase + fila * alturaItemLey + (alturaItemLey - fuenteLey.Height) / 2f;
                g.DrawString(nombre, fuenteLey, brushTexto, xNombre, yTexto);

                // Valor alineado a la derecha de la columna
                float pct = (float) seg.Cantidad / total * 100f;
                string valStr = $"{seg.Cantidad} ({pct:F0}%)";
                var tamVal = g.MeasureString(valStr, fuenteVal);
                g.DrawString(valStr, fuenteVal, brushOscuro,
                    itemX + colW - tamVal.Width - 4f,
                    yTexto);
            }
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir("MostrarVistaEstadisticasCompras", OnMostrarVistaEstadisticasCompras);

            Vista.Cerrar();
        }

        #region HELPERS

        /// <summary>
        /// Devuelve el color del sistema de diseño para cada estado de compra.
        /// Si el estado no está mapeado, usa un gris neutro para no romper el donut.
        /// </summary>
        private static Color ResolverColorEstado(EstadoCompraEnum estado) {
            if (MapaColoresEstado.TryGetValue(estado, out var color))
                return color;

            // Fallback: gris neutro para estados desconocidos
            return Color.FromArgb(200, 200, 200);
        }

        private static void DibujarEstadoVacio(Graphics g, Rectangle rect, string mensaje) {
            using var f = new Font("Segoe UI", 9.5f);
            using var b = new SolidBrush(ColorEjeTexto);
            var tam = g.MeasureString(mensaje, f);

            g.DrawString(mensaje, f, b,
                (rect.Width - tam.Width) / 2f,
                (rect.Height - tam.Height) / 2f);
        }

        /// <summary>Leyenda de dos ítems para el gráfico de barras.</summary>
        private static void DibujarLeyendaBarras(
            Graphics g, Rectangle rect,
            Font fuente, SolidBrush brushTexto) {
            float xBase = 52f;
            float yBase = rect.Height - 16f;

            // Meses anteriores
            using var brushPas = new SolidBrush(ColorBarraPasada);
            g.FillRectangle(brushPas, xBase, yBase, 14f, 9f);
            g.DrawString("Meses anteriores", fuente, brushTexto, xBase + 18f, yBase - 1f);

            // Mes actual
            float xAct = xBase + 130f;
            using var brushAct = new SolidBrush(ColorBarraActual);
            g.FillRectangle(brushAct, xAct, yBase, 14f, 9f);
            g.DrawString("Mes actual", fuente, brushTexto, xAct + 18f, yBase - 1f);
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

        private static string AcortarTexto(string texto, int maxChars) {
            if (string.IsNullOrEmpty(texto) || texto.Length <= maxChars) return texto ?? "";
            return texto[..maxChars] + "…";
        }

        private static GraphicsPath CrearRectanguloRedondeado(
            RectangleF rect, float radio, bool soloArriba = false) {
            var path = new GraphicsPath();
            float r2 = radio * 2f;

            if (soloArriba) {
                path.AddArc(rect.Left, rect.Top, r2, r2, 180, 90);
                path.AddArc(rect.Right - r2, rect.Top, r2, r2, 270, 90);
                path.AddLine(rect.Right, rect.Top + radio, rect.Right, rect.Bottom);
                path.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom);
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
