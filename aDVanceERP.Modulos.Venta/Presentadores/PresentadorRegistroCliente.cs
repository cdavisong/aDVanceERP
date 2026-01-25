using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    public class PresentadorRegistroCliente : PresentadorVistaRegistro<IVistaRegistroCliente, Cliente, RepoCliente, FiltroBusquedaCliente> {
        public PresentadorRegistroCliente(IVistaRegistroCliente vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroCliente", OnMostrarVistaRegistroCliente);
            AgregadorEventos.Suscribir("MostrarVistaEdicionCliente", OnMostrarVistaEdicionCliente);
        }

        private void OnMostrarVistaRegistroCliente(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            Vista.CargarNombresPersonas(RepoPersona.Instancia.NombresPersonasNoClientes());

            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionCliente(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var cliente = AgregadorEventos.DeserializarPayload<Cliente>(obj);

            if (cliente == null)
                return;

            // Carga inicial de datos
            Vista.CargarNombresPersonas(RepoPersona.Instancia.NombresPersonasNoClientes());

            PopularVistaDesdeEntidad(cliente);

            Vista.Mostrar();
        }

        public override void PopularVistaDesdeEntidad(Cliente entidad) {
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
            }

            Vista.FechaRegistro = entidad.FechaRegistro;
            Vista.CodigoCliente = entidad.CodigoCliente;
            Vista.LimiteCredito = entidad.LimiteCredito;
        }

        protected override Cliente? ObtenerEntidadDesdeVista() {
            var persona = RepoPersona.Instancia.Buscar(FiltroBusquedaPersona.NombreCompleto, Vista.NombreCompleto).resultadosBusqueda.FirstOrDefault().entidadBase;

            return new Cliente(
                id: 0,
                idPersona: persona?.Id ?? 0,
                codigoCliente: Vista.CodigoCliente,           
                limiteCredito: Vista.LimiteCredito,
                fechaRegistro: Vista.FechaRegistro,
                activo: true
            );
        }

        protected override void RegistroEdicionAuxiliar(RepoCliente repositorio, long id) {
            // Persona
            var repoPersona = RepoPersona.Instancia;
            var personaBd = repoPersona.Buscar(FiltroBusquedaPersona.NombreCompleto, Vista.NombreCompleto).resultadosBusqueda.FirstOrDefault().entidadBase;
            var personaVista = new Persona(
                id: personaBd?.Id ?? 0,
                nombreCompleto: Vista.NombreCompleto,
                tipoDocumento: Vista.TipoDocumento,
                numeroDocumento: Vista.NumeroDocumento,
                direccionPrincipal: Vista.DireccionPrincipal,
                fechaRegistro: personaBd?.FechaRegistro ?? Vista.FechaRegistro,
                activo: true
            );

            if (!Vista.ModoEdicion && personaVista.Id == 0) {
                personaVista.Id = repoPersona.Adicionar(personaVista);

                _entidad!.IdPersona = personaVista.Id;

                repositorio.Editar(_entidad);
            } else repoPersona.Editar(personaVista);

            // Telefonos
            var repoTelefonos = RepoTelefonoContacto.Instancia;
            var telefonosBd = repoTelefonos.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, personaVista.Id.ToString()).resultadosBusqueda.Select(t => t.entidadBase).ToList();

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
        }

        protected override bool EntidadCorrecta() {
            var idPersonaConNombreCoincidente = RepoPersona.Instancia.Buscar(FiltroBusquedaPersona.NombreCompleto, Vista.NombreCompleto).resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0;
            var clientesConNombreRepetido = RepoCliente.Instancia.Buscar(FiltroBusquedaCliente.IdPersona, idPersonaConNombreCoincidente.ToString()).cantidad;
            var nombreRepetido = !Vista.ModoEdicion && clientesConNombreRepetido > 0;
            var nombreOk = !string.IsNullOrEmpty(Vista.NombreCompleto) && !nombreRepetido;

            if (nombreRepetido)
                CentroNotificaciones.Mostrar("Ye existe un mensajero con el mismo nombre registrado en el sistema, los nombres de mensajeros deben ser únicos.", TipoNotificacion.Advertencia);
            if (!nombreOk)
                CentroNotificaciones.Mostrar("El campo de nombre es obligatorio para el mensajero, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);

            return nombreOk;
        }
    }
}