namespace aDVanceERP.Core.Modelos.Comun.Interfaces;

public interface IBuscadorEntidades<Fb>
    where Fb : Enum {
    Fb FiltroBusqueda { get; }
    string? CriterioBusqueda { get; }


    event EventHandler<(Fb, string?)>? BuscarEntidades;

    void CargarFiltrosBusqueda(object[] filtrosBusqueda);
}