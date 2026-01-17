using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorRegistroProveedor : PresentadorVistaRegistro<IVistaRegistroProveedor, Proveedor, RepoProveedor, FiltroBusquedaProveedor> {
    public PresentadorRegistroProveedor(IVistaRegistroProveedor vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Proveedor objeto) {
        
    }

    protected override bool EntidadCorrecta() {
        return true;
    }

    protected override void RegistroEdicionAuxiliar(RepoProveedor datosProveedor, long id) {
        
    }

    protected override Proveedor? ObtenerEntidadDesdeVista() {
        return null;
    }
}