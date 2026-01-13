using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Contactos;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores.Producto;

public class PresentadorRegistroProducto : PresentadorVistaRegistro<IVistaRegistroProducto, Core.Modelos.Modulos.Inventario.Producto, RepoProducto,
    FiltroBusquedaProducto> {
    public PresentadorRegistroProducto(IVistaRegistroProducto vista) : base(vista) {
        AgregadorEventos.Suscribir("MostrarVistaRegistroProducto", OnMostrarVistaRegistroProducto);
        AgregadorEventos.Suscribir("MostrarVistaEdicionProducto", OnMostrarVistaEdicionProducto);
    }

    private void OnMostrarVistaRegistroProducto(string obj) {
        Vista.ModoEdicion = false;

        // Carga inicial de datos
        Vista.CargarNombresProveedores(RepoProveedor.Instancia.ObtenerTodos().Select(p => p.RazonSocial).ToArray());
        Vista.CargarUnidadesMedida(RepoUnidadMedida.Instancia.ObtenerTodos().ToArray());
        Vista.CargarNombresClasificaciones(RepoClasificacionProducto.Instancia.ObtenerTodos().Select(c => c.Nombre).ToArray());
        Vista.CargarNombresAlmacenes(RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.Nombre).ToArray());

        Vista.Restaurar();
        Vista.Mostrar();
    }

    private void OnMostrarVistaEdicionProducto(string obj) {
        Vista.ModoEdicion = true;

        if (string.IsNullOrEmpty(obj))
            return;

        var producto = AgregadorEventos.DeserializarPayload<Core.Modelos.Modulos.Inventario.Producto>(obj);

        if (producto == null)
            return;

        // Carga inicial de datos
        Vista.CargarNombresProveedores(RepoProveedor.Instancia.ObtenerTodos().Select(p => p.RazonSocial).ToArray());
        Vista.CargarUnidadesMedida(RepoUnidadMedida.Instancia.ObtenerTodos().ToArray());
        Vista.CargarNombresClasificaciones(RepoClasificacionProducto.Instancia.ObtenerTodos().Select(c => c.Nombre).ToArray());
        Vista.CargarNombresAlmacenes(RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.Nombre).ToArray());

        Vista.Restaurar();

        PopularVistaDesdeEntidad(producto);

        Vista.Mostrar();
    }

    public override void PopularVistaDesdeEntidad(Core.Modelos.Modulos.Inventario.Producto objeto) {
        base.PopularVistaDesdeEntidad(objeto);

        // Variables auxiliares
        var proveedor = RepoProveedor.Instancia.ObtenerPorId(objeto.IdProveedor);
        var unidadMedida = RepoUnidadMedida.Instancia.ObtenerPorId(objeto.IdUnidadMedida);

        Vista.Categoria = objeto.Categoria;
        Vista.Nombre = objeto.Nombre;
        Vista.Codigo = objeto.Codigo;
        Vista.Descripcion = objeto.Descripcion;
        Vista.NombreProveedor = proveedor?.RazonSocial ?? string.Empty;
        Vista.NombreUnidadMedida = unidadMedida?.Nombre ?? string.Empty;
        Vista.EsVendible = objeto.EsVendible;
        Vista.CostoUnitario = objeto.Categoria == CategoriaProducto.Mercancia || objeto.Categoria == CategoriaProducto.MateriaPrima
                ? objeto.CostoAdquisicionUnitario
                : objeto.Categoria == CategoriaProducto.ProductoTerminado
                    ? objeto.CostoProduccionUnitario
                    : 0m;
        Vista.ImpuestoVentaPorcentaje = objeto.ImpuestoVentaPorcentaje;
        Vista.MargenGananciaDeseado = objeto.MargenGananciaDeseado;
        Vista.PrecioVentaBase = objeto.PrecioVentaBase;
    }

    protected override Core.Modelos.Modulos.Inventario.Producto? ObtenerEntidadDesdeVista() {
        var proveedor = RepoProveedor.Instancia.Buscar(Core.Modelos.Modulos.Contactos.FiltroBusquedaProveedor.RazonSocial, Vista.NombreProveedor).entidades.FirstOrDefault();
        var unidadMedida = RepoUnidadMedida.Instancia.Buscar(FiltroBusquedaUnidadMedida.Nombre, Vista.NombreUnidadMedida).entidades.FirstOrDefault();

        return new Core.Modelos.Modulos.Inventario.Producto {
            Id = _entidad?.Id ?? 0,
            Categoria = Vista.Categoria,
            Nombre = Vista.Nombre,
            Codigo = Vista.Codigo,
            Descripcion = Vista.Descripcion,
            IdProveedor = proveedor?.Id ?? 0,
            IdUnidadMedida = unidadMedida?.Id ?? 0,
            EsVendible = Vista.EsVendible,
            CostoAdquisicionUnitario = Vista.CostoAdquisicionUnitario,
            CostoProduccionUnitario = Vista.CostoProduccionUnitario,
            ImpuestoVentaPorcentaje = Vista.ImpuestoVentaPorcentaje,
            MargenGananciaDeseado = Vista.MargenGananciaDeseado,
            PrecioVentaBase = Vista.PrecioVentaBase,
            Activo = true
        };
    }

    protected override bool EntidadCorrecta() {
        var nombreRepetido = !Vista.ModoEdicion && UtilesProducto.ObtenerIdProducto(Vista.Nombre).Result > 0;
        var nombreOk = !string.IsNullOrEmpty(Vista.Nombre) && !nombreRepetido;
        var codigoOk = !string.IsNullOrEmpty(Vista.Codigo);
        var unidadMedidaOk = !string.IsNullOrEmpty(Vista.NombreUnidadMedida);

        if (nombreRepetido)
            CentroNotificaciones.Mostrar("Ye existe un producto con el mismo nombre registrado en el sistema, los nombres de productos deben ser únicos.", TipoNotificacion.Advertencia);
        if (!nombreOk)
            CentroNotificaciones.Mostrar("El campo de nombre es obligatorio para el producto, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);
        if (!codigoOk)
            CentroNotificaciones.Mostrar("El campo de código es obligatorio para el producto, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);
        if (!unidadMedidaOk)
            CentroNotificaciones.Mostrar("El campo de unidad de medida es obligatorio para el producto, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);

        return nombreOk && codigoOk && unidadMedidaOk;
    }    
}