using aDVanceERP.Core.Repositorios.Comun;
using Guna.UI2.WinForms;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces;

public interface IVistaPrincipal : IVistaBase {
    string Titulo { get; }

    #region Barra de título

    RepoVistaBase BarraTitulo { get; }
    Guna2Button BtnNotificaciones { get; }
    Guna2Button BtnMensajes { get; }
    Guna2CirclePictureBox BtnMenuUsuario { get; }

    #endregion

    RepoVistaBase PanelCentral { get; }
    RepoVistaBase BarraEstado { get; }

    event EventHandler? VerMenuUsuario;
    event EventHandler? VerMensajes;
    event EventHandler? VerNotificaciones;

    void ModificarVisibilidadBotonesBarraTitulo(bool visible);
}
