using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Interfaces {
    public interface IVistaGestionEmpleados : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaEmpleado>, INavegadorTuplasEntidades {
    }
}
