namespace aDVanceERP.Actualizador.Modelos;

public class InfoActualizacion {
    public Version UltimaVersion { get; set; }
    public string UrlDescarga { get; set; }
    public string NotasVersion { get; set; }
    public DateTime FechaLanzamiento { get; set; }
    public long TamanoArchivo { get; set; }
    public bool EsPreActualizacion { get; set; }

    public bool ActualizacionDisponible { get; set; }
}
