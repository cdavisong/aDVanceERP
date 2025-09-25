namespace aDVanceERP.Core.Excepciones;

public class ExcepcionCargaModuloExtensible : Exception {
    public ExcepcionCargaModuloExtensible() 
        : base("Se produjo un error al intentar cargar uno o varios módulos de extensión.") { }
}