using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Inventario.Presentadores;
using aDVanceERP.Modulos.Inventario.Properties;
using aDVanceERP.Modulos.Inventario.Vistas;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Inventario; 

public sealed class ModuloInventario : ModuloExtensionBase {
    private Guna2CircleButton _btnAccesoModulo = new Guna2CircleButton();
    private PresentadorMenuInventario _menuInventario = null!;
    private PresentadorGestionProductos _productos = null!;
    private PresentadorRegistroProducto _registroProducto = null!;
    private PresentadorGestionMovimientos _movimientos = null!;
    private PresentadorRegistroMovimiento _registroMovimiento = null!;
    private PresentadorGestionAlmacenes _almacenes = null!;
    private PresentadorRegistroAlmacen _registroAlmacen = null!;

    public ModuloInventario() {
        Nombre = "MOD_INVENTARIO";
        Descripcion = "Proporciona funcionalidades de gestión de inventarios y productos.";
        Version = new Version(1, 0, 0, 0);
    }

    public override void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
        // Botón de acceso al módulo
        _btnAccesoModulo.Name = "btnAccesoModuloInventario";
        _btnAccesoModulo.ImageSize = new Size(24, 24);
        _btnAccesoModulo.CustomImages.ImageSize = new Size(24, 24);
        _btnAccesoModulo.CustomImages.Image = Resources.inventory_24px;
        _btnAccesoModulo.Click += delegate {
            AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
            AgregadorEventos.Publicar("MostrarVistaMenuInventario", string.Empty);
        };

        // Menu
        _menuInventario = new PresentadorMenuInventario(new VistaMenuInventario());

        // Contenedor de módulos
        // Productos
        _productos = new PresentadorGestionProductos(new VistaGestionProductos());
        _registroProducto = new PresentadorRegistroProducto(new VistaRegistroProducto());
        _registroProducto.EntidadRegistradaActualizada += (s, e) => _productos.ActualizarResultadosBusqueda();
        // Movimientos
        _movimientos = new PresentadorGestionMovimientos(new VistaGestionMovimientos());
        _registroMovimiento = new PresentadorRegistroMovimiento(new VistaRegistroMovimiento());
        _registroMovimiento.EntidadRegistradaActualizada += (s, e) => _movimientos.ActualizarResultadosBusqueda();
        // Almacenes
        _almacenes = new PresentadorGestionAlmacenes(new VistaGestionAlmacenes());
        _registroAlmacen = new PresentadorRegistroAlmacen(new VistaRegistroAlmacen());
        _registroAlmacen.EntidadRegistradaActualizada += (s, e) => _almacenes.ActualizarResultadosBusqueda();

        base.Inicializar(principal);
    }

    protected override void InicializarVistas() {
        // Agregar botón de acceso al módulo
        _principal.Modulos.AdicionarBotonAccesoModulo(_btnAccesoModulo);

        // Agregar menú del módulo
        _principal.Vista.BarraTitulo.Registrar(_menuInventario.Vista);

        // Contenedor de módulos
        // Productos
        _principal.Modulos.Vista.PanelCentral.Registrar(_productos.Vista);
        _principal.Modulos.Vista.PanelCentral.Registrar(_registroProducto.Vista);
        // Movimientos
        _principal.Modulos.Vista.PanelCentral.Registrar(_movimientos.Vista);
        _principal.Modulos.Vista.PanelCentral.Registrar(_registroMovimiento.Vista);
        // Almacenes
        _principal.Modulos.Vista.PanelCentral.Registrar(_almacenes.Vista);
        _principal.Modulos.Vista.PanelCentral.Registrar(_registroAlmacen.Vista);
    }

    public override void Apagar() {
        throw new NotImplementedException();
    }

    public static readonly string[] Permisos = {
        "MOD_INVENTARIO_TODOS",
        "MOD_INVENTARIO_PRODUCTOS_TODOS",
        "MOD_INVENTARIO_PRODUCTOS_ADICIONAR",
        "MOD_INVENTARIO_PRODUCTOS_EDITAR",
        "MOD_INVENTARIO_PRODUCTOS_ELIMINAR",
        "MOD_INVENTARIO_MOVIMIENTOS_TODOS",
        "MOD_INVENTARIO_MOVIMIENTOS_ADICIONAR",
        "MOD_INVENTARIO_MOVIMIENTOS_EDITAR",
        "MOD_INVENTARIO_MOVIMIENTOS_ELIMINAR",
        "MOD_INVENTARIO_ALMACENES_TODOS",
        "MOD_INVENTARIO_ALMACENES_ADICIONAR",
        "MOD_INVENTARIO_ALMACENES_EDITAR",
        "MOD_INVENTARIO_ALMACENES_ELIMINAR"
    };
}