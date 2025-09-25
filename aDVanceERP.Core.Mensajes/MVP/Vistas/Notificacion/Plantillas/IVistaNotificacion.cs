using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Mensajes.MVP.Vistas.Notificacion.Plantillas;

public interface IVistaNotificacion : IVistaBase {
    string? Mensaje { get; set; }

    TipoNotificacion Tipo { get; set; }

    void ActualizarPosicionObjetivo(Point objetivo);
    void EstablecerPosicionObjetivo(Point objetivo);
}