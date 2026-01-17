using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores;

public class PresentadorRegistroAlmacen : PresentadorVistaRegistro<IVistaRegistroAlmacen, Core.Modelos.Modulos.Inventario.Almacen, RepoAlmacen, FiltroBusquedaAlmacen> {
    public PresentadorRegistroAlmacen(IVistaRegistroAlmacen vista) : base(vista) {
        AgregadorEventos.Suscribir("MostrarVistaRegistroAlmacen", OnMostrarVistaRegistroAlmacen);
        AgregadorEventos.Suscribir("MostrarVistaEdicionAlmacen", OnMostrarVistaEdicionAlmacen);
    }

    private void OnMostrarVistaRegistroAlmacen(string obj) {
        Vista.ModoEdicion = false;
        Vista.Restaurar();
        
        Vista.Mostrar();
    }

    private void OnMostrarVistaEdicionAlmacen(string obj) {
        Vista.ModoEdicion = true;
        Vista.Restaurar();

        if (string.IsNullOrEmpty(obj))
            return;

        var almacen = AgregadorEventos.DeserializarPayload<Core.Modelos.Modulos.Inventario.Almacen>(obj);

        if (almacen == null)
            return;

        PopularVistaDesdeEntidad(almacen);

        Vista.Mostrar();
    }

    public override void PopularVistaDesdeEntidad(Core.Modelos.Modulos.Inventario.Almacen objeto) {
        base.PopularVistaDesdeEntidad(objeto);

        Vista.NombreAlmacen = objeto.Nombre;
        Vista.Direccion = objeto.Direccion;
        Vista.Descripcion = objeto.Descripcion;
        Vista.Capacidad = objeto.Capacidad;
        Vista.Tipo = objeto.Tipo;
        Vista.Estado = objeto.Estado;
        Vista.CoordenadasGeograficas = objeto.Coordenadas;
    }

    protected override Core.Modelos.Modulos.Inventario.Almacen? ObtenerEntidadDesdeVista() {
        return new Core.Modelos.Modulos.Inventario.Almacen {
            Id = _entidad?.Id ?? 0,
            Nombre = Vista.NombreAlmacen,
            Descripcion = Vista.Descripcion,
            Direccion = Vista.Direccion,
            Capacidad = Vista.Capacidad,
            Tipo = Vista.Tipo,
            Estado = Vista.Estado,
            Coordenadas = Vista.CoordenadasGeograficas
        }; 
    }

    protected override bool EntidadCorrecta() {
        var nombreRepetido = !Vista.ModoEdicion && RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacen).cantidad > 0;
        var nombreOk = !string.IsNullOrEmpty(Vista.NombreAlmacen) && !nombreRepetido;

        if (nombreRepetido)
            CentroNotificaciones.Mostrar("Ye existe un almacén con el mismo nombre registrado en el sistema, los nombres de almacenes deben ser únicos.", TipoNotificacion.Advertencia);
        if (!nombreOk)
            CentroNotificaciones.Mostrar("El campo de nombre es obligatorio para el almacén, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);

        return nombreOk;
    }
}