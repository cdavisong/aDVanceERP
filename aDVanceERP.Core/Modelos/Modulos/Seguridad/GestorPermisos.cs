namespace aDVanceERP.Core.Modelos.Modulos.Seguridad {
    /// <summary>
    /// Gestor de permisos para el usuario actual
    /// </summary>
    public class GestorPermisos {
        private readonly List<PermisoRol> _permisos;

        public GestorPermisos() {
            _permisos = [];
        }

        public void CargarPermisos(IEnumerable<PermisoRol> permisos) {
            _permisos.Clear();
            _permisos.AddRange(permisos);
        }

        public bool TienePermiso(string modulo, AccionModuloEnum accion) {
            var permiso = _permisos.FirstOrDefault(p => p.Modulo.Equals(modulo, StringComparison.OrdinalIgnoreCase));

            if (permiso == null) 
                return false;

            return accion switch {
                AccionModuloEnum.Ver => permiso.PuedeVer,
                AccionModuloEnum.Crear => permiso.PuedeCrear,
                AccionModuloEnum.Editar => permiso.PuedeEditar,
                AccionModuloEnum.Eliminar => permiso.PuedeEliminar,
                _ => false
            };
        }

        public bool TienePermiso(ModuloSistemaEnum modulo, AccionModuloEnum accion) {
            return TienePermiso(modulo.ToString(), accion);
        }

        public List<PermisoRol> ObtenerPermisosModulo(string modulo) {
            return [.. _permisos.Where(p => p.Modulo.Equals(modulo, StringComparison.OrdinalIgnoreCase))];
        }

        public bool TieneAccesoModulo(string modulo) {
            return TienePermiso(modulo, AccionModuloEnum.Ver);
        }

        public bool TieneAccesoModulo(ModuloSistemaEnum modulo) {
            return TieneAccesoModulo(modulo.ToString());
        }
    }
}