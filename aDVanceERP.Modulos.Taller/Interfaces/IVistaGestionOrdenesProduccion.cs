using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Taller;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Taller.Interfaces {
    public interface IVistaGestionOrdenesProduccion : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaOrdenProduccion>, INavegadorTuplasEntidades {
        bool HabilitarBtnCierreOrdenProduccion { get; set; }

        event EventHandler? CerrarOrdenProduccionSeleccionada;
    }
}