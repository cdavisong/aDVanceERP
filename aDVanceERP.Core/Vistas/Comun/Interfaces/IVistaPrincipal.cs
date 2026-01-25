using aDVanceERP.Core.Repositorios.Comun;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces {
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
}
