using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario;

public sealed class Almacen : IEntidadBaseDatos {
    public Almacen() {
        Nombre = "Genérico";
        Descripcion = "No hay descripción disponible.";
        Tipo = TipoAlmacen.Secundario;
    }

    public Almacen(long id, string nombre, string? descripcion, string? direccion, float? capacidad, TipoAlmacen tipo, bool estado, CoordenadasGeograficas? coordenadas) {
        Id = id;
        Nombre = nombre;
        Descripcion = descripcion;
        Direccion = direccion;
        Capacidad = capacidad;
        Tipo = tipo;
        Estado = estado;
        Coordenadas = coordenadas;
    }

    public long Id { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public string? Direccion { get; set; } // Dirección física o ubicación geográfica para envíos y logística.
    public float? Capacidad { get; set; } // Volumen máximo de almacenamiento (ej: en metros cúbicos o unidades)
    public TipoAlmacen Tipo {  get; set; }
    public bool Estado { get; set; } //  Indicador de actividad (activo/inactivo) para control operativo
    public CoordenadasGeograficas? Coordenadas { get; set; } // Para optimizar enrutamiento en cadenas de suministro
}

public enum FiltroBusquedaAlmacen {
    Todos,
    Id,
    Nombre
}

public static class UtilesBusquedaAlmacen {
    public static object[] FiltroBusquedaAlmacen = {
        "Todos los almacenes",
        "Identificador de BD",
        "Nombre del almacén"
    };
}
