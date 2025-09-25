using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Menu.Plantillas;

public interface IVistaMenuContacto : IVistaMenu {
    event EventHandler? VerProveedores;
    event EventHandler? VerMensajeros;
    event EventHandler? VerClientes;
    event EventHandler? VerContactos;
}