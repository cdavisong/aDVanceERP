using System.Security;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion.Plantillas;
using aDVanceERP.Core.Seguridad.Properties;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion; 

public partial class VistaRegistroUsuario : Form, IVistaRegistroUsuario {
    public VistaRegistroUsuario() {
        InitializeComponent();

        NombreVista = nameof(VistaRegistroUsuario);

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

    public string NombreUsuario {
        get => fieldNombreUsuario.Text;
        set => fieldNombreUsuario.Text = value;
    }

    public SecureString? Password {
        get {
            if (!fieldPassword.Text.Equals(fieldConfirmarPassword.Text))
                return null;

            var password = new SecureString();

            foreach (var c in fieldPassword.Text)
                password.AppendChar(c);
            password.MakeReadOnly();

            return password;
        }
    }

    public bool ConfirmacionTerminosServicio {
        get => fieldAceptacionTerminosServicio.Checked;
        set => fieldAceptacionTerminosServicio.Checked = value;
    }

    public bool ModoEdicion { get; set; }

    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;
    public event EventHandler? AutenticarUsuario;
    

    public void Inicializar() {
        // Eventos
        fieldPassword.IconRightClick += delegate {
            // fieldPassword
            fieldPassword.UseSystemPasswordChar = !fieldPassword.UseSystemPasswordChar;
            fieldPassword.PasswordChar = fieldPassword.UseSystemPasswordChar ? '●' : char.MinValue;
            fieldPassword.IconRight =
                fieldPassword.UseSystemPasswordChar ? Resources.closed_eye_20px : Resources.eye_20px;
            // fieldConfirmarPassword
            fieldConfirmarPassword.UseSystemPasswordChar = fieldPassword.UseSystemPasswordChar;
            fieldConfirmarPassword.PasswordChar = fieldPassword.UseSystemPasswordChar ? '●' : char.MinValue;
        };
        btnRegistrarCuentaUsuario.Click += delegate(object? sender, EventArgs e) { 
            RegistrarEntidad?.Invoke(sender, e); 
        };
        btnRegresarAutenticar.Click += delegate(object? sender, EventArgs e) {
            AutenticarUsuario?.Invoke(sender, e);
            Ocultar();
        };
    }

    public void Mostrar() {
        BringToFront();
        Show();
    }

    public void Restaurar() {
        NombreUsuario = string.Empty;
        fieldPassword.Text = string.Empty;
        fieldConfirmarPassword.Text = string.Empty;
        ConfirmacionTerminosServicio = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}