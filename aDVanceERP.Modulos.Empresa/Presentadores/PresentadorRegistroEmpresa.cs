using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Empresas;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Empresas;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Modulos.Empresa.Interfaces;

namespace aDVanceERP.Modulos.Empresa.Presentadores {
    internal class PresentadorRegistroEmpresa : PresentadorVistaRegistro<IVIstaRegistroEmpresa, Core.Modelos.Modulos.Empresas.Empresa, RepoEmpresa, FiltroBusquedaEmpresa> {
        public PresentadorRegistroEmpresa(IVIstaRegistroEmpresa vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroEmpresa", OnMostrarVistaRegistroEmpresa);
            AgregadorEventos.Suscribir("MostrarVistaEdicionEmpresa", OnMostrarVistaEdicionEmpresa);
        }

        private void OnMostrarVistaRegistroEmpresa(string obj) {
            if (RepoEmpresa.Instancia.Cantidad() > 0)
                return;

            Vista.ModoEdicion = false;
            Vista.Restaurar();

            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionEmpresa(string obj) {
            if (RepoEmpresa.Instancia.Cantidad() == 0) {
                if (CentroNotificaciones.MostrarMensaje("No se han proporcionado datos de la empresa. Deseas ingresar los datos de la empresa ahora?", Core.Modelos.Comun.TipoMensaje.Info, Core.Modelos.Comun.BotonesMensaje.SiNo) == DialogResult.Yes)
                    OnMostrarVistaRegistroEmpresa(string.Empty);                
                return;
            }

            Vista.ModoEdicion = true;
            Vista.Restaurar();

            var empresa = RepoEmpresa.Instancia.ObtenerPorId(1);

            if (empresa == null)
                return;

            PopularVistaDesdeEntidad(empresa);

            Vista.Mostrar();
        }

        public override void PopularVistaDesdeEntidad(Core.Modelos.Modulos.Empresas.Empresa entidad) {
            base.PopularVistaDesdeEntidad(entidad);

            var repoPersona = RepoPersona.Instancia;
            var persona = repoPersona.Buscar(FiltroBusquedaPersona.NombreCompleto, entidad.Nombre).resultadosBusqueda.Select(p => p.entidadBase).FirstOrDefault();
            var telefono = persona != null ? RepoTelefonoContacto.Instancia.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, persona.Id.ToString()).resultadosBusqueda.Select(t => t.entidadBase).FirstOrDefault() : null;
            var correo = persona != null ? RepoCorreoContacto.Instancia.Buscar(FiltroBusquedaCorreoContacto.IdPersona, persona.Id.ToString()).resultadosBusqueda.Select(c => c.entidadBase).FirstOrDefault() : null;

            // Cargar la imagen sin bloquear el archivo: crear una copia en memoria (Bitmap).
            try {
                if (!string.IsNullOrWhiteSpace(entidad?.RutaLogo) && File.Exists(entidad.RutaLogo)) {
                    using var fs = File.OpenRead(entidad.RutaLogo);
                    using var img = Image.FromStream(fs);
                    Vista.Imagen = new Bitmap(img); // copia desconectada del stream/archivo
                } else {
                    Vista.Imagen = null;
                }
            } catch {
                Vista.Imagen = null;
            }

            Vista.Nombre = entidad.Nombre;
            Vista.RazonSocial = entidad.RazonSocial ?? string.Empty;
            Vista.Rif = entidad.Rif;
            Vista.Direccion = entidad.Direccion;
            Vista.Telefono = telefono;
            Vista.Email = correo;
            Vista.Web = entidad.Web;
            Vista.FechaRegistro = entidad.FechaRegistro;
        }

        protected override Core.Modelos.Modulos.Empresas.Empresa? ObtenerEntidadDesdeVista() {
            // Salvar imagen en el proceso de obtención de la entidad
            Vista.SalvarImagenEnDirectorioLocal();

            return new Core.Modelos.Modulos.Empresas.Empresa {
                Id = 0,
                Nombre = Vista.Nombre,
                RazonSocial = Vista.RazonSocial,
                Rif = Vista.Rif,
                Direccion = Vista.Direccion,
                Telefono = Vista.Telefono != null ? $"{Vista.Telefono.PrefijoPais} {Vista.Telefono.NumeroTelefono}" : string.Empty,
                Email = Vista.Email != null ? Vista.Email.DireccionCorreo : string.Empty,
                Web = Vista.Web,
                RutaLogo = Vista.RutaLogo,
                FechaRegistro = Vista.FechaRegistro
            };
        }

        protected override void RegistroEdicionAuxiliar(RepoEmpresa repositorio, long id) {
            // Persona
            var repoPersona = RepoPersona.Instancia;
            var personaBd = repoPersona.Buscar(FiltroBusquedaPersona.NombreCompleto, Vista.Nombre).resultadosBusqueda.FirstOrDefault().entidadBase;
            var personaVista = new Persona(
                id: personaBd?.Id ?? 0,
                nombreCompleto: Vista.Nombre,
                tipoDocumento: TipoDocumento.CI,
                numeroDocumento: Vista.Rif,
                direccionPrincipal: personaBd?.DireccionPrincipal ?? string.Empty,
                fechaRegistro: Vista.FechaRegistro,
                activo: true
            );

            if (!Vista.ModoEdicion && personaVista.Id == 0) {
                personaVista.Id = repoPersona.Adicionar(personaVista);

                Vista.IdPersona = personaVista.Id;

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

            // Correo
            var repoCorreos = RepoCorreoContacto.Instancia;
            var correoBd = repoCorreos.Buscar(FiltroBusquedaCorreoContacto.IdPersona, personaVista.Id.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;
            var correoVista = new CorreoContacto(
                id: correoBd?.Id ?? 0,
                direccionCorreo: Vista.Email.DireccionCorreo,
                categoria: Vista.Email.Categoria,
                idPersona: personaVista.Id
            );

            if (!Vista.ModoEdicion && correoVista.Id == 0) {
                repoCorreos.Adicionar(correoVista);
            } else repoCorreos.Editar(correoVista);
        }
    }
}
