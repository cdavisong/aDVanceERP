using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Repositorios.Comun.Interfaces;

public interface IRepoVistaBase<Vb> : IRepoBase<IVistaBase>
    where Vb : IVistaBase {
    Dictionary<string, Vb>? Vistas { get; }
    Vb? VistaActual { get; }

    void Registrar(Vb vista);
    void Registrar(IVistaBase vista, Point coordenadas, Size dimensiones, TipoRedimensionadoVista tipoRedimensionado);

    void Inicializar(string nombre);
    void Mostrar(string nombre);
    void Restaurar(string nombre);
    void Ocultar(string nombre);
    void Cerrar(string nombre);
}