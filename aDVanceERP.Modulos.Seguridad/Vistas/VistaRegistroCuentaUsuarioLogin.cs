using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Temas;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Interfaces;
using aDVanceERP.Modulos.Seguridad.Properties;

using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Security;

namespace aDVanceERP.Modulos.Seguridad.Vistas {
    public partial class VistaRegistroCuentaUsuarioLogin : Form, IVistaRegistroCuentaUsuario {
        private CorreoContacto? _correoContacto;

        public VistaRegistroCuentaUsuarioLogin() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroCuentaUsuarioLogin);

            Inicializar();
        }

        public bool ModoEdicion { get; set; }

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

        public Rol? RolUsuario { get; set; }

        public CorreoContacto? CorreoContacto { 
            get {
                if (!fieldCorreoElectronico.Text.Contains('@') || !fieldCorreoElectronico.Text.Contains('.'))
                    return null;

                _correoContacto = new CorreoContacto() {
                    Id = _correoContacto?.Id ?? 0,
                    DireccionCorreo = fieldCorreoElectronico.Text,
                    Categoria = _correoContacto?.Categoria ?? CategoriaCorreoContactoEnum.Personal,
                    IdPersona = _correoContacto?.IdPersona ?? 0
                };

                return _correoContacto;
            } 
            set { 
                _correoContacto = value; 

                fieldCorreoElectronico.Text = value?.DireccionCorreo ?? string.Empty;
            }
        }

        public SecureString? Password {
            get {
                if (!fieldPassword.Text.Equals(fieldConfirmarPassword.Text))
                    return null;

                var password = new SecureString();

                foreach (var c in fieldPassword.Text)
                    password.AppendChar(c);
                password.MakeReadOnly();

                return password;
            }
        }

        public string? NombreCompleto { get; set; }

        public TipoDocumentoEnum TipoDocumento { get; set; }

        public string? NumeroDocumento { get; set; }

        public string? DireccionPrincipal { get; set; }
        
        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            // Eventos
            pbBannerTitulo.Paint += (s, e) => {
                FondosAplicacion.DibujarMallaHexagonos(e.Graphics, pbBannerTitulo.ClientRectangle);

                DibujarTituloRegistro(e.Graphics, pbBannerTitulo.ClientRectangle);
            };
            fieldConfirmarPassword.TextChanged += delegate {
                if (!string.IsNullOrEmpty(fieldConfirmarPassword.Text)) {
                    var coinciden = fieldPassword.Text.Equals(fieldConfirmarPassword.Text);

                    fieldPassword.BorderColor = coinciden
                        ? Color.FromArgb(0, 150, 136)
                        : Color.FromArgb(244, 67, 54);
                    fieldConfirmarPassword.BorderColor = coinciden
                        ? Color.FromArgb(0, 150, 136)
                        : Color.FromArgb(244, 67, 54);
                }
            };
            fieldPassword.IconRightClick += delegate {
                fieldPassword.UseSystemPasswordChar = !fieldPassword.UseSystemPasswordChar;
                fieldPassword.PasswordChar = fieldPassword.UseSystemPasswordChar
                    ? '●'
                    : char.MinValue;
                fieldPassword.IconRight = fieldPassword.UseSystemPasswordChar
                    ? Resources.closed_eye_20px
                    : Resources.eye_20px;

            };
            fieldConfirmarPassword.IconRightClick += delegate {
                fieldConfirmarPassword.UseSystemPasswordChar = !fieldConfirmarPassword.UseSystemPasswordChar;
                fieldConfirmarPassword.PasswordChar = fieldConfirmarPassword.UseSystemPasswordChar
                    ? '●'
                    : char.MinValue;
                fieldConfirmarPassword.IconRight = fieldConfirmarPassword.UseSystemPasswordChar
                    ? Resources.closed_eye_20px
                    : Resources.eye_20px;
            };
            btnRegistrarCuentaUsuario.Click += delegate (object? sender, EventArgs e) {
                RegistrarEntidad?.Invoke(sender, e);
            };
            btnIniciarSesion.Click += delegate (object? sender, EventArgs e) {
                AgregadorEventos.Publicar(new EventoMostrarVistaAutenticacionCuentaUsuario());

                Ocultar();
            };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            NombreUsuario = string.Empty;
            RolUsuario = null;
            CorreoContacto = null;
            fieldPassword.Text = string.Empty;
            fieldConfirmarPassword.Text = string.Empty;
            NombreCompleto = string.Empty;
            TipoDocumento = default;
            NumeroDocumento = string.Empty;
            DireccionPrincipal = string.Empty;
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
        private void DibujarTituloRegistro(Graphics g, Rectangle rect) {
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
            SizeF szSub = g.MeasureString("REGISTRO  DE USUARIO", fSubtitulo, 999, StringFormat.GenericTypographic);

            totalHeight = szTitulo.Height + 6f + szSub.Height + 10f;
            float currentY = rect.Top + (rect.Height - totalHeight) / 2f;

            // ── "aDVance ERP" (completo, color oscuro) ───────────────────────
            float tituloX = centerX - szTitulo.Width / 2f;
            using (var brushOscuro = new SolidBrush(Color.FromArgb(51, 51, 51)))
                g.DrawString("aDVance ERP", fTitulo, brushOscuro, tituloX, currentY, StringFormat.GenericTypographic);

            currentY += szTitulo.Height + 6f;

            // ── Subtítulo: "REGISTRO DE USUARIO" ───────────────────
            float subX = centerX - szSub.Width / 2f;
            float xAcum = subX;

            // "REGISTTRO" en rojo
            string parte1 = "REGISTRO";
            SizeF szParte1 = g.MeasureString(parte1, fSubtitulo, 999, StringFormat.GenericTypographic);
            using (var brushRed = new SolidBrush(Color.Firebrick))
                g.DrawString(parte1, fSubtitulo, brushRed, xAcum, currentY, StringFormat.GenericTypographic);
            xAcum += szParte1.Width;

            // Resto en gris claro
            string parte2 = "  DE USUARIO";
            using (var brushGris = new SolidBrush(Color.DimGray))
                g.DrawString(parte2, fSubtitulo, brushGris, xAcum, currentY, StringFormat.GenericTypographic);
        }

        public void CargarRoles(Rol[] roles) {
            throw new NotImplementedException();
        }
    }
}