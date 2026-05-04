using aDVanceERP.Core.Infraestructura.Helpers.Comun;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Interfaces;
using aDVanceERP.Modulos.Seguridad.Properties;

using System.Security;

namespace aDVanceERP.Modulos.Seguridad.Vistas {
    public partial class VistaRegistroCuentaUsuario : Form, IVistaRegistroCuentaUsuario {
        private bool _modoEdicion = false;
        private CorreoContacto _correoContacto = null!;

        public VistaRegistroCuentaUsuario() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroCuentaUsuario);

            Inicializar();
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrarActualizar.Text = value ? "Actualizar cuenta de usuario" : "Registrar cuenta de usuario";
                fieldPassword.Enabled = !value;
                btnGenerarPasswordSeguro.Enabled = !value;
            }
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

        public Rol? RolUsuario { 
            get => fieldRolUsuario.SelectedItem as Rol; 
            set => fieldRolUsuario.SelectedItem = value;
        }

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
                _correoContacto = value ?? null!;

                fieldCorreoElectronico.Text = value?.DireccionCorreo ?? string.Empty;
            }
        }

        public SecureString? Password {
            get {
                var password = new SecureString();

                foreach (var c in fieldPassword.Text)
                    password.AppendChar(c);
                password.MakeReadOnly();

                return password;
            }
        }

        public string? NombreCompleto {
            get => fieldNombreCompleto.Text;
            set => fieldNombreCompleto.Text = value ?? string.Empty;
        }

        public TipoDocumentoEnum TipoDocumento {
            get {
                if (fieldTipoDocumento.SelectedIndex < 0)
                    return default;
                return (TipoDocumentoEnum) fieldTipoDocumento.SelectedIndex;
            }
            set => fieldTipoDocumento.SelectedIndex = (int) value;
        }

        public string? NumeroDocumento {
            get => fieldNumeroDocumento.Text;
            set => fieldNumeroDocumento.Text = value ?? string.Empty;
        }

        public string? DireccionPrincipal {
            get => fieldDireccionPrincipal.Text;
            set => fieldDireccionPrincipal.Text = value ?? string.Empty;
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            btnGenerarPasswordSeguro.Click += delegate (object? sender, EventArgs args) {
                fieldPassword.Text = PasswordHelper.GenerarPasswordSeguro();
                fieldPassword.UseSystemPasswordChar = false;
                fieldPassword.PasswordChar = char.MinValue;
                fieldPassword.IconRight = Resources.eye_20px;
            };
            fieldPassword.TextChanged += delegate (object? sender, EventArgs args) {
                fieldBarraFuerzaPassword.Invalidate();
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
            fieldBarraFuerzaPassword.Paint += OnDibujarBarraFuerzaPassword;
            btnSalir.Click += delegate (object? sender, EventArgs args) {
                Ocultar();
            };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            NombreUsuario = string.Empty;
            RolUsuario = null;
            CorreoContacto = null;
            fieldPassword.Text = string.Empty;
            NombreCompleto = string.Empty;
            TipoDocumento = default;
            NumeroDocumento = string.Empty;
            DireccionPrincipal = string.Empty;
        }

        public void Cerrar() {
            fieldBarraFuerzaPassword.Paint -= OnDibujarBarraFuerzaPassword;

            Dispose();
        }

        public void CargarRoles(Rol[] roles) {
            fieldRolUsuario.Items.Clear();
            fieldRolUsuario.Items.AddRange(roles);
            if (fieldRolUsuario.Items.Count > 0)
                fieldRolUsuario.SelectedIndex = 0;
        }

        private void OnDibujarBarraFuerzaPassword(object? sender, PaintEventArgs e) {
            var fuerzaPassword = PasswordHelper.VerificarFuerza(fieldPassword.Text);
            var porcentajeFuerza = ((int) fuerzaPassword + 1) / 5f;
            var colorFuerza = fuerzaPassword switch {
                PasswordHelper.FuerzaPassword.MuyDebil => Color.FromArgb(244, 67, 54),
                PasswordHelper.FuerzaPassword.Debil => Color.FromArgb(255, 152, 0),
                PasswordHelper.FuerzaPassword.Media => Color.FromArgb(255, 235, 59),
                PasswordHelper.FuerzaPassword.Fuerte => Color.FromArgb(76, 175, 80),
                PasswordHelper.FuerzaPassword.MuyFuerte => Color.FromArgb(0, 150, 136),
                _ => Color.Gray
            };

            using (var brush = new SolidBrush(colorFuerza)) {
                var width = (int) (fieldBarraFuerzaPassword.Width * porcentajeFuerza);

                e.Graphics.FillRectangle(brush, 0, 0, width, fieldBarraFuerzaPassword.Height);
            }
        }
    }
}
