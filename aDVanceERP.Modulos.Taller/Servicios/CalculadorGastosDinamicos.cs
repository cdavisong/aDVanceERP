using System.Data;
using System.Globalization;

namespace aDVanceERP.Modulos.Taller.Servicios;

public class CalculadorGastosDinamicos {
    private readonly Dictionary<string, decimal> _valoresConceptos;

    public CalculadorGastosDinamicos(Dictionary<string, decimal> valoresConceptos) {
        _valoresConceptos = valoresConceptos;
    }

    public decimal Calcular(string formula) {
        // Reemplazar variables con sus valores actuales
        var formulaParaCalcular = formula;

        foreach (var concepto in _valoresConceptos) {
            formulaParaCalcular = formulaParaCalcular.Replace(concepto.Key, concepto.Value.ToString(CultureInfo.InvariantCulture));
        }

        // Calcular el resultado
        return Convert.ToDecimal(new DataTable().Compute(formulaParaCalcular, null));
    }
}
