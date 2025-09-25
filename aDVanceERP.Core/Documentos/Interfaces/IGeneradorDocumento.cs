namespace aDVanceERP.Core.Documentos.Interfaces;

public enum FormatoDocumento {
    PDF,
    Excel
}

public interface IGeneradorDocumento {
    void GenerarDocumento(bool mostrar = true, FormatoDocumento formato = FormatoDocumento.PDF);
    void GenerarDocumentoConParametros(FormatoDocumento formato, params object[] parametros);
}

