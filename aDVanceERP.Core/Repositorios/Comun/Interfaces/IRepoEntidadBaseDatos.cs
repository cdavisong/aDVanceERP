using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Repositorios.Comun.Interfaces;

public interface IRepoEntidadBaseDatos<En, Fb> : IRepoBase<En>
    where En : class, IEntidadBaseDatos, new()
    where Fb : Enum
{
    #region Obtención de datos y búsqueda de entidades

    (int cantidad, List<En> resultados) Buscar(string? consulta = "", int limite = 0, int desplazamiento = 0);
    (int cantidad, List<En> resultados) Buscar(Fb? filtroBusqueda, string? criterio, int limite = 0, int desplazamiento = 0);

    #endregion

    long Cantidad();
    long Adicionar(En objeto);
    bool Editar(En objeto, long nuevoId = 0);
    bool Eliminar(long id);
    bool Existe(long id);
}