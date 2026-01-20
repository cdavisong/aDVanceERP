using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores {
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
            var telefonos = repoTelefonos.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, _entidad?.Id.ToString()).resultadosBusqueda.Select(t => t.entidadBase).ToList();

            // Buscar si se borró algún teléfono registrado
            telefonos.ForEach(tbd => {
                if (Vista.Telefonos.Find(tv => tv.Id.Equals(tbd.Id)) == null)
                    repoTelefonos.Eliminar(tbd.Id);
            });

            // Registrar o actualizar los teléfonos de la vista
            Vista.Telefonos.ForEach(tv => {
                if (tv.Id == 0) {
                    tv.IdPersona = id;
                    repoTelefonos.Adicionar(tv);
                } else repoTelefonos.Editar(tv);
            });

            // Direcciones de correo
            var repoDireccionesCorreo = RepoCorreoContacto.Instancia;
            var direccionesCorreo = repoDireccionesCorreo.Buscar(FiltroBusquedaCorreoContacto.IdPersona, _entidad?.Id.ToString()).resultadosBusqueda.Select(t => t.entidadBase).ToList();

            // Buscar si se borró algún teléfono registrado
            direccionesCorreo.ForEach(cbd => {
                if (Vista.DireccionesCorreo.Find(cv => cv.Id.Equals(cbd.Id)) == null)
                    repoDireccionesCorreo.Eliminar(cbd.Id);
            });

            // Registrar o actualizar los teléfonos de la vista
            Vista.DireccionesCorreo.ForEach(cv => {
                if (cv.Id == 0) {
                    cv.IdPersona = id;
                    repoDireccionesCorreo.Adicionar(cv);
                } else repoDireccionesCorreo.Editar(cv);
            });
        }

        protected override bool EntidadCorrecta() {
            return base.EntidadCorrecta();
        }
    }
}
