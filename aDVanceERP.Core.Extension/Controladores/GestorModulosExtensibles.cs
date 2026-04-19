using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Extension.Atributos;
using aDVanceERP.Core.Extension.Infraestructura.Globales;
using aDVanceERP.Core.Extension.Interfaces;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

using System.Reflection;

namespace aDVanceERP.Core.Extension.Controladores {
    public sealed class GestorModulosExtensibles {
        private readonly Dictionary<string, IModuloExtension> _modulosCargados;

        public GestorModulosExtensibles() {
            _modulosCargados = new Dictionary<string, IModuloExtension>();
        }

        public IEnumerable<IModuloExtension> ObtenerModulosExtension() => _modulosCargados.Values;

        public void CargarModulos(IPresentadorVistaPrincipal<IVistaPrincipal> principal, IProgress<(string texto, int porcentaje)> progreso) {
            ContextoModulos.NombresModulosCargados.Clear();

            var archivosDll = Directory.GetFiles(".\\", "*.dll");

            foreach (var rutaArchivo in archivosDll) {
                try {
                    var ensamblado = Assembly.LoadFrom(rutaArchivo);
                    var tiposModuloExtension = ensamblado.GetTypes()
                        .Where(t => typeof(IModuloExtension).IsAssignableFrom(t) && !t.IsAbstract);
                    
                    foreach (var tipo in tiposModuloExtension) {
                        var moduloExtension = Activator.CreateInstance(tipo) as IModuloExtension;   

                        if (moduloExtension != null && VerificarDependencias(moduloExtension)) {
                            (principal.Vista as Control)?.Invoke(() => {
                                progreso.Report(($"Cargando módulo {moduloExtension.Nombre.ObtenerNombreDescripcion().Nombre}", 90));
                                moduloExtension.Inicializar(principal);
                            });

                            _modulosCargados.Add(moduloExtension.Nombre.ToString(), moduloExtension);

                            ContextoModulos.NombresModulosCargados.Add(moduloExtension.Nombre.ToString());
                            AgregadorEventos.Publicar($"Modulo{moduloExtension.Nombre.ObtenerNombreDescripcion().Nombre}Cargado", AgregadorEventos.SerializarPayload(moduloExtension));
                        }
                    }
                }
                catch (ExcepcionCargaModuloExtensible) {
                    throw;
                }
                catch (ReflectionTypeLoadException) {
                    continue;
                }
            }

            progreso.Report(("Carga de módulos completada", 100));
            AgregadorEventos.Publicar("CargaModulosCompletada", AgregadorEventos.SerializarPayload(ContextoModulos.NombresModulosCargados.ToList()));
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
            var dependencias = tipo.GetCustomAttributes(typeof(DependenciaModulo), true)
                                   .Cast<DependenciaModulo>();

            foreach (var dependencia in dependencias) {
                var moduloDependiente = _modulosCargados.Values.FirstOrDefault(m =>
                    m.Nombre.ToString().Equals(dependencia.NombreModulo, StringComparison.OrdinalIgnoreCase));

                if (moduloDependiente == null) {
                    CentroNotificaciones.MostrarNotificacion($"Faltan dependencias para el módulo {modulo.Nombre}: " +
                                    $"El módulo requerido {dependencia.NombreModulo} no ha sido cargado", Modelos.Comun.TipoNotificacionEnum.Error);
                    return false;
                }

                if (CompararVersiones(moduloDependiente.Version, dependencia.VersionMinima) < 0) {
                    CentroNotificaciones.MostrarNotificacion($"La versión de dependencia requerida no coincide para el módulo {modulo.Nombre}: " +
                                    $"Se requiere {dependencia.NombreModulo} v{dependencia.VersionMinima}, " +
                                    $"Encontrado v{moduloDependiente.Version}", Modelos.Comun.TipoNotificacionEnum.Error);
                    return false;
                }
            }

            return true;
        }

        private int CompararVersiones(Version version, Version versionMinima) {
            return version.CompareTo(versionMinima);
        }
    }
}