using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Helpers.Comun;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;
using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaRegistroEnvio : Form, IVistaRegistroEnvio {
        private bool _modoEdicion = false;
        private Core.Modelos.Modulos.Venta.Venta? _ventaSelecionada = null!;
        private Persona? _personaClienteSeleccionada = null!;
        private Cliente? _clienteSeleccionado = null!;
        private TelefonoContacto? _telefonoContactoClienteSeleccionado = null!;
        private Persona? _personaMensajeroSeleccionado = null!;
        private Mensajero? _mensajeroSeleccionado = null!;
        private TelefonoContacto? _telefonoContactoMensajeroSeleccionado = null!;

        public VistaRegistroEnvio() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroEnvio);

            Inicializar();
            InicializarPaisesPrefijos();
        }

        private void InicializarPaisesPrefijos() {
            fieldPaisesCliente.Items.Clear();
            fieldPaisesCliente.Items.AddRange(PrefijosInternacionales.ObtenerPaises());
            fieldPaisesCliente.StartIndex = 53;

            fieldPaisesMensajero.Items.Clear();
            fieldPaisesMensajero.Items.AddRange(PrefijosInternacionales.ObtenerPaises());
            fieldPaisesMensajero.StartIndex = 53;
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrarActualizar.Text = value ? "Actualizar el envío" : "Registrar el envío";
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

        public string NumeroFacturaVenta {
            get => fieldNumeroFactura.SelectedItem?.ToString() ?? string.Empty;
            set {
                if (fieldNumeroFactura.Items.Contains(value)) {
                    fieldNumeroFactura.SelectedItem = value;
                } else {
                    fieldNumeroFactura.SelectedIndex = -1;
                }
            }
        }

        public (Persona persona, Cliente cliente, TelefonoContacto telefono) Cliente {
            get {
                var persona = new Persona() {
                    Id = _personaClienteSeleccionada?.Id ?? 0,
                    NombreCompleto = fieldNombreCompletoCliente.Text,
                    TipoDocumento = (TipoDocumento)fieldTipoDocumentoCliente.SelectedIndex,
                    NumeroDocumento = fieldNumeroDocumentoCliente.Text,
                    DireccionPrincipal = fieldDireccionPrincipalCliente.Text,
                    FechaRegistro = _personaClienteSeleccionada?.FechaRegistro ?? DateTime.Today,
                    Activo = true
                };

                var cliente = new Cliente() {
                    Id = _clienteSeleccionado?.Id ?? 0,
                    IdPersona = persona.Id,
                    CodigoCliente = CodigoHelper.GenerarEan13($"{persona.NumeroDocumento}.{persona.NombreCompleto}"),
                    LimiteCredito = decimal.TryParse(fieldLimiteCredito.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m,
                    FechaRegistro = _clienteSeleccionado?.FechaRegistro ?? DateTime.Today,
                    Activo = true
                };

                var telefonoContacto = new TelefonoContacto() {
                    Id = _telefonoContactoClienteSeleccionado?.Id ?? 0,
                    PrefijoPais = fieldPrefijoInternacionalCliente.Text,
                    NumeroTelefono = fieldNumeroTelefonoCliente.Text,
                    Categoria = (CategoriaTelefonoContacto)fieldCategoriaTelefonoCliente.SelectedIndex,
                    IdPersona = persona.Id
                };

                return (persona, cliente, telefonoContacto);
            }
        }

        public (Persona persona, Mensajero mensajero, TelefonoContacto telefono) Mensajero { 
            get {
                var persona = new Persona() {
                    Id = _mensajeroSeleccionado?.IdPersona ?? 0,
                    NombreCompleto = fieldNombreCompletoMensajero.Text,
                    TipoDocumento = (TipoDocumento)fieldTipoDocumentoMensajero.SelectedIndex,
                    NumeroDocumento = fieldNumeroDocumentoMensajero.Text,
                    DireccionPrincipal = _personaMensajeroSeleccionado?.DireccionPrincipal ?? "N/A",
                    FechaRegistro = _personaMensajeroSeleccionado?.FechaRegistro ?? DateTime.Today,
                    Activo = true
                };

                var mensajero = new Mensajero() {
                    Id = _mensajeroSeleccionado?.Id ?? 0,
                    IdPersona = persona.Id,
                    CodigoMensajero = CodigoHelper.GenerarEan13($"{persona.NumeroDocumento}.{persona.NombreCompleto}"),
                    MatriculaVehiculo = fieldMatricula.Text,
                    Activo = true
                };

                var telefonoContacto = new TelefonoContacto() {
                    Id = _telefonoContactoMensajeroSeleccionado?.Id ?? 0,
                    PrefijoPais = fieldPrefijoInternacionalMensajero.Text,
                    NumeroTelefono = fieldNumeroTelefonoMensajero.Text,
                    Categoria = (CategoriaTelefonoContacto)fieldCategoriaTelefonoMensajero.SelectedIndex,
                    IdPersona = persona.Id
                };

                return (persona, mensajero, telefonoContacto);
            }
        }

        public TipoEnvioEnum TipoEnvio {
            get => (TipoEnvioEnum)fieldTipoEnvio.SelectedIndex;
            set => fieldTipoEnvio.SelectedItem = value.ObtenerDisplayName();
        }

        public DateTime FechaAsignacion {
            get => fieldFechaAsignacion.Value;
            set => fieldFechaAsignacion.Value = value;
        }

        public string? ObservacionesEntrega {
            get => string.IsNullOrWhiteSpace(fieldObservaciones.Text) ? null : fieldObservaciones.Text;
            set => fieldObservaciones.Text = value ?? string.Empty;
        }

        public decimal MontoCobradoAlCliente {
            get => decimal.TryParse(fieldMonto.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            set => fieldMonto.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            fieldNumeroFactura.SelectedIndexChanged += ObtenerVentaDesdeNumeroFacturaSeleccionada;
            fieldTipoEnvio.SelectedIndexChanged += delegate {
                separador2.Visible = fieldTipoEnvio.SelectedIndex != (int)TipoEnvioEnum.RetiroEnLocal;
                layoutDistribucion3.Visible = fieldTipoEnvio.SelectedIndex != (int)TipoEnvioEnum.RetiroEnLocal;
                layoutTitulos5.Visible = fieldTipoEnvio.SelectedIndex != (int)TipoEnvioEnum.RetiroEnLocal;
                layoutDatos5.Visible = fieldTipoEnvio.SelectedIndex != (int)TipoEnvioEnum.RetiroEnLocal;
            };
            fieldNombreCompletoCliente.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                ObtenerDatosClienteSeleccionado();

                args.SuppressKeyPress = true;
            };
            fieldPaisesCliente.SelectedIndexChanged += delegate {
                fieldPrefijoInternacionalCliente.Text = $"{PrefijosInternacionales.ObtenerPrefijo(fieldPaisesCliente.Text)}";
                fieldNumeroTelefonoCliente.IconLeft = PrefijosInternacionales.ObtenerFlag(fieldPaisesCliente.Text);
            };
            fieldNombreCompletoMensajero.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                ObtenerDatosMensajeroSeleccionado();

                args.SuppressKeyPress = true;
            };
            fieldPaisesMensajero.SelectedIndexChanged += delegate {
                fieldPrefijoInternacionalMensajero.Text = $"{PrefijosInternacionales.ObtenerPrefijo(fieldPaisesMensajero.Text)}";
                fieldNumeroTelefonoMensajero.IconLeft = PrefijosInternacionales.ObtenerFlag(fieldPaisesMensajero.Text);
            };
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        private void ObtenerVentaDesdeNumeroFacturaSeleccionada(object? sender, EventArgs e) {
            if (_ventaSelecionada != null && fieldNumeroFactura.SelectedItem?.ToString() == _ventaSelecionada.NumeroFacturaTicket || fieldNumeroFactura.SelectedItem == null) 
                return;

            _ventaSelecionada = RepoVenta.Instancia.Buscar(FiltroBusquedaVenta.NumeroFactura, NumeroFacturaVenta).resultadosBusqueda.FirstOrDefault().entidadBase;
            _clienteSeleccionado = null;
            _personaClienteSeleccionada = null;
            _telefonoContactoClienteSeleccionado = null;

            if (_ventaSelecionada == null) {
                CentroNotificaciones.MostrarNotificacion("El número de factura o tícket seleccionado no es válido u ocurrió un error durante la selección.", TipoNotificacion.Error);
                return;
            }

            // Rellenar automáticamente los campos relacionados con la venta
            _clienteSeleccionado = RepoCliente.Instancia.Buscar(FiltroBusquedaCliente.Id, _ventaSelecionada.IdCliente.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;

            if (_clienteSeleccionado != null) {
                _personaClienteSeleccionada = RepoPersona.Instancia.Buscar(FiltroBusquedaPersona.Id, _clienteSeleccionado.IdPersona.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;
                _telefonoContactoClienteSeleccionado = RepoTelefonoContacto.Instancia.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, _personaClienteSeleccionada?.Id.ToString() ?? "0").resultadosBusqueda.FirstOrDefault(t => t.entidadBase.Categoria == CategoriaTelefonoContacto.Movil).entidadBase;

                ActualizarDatosCliente();
            }
        }

        private void ObtenerDatosClienteSeleccionado() {
            _personaClienteSeleccionada = RepoPersona.Instancia.Buscar(FiltroBusquedaPersona.NombreCompleto, fieldNombreCompletoCliente.Text).resultadosBusqueda.FirstOrDefault().entidadBase;
            _clienteSeleccionado = RepoCliente.Instancia.Buscar(FiltroBusquedaCliente.IdPersona, _personaClienteSeleccionada?.Id.ToString() ?? "0").resultadosBusqueda.FirstOrDefault().entidadBase;
            _telefonoContactoClienteSeleccionado = RepoTelefonoContacto.Instancia.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, _personaClienteSeleccionada?.Id.ToString() ?? "0").resultadosBusqueda.FirstOrDefault(t => t.entidadBase.Categoria == CategoriaTelefonoContacto.Movil).entidadBase;

            if (_clienteSeleccionado == null || _personaClienteSeleccionada == null) {
                CentroNotificaciones.MostrarNotificacion("El cliente seleccionado no es válido o no existe en el sistema.", TipoNotificacion.Error);
                return;
            }

            ActualizarDatosCliente();
        }

        private void ActualizarDatosCliente() {
            fieldNombreCompletoCliente.Text = _personaClienteSeleccionada?.NombreCompleto ?? string.Empty;
            fieldTipoDocumentoCliente.SelectedIndex = _personaClienteSeleccionada != null ? (int)_personaClienteSeleccionada.TipoDocumento : 0;
            fieldNumeroDocumentoCliente.Text = _personaClienteSeleccionada?.NumeroDocumento ?? string.Empty;
            fieldDireccionPrincipalCliente.Text = _personaClienteSeleccionada?.DireccionPrincipal ?? string.Empty;
            fieldLimiteCredito.Text = _clienteSeleccionado != null ? _clienteSeleccionado.LimiteCredito.ToString("N2", CultureInfo.InvariantCulture) : string.Empty;
            fieldPaisesCliente.SelectedItem = _telefonoContactoClienteSeleccionado != null ? PrefijosInternacionales.ObtenerPais(_telefonoContactoClienteSeleccionado.PrefijoPais) : "Cuba";
            fieldNumeroTelefonoCliente.Text = _telefonoContactoClienteSeleccionado?.NumeroTelefono ?? string.Empty;
        }

        private void ObtenerDatosMensajeroSeleccionado() {
            _personaMensajeroSeleccionado = RepoPersona.Instancia.Buscar(FiltroBusquedaPersona.NombreCompleto, fieldNombreCompletoMensajero.Text).resultadosBusqueda.FirstOrDefault().entidadBase;
            _mensajeroSeleccionado = RepoMensajero.Instancia.Buscar(FiltroBusquedaMensajero.IdPersona, _personaMensajeroSeleccionado?.Id.ToString() ?? "0").resultadosBusqueda.FirstOrDefault().entidadBase;
            _telefonoContactoMensajeroSeleccionado = RepoTelefonoContacto.Instancia.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, _personaMensajeroSeleccionado?.Id.ToString() ?? "0").resultadosBusqueda.FirstOrDefault(t => t.entidadBase.Categoria == CategoriaTelefonoContacto.Movil).entidadBase;

            if (_mensajeroSeleccionado == null || _personaMensajeroSeleccionado == null) {
                CentroNotificaciones.MostrarNotificacion("El mensajero seleccionado no es válido o no existe en el sistema.", TipoNotificacion.Error);
                return;
            }

            ActualizarDatosMensajero();
        }

        private void ActualizarDatosMensajero() {
            fieldNombreCompletoMensajero.Text = _personaMensajeroSeleccionado?.NombreCompleto ?? string.Empty;
            fieldTipoDocumentoMensajero.SelectedIndex = _personaMensajeroSeleccionado != null ? (int)_personaMensajeroSeleccionado.TipoDocumento : 0;
            fieldNumeroDocumentoMensajero.Text = _personaMensajeroSeleccionado?.NumeroDocumento ?? string.Empty;
            fieldMatricula.Text = _mensajeroSeleccionado?.MatriculaVehiculo ?? string.Empty;
            fieldPaisesMensajero.SelectedItem = _telefonoContactoMensajeroSeleccionado != null ? PrefijosInternacionales.ObtenerPais(_telefonoContactoMensajeroSeleccionado.PrefijoPais) : "Cuba";
            fieldNumeroTelefonoMensajero.Text = _telefonoContactoMensajeroSeleccionado?.NumeroTelefono ?? string.Empty;
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            _ventaSelecionada = null;
            _personaClienteSeleccionada = null;
            _clienteSeleccionado = null;
            _telefonoContactoClienteSeleccionado = null;
            _personaMensajeroSeleccionado = null;
            _mensajeroSeleccionado = null;
            _telefonoContactoMensajeroSeleccionado = null;

            FechaAsignacion = DateTime.Today;
            ObservacionesEntrega = string.Empty;

            fieldNumeroFactura.SelectedIndex = -1;
            fieldTipoEnvio.SelectedIndex = fieldTipoEnvio.Items.Count > 0 ? 0 : -1;
            fieldMonto.Text = string.Empty;

            fieldNombreCompletoCliente.Text = string.Empty;
            fieldTipoDocumentoCliente.SelectedIndex = 0;
            fieldNumeroDocumentoCliente.Text = string.Empty;
            fieldDireccionPrincipalCliente.Text = string.Empty;
            fieldLimiteCredito.Text = string.Empty;
            fieldPaisesCliente.SelectedIndex = 53;
            fieldNumeroTelefonoCliente.Text = string.Empty;

            fieldNombreCompletoMensajero.Text = string.Empty;
            fieldTipoDocumentoMensajero.SelectedIndex = 0;
            fieldNumeroDocumentoMensajero.Text = string.Empty;
            fieldMatricula.Text = string.Empty;
            fieldPaisesMensajero.SelectedIndex = 53;
            fieldNumeroTelefonoMensajero.Text = string.Empty;
        }

        public void Cerrar() {
            Dispose();
        }

        public void CargarFacturasVentasPendientes(string[] numerosFacturasPendientes) {
            fieldNumeroFactura.Items.Clear();
            fieldNumeroFactura.Items.AddRange(numerosFacturasPendientes);
            fieldNumeroFactura.SelectedIndex = -1;
        }

        public void CargarTiposEnvio(string[] tiposEnvio) {
            fieldTipoEnvio.Items.Clear();
            fieldTipoEnvio.Items.AddRange(tiposEnvio);
            fieldTipoEnvio.SelectedIndex = -1;
        }

        public void CargarNombresClientes(string[] nombresClientes) {
            fieldNombreCompletoCliente.AutoCompleteCustomSource.Clear();
            fieldNombreCompletoCliente.AutoCompleteCustomSource.AddRange(nombresClientes);
            fieldNombreCompletoCliente.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreCompletoCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void CargarNombresMensajeros(string[] nombresMensajeros) {
            fieldNombreCompletoMensajero.AutoCompleteCustomSource.Clear();
            fieldNombreCompletoMensajero.AutoCompleteCustomSource.AddRange(nombresMensajeros);
            fieldNombreCompletoMensajero.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreCompletoMensajero.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
    }
}
