using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Movil.Interfaces {
    internal interface IVistaGestionAdvancePos : IVistaBase {
        bool DispositivoConectado { get; set; }
        bool AppInstalada { get; set; }
        bool CatalogoExisteEnDispositivo { get; set; }
        DateTime? FechaActualizacionCatalogo { get; set; }
        bool MostrarBotonEnviarCatalogo { get; set; }
        bool MostrarBotonEliminarCatalogo { get; set; }
        int ArchivosDisponiblesDispositivo {  get; set; }
        bool MostrarBotonImportarVentas { get; set; }

        event EventHandler? VerificarConexion;
        event EventHandler? EnviarCatalogo;
        event EventHandler? EliminarCatalogo;
        event EventHandler? ImportarVentas;
        event EventHandler? ImportarTodasLasVentas;

        void ActualizarArchivosVenta(List<(string fileName, DateTime fecha, double tamanoKb)> archivosVenta);
    }
}
