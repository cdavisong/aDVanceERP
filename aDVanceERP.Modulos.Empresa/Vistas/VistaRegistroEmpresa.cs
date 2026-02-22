using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Modulos.Empresa.Interfaces;
using aDVanceERP.Modulos.Empresa.Properties;

namespace aDVanceERP.Modulos.Empresa.Vistas {
    public partial class VistaRegistroEmpresa : Form, IVIstaRegistroEmpresa {
        private bool _modoEdicion = false;
        private string _rutaImagen = string.Empty;
        private TelefonoContacto _telefono = null!;
        private CorreoContacto _correo = null!;

        public VistaRegistroEmpresa() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroEmpresa);

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
                btnRegistrarActualizar.Text = value ? "Actualizar la empresa" : "Registrar empresa";
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

        public Image? Imagen {
            get => fieldImagen.BackgroundImage;
            set {
                if (value == null) {
                    fieldImagen.BackgroundImage = Resources.picture_170px;
                    return;
                }

                var imagen = new Bitmap(fieldImagen.Size.Width, fieldImagen.Size.Height);
                var imagenProducto = value.ObtenerRecorteImagen(imagen.Size);

                using (var g = Graphics.FromImage(imagen)) {
                    g.Clear(Color.White);
                    g.DrawImage(imagenProducto, 0, 0, imagen.Width, imagen.Height);
                }

                fieldImagen.BackgroundImage = imagen;
            }
        }

        public string RutaLogo { get => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "res", "imagenes", "logo", $"{Nombre}{Path.GetExtension(_rutaImagen)}"); }

        public long IdPersona { get; set; }

        public string Nombre { 
            get => fieldNombreComercial.Text;
            set => fieldNombreComercial.Text = value;
        }

        public string RazonSocial { 
            get => fieldRazonSocial.Text;
            set => fieldRazonSocial.Text = value;
        }

        public string? Rif { 
            get => fieldRifNit.Text;
            set => fieldRifNit.Text = value;
        }

        public string? Direccion { 
            get => fieldDireccion.Text;
            set => fieldDireccion.Text = value;
        }

        public TelefonoContacto Telefono {
            get {
                return new TelefonoContacto(
                    id: _telefono != null ? _telefono.Id : 0,
                    prefijoPais: fieldPrefijoInternacional.Text,
                    numeroTelefono: fieldNumeroTelefono.Text,
                    categoria: (CategoriaTelefonoContacto) fieldCategoriaTelefono.SelectedIndex,
                    idPersona: _telefono != null ? _telefono.IdPersona : 0
                    );
            }
            set {
                fieldPaises.Text = PrefijosInternacionales.ObtenerPais(value?.PrefijoPais ?? "+53");
                fieldNumeroTelefono.Text = value?.NumeroTelefono ?? string.Empty;
                fieldCategoriaTelefono.SelectedIndex = (int) (value?.Categoria ?? CategoriaTelefonoContacto.Movil);

                _telefono = value!;
            }
        }

        public CorreoContacto Email {
            get {
                return new CorreoContacto(
                    id: _correo != null ? _correo.Id : 0,
                    direccionCorreo: fieldDireccionCorreo.Text,
                    categoria: (CategoriaCorreoContacto) fieldCategoriaDireccionCorreo.SelectedIndex,
                    idPersona: _correo != null ? _correo.IdPersona : 0
                    );
            }
            set {
                fieldDireccionCorreo.Text = value?.DireccionCorreo ?? string.Empty;
                fieldCategoriaDireccionCorreo.SelectedIndex = (int) (value?.Categoria ?? CategoriaCorreoContacto.Personal);

                _correo = value!;
            }
        }

        public string? Web {
            get => fieldDireccionWeb.Text;
            set => fieldDireccionWeb.Text = value;
        }

        public DateTime FechaRegistro {
            get => fieldFechaRegistro.Value;
            set => fieldFechaRegistro.Value = value;
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            fieldImagen.Click += ObtenerLogotipoEmpresa;
            fieldPaises.SelectedIndexChanged += delegate {
                fieldPrefijoInternacional.Text = $"{PrefijosInternacionales.ObtenerPrefijo(fieldPaises.Text)}";
                fieldNumeroTelefono.IconLeft = PrefijosInternacionales.ObtenerFlag(fieldPaises.Text);
            };
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        private void ObtenerLogotipoEmpresa(object? sender, EventArgs e) {
            // Crear el directorio base si no existe en la ruta res/imagenes/logo
            var directorioBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "res", "imagenes", "logo");

            if (!Directory.Exists(directorioBase))
                Directory.CreateDirectory(directorioBase);

            if (fieldDialogoImagen.ShowDialog() == DialogResult.OK) {
                _rutaImagen = fieldDialogoImagen.FileName;

                Imagen = Image.FromFile(_rutaImagen);
            }
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
            Imagen = null;
            Nombre = string.Empty;
            RazonSocial = string.Empty;
            Rif = string.Empty;
            FechaRegistro = DateTime.Now;
            Direccion = string.Empty;
            Telefono = null!;
            Email = null!;
            Web = string.Empty;
        }

        public void Cerrar() {
            Dispose();
        }

        public void SalvarImagenEnDirectorioLocal() {
            if (string.IsNullOrEmpty(_rutaImagen) || Imagen == null)
                return;

            if (File.Exists(RutaLogo))
                File.Delete(RutaLogo);

            // Convertir la imagen original del producto a un formato compatible con el guardado (por ejemplo, JPEG o PNG)
            var formatoImagen = Path.GetExtension(_rutaImagen).ToLower() switch {
                ".jpg" or ".jpeg" => System.Drawing.Imaging.ImageFormat.Jpeg,
                ".png" => System.Drawing.Imaging.ImageFormat.Png,
                _ => System.Drawing.Imaging.ImageFormat.Png
            };

            var bitmap = Image.FromFile(_rutaImagen) as Bitmap;

            bitmap?.Save(RutaLogo, formatoImagen);
        }
    }
}
