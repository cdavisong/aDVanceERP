using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    internal interface IVistaRegistroClasificacion : IVistaRegistro {
        string Nombre { get; set; }
        string Descripcion { get; set; }
    }
}