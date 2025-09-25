namespace aDVanceERP.Core.Excepciones;

public class ExcepcionApagadoModuloExtensible : Exception {
    public ExcepcionApagadoModuloExtensible()
        : base("Se produjo un error al intentar apagar uno o varios módulos de extensión.") { }
}