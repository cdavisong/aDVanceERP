using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    internal interface IVistaTuplaUnidadMedida : IVistaTupla {
        long Id { get; set; }
        string Nombre { get; set; }
        string Abreviatura { get; set; }
        string Descripcion { get; set; }
    }
}