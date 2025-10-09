using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces
{
    public interface IVistaRegistroUnidadMedida : IVistaRegistro
    {
        string NombreUnidadMedida { get; set; }
        string Abreviatura { get; set; }
        string? Descripcion { get; set; }
    }
}
