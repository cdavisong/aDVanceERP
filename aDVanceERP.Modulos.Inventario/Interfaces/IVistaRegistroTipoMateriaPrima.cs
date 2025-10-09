using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces;

public interface IVistaRegistroTipoMateriaPrima : IVistaRegistro
{
    string NombreTipoMateriaPrima { get; set; }
    string? Descripcion { get; set; }
}