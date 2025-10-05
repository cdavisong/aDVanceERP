using aDVanceERP.Core.Modelos.Comun;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces;

public interface IVistaNotificacion : IVistaBase {
    string? Mensaje { get; set; }

    TipoNotificacion Tipo { get; set; }

    void ActualizarPosicionObjetivo(Point objetivo);
    void EstablecerPosicionObjetivo(Point objetivo);
}