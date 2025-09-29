using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Finanzas;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja.Plantillas {
    public interface IVistaGestionCajas : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaCaja>, INavegadorTuplasEntidades {
        bool HabilitarBtnRegistroMovimientoCaja { get; set; }
        bool HabilitarBtnCierreCaja { get; set; }

        event EventHandler? RegistrarMovimientoCajaSeleccionada;
        event EventHandler? CerrarCajaSeleccionada;
    }
}
