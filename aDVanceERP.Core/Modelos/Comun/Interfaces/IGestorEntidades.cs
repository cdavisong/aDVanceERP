namespace aDVanceERP.Core.Modelos.Comun.Interfaces;

public interface IGestorEntidades {
    event EventHandler? RegistrarEntidad;
    event EventHandler? EditarEntidad;
    event EventHandler? EliminarEntidad;
}