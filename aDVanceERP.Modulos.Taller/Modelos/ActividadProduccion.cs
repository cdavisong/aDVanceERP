using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.Taller.Modelos {
    public enum TipoCostoActividad {
        PorHora,
        Fijo
    }

    public class ActividadProduccion : IEntidadBaseDatos {
        public ActividadProduccion() {
            Nombre = "Genérica";
            TipoCosto = TipoCostoActividad.PorHora;
            Costo = 0.0m; // Costo por hora o costo fijo
            Descripcion = null;
            Activo = true;
        }

        public ActividadProduccion(long id, string nombre, TipoCostoActividad tipoCosto, decimal costo,
            string? descripcion, bool activo) {
            Id = id;
            Nombre = nombre;
            TipoCosto = tipoCosto;
            Costo = costo;
            Descripcion = descripcion;
            Activo = activo;
        }

        public long Id { get; set; }
        public string Nombre { get; set; } // Ej: "Corte", "Costura", "Planchado"
        public TipoCostoActividad TipoCosto { get; set; }
        public decimal Costo { get; set; } // Costo por hora o costo fijo
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;
    }

    public enum FiltroBusquedaActividadProduccion {
        Todas,
        Id,
        Nombre,
        TipoCosto,
        Estado
    }

    public static class UtilesBusquedaActividadProduccion {
        public static object[] FiltroBusquedaActividadProduccion =
        {
            "Todas las actividades",
            "Identificador de BD",
            "Nombre de la actividad",
            "Tipo de costo",
            "Estado (activo/inactivo)"
        };

        public static object[] TiposCostoActividad =
        {
            "Por hora",
            "Costo fijo"
        };
    }
}