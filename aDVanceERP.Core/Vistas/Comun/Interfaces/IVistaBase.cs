using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces;

public interface IVistaBase : IEntidadBase, IDisposable {
    string NombreVista { get; }
    bool Habilitada { get; set; }
    Point Coordenadas { get; set; }
    Size Dimensiones { get; set; }

    void Inicializar();
    void Mostrar();
    void Restaurar();
    void Ocultar();
    void Cerrar();
}