using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Temas;
using aDVanceERP.Modulos.Seguridad.Interfaces;
using aDVanceERP.Modulos.Seguridad.Properties;

using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Security;

namespace aDVanceERP.Modulos.Seguridad.Vistas {
    public partial class VistaAutenticacionUsuario : Form, IVistaAutenticacionUsuario {
        public VistaAutenticacionUsuario() {
            InitializeComponent();

            NombreVista = nameof(VistaAutenticacionUsuario);

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

        public string NombreUsuario {
            get => fieldNombreUsuario.Text;
            set => fieldNombreUsuario.Text = value;
        }

        public SecureString Password {
            get {
                var password = new SecureString();

                foreach (var c in fieldPassword.Text)
                    password.AppendChar(c);
                password.MakeReadOnly();

                return password;
            }
        }    

        public void Inicializar() {
            // Eventos       
            pbBannerTitulo.Paint += (s, e) => {
                FondosAplicacion.DibujarMallaHexagonos(e.Graphics, pbBannerTitulo.ClientRectangle); ;
                DibujarTituloAutenticacion(e.Graphics, pbBannerTitulo.ClientRectangle, $"0.1.1.1-beta");
            };
            fieldPassword.IconRightClick += delegate {
                fieldPassword.UseSystemPasswordChar = !fieldPassword.UseSystemPasswordChar;
                fieldPassword.PasswordChar = fieldPassword.UseSystemPasswordChar ? '●' : char.MinValue;
                fieldPassword.IconRight = fieldPassword.UseSystemPasswordChar ? Resources.closed_eye_20px : Resources.eye_20px;
            };
            fieldPassword.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode == Keys.Enter) {
                    AgregadorEventos.Publicar("EventoAutenticarCuentaUsuario", string.Empty);

                    args.SuppressKeyPress = true;
                }
            };
            btnAutenticarUsuario.Click += delegate {
                AgregadorEventos.Publicar("EventoAutenticarCuentaUsuario", string.Empty);
            };
            btnRegistrarCuenta.Click += delegate (object? sender, EventArgs e) {
                AgregadorEventos.Publicar("MostrarVistaRegistroUsuario", string.Empty);
                Ocultar();
            };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            NombreUsuario = string.Empty;
            fieldPassword.Text = string.Empty;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }

        /// <summary>
        /// Dibuja el título de autenticación optimizado para un PictureBox de 450 x 160.
        /// </summary>
        private void DibujarTituloAutenticacion(Graphics g, Rectangle rect, string version) {
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Fuentes escaladas para 450x160
            using var fTitulo = new Font("Segoe UI", 38f, FontStyle.Bold, GraphicsUnit.Pixel);
            using var fSubtitulo = new Font("Segoe UI", 15f, FontStyle.Bold, GraphicsUnit.Pixel);
            using var fVersion = new Font("Segoe UI", 13f, FontStyle.Regular, GraphicsUnit.Pixel);

            // Centro horizontal y punto de inicio vertical (margen superior dinámico)
            float centerX = rect.Left + rect.Width / 2f;
            float totalHeight = 0f;

            // Calcular alturas para centrar verticalmente
            SizeF szTitulo = g.MeasureString("aDVance", fTitulo, 999, StringFormat.GenericTypographic);
            SizeF szSub = g.MeasureString("ERP · GESTIÓN INTELIGENTE", fSubtitulo, 999, StringFormat.GenericTypographic);
            SizeF szVer = g.MeasureString($"versión {version}", fVersion, 999, StringFormat.GenericTypographic);

            totalHeight = szTitulo.Height + 6f + szSub.Height + 10f + szVer.Height;
            float currentY = rect.Top + (rect.Height - totalHeight) / 2f;

            // ── "aDVance" (completo, color oscuro) ───────────────────────
            float tituloX = centerX - szTitulo.Width / 2f;
            using (var brushOscuro = new SolidBrush(Color.FromArgb(51, 51, 51)))
                g.DrawString("aDVance", fTitulo, brushOscuro, tituloX, currentY, StringFormat.GenericTypographic);

            currentY += szTitulo.Height + 6f;

            // ── Subtítulo: "ERP · GESTIÓN INTELIGENTE" ───────────────────
            float subX = centerX - szSub.Width / 2f;
            float xAcum = subX;

            // "ERP" en rojo
            string parte1 = "ERP";
            SizeF szParte1 = g.MeasureString(parte1, fSubtitulo, 999, StringFormat.GenericTypographic);
            using (var brushRed = new SolidBrush(Color.Firebrick))
                g.DrawString(parte1, fSubtitulo, brushRed, xAcum, currentY, StringFormat.GenericTypographic);
            xAcum += szParte1.Width;

            // Resto en gris claro
            string parte2 = " · GESTIÓN INTELIGENTE";
            using (var brushGris = new SolidBrush(Color.DimGray))
                g.DrawString(parte2, fSubtitulo, brushGris, xAcum, currentY, StringFormat.GenericTypographic);

            currentY += szSub.Height + 10f;

            // ── Versión ──────────────────────────────────────────────────
            float verX = centerX - szVer.Width / 2f;
            using (var brushVer = new SolidBrush(Color.Silver))
                g.DrawString($"versión {version}", fVersion, brushVer, verX, currentY, StringFormat.GenericTypographic);
        }
    }
}