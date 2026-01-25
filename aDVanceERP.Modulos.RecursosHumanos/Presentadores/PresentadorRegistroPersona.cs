using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores
{
    public class PresentadorRegistroPersona : PresentadorVistaRegistro<IVIstaRegistroPersona, Persona, RepoPersona, FiltroBusquedaPersona> {
        public PresentadorRegistroPersona(IVIstaRegistroPersona vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroPersona", OnMostrarVistaRegistroPersona);
            AgregadorEventos.Suscribir("MostrarVistaEdicionPersona", OnMostrarVistaEdicionPersona);
        }

        private void OnMostrarVistaRegistroPersona(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionPersona(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var persona = AgregadorEventos.DeserializarPayload<Persona>(obj);

            if (persona == null)
                return;

            PopularVistaDesdeEntidad(persona);

            Vista.Mostrar();
        }

        public override void PopularVistaDesdeEntidad(Persona entidad) {
            base.PopularVistaDesdeEntidad(entidad);

            Vista.NombreCompleto = entidad.NombreCompleto;
            Vista.TipoDocumento = entidad.TipoDocumento;
            Vista.NumeroDocumento = entidad.NumeroDocumento;
            Vista.FechaRegistro = entidad.FechaRegistro;
            Vista.DireccionPrincipal = entidad.DireccionPrincipal;

            // Agregar teléfonos
            var telefonos = RepoTelefonoContacto.Instancia.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, entidad.Id.ToString()).resultadosBusqueda.Select(t => t.entidadBase).ToList();
            telefonos.ForEach(t => { Vista.AgregarTelefono(t.Id, t.Categoria.ToString(), t.PrefijoPais, t.NumeroTelefono, t.IdPersona); });

            // Agregar direcciones correo
            var direccionesCorreo = RepoCorreoContacto.Instancia.Buscar(FiltroBusquedaCorreoContacto.IdPersona, entidad.Id.ToString()).resultadosBusqueda.Select(t => t.entidadBase).ToList();
            direccionesCorreo.ForEach(c => { Vista.AgregarDireccionCorreo(c.Id, c.Categoria.ToString(), c.DireccionCorreo, c.IdPersona); });
        }

        protected override Persona? ObtenerEntidadDesdeVista() {
            return new Persona(
                id: 0,
                nombreCompleto: Vista.NombreCompleto,
                tipoDocumento: Vista.TipoDocumento,
                numeroDocumento: Vista.NumeroDocumento,
                direccionPrincipal: Vista.DireccionPrincipal,
                fechaRegistro: Vista.FechaRegistro,
                activo: true
            );
        }

        protected override void RegistroEdicionAuxiliar(RepoPersona repositorio, long id) {
            // Telefonos
            var repoTelefonos = RepoTelefonoContacto.Instancia;
            var telefonosBd = repoTelefonos.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, _entidad?.Id.ToString()).resultadosBusqueda.Select(t => t.entidadBase).ToList();

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
                    idPersona: id
                );

                if (!Vista.ModoEdicion && telefonoVista.Id == 0) {
                    repoTelefonos.Adicionar(telefonoVista);
                } else repoTelefonos.Editar(telefonoVista);
            });

            // Direcciones de correo
            var repoDireccionesCorreo = RepoCorreoContacto.Instancia;
            var direccionesCorreoBd = repoDireccionesCorreo.Buscar(FiltroBusquedaCorreoContacto.IdPersona, _entidad?.Id.ToString()).resultadosBusqueda.Select(t => t.entidadBase).ToList();

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
                    idPersona: id
                );

                if (!Vista.ModoEdicion && direccionCorreoVista.Id == 0) {
                    repoDireccionesCorreo.Adicionar(direccionCorreoVista);
                } else repoDireccionesCorreo.Editar(direccionCorreoVista);
            });
        }

        protected override bool EntidadCorrecta() {
            var personasConNombreRepetido = RepoPersona.Instancia.Buscar(FiltroBusquedaPersona.NombreCompleto, Vista.NombreCompleto).cantidad;
            var nombreRepetido = !Vista.ModoEdicion && personasConNombreRepetido > 0;
            var nombreOk = !string.IsNullOrEmpty(Vista.NombreCompleto) && !nombreRepetido;

            if (nombreRepetido)
                CentroNotificaciones.Mostrar("Ye existe una persona con el mismo nombre registrada en el sistema, los nombres de personas deben ser únicos.", TipoNotificacion.Advertencia);
            if (!nombreOk)
                CentroNotificaciones.Mostrar("El campo de nombre es obligatorio para la persona, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);

            return nombreOk;
        }
    }
}
