using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaTuplaClasificacion : IVistaTupla {
        long Id { get; set; }
        string Nombre { get; set; }
        string Descripcion { get; set; }
    }
}