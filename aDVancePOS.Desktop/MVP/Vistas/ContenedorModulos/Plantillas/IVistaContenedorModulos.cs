using aDVanceERP.Core.MVP.Vistas.Plantillas;

namespace aDVancePOS.Desktop.MVP.Vistas.ContenedorModulos.Plantillas;

public interface IVistaContenedorModulos : IVistaContenedor {
    //bool BtnModuloAdministracionVisible { get; set; }

    event EventHandler? MostrarVistaInicio;
    event EventHandler? MostrarVistaPuntoVenta;
    event EventHandler? CambioModulo;

    void PresionarBotonModulo(object? sender, EventArgs e);
}