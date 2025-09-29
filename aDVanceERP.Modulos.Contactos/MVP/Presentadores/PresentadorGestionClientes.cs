using aDVanceERP.Core.Modelos.Modulos.Contactos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Contactos;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Cliente;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Cliente.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Presentadores;

public class PresentadorGestionClientes : PresentadorVistaGestion<PresentadorTuplaCliente, IVistaGestionClientes, IVistaTuplaCliente, Cliente, RepoCliente, FiltroBusquedaCliente> {
    public PresentadorGestionClientes(IVistaGestionClientes vista) : base(vista) { }

    protected override PresentadorTuplaCliente ObtenerValoresTupla(Cliente objeto) {
        var presentadorTupla = new PresentadorTuplaCliente(new VistaTuplaCliente(), objeto);

        presentadorTupla.Vista.Id = objeto.Id.ToString();
        presentadorTupla.Vista.Numero = objeto.Numero ?? string.Empty;
        presentadorTupla.Vista.RazonSocial = objeto.RazonSocial ?? string.Empty;

        using (var datosContacto = new RepoContacto()) {
            var contacto = datosContacto.Buscar(FiltroBusquedaContacto.Id, objeto.IdContacto.ToString()).entidades.FirstOrDefault();

            if (contacto != null) {
                using (var datosTelefonoContacto = new RepoTelefonoContacto()) {
                    var telefonosContacto =
                        datosTelefonoContacto.Buscar(FiltroBusquedaTelefonoContacto.IdContacto, contacto.Id.ToString()).entidades;
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