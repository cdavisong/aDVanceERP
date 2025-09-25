using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto.Plantillas;

namespace aDVanceERP.Modulos.Inventario.MVP.Presentadores;

public class PresentadorRegistroProducto : PresentadorVistaRegistro<IVistaRegistroProducto, Producto, RepoProducto,
    FiltroBusquedaProducto> {
    public PresentadorRegistroProducto(IVistaRegistroProducto vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Producto objeto) {
        Vista.ModoEdicion = true;
        Vista.CategoriaProducto = objeto.Categoria;
        Vista.NombreProducto = objeto.Nombre ?? string.Empty;
        Vista.Codigo = objeto.Codigo ?? string.Empty;
        Vista.RazonSocialProveedor = UtilesProveedor.ObtenerRazonSocialProveedor(objeto.IdProveedor) ?? string.Empty;
        Vista.EsVendible = objeto.EsVendible;
        Vista.TipoMateriaPrima = UtilesTipoMateriaPrima.ObtenerNombreTipoMateriaPrima(objeto.IdTipoMateriaPrima) ?? string.Empty;

        using (var datos = new RepoDetalleProducto()) {
            var detalleProducto = datos.Buscar(FiltroBusquedaDetalleProducto.Id, objeto.IdDetalleProducto.ToString()).resultados.FirstOrDefault();

            if (detalleProducto != null) {
                Vista.UnidadMedida = UtilesUnidadMedida.ObtenerNombreUnidadMedida(detalleProducto.IdUnidadMedida) ?? string.Empty;
                Vista.Descripcion = detalleProducto.Descripcion ?? "No hay una descripción disponible para el producto actual";
            }
        }

        Vista.CostoProduccionUnitario = objeto.CostoProduccionUnitario;
        Vista.PrecioCompra = objeto.PrecioCompra;
        Vista.PrecioVentaBase = objeto.PrecioVentaBase;
        Vista.ModoEdicion = true;

        _entidad = objeto;
    }

    protected override bool EntidadCorrecta() {
        var nombreRepetido = !Vista.ModoEdicion && UtilesProducto.ObtenerIdProducto(Vista.NombreProducto).Result > 0;
        var nombreOk = !string.IsNullOrEmpty(Vista.NombreProducto) && !nombreRepetido;
        var codigoOk = !string.IsNullOrEmpty(Vista.Codigo);
        var unidadMedidaOk = !string.IsNullOrEmpty(Vista.UnidadMedida);

        if (nombreRepetido)
            CentroNotificaciones.Mostrar("Ye existe un producto con el mismo nombre registrado en el sistema, los nombres de productos deben ser únicos.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
        if (!nombreOk)
            CentroNotificaciones.Mostrar("El campo de nombre es obligatorio para el producto, por favor, corrija los datos entrados", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
        if (!codigoOk)
            CentroNotificaciones.Mostrar("El campo de código es obligatorio para el producto, por favor, corrija los datos entrados", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
        if (!unidadMedidaOk)
            CentroNotificaciones.Mostrar("El campo de unidad de medida es obligatorio para el producto, por favor, corrija los datos entrados", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);

        return nombreOk && codigoOk && unidadMedidaOk;
    }

    protected override void RegistroAuxiliar(RepoProducto datosProducto, long id) {
        var detalleProducto = new DetalleProducto(Entidad?.IdDetalleProducto ?? 0,
            UtilesUnidadMedida.ObtenerIdUnidadMedida(Vista.UnidadMedida).Result,
            Vista.Descripcion ?? "No hay una descripción disponible para el producto actual"
        );

        // Registrar detalles del producto
        using (var datos = new RepoDetalleProducto()) {
            if (Vista.ModoEdicion && Entidad?.IdDetalleProducto != 0)
                datos.Editar(detalleProducto);
            else if (Entidad?.IdDetalleProducto != 0)
                datos.Editar(detalleProducto);
            else {
                // Editar producto para modificar Id de los detalles
                Entidad.IdDetalleProducto = datos.Adicionar(detalleProducto);
                datosProducto.Editar(Entidad);

                // Cantidad inicial del producto
                RepoInventario.Instancia.ModificarInventario(
                    Entidad.Nombre,
                    string.Empty,
                    Vista.NombreAlmacen,
                    Vista.CantidadInicial
                );
            }
        }
    }

    protected override Producto? ObtenerEntidadDesdeVista() {
        return new Producto(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.CategoriaProducto,
            Vista.NombreProducto,
            Vista.Codigo,
            Entidad?.IdDetalleProducto ?? 0,
            UtilesProveedor.ObtenerIdProveedor(Vista.RazonSocialProveedor).Result,
            UtilesTipoMateriaPrima.ObtenerIdTipoMateriaPrima(Vista.TipoMateriaPrima).Result,
            Vista.EsVendible,
            Vista.PrecioCompra,
            Vista.CostoProduccionUnitario,
            Vista.PrecioVentaBase
        );
    }
}