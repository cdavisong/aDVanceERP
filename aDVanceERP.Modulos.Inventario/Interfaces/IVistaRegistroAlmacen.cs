using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaRegistroAlmacen : IVistaRegistro {
        string NombreAlmacen { get; set; }
        string? Descripcion { get; set; }
        string? Direccion { get; set; }
        float? Capacidad { get; set; }
        TipoAlmacen Tipo { get; set; }
        bool Estado { get; set; }
        CoordenadasGeograficas? CoordenadasGeograficas { get; set; }
        string? Latitud { get; }
        string? Longitud { get; }
    }
}