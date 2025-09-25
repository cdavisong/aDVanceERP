using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.CuentaBancaria.Plantillas;

public interface IVistaTuplaCuentaBancaria : IVistaTupla {
    string Id { get; set; }
    string Alias { get; set; }
    string NumeroTarjeta { get; set; }
    string Moneda { get; set; }
    string NombrePropietario { get; set; }

    event EventHandler? MostrarQR;
}