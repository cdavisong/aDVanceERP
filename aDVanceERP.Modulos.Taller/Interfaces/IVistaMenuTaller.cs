using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Taller.Interfaces
{
    public interface IVistaMenuTaller : IVistaMenu {
        event EventHandler? VerOrdenesProduccion;
    }
}