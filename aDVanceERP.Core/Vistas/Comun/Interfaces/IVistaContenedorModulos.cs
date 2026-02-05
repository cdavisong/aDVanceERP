using aDVanceERP.Core.Repositorios.Comun;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces {
    public interface IVistaContenedorModulos : IVistaBase {
        FlowLayoutPanel PanelMenuLateral { get; }
        RepoVistaBase PanelCentral { get; }

        string MensajePortada { get; set; }
    }
}