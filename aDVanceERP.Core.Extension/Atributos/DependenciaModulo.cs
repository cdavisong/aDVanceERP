namespace aDVanceERP.Core.Extension.Atributos;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependenciaModulo : Attribute {
    public DependenciaModulo(string nombreModulo, Version versionMinima) {
        NombreModulo = nombreModulo;
        VersionMinima = versionMinima;
    }

    public string NombreModulo { get; }
    public Version VersionMinima { get; }
}