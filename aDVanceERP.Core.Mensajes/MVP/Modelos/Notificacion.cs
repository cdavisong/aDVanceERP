namespace aDVanceERP.Core.Mensajes.MVP.Modelos; 

public enum TipoNotificacion {
    Info,
    Advertencia,
    Error
}

public class Notificacion {
    public Notificacion() { }

    public Notificacion(string mensaje, TipoNotificacion tipo) {
        Mensaje = mensaje;
        Tipo = tipo;
    }

    public string? Mensaje { get; set; }

    public int Duracion {
        get {
            var tiempoBase = 3000;
            var tiempoExtra = Mensaje?.Length * 50 ?? 1; // 50 milisegundos extra por caracter

            return tiempoBase + tiempoExtra;
        }
    }

    public TipoNotificacion Tipo { get; set; }
}