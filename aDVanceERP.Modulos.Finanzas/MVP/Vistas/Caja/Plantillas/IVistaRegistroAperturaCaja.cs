using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja.Plantillas
{
    public interface IVistaRegistroAperturaCaja : IVistaRegistro {
        DateTime Fecha { get; set; }
        decimal SaldoInicial { get; set; }
    }
}
