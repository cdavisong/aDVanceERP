using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Temas;
using aDVanceERP.Core.Modelos.BD;
using aDVanceERP.Core.Properties;
using aDVanceERP.Core.Vistas.BD.Interfaces;

using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace aDVanceERP.Core.Vistas.BD {
    public partial class VistaConfiguracionBaseDatos : Form, IVistaConfServidorMySQL {
        public VistaConfiguracionBaseDatos() {
            InitializeComponent();

            NombreVista = nameof(VistaConfiguracionBaseDatos);

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

        public string? NombreDireccionServidor {
            get => fieldDireccionServidor.Text;
            set => fieldDireccionServidor.Text = value;
        }

        public string? NombreBaseDatos {
            get => fieldNombreBd.Text;
            set => fieldNombreBd.Text = value;
        }

        public string? NombreUsuario {
            get => fieldNombreUsuario.Text;
            set => fieldNombreUsuario.Text = value;
        }

        public string? Password {
            get => fieldPassword.Text;
            set => fieldPassword.Text = value;
        }

        public bool RecordarConfiguracion { get; set; } = true;

        public event EventHandler<ConfiguracionBaseDatos>? AlmacenarConfiguracion;

        public void Inicializar() {
            // Eventos
            pbBannerTitulo.Paint += (s, e) => {
                FondosAplicacion.DibujarMallaHexagonos(e.Graphics, pbBannerTitulo.ClientRectangle); ;
                DibujarTituloConfiguracionBd(e.Graphics, pbBannerTitulo.ClientRectangle);
            };
            fieldPassword.IconRightClick += delegate {
                fieldPassword.UseSystemPasswordChar = !fieldPassword.UseSystemPasswordChar;
                fieldPassword.PasswordChar = fieldPassword.UseSystemPasswordChar ? '●' : char.MinValue;
                fieldPassword.IconRight = fieldPassword.UseSystemPasswordChar ? Resources.closed_eye_20px : Resources.eye_20px;
            };
            btnValidarConexion.Click += delegate(object? sender, EventArgs e) {
                AlmacenarConfiguracion?.Invoke(sender,
                    new ConfiguracionBaseDatos {
                        Servidor = NombreDireccionServidor ?? "localhost",
                        BaseDatos = NombreBaseDatos ?? "advanceerp",
                        Usuario = NombreUsuario ?? "admin",
                        Password = Password ?? "admin",
                        RecordarConfiguracion = RecordarConfiguracion
                    });
                Ocultar();
            };
        }

        public void Mostrar() {
            if (!ContextoBaseDatos.EsConfiguracionCargada) {
                BringToFront();
                Show();
            }
        }

        public void Restaurar() {
            NombreDireccionServidor = string.Empty;
            NombreBaseDatos = string.Empty;
            NombreUsuario = string.Empty;
            Password = string.Empty;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }

        // <summary>
        /// Dibuja el título de autenticación optimizado para un PictureBox de 450 x 160.
        /// </summary>
        private void DibujarTituloConfiguracionBd(Graphics g, Rectangle rect) {
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Fuentes escaladas para 450x160
            using var fTitulo = new Font("Segoe UI", 38f, FontStyle.Bold, GraphicsUnit.Pixel);
            using var fSubtitulo = new Font("Segoe UI", 15f, FontStyle.Bold, GraphicsUnit.Pixel);

            // Centro horizontal y punto de inicio vertical (margen superior dinámico)
            float centerX = rect.Left + rect.Width / 2f;
            float totalHeight = 0f;

            // Calcular alturas para centrar verticalmente
            SizeF szTitulo = g.MeasureString("aDVance ERP", fTitulo, 999, StringFormat.GenericTypographic);
            SizeF szSub = g.MeasureString("CONFIGURACIÓN  DE BASE DE DATOS", fSubtitulo, 999, StringFormat.GenericTypographic);

            totalHeight = szTitulo.Height + 6f + szSub.Height + 10f;
            float currentY = rect.Top + (rect.Height - totalHeight) / 2f;

            // ── "aDVance ERP" (completo, color oscuro) ───────────────────────
            float tituloX = centerX - szTitulo.Width / 2f;
            using (var brushOscuro = new SolidBrush(Color.FromArgb(51, 51, 51)))
                g.DrawString("aDVance ERP", fTitulo, brushOscuro, tituloX, currentY, StringFormat.GenericTypographic);

            currentY += szTitulo.Height + 6f;

            // ── Subtítulo: "CONFIGURACIÓN DE BASE DE DATOS" ───────────────────
            float subX = centerX - szSub.Width / 2f;
            float xAcum = subX;

            // "CONFIGURACIÓN" en rojo
            string parte1 = "CONFIGURACIÓN";
            SizeF szParte1 = g.MeasureString(parte1, fSubtitulo, 999, StringFormat.GenericTypographic);
            using (var brushRed = new SolidBrush(Color.Firebrick))
                g.DrawString(parte1, fSubtitulo, brushRed, xAcum, currentY, StringFormat.GenericTypographic);
            xAcum += szParte1.Width;

            // Resto en gris claro
            string parte2 = "  DE BASE DE DATOS";
            using (var brushGris = new SolidBrush(Color.DimGray))
                g.DrawString(parte2, fSubtitulo, brushGris, xAcum, currentY, StringFormat.GenericTypographic);
        }
    }
}