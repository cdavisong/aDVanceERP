using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores.TipoMateriaPrima;

public class PresentadorRegistroTipoMateriaPrima : PresentadorVistaRegistro<IVistaRegistroTipoMateriaPrima, Core.Modelos.Modulos.Inventario.ClasificacionProducto, RepoClasificacionProducto, FiltroBusquedaClasificacionProducto> {
    public PresentadorRegistroTipoMateriaPrima(IVistaRegistroTipoMateriaPrima vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(ClasificacionProducto objeto) {
        Vista.ModoEdicion = true;
        Vista.NombreTipoMateriaPrima = objeto.Nombre;
        Vista.Descripcion = objeto.Descripcion ?? string.Empty;

        _entidad = objeto;
    }

    protected override bool EntidadCorrecta() {
        var nombreOk = !string.IsNullOrEmpty(Vista.NombreTipoMateriaPrima);

        if (!nombreOk)
            CentroNotificaciones.Mostrar("El campo de nombre es obligatorio para el tipo de materia prima, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);

        return nombreOk;
    }

    protected override Core.Modelos.Modulos.Inventario.ClasificacionProducto? ObtenerEntidadDesdeVista() {
        return new Core.Modelos.Modulos.Inventario.ClasificacionProducto(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreTipoMateriaPrima,
            string.IsNullOrEmpty(Vista.Descripcion) ? null : Vista.Descripcion
        );
    }
}