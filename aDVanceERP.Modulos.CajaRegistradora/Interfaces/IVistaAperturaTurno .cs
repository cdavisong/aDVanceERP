using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Interfaces {
    internal interface IVistaAperturaTurno : IVistaRegistro {
        /// <summary>Almacén sobre el que se abre el turno. Solo lectura en la vista (se inyecta desde el presentador).</summary>
        long IdAlmacen { get; set; }
        string NombreAlmacen { get; set; }

        /// <summary>Efectivo declarado al abrir la caja.</summary>
        decimal MontoApertura { get; set; }

        string? Observaciones { get; set; }
    }
}
