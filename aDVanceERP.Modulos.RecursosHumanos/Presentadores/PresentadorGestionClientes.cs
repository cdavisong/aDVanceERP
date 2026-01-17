using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;
using aDVanceERP.Modulos.RecursosHumanos.Vistas;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorGestionClientes : PresentadorVistaGestion<PresentadorTuplaCliente, IVistaGestionClientes, IVistaTuplaCliente, Cliente, RepoCliente, FiltroBusquedaCliente> {
    public PresentadorGestionClientes(IVistaGestionClientes vista) : base(vista) { }

    protected override PresentadorTuplaCliente ObtenerValoresTupla(Cliente objeto, List<IEntidadBaseDatos> entidadesExtra) {
        var presentadorTupla = new PresentadorTuplaCliente(new VistaTuplaCliente(), objeto);

        

        return presentadorTupla;
    }
}