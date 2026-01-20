using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;
using aDVanceERP.Modulos.RecursosHumanos.Properties;

using Guna.UI2.WinForms;

using static Guna.UI2.Native.WinApi;

namespace aDVanceERP.Modulos.RecursosHumanos.Vistas {
    public partial class VistaRegistroPersona : Form, IVIstaRegistroPersona {
        private bool _modoEdicion = false;
        private List<Guna2TextBox> _telefonos = new List<Guna2TextBox>();
        private List<Guna2TextBox> _direccionesCorreo = new List<Guna2TextBox>();

        public VistaRegistroPersona() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroPersona);

            Inicializar();
            InicializarPaisesPrefijos();
        }

        private void InicializarPaisesPrefijos() {
            fieldPaises.Items.Clear();
            fieldPaises.Items.AddRange(PrefijosInternacionales.ObtenerPaises());
            fieldPaises.StartIndex = 53;
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrarActualizar.Text = value ? "Actualizar el almacén" : "Registrar el almacén";
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

        public string NombreCompleto {
            get => fieldNombreCompleto.Text;
            set => fieldNombreCompleto.Text = value;
        }

        public TipoDocumento TipoDocumento {
            get => (TipoDocumento) fieldTipoDocumento.SelectedIndex;
            set => fieldTipoDocumento.SelectedIndex = (int) value;
        }

        public string? DireccionPrincipal {
            get => fieldDireccionPrincipal.Text;
            set => fieldDireccionPrincipal.Text = value;
        }

        public string NumeroDocumento {
            get => fieldNumeroDocumento.Text;
            set => fieldNumeroDocumento.Text = value;
        }

        public DateTime FechaRegistro {
            get => fieldFechaRegistro.Value;
            set => fieldFechaRegistro.Value = value;
        }

        public List<TelefonoContacto> Telefonos {
            get => [.. _telefonos.Select(t => t.Tag).Cast<TelefonoContacto>()];
        }

        public List<CorreoContacto> DireccionesCorreo {
            get => [.. _direccionesCorreo.Select(c => c.Tag).Cast<CorreoContacto>()];
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            fieldPaises.SelectedIndexChanged += delegate {
                fieldPrefijoInternacional.Text = $"{PrefijosInternacionales.ObtenerPrefijo(fieldPaises.Text)}";
                fieldNumeroTelefono.IconLeft = PrefijosInternacionales.ObtenerFlag(fieldPaises.Text);
            };
            fieldNumeroTelefono.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                AgregarTelefono(0, ((CategoriaTelefonoContacto) fieldCategoriaTelefono.SelectedIndex).ToString(), fieldPrefijoInternacional.Text, fieldNumeroTelefono.Text);
                fieldNumeroTelefono.Focus();

                args.SuppressKeyPress = true;
            };
            fieldDireccionCorreo.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                AgregarDireccionCorreo(0, ((CategoriaCorreoContacto) fieldCategoriaDireccionCorreo.SelectedIndex).ToString(), fieldDireccionCorreo.Text);
                fieldDireccionCorreo.Focus();

                args.SuppressKeyPress = true;
            };
            btnAgregarTelefono.Click += delegate {
                AgregarTelefono(0, ((CategoriaTelefonoContacto) fieldCategoriaTelefono.SelectedIndex).ToString(), fieldPrefijoInternacional.Text, fieldNumeroTelefono.Text);
            };
            btnAgregarDireccionCorreo.Click += delegate {
                AgregarDireccionCorreo(0, ((CategoriaCorreoContacto) fieldCategoriaDireccionCorreo.SelectedIndex).ToString(), fieldDireccionCorreo.Text);
            };
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        public void AgregarTelefono(long id, string categoria, string prefijo, string numero, long idPersona = 0) {
            var telefono = new TelefonoContacto(id, prefijo, numero, Enum.TryParse<CategoriaTelefonoContacto>(categoria, out var ct) ? ct : CategoriaTelefonoContacto.Otro, idPersona);
            var componente = new Guna2TextBox() {
                BorderColor = Color.Gainsboro,
                BorderThickness = 0,
                Cursor = Cursors.IBeam,
                DefaultText = "",
                Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.Black,
                IconLeft = Resources.phone_fill_20px,
                IconLeftOffset = new Point(10, 0),
                IconLeftSize = new Size(20, 20),
                IconRight = Resources.deleteF_20px,
                IconRightOffset = new Point(6, 0),
                IconRightSize = new Size(20, 20),
                Margin = new Padding(5),
                Name = $"field{_telefonos.Count}",
                Padding = new Padding(5),
                PasswordChar = '\0',
                PlaceholderForeColor = Color.DimGray,
                SelectedText = "",
                TextOffset = new Point(5, 0)
            };

            var texto = $"{fieldCategoriaTelefono.Items[(int) telefono.Categoria]}: {prefijo} {numero}";
            var dimensionesTexto = TextRenderer.MeasureText(texto, componente.Font);

            componente.Size = new Size(dimensionesTexto.Width + 81, 35);
            componente.Tag = telefono;
            componente.Text = texto;
            componente.IconRightClick += delegate (object? sender, EventArgs e) {
                var comp = sender as Guna2TextBox;

                if (comp != null) {
                    // Restablecer datos (Edición)
                    var telefono = comp.Tag as TelefonoContacto;

                    fieldCategoriaTelefono.SelectedIndex = (int) (telefono?.Categoria ?? CategoriaTelefonoContacto.Movil);
                    fieldPaises.Text = PrefijosInternacionales.ObtenerPais(telefono?.PrefijoPais ?? "+53");
                    fieldNumeroTelefono.Text = telefono?.NumeroTelefono ?? string.Empty;

                    _telefonos.Remove(comp);
                    layoutListaTelefonos.Controls.Remove(comp);
                    fieldNumeroTelefono.Focus();
                }
            };

            _telefonos.Add(componente);
            layoutListaTelefonos.Controls.Add(componente);

            // Limpiar campos
            fieldCategoriaTelefono.SelectedIndex = 0;
            fieldPaises.SelectedIndex = 53;
            fieldNumeroTelefono.Text = string.Empty;
        }

        public void AgregarDireccionCorreo(long id, string categoria, string direccionCorreo, long idPersona = 0) {
            var correo = new CorreoContacto(id, direccionCorreo, Enum.TryParse<CategoriaCorreoContacto>(categoria, out var cc) ? cc : CategoriaCorreoContacto.Otro, idPersona);
            var componente = new Guna2TextBox() {
                BorderColor = Color.Gainsboro,
                BorderThickness = 0,
                Cursor = Cursors.IBeam,
                DefaultText = "",
                Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.Black,
                IconLeft = Resources.mail_fill_20px,
                IconLeftOffset = new Point(10, 0),
                IconLeftSize = new Size(20, 20),
                IconRight = Resources.deleteF_20px,
                IconRightOffset = new Point(6, 0),
                IconRightSize = new Size(20, 20),
                Margin = new Padding(5),
                Name = $"field{_direccionesCorreo.Count}",
                Padding = new Padding(5),
                PasswordChar = '\0',
                PlaceholderForeColor = Color.DimGray,
                SelectedText = "",
                TextOffset = new Point(5, 0)
            };

            var texto = $"{fieldCategoriaDireccionCorreo.Items[(int) correo.Categoria]}: {direccionCorreo}";
            var dimensionesTexto = TextRenderer.MeasureText(texto, componente.Font);

            componente.Size = new Size(dimensionesTexto.Width + 81, 35);
            componente.Tag = correo;
            componente.Text = texto;
            componente.IconRightClick += delegate (object? sender, EventArgs e) {
                var comp = sender as Guna2TextBox;

                if (comp != null) {
                    // Restablecer datos (Edición)
                    var direccionCorreo = comp.Tag as CorreoContacto;

                    fieldCategoriaDireccionCorreo.SelectedIndex = (int) (direccionCorreo?.Categoria ?? CategoriaCorreoContacto.Personal);
                    fieldDireccionCorreo.Text = direccionCorreo?.DireccionCorreo ?? string.Empty;

                    _direccionesCorreo.Remove(comp);
                    layoutListaDireccionesCorreo.Controls.Remove(comp);
                    fieldDireccionCorreo.Focus();
                }
            };

            _direccionesCorreo.Add(componente);
            layoutListaDireccionesCorreo.Controls.Add(componente);

            // Limpiar campos
            fieldCategoriaDireccionCorreo.SelectedIndex = 0;
            fieldDireccionCorreo.Text = string.Empty;
        }

        public void Mostrar() {
            FechaRegistro = DateTime.Now;

            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            NombreCompleto = string.Empty;
            TipoDocumento = TipoDocumento.CI;
            NumeroDocumento = string.Empty;
            DireccionPrincipal = string.Empty;
            FechaRegistro = DateTime.Now;
            fieldCategoriaTelefono.SelectedIndex = 0;
            fieldPaises.SelectedIndex = 53;
            fieldNumeroTelefono.Text = string.Empty;
            fieldCategoriaDireccionCorreo.SelectedIndex = 0;
            fieldDireccionCorreo.Text = string.Empty;

            // Borrar telefonos
            _telefonos.Clear();
            layoutListaTelefonos.Controls.Clear();

            // Borrar correos
            _direccionesCorreo.Clear();
            layoutListaDireccionesCorreo.Controls.Clear();
        }

        public void Cerrar() {
            Dispose();
        }
    }
}
