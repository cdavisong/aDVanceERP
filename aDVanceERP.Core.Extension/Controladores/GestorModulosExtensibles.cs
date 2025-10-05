using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Extension.Atributos;
using aDVanceERP.Core.Extension.Interfaces;
using aDVanceERP.Core.Infraestructura.Globales;
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
                CentroNotificaciones.Mostrar($"Faltan dependencias para el módulo {modulo.Nombre}: " +
                                $"El módulo requerido {dependencia.NombreModulo} no ha sido cargado", Modelos.Comun.TipoNotificacion.Error);
                return false;
            }

            if (CompararVersiones(moduloDependiente.Version, dependencia.VersionMinima) < 0) {
                CentroNotificaciones.Mostrar($"La versión de dependencia requerida no coincide para el módulo {modulo.Nombre}: " +
                                $"Se requiere {dependencia.NombreModulo} v{dependencia.VersionMinima}, " +
                                $"Encontrado v{moduloDependiente.Version}", Modelos.Comun.TipoNotificacion.Error);
                return false;
            }
        }

        return true;
    }

    private int CompararVersiones(Version version, Version versionMinima) {
        return version.CompareTo(versionMinima);
    }
}