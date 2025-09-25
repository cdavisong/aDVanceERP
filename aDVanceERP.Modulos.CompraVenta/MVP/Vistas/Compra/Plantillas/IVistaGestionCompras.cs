using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Compra.Plantillas;

public interface IVistaGestionCompras : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaCompra>,
    INavegadorTuplasEntidades {
    string FormatoReporte { get; }
    string ValorBrutoCompra { get; }

    event EventHandler? DescargarReporte;
    event EventHandler? ImprimirReporte;

    void ActualizarValorBrutoCompras();
}