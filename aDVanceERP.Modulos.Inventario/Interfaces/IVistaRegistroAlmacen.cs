using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaRegistroAlmacen : IVistaRegistro {
        string NombreAlmacen { get; set; }
        string? Direccion { get; set; }
        TipoAlmacen Tipo { get; set; }
        string? Descripcion { get; set; }
    }
}