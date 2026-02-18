using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaGestionMovimientos : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaMovimiento>, INavegadorTuplasEntidades {
        event EventHandler<(DateTime fechaDesde, DateTime fechaHasta, FormatoDocumento formato)>? AuditarInventario;
    }
}