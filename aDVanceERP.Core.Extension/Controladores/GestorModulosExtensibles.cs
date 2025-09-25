using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Extension.Atributos;
using aDVanceERP.Core.Extension.Interfaces;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

using System.Reflection;
using System.Runtime.CompilerServices;

namespace aDVanceERP.Core.Extension.Controladores;

public sealed class GestorModulosExtensibles {
    private readonly Dictionary<string, IModuloExtension> _modulosCargados;

    public GestorModulosExtensibles() {
        _modulosCargados = new Dictionary<string, IModuloExtension>();
    }

    public IEnumerable<IModuloExtension> ObtenerModulosExtension() => _modulosCargados.Values;

    public void CargarModulos(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
        var archivosDll = Directory.GetFiles(".\\", "*.dll");

        foreach (var rutaArchivo in archivosDll) {
            try {
                var ensamblado = Assembly.LoadFrom(rutaArchivo);
                var tiposModuloExtension = ensamblado.GetTypes()
                    .Where(t => typeof(IModuloExtension).IsAssignableFrom(t) && !t.IsAbstract);

                foreach (var tipo in tiposModuloExtension) {
                    var moduloExtension = Activator.CreateInstance(tipo) as IModuloExtension;

                    if (moduloExtension != null && VerificarDependencias(moduloExtension)) {
                        moduloExtension.Inicializar(principal);

                        _modulosCargados.Add(moduloExtension.Nombre, moduloExtension);
                    }
                }
            }
            catch (ExcepcionCargaModuloExtensible) {
                throw;
            }
        }
    }

    public void ApagarModulos() {
        foreach (var modulo in _modulosCargados) {
            try {
                modulo.Value.Apagar();
            }
            catch (ExcepcionApagadoModuloExtensible) {
                throw;
            }
        }

        _modulosCargados.Clear();
    }

    private bool VerificarDependencias(IModuloExtension modulo) {
        var tipo = modulo.GetType();
        var dependencias = tipo.GetCustomAttributes(typeof(DependencyAttribute), true)
                               .Cast<DependenciaModulo>();

        foreach (var dependencia in dependencias) {
            var moduloDependiente = _modulosCargados.Values.FirstOrDefault(m =>
                m.Nombre.Equals(dependencia.NombreModulo, StringComparison.OrdinalIgnoreCase));

            if (moduloDependiente == null) {
                Console.WriteLine($"Dependency not met for {modulo.Nombre}: " +
                                $"Required module {dependencia.NombreModulo} not loaded");
                return false;
            }

            if (CompararVersiones(moduloDependiente.Version, dependencia.VersionMinima) < 0) {
                Console.WriteLine($"Dependency version not met for {modulo.Nombre}: " +
                                $"Required {dependencia.NombreModulo} v{dependencia.VersionMinima}, " +
                                $"found v{moduloDependiente.Version}");
                return false;
            }
        }

        return true;
    }

    private int CompararVersiones(Version version, Version versionMinima) {
        return version.CompareTo(versionMinima);
    }
}