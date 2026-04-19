using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Temas;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

using System.Drawing.Drawing2D;

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

            panelCentral.Resize += (s, e) => { panelCentral.Refresh(); };
            panelCentral.Paint += (s, e) => {
                FondosAplicacion.DibujarOndasSuaves(e.Graphics, panelCentral.ClientRectangle, 1.0f);
                DibujarPortadaInicio(e.Graphics, panelCentral.ClientRectangle, _version, _nombreUsuario);
            };
            //pbPortada.Paint += (s, e) => DibujarPortadaInicio(e.Graphics, pbPortada.ClientRectangle, _version, _nombreUsuario);
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
            float contenidoAlturaTotal = 0f;

            // ------------------------------------------------------------------
            // 1. Calcular altura total del contenido para centrado vertical
            // ------------------------------------------------------------------
            using (var fLogo = new Font("Segoe UI", 32f, FontStyle.Bold, GraphicsUnit.Pixel))
            using (var fBienvenida = new Font("Segoe UI", 32f, FontStyle.Regular, GraphicsUnit.Pixel))
            using (var fBienvenidaBold = new Font("Segoe UI", 32f, FontStyle.Bold, GraphicsUnit.Pixel))
            using (var fDesc = new Font("Segoe UI", 18f, FontStyle.Regular, GraphicsUnit.Pixel))
            using (var fCita = new Font("Segoe UI", 14f, FontStyle.Italic, GraphicsUnit.Pixel)) {
                var sf = StringFormat.GenericTypographic;

                float logoAltura = g.MeasureString("aDVance", fLogo, 999, sf).Height;
                float bienvenidaAltura = g.MeasureString("Hola", fBienvenida, 999, sf).Height;
                float descAltura = g.MeasureString("aDVance ERP", fDesc, 999, sf).Height;
                float extraAltura = g.MeasureString("Optimiza procesos", fDesc, 999, sf).Height;
                float citaAltura = g.MeasureString("“La excelencia”", fCita, 999, sf).Height;

                contenidoAlturaTotal = logoAltura + 12f +
                                       bienvenidaAltura + 8f +
                                       descAltura + 8f +
                                       extraAltura + 24f +
                                       citaAltura;

                float y = rect.Top + (rect.Height - contenidoAlturaTotal) / 2f;
                if (y < 20f) y = 20f;

                // ------------------------------------------------------------------
                // 2. Logo centrado
                // ------------------------------------------------------------------
                using var fErp = new Font("Segoe UI", 22f, FontStyle.Bold, GraphicsUnit.Pixel);
                using var fVer = new Font("Segoe UI", 13f, FontStyle.Regular, GraphicsUnit.Pixel);

                float aW = g.MeasureString("a", fLogo, 999, sf).Width;
                float dvW = g.MeasureString("DV", fLogo, 999, sf).Width;
                float anceW = g.MeasureString("ance", fLogo, 999, sf).Width;
                var erpSz = g.MeasureString("ERP", fErp, 999, sf);
                float pillW = erpSz.Width + 20f;
                float pillH = 30f;
                float verW = g.MeasureString(version, fVer, 999, sf).Width + 10f;
                float totalW = aW + dvW + anceW + 6f + pillW + 10f + verW;

                float lx = cx - totalW / 2f;
                float logoBaseline = y + g.MeasureString("a", fLogo, 999, sf).Height;

                using (var dark = new SolidBrush(Color.FromArgb(51, 51, 51)))
                using (var gray = new SolidBrush(Color.Gray)) {
                    g.DrawString("a", fLogo, dark, lx, y, sf); lx += aW;
                    g.DrawString("DV", fLogo, gray, lx, y, sf); lx += dvW;
                    g.DrawString("ance", fLogo, dark, lx, y, sf); lx += anceW + 6f;
                }

                var pillRect = new RectangleF(lx, logoBaseline - pillH, pillW, pillH);
                using (var path = RoundedRect(pillRect, 6f))
                using (var bg = new SolidBrush(Color.Firebrick))
                    g.FillPath(bg, path);
                using (var b = new SolidBrush(Color.White)) {
                    var sfCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString("ERP", fErp, b, pillRect, sfCenter);
                }

                lx += pillW + 10f;
                using (var bVer = new SolidBrush(Color.Silver))
                    g.DrawString(version, fVer, bVer, lx, logoBaseline - pillH + (pillH - g.MeasureString(version, fVer).Height) / 2f, sf);

                y = logoBaseline + 20f;

                // ------------------------------------------------------------------
                // 3. Bienvenida personalizada
                // ------------------------------------------------------------------
                float espacioNombre = g.MeasureString("_", fBienvenida).Width * 0.5f;
                var partesBienvenida = new List<(string, Font, Color)>                {
                    ("Hola ", fBienvenida, Color.FromArgb(85, 85, 85)),
                    (nombreUsuario, fBienvenidaBold, Color.Firebrick),
                    (", ¡", fBienvenida, Color.FromArgb(85, 85, 85)),
                    ("Bienvenido", fBienvenida, Color.Firebrick),
                    (" a la transformación digital de tu negocio!", fBienvenida, Color.FromArgb(85, 85, 85))
                };

                DibujarLineaMixtaConEspaciado(g, cx, y, partesBienvenida.ToArray(), espacioNombre);

                y += g.MeasureString("Hola", fBienvenida).Height + 12f;

                // Línea descriptiva
                DibujarLineaMixta(g, cx, y, new[] {
                    ("aDVance ERP: La solución integral que", fDesc, Color.FromArgb(119, 119, 119)),
                    (" impulsa", fDesc, Color.Firebrick),
                    (" tu gestión empresarial.", fDesc, Color.FromArgb(119, 119, 119)),
                });

                y += g.MeasureString("aDVance ERP", fDesc).Height + 12f;

                // Texto adicional
                using var bMuted = new SolidBrush(Color.FromArgb(119, 119, 119));
                var sfCenter2 = new StringFormat { Alignment = StringAlignment.Center };
                g.DrawString("Optimiza procesos, maximiza eficiencia y eleva el rendimiento de tu negocio.",
                             fDesc, bMuted, new RectangleF(0, y, rect.Width, 40), sfCenter2);

                y += 80f; // espacio generoso antes de la cita

                // ------------------------------------------------------------------
                // 4. Cita inspiradora final
                // ------------------------------------------------------------------
                using var bCita = new SolidBrush(Color.FromArgb(140, 140, 140));
                g.DrawString("“La excelencia no es un acto, es un hábito.”", fCita, bCita,
                             new RectangleF(0, y, rect.Width, 30), sfCenter2);
            }
        }

        public void ActualizarPortadaInicio(string version, string nombreUsuario) {
            _version = version;
            _nombreUsuario = nombreUsuario;

            panelCentral.Refresh();
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

        private void DibujarLineaMixta(Graphics g, float centerX, float y, (string text, Font font, Color color)[] partes) {
            var sf = StringFormat.GenericTypographic;
            float totalWidth = 0;
            foreach (var p in partes)
                totalWidth += g.MeasureString(p.text, p.font, 999, sf).Width;

            float x = centerX - totalWidth / 2f;
            foreach (var p in partes) {
                using var brush = new SolidBrush(p.color);
                g.DrawString(p.text, p.font, brush, x, y, sf);
                x += g.MeasureString(p.text, p.font, 999, sf).Width;
            }
        }

        private void DibujarLineaMixtaConEspaciado(Graphics g, float centerX, float y,
            (string text, Font font, Color color)[] partes, float espacioExtra = 0f) {
            var sf = StringFormat.GenericTypographic;
            float totalWidth = 0;

            foreach (var p in partes)
                totalWidth += g.MeasureString(p.text, p.font, 999, sf).Width;

            if (partes.Length > 1)
                totalWidth += espacioExtra;

            float x = centerX - totalWidth / 2f;

            for (int i = 0; i < partes.Length; i++) {
                using var brush = new SolidBrush(partes[i].color);

                g.DrawString(partes[i].text, partes[i].font, brush, x, y, sf);
                x += g.MeasureString(partes[i].text, partes[i].font, 999, sf).Width;

                if (i == 0 && partes.Length > 1)
                    x += espacioExtra;
            }
        }

        private GraphicsPath RoundedRect(RectangleF bounds, float radius) {
            float diameter = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        #endregion
    }
}