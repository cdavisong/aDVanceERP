using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.CuentaBancaria.Plantillas;

namespace aDVanceERP.Modulos.Finanzas.MVP.Presentadores;

public class PresentadorTuplaCuentaBancaria : PresentadorVistaTupla<IVistaTuplaCuentaBancaria, CuentaBancaria> {
    public PresentadorTuplaCuentaBancaria(IVistaTuplaCuentaBancaria vista, CuentaBancaria objeto) :
        base(vista, objeto) { }
}