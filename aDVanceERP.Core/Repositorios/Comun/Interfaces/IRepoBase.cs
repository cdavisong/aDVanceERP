using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Repositorios.Comun.Interfaces {
    public interface IRepoBase<En> : IDisposable
        where En : class, IEntidadBase {
        En? ObtenerPorId(object id);
        List<En> ObtenerTodos();
    }
}