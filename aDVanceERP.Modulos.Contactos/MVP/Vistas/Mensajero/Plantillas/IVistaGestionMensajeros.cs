using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Contactos;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Mensajero.Plantillas;

public interface IVistaGestionMensajeros : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaMensajero>,
    INavegadorTuplasEntidades {
    bool MostrarBtnHabilitarDeshabilitarMensajero { get; set; }

    event EventHandler? HabilitarDeshabilitarMensajero;
}