using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Mensajero.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Presentadores;

public class PresentadorTuplaMensajero : PresentadorVistaTupla<IVistaTuplaMensajero, Mensajero> {
    public PresentadorTuplaMensajero(IVistaTuplaMensajero vista, Mensajero objeto) : base(vista, objeto) { }
}