using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Interfaces {
    internal interface IVistaCierreTurno : IVistaRegistro {

        // ── Info del turno (solo lectura — inyectada por el presentador) ──
        string CodigoTurno { get; set; }
        string NombreAlmacen { get; set; }
        DateTime FechaApertura { get; set; }
        decimal MontoApertura { get; set; }

        // ── Totales calculados (solo lectura — inyectados por el presentador) ──
        decimal TotalEfectivoCalculado { get; set; }
        decimal TotalTransferenciasCalculado { get; set; }

        // ── Declarados por el cajero ──────────────────────────────
        decimal MontoEfectivoDeclarado { get; set; }
        decimal MontoTransferenciasDeclarado { get; set; }

        // ── Diferencias (calculadas en la vista al cambiar los declarados) ──
        decimal DiferenciaEfectivo { get; set; }
        decimal DiferenciaTransferencias { get; set; }

        string? Observaciones { get; set; }

        // ── Arqueo (denominaciones) ───────────────────────────────

        /// <summary>
        /// Devuelve el conteo actual ingresado por el cajero para cada denominación.
        /// La vista los construye como CajaArqueo con Cantidad según lo ingresado.
        /// </summary>
        IEnumerable<CajaArqueo> ObtenerArqueo();

        /// <summary>
        /// El presentador llama a esto después de que la vista actualiza las
        /// cantidades del arqueo, para que el total contado se refleje en pantalla.
        /// </summary>
        void ActualizarTotalArqueo(decimal totalContado);

        /// <summary>
        /// Evento disparado cuando el cajero termina de ingresar las cantidades
        /// del arqueo y pide recalcular el total.
        /// </summary>
        event EventHandler? ArqueoModificado;
    }
}
