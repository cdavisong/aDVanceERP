using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Modulos.Venta.Interfaces;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;

using System.Globalization;
using aDVanceERP.Core.Modelos.Modulos.Venta;

namespace DVanceERP.Modulos.Venta.Vistas;

public partial class VistaTuplaPedido : Form, IVistaTuplaPedido {
    private EstadoPedidoEnum _estadoPedido;

    public VistaTuplaPedido() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaPedido);

        Inicializar();
    }

    public string NombreVista {
        get => $"{Name}{Id}";
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

    public Color ColorFondoTupla {
        get => layoutVista.BackColor;
        set => layoutVista.BackColor = value;
    }

    public bool EstadoSeleccion { get; set; }

    public long Id {
        get => Convert.ToInt64(fieldId.Text);
        set => fieldId.Text = value.ToString();
    }

    public DateTime FechaPedido {
        get => fieldFechaPedido.Text.Equals("-")
                ? DateTime.MinValue
                : DateTime.ParseExact(fieldFechaPedido.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        set => fieldFechaPedido.Text = value.Equals(DateTime.MinValue)
            ? "-"
            : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    public string NombreCliente {
        get => fieldNombreCliente.Text;
        set {
            fieldNombreCliente.Text = value;
            fieldNombreCliente.Margin = fieldNombreCliente.AjusteAutomaticoMargenTexto();
        }
    }

    public DateTime FechaEntrega {
        get => fieldFechaEntrega.Text.Equals("-")
                    ? DateTime.MinValue
                    : DateTime.ParseExact(fieldFechaEntrega.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        set => fieldFechaEntrega.Text = value.Equals(DateTime.MinValue)
            ? "-"
            : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    public string DireccionEntrega {
        get => fieldDireccionaEntrega.Text;
        set {
            fieldDireccionaEntrega.Text = value;
            fieldDireccionaEntrega.Margin = fieldDireccionaEntrega.AjusteAutomaticoMargenTexto();
        }
    }

    public decimal ImporteTotal {
        get => decimal.TryParse(fieldImporteTotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                out var value)
                                ? value
                                : 0m;
        set => fieldImporteTotal.Text = value > 0
                ? value.ToString("N2", CultureInfo.InvariantCulture)
                : "-";
    }

    public EstadoPedidoEnum EstadoPedido {
        get => _estadoPedido;
        set {
            _estadoPedido = value;
            fieldEstado.Text = value.ToString();
            layoutVista.BackColor = ObtenerColorFondoTupla(value);
        }
    }

    public bool Activo {
        get => fieldEstado.Text.Equals("Activo");
        set {
            fieldEstado.Text = value ? "Activo" : "Inactivo";
            fieldEstado.ForeColor = value ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
        }
    }
        

    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;

    public void Inicializar() {
        // Eventos
        btnEditar.Click += delegate (object? sender, EventArgs e) { EditarDatosTupla?.Invoke(this, e); };
        btnEliminar.Click += delegate (object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
    }

    public void Mostrar() {
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        ColorFondoTupla = BackColor;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }

    private void VerificarPermisos() {
        btnEditar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_VENTA_EDITAR")
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_VENTA_TODOS")
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_TODOS");
        btnEliminar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_VENTA_ELIMINAR")
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_VENTA_TODOS")
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_TODOS");
    }

    private Color ObtenerColorFondoTupla(EstadoPedidoEnum estado) {
        return estado switch { 
            EstadoPedidoEnum.Pendiente => ContextoAplicacion.ColorAdvertenciaTupla,
            EstadoPedidoEnum.Confirmado => ContextoAplicacion.ColorAdvertenciaTupla,
            EstadoPedidoEnum.ListoParaRetirar => ContextoAplicacion.ColorOkTupla,
            EstadoPedidoEnum.Cancelado => ContextoAplicacion.ColorAdvertenciaTupla, 
            _ => BackColor
        };
    }
}