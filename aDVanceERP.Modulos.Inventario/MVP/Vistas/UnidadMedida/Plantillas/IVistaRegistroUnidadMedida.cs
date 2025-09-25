using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.UnidadMedida.Plantillas
{
    public interface IVistaRegistroUnidadMedida : IVistaRegistro {
        string NombreUnidadMedida { get; set; }
        string Abreviatura { get; set; }
        string? Descripcion { get; set; }
    }
}
