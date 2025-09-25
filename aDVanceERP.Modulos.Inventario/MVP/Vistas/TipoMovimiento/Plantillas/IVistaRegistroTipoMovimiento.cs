using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.TipoMovimiento.Plantillas;

public interface IVistaRegistroTipoMovimiento : IVistaRegistro {
    string NombreTipoMovimiento { get; set; }
    string Efecto { get; set; }
}