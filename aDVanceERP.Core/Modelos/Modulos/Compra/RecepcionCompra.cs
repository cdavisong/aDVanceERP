using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public sealed class RecepcionCompra : IEntidadBaseDatos {
        public RecepcionCompra() {
            Observaciones = "N/A";
            FechaRecepcion = DateTime.Now;
        }

        public RecepcionCompra(
            long id,
            long idCompra,
            DateTime fechaRecepcion,
            long recibidoPor,
            string observaciones,
            long? idMovimientoGenerado) {
            Id = id;
            IdCompra = idCompra;
            FechaRecepcion = fechaRecepcion;
            RecibidoPor = recibidoPor;
            Observaciones = observaciones;
            IdMovimientoGenerado = idMovimientoGenerado;
        }

        public long Id { get; set; }
        public long IdCompra { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public long RecibidoPor { get; set; }
        public string Observaciones { get; set; }
        public long? IdMovimientoGenerado { get; set; }

        // Propiedades de navegación
        public List<DetalleRecepcionCompra> Detalles { get; set; } = new();
    }

    public enum FiltroBusquedaRecepcionCompra {
        Todos,
        Id,
        IdCompra,
        FechaRecepcion,
        RecibidoPor
    }

    public static class UtilesBusquedaRecepcionCompra {
        public static object[] Filtros = {
            "Todas las recepciones",
            "Identificador de BD",
            "ID de compra",
            "Fecha de recepción",
            "Recibido por"
        };
    }
}