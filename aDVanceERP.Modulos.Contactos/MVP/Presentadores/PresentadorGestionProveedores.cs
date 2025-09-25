using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Proveedor;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Proveedor.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Presentadores;

public class PresentadorGestionProveedores : PresentadorVistaGestion<PresentadorTuplaProveedor, IVistaGestionProveedores,
    IVistaTuplaProveedor, Proveedor, RepoProveedor, FiltroBusquedaProveedor> {
    public PresentadorGestionProveedores(IVistaGestionProveedores vista) : base(vista) { }

    protected override PresentadorTuplaProveedor ObtenerValoresTupla(Proveedor objeto) {
        var presentadorTupla = new PresentadorTuplaProveedor(new VistaTuplaProveedor(), objeto);

        presentadorTupla.Vista.Id = objeto.Id.ToString();
        presentadorTupla.Vista.NumeroIdentificacionTributaria = objeto.NumeroIdentificacionTributaria ?? string.Empty;
        presentadorTupla.Vista.RazonSocial = objeto.RazonSocial ?? string.Empty;
        
        using (var datosContacto = new RepoContacto()) {
            var contacto = datosContacto.Buscar(FiltroBusquedaContacto.Id, objeto.IdContacto.ToString()).resultados.FirstOrDefault();

            if (contacto != null) {
                using (var datosTelefonoContacto = new RepoTelefonoContacto()) {
                    var telefonosContacto =
                        datosTelefonoContacto.Buscar(FiltroBusquedaTelefonoContacto.IdContacto, contacto.Id.ToString()).resultados;
                    var telefonoString = telefonosContacto.Aggregate(string.Empty,
                        (current, telefono) => current + $"{telefono.Prefijo} {telefono.Numero}, ");

                    if (!string.IsNullOrEmpty(telefonoString))
                        telefonoString = telefonoString[..^2];

                    presentadorTupla.Vista.Telefonos = telefonoString;
                }

                presentadorTupla.Vista.Direccion = contacto.Direccion ?? string.Empty;
            } else {
                presentadorTupla.Vista.Telefonos = string.Empty;
                presentadorTupla.Vista.Direccion = string.Empty;
            }
        }

        return presentadorTupla;
    }
}