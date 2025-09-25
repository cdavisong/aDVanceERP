using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Inventario.MVP.Presentadores;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos; 

public partial class PresentadorModulos {
    private PresentadorRegistroProducto? _registroProducto;

    private void InicializarVistaRegistroProducto() {
        _registroProducto = new PresentadorRegistroProducto(new VistaRegistroProducto());
        _registroProducto.Vista.CargarNombresProductos(UtilesProducto.ObtenerNombresProductos().Result);
        _registroProducto.Vista.CargarRazonesSocialesProveedores(UtilesProveedor.ObtenerRazonesSocialesProveedores());
        _registroProducto.Vista.CargarUnidadesMedida(UtilesUnidadMedida.ObtenerUnidadesMedida());
        _registroProducto.Vista.CargarDescripcionesTiposMateriaPrima(UtilesTipoMateriaPrima.ObtenerDescripcionesTiposMateriaPrima());
        _registroProducto.Vista.CargarTiposMateriaPrima(UtilesTipoMateriaPrima.ObtenerNombresTiposMateriasPrimas());
        _registroProducto.Vista.CargarNombresAlmacenes(UtilesAlmacen.ObtenerNombresAlmacenes());
        _registroProducto.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroProducto.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroProducto.Vista.RegistrarUnidadMedida += MostrarVistaRegistroUnidadMedida;
        _registroProducto.Vista.RegistrarTipoMateriaPrima += MostrarVistaRegistroTipoMateriaPrima;
        _registroProducto.Vista.EliminarUnidadMedida += EliminarUnidadMedida;
        _registroProducto.Vista.EliminarTipoMateriaPrima += EliminarTipoMateriaPrima;
        _registroProducto.EntidadRegistradaActualizada += delegate {
            if (_gestionProductos == null)
                return;

            _gestionProductos?.ActualizarResultadosBusqueda();
        };
    }

    private void MostrarVistaRegistroProducto(object? sender, EventArgs e) {
        // Comprobar la existencia de al menos un almacén registrado.
        var existenAlmacenes = false;

        using (var datos = new RepoAlmacen())
            existenAlmacenes = datos.Cantidad() > 0;
        
        if (!existenAlmacenes) {
            CentroNotificaciones.Mostrar("No es posible registrar nuevos productos. Debe existir al menos un almacén registrado.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
            return;
        }

        InicializarVistaRegistroProducto();

        if (_registroProducto == null) 
            return;

        _registroProducto.Vista.Mostrar();
        _registroProducto.Dispose();
    }

    private void MostrarVistaEdicionProducto(object? sender, EventArgs e) {
        InicializarVistaRegistroProducto();

        if (sender is Producto producto) {
            if (_registroProducto != null) {
                _registroProducto.PopularVistaDesdeEntidad(producto);
                _registroProducto.Vista.Mostrar();
            }
        }

        _registroProducto?.Dispose();
    }
}