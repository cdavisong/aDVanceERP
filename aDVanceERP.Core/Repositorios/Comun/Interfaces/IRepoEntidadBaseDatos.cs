using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Repositorios.Comun.Interfaces {
    public interface IRepoEntidadBaseDatos<En, Fb> : IRepoBase<En>
        where En : class, IEntidadBaseDatos, new()
        where Fb : Enum {
        #region Obtención de datos y búsqueda de entidades

        int Limite {  get; set; } 

        int Desplazamiento { get; set; }

        new List<(En entidadBase, List<IEntidadBaseDatos> entidadesExtra)> ObtenerTodos();
        (int cantidad, List<(En entidadBase, List<IEntidadBaseDatos> entidadesExtra)> resultadosBusqueda) Buscar(Fb? filtroBusqueda, params string[] criteriosBusqueda);

        #endregion

        long Cantidad();
        long Adicionar(En objeto, params IEntidadBaseDatos[] entidadesExtra);
        bool Editar(En objeto, params IEntidadBaseDatos[] entidadesExtra);
        bool Eliminar(long id);
        bool Existe(long id);
    }
}