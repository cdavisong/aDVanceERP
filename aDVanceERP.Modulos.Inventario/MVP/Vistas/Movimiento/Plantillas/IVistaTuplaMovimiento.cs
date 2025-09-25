using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Movimiento.Plantillas;

public interface IVistaTuplaMovimiento : IVistaTupla {
    string Id { get; set; }
    string NombreProducto { get; set; }
    string NombreAlmacenOrigen { get; set; }
    string NombreAlmacenDestino { get; set; }
    string SaldoInicial { get; set; }
    string CantidadMovida { get; set; }
    string SaldoFinal { get; set; }
    string TipoMovimiento { get; set; }
    string Fecha { get; set; }

    void ActualizarIconoStock(EfectoMovimiento efecto);
}