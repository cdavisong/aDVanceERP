namespace aDVanceERP.Core.Vistas.Comun.Interfaces;

public interface IVistaTupla : IVistaBase {
    Color ColorFondoTupla { get; set; }
    bool EstadoSeleccion {  get; set; }

    event EventHandler? EditarDatosTupla;
    event EventHandler? EliminarDatosTupla;
}