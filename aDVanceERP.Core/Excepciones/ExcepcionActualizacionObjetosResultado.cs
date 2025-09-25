namespace aDVanceERP.Core.Excepciones;

public class ExcepcionActualizacionObjetosResultado : Exception {
    public ExcepcionActualizacionObjetosResultado()
        : base("Se produjo un error al intentar refrescar la lista de objetos.") { }
}
