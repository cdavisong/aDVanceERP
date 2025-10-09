using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces;

public interface IVistaRegistroAlmacen : IVistaRegistro
{
    string NombreAlmacen { get; set; }
    string Direccion { get; set; }
    bool AutorizoVenta { get; set; }
    string Descripcion { get; set; }
}