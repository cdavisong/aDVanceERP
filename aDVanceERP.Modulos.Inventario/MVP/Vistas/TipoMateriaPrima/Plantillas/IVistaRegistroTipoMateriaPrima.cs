using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.TipoMateriaPrima.Plantillas;

public interface IVistaRegistroTipoMateriaPrima : IVistaRegistro {
    string NombreTipoMateriaPrima { get; set; }
    string? Descripcion { get; set; }
}