using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Contactos;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Contacto.Plantillas;

public interface IVistaGestionContactos : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaContacto>,
    INavegadorTuplasEntidades { }