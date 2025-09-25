using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.MVP.Presentadores;
using aDVanceERP.Core.Mensajes.MVP.Vistas.Notificacion;
using aDVanceERP.Core.Mensajes.MVP.Vistas.Notificacion.Plantillas;

namespace aDVanceERP.Core.Mensajes.Utiles; 

public static class CentroNotificaciones {
    private static readonly List<IVistaNotificacion> _notificacionesActivas = new();
    private static readonly int _margen = 10; // Margen entre notificaciones y respecto a la pantalla

    public static void Mostrar(string mensaje, TipoNotificacion tipo = TipoNotificacion.Info) {
        var areaTrabajo = Screen.PrimaryScreen?.WorkingArea;
        var modelo = new Notificacion(mensaje, tipo);
        var vista = new VistaNotificacion(modelo);

        // Calculamos la posición final (para ubicar la notificación apilada)
        var xFinal = areaTrabajo.GetValueOrDefault().Right;
        var alturaPila = _margen;

        foreach (var notificacion in _notificacionesActivas)
            alturaPila += notificacion.Dimensiones.Height + _margen;

        var yFinal = areaTrabajo.GetValueOrDefault().Bottom - vista.Height - alturaPila;

        // Verificamos que haya espacio vertical suficiente
        if (yFinal < areaTrabajo.GetValueOrDefault().Top)
            return;

        // Posicion inicial : fuera de la pantalla (a la derecha)
        vista.Coordenadas = new Point(areaTrabajo.GetValueOrDefault().Right, yFinal);
        vista.Salir += delegate {
            _notificacionesActivas.Remove(vista);

            ReposicionarNotificaciones();
        };

        _notificacionesActivas.Add(vista);

        var presentador = new PresentadorNotificacion(vista, modelo);

        presentador.Vista.EstablecerPosicionObjetivo(new Point(xFinal, yFinal));
        presentador.Vista.Mostrar();

        ReposicionarNotificaciones();
    }

    private static void ReposicionarNotificaciones() {
        var areaTrabajo = Screen.PrimaryScreen?.WorkingArea;
        var margenActual = _margen;

        foreach (var notificacion in _notificacionesActivas) {
            var xFinal = areaTrabajo.GetValueOrDefault().Right - notificacion.Dimensiones.Width - _margen;
            var yFinal = areaTrabajo.GetValueOrDefault().Bottom - notificacion.Dimensiones.Height - margenActual;

            notificacion.ActualizarPosicionObjetivo(new Point(xFinal, yFinal));

            margenActual += notificacion.Dimensiones.Height + _margen;
        }
    }
}