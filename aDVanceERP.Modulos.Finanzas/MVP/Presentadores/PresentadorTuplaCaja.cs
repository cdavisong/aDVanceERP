using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja.Plantillas;

namespace aDVanceERP.Modulos.Finanzas.MVP.Presentadores
{
    public class PresentadorTuplaCaja : PresentadorVistaTupla<IVistaTuplaCaja, Caja> {
        public PresentadorTuplaCaja(IVistaTuplaCaja vista, Caja objeto) 
            : base(vista, objeto) { }
    }
}
