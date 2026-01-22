using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;
using aDVanceERP.Modulos.RecursosHumanos.Vistas;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorGestionClientes : PresentadorVistaGestion<PresentadorTuplaCliente, IVistaGestionClientes, IVistaTuplaCliente, Cliente, RepoCliente, FiltroBusquedaCliente> {
    public PresentadorGestionClientes(IVistaGestionClientes vista) : base(vista) {
        RegistrarEntidad += OnRegistrarCliente;
        EditarEntidad += OnEditarCliente;

        AgregadorEventos.Suscribir("MostrarVistaGestionClientes", OnMostrarVistaGestionClientes);
        AgregadorEventos.Suscribir("ActivarDesactivarCliente", OnActivarDesactivarCliente);
    }

    private void OnRegistrarCliente(object? sender, EventArgs e) {
        AgregadorEventos.Publicar("MostrarVistaRegistroCliente", string.Empty);
    }

    private void OnEditarCliente(object? sender, Cliente e) {
        AgregadorEventos.Publicar("MostrarVistaEdicionCliente", AgregadorEventos.SerializarPayload(e));
    }

    private void OnMostrarVistaGestionClientes(string obj) {
        Vista.CargarFiltrosBusqueda(UtilesBusquedaCliente.FiltroBusquedaCliente);
        Vista.Restaurar();
        Vista.Mostrar();

        ActualizarResultadosBusqueda();
    }

    private void OnActivarDesactivarCliente(string obj) {
        var idClienteSeleccionado = _tuplasEntidades.FirstOrDefault(t => t.EstadoSeleccion)?.Vista.Id ?? 0;

        if (idClienteSeleccionado != 0) {
            var estado = RepoCliente.Instancia.HabilitarDeshabilitarCliente(idClienteSeleccionado);

            ActualizarResultadosBusqueda();

            CentroNotificaciones.Mostrar($"El cliente ha sido {(estado ? "activado" : "desactivado")} satisfactoriamente.", TipoNotificacion.Info);
        }
    }

    protected override PresentadorTuplaCliente ObtenerValoresTupla(Cliente entidad, List<IEntidadBaseDatos> entidadesExtra) {
        var presentadorTupla = new PresentadorTuplaCliente(new VistaTuplaCliente(), entidad);
        var persona = entidadesExtra.Count > 0 ? entidadesExtra.FirstOrDefault() as Persona : null!;
        var telefonos = RepoTelefonoContacto.Instancia.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, persona?.Id.ToString() ?? "0").resultadosBusqueda.Select(t => t.entidadBase);

        presentadorTupla.Vista.Id = entidad.Id;
        presentadorTupla.Vista.CodigoCliente = entidad.CodigoCliente;
        presentadorTupla.Vista.NombreCompleto = persona?.NombreCompleto ?? "N/A";
        presentadorTupla.Vista.Telefonos = string.Concat(telefonos.Select(t => $"{t.PrefijoPais} {t.NumeroTelefono}, ")).TrimEnd(',', ' ');
        presentadorTupla.Vista.Direccion = persona?.DireccionPrincipal ?? "N/A";
        presentadorTupla.Vista.FechaRegistro = entidad.FechaRegistro.ToString("yyyy-MM-dd");
        presentadorTupla.Vista.Activo = entidad.Activo;

        return presentadorTupla;
    }
}