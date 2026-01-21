using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorRegistroMensajero : PresentadorVistaRegistro<IVistaRegistroMensajero, Mensajero, RepoMensajero, FiltroBusquedaMensajero> {
    public PresentadorRegistroMensajero(IVistaRegistroMensajero vista) : base(vista) {
        AgregadorEventos.Suscribir("MostrarVistaRegistroMensajero", OnMostrarVistaRegistroMensajero);
        AgregadorEventos.Suscribir("MostrarVistaEdicionMensajero", OnMostrarVistaEdicionMensajero);
    }

    private void OnMostrarVistaRegistroMensajero(string obj) {
        Vista.ModoEdicion = false;
        Vista.Restaurar();

        // Carga inicial de datos
        Vista.CargarNombresPersonas(RepoPersona.Instancia.NombresPersonasNoMensajeros());

        Vista.Mostrar();
    }

    private void OnMostrarVistaEdicionMensajero(string obj) {
        Vista.ModoEdicion = true;
        Vista.Restaurar();

        if (string.IsNullOrEmpty(obj))
            return;

        var mensajero = AgregadorEventos.DeserializarPayload<Mensajero>(obj);

        if (mensajero == null)
            return;

        // Carga inicial de datos
        Vista.CargarNombresPersonas(RepoPersona.Instancia.NombresPersonasNoMensajeros());

        PopularVistaDesdeEntidad(mensajero);

        Vista.Mostrar();
    }

    public override void PopularVistaDesdeEntidad(Mensajero entidad) {
        base.PopularVistaDesdeEntidad(entidad);

        var persona = RepoPersona.Instancia.ObtenerPorId(entidad.IdPersona);

        if (persona != null) {
            Vista.NombreCompleto = persona.NombreCompleto;
            Vista.TipoDocumento = persona.TipoDocumento;
            Vista.NumeroDocumento = persona.NumeroDocumento;
            Vista.FechaRegistro = persona.FechaRegistro;
            Vista.Telefono = RepoTelefonoContacto.Instancia.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, persona.Id.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;
        }

        Vista.CodigoMensajero = entidad.CodigoMensajero;
        Vista.MatriculaVehiculo = entidad.MatriculaVehiculo;
    }

    protected override Mensajero? ObtenerEntidadDesdeVista() {
        var persona = RepoPersona.Instancia.Buscar(FiltroBusquedaPersona.NombreCompleto, Vista.NombreCompleto).resultadosBusqueda.FirstOrDefault().entidadBase;

        return new Mensajero(
            id: 0,
            idPersona: persona?.Id ?? 0,
            codigoMensajero: Vista.CodigoMensajero,
            matriculaVehiculo: Vista.MatriculaVehiculo,
            activo: true
        );
    }

    protected override void RegistroEdicionAuxiliar(RepoMensajero repositorio, long id) {
        // Persona
        var repoPersona = RepoPersona.Instancia;
        var personaBd = repoPersona.Buscar(FiltroBusquedaPersona.NombreCompleto, Vista.NombreCompleto).resultadosBusqueda.FirstOrDefault().entidadBase;
        var personaVista = new Persona(
            id: personaBd?.Id ?? 0,
            nombreCompleto: Vista.NombreCompleto,
            tipoDocumento: Vista.TipoDocumento,
            numeroDocumento: Vista.NumeroDocumento,
            direccionPrincipal: string.Empty,
            fechaRegistro: Vista.FechaRegistro,
            activo: true
        );

        if (!Vista.ModoEdicion && personaVista.Id == 0) {
            personaVista.Id = repoPersona.Adicionar(personaVista);

            _entidad!.IdPersona = personaVista.Id;

            repositorio.Editar(_entidad);
        } else repoPersona.Editar(personaVista);

        // Telefono
        var repoTelefonos = RepoTelefonoContacto.Instancia;
        var telefonoBd = repoTelefonos.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, personaVista.Id.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;
        var telefonoVista = new TelefonoContacto(
            id: telefonoBd?.Id ?? 0,
            prefijoPais: Vista.Telefono.PrefijoPais,
            numeroTelefono: Vista.Telefono.NumeroTelefono,
            categoria: Vista.Telefono.Categoria,
            idPersona: personaVista.Id
        );

        if (!Vista.ModoEdicion && telefonoVista.Id == 0) {
            repoTelefonos.Adicionar(telefonoVista);
        } else repoTelefonos.Editar(telefonoVista);
    }

    protected override bool EntidadCorrecta() {
        var idPersonaConNombreCoincidente = RepoPersona.Instancia.Buscar(FiltroBusquedaPersona.NombreCompleto, Vista.NombreCompleto).resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0;
        var mensajerosConNombreRepetido = RepoMensajero.Instancia.Buscar(FiltroBusquedaMensajero.IdPersona, idPersonaConNombreCoincidente.ToString()).cantidad;
        var nombreRepetido = !Vista.ModoEdicion && mensajerosConNombreRepetido > 0;
        var nombreOk = !string.IsNullOrEmpty(Vista.NombreCompleto) && !nombreRepetido;

        if (nombreRepetido)
            CentroNotificaciones.Mostrar("Ye existe un mensajero con el mismo nombre registrado en el sistema, los nombres de mensajeros deben ser únicos.", TipoNotificacion.Advertencia);
        if (!nombreOk)
            CentroNotificaciones.Mostrar("El campo de nombre es obligatorio para el mensajero, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);

        return nombreOk;
    }
}