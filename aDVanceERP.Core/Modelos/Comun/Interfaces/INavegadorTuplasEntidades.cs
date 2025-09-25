namespace aDVanceERP.Core.Modelos.Comun.Interfaces;

public interface INavegadorTuplasEntidades {
    int PaginaActual { get; set; }
    int PaginasTotales { get; set; }

    event EventHandler? AlturaContenedorTuplasModificada;
    event EventHandler? MostrarPrimeraPagina;
    event EventHandler? MostrarPaginaAnterior;
    event EventHandler? MostrarPaginaSiguiente;
    event EventHandler? MostrarUltimaPagina;
    event EventHandler? SincronizarDatos;
}