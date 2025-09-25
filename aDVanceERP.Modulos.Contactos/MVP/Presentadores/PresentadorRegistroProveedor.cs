using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Proveedor.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Presentadores;

public class PresentadorRegistroProveedor : PresentadorVistaRegistro<IVistaRegistroProveedor, Proveedor, RepoProveedor, FiltroBusquedaProveedor> {
    public PresentadorRegistroProveedor(IVistaRegistroProveedor vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Proveedor objeto) {
        Vista.ModoEdicion = true;
        Vista.RazonSocial = objeto.RazonSocial ?? string.Empty;
        Vista.NumeroIdentificacionTributaria = objeto.NumeroIdentificacionTributaria ?? string.Empty;

        using (var datosContacto = new RepoContacto()) {
            var contacto = datosContacto.Buscar(FiltroBusquedaContacto.Id, objeto.IdContacto.ToString()).resultados.FirstOrDefault();

            if (contacto != null) {
                Vista.TelefonoMovil = UtilesTelefonoContacto.ObtenerTelefonoContacto(contacto.Id, true) ?? string.Empty;
                Vista.TelefonoFijo = UtilesTelefonoContacto.ObtenerTelefonoContacto(contacto.Id, false) ?? string.Empty;
                Vista.CorreoElectronico = contacto.DireccionCorreoElectronico ?? string.Empty;
                Vista.Direccion = contacto.Direccion ?? string.Empty;
            }
        }

        _entidad = objeto;
    }

    protected override bool EntidadCorrecta() {
        var nombreEncontrado = UtilesContacto.ObtenerIdContacto(Vista.RazonSocial).Result > 0 && !Vista.ModoEdicion;
        var nombreOk = !string.IsNullOrEmpty(Vista.RazonSocial) && !nombreEncontrado;

        if (!string.IsNullOrEmpty(Vista.TelefonoMovil)) {
            var noLetrasTelefonosOk = !Vista.TelefonoMovil.Replace(" ", "").Any(char.IsLetter);
            var numeroDijitos = Vista.TelefonoMovil.Select(char.IsDigit).Count(result => result == true);
            var numeroDijitosOk = numeroDijitos == 8;

            if (!noLetrasTelefonosOk || !numeroDijitosOk) {
                CentroNotificaciones.Mostrar("El campo del teléfono móvil tiene caracteres no permitidos o no tiene la cantidad de dígitos correcta, corrija los datos por favor", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
                return false;
            }
        }

        if (!string.IsNullOrEmpty(Vista.TelefonoFijo)) {
            var noLetrasTelefonosOk = !Vista.TelefonoFijo.Replace(" ", "").Any(char.IsLetter);
            var numeroDijitos = Vista.TelefonoFijo.Select(char.IsDigit).Count(result => result == true);
            var numeroDijitosOk = numeroDijitos == 8;

            if (!noLetrasTelefonosOk || !numeroDijitosOk) {
                CentroNotificaciones.Mostrar("El campo del teléfono fijo tiene caracteres no permitidos o no tiene la cantidad de dígitos correcta, corrija los datos por favor", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
                return false;
            }
        }

        if (!nombreOk)
            CentroNotificaciones.Mostrar("Existe un contacto con el mismo nombre registrado o el campo de razón social se encuentra vacío, corrija los datos por favor", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);


        return nombreOk;
    }

    protected override void RegistroAuxiliar(RepoProveedor datosProveedor, long id) {
        using (var datosContacto = new RepoContacto()) {
            // Contacto
            var contacto = datosContacto.Buscar(FiltroBusquedaContacto.Id, (Entidad?.IdContacto ?? 0).ToString()).resultados.FirstOrDefault() ??
                new Contacto();

            contacto.Nombre = Vista.RazonSocial;
            contacto.DireccionCorreoElectronico = Vista.CorreoElectronico;
            contacto.Direccion = Vista.Direccion;
            contacto.Notas = "Proveedor";

            if (Vista.ModoEdicion && contacto.Id != 0)
                datosContacto.Editar(contacto);
            else if (contacto.Id != 0)
                datosContacto.Editar(contacto);
            else if (Entidad != null) {
                Entidad.IdContacto = datosContacto.Adicionar(contacto);

                // Editar proveedor para modificar Id del contacto
                datosProveedor.Editar(Entidad);
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
                            Entidad?.IdContacto ?? 0);

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

    protected override Proveedor? ObtenerEntidadDesdeVista() {
        return new Proveedor(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.RazonSocial,
            Vista.NumeroIdentificacionTributaria,
            UtilesContacto.ObtenerIdContacto(Vista.RazonSocial).Result
        );
    }
}