using System.Drawing.Drawing2D;

namespace aDVanceERP.Core.Infraestructura.Temas {
    public static class FondosAplicacion {
        // Colores base de la identidad aDVance
        private static readonly Color FondoBase = Color.White; // blanco cálido
        private static readonly Color Melocoton = Color.FromArgb(255, 218, 185);
        private static readonly Color AzulClaro = Color.FromArgb(173, 216, 230);
        private static readonly Color RojoMarca = Color.FromArgb(178, 34, 34);
        private static readonly Color GrisLinea = Color.Gainsboro;

        // Método auxiliar para escalar el canal alfa
        private static int EscalarAlpha(Color color, float opacidad) {
            return ((int) (color.A * opacidad)).Clamp(0, 255);
        }

        // ------------------------------------------------------------------------
        // 1. ONDAS SUAVES
        // ------------------------------------------------------------------------
        public static void DibujarOndasSuaves(Graphics g, Rectangle bounds, float opacidad = 1.0f) {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;

            // Fondo base (sin opacidad)
            using (var brushFondo = new SolidBrush(FondoBase))
                g.FillRectangle(brushFondo, bounds);

            int w = bounds.Width;
            int h = bounds.Height;

            // Primera onda
            using (var path = new GraphicsPath()) {
                path.StartFigure();
                path.AddLine(0, h * 0.5f, 0, h * 0.5f);
                path.AddBezier(0, h * 0.5f, w * 0.2f, h * 0.2f, w * 0.4f, h * 0.7f, w * 0.6f, h * 0.4f);
                path.AddBezier(w * 0.6f, h * 0.4f, w * 0.8f, h * 0.1f, w * 0.9f, h * 0.6f, w, h * 0.3f);
                path.AddLine(w, h * 0.3f, w, h);
                path.AddLine(w, h, 0, h);
                path.CloseFigure();

                int alphaMel = EscalarAlpha(Color.FromArgb(45, Melocoton), opacidad);
                int alphaAzul = EscalarAlpha(Color.FromArgb(38, AzulClaro), opacidad);
                using (var brush = new LinearGradientBrush(
                    new Point(0, 0), new Point((int) (w * 0.8), h),
                    Color.FromArgb(alphaMel, Melocoton),
                    Color.FromArgb(alphaAzul, AzulClaro))) {
                    g.FillPath(brush, path);
                }
            }

            // Segunda onda
            using (var path2 = new GraphicsPath()) {
                path2.StartFigure();
                path2.AddLine(0, h * 0.7f, 0, h * 0.7f);
                path2.AddBezier(0, h * 0.7f, w * 0.3f, h * 0.9f, w * 0.5f, h * 0.4f, w * 0.8f, h * 0.8f);
                path2.AddLine(w, h * 0.9f, w, h);
                path2.AddLine(w, h, 0, h);
                path2.CloseFigure();

                int alphaMel2 = EscalarAlpha(Color.FromArgb(35, Melocoton), opacidad);
                int alphaAzul2 = EscalarAlpha(Color.FromArgb(30, AzulClaro), opacidad);
                using (var brush = new LinearGradientBrush(
                    new Point(0, (int) (h * 0.5)), new Point(w, h),
                    Color.FromArgb(alphaMel2, Melocoton),
                    Color.FromArgb(alphaAzul2, AzulClaro))) {
                    g.FillPath(brush, path2);
                }
            }

            // Línea de acento rojo
            int alphaRojo = EscalarAlpha(Color.FromArgb(30, RojoMarca), opacidad);
            using (var pen = new Pen(Color.FromArgb(alphaRojo, RojoMarca), 1.5f)) {
                g.DrawBezier(pen, 0, h * 0.5f, w * 0.2f, h * 0.2f, w * 0.4f, h * 0.7f, w * 0.6f, h * 0.4f);
                g.DrawBezier(pen, w * 0.6f, h * 0.4f, w * 0.8f, h * 0.1f, w * 0.9f, h * 0.6f, w, h * 0.3f);
            }
        }

        // ------------------------------------------------------------------------
        // 2. PUNTOS GEOMÉTRICOS
        // ------------------------------------------------------------------------
        public static void DibujarPuntosGeometricos(Graphics g, Rectangle bounds, float opacidad = 1.0f) {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brushFondo = new SolidBrush(Color.White))
                g.FillRectangle(brushFondo, bounds);

            int w = bounds.Width;
            int h = bounds.Height;
            int cols = 12, rows = 6;
            float spacingX = w / (float) cols;
            float spacingY = h / (float) rows;

            int alphaRojo = EscalarAlpha(Color.FromArgb(15, RojoMarca), opacidad);
            using (var brush = new SolidBrush(Color.FromArgb(alphaRojo, RojoMarca))) {
                for (int i = 0; i < cols; i++) {
                    for (int j = 0; j < rows; j++) {
                        float x = i * spacingX + spacingX / 2f;
                        float y = j * spacingY + spacingY / 2f;
                        float radio = (i % 3 + 1) * 2.5f;
                        g.FillEllipse(brush, x - radio, y - radio, radio * 2, radio * 2);
                    }
                }
            }

            int alphaAzul = EscalarAlpha(Color.FromArgb(25, AzulClaro), opacidad);
            using (var brush = new SolidBrush(Color.FromArgb(alphaAzul, AzulClaro))) {
                for (int i = 0; i < cols; i += 2) {
                    for (int j = 0; j < rows; j += 2) {
                        float x = i * spacingX + 10;
                        float y = j * spacingY + 8;
                        g.FillEllipse(brush, x - 5, y - 5, 10, 10);
                    }
                }
            }
        }

        // ------------------------------------------------------------------------
        // 3. LÍNEAS DIAGONALES
        // ------------------------------------------------------------------------
        public static void DibujarLineasDiagonales(Graphics g, Rectangle bounds, float opacidad = 1.0f) {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brushFondo = new SolidBrush(Color.FromArgb(245, 245, 245)))
                g.FillRectangle(brushFondo, bounds);

            int w = bounds.Width;
            int h = bounds.Height;
            int step = 18;

            int alphaGris = EscalarAlpha(Color.FromArgb(128, GrisLinea), opacidad);
            using (var pen = new Pen(Color.FromArgb(alphaGris, GrisLinea), 1.2f)) {
                for (int i = -h; i < w + h; i += step) {
                    g.DrawLine(pen, i, 0, i + h, h);
                }
            }

            int alphaRojo = EscalarAlpha(Color.FromArgb(30, RojoMarca), opacidad);
            using (var pen = new Pen(Color.FromArgb(alphaRojo, RojoMarca), 2.5f)) {
                for (int i = -h * 2; i < w + h; i += step * 4) {
                    g.DrawLine(pen, i, 0, i + h, h);
                }
            }
        }

        // ------------------------------------------------------------------------
        // 4. POLÍGONOS ABSTRACTOS
        // ------------------------------------------------------------------------
        public static void DibujarPoligonosAbstractos(Graphics g, Rectangle bounds, float opacidad = 1.0f) {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brushFondo = new SolidBrush(Color.FromArgb(252, 249, 242)))
                g.FillRectangle(brushFondo, bounds);

            int w = bounds.Width;
            int h = bounds.Height;

            int alphaMel = EscalarAlpha(Color.FromArgb(64, Melocoton), opacidad);
            using (var brush = new SolidBrush(Color.FromArgb(alphaMel, Melocoton))) {
                PointF[] pts = { new(20, h - 20), new(120, 40), new(220, h - 10) };
                g.FillPolygon(brush, pts);
            }

            int alphaAzul = EscalarAlpha(Color.FromArgb(51, AzulClaro), opacidad);
            using (var brush = new SolidBrush(Color.FromArgb(alphaAzul, AzulClaro))) {
                PointF[] pts = { new(w - 80, 30), new(w - 30, h - 50), new(w - 180, h - 30) };
                g.FillPolygon(brush, pts);
            }

            int alphaRojo = EscalarAlpha(Color.FromArgb(20, RojoMarca), opacidad);
            using (var brush = new SolidBrush(Color.FromArgb(alphaRojo, RojoMarca))) {
                g.FillEllipse(brush, w - 115, 15, 110, 110);
            }
        }

        // ------------------------------------------------------------------------
        // 5. MALLA DE HEXÁGONOS
        // ------------------------------------------------------------------------
        public static void DibujarMallaHexagonos(Graphics g, Rectangle bounds, float opacidad = 1.0f) {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brushFondo = new SolidBrush(Color.White))
                g.FillRectangle(brushFondo, bounds);

            int w = bounds.Width;
            int h = bounds.Height;
            float side = 22f;
            float hStep = side * (float) Math.Sqrt(3);
            float vStep = side * 1.5f;

            int alphaGris = EscalarAlpha(Color.FromArgb(77, GrisLinea), opacidad);
            using (var pen = new Pen(Color.FromArgb(alphaGris, GrisLinea), 1f)) {
                for (int row = -2; row < h / vStep + 2; row++) {
                    for (int col = -2; col < w / hStep + 2; col++) {
                        float cx = col * hStep + (row % 2) * hStep / 2f;
                        float cy = row * vStep;
                        DibujarHexagono(g, pen, cx, cy, side);
                    }
                }
            }
        }

        private static void DibujarHexagono(Graphics g, Pen pen, float cx, float cy, float r) {
            PointF[] puntos = new PointF[6];
            for (int i = 0; i < 6; i++) {
                double angle = i * Math.PI / 3;
                puntos[i] = new PointF(cx + r * (float) Math.Cos(angle), cy + r * (float) Math.Sin(angle));
            }
            g.DrawPolygon(pen, puntos);
        }

        // ------------------------------------------------------------------------
        // 6. ANILLOS CONCÉNTRICOS
        // ------------------------------------------------------------------------
        public static void DibujarAnillosConcentricos(Graphics g, Rectangle bounds, float opacidad = 1.0f) {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brushFondo = new SolidBrush(Color.FromArgb(248, 248, 248)))
                g.FillRectangle(brushFondo, bounds);

            int w = bounds.Width;
            int h = bounds.Height;
            float cx = w / 2f;
            float cy = h / 2f;
            float maxR = (float) Math.Sqrt(w * w + h * h);

            int alphaGris = EscalarAlpha(Color.FromArgb(102, GrisLinea), opacidad);
            using (var pen = new Pen(Color.FromArgb(alphaGris, GrisLinea), 1.5f)) {
                for (float r = 20; r < maxR; r += 30) {
                    g.DrawEllipse(pen, cx - r, cy - r, r * 2, r * 2);
                }
            }

            int alphaRojo = EscalarAlpha(Color.FromArgb(38, RojoMarca), opacidad);
            using (var pen = new Pen(Color.FromArgb(alphaRojo, RojoMarca), 2.2f)) {
                for (float r = 50; r < maxR; r += 90) {
                    g.DrawEllipse(pen, cx - r, cy - r, r * 2, r * 2);
                }
            }
        }
    }

    // Método de extensión para clamp
    internal static class Extensions {
        public static int Clamp(this int value, int min, int max) {
            return value < min ? min : (value > max ? max : value);
        }
    }

    public enum EstiloFondoEnum {
        OndasSuaves,
        PuntosGeometricos,
        LineasDiagonales,
        PoligonosAbstractos,
        MallaHexagonos,
        AnillosConcentricos
    }
}