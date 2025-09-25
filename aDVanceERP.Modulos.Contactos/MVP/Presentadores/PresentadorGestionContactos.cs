using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Contacto;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Contacto.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Presentadores;

public class PresentadorGestionContactos : PresentadorVistaGestion<PresentadorTuplaContacto, IVistaGestionContactos,
    IVistaTuplaContacto, Contacto, RepoContacto, FiltroBusquedaContacto> {
    public PresentadorGestionContactos(IVistaGestionContactos vista) : base(vista) { }

    protected override PresentadorTuplaContacto ObtenerValoresTupla(Contacto objeto) {
        var presentadorTupla = new PresentadorTuplaContacto(new VistaTuplaContacto(), objeto);

        presentadorTupla.Vista.Id = objeto.Id.ToString();
        presentadorTupla.Vista.NombreContacto = objeto.Nombre ?? string.Empty;

        using (var datosTelefonoContacto = new RepoTelefonoContacto()) {
            var telefonosContacto =
                datosTelefonoContacto.Buscar(FiltroBusquedaTelefonoContacto.IdContacto, objeto.Id.ToString()).resultados;
            var telefonoString = telefonosContacto.Aggregate(string.Empty,
                (current, telefono) => current + $"{telefono.Prefijo} {telefono.Numero}, ");

            if (!string.IsNullOrEmpty(telefonoString))
                telefonoString = telefonoString[..^2];

            presentadorTupla.Vista.Telefonos = telefonoString;
        }

        presentadorTupla.Vista.CorreoElectronico = objeto.DireccionCorreoElectronico ?? string.Empty;
        presentadorTupla.Vista.Direccion = objeto.Direccion ?? string.Empty;


        return presentadorTupla;
    }
}