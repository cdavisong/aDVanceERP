using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Mensajero;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Mensajero.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Presentadores;

public class PresentadorGestionMensajeros : PresentadorVistaGestion<PresentadorTuplaMensajero, IVistaGestionMensajeros,
    IVistaTuplaMensajero, Mensajero, RepoMensajero, FiltroBusquedaMensajero> {
    public PresentadorGestionMensajeros(IVistaGestionMensajeros vista) : base(vista) {
        vista.HabilitarDeshabilitarMensajero += IntercambiarHabilitacionMensajero;
        vista.EditarEntidad += delegate {
            Vista.MostrarBtnHabilitarDeshabilitarMensajero = false;
        };
    }

    protected override PresentadorTuplaMensajero ObtenerValoresTupla(Mensajero objeto) {
        var presentadorTupla = new PresentadorTuplaMensajero(new VistaTuplaMensajero(), objeto);

        presentadorTupla.Vista.Id = objeto.Id.ToString();
        presentadorTupla.Vista.NombreMensajero = objeto.Nombre;

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

        presentadorTupla.Vista.Activo = objeto.Activo;
        presentadorTupla.EntidadSeleccionada += CambiarVisibilidadBtnHabilitacionMensajero;
        presentadorTupla.EntidadDeseleccionada += CambiarVisibilidadBtnHabilitacionMensajero;

        return presentadorTupla;
    }

    public override void ActualizarResultadosBusqueda() {
        // Cambiar la visibilidad de los botones
        Vista.MostrarBtnHabilitarDeshabilitarMensajero = false;

        base.ActualizarResultadosBusqueda();
    }

    private void IntercambiarHabilitacionMensajero(object? sender, EventArgs e) {
        // 1. Filtrar primero las tuplas seleccionadas para evitar procesamiento innecesario
        var tuplasSeleccionadas = _tuplasEntidades.Where(t => t.EstadoSeleccion).ToList();

        if (!tuplasSeleccionadas.Any()) {
            Vista.MostrarBtnHabilitarDeshabilitarMensajero = false;
            return;
        }

        // 2. Mover la instancia de RepoMensajero fuera del bucle
        using (var datosMensajero = new RepoMensajero()) {
            foreach (var tupla in tuplasSeleccionadas) {
                var mensajero = new Mensajero(
                        long.Parse(tupla.Vista.Id),
                        tupla.Vista.NombreMensajero,
                        !tupla.Vista.Activo,
                        tupla.Entidad.IdContacto
                    );

                // 3. Actualizar el mensajero 1 vez por tupla
                datosMensajero.Editar(mensajero);
            }
        }

        Vista.MostrarBtnHabilitarDeshabilitarMensajero = false;
        ActualizarResultadosBusqueda();
    }

    private void CambiarVisibilidadBtnHabilitacionMensajero(object? sender, EventArgs e) {
        Vista.MostrarBtnHabilitarDeshabilitarMensajero = _tuplasEntidades.Any(t => t.EstadoSeleccion);
    }
}