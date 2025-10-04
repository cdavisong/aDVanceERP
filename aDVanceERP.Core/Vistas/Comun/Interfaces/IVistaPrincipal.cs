using aDVanceERP.Core.Repositorios.Comun;
using Guna.UI2.WinForms;

using System.Windows.Forms;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces;

public interface IVistaPrincipal : IVistaBase {
    string Titulo { get; }

    #region Barra de título

    RepoVistaBase BarraTitulo { get; }
    FlowLayoutPanel BotonesTitulo { get; }

    #endregion

    RepoVistaBase PanelCentral { get; }
    RepoVistaBase BarraEstado { get; }

    void ModificarVisibilidadBotonesBarraTitulo(bool visible);
}
