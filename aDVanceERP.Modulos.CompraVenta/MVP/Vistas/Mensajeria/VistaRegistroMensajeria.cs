using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Mensajeria.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Mensajeria;

public partial class VistaRegistroMensajeria : Form, IVistaRegistroMensajeria {
    private bool _modoEdicion;
    private string[] _descripcionesTiposEntrega = Array.Empty<string>();
    private List<string[]>? _productosVenta = new();

    public VistaRegistroMensajeria() {
        InitializeComponent();

        NombreVista = nameof(VistaRegistroMensajeria);

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

    public long IdVenta { get; set; }

    public string? RazonSocialCliente {
        get => fieldRazonSocialCliente.Text;
        set {
            fieldRazonSocialCliente.Text = value;

            PopularRepoCliente(value);
        }
    }

    public string? TelefonosCliente { get; private set; }

    public string? NombreMensajero {
        get => fieldNombreMensajero.Text;
        set => fieldNombreMensajero.Text = value;
    }

    public string? TipoEntrega {
        get => fieldTipoEntrega.Text;
        set => fieldTipoEntrega.Text = value;
    }

    public string DescripcionTipoEntrega {
        get => fieldDescripcionTipoEntrega.Text;
        set => fieldDescripcionTipoEntrega.Text = value;
    }

    public string? Direccion {
        get => fieldDireccion.Text;
        set {
            fieldDireccion.Text = value;
            fieldDireccion.Margin = new Padding(1, value?.Length > 43 ? 10 : 1, 1, 1);
        }
    }

    public string ResumenEntrega {
        get => fieldResumenEntrega.Text;
        set => fieldResumenEntrega.Text = value;
    }

    public bool ModoEdicion {
        get => _modoEdicion;
        set {
            btnAdicionarMensajero.Enabled = !value;
            btnAdicionarCliente.Enabled = !value;
            fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
            btnRegistrar.Text = value ? "Actualizar contacto" : "Registrar contacto";
            _modoEdicion = value;
        }
    }

    public string? Observaciones {
        get => fieldObservaciones.Text;
        set => fieldObservaciones.Text = value;
    }

    public event EventHandler? AsignarNuevoMensajero;
    public event EventHandler? AsignarNuevoCliente;
    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;


    public void Inicializar() {
        // Eventos
        btnCerrar.Click += delegate (object? sender, EventArgs args) {
            Close();
        };
        btnAdicionarMensajero.Click += delegate (object? sender, EventArgs args) {
            AsignarNuevoMensajero?.Invoke(sender, args);
        };
        btnAdicionarCliente.Click += delegate (object? sender, EventArgs args) {
            AsignarNuevoCliente?.Invoke(sender, args);
        };
        fieldNombreMensajero.SelectedIndexChanged += delegate {
            ActualizarResumenEntrega();
        };
        fieldTipoEntrega.SelectedIndexChanged += delegate {
            DescripcionTipoEntrega = _descripcionesTiposEntrega[fieldTipoEntrega.SelectedIndex];

            ActualizarResumenEntrega();
        };
        fieldRazonSocialCliente.TextChanged += delegate {
            PopularRepoCliente(RazonSocialCliente);
            ActualizarResumenEntrega();
        };
        fieldDireccion.TextChanged += delegate {
            ActualizarResumenEntrega();
        };
        fieldObservaciones.TextChanged += delegate {
            ActualizarResumenEntrega();
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

    public void CargarNombresMensajeros(object[] nombresMensajeros) {
        fieldNombreMensajero.Items.Clear();
        fieldNombreMensajero.Items.AddRange(nombresMensajeros);
        fieldNombreMensajero.SelectedIndex = -1;
    }

    public void CargarTiposEntrega() {
        #region Obtención de tipos y descripciones

        var tiposDescripciones = UtilesEntrega.ObtenerNombreDescripcionTiposEntrega(false).Result;
        var tipos = new List<object>();
        var descripciones = new List<string>();

        foreach (var item in tiposDescripciones) {
            var partes = item.Split('|');

            if (partes.Length < 2)
                continue;

            tipos.Add(partes[0].Trim()); // Nombre
            descripciones.Add(partes[1].Trim()); // Descripción
        }

        #endregion

        fieldTipoEntrega.Items.Clear();
        fieldTipoEntrega.Items.AddRange(tipos.ToArray());
        fieldTipoEntrega.SelectedIndex = -1;

        _descripcionesTiposEntrega = descripciones.ToArray();
    }

    public void CargarRazonesSocialesClientes(string[] razonesSocialesClientes) {
        fieldRazonSocialCliente.AutoCompleteCustomSource.Clear();
        fieldRazonSocialCliente.AutoCompleteCustomSource.AddRange(razonesSocialesClientes);
        fieldRazonSocialCliente.AutoCompleteMode = AutoCompleteMode.Suggest;
        fieldRazonSocialCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;
    }

    private void PopularRepoCliente(string? nombreCliente) {
        if (nombreCliente != null) {
            var idCliente = UtilesCliente.ObtenerIdCliente(nombreCliente);

            if (idCliente == 0)
                return;

            TelefonosCliente = UtilesTelefonoContacto.ObtenerTelefonoCliente(idCliente, true) ?? UtilesTelefonoContacto.ObtenerTelefonoCliente(idCliente, false);
            Direccion = UtilesCliente.ObtenerDireccionCliente(idCliente);
        }
    }

    public void PopularProductosVenta(List<string[]>? datosProductos) {
        _productosVenta = datosProductos;

        ActualizarResumenEntrega();
    }

    public void Mostrar() {
        BringToFront();
        ShowDialog();
    }

    public void Restaurar() {
        NombreMensajero = string.Empty;
        fieldNombreMensajero.SelectedIndex = -1;
        TipoEntrega = string.Empty;
        fieldTipoEntrega.SelectedIndex = -1;
        DescripcionTipoEntrega = "...";
        Direccion = string.Empty;
        ResumenEntrega = "...";
        ModoEdicion = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }

    private void ActualizarResumenEntrega() {
        var resumenHtml = $@"
<div class='resumen-entrega'>
    <h3>Entrega #{UtilesBD.ObtenerUltimoIdTabla("seguimiento_entrega") + 1:000}</h3>
    <p><strong>Fecha:</strong> {DateTime.Now:yyyy-MM-dd}</p>
    
    <div class='seccion-cliente' style='margin-bottom: 10px;'>
        <h4 style='margin-bottom: 5px;'>Datos del cliente</h4>
        <hr style='margin: 5px 0;'>
        <p style='margin: 2px 0;'><strong>Nombre:</strong> {RazonSocialCliente}</p>
        <p style='margin: 2px 0;'><strong>Teléfonos:</strong> {TelefonosCliente}</p>
        <p style='margin: 2px 0;'><strong>Dirección:</strong> {Direccion}</p>
        <p style='margin: 2px 0;'><strong>Observaciones:</strong> {Observaciones}</p>
    </div>
    
    <div class='seccion-productos'>
        <h4 style='margin-bottom: 5px;'>Productos</h4>
        <hr style='margin: 5px 0;'>";

        if (_productosVenta != null)
            resumenHtml = _productosVenta.Aggregate(resumenHtml,
                (current, producto) => current + $@"
        <p style='margin: 2px 0;'><strong>{producto[4]}</strong> - {producto[1]}</p>");

        resumenHtml += @"
    </div>
</div>";

        // Actualizar el resúmen de entrega
        ResumenEntrega = resumenHtml;

        if (ModoEdicion)
            return;

        // Verificar si la entrega es válida
        var mensajeroOk = !string.IsNullOrEmpty(NombreMensajero);
        var tipoEntregaOk = !string.IsNullOrEmpty(TipoEntrega);
        var direccionOk = !string.IsNullOrEmpty(Direccion);

        btnRegistrar.Enabled = mensajeroOk && tipoEntregaOk && direccionOk;
    }
}