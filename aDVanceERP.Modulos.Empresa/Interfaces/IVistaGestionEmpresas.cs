using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Empresas;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Empresa.Interfaces {
    public interface IVistaGestionEmpresas : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaEmpresa>, INavegadorTuplasEntidades { }
}