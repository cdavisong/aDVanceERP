using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorRegistroEmpleado : PresentadorVistaRegistro<IVistaRegistroEmpleado, Empleado, RepoEmpleado, FiltroBusquedaEmpleado> {
    public PresentadorRegistroEmpleado(IVistaRegistroEmpleado vista) : base(vista) {
        AgregadorEventos.Suscribir("MostrarVistaRegistroEmpleado", OnMostrarVistaRegistroEmpleado);
        AgregadorEventos.Suscribir("MostrarVistaEdicionEmpleado", OnMostrarVistaEdicionEmpleado);
    }

    private void OnMostrarVistaRegistroEmpleado(string obj) {
        Vista.ModoEdicion = false;
        Vista.Restaurar();

        // Carga inicial de datos
        Vista.CargarNombresPersonas(RepoPersona.Instancia.NombresPersonasNoEmpleados());

        Vista.Mostrar();
    }

    private void OnMostrarVistaEdicionEmpleado(string obj) {
        Vista.ModoEdicion = true;
        Vista.Restaurar();

        if (string.IsNullOrEmpty(obj))
            return;

        var proveedor = AgregadorEventos.DeserializarPayload<Empleado>(obj);

        if (proveedor == null)
            return;

        // Carga inicial de datos
        Vista.CargarNombresPersonas(RepoPersona.Instancia.NombresPersonasNoEmpleados());

        PopularVistaDesdeEntidad(proveedor);

        Vista.Mostrar();
    }

    public override void PopularVistaDesdeEntidad(Empleado entidad) {
        base.PopularVistaDesdeEntidad(entidad);

        var persona = RepoPersona.Instancia.ObtenerPorId(entidad.IdPersona);

        if (persona != null) {
            Vista.NombreCompleto = persona.NombreCompleto;
            Vista.TipoDocumento = persona.TipoDocumento;
            Vista.NumeroDocumento = persona.NumeroDocumento;
            Vista.DireccionPrincipal = persona.DireccionPrincipal;

            // Agregar teléfonos
            var telefonos = RepoTelefonoContacto.Instancia.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, persona.Id.ToString()).resultadosBusqueda.Select(t => t.entidadBase).ToList();
            telefonos.ForEach(t => { Vista.AgregarTelefono(t.Id, t.Categoria.ToString(), t.PrefijoPais, t.NumeroTelefono, t.IdPersona); });

            // Agregar direcciones correo
            var direccionesCorreo = RepoCorreoContacto.Instancia.Buscar(FiltroBusquedaCorreoContacto.IdPersona, persona.Id.ToString()).resultadosBusqueda.Select(t => t.entidadBase).ToList();
            direccionesCorreo.ForEach(c => { Vista.AgregarDireccionCorreo(c.Id, c.Categoria.ToString(), c.DireccionCorreo, c.IdPersona); });
        }

        Vista.FechaNacimiento = entidad.FechaNacimiento;
        Vista.CodigoEmpleado = entidad.CodigoEmpleado;
        Vista.FechaContratacion = entidad.FechaContratacion;
        Vista.Cargo = entidad.Cargo;
        Vista.Departamento = entidad.Departamento;
        Vista.Salario = entidad.Salario;
    }

    protected override Empleado? ObtenerEntidadDesdeVista() {
        var persona = RepoPersona.Instancia.Buscar(FiltroBusquedaPersona.NombreCompleto, Vista.NombreCompleto).resultadosBusqueda.FirstOrDefault().entidadBase;

        return new Empleado(
            id: 0,
            idPersona: persona?.Id ?? 0,
            codigoEmpleado: Vista.CodigoEmpleado,
            fechaContratacion: Vista.FechaContratacion,
            fechaNacimiento: Vista.FechaNacimiento,
            cargo: Vista.Cargo,
            departamento: Vista.Departamento,
            salario: Vista.Salario,
            true
        );
    }

    protected override void RegistroEdicionAuxiliar(RepoEmpleado repositorio, long id) {
        // Persona
        var repoPersona = RepoPersona.Instancia;
        var personaBd = repoPersona.Buscar(FiltroBusquedaPersona.NombreCompleto, Vista.NombreCompleto).resultadosBusqueda.FirstOrDefault().entidadBase;
        var personaVista = new Persona(
            id: personaBd?.Id ?? 0,
            nombreCompleto: Vista.NombreCompleto,
            tipoDocumento: Vista.TipoDocumento,
            numeroDocumento: Vista.NumeroDocumento,
            direccionPrincipal: Vista.DireccionPrincipal,
            fechaRegistro: personaBd?.FechaRegistro ?? Vista.FechaContratacion,
            activo: true
        );

        if (!Vista.ModoEdicion && personaVista.Id == 0) {
            personaVista.Id = repoPersona.Adicionar(personaVista);

            _entidad!.IdPersona = personaVista.Id;

            repositorio.Editar(_entidad);
        } else repoPersona.Editar(personaVista);

        // Telefonos
        var repoTelefonos = RepoTelefonoContacto.Instancia;
        var telefonosBd = repoTelefonos.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, personaVista?.Id.ToString()).resultadosBusqueda.Select(t => t.entidadBase).ToList();

        // Buscar si se borró algún teléfono registrado
        telefonosBd.ForEach(telefonoBd => {
            if (Vista.Telefonos.Find(telefonoVista => telefonoVista.Id.Equals(telefonoBd.Id)) == null)
                repoTelefonos.Eliminar(telefonoBd.Id);
        });

        // Registrar o actualizar los teléfonos de la vista
        Vista.Telefonos.ForEach(telefono => {
            var telefonoBd = telefonosBd.FirstOrDefault(t => t.Id == telefono.Id);
            var telefonoVista = new TelefonoContacto(
                id: telefonoBd?.Id ?? 0,
                prefijoPais: telefono.PrefijoPais,
                numeroTelefono: telefono.NumeroTelefono,
                categoria: telefono.Categoria,
                idPersona: personaVista.Id
            );

            if (!Vista.ModoEdicion && telefonoVista.Id == 0) {
                repoTelefonos.Adicionar(telefonoVista);
            } else repoTelefonos.Editar(telefonoVista);
        });

        // Direcciones de correo
        var repoDireccionesCorreo = RepoCorreoContacto.Instancia;
        var direccionesCorreoBd = repoDireccionesCorreo.Buscar(FiltroBusquedaCorreoContacto.IdPersona, personaVista?.Id.ToString()).resultadosBusqueda.Select(t => t.entidadBase).ToList();

        // Buscar si se borró algún teléfono registrado
        direccionesCorreoBd.ForEach(direccionCorreoBd => {
            if (Vista.DireccionesCorreo.Find(direccionCorreoVista => direccionCorreoVista.Id.Equals(direccionCorreoBd.Id)) == null)
                repoDireccionesCorreo.Eliminar(direccionCorreoBd.Id);
        });

        // Registrar o actualizar los teléfonos de la vista
        Vista.DireccionesCorreo.ForEach(direccionCorreo => {
            var direccionCorreoBd = direccionesCorreoBd.FirstOrDefault(c => c.Id == direccionCorreo.Id);
            var direccionCorreoVista = new CorreoContacto(
                id: direccionCorreoBd?.Id ?? 0,
                direccionCorreo: direccionCorreo.DireccionCorreo,
                categoria: direccionCorreo.Categoria,
                idPersona: personaVista.Id
            );

            if (!Vista.ModoEdicion && direccionCorreoVista.Id == 0) {
                repoDireccionesCorreo.Adicionar(direccionCorreoVista);
            } else repoDireccionesCorreo.Editar(direccionCorreoVista);
        });
    }

    protected override bool EntidadCorrecta() {
        return true;
    }
}