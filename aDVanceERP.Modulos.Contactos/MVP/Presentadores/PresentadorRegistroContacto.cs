using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Contacto.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Presentadores;

public class PresentadorRegistroContacto : PresentadorVistaRegistro<IVistaRegistroContacto, Contacto, RepoContacto,
    FiltroBusquedaContacto> {
    public PresentadorRegistroContacto(IVistaRegistroContacto vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Contacto objeto) {
        Vista.ModoEdicion = true;
        Vista.NombreContacto = objeto.Nombre ?? string.Empty;
        Vista.TelefonoMovil = UtilesTelefonoContacto.ObtenerTelefonoContacto(objeto.Id, true) ?? string.Empty;
        Vista.TelefonoFijo = UtilesTelefonoContacto.ObtenerTelefonoContacto(objeto.Id, false) ?? string.Empty;
        Vista.CorreoElectronico = objeto.DireccionCorreoElectronico ?? string.Empty;
        Vista.Direccion = objeto.Direccion ?? string.Empty;
        Vista.Notas = objeto.Notas ?? string.Empty;

        _entidad = objeto;
    }

    protected override bool EntidadCorrecta() {
        var nombreEncontrado = UtilesContacto.ObtenerIdContacto(Vista.NombreContacto).Result > 0 && !Vista.ModoEdicion;
        var nombreOk = !string.IsNullOrEmpty(Vista.NombreContacto) && !nombreEncontrado;

        if (!string.IsNullOrEmpty(Vista.TelefonoMovil)) {
            var noLetrasTelefonosOk = !Vista.TelefonoMovil.Replace(" ", "").Any(char.IsLetter);
            var numeroDijitos = Vista.TelefonoMovil.Select(char.IsDigit).Count(result => result == true);
            var numeroDijitosOk = numeroDijitos == 8;

            if (!noLetrasTelefonosOk || !numeroDijitosOk)
                CentroNotificaciones.Mostrar("El campo del teléfono móvil tiene caracteres no permitidos o no tiene la cantidad de dígitos correcta, corrija los datos por favor", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);

            return false;
        }

        if (!string.IsNullOrEmpty(Vista.TelefonoFijo)) {
            var noLetrasTelefonosOk = !Vista.TelefonoFijo.Replace(" ", "").Any(char.IsLetter);
            var numeroDijitos = Vista.TelefonoFijo.Select(char.IsDigit).Count(result => result == true);
            var numeroDijitosOk = numeroDijitos == 8;

            if (!noLetrasTelefonosOk || !numeroDijitosOk)
                CentroNotificaciones.Mostrar("El campo del teléfono fijo tiene caracteres no permitidos o no tiene la cantidad de dígitos correcta, corrija los datos por favor", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);

            return false;
        }

        if (!nombreOk)
            CentroNotificaciones.Mostrar("Existe un contacto con el mismo nombre registrado o el campo de nombre se encuentra vacío, corrija los datos por favor", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);


        return nombreOk;
    }

    protected override Contacto? ObtenerEntidadDesdeVista() {
        return new Contacto(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreContacto,
            Vista.CorreoElectronico,
            Vista.Direccion,
            Vista.Notas
        );
    }

    /// <summary>
    ///     Registro o actualización de teléfonos para el contacto.
    /// </summary>
    protected override void RegistroAuxiliar(RepoContacto datosContacto, long id) {
        using (var datosTelefonoContacto = new RepoTelefonoContacto()) {
            var telefonos = datosTelefonoContacto.Buscar(FiltroBusquedaTelefonoContacto.IdContacto, (Entidad?.Id ?? 0).ToString()).resultados.ToList() ??
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
                        Entidad?.Id ?? 0);

                    telefonos.Add(telefonoMovil);
                }
            } else {
                if (Vista.ModoEdicion && indiceTelefonoMovil != -1) {
                    datosTelefonoContacto.Eliminar(telefonos[indiceTelefonoMovil].Id);
                    telefonos.RemoveAt(indiceTelefonoMovil);
                }
            }

            // Teléfono fijo
            if (!string.IsNullOrEmpty(Vista.TelefonoFijo)) {
                if (indiceTelefonoFijo != -1) {
                    telefonos[indiceTelefonoFijo].Numero = Vista.TelefonoFijo;
                } else {
                    var telefonoFijo = new TelefonoContacto(
                        0,
                        "+53",
                        Vista.TelefonoFijo,
                        CategoriaTelefonoContacto.Fijo,
                        Entidad?.Id ?? 0);

                    telefonos.Add(telefonoFijo);
                }
            } else {
                if (Vista.ModoEdicion && indiceTelefonoFijo != -1) {
                    datosTelefonoContacto.Eliminar(telefonos[indiceTelefonoFijo].Id);
                    telefonos.RemoveAt(indiceTelefonoFijo);
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