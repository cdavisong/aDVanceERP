using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun.Interfaces;

public interface IPresentadorVistaTupla<Vt, En> : IPresentadorVistaBase<Vt>
    where Vt : class, IVistaTupla
    where En : class, IEntidadBaseDatos, new() {

    En Entidad { get; }

    bool EstadoSeleccion { get; set; }

    event EventHandler? EntidadSeleccionada;
    event EventHandler? EntidadDeseleccionada;
    event EventHandler? EditarEntidad;
    event EventHandler? EliminarEntidad;
}