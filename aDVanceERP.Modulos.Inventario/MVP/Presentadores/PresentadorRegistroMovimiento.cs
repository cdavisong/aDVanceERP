using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Movimiento.Plantillas;

namespace aDVanceERP.Modulos.Inventario.MVP.Presentadores;

public class PresentadorRegistroMovimiento : PresentadorVistaRegistro<IVistaRegistroMovimiento, Movimiento,
    RepoMovimiento, FiltroBusquedaMovimiento> {

    public PresentadorRegistroMovimiento(IVistaRegistroMovimiento vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Movimiento entidad) {
        var tipoMovimiento = RepoTipoMovimiento.Instancia.ObtenerPorId(entidad.IdTipoMovimiento);

        Vista.ModoEdicion = true;
        Vista.NombreProducto = UtilesProducto.ObtenerNombreProducto(entidad.IdProducto).Result ?? string.Empty;
        Vista.NombreAlmacenOrigen = UtilesAlmacen.ObtenerNombreAlmacen(entidad.IdAlmacenOrigen) ?? string.Empty;
        Vista.NombreAlmacenDestino = UtilesAlmacen.ObtenerNombreAlmacen(entidad.IdAlmacenDestino) ?? string.Empty;
        Vista.Fecha = entidad.Fecha;
        Vista.CantidadMovida = entidad.CantidadMovida;
        Vista.TipoMovimiento = tipoMovimiento?.Nombre ?? string.Empty;

        _entidad = entidad;
    }

    protected override bool EntidadCorrecta() {
        var nombreProductoOk = !string.IsNullOrEmpty(Vista.NombreProducto);
        var tipoMovimientoOk = !string.IsNullOrEmpty(Vista.TipoMovimiento);
        var noCompraventaOk = !(Vista.TipoMovimiento.Equals("Compra") || Vista.TipoMovimiento.Equals("Venta"));
        var tipoMovimiento = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, Vista.TipoMovimiento).resultados.FirstOrDefault(tm => tm.Nombre.Equals(Vista.TipoMovimiento));
        var transferenciaAlmacenesIguales = Vista.NombreAlmacenOrigen?.Equals(Vista?.NombreAlmacenDestino) ?? false;

        if (tipoMovimiento != null) {
            switch (tipoMovimiento.Efecto) {
                case EfectoMovimiento.Carga:
                    if (string.IsNullOrEmpty(Vista.NombreAlmacenDestino) || Vista.NombreAlmacenDestino.Equals("Ninguno")) {
                        CentroNotificaciones.Mostrar("Debe especificar un almacén de destino para la operación de carga solicitada", TipoNotificacion.Advertencia);
                        return false;
                    }
                    break;
                case EfectoMovimiento.Descarga:
                    if (string.IsNullOrEmpty(Vista.NombreAlmacenOrigen) || Vista.NombreAlmacenOrigen.Equals("Ninguno")) {
                        CentroNotificaciones.Mostrar("Debe especificar un almacén de origen para la operación de descarga solicitada", TipoNotificacion.Advertencia);
                        return false;
                    }
                    break;
                case EfectoMovimiento.Transferencia:
                    if (string.IsNullOrEmpty(Vista.NombreAlmacenOrigen) || string.IsNullOrEmpty(Vista.NombreAlmacenDestino) || Vista.NombreAlmacenOrigen.Equals("Ninguno") || Vista.NombreAlmacenDestino.Equals("Ninguno")) {
                        CentroNotificaciones.Mostrar("Debe especificar un almacén de origen y un destino para la operación de transferencia solicitada", TipoNotificacion.Advertencia);
                        return false;
                    }
                    if (transferenciaAlmacenesIguales) {
                        CentroNotificaciones.Mostrar("Error al especificar el origen o destino del producto, ambos almacenes deben tener distinta nomenclatura. Verifique los datos.", TipoNotificacion.Error);
                        return false;
                    }
                    break;
                default:
                    CentroNotificaciones.Mostrar("Efecto de movimiento desconocido", TipoNotificacion.Error);
                    return false;
            }
        }

        var cantidadOk = Vista.CantidadMovida > 0;

        if (tipoMovimiento?.Efecto == EfectoMovimiento.Descarga || tipoMovimiento?.Efecto == EfectoMovimiento.Transferencia) {
            if (!string.IsNullOrEmpty(Vista.NombreAlmacenOrigen)) {
                var cantidadInicialOrigen = UtilesProducto.ObtenerStockProducto(Vista.NombreProducto, Vista.NombreAlmacenOrigen).Result;

                if (cantidadInicialOrigen - Vista.CantidadMovida < 0) {
                    CentroNotificaciones.Mostrar($"No se puede mover una cantidad de productos hacia el destino menor que la cantidad orígen ({cantidadInicialOrigen} unidades) en el almacén {Vista.NombreAlmacenOrigen}", TipoNotificacion.Advertencia);
                    return false;
                }
            }
        }

        if (!nombreProductoOk)
            CentroNotificaciones.Mostrar("El campo de nombre para el producto es obligatorio para el producto, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);
        if (!tipoMovimientoOk)
            CentroNotificaciones.Mostrar("Debe especificar un tipo de movimiento válido para el movimiento de productos, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);
        if (!noCompraventaOk)
            CentroNotificaciones.Mostrar("Las operaciones de compraventa no están permitidas directamente desde la sección de movimientos de inventario. Para registrar compras o ventas diríjase al módulo correspondiente", TipoNotificacion.Advertencia);
        if (!cantidadOk)
            CentroNotificaciones.Mostrar("La cantidad de productos a mover en una operación de carga, descarga o transferencia debe ser mayor que 0, corrija los datos entrados", TipoNotificacion.Advertencia);


        return nombreProductoOk && tipoMovimientoOk && noCompraventaOk && cantidadOk;
    }

    protected override void RegistroAuxiliar(RepoMovimiento repoMovimiento, long id) {
        if (Entidad != null)
            RepoInventario.Instancia.ModificarInventario(
                Vista.NombreProducto,
                Vista.NombreAlmacenOrigen,
                Vista.NombreAlmacenDestino,
                Vista.CantidadMovida
            );
    }

    protected override Movimiento? ObtenerEntidadDesdeVista() {
        var producto = RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, Vista.NombreProducto).resultados.FirstOrDefault(p => p.Nombre.Equals(Vista.NombreProducto));
        var almacenOrigen = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenOrigen).resultados.FirstOrDefault(a => a.Nombre.Equals(Vista.NombreAlmacenOrigen));
        var almacenDestino = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenDestino).resultados.FirstOrDefault(a => a.Nombre.Equals(Vista.NombreAlmacenDestino));
        var inventario = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, producto.Id.ToString()).resultados.FirstOrDefault(i => i.IdAlmacen.Equals(almacenOrigen?.Id));
        var costoUnitario = producto.Categoria == CategoriaProducto.ProductoTerminado ? producto.CostoProduccionUnitario : producto.PrecioCompra;
        var tipoMovimiento = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, Vista.TipoMovimiento).resultados.FirstOrDefault(tm => tm.Nombre.Equals(Vista.TipoMovimiento));
        var saldoFinal = inventario?.Cantidad ?? 0 + (Vista.CantidadMovida * (tipoMovimiento?.Efecto == EfectoMovimiento.Carga ? 1 : -1));

        return new Movimiento(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            producto?.Id ?? throw  new ArgumentNullException("El producto especificado no es válido"),
            costoUnitario,
            costoUnitario * Vista.CantidadMovida,
            almacenOrigen?.Id ?? 0,
            almacenDestino?.Id ?? 0,
            Vista.Fecha,
            EstadoMovimiento.Pendiente,
            DateTime.MinValue,
            inventario?.Cantidad ?? 0,
            Vista.CantidadMovida,
            saldoFinal,
            tipoMovimiento?.Id ?? 0,
            UtilesCuentaUsuario.UsuarioAutenticado?.Id ?? 0
        );
    }
}