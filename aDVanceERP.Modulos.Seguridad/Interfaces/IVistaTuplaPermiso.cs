using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Interfaces {
    public interface IVistaTuplaPermiso : IVistaTupla {
        string Id { get; set; }
        string NombrePermiso { get; set; }
    }
}