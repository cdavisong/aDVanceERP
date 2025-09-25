namespace aDVanceERP.Core.Excepciones; 

public class ExcepcionConexionServidorMySQL : Exception {
    public ExcepcionConexionServidorMySQL()
        : base("Se produjo un error al conectar con el servidor MySQL. Si el problema persiste, póngase en contacto con un administrador.") { }
}