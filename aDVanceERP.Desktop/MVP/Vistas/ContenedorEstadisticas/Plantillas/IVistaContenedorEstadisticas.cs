using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Desktop.MVP.Vistas.ContenedorEstadisticas.Plantillas;

public interface IVistaContenedorEstadisticas : IVistaBase {
    decimal CantidadProductosRegistrados { get; set; }
    decimal MontoInversionProductos { get; set; }
    decimal CantidadProductosVendidos { get; set; }
    decimal MontoVentaProductosVendidos { get; set; }
    decimal MontoGananciaTotalNegocio { get; set; }
    decimal MontoGananciaAcumuladaDia { get; set; }
    RepoEstadisticosVentas DatosEstadisticosVentas { get; set; }
    DateTime FechaEstadisticasVentas { get; }

    event EventHandler? MostrarVistaGestionProductos;
    event EventHandler? MostrarVistaGestionVentas;
    event EventHandler? FechaEstadsticasModificada;
}