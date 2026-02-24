using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public sealed class TipoCompra : IEntidadBaseDatos {
        public TipoCompra() {
            Nombre = "N/A";
            Descripcion = "N/A";
            Activo = true;
        }

        public TipoCompra(long id, string nombre, string descripcion, bool activo) {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Activo = activo;
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }

    public enum FiltroBusquedaTipoCompra {
        Todos,
        Id,
        Nombre,
        Activos
    }

    public static class UtilesBusquedaTipoCompra {
        public static object[] Filtros = {
            "Todos los tipos",
            "Identificador de BD",
            "Nombre",
            "Solo activos"
        };
    }
}