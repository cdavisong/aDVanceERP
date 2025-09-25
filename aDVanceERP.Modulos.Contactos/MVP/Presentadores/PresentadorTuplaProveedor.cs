using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Proveedor.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Presentadores;

public class PresentadorTuplaProveedor : PresentadorVistaTupla<IVistaTuplaProveedor, Proveedor> {
    public PresentadorTuplaProveedor(IVistaTuplaProveedor vista, Proveedor objeto) : base(vista, objeto) { }
}