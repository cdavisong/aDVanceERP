using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Movil.Interfaces {
    internal interface IVistaMenuMovil : IVistaMenu {
        bool BtnAdvancePosVisible { get; set; }
        bool BtnAdvanceStockVisible { get; set; }
    }
}
