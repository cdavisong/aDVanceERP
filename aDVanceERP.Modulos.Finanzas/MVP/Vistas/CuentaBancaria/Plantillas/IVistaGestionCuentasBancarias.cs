using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.CuentaBancaria.Plantillas;

public interface IVistaGestionCuentasBancarias : IVistaContenedor, IGestorEntidades,
    IBuscadorEntidades<FiltroBusquedaCuentaBancaria>, INavegadorTuplasEntidades { }