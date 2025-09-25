using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.TipoMovimiento.Plantillas;

namespace aDVanceERP.Modulos.Inventario.MVP.Presentadores;

public class PresentadorRegistroTipoMovimiento : PresentadorVistaRegistro<IVistaRegistroTipoMovimiento, TipoMovimiento, RepoTipoMovimiento, FiltroBusquedaTipoMovimiento> {
    public PresentadorRegistroTipoMovimiento(IVistaRegistroTipoMovimiento vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(TipoMovimiento objeto) {
        Vista.NombreTipoMovimiento = objeto.Nombre;
        Vista.Efecto = objeto.Efecto.ToString();
        Vista.ModoEdicion = true;

        _entidad = objeto;
    }

    protected override bool EntidadCorrecta() {
        var nombreOk = !string.IsNullOrEmpty(Vista.NombreTipoMovimiento);
        var efectoOk = !string.IsNullOrEmpty(Vista.Efecto) && !Vista.Efecto.Equals("Ninguno");

        if (!nombreOk)
            CentroNotificaciones.Mostrar("El campo de nombre es obligatorio para el tipo de movimiento, por favor, corrija los datos entrados", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
        if (!efectoOk)
            CentroNotificaciones.Mostrar("Debe seleccionar un efecto para el nuevo tipo de movimiento, el campo no puede estar vacío", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);

        return nombreOk && efectoOk;
    }

    protected override TipoMovimiento? ObtenerEntidadDesdeVista() {
        return new TipoMovimiento(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreTipoMovimiento,
            (EfectoMovimiento)(Enum.TryParse(typeof(EfectoMovimiento), Vista.Efecto, out var efecto)
                ? efecto
                : default(EfectoMovimiento))
        );
    }
}