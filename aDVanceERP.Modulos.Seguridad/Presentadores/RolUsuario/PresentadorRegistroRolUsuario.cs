﻿using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Modulos.Seguridad.Interfaces;
using aDVanceERP.Modulos.Seguridad.Presentadores.Permiso;
using aDVanceERP.Modulos.Seguridad.Vistas.Permiso;

namespace aDVanceERP.Modulos.Seguridad.Presentadores.RolUsuario;

public class PresentadorRegistroRolUsuario : PresentadorVistaRegistro<IVistaRegistroRolUsuario, Core.Modelos.Modulos.Seguridad.RolUsuario, RepoRolUsuario, FiltroBusquedaRolUsuario> {
    private List<PresentadorTuplaPermiso> TuplasPermisosUsuario => new();

    public PresentadorRegistroRolUsuario(IVistaRegistroRolUsuario vista) : base(vista) {
        vista.CambioModulo += OnCambioModulo;
        vista.RegistrarPermiso += OnRegistrarPermiso;

        AgregadorEventos.Suscribir("MostrarVistaRegistroRolUsuario", OnMostrarVistaRegistroRolUsuario);
        AgregadorEventos.Suscribir("MostrarVistaEdicionRolUsuario", OnMostrarVistaEdicionRolUsuario);
    }

    private void OnMostrarVistaRegistroRolUsuario(string obj) {
        Vista.Restaurar();
        Vista.Mostrar();
    }

    private void OnMostrarVistaEdicionRolUsuario(string obj) {
        if (string.IsNullOrEmpty(obj))
            return;

        var rolUsuario = AgregadorEventos.DeserializarPayload<Core.Modelos.Modulos.Seguridad.RolUsuario>(obj);

        if (rolUsuario == null)
            return;

        Vista.Restaurar();

        PopularVistaDesdeEntidad(rolUsuario);

        Vista.Mostrar();
    }

    private void OnCambioModulo(object? sender, string e) {
        var permisos = RepoPermiso.Instancia.ObtenerPorNombreModulo(e);

        Vista.NombresPermisos = permisos.Select(p => p.Nombre).ToArray();
    }

    private void OnRegistrarPermiso(object? sender, string e) {
        var permiso = RepoPermiso.Instancia.Buscar(FiltroBusquedaPermiso.Nombre, e).entidades.FirstOrDefault();

        if (permiso == null) {
            CentroNotificaciones.Mostrar($"No se encontró el permiso '{e}' en el módulo seleccionado.", TipoNotificacion.Advertencia);
            return;
        }

        if (Vista.Permisos.Any(p => p.Id == permiso.Id)) {
            CentroNotificaciones.Mostrar($"El permiso '{e}' ya ha sido agregado al rol de usuario.", TipoNotificacion.Advertencia);
            return;
        }

        Vista.Permisos.Add(permiso);

        // Actualizar la lista de permisos en la vista
        ActualizarPermisosEnVista();
    }

    private void OnEliminarPermiso(object? sender, Core.Modelos.Modulos.Seguridad.Permiso e) {
        Vista.Permisos.Remove(e);

        // Actualizar la lista de permisos en la vista
        ActualizarPermisosEnVista();
    }

    public override void PopularVistaDesdeEntidad(Core.Modelos.Modulos.Seguridad.RolUsuario entidad) {
        base.PopularVistaDesdeEntidad(entidad);

        // Cargar inicial de datos
        Vista.NombresModulos = RepoModulo.Instancia.ObtenerTodos().Select(m => m.Nombre).ToArray();

        Vista.NombreRolUsuario = entidad.Nombre;
        Vista.Permisos.Clear();
        Vista.Permisos.AddRange(RepoPermiso.Instancia.ObtenerPorIdRolUsuario(entidad.Id));

        // Actualizar la lista de permisos en la vista
        ActualizarPermisosEnVista();
    }

    protected override Core.Modelos.Modulos.Seguridad.RolUsuario? ObtenerEntidadDesdeVista() {
        return new Core.Modelos.Modulos.Seguridad.RolUsuario(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreRolUsuario
        );
    }

    protected override void RegistroEdicionAuxiliar(RepoRolUsuario repositorio, long id) {
        base.RegistroEdicionAuxiliar(repositorio, id);

        // Eliminar todos los permisos actuales del rol de usuario
        RepoRolPermisoUsuario.Instancia.Eliminar(id);

        // Agregar los permisos seleccionados al rol de usuario
        foreach (var permiso in Vista.Permisos) {
            var rolPermiso = new RolPermisoUsuario(0, id, permiso.Id);

            RepoRolPermisoUsuario.Instancia.Adicionar(rolPermiso);
        }
    }

    private void ActualizarPermisosEnVista() {
        if (!Vista.Habilitada)
            return;

        try {
            Vista.PanelCentral.CerrarTodos();

            // Desuscribir eventos del presentador de tuplas
            foreach (var tuplaPermiso in TuplasPermisosUsuario) {
                tuplaPermiso.EliminarEntidad -= OnEliminarPermiso;
                tuplaPermiso.Dispose();
            }

            VariablesGlobales.CoordenadaYUltimaTupla = 0;

            for (var i = 0; i < Vista.Permisos.Count; i++) {
                var permiso = Vista.Permisos[i];
                var tuplaPermiso = new PresentadorTuplaPermiso(new VistaTuplaPermiso() {
                    Id = permiso.Id.ToString(),
                    NombrePermiso = permiso.Nombre
                }, permiso);

                (Vista as Control)?.Invoke(() => {
                    AdicionarTuplaPermiso(tuplaPermiso);
                });

                Application.DoEvents();
            }
        } catch (Exception ex) {
            CentroNotificaciones.Mostrar($"Error al refrescar la lista de permisos de usuario: {ex.Message}", TipoNotificacion.Error);
        }
    }

    private void AdicionarTuplaPermiso(PresentadorTuplaPermiso tuplaPermiso) {
        (Vista as Control)?.Invoke(() => {
            tuplaPermiso.EliminarEntidad += OnEliminarPermiso;

            TuplasPermisosUsuario.Add(tuplaPermiso);

            Vista.PanelCentral.Registrar(
                tuplaPermiso.Vista,
                new Point(0, VariablesGlobales.CoordenadaYUltimaTupla),
                new Size(Vista.PanelCentral.Dimensiones.Width - 20, VariablesGlobales.AlturaTuplaPredeterminada),
                TipoRedimensionadoVista.Ninguno);

            tuplaPermiso.Vista.Mostrar();
        });

        VariablesGlobales.CoordenadaYUltimaTupla += VariablesGlobales.AlturaTuplaPredeterminada;
    }

    public override void Dispose() {
        Vista.CambioModulo -= OnCambioModulo;
        Vista.RegistrarPermiso -= OnRegistrarPermiso;

        AgregadorEventos.Desuscribir("MostrarVistaRegistroRolUsuario", OnMostrarVistaRegistroRolUsuario);
        AgregadorEventos.Desuscribir("MostrarVistaEdicionRolUsuario", OnMostrarVistaEdicionRolUsuario);

        base.Dispose();
    }
}