using aDVanceERP.Actualizador.Modelos;

namespace aDVanceERP.Actualizador.Interfaces;

public interface IServicioActualizacion {
    Task<InfoActualizacion> ComprobarActualizaciones(string versionActual, bool incluirPreActualizaciones = false);
    Task<bool> DescargarActualizacion(InfoActualizacion info, IProgress<double> progreso = null);

    void AplicarActualizacion(string rutaDescargaActualizacion, IProgress<double> progreso = null);
}
