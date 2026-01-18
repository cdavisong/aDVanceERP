using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;
using aDVanceERP.Modulos.RecursosHumanos.Vistas;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorGestionProveedores : PresentadorVistaGestion<PresentadorTuplaProveedor, IVistaGestionProveedores, IVistaTuplaProveedor, Proveedor, RepoProveedor, FiltroBusquedaProveedor> {
    public PresentadorGestionProveedores(IVistaGestionProveedores vista) : base(vista) {
        RegistrarEntidad += OnRegistrarProveedor;
        EditarEntidad += OnEditarProveedor;

        AgregadorEventos.Suscribir("MostrarVistaGestionProveedores", OnMostrarVistaGestionProveedores);
    }

    private void OnRegistrarProveedor(object? sender, EventArgs e) {
        AgregadorEventos.Publicar("MostrarVistaRegistroProveedor", string.Empty);
    }

    private void OnEditarProveedor(object? sender, Proveedor e) {
        AgregadorEventos.Publicar("MostrarVistaEdicionProveedor", AgregadorEventos.SerializarPayload(e));
    }

    private void OnMostrarVistaGestionProveedores(string obj) {
        Vista.CargarFiltrosBusqueda(UtilesBusquedaProveedor.FiltroBusquedaProveedor);
        Vista.Restaurar();
        Vista.Mostrar();

        ActualizarResultadosBusqueda();
    }

    protected override PresentadorTuplaProveedor ObtenerValoresTupla(Proveedor entidad, List<IEntidadBaseDatos> entidadesExtra) {
        var presentadorTupla = new PresentadorTuplaProveedor(new VistaTuplaProveedor(), entidad);
        var persona = entidadesExtra.Count > 0 ? entidadesExtra.FirstOrDefault() as Persona : null!;
        var telefonos = RepoTelefonoContacto.Instancia.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, persona?.Id.ToString() ?? "0").resultadosBusqueda.Select(t => t.entidadBase);

        presentadorTupla.Vista.Id = entidad.Id.ToString();
        presentadorTupla.Vista.NumeroIdentificacionTributaria = entidad.NIT;
        presentadorTupla.Vista.RazonSocial = string.IsNullOrEmpty(entidad.RazonSocial) ? "N/A" : entidad.RazonSocial;
        presentadorTupla.Vista.Telefonos = string.Concat(telefonos.Select(t => $"+{t.PrefijoPais} {t.NumeroTelefono}, ")).TrimEnd(',');
        presentadorTupla.Vista.Direccion = persona?.DireccionPrincipal ?? "N/A";
        presentadorTupla.Vista.NombreRepresentante = persona?.NombreCompleto ?? "N/A";

        return presentadorTupla;
    }
}