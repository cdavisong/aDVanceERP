namespace aDVanceERP.Core.Utiles; 

public static class UtilesString {
    public static string AgregarEspacioCadaXCaracteres(this string str, int espaciado) {
        return string.Join(" ", Enumerable.Range(0, str.Length / espaciado + 1)
            .Select(i => str.Substring(i * espaciado, Math.Min(espaciado, str.Length - i * espaciado))));
    }
}