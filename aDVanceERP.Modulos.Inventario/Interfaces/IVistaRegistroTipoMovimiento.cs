using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaRegistroTipoMovimiento : IVistaRegistro
    {
        string NombreTipoMovimiento { get; set; }
        string Efecto { get; set; }
    }
}