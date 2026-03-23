using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Desktop.Vistas {
    public partial class VistaContenedorModulos : Form, IVistaContenedorModulos {
        private string _version;
        private string _nombreUsuario;

        public VistaContenedorModulos() {
            InitializeComponent();

            NombreVista = nameof(VistaContenedorModulos);
            PanelCentral = new RepoVistaBase(panelCentral);

            Inicializar();
        }

        public string NombreVista {
            get => Name;
            private set => Name = value;
        }

        public bool Habilitada {
            get => Enabled;
            set => Enabled = value;
        }

        public Point Coordenadas {
            get => Location;
            set => Location = value;
        }

        public Size Dimensiones {
            get => Size;
            set => Size = value;
        }

        public FlowLayoutPanel PanelMenuLateral => layoutModulos;

        public RepoVistaBase PanelCentral { get; private set; }

        public Label NombreModulo => fieldNombreModulo;

        public void Inicializar() {
            // Estado inicial
            btnInicio.PerformClick();

            // Eventos
            AgregadorEventos.Suscribir("MostrarInicio", s => btnInicio.PerformClick());

            pbPortada.Paint += (s, e) => DibujarPortadaInicio(e.Graphics, pbPortada.ClientRectangle, _version, _nombreUsuario);
            btnInicio.Click += delegate { PanelCentral.OcultarTodos(); };
            btnInicio.Click += (s, e) => {
                AgregadorEventos.Publicar("EventoCambioModulo", string.Empty);
                AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
            };
            btnInicio.MouseEnter += delegate {
                NombreModulo.Text = "Inicio";
                NombreModulo.Location = new Point(5, btnInicio.Top + 22);
                NombreModulo.BringToFront();
                NombreModulo.Show();
            };
            btnInicio.MouseLeave += delegate {
                NombreModulo.Hide();
            };
            btnGestorModulos.Click += (s, e) => {
                AgregadorEventos.Publicar("EventoCambioModulo", string.Empty);
                AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
                AgregadorEventos.Publicar("MostrarVistaContenedorExtensiones", string.Empty);
            };

            AgregadorEventos.Suscribir("EventoCambioModulo", OnEventoCambioModulo);
        }

        public void DibujarPortadaInicio(Graphics g, Rectangle rect, string version, string nombreUsuario) {
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float cx = rect.Width / 2f;
            float y = 40f;

            // ══════════════════════════════════════════════════════════════
            // 1. Logo centrado
            // ══════════════════════════════════════════════════════════════
            using var fLogo = new Font("Segoe UI", 26f, FontStyle.Bold, GraphicsUnit.Pixel);
            using var fErp = new Font("Segoe UI", 19f, FontStyle.Bold, GraphicsUnit.Pixel);
            using var fVer = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);

            // Medir segmentos del logo para centrado
            var sf = StringFormat.GenericTypographic;
            float aW = g.MeasureString("a", fLogo, 999, sf).Width;
            float dvW = g.MeasureString("DV", fLogo, 999, sf).Width;
            float anceW = g.MeasureString("ance", fLogo, 999, sf).Width;
            var erpSz = g.MeasureString("ERP", fErp, 999, sf);
            float pillW = erpSz.Width + 18f;
            float pillH = 26f;
            float verW = g.MeasureString(version, fVer, 999, sf).Width + 8f;
            float totalW = aW + dvW + anceW + 6f + pillW + 8f + verW;

            float lx = cx - totalW / 2f;
            float logoBaseline = y + 24f; // baseline de fLogo

            using (var dark = new SolidBrush(Color.FromArgb(51, 51, 51)))
            using (var gray = new SolidBrush(Color.Gray)) {
                g.DrawString("a", fLogo, dark, lx, logoBaseline - g.MeasureString("a", fLogo, 999, sf).Height, sf); lx += aW;
                g.DrawString("DV", fLogo, gray, lx, logoBaseline - g.MeasureString("DV", fLogo, 999, sf).Height, sf); lx += dvW;
                g.DrawString("ance", fLogo, dark, lx, logoBaseline - g.MeasureString("ance", fLogo, 999, sf).Height, sf); lx += anceW + 6f;
            }

            // Píldora ERP
            var pillRect = new RectangleF(lx, logoBaseline - pillH, pillW, pillH);
            using (var path = RoundedRect(pillRect, 5f))
            using (var bg = new SolidBrush(Color.Firebrick))
                g.FillPath(bg, path);
            using (var b = new SolidBrush(Color.White)) {
                var sfCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("ERP", fErp, b, pillRect, sfCenter);
            }

            lx += pillW + 8f;

            // Versión
            using (var b = new SolidBrush(Color.Silver))
                g.DrawString(version, fVer, b, lx, logoBaseline - pillH + (pillH - g.MeasureString(version, fVer).Height) / 2f, sf);

            // ══════════════════════════════════════════════════════════════
            // 2. Líneas de texto multi-color (helper inline)
            // ══════════════════════════════════════════════════════════════
            y = logoBaseline + 36f;

            using var f26 = new Font("Segoe UI", 26f, FontStyle.Regular, GraphicsUnit.Pixel);
            using var f26B = new Font("Segoe UI", 26f, FontStyle.Bold, GraphicsUnit.Pixel);
            using var f16 = new Font("Segoe UI", 16f, FontStyle.Regular, GraphicsUnit.Pixel);
            using var f18B = new Font("Segoe UI", 18f, FontStyle.Bold, GraphicsUnit.Pixel);

            var clrDark = Color.FromArgb(85, 85, 85);
            var clrMuted = Color.FromArgb(119, 119, 119);
            var clrFire = Color.Firebrick;
            var clrTitle = Color.FromArgb(51, 51, 51);

            // "Hola [usuario], ¡Bienvenido a la transformación digital de tu empresa!"
            DibujarLineaMixta(g, cx, y, new[] {
                (text: "Hola ",                                                   font: f26,  color: clrDark),
                (text: nombreUsuario,                                              font: f26B, color: clrFire),
                (text: ", ¡",                                                      font: f26,  color: clrDark),
                (text: "Bienvenido",                                               font: f26,  color: clrFire),
                (text: " a la transformación digital de tu empresa!",              font: f26,  color: clrDark),
            });

            y += 38f;
            // "aDVance ERP: La solución integral que impulsa tu gestión empresarial."
            DibujarLineaMixta(g, cx, y, new[] {
                (text: "aDVance ERP: La solución integral que ",  font: f16, color: clrMuted),
                (text: "impulsa",                                  font: f16, color: clrFire),
                (text: " tu gestión empresarial.",                 font: f16, color: clrMuted),
            });

            y += 28f;
            using var bMuted = new SolidBrush(clrMuted);
            var sfCenter2 = new StringFormat { Alignment = StringAlignment.Center };
            g.DrawString("Optimiza procesos, maximiza eficiencia y eleva el rendimiento de tu negocio.", f16, bMuted,
                         new RectangleF(0, y, rect.Width, 30), sfCenter2);

            y += 28f;
            g.DrawString("Descubre un mundo de posibilidades y alcanza el éxito que mereces.", f16, bMuted,
                         new RectangleF(0, y, rect.Width, 30), sfCenter2);

            y += 52f;
            using var bTitle = new SolidBrush(clrTitle);
            g.DrawString("Estas empresas confían en nosotros. ¡Únete a la familia!", f18B, bTitle,
                         new RectangleF(0, y, rect.Width, 30), sfCenter2);

            // Separador
            y += -12f;
            using var pen = new Pen(Color.FromArgb(228, 228, 228), 1f);
            g.DrawLine(pen, cx - 180f, y, cx + 180f, y);
        }

        public void ActualizarPortadaInicio(string version, string nombreUsuario) {
            _version = version;
            _nombreUsuario = nombreUsuario;

            pbPortada.Refresh();
        }

        private void OnEventoCambioModulo(string obj) {
            Restaurar();
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            PanelCentral.OcultarTodos();

            btnGestorModulos.Checked = false;
            btnConfiguracionGeneral.Checked = false;
        }

        public void Ocultar() {
            btnInicio.Checked = true;
            btnGestorModulos.Checked = false;
            btnConfiguracionGeneral.Checked = false;

            Hide();
        }

        public void Cerrar() {
            AgregadorEventos.Desuscribir("EventoCambioModulo", OnEventoCambioModulo);

            PanelCentral?.CerrarTodos();
        }

        #region HELPERS

        private System.Drawing.Drawing2D.GraphicsPath RoundedRect(RectangleF r, float radius) {
            var path = new System.Drawing.Drawing2D.GraphicsPath();

            path.AddArc(r.X, r.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(r.Right - radius * 2, r.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(r.Right - radius * 2, r.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(r.X, r.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void DibujarLineaMixta(Graphics g, float cx, float y, IEnumerable<(string text, Font font, Color color)> segmentos) {
            var sf = StringFormat.GenericTypographic;

            float MedirConEspacios(string texto, Font f) {
                if (string.IsNullOrEmpty(texto)) return 0f;
                float conPadding = g.MeasureString("X" + texto + "X", f, 999, sf).Width;
                float soloX = g.MeasureString("XX", f, 999, sf).Width;
                return conPadding - soloX;
            }

            float total = segmentos.Sum(s => MedirConEspacios(s.text, s.font));
            float x = cx - total / 2f;
            float maxH = segmentos.Max(s => g.MeasureString("X", s.font, 999, sf).Height);

            foreach (var (text, font, color) in segmentos) {
                float h = g.MeasureString("X", font, 999, sf).Height;
                using var brush = new SolidBrush(color);
                g.DrawString(text, font, brush, x, y + (maxH - h) / 2f, sf);
                x += MedirConEspacios(text, font);
            }
        }

        #endregion
    }
}