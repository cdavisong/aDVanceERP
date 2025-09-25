using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Mensajero.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Presentadores;

public class PresentadorRegistroMensajero : PresentadorVistaRegistro<IVistaRegistroMensajero, Mensajero, RepoMensajero, FiltroBusquedaMensajero> {
    public PresentadorRegistroMensajero(IVistaRegistroMensajero vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Mensajero objeto) {
        Vista.ModoEdicion = true;
        Vista.NombreMensajero = objeto.Nombre;

        using (var datosContacto = new RepoContacto()) {
            var contacto = datosContacto.Buscar(FiltroBusquedaContacto.Id, objeto.IdContacto.ToString()).resultados.FirstOrDefault();

            if (contacto != null) {
                Vista.TelefonoMovil = UtilesTelefonoContacto.ObtenerTelefonoContacto(contacto.Id, true) ?? string.Empty;
            }
        }

        _entidad = objeto;
    }

    protected override bool EntidadCorrecta() {
        var nombreEncontrado = UtilesContacto.ObtenerIdContacto(Vista.NombreMensajero).Result > 0 && !Vista.ModoEdicion;
        var nombreOk = !string.IsNullOrEmpty(Vista.NombreMensajero) && !nombreEncontrado;
        var telefonoOk = !string.IsNullOrEmpty(Vista.TelefonoMovil);

        if (!string.IsNullOrEmpty(Vista.TelefonoMovil)) {
            var noLetrasTelefonosOk = !Vista.TelefonoMovil.Replace(" ", "").Any(char.IsLetter);
            var numeroDijitos = Vista.TelefonoMovil.Select(char.IsDigit).Count(result => result == true);
            var numeroDijitosOk = numeroDijitos == 8;

            if (!noLetrasTelefonosOk || !numeroDijitosOk) {
                CentroNotificaciones.Mostrar("El campo del teléfono móvil tiene caracteres no permitidos o no tiene la cantidad de dígitos correcta, corrija los datos por favor", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
                return false;
            }
        }

        if (!nombreOk)
            CentroNotificaciones.Mostrar("Existe un contacto con el mismo nombre registrado o el campo de nombre se encuentra vacío, corrija los datos por favor", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
        if (!telefonoOk)
            CentroNotificaciones.Mostrar("EL campo del teléfono móvil es obligatorio para el mensajero, rellene los datos necesarios de forma correcta y proceda al registro", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);

        return nombreOk && telefonoOk;
    }

    protected override void RegistroAuxiliar(RepoMensajero datosMensajero, long id) {
        using (var datosContacto = new RepoContacto()) {
            // Contacto
            var contacto = datosContacto.Buscar(FiltroBusquedaContacto.Id, (Entidad?.IdContacto ?? 0).ToString()).resultados.FirstOrDefault() ??
                new Contacto();

            contacto.Nombre = Vista.NombreMensajero;
            contacto.Notas = "Mensajero";

            if (Vista.ModoEdicion && contacto.Id != 0)
                datosContacto.Editar(contacto);
            else if (contacto.Id != 0)
                datosContacto.Editar(contacto);
            else if (Entidad != null) {
                Entidad.IdContacto = datosContacto.Adicionar(contacto);

                // Editar mensajero para modificar Id del contacto
                datosMensajero.Editar(Entidad);
            }

            using (var datosTelefonoContacto = new RepoTelefonoContacto()) {
                var telefonos = datosTelefonoContacto.Buscar(FiltroBusquedaTelefonoContacto.IdContacto, (Entidad?.IdContacto ?? 0).ToString()).resultados.ToList() ??
                    new List<TelefonoContacto>();
                var indiceTelefonoMovil = telefonos.FindIndex(t => t.Categoria == CategoriaTelefonoContacto.Movil);
                var indiceTelefonoFijo = telefonos.FindIndex(t => t.Categoria == CategoriaTelefonoContacto.Fijo);

                // Teléfono móvil
                if (!string.IsNullOrEmpty(Vista.TelefonoMovil)) {
                    if (indiceTelefonoMovil != -1) {
                        telefonos[indiceTelefonoMovil].Numero = Vista.TelefonoMovil;
                    } else {
                        var telefonoMovil = new TelefonoContacto(
                            0,
                            "+53",
                            Vista.TelefonoMovil,
                            CategoriaTelefonoContacto.Movil,
                            Entidad?.IdContacto ?? 0);

                        telefonos.Add(telefonoMovil);
                    }
                } else {
                    if (Vista.ModoEdicion && indiceTelefonoMovil != -1) {
                        datosTelefonoContacto.Eliminar(telefonos[indiceTelefonoMovil].Id);
                        telefonos.RemoveAt(indiceTelefonoMovil);
                    }
                }

                foreach (var telefono in telefonos)
                    if (Vista.ModoEdicion && telefono.Id != 0)
                        datosTelefonoContacto.Editar(telefono);
                    else if (telefono.Id != 0)
                        datosTelefonoContacto.Editar(telefono);
                    else
                        datosTelefonoContacto.Adicionar(telefono);
            }
        }
    }

    protected override Mensajero? ObtenerEntidadDesdeVista() {
        return new Mensajero(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreMensajero,
            true,
            UtilesContacto.ObtenerIdContacto(Vista.NombreMensajero).Result
        );
    }
}