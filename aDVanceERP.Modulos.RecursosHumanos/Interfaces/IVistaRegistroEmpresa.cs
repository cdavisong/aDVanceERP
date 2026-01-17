using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Interfaces {
    public interface IVistaRegistroEmpresa : IVistaRegistro {
        Image? Logotipo { get; set; }
        string NombreEmpresa { get; set; }
        string TelefonoMovil { get; set; }
        string TelefonoFijo { get; set; }
        string CorreoElectronico { get; set; }
        string Direccion { get; set; }
    }
}
