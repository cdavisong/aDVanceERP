using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Desktop.Vistas {
    public partial class VistaPrincipal : Form, IVistaPrincipal {
        public VistaPrincipal() {
            InitializeComponent();

            NombreVista = nameof(VistaPrincipal);
            BarraTitulo = new RepoVistaBase(barraTitulo);
            PanelCentral = new RepoVistaBase(panelCentral);
            BarraEstado = new RepoVistaBase(barraEstado);

            Inicializar();
        }

        public string Titulo {
            get => pbBannerTitulo.Text;
            private set => pbBannerTitulo.Text = value;
        }

        #region Barra de título

        public RepoVistaBase BarraTitulo { get; private set; }
        public FlowLayoutPanel BotonesTitulo => layoutBotones;

        #endregion

        public RepoVistaBase PanelCentral { get; private set; }
        public RepoVistaBase BarraEstado { get; private set; }

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

        public event EventHandler? VerMenuUsuario;
        public event EventHandler? VerMensajes;
        public event EventHandler? VerNotificaciones;

        public void Inicializar() {
            FormClosing += delegate {
                Cerrar();
            };

            // Eventos        
            pbBannerTitulo.Paint += (s, e) => DibujarBannerTitulo(e.Graphics, pbBannerTitulo.ClientRectangle, $"{Program.Version}-beta");
            btnMinimizar.Click += (sender, e) => Ocultar();
            btnCerrar.Click += (sender, e) => Close();
        }

        private void DibujarBannerTitulo(Graphics g, Rectangle rect, string version) {
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float cx = rect.Left + 14f;
            float cy = rect.Height / 2f;

            using var fBold16 = new Font("Segoe UI", 16f, FontStyle.Bold, GraphicsUnit.Pixel);
            using var fVer = new Font("Segoe UI", 13f, FontStyle.Regular, GraphicsUnit.Pixel);
            using var fErp = new Font("Segoe UI", 13f, FontStyle.Bold, GraphicsUnit.Pixel);

            // ── "a" ──────────────────────────────────────────────────────────────
            var sz = g.MeasureString("a", fBold16, 999, StringFormat.GenericTypographic);
            using (var b = new SolidBrush(Color.FromArgb(51, 51, 51)))
                g.DrawString("a", fBold16, b, cx, cy - sz.Height / 2f, StringFormat.GenericTypographic);
            cx += sz.Width;

            // ── "DV" ─────────────────────────────────────────────────────────────
            sz = g.MeasureString("DV", fBold16, 999, StringFormat.GenericTypographic);
            using (var b = new SolidBrush(Color.Gray))
                g.DrawString("DV", fBold16, b, cx, cy - sz.Height / 2f, StringFormat.GenericTypographic);
            cx += sz.Width;

            // ── "ance" ────────────────────────────────────────────────────────────
            sz = g.MeasureString("ance", fBold16, 999, StringFormat.GenericTypographic);
            using (var b = new SolidBrush(Color.FromArgb(51, 51, 51)))
                g.DrawString("ance", fBold16, b, cx, cy - sz.Height / 2f, StringFormat.GenericTypographic);
            cx += sz.Width + 5f;

            // ── Píldora "ERP" ─────────────────────────────────────────────────────
            var erpSz = g.MeasureString("ERP", fErp, 999, StringFormat.GenericTypographic);
            float pillW = erpSz.Width + 16f;
            float pillH = 22f;
            var pillRect = new RectangleF(cx, cy - pillH / 2f, pillW, pillH);
            using (var path = RoundedRect(pillRect, 5f))
            using (var bg = new SolidBrush(Color.Firebrick))
                g.FillPath(bg, path);

            using (var b = new SolidBrush(Color.White)) {
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("ERP", fErp, b, pillRect, sf);
            }
            cx += pillW + 8f;

            // ── Versión ───────────────────────────────────────────────────────────
            var verTxt = $"versión {version}";
            sz = g.MeasureString(verTxt, fVer, 999, StringFormat.GenericTypographic);
            using (var b = new SolidBrush(Color.LightGray))
                g.DrawString(verTxt, fVer, b, cx, cy - sz.Height / 2f, StringFormat.GenericTypographic);
        }

        public void ModificarVisibilidadBotonesBarraTitulo(bool visible) {
            BotonesTitulo.Visible = visible;
        }

        public void Mostrar() {
            BringToFront();
            Show();

            WindowState = FormWindowState.Maximized;
        }

        public void Ocultar() {
            WindowState = FormWindowState.Minimized;
        }

        public void Restaurar() {
            //...
        }

        public void Cerrar() {
            BarraTitulo?.CerrarTodos();
            PanelCentral?.CerrarTodos();
            BarraEstado?.CerrarTodos();
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

        #endregion
    }
}