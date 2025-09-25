using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Taller.Modelos;

namespace aDVanceERP.Modulos.Taller.Interfaces
{
    public interface IVistaGestionOrdenesProduccion : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaOrdenProduccion>, INavegadorTuplasEntidades {
        bool HabilitarBtnCierreOrdenProduccion { get; set; }

        event EventHandler? CerrarOrdenProduccionSeleccionada;
    }
}