using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores;

public class PresentadorRegistroProducto : PresentadorVistaRegistro<IVistaRegistroProducto, Core.Modelos.Modulos.Inventario.Producto, RepoProducto, FiltroBusquedaProducto> {
    public PresentadorRegistroProducto(IVistaRegistroProducto vista) : base(vista) {
        AgregadorEventos.Suscribir("MostrarVistaRegistroProducto", OnMostrarVistaRegistroProducto);
        AgregadorEventos.Suscribir("MostrarVistaEdicionProducto", OnMostrarVistaEdicionProducto);
    }

    public string? NombreAlmacen { get; set; }

    private void OnMostrarVistaRegistroProducto(string obj) {
        Vista.ModoEdicion = false;
        Vista.Restaurar();

        // Carga inicial de datos
        Vista.CargarNombresProveedores(RepoProveedor.Instancia.Cantidad() > 0 ? [.. RepoProveedor.Instancia.ObtenerTodos().Select(p => p.entidadBase.RazonSocial)] : []);
        Vista.CargarUnidadesMedida([.. RepoUnidadMedida.Instancia.ObtenerTodos().Select(um => um.entidadBase)]);
        Vista.CargarClasificaciones([.. RepoClasificacionProducto.Instancia.ObtenerTodos().Select(c => c.entidadBase)]);
        Vista.CargarNombresAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.entidadBase.Nombre)]);

        Vista.Mostrar();
    }

    private void OnMostrarVistaEdicionProducto(string obj) {
        Vista.ModoEdicion = true;
        Vista.Restaurar();

        if (string.IsNullOrEmpty(obj))
            return;

        var datos = AgregadorEventos.DeserializarPayload<object[]>(obj);
        var datosExtra = datos != null ? AgregadorEventos.DeserializarPayload<object[]>(datos[1].ToString()) : null;
        var producto = datos != null ? AgregadorEventos.DeserializarPayload<Producto>(datos[0].ToString()) : null;

        if (producto == null)
            return;

        // Carga inicial de datos
        Vista.CargarNombresProveedores(RepoProveedor.Instancia.Cantidad() > 0 ? [.. RepoProveedor.Instancia.ObtenerTodos().Select(p => p.entidadBase.RazonSocial)] : []);
        Vista.CargarUnidadesMedida([.. RepoUnidadMedida.Instancia.ObtenerTodos().Select(um => um.entidadBase)]);
        Vista.CargarClasificaciones([.. RepoClasificacionProducto.Instancia.ObtenerTodos().Select(c => c.entidadBase)]);
        Vista.CargarNombresAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.entidadBase.Nombre)]);

        // Carga de datos extra
        if (datosExtra != null) {
            NombreAlmacen = datosExtra[0].ToString();
        }

        PopularVistaDesdeEntidad(producto);

        Vista.Mostrar();
    }

    public override void PopularVistaDesdeEntidad(Producto entidad) {
        base.PopularVistaDesdeEntidad(entidad);

        // Variables auxiliares
        var proveedor = RepoProveedor.Instancia.ObtenerPorId(entidad.IdProveedor);
        var unidadMedida = RepoUnidadMedida.Instancia.ObtenerPorId(entidad.IdUnidadMedida);
        var clasificacion = RepoClasificacionProducto.Instancia.ObtenerPorId(entidad.IdClasificacionProducto);

        Vista.Categoria = entidad.Categoria;
        Vista.NombreProducto = entidad.Nombre;
        Vista.Codigo = entidad.Codigo;
        Vista.Descripcion = entidad.Descripcion;
        Vista.NombreProveedor = proveedor?.RazonSocial ?? string.Empty;
        Vista.NombreUnidadMedida = unidadMedida?.Nombre ?? string.Empty;
        Vista.NombreClasificacionProducto = clasificacion?.Nombre ?? string.Empty;
        Vista.EsVendible = entidad.EsVendible;
        Vista.CostoUnitario = entidad.Categoria == CategoriaProducto.Mercancia || entidad.Categoria == CategoriaProducto.MateriaPrima
                ? entidad.CostoAdquisicionUnitario
                : entidad.Categoria == CategoriaProducto.ProductoTerminado
                    ? entidad.CostoProduccionUnitario
                    : 0m;
        Vista.ImpuestoVentaPorcentaje = entidad.ImpuestoVentaPorcentaje;
        Vista.MargenGananciaDeseado = entidad.MargenGananciaDeseado;
        Vista.PrecioVentaBase = entidad.PrecioVentaBase;
    }

    protected override Producto? ObtenerEntidadDesdeVista() {
        var proveedor = RepoProveedor.Instancia.Buscar(FiltroBusquedaProveedor.RazonSocial, Vista.NombreProveedor).resultadosBusqueda.FirstOrDefault().entidadBase;
        var unidadMedida = RepoUnidadMedida.Instancia.Buscar(FiltroBusquedaUnidadMedida.Nombre, Vista.NombreUnidadMedida).resultadosBusqueda.FirstOrDefault().entidadBase;

        return new Producto {
            Id = _entidad?.Id ?? 0,
            Categoria = Vista.Categoria,
            Nombre = Vista.NombreProducto,
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
        var productosConNombreRepetido = RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, Vista.NombreProducto).cantidad;
        var nombreRepetido = !Vista.ModoEdicion && productosConNombreRepetido > 0;
        var nombreOk = !string.IsNullOrEmpty(Vista.NombreProducto) && !nombreRepetido;
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

    protected override async void RegistroEdicionAuxiliar(RepoProducto repositorio, long id) {
        base.RegistroEdicionAuxiliar(repositorio, id);

        if (!Vista.ModoEdicion) {
            // Crear inventario inicial en el almacén seleccionado
            var almacen = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacen).resultadosBusqueda.FirstOrDefault().entidadBase;
            var inventario = new Core.Modelos.Modulos.Inventario.Inventario() {
                Id = 0,
                IdProducto = id,
                IdAlmacen = almacen?.Id ?? 0,
                Cantidad = Vista.CantidadInicial,
                CostoPromedio = Vista.CostoUnitario,
                ValorTotal = Vista.CostoUnitario * Vista.CantidadInicial,
                UltimaActualizacion = DateTime.Now
            };

            RepoInventario.Instancia.Adicionar(inventario);

            // Crear movimiento de inventario inicial
            var movimiento = new Movimiento() {
                Id = 0,
                IdProducto = id,
                CostoUnitario = Vista.CostoUnitario,
                IdAlmacenOrigen = 0,
                IdAlmacenDestino = almacen?.Id ?? 0,
                Estado = EstadoMovimiento.Completado,
                FechaCreacion = DateTime.Now,
                SaldoInicial = 0m,
                FechaTermino = DateTime.Now,
                CantidadMovida = Vista.CantidadInicial,
                SaldoFinal = Vista.CantidadInicial,
                IdTipoMovimiento = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Carga Inicial").resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0,
                IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                Notas = "Movimiento de inventario inicial al registrar el producto.",
            };

            // Adicionar a la base de datos local
            RepoMovimiento.Instancia.Adicionar(movimiento);
        }
    }
}