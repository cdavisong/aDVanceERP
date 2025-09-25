namespace aDVanceERP.Core.Excepciones; 

public class ExcepcionConexionServidorFTP : Exception {
    public ExcepcionConexionServidorFTP()
        : base("Se produjo un error al conectar con el servidor FTP. Si el problema persiste, póngase en contacto con un administrador.") { }
}