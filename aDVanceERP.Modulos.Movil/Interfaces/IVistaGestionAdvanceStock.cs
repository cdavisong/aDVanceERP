using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Movil.Interfaces {
    internal interface IVistaGestionAdvanceStock : IVistaBase {
        bool DispositivoConectado { get; set; }
        bool AppInstalada { get; set; }
        bool CatalogosExistenEnDispositivo { get; set; }
        DateTime? FechaActualizacionCatalogos { get; set; }
        bool MostrarBotonEnviarCatalogos { get; set; }
        bool MostrarBotonEliminarCatalogos { get; set; }
        bool MostrarBotonImportarSesiones { get; set; }
        int ArchivosDisponiblesDispositivo { get; set; }

        void ActualizarArchivosSesion(List<(string fileName, DateTime fechaHora, double tamanoKb)> archivos);

        event EventHandler? VerificarConexion;
        event EventHandler? EnviarCatalogos;
        event EventHandler? EliminarCatalogos;
        event EventHandler? ImportarSesiones;
    }
}