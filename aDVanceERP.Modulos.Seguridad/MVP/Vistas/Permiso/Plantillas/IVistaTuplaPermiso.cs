using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.MVP.Vistas.Permiso.Plantillas;

public interface IVistaTuplaPermiso : IVistaTupla {
    string? Id { get; set; }
    string? NombrePermiso { get; set; }
}