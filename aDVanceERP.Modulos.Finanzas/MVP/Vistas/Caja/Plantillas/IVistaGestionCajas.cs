using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja.Plantillas
{
    public interface IVistaGestionCajas : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaCaja>, INavegadorTuplasEntidades {
        bool HabilitarBtnRegistroMovimientoCaja { get; set; }
        bool HabilitarBtnCierreCaja { get; set; }

        event EventHandler? RegistrarMovimientoCajaSeleccionada;
        event EventHandler? CerrarCajaSeleccionada;
    }
}
