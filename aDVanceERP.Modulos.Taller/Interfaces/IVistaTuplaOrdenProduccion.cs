using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Taller.Interfaces
{
    public interface IVistaTuplaOrdenProduccion : IVistaTupla {
        string Id { get; set; }
        string NumeroOrden { get; set; }
        string FechaApertura { get; set; }
        string NombreProducto { get; set; }
        string TotalUnidadesProducidas { get; set; }
        string CostoTotal { get; set; }
        string PrecioUnitario { get; set; }
        int Estado { get; set; }
        string FechaCierre { get; set; }
    }
}