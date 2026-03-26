using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Interfaces {
    internal interface IVistaDetalleTurno : IVistaContenedor, IBuscadorEntidades<FiltroBusquedaCajaMovimiento>, IGestorEntidades, INavegadorTuplasEntidades {
        void CargarDatosGeneralesTurno(CajaTurno turno);
    }
}
