using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Cliente.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Presentadores;

public class PresentadorTuplaCliente : PresentadorVistaTupla<IVistaTuplaCliente, Cliente> {
    public PresentadorTuplaCliente(IVistaTuplaCliente vista, Cliente objeto) : base(vista, objeto) { }
}