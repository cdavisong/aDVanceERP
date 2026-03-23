using aDVanceERP.Core.Repositorios.Comun;

using System.Security.Cryptography;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces {
    public interface IVistaContenedorModulos : IVistaBase {
        FlowLayoutPanel PanelMenuLateral { get; }
        RepoVistaBase PanelCentral { get; }

        Label NombreModulo { get; }

        void ActualizarPortadaInicio(string version, string nombreUsuario);
    }
}