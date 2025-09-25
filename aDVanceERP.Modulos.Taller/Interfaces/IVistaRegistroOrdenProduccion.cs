using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Taller.Interfaces
{
    public interface IVistaRegistroOrdenProduccion : IVistaRegistro {
        long Id { get; set; }
        string NumeroOrden { get; set; }
        DateTime FechaApertura { get; set; }
        string NombreProductoTerminado { get; set; }
        string NombreAlmacenDestino { get; set; }
        decimal Cantidad { get; set; }
        decimal MargenGanancia { get; set; }
        string Observaciones { get; set; }
        decimal CostoTotal { get; set; }
        decimal PrecioUnitario { get; set; }
        string NombreAlmacenMateriales { get; }
        List<string[]> MateriasPrimas { get; }
        List<string[]> ActividadesProduccion { get; }
        List<string[]> GastosIndirectos { get; }

        event EventHandler? MateriaPrimaEliminada;
        event EventHandler? ActividadProduccionEliminada;
        event EventHandler? GastoIndirectoEliminado;


        void CargarNombresAlmacenesMateriales(object[] nombresAlmacenes);
        void CargarNombresAlmacenesDestino(object[] nombresAlmacenes);

        void CargarNombresProductosTerminados(string[] nombresProductosTerminados);
        void CargarNombresMateriasPrimas(string[] nombresMateriasPrimas);
        void CargarNombresActividadesProduccion(string[] nombresActividadesProduccion);
        void CargarConceptosGastosIndirectos(string[] conceptosGastosIndirectos);
        void AdicionarMateriaPrima(string nombreAlmacen = "", string nombre = "", decimal cantidad = 0m);
        void AdicionarActividadProduccion(string nombre = "", decimal cantidad = 0m);
        void InsertarGastoIndirectoNormal(string concepto = "", decimal cantidad = 0m);
        void InsertarGastoIndirectoDinamico(string concepto = "", decimal cantidad = 0m, string ecuacion = "");
    }
}