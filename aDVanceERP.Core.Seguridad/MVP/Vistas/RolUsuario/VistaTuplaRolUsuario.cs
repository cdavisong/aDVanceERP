using aDVanceERP.Core.Seguridad.MVP.Vistas.RolUsuario.Plantillas;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.RolUsuario; 

public partial class VistaTuplaRolUsuario : Form, IVistaTuplaRolUsuario {
    public VistaTuplaRolUsuario() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaRolUsuario);

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

    public string? Id {
        get => fieldId.Text;
        set => fieldId.Text = value;
    }

    public string? NombreRolUsuario {
        get => fieldNombreRolUsuario.Text;
        set {
            fieldNombreRolUsuario.Text = value;

            if (value != null && value.Equals("Administrador")) {
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }
    }

    public string? CantidadPermisos {
        get => fieldCantPermisosRol.Text;
        set => fieldCantPermisosRol.Text = value;
    }

    public string? CantidadUsuarios {
        get => fieldCantidadUsuariosRol.Text;
        set => fieldCantidadUsuariosRol.Text = value;
    }

    public Color ColorFondoTupla {
        get => layoutVista.BackColor;
        set => layoutVista.BackColor = value;
    }

    public event EventHandler? TuplaSeleccionada;
    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;
    

    public void Inicializar() {
        // Eventos
        fieldId.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldNombreRolUsuario.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldCantPermisosRol.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldCantidadUsuariosRol.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };

        btnEditar.Click += delegate(object? sender, EventArgs e) { EditarDatosTupla?.Invoke(this, e); };
        btnEliminar.Click += delegate(object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
    }

    public void Mostrar() {
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
}