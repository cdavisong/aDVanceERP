using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Taller.Interfaces
{
    public interface IVistaTuplaOrdenActividadProduccion : IVistaTupla {
        int Indice { get; set; }
        string NombreActividadProduccion { get; set; }
        string CostoActividad { get; set; }
        string Cantidad { get; set; }

        event EventHandler? CostoActividadModificado;
    }
}