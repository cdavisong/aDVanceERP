using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Mensajero.Plantillas;

public interface IVistaGestionMensajeros : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaMensajero>,
    INavegadorTuplasEntidades {
    bool MostrarBtnHabilitarDeshabilitarMensajero { get; set; }

    event EventHandler? HabilitarDeshabilitarMensajero;
}