using System.Globalization;

using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Finanzas;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Pago.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Pago;

public partial class VistaRegistroPago : Form, IVistaRegistroPago, IVistaGestionPagos {
    private bool _modoEdicion;
    private decimal _total;
    private TasaCambio? _tasaCambio;

    public VistaRegistroPago() {
        InitializeComponent();

        NombreVista = nameof(VistaRegistroPago);
        Pagos = new List<string[]>();
        PanelCentral = new RepoVistaBase(contenedorVistas);

        Inicializar();
    }

    public string NombreVista {
        get => Name;
        private set => Name = value;
    }

    public int AlturaContenedorVistas {
        get => contenedorVistas.Height;
    }

    public int TuplasMaximasContenedor {
        get => AlturaContenedorVistas / VariablesGlobales.AlturaTuplaPredeterminada;
    }

    public RepoVistaBase PanelCentral { get; private set; }

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

    public long IdVenta { get; set; }

    public string MetodoPago {
        get => fieldMetodoPago.Text;
        set => fieldMetodoPago.Text = value;
    }

    public decimal Monto {
        get => decimal.TryParse(fieldMonto.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var total)
            ? total
            : 0;
        set => fieldMonto.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public List<string[]> Pagos { get; private set; }

    public bool ModoEdicion {
        get => _modoEdicion;
        set {
            fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
            btnRegistrar.Text = value ? "Actualizar pagos" : "Registrar pagos";
            _modoEdicion = value;
        }
    }

    public decimal Total {
        get => _total;
        set {
            _total = value;

            if (ModoEdicion)
                Suma = value;

            ActualizarPendiente();
        }
    }

    public decimal Suma {
        get => decimal.TryParse(fieldSuma.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var total)
            ? total
            : 0;
        set => fieldSuma.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public decimal Pendiente {
        get => decimal.TryParse(fieldPendiente.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var total)
            ? total
            : 0;
        set => fieldPendiente.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public decimal Devolucion {
        get => decimal.TryParse(fieldDevolucion.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var total)
            ? total
            : 0;
        set => fieldDevolucion.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public event EventHandler? EfectuarTransferencia;
    public event EventHandler? PagoAgregado;
    public event EventHandler? PagoEliminado;
    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;


    public void Inicializar() {
        CargarTiposMoneda(Enum.GetNames(typeof(TipoMoneda)));
        CargarMetodosPago();

        // Eventos
        btnCerrar.Click += delegate (object? sender, EventArgs args) { Close(); };
        fieldMetodoPago.SelectedIndexChanged += delegate (object? sender, EventArgs args) {
            if (fieldMetodoPago.SelectedIndex == 1) {
                EfectuarTransferencia?.Invoke(sender, args);

                fieldMonto.Focus();
            }
        };
        fieldMonto.TextChanged += delegate {
            btnAdicionarPago.Enabled = Monto > 0;
        };
        fieldMonto.KeyDown += delegate (object? sender, KeyEventArgs args) {
            if (args.KeyCode == Keys.Enter) {
                AdicionarPago(0, IdVenta, MetodoPago, Monto);

                args.SuppressKeyPress = true;
            }
        };
        fieldTipoMoneda.SelectedIndexChanged += async delegate {
            if (fieldTipoMoneda.SelectedIndex == 0)
                return;

            try {
                _tasaCambio = await UtilesCambioMoneda.ObtenerTasaPorDivisa(fieldTipoMoneda.Text);
            } catch (Exception ex) {
                CentroNotificaciones.Mostrar($"ERROR al consultar las tasas de cambio vigentes. {ex.Message}", Core.Mensajes.MVP.Modelos.TipoNotificacion.Error);
                fieldTipoMoneda.SelectedIndex = 0;
                return;
            }

            CentroNotificaciones.Mostrar($"La tasa de cambio del mercado informal para la moneda {fieldTipoMoneda.Text} fue actualizada con éxito.");
        };
        btnAdicionarPago.Click += delegate {
            AdicionarPago(0, IdVenta, MetodoPago, Monto);
        };
        PagoEliminado += delegate {
            ActualizarTuplasPagos();
            ActualizarSuma();
        };
        btnRegistrar.Click += delegate (object? sender, EventArgs args) {
            if (ModoEdicion)
                EditarEntidad?.Invoke(sender, args);
            else
                RegistrarEntidad?.Invoke(sender, args);
        };
        btnSalir.Click += delegate (object? sender, EventArgs args) {
            Close();
        };
    }

    public void AdicionarPago(long id, long idVenta, string metodoPago, decimal monto) {
        var adMetodoPago = string.IsNullOrEmpty(metodoPago) ? MetodoPago : metodoPago;
        var adMonto = monto < 0 ? Monto : monto;

        // Verificar cambio de moneda y multiplicar por el valor
        if (fieldTipoMoneda.SelectedIndex != 0 && _tasaCambio != null)
            adMonto *= _tasaCambio.Valor;

        var tuplaPago = new[] {
            id.ToString(),
            IdVenta.ToString(),
            adMetodoPago,
            adMonto.ToString("N2", CultureInfo.InvariantCulture)
        };

        Pagos?.Add(tuplaPago);

        ActualizarTuplasPagos();
        ActualizarSuma();

        if (adMetodoPago.Contains("Transferencia"))
            fieldMetodoPago.Items.Remove(adMetodoPago);

        fieldMetodoPago.SelectedIndex = 0;
        fieldMonto.Text = string.Empty;
    }

    public void CargarTiposMoneda(string[] tiposMoneda) {
        fieldTipoMoneda.Items.Clear();
        fieldTipoMoneda.Items.AddRange(tiposMoneda);
        fieldTipoMoneda.SelectedIndex = tiposMoneda.Length > 0 ? 0 : -1;
    }

    public void CargarMetodosPago() {
        fieldMetodoPago.Items.Clear();
        fieldMetodoPago.Items.AddRange(new[] {
            "Efectivo",
            "Transferencia"
        });
        fieldMetodoPago.SelectedIndex = 0;
    }

    public void Mostrar() {
        BringToFront();
        ShowDialog();
    }

    public void Restaurar() {
        ModoEdicion = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }

    private void ActualizarTuplasPagos() {
        PanelCentral.CerrarTodos();

        // Restablecer útima coordenada Y de la tupla
        VariablesGlobales.CoordenadaYUltimaTupla = 0;

        for (var i = 0; i < Pagos.Count; i++) {
            var pago = Pagos[i];
            var tuplaPago = new VistaTuplaPago();

            tuplaPago.Indice = i;
            tuplaPago.MetodoPago = pago[2];
            tuplaPago.Monto = pago[3];
            tuplaPago.Habilitada = !ModoEdicion;
            tuplaPago.EliminarDatosTupla += delegate (object? sender, EventArgs args) {
                pago = sender as string[];

                if (pago != null) {
                    Pagos.RemoveAt(Pagos.FindIndex(p => p[2].Equals(pago[0]) && p[3].Equals(pago[1])));
                    PagoEliminado?.Invoke(pago, args);
                }
            };

            // Registro y muestra
            PanelCentral?.Registrar(
                tuplaPago,
                new Point(0, VariablesGlobales.CoordenadaYUltimaTupla),
                new Size(contenedorVistas.Width - 20, VariablesGlobales.AlturaTuplaPredeterminada), 
                TipoRedimensionadoVista.Ninguno);
            tuplaPago.Mostrar();

            // Incremento de la útima coordenada Y de la tupla
            VariablesGlobales.CoordenadaYUltimaTupla += VariablesGlobales.AlturaTuplaPredeterminada;
        }
    }

    private void ActualizarSuma() {
        Suma = 0;

        foreach (var pago in Pagos)
            Suma += decimal.TryParse(pago[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var pagoParcial)
                ? pagoParcial
                : 0;

        ActualizarPendiente();
    }

    private void ActualizarPendiente() {
        var pendiente = Total - Suma;

        Pendiente = pendiente < 0 ? 0 : pendiente;

        if (!ModoEdicion)
            btnRegistrar.Enabled = Pendiente == 0;

        ActualizarDevolucion();
    }

    private void ActualizarDevolucion() {
        var pendiente = Total - Suma;

        Devolucion = pendiente < 0 ? pendiente * -1 : 0;
    }
}