using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja.Plantillas
{
    public interface IVistaTuplaCaja : IVistaTupla {
        string Id { get; set; }        
        string FechaApertura { get; set; }
        string SaldoInicial { get; set; }
        string CantidadMovimientos { get; set; }
        string SaldoActual { get; set; }
        string FechaCierre { get; set; }
        int Estado { get; set; }
        string NombreUsuario { get; set; }
    }
}
