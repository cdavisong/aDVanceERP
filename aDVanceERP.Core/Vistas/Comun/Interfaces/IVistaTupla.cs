namespace aDVanceERP.Core.Vistas.Comun.Interfaces;

public interface IVistaTupla : IVistaBase {
    Color ColorFondoTupla { get; set; }

    event EventHandler? TuplaSeleccionada;
    event EventHandler? EditarDatosTupla;
    event EventHandler? EliminarDatosTupla;
}