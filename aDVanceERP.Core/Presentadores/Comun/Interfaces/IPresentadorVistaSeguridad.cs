using aDVanceERP.Core.Vistas.BD;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun.Interfaces {
    public interface IPresentadorVistaContenedorSeguridad<Vs> : IPresentadorVistaBase<Vs>
         where Vs : class, IVistaContenedorSeguridad {
        VistaConfiguracionBaseDatos ConfiguracionBaseDatos { get; }
    }
}