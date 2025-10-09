using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores.Almacen;

public class PresentadorRegistroAlmacen : PresentadorVistaRegistro<IVistaRegistroAlmacen, Core.Modelos.Modulos.Inventario.Almacen, RepoAlmacen, FiltroBusquedaAlmacen> {
    public PresentadorRegistroAlmacen(IVistaRegistroAlmacen vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Core.Modelos.Modulos.Inventario.Almacen entidad) {
        Vista.ModoEdicion = true;
        Vista.NombreAlmacen = entidad.Nombre ?? string.Empty;
        Vista.Direccion = entidad.Direccion ?? string.Empty;
        Vista.AutorizoVenta = true;
        Vista.Descripcion = entidad.Descripcion ?? string.Empty;
        Vista.ModoEdicion = true;

        _entidad = entidad;
    }

    protected override bool EntidadCorrecta() {
        var nombreRepetido = !Vista.ModoEdicion && UtilesAlmacen.ObtenerIdAlmacen(Vista.NombreAlmacen).Result > 0;
        var nombreOk = !string.IsNullOrEmpty(Vista.NombreAlmacen) && !nombreRepetido;

        if (nombreRepetido)
            CentroNotificaciones.Mostrar("Ye existe un almacén con el mismo nombre registrado en el sistema, los nombres de almacenes deben ser únicos.", TipoNotificacion.Advertencia);
        if (!nombreOk)
            CentroNotificaciones.Mostrar("El campo de nombre es obligatorio para el almacén, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);

        return nombreOk;
    }

    protected override Core.Modelos.Modulos.Inventario.Almacen? ObtenerEntidadDesdeVista() {
        if (Vista == null) {
            CentroNotificaciones.Mostrar("La vista no está inicializada.", TipoNotificacion.Error);
            return null;
        }

        //TODO: Trabajar en los campos que faltan para adicionar en el almacen
        return new Core.Modelos.Modulos.Inventario.Almacen(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreAlmacen ?? string.Empty,
            Vista.Descripcion ?? string.Empty,
            Vista.Direccion ?? string.Empty,
            0,
            TipoAlmacen.Principal,
            true,
            null
        );
    }
}