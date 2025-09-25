using aDVanceERP.Core.Repositorios.Comun;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces;

public interface IVistaModulos : IVistaBase {
    FlowLayoutPanel PanelMenuLateral { get; }
    RepoVistaBase PanelCentral { get; }

    string MensajePortada { get; set; }

    event EventHandler? MostrarVistaInicio;
    event EventHandler? MostrarVistaEstadisticas;
    event EventHandler? MostrarMenuContactos;
    event EventHandler? MostrarMenuFinanzas;
    event EventHandler? MostrarMenuInventario;
    event EventHandler? MostrarMenuTaller;
    event EventHandler? MostrarMenuVentas;
    event EventHandler? MostrarMenuSeguridad;
    event EventHandler? CambioModulo;

    void PresionarBotonModulo(object? sender, EventArgs e);
}