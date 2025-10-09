using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Inventario.Properties;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Inventario; 

public sealed class ModuloInventario : ModuloExtensionBase {
    private Guna2CircleButton _btnAccesoModulo = new Guna2CircleButton();

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

        base.Inicializar(principal);
    }

    protected override void InicializarVistas() {
        // Agregar botón de acceso al módulo
        _principal.Modulos.AdicionarBotonAccesoModulo(_btnAccesoModulo);
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