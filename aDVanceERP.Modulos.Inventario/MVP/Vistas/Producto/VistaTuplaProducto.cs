﻿using System.Globalization;

using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto.Plantillas;
using Guna.UI2.WinForms;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto;

public partial class VistaTuplaProducto : Form, IVistaTuplaProducto {
    private string? _nombreAlmacen;

    public VistaTuplaProducto() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaProducto);

        Inicializar();
    }

    public string NombreVista {
        get => $"{Name}{Codigo}";
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

    public string NombreAlmacen {
        get => _nombreAlmacen ?? string.Empty;
        set => _nombreAlmacen = string.IsNullOrEmpty(value) ? "Ninguno" : value;
    }

    public string Codigo {
        get => fieldCodigo.Text;
        set => fieldCodigo.Text = value;
    }

    public DateTime FechaUltimoMovimiento {
        get => fieldFechaUltimoMovimiento.Text.Equals("-")
            ? DateTime.MinValue
            : DateTime.ParseExact(fieldFechaUltimoMovimiento.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        set => fieldFechaUltimoMovimiento.Text = value.Equals(DateTime.MinValue) 
            ? "-"
            : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    public string NombreProducto {
        get => fieldNombre.Text;
        set => fieldNombre.Text = value;
    }

    public string Descripcion {
        get => fieldDescripcion.Text;
        set => fieldDescripcion.Text = value;
    }

    public decimal CostoUnitario {
        get => decimal.TryParse(fieldCostoUnitario.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
            out var value)
            ? value
            : 0m;
        set => fieldCostoUnitario.Text = value > 0
                ? value.ToString("N2", CultureInfo.InvariantCulture)
                : "-";
    }

    public decimal PrecioVentaBase {
        get => decimal.TryParse(fieldPrecioVentaBase.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
            out var value)
            ? value
            : 0m;
        set => fieldPrecioVentaBase.Text = value > 0
                ? value.ToString("N2", CultureInfo.InvariantCulture)
                : "-";
    }

    public string UnidadMedida {
        get => fieldUnidadMedida.Text;
        set => fieldUnidadMedida.Text = value;
    }

    public decimal Stock {
        get => decimal.TryParse(fieldStock.Text, NumberStyles.Any, CultureInfo.InvariantCulture, 
            out var value) 
            ? value 
            : 0;
        set {
            fieldStock.ForeColor = value == 0 ? Color.Firebrick : Color.FromArgb(115, 109, 106);
            fieldStock.Font = new Font(fieldStock.Font, value == 0 ? FontStyle.Bold : FontStyle.Regular);
            fieldStock.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }
    }

    public Color ColorFondoTupla {
        get => layoutVista.BackColor;
        set => layoutVista.BackColor = value;
    }
    
    public event EventHandler? TuplaSeleccionada;
    public event EventHandler? MovimientoPositivoStock;
    public event EventHandler? MovimientoNegativoStock;
    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;
    

    public void Inicializar() {
        // Eventos
        foreach (var control in layoutVista.Controls) {
            if (control is Guna2CircleButton || control is Guna2Button)
                continue;

            ((Control)control).Click += OnSeleccionTupla;
        }
        btnMovimientoPositivo.Click += delegate (object? sender, EventArgs e) {
            MovimientoPositivoStock?.Invoke(NombreAlmacen, e);
        };
        btnMovimientoNegativo.Click += delegate (object? sender, EventArgs e) {
            MovimientoNegativoStock?.Invoke(NombreAlmacen, e);
        };
        btnEditar.Click += delegate (object? sender, EventArgs e) {
            EditarDatosTupla?.Invoke(this, e);
        };
        btnEliminar.Click += async delegate (object? sender, EventArgs e) {
            if (await UtilesProducto.PuedeEliminarProducto(long.Parse(Id)))
                EliminarDatosTupla?.Invoke(this, e);
            else
                CentroNotificaciones.Mostrar(
                    $"No se puede eliminar el producto {NombreProducto}, existen registros de movimientos asociados al mismo y podría dañar la integridad y trazabilidad de los datos.",
                    TipoNotificacion.Advertencia);
        };
    }

    private void OnSeleccionTupla(object? sender, EventArgs e) {
        TuplaSeleccionada?.Invoke(this, e);
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
        if (ContextoSeguridad.UsuarioAutenticado == null || ContextoSeguridad.PermisosUsuario == null) {
            btnMovimientoPositivo.Enabled = false;
            btnMovimientoNegativo.Enabled = false;
            btnEditar.Enabled = false;
            return;
        }

        btnMovimientoPositivo.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                        || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                            "MOD_INVENTARIO_MOVIMIENTOS_ADICIONAR")
                                        || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                            "MOD_INVENTARIO_MOVIMIENTOS_TODOS")
                                        || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                            "MOD_INVENTARIO_TODOS");
        btnMovimientoNegativo.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                        || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                            "MOD_INVENTARIO_MOVIMIENTOS_ADICIONAR")
                                        || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                            "MOD_INVENTARIO_MOVIMIENTOS_TODOS")
                                        || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                            "MOD_INVENTARIO_TODOS");
        btnEditar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                "MOD_INVENTARIO_PRODUCTOS_EDITAR")
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                "MOD_INVENTARIO_PRODUCTOS_TODOS")
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
    }
}