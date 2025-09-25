using System.Security;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion.Plantillas;
using aDVanceERP.Core.Seguridad.Properties;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion; 

public partial class VistaAutenticacionUsuario : Form, IVistaAutenticacionUsuario {
    public VistaAutenticacionUsuario() {
        InitializeComponent();

        NombreVista = nameof(VistaAutenticacionUsuario);

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

    public SecureString Password {
        get {
            var password = new SecureString();

            foreach (var c in fieldPassword.Text)
                password.AppendChar(c);
            password.MakeReadOnly();

            return password;
        }
    }

    public event EventHandler? Autenticar;
    public event EventHandler? RegistrarCuenta;
    

    public void Inicializar() {
        // Eventos
        fieldPassword.IconRightClick += delegate {
            fieldPassword.UseSystemPasswordChar = !fieldPassword.UseSystemPasswordChar;
            fieldPassword.PasswordChar = fieldPassword.UseSystemPasswordChar ? '●' : char.MinValue;
            fieldPassword.IconRight = fieldPassword.UseSystemPasswordChar ? Resources.closed_eye_20px : Resources.eye_20px;
        };
        fieldPassword.KeyDown += delegate(object? sender, KeyEventArgs args) {
            if (args.KeyCode == Keys.Enter) {
                Autenticar?.Invoke(this, EventArgs.Empty);

                args.SuppressKeyPress = true;
            }
        };
        btnAutenticarUsuario.Click += delegate { 
            Autenticar?.Invoke(this, EventArgs.Empty); 
        };
        btnRegistrarCuenta.Click += delegate(object? sender, EventArgs e) {
            RegistrarCuenta?.Invoke(sender, e);
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
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}