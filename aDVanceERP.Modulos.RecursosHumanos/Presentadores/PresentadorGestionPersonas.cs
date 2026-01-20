using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;
using aDVanceERP.Modulos.RecursosHumanos.Vistas;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorGestionPersonas : PresentadorVistaGestion<PresentadorTuplaPersona, IVistaGestionPersonas, IVistaTuplaPersona, Persona, RepoPersona, FiltroBusquedaPersona> {
    public PresentadorGestionPersonas(IVistaGestionPersonas vista) : base(vista) {
        RegistrarEntidad += OnRegistrarPersona;
        EditarEntidad += OnEditarPersona;

        AgregadorEventos.Suscribir("MostrarVistaGestionPersonas", OnMostrarVistaGestionPersonas);
    }

    private void OnRegistrarPersona(object? sender, EventArgs e) {
        AgregadorEventos.Publicar("MostrarVistaRegistroPersona", string.Empty);
    }

    private void OnEditarPersona(object? sender, Persona e) {
        AgregadorEventos.Publicar("MostrarVistaEdicionPersona", AgregadorEventos.SerializarPayload(e));
    }

    private void OnMostrarVistaGestionPersonas(string obj) {
        Vista.CargarFiltrosBusqueda(UtilesBusquedaPersona.FiltroBusquedaPersona);
        Vista.Restaurar();
        Vista.Mostrar();

        ActualizarResultadosBusqueda();
    }

    protected override PresentadorTuplaPersona ObtenerValoresTupla(Persona entidad, List<IEntidadBaseDatos> entidadesExtra) {
        var presentadorTupla = new PresentadorTuplaPersona(new VistaTuplaPersona(), entidad);
        var telefonos = RepoTelefonoContacto.Instancia.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, entidad.Id.ToString() ?? "0").resultadosBusqueda.Select(t => t.entidadBase);

        presentadorTupla.Vista.Id = entidad.Id;
        presentadorTupla.Vista.NumeroIdentidad = entidad.NumeroDocumento;
        presentadorTupla.Vista.NombreCompleto = entidad.NombreCompleto ?? "N/A";
        presentadorTupla.Vista.Telefonos = string.Concat(telefonos.Select(t => $"{t.PrefijoPais} {t.NumeroTelefono}, ")).TrimEnd(',', ' ');
        presentadorTupla.Vista.Direccion = entidad.DireccionPrincipal ?? "N/A";
        presentadorTupla.Vista.FechaRegistro = entidad.FechaRegistro.ToString("yyyy-MM-dd");
        presentadorTupla.Vista.Activo = entidad.Activo;

        return presentadorTupla;
    }
}