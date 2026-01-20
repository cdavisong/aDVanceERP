using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.RecursosHumanos.Presentadores;
using aDVanceERP.Modulos.RecursosHumanos.Properties;
using aDVanceERP.Modulos.RecursosHumanos.Vistas;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.RecursosHumanos; 

public sealed class ModuloRecursosHumanos : ModuloExtensionBase {
    private Guna2CircleButton _btnAccesoModulo = new Guna2CircleButton();
    private PresentadorMenuRecursosHumanos _menuRecursosHumanos = null!;
    private PresentadorGestionEmpleados _empleados = null!;
    private PresentadorGestionProveedores _proveedores = null!;
    private PresentadorGestionClientes _clientes = null!;
    private PresentadorGestionMensajeros _mensajeros = null!;
    private PresentadorGestionPersonas _personas = null!;
    private PresentadorRegistroPersona _registroPersona = null!;

    public ModuloRecursosHumanos() {
        Nombre = "MOD_RRHH";
        Descripcion = "Proporciona funcionalidades de gestión de recursos humanos.";
        Version = new Version(1, 0, 0, 0);
    }

    public override void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
        // Boton de acceso al módulo
        _btnAccesoModulo.Name = "btnModuloRecursosHumanos";
        _btnAccesoModulo.ImageSize = new Size(24, 24);
        _btnAccesoModulo.CustomImages.ImageSize = new Size(24, 24);
        _btnAccesoModulo.Image = Resources.businessmanB_24px;
        _btnAccesoModulo.Click += delegate {
            AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
            AgregadorEventos.Publicar("MostrarVistaMenuRecursosHumanos", string.Empty);
        };

        // Menu
        _menuRecursosHumanos = new PresentadorMenuRecursosHumanos(new VistaMenuRecursosHumanos());

        // Contenedor de módulos
        // Empleados
        _empleados = new PresentadorGestionEmpleados(new VistaGestionEmpleados());
        // Proveedores
        _proveedores = new PresentadorGestionProveedores(new VistaGestionProveedores());
        // Clientes
        _clientes = new PresentadorGestionClientes(new VistaGestionClientes());
        // Mensajeros
        _mensajeros = new PresentadorGestionMensajeros(new VistaGestionMensajeros());
        // Personas
        _personas = new PresentadorGestionPersonas(new VistaGestionPersonas());
        _registroPersona = new PresentadorRegistroPersona(new VistaRegistroPersona());
        _registroPersona.EntidadRegistradaActualizada += (s, e) => _personas.ActualizarResultadosBusqueda();

        base.Inicializar(principal);
    }

    protected override void InicializarVistas() {
        // Agregar botón de acceso al módulo
        _principal.Modulos.AdicionarBotonAccesoModulo(_btnAccesoModulo);

        // Agregar menú del módulo
        _principal.Vista.BarraTitulo.Registrar(_menuRecursosHumanos.Vista);

        // Contenedor de módulos
        // Empleados
        _principal.Modulos.Vista.PanelCentral.Registrar(_empleados.Vista);
        // Proveedores
        _principal.Modulos.Vista.PanelCentral.Registrar(_proveedores.Vista);
        // Clientes
        _principal.Modulos.Vista.PanelCentral.Registrar(_clientes.Vista);
        // Mensajeros
        _principal.Modulos.Vista.PanelCentral.Registrar(_mensajeros.Vista);
        // Personas
        _principal.Modulos.Vista.PanelCentral.Registrar(_personas.Vista);
        _principal.Modulos.Vista.PanelCentral.Registrar(_registroPersona.Vista);
    }

    public override void Apagar() {
        throw new NotImplementedException();
    }

    public static readonly string[] Permisos = {
        "MOD_RRHH_TODOS",
        "MOD_RRHH_EMPLEADOS_TODOS",
        "MOD_RRHH_EMPLEADOS_ADICIONAR",
        "MOD_RRHH_EMPLEADOS_EDITAR",
        "MOD_RRHH_EMPLEADOS_ELIMINAR",
        "MOD_RRHH_CLIENTES_TODOS",
        "MOD_RRHH_CLIENTES_ADICIONAR",
        "MOD_RRHH_CLIENTES_EDITAR",
        "MOD_RRHH_CLIENTES_ELIMINAR",
        "MOD_RRHH_PROVEEDORES_TODOS",
        "MOD_RRHH_PROVEEDORES_ADICIONAR",
        "MOD_RRHH_PROVEEDORES_EDITAR",
        "MOD_RRHH_PROVEEDORES_ELIMINAR",
        "MOD_RRHH_MENSAJEROS_TODOS",
        "MOD_RRHH_MENSAJEROS_ADICIONAR",
        "MOD_RRHH_MENSAJEROS_EDITAR",
        "MOD_RRHH_MENSAJEROS_ELIMINAR",
        "MOD_RRHH_PERSONAS_TODOS",
        "MOD_RRHH_PERSONAS_ADICIONAR",
        "MOD_RRHH_PERSONAS_EDITAR",
        "MOD_RRHH_PERSONAS_ELIMINAR"
    };
}