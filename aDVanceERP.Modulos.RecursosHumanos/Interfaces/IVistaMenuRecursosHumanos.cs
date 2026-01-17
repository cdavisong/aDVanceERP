using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Interfaces;

public interface IVistaMenuRecursosHumanos : IVistaMenu {
    event EventHandler? VerProveedores;
    event EventHandler? VerMensajeros;
    event EventHandler? VerClientes;
    event EventHandler? VerContactos;
}