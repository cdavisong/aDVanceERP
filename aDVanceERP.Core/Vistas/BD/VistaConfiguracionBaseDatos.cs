using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.BD;
using aDVanceERP.Core.Presentadores.BD;
using aDVanceERP.Core.Properties;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Core.Vistas.BD.Interfaces;

namespace aDVanceERP.Core.Vistas.BD;

public partial class VistaConfiguracionBaseDatos : Form, IVistaConfServidorMySQL {
    private PresentadorConfiguracionBaseDatos? _presentador;

    public VistaConfiguracionBaseDatos() {
        InitializeComponent();

        NombreVista = nameof(VistaConfiguracionBaseDatos);

        _presentador = new PresentadorConfiguracionBaseDatos(this, new RepoConfiguracionBaseDatos());
        _presentador.ConfiguracionCargada += (s, e) => ConfiguracionCargada?.Invoke(s, e);

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

    public string? NombreDireccionServidor {
        get => fieldDireccionServidor.Text;
        set => fieldDireccionServidor.Text = value;
    }

    public string? NombreBaseDatos {
        get => fieldNombreBd.Text;
        set => fieldNombreBd.Text = value;
    }

    public string? NombreUsuario {
        get => fieldNombreUsuario.Text;
        set => fieldNombreUsuario.Text = value;
    }

    public string? Password {
        get => fieldPassword.Text;
        set => fieldPassword.Text = value;
    }

    public bool RecordarConfiguracion {
        get => fieldRecordarConfiguracionBd.Checked;
        set => fieldRecordarConfiguracionBd.Checked = value;
    }

    public event EventHandler<ConfiguracionBaseDatos>? AlmacenarConfiguracion;
    public event EventHandler? ConfiguracionCargada;

    public void Inicializar() {
        // Conectar eventos
        fieldPassword.IconRightClick += delegate {
            fieldPassword.UseSystemPasswordChar = !fieldPassword.UseSystemPasswordChar;
            fieldPassword.PasswordChar = fieldPassword.UseSystemPasswordChar ? '●' : char.MinValue;
            fieldPassword.IconRight = fieldPassword.UseSystemPasswordChar ? Resources.closed_eye_20px : Resources.eye_20px;
        };
        btnValidarConexion.Click += delegate(object? sender, EventArgs e) {
            AlmacenarConfiguracion?.Invoke(sender,
                new ConfiguracionBaseDatos {
                    Servidor = NombreDireccionServidor ?? "localhost",
                    BaseDatos = NombreBaseDatos ?? "advanceerp",
                    Usuario = NombreUsuario ?? "admin",
                    Password = Password ?? "admin",
                    RecordarConfiguracion = RecordarConfiguracion
                });
        };
    }

    public void Mostrar() {
        _presentador?.CargarConfiguracion();

        if (!ContextoBaseDatos.EsConfiguracionCargada) {
            BringToFront();
            Show();
        }
    }

    public void Restaurar() {
        NombreDireccionServidor = string.Empty;
        NombreBaseDatos = string.Empty;
        NombreUsuario = string.Empty;
        Password = string.Empty;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}