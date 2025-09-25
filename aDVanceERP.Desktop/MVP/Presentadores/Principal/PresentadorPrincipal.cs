using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Extension.Controladores;
using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

using aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;
using aDVanceERP.Desktop.MVP.Presentadores.ContenedorSeguridad;
using aDVanceERP.Desktop.MVP.Vistas.Modulos;
using aDVanceERP.Desktop.MVP.Vistas.Principal;
using aDVanceERP.Desktop.MVP.Vistas.Seguridad;
using aDVanceERP.Desktop.Properties;
using aDVanceERP.Modulos.CompraVenta;
using aDVanceERP.Modulos.Contactos;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.Finanzas;
using aDVanceERP.Modulos.Inventario;
using aDVanceERP.Modulos.Taller;

using System.Diagnostics;

namespace aDVanceERP.Desktop.MVP.Presentadores.Principal;

public partial class PresentadorPrincipal : IPresentadorVistaPrincipal<IVistaPrincipal> {
    private readonly GestorModulosExtensibles _gestorModulos = new GestorModulosExtensibles();
    private Empresa? _empresa;

    public PresentadorPrincipal() {
        Vista = new VistaPrincipal();
        Seguridad = new PresentadorSeguridad(Vista, new VistaSeguridad());
        Modulos = new PresentadorModulos(Vista, new VistaModulos());

        // Adicionar vistas al panel central
        Vista.PanelCentral.Registrar(Seguridad.Vista);
        Vista.PanelCentral.Registrar(Modulos.Vista);

        // Eventos de la vista principal
        ((Form) Vista).Shown += OnVistaPrincipalMostrada;
        Vista.VerMenuUsuario += OnVerMenuUsuario;

        // Eventos de seguridad
        AgregadorEventos.Subscribir("EventoUsuarioAutenticado", OnUsuarioAutenticado);
        AgregadorEventos.Subscribir("EventoSesionCerrada", OnSesionCerrada);


        #region Menu de usuario

        InicializarVistaMenuUsuario();

        #endregion

        #region Modulos

        Modulos.Vista.CambioModulo += delegate { Vista.BarraTitulo.OcultarTodos(); };

        #endregion

        #region Seguridad de los módulos en la aplicación

        InicializarPermisosModulos();

        #endregion
    }

    public IVistaPrincipal Vista { get; }

    public IPresentadorVistaSeguridad<IVistaSeguridad> Seguridad { get; }

    public IPresentadorVistaModulos<IVistaModulos> Modulos { get; }

    public long IdEmpresa {
        get => _empresa?.Id ?? 0;
        set {
            if (_empresa != null)
                _empresa.Id = value;
        }
    }

    private void CargarModulosExtension() {
        _gestorModulos.CargarModulos(this);
    }

    private void OnVistaPrincipalMostrada(object? sender, EventArgs e) {
        // Verificar actualizaciones
        if (File.Exists(@".\Actualizador.exe")) {
            // Ejecutar el instalador
            Process ActualizadorProcess = Process.Start(@".\Actualizador.exe");

            // Esperar a que el instalador termine
            ActualizadorProcess.WaitForExit();

            // Verificar si la instalación fue exitosa
            if (ActualizadorProcess.ExitCode != 0)
                CentroNotificaciones.Mostrar($"El actualizador de la aplicación falló con código de error: {ActualizadorProcess.ExitCode}", TipoNotificacion.Error);
        }

        Vista.BarraTitulo.OcultarTodos();
        Vista.ModificarVisibilidadBotonesBarraTitulo(false);
        Vista.PanelCentral.Mostrar(nameof(VistaSeguridad));
        Vista.BarraEstado.OcultarTodos();
    }

    private void OnUsuarioAutenticado(string obj) {
        // Verificar el registro de la empresa y mostrar la vista de Login
        using (var datosEmpresa = new RepoEmpresa()) {
            if (datosEmpresa.Cantidad() == 0)
                MostrarVistaRegistroEmpresa(this, EventArgs.Empty);
            else _isRegistroEmpresa = true;
        }

        if (_isRegistroEmpresa) {
            Vista.ModificarVisibilidadBotonesBarraTitulo(true);
            Vista.PanelCentral.Ocultar(nameof(VistaSeguridad));
            Vista.PanelCentral.Restaurar(nameof(VistaModulos));
            Vista.PanelCentral.Mostrar(nameof(VistaModulos));

            if (_menuUsuario != null)
                _menuUsuario.Vista.NombreUsuario = UtilesCuentaUsuario.UsuarioAutenticado?.Nombre;

            ActualizarRepoEmpresa();
        } else return;

        Modulos.Vista.MensajePortada = Resources.MensajePortada
            .Replace("[version]", $"{Program.Version}-beta")
            .Replace("[user]", UtilesCuentaUsuario.UsuarioAutenticado?.Nombre ?? "invitado");
    }

    private void OnSesionCerrada(string obj) {
        Vista.ModificarVisibilidadBotonesBarraTitulo(false);
        Vista.PanelCentral.Ocultar(nameof(VistaModulos));
        Vista.PanelCentral.Restaurar(nameof(VistaSeguridad));
        Vista.PanelCentral.Mostrar(nameof(VistaSeguridad));
    }


    private void InicializarPermisosModulos() {
        try {
            UtilesSeguridadModulosAplicacion.InicializarPermisosModulo(ModuloContactos.Nombre, ModuloContactos.Permisos);
            UtilesSeguridadModulosAplicacion.InicializarPermisosModulo(ModuloFinanzas.Nombre, ModuloFinanzas.Permisos);
            UtilesSeguridadModulosAplicacion.InicializarPermisosModulo(ModuloInventario.Nombre, ModuloInventario.Permisos);
            UtilesSeguridadModulosAplicacion.InicializarPermisosModulo(ModuloTaller.Nombre, ModuloTaller.Permisos);
            UtilesSeguridadModulosAplicacion.InicializarPermisosModulo(ModuloCompraventa.Nombre, ModuloCompraventa.Permisos);
        } catch (ExcepcionConexionServidorMySQL e) {
            CentroNotificaciones.Mostrar(e.Message, TipoNotificacion.Error);
        }
    }

    private void ActualizarRepoEmpresa() {
        using (var datosEmpresa = new RepoEmpresa()) {
            _empresa = datosEmpresa.Buscar(FiltroBusquedaEmpresa.Todos, string.Empty).resultados.FirstOrDefault();

            if (_menuUsuario != null && _empresa != null) {
                _menuUsuario.Vista.LogotipoEmpresa = _empresa.Logotipo;
                _menuUsuario.Vista.NombreEmpresa = _empresa.Nombre;
                _menuUsuario.Vista.CorreoElectronico = UtilesContacto.ObtenerCorreoElectronicoContacto(_empresa.IdContacto);

                // Actualizar el id de la empresa
                IdEmpresa = _menuUsuario.Vista.IdEmpresa;
            }
        }
    }

    public void Dispose() {
        Vista.Dispose();
        Seguridad.Dispose();
        Modulos.Dispose();
    }
}