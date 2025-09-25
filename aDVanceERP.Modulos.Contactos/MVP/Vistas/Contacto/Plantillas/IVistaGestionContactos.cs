using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Contacto.Plantillas;

public interface IVistaGestionContactos : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaContacto>,
    INavegadorTuplasEntidades { }