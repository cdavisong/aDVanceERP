using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorRegistroCliente : PresentadorVistaRegistro<IVistaRegistroCliente, Cliente, RepoCliente, FiltroBusquedaCliente> {
    public PresentadorRegistroCliente(IVistaRegistroCliente vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Cliente objeto) {
        
    }

    protected override bool EntidadCorrecta() {
        return true;
    }

    protected override void RegistroEdicionAuxiliar(RepoCliente datosCliente, long id) {
        
    }

    protected override Cliente? ObtenerEntidadDesdeVista() {
        return null;
    }
}