using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Finanzas;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.CuentaBancaria.Plantillas;

public interface IVistaGestionCuentasBancarias : IVistaContenedor, IGestorEntidades,
    IBuscadorEntidades<FiltroBusquedaCuentaBancaria>, INavegadorTuplasEntidades { }