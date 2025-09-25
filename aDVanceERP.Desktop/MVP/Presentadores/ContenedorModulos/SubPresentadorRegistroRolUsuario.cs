using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Seguridad.MVP.Modelos;
using aDVanceERP.Core.Seguridad.MVP.Modelos.Repositorios;
using aDVanceERP.Core.Seguridad.MVP.Presentadores;
using aDVanceERP.Core.Seguridad.MVP.Vistas.RolUsuario;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.Utiles;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorRegistroRolUsuario? _registroRolUsuario;

    private List<string[]>? Permisos { get; set; } = new();

    private void InicializarVistaRegistroRolUsuario() {
        try {
            _registroRolUsuario = new PresentadorRegistroRolUsuario(new VistaRegistroRolUsuario());
            _registroRolUsuario.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
            _registroRolUsuario.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
            _registroRolUsuario.Vista.CargarNombresModulos(UtilesModulo.ObtenerNombresModulos());
            _registroRolUsuario.EntidadRegistradaActualizada += delegate {
                Permisos = _registroRolUsuario.Vista.Permisos;

                RegistrarEditarPermisosRol(UtilesRolUsuario.ObtenerIdRolUsuario(_registroRolUsuario.Vista.NombreRolUsuario!).Result);

                if (_gestionRolesUsuarios == null)
                    return;

                _gestionRolesUsuarios.ActualizarResultadosBusqueda();
            };

            Permisos?.Clear();
        } catch (ExcepcionConexionServidorMySQL e) {
            CentroNotificaciones.Mostrar(e.Message, TipoNotificacion.Error);
        }
    }

    private void MostrarVistaRegistroRolUsuario(object? sender, EventArgs e) {
        InicializarVistaRegistroRolUsuario();

        if (_registroRolUsuario == null)
            return;

        _registroRolUsuario.Vista.Mostrar();
        _registroRolUsuario.Dispose();
    }

    private void MostrarVistaEdicionRolUsuario(object? sender, EventArgs e) {
        InicializarVistaRegistroRolUsuario();

        if (sender is RolUsuario rolUsuario) {
            if (_registroRolUsuario != null) {
                _registroRolUsuario.PopularVistaDesdeEntidad(rolUsuario);
                _registroRolUsuario.Vista.Mostrar();
            }
        }

        _registroRolUsuario?.Dispose();
    }

    private void RegistrarEditarPermisosRol(long idRolUsuario = 0) {
        if (Permisos == null || Permisos.Count == 0)
            return;

        if (idRolUsuario == 0)
            idRolUsuario = UtilesBD.ObtenerUltimoIdTabla("rol_usuario");
        else {
            using (var datosPermisoRolUsuario = new RepoPermisoRolUsuario())
                datosPermisoRolUsuario.EliminarPorRol(idRolUsuario);

            UtilesRolUsuario.LimpiarCacheRol(idRolUsuario);
        }

        foreach (var permiso in Permisos) {
            var permisoRolUsuario = new PermisoRolUsuario(
                0,
                idRolUsuario,
                long.Parse(permiso[0])
            );

            using (var datosPermisoRolUsuario = new RepoPermisoRolUsuario())
                datosPermisoRolUsuario.Adicionar(permisoRolUsuario);
        }
    }
}