using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Cliente.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Cliente; 

public partial class VistaTuplaCliente : Form, IVistaTuplaCliente {
    public VistaTuplaCliente() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaCliente);

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

    public string Id {
        get => fieldId.Text;
        set => fieldId.Text = value;
    }

    public string Numero {
        get => fieldNumero.Text;
        set => fieldNumero.Text = value;
    }

    public string RazonSocial {
        get => fieldRazonSocial.Text;
        set {
            fieldRazonSocial.Text = value;
            fieldRazonSocial.Margin = new Padding(1, value?.Length > 28 ? 10 : 1, 1, 1);
        }
    }

    public string Telefonos {
        get => fieldTelefonos.Text;
        set => fieldTelefonos.Text = value;
    }

    public string Direccion {
        get => fieldDireccion.Text;
        set {
            fieldDireccion.Text = value;
            fieldDireccion.Margin = new Padding(1, value?.Length > 28 ? 10 : 1, 1, 1);
        }
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
        fieldNumero.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldRazonSocial.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldTelefonos.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldDireccion.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };

        btnEditar.Click += delegate(object? sender, EventArgs e) { EditarDatosTupla?.Invoke(this, e); };
        btnEliminar.Click += delegate(object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
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
        btnEditar.Enabled = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_CONTACTO_CLIENTES_EDITAR")
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_CONTACTO_CLIENTES_TODOS")
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_CONTACTO_TODOS");
        btnEliminar.Enabled = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_CONTACTO_CLIENTES_ELIMINAR")
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_CONTACTO_CLIENTES_TODOS")
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_CONTACTO_TODOS");
    }
}