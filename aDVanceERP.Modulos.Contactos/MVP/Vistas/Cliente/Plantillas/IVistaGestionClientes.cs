using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Contactos;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Cliente.Plantillas;

public interface IVistaGestionClientes : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaCliente>,
    INavegadorTuplasEntidades { }