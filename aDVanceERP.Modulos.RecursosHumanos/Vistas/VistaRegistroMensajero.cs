using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Vistas {
    public partial class VistaRegistroMensajero : Form, IVistaRegistroMensajero {
        private bool _modoEdicion = false;
        private TelefonoContacto _telefono = null!;

        public VistaRegistroMensajero() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroMensajero);

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
                btnRegistrarActualizar.Text = value ? "Actualizar el mensajero" : "Registrar el mensajero";
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

        public string NumeroDocumento {
            get => fieldNumeroDocumento.Text;
            set => fieldNumeroDocumento.Text = value;
        }

        public DateTime FechaRegistro {
            get => fieldFechaRegistro.Value;
            set => fieldFechaRegistro.Value = value;
        }

        public string CodigoMensajero {
            get => fieldCodigoMensajero.Text;
            set => fieldCodigoMensajero.Text = value;
        }

        public string MatriculaVehiculo {
            get => fieldMatricula.Text;
            set => fieldMatricula.Text = value;
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

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            fieldNombreCompleto.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                var persona = RepoPersona.Instancia.Buscar(FiltroBusquedaPersona.NombreCompleto, fieldNombreCompleto.Text).resultadosBusqueda.FirstOrDefault().entidadBase;

                TipoDocumento = persona.TipoDocumento;
                NumeroDocumento = persona.NumeroDocumento;
                Telefono = RepoTelefonoContacto.Instancia.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, persona.Id.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;

                fieldNombreCompleto.Focus();

                args.SuppressKeyPress = true;
            };
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
            FechaRegistro = DateTime.Now;
            CodigoMensajero = string.Empty;
            MatriculaVehiculo = string.Empty;
            Telefono = null!;
        }

        public void Cerrar() {
            Dispose();
        }

        public void CargarNombresPersonas(string[] nombresPersonas) {
            fieldNombreCompleto.AutoCompleteCustomSource.Clear();
            fieldNombreCompleto.AutoCompleteCustomSource.AddRange(nombresPersonas);
            fieldNombreCompleto.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreCompleto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
    }
}
