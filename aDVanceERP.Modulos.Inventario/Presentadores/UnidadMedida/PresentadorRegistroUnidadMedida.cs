using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores.UnidadMedida;

public class PresentadorRegistroUnidadMedida : PresentadorVistaRegistro<IVistaRegistroUnidadMedida, Core.Modelos.Modulos.Inventario.UnidadMedida, RepoUnidadMedida, FiltroBusquedaUnidadMedida> {
    public PresentadorRegistroUnidadMedida(IVistaRegistroUnidadMedida vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Core.Modelos.Modulos.Inventario.UnidadMedida objeto) {
        Vista.ModoEdicion = true;
        Vista.NombreUnidadMedida = objeto.Nombre;
        Vista.Abreviatura = objeto.Abreviatura;
        Vista.Descripcion = objeto.Descripcion ?? string.Empty; // Asegurar que no sea null

        _entidad = objeto;
    }

    protected override bool EntidadCorrecta() {
        var nombreOk = !string.IsNullOrEmpty(Vista.NombreUnidadMedida);
        var abreviaturaOk = !string.IsNullOrEmpty(Vista.Abreviatura);

        return nombreOk && abreviaturaOk;
    }

    protected override Core.Modelos.Modulos.Inventario.UnidadMedida? ObtenerEntidadDesdeVista() {
        return new Core.Modelos.Modulos.Inventario.UnidadMedida(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreUnidadMedida,
            Vista.Abreviatura,
            string.IsNullOrEmpty(Vista.Descripcion) ? null : Vista.Descripcion
        );
    }
}