using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorRegistroMensajero : PresentadorVistaRegistro<IVistaRegistroMensajero, Mensajero, RepoMensajero, FiltroBusquedaMensajero> {
    public PresentadorRegistroMensajero(IVistaRegistroMensajero vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Mensajero objeto) {
        
    }

    protected override bool EntidadCorrecta() {
        return true;
    }

    protected override void RegistroEdicionAuxiliar(RepoMensajero datosMensajero, long id) {
        
    }

    protected override Mensajero? ObtenerEntidadDesdeVista() {
        return null;
    }
}