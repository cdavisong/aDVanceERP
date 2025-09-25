using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Taller.Interfaces
{
    internal interface IVistaTuplaOrdenMateriaPrima : IVistaTupla {
        int Indice { get; set; }
        string NombreAlmacen { get; set; }
        string NombreMateriaPrima { get; set; }
        string PrecioUnitario { get; set; }
        string Cantidad { get; set; }

        event EventHandler? PrecioUnitarioModificado;
    }
}
