namespace aDVanceERP.Core.Modelos.Modulos.Seguridad {
    /// <summary>
    /// Gestor de permisos para el usuario actual
    /// </summary>
    public class GestorPermisos {
        private readonly List<PermisoRol> _permisos;

        public GestorPermisos() {
            _permisos = new List<PermisoRol>();
        }

        public void CargarPermisos(IEnumerable<PermisoRol> permisos) {
            _permisos.Clear();
            _permisos.AddRange(permisos);
        }

        public bool TienePermiso(string modulo, AccionModulo accion) {
            var permiso = _permisos.FirstOrDefault(p =>
                p.Modulo.Equals(modulo, StringComparison.OrdinalIgnoreCase));

            if (permiso == null) return false;

            return accion switch {
                AccionModulo.Ver => permiso.PuedeVer,
                AccionModulo.Crear => permiso.PuedeCrear,
                AccionModulo.Editar => permiso.PuedeEditar,
                AccionModulo.Eliminar => permiso.PuedeEliminar,
                _ => false
            };
        }

        public bool TienePermiso(ModuloSistemaEnum modulo, AccionModulo accion) {
            return TienePermiso(modulo.ToString(), accion);
        }

        public List<PermisoRol> ObtenerPermisosModulo(string modulo) {
            return _permisos.Where(p =>
                p.Modulo.Equals(modulo, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool TieneAccesoModulo(string modulo) {
            return TienePermiso(modulo, AccionModulo.Ver);
        }

        public bool TieneAccesoModulo(ModuloSistemaEnum modulo) {
            return TieneAccesoModulo(modulo.ToString());
        }
    }
}