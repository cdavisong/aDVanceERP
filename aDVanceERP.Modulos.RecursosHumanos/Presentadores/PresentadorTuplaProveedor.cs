using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorTuplaProveedor : PresentadorVistaTupla<IVistaTuplaProveedor, Proveedor> {
    public PresentadorTuplaProveedor(IVistaTuplaProveedor vista, Proveedor objeto) : base(vista, objeto) { }
}