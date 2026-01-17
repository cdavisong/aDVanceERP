using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;
using aDVanceERP.Modulos.RecursosHumanos.Vistas;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorGestionProveedores : PresentadorVistaGestion<PresentadorTuplaProveedor, IVistaGestionProveedores,
    IVistaTuplaProveedor, Proveedor, RepoProveedor, FiltroBusquedaProveedor> {
    public PresentadorGestionProveedores(IVistaGestionProveedores vista) : base(vista) { }

    protected override PresentadorTuplaProveedor ObtenerValoresTupla(Proveedor objeto, List<IEntidadBaseDatos> entidadesExtra) {
        var presentadorTupla = new PresentadorTuplaProveedor(new VistaTuplaProveedor(), objeto);

        

        return presentadorTupla;
    }
}