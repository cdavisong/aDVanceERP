namespace aDVanceERP.Core.Utiles; 

public static class VariablesGlobales {
    public const int AlturaTuplaPredeterminada = 42;
    public const int AlturaBarraTituloPredeterminada = 56;
    public const int AlturaBarraPiePagina = 25;

    public static readonly Color ColorResaltadoTupla = Color.Gainsboro;
    public static readonly Color ColorAdvertenciaTupla = Color.FromArgb(255, 255, 196);
    public static readonly Color ColorErrorTupla = Color.FromArgb(255, 196, 196);
    public static int CoordenadaYUltimaTupla = 0;

    public static string StringConexionBaseDatos { get; set; } = string.Empty;

    public static long ObtenerVariableIdNull(this object id) {
        var idString = id.ToString();

        return string.IsNullOrEmpty(idString) ? 0 : long.Parse(idString);
    }
}