﻿using System.Security;

using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Helpers;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Interfaces;
using aDVanceERP.Modulos.Seguridad.Properties;

namespace aDVanceERP.Modulos.Seguridad.Vistas.CuentaUsuario;

public partial class VistaRegistroCuentaUsuario : Form, IVistaRegistroCuentaUsuario {
    private bool _modoEdicion;

    public VistaRegistroCuentaUsuario() {
        InitializeComponent();

        NombreVista = nameof(VistaRegistroCuentaUsuario);

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

    public string? NombreUsuario {
        get => fieldNombreUsuario.Text;
        set => fieldNombreUsuario.Text = value;
    }

    public SecureString? Password {
        get {
            if (!fieldPassword.Text.Equals(fieldConfirmarPassword.Text)) {
                CentroNotificaciones.Mostrar($"Las contraseñas no coinciden, rectifique los datos y presiones al botón \"{btnRegistrar.Text}\"", TipoNotificacion.Advertencia);
                return null;
            }

            var password = new SecureString();

            foreach (var c in fieldPassword.Text)
                password.AppendChar(c);
            password.MakeReadOnly();

            return password;
        }
    }

    public string? NombreRolUsuario {
        get => fieldNombreRolUsuario.Text;
        set => fieldNombreRolUsuario.Text = value;
    }

    public bool ModoEdicion {
        get => _modoEdicion;
        set {
            if (value) {
                fieldPassword.Text = "test-password1";
                fieldConfirmarPassword.Text = "test-password1";
            }

            fieldPassword.IconRight = null;
            fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
            btnRegistrar.Text = value ? "Actualizar usuario" : "Registrar usuario";
            _modoEdicion = value;
        }
    }

    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;

    public void Inicializar() {
        // Eventos
        fieldPassword.IconRightClick += delegate {
            if (ModoEdicion)
                return;

            // fieldPassword
            fieldPassword.UseSystemPasswordChar = !fieldPassword.UseSystemPasswordChar;
            fieldPassword.PasswordChar = fieldPassword.UseSystemPasswordChar ? '●' : char.MinValue;
            fieldPassword.IconRight = fieldPassword.UseSystemPasswordChar ? Resources.closed_eye_20px : Resources.eye_20px;

            // fieldConfirmarPassword
            fieldConfirmarPassword.UseSystemPasswordChar = fieldPassword.UseSystemPasswordChar;
            fieldConfirmarPassword.PasswordChar = fieldPassword.UseSystemPasswordChar ? '●' : char.MinValue;
        };
        btnRegistrar.Click += delegate (object? sender, EventArgs args) {
            if (ModoEdicion && fieldPassword.Text != "test-password1") {
                if (!fieldPassword.Text.Equals(fieldConfirmarPassword.Text)) {
                    CentroNotificaciones.Mostrar($"Las contraseñas no coinciden, rectifique los datos y presiones al botón \"{btnRegistrar.Text}\"", TipoNotificacion.Advertencia);
                    return;
                }
                
                var usuario = RepoCuentaUsuario.Instancia.Buscar(FiltroBusquedaCuentaUsuario.Nombre, NombreUsuario).entidades.FirstOrDefault();

                if (usuario != null)
                    RepoCuentaUsuario.Instancia.CambiarPassword(usuario.Id, SecureStringHelper.HashPassword(Password));
            }

            if (ModoEdicion)
                EditarEntidad?.Invoke(sender, args);
            else
                RegistrarEntidad?.Invoke(sender, args);
        };
        btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
    }

    public void CargarRolesUsuarios(string?[] rolesUsuarios) {
        if (rolesUsuarios == null || rolesUsuarios.Length == 0)
            return;

        var rolesFiltrados = rolesUsuarios.Where(rol => rol != "Administrador").ToArray();

        fieldNombreRolUsuario.Items.Clear();
        fieldNombreRolUsuario.Items.Add("Ninguno");
        fieldNombreRolUsuario.Items.AddRange(rolesFiltrados);
        fieldNombreRolUsuario.SelectedIndex = rolesUsuarios.Length > 0 ? 0 : -1;
    }

    public void Mostrar() {
        BringToFront();
        Show();
    }

    public void Restaurar() {
        NombreUsuario = string.Empty;
        fieldPassword.Text = string.Empty;
        fieldConfirmarPassword.Text = string.Empty;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}