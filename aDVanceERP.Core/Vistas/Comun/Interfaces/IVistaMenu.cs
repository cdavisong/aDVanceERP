namespace aDVanceERP.Core.Vistas.Comun.Interfaces;

public interface IVistaMenu : IVistaBase {
    event EventHandler? CambioMenu;

    void MostrarCaracteristicaInicial();
    void PresionarBotonSeleccion(object? sender, EventArgs e);
}