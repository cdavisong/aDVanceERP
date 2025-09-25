using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using Guna.UI2.WinForms.Suite;
using Guna.UI2.WinForms;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos : IPresentadorVistaModulos<IVistaModulos> {
    public PresentadorModulos(IVistaPrincipal vistaPrincipal, IVistaModulos vistaModulos) {
        VistaPrincipal = vistaPrincipal;
        Vista = vistaModulos;

        // Eventos
        Vista.MostrarVistaEstadisticas += MostrarVistaContenedorEstadisticas;
        Vista.MostrarMenuContactos += MostrarVistaMenuContacto;
        Vista.MostrarMenuFinanzas += MostrarVistaMenuFinanzas;
        Vista.MostrarMenuInventario += MostrarVistaMenuInventario;
        Vista.MostrarMenuTaller += MostrarVistaMenuTaller;
        Vista.MostrarMenuVentas += MostrarVistaMenuVentas;
        Vista.MostrarMenuSeguridad += MostrarVistaMenuSeguridad;

        #region Vista : Estadísticas

        InicializarVistaContenedorEstadisticas();

        #endregion

        #region Módulo : Contactos

        InicializarVistaMenuContacto();
        InicializarVistaGestionProveedores();
        InicializarVistaGestionMensajeros();
        InicializarVistaGestionClientes();
        InicializarVistaGestionContactos();

        #endregion

        #region Módulo : Finanzas

        InicializarVistaMenuFinanzas();
        InicializarVistaGestionCuentasBancarias();
        InicializarVistaGestionCajas();

        #endregion

        #region Módulo : Inventario

        InicializarVistaMenuInventario();
        InicializarVistaGestionProductos();
        InicializarVistaGestionMovimientos();
        InicializarVistaGestionAlmacenes();
        InicializarVistaRegistroAlmacen();

        #endregion

        #region Módulo : Taller

        InicializarVistaMenuTaller();
        InicializarVistaGestionOrdenesProduccion();
        InicializarVistaRegistroOrdenProduccion();

        #endregion

        #region Módulo : Compraventa

        InicializarVistaMenuCompraventas();
        InicializarVistaGestionCompras();
        InicializarVistaGestionVentas();

        #endregion

        #region Módulo : Seguridad

        InicializarVistaMenuSeguridad();
        InicializarVistaGestionCuentasUsuarios();
        InicializarVistaGestionRolesUsuarios();

        #endregion
    }

    public void AdicionarBotonAccesoModulo(Guna2CircleButton btnModulo) {
        Vista.PanelMenuLateral.SuspendLayout();

        CustomizableEdges customizableEdges = new CustomizableEdges();

        btnModulo.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
        btnModulo.CheckedState.FillColor = Color.PeachPuff;
        btnModulo.CustomImages.ImageAlign = HorizontalAlignment.Center;
        btnModulo.CustomImages.ImageSize = new Size(24, 24);
        btnModulo.FillColor = Color.White;
        btnModulo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        btnModulo.ForeColor = Color.White;
        btnModulo.ImageSize = new Size(24, 24);
        btnModulo.ShadowDecoration.CustomizableEdges = customizableEdges;
        btnModulo.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
        btnModulo.Size = new Size(44, 44);
        btnModulo.TabIndex = Vista.PanelMenuLateral.Controls.Count + 1;

        Vista.PanelMenuLateral.Controls.Add(btnModulo);
        Vista.PanelMenuLateral.ResumeLayout(false);
    }


    public IVistaPrincipal VistaPrincipal { get; }

    public IVistaModulos Vista { get; }

    public void Dispose() {
        Vista.Dispose();
    }
}