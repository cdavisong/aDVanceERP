using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad {
    public class Rol : IEntidadBaseDatos, IComparer<Rol> {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public int Compare(Rol? x, Rol? y) {
            if (x == null || y == null)
                return 0;

            return string.Compare(x.Nombre, y.Nombre, StringComparison.Ordinal);
        }

        public override string ToString() {
            return Nombre;
        }
    }

    public enum FiltroBusquedaRol {
        Todos,
        Nombre
    }
}