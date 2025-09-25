using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.TipoMateriaPrima.Plantillas;

namespace aDVanceERP.Modulos.Inventario.MVP.Presentadores;

public class PresentadorRegistroTipoMateriaPrima : PresentadorVistaRegistro<IVistaRegistroTipoMateriaPrima, TipoMateriaPrima, RepoTipoMateriaPrima, FiltroBusquedaTipoMateriaPrima> {
    public PresentadorRegistroTipoMateriaPrima(IVistaRegistroTipoMateriaPrima vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(TipoMateriaPrima objeto) {
        Vista.ModoEdicion = true;
        Vista.NombreTipoMateriaPrima = objeto.Nombre;
        Vista.Descripcion = objeto.Descripcion ?? string.Empty;

        _entidad = objeto;
    }

    protected override bool EntidadCorrecta() {
        var nombreOk = !string.IsNullOrEmpty(Vista.NombreTipoMateriaPrima);

        if (!nombreOk)
            CentroNotificaciones.Mostrar("El campo de nombre es obligatorio para el tipo de materia prima, por favor, corrija los datos entrados", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);

        return nombreOk;
    }

    protected override TipoMateriaPrima? ObtenerEntidadDesdeVista() {
        return new TipoMateriaPrima(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreTipoMateriaPrima,
            string.IsNullOrEmpty(Vista.Descripcion) ? null : Vista.Descripcion
        );
    }
}