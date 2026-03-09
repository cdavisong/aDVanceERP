using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Movil.Interfaces {
    internal interface IVistaTuplaArchivoVenta : IVistaTupla {
        string NombreArchivo {  get; set; }
        DateTime Fecha { get; set; } 
        string TamannoArchivo { get; set; }

        event EventHandler<string>? ImportarArchivo;
    }
}
