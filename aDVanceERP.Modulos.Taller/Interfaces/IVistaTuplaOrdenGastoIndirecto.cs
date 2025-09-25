using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Taller.Interfaces
{
    public interface IVistaTuplaOrdenGastoIndirecto : IVistaTupla {
        int Indice { get; set; }
        string ConceptoGasto { get; set; }
        string Monto { get; set; }
        string Cantidad { get; set; }

        event EventHandler? MontoGastoModificado;
    }
}