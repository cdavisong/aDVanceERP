using aDVanceERP.Core.Modelos.Modulos.Contactos;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Contactos;

public class RepoProveedor : RepoEntidadBaseDatos<Proveedor, FiltroBusquedaProveedor> {
    public RepoProveedor() : base("adv__proveedor", "id_proveedor") { }

    protected override string GenerarComandoAdicionar(Proveedor objeto) {
        return $"""
            INSERT INTO adv__proveedor (
                razon_social, 
                nit, 
                id_contacto
            ) VALUES (
                '{objeto.RazonSocial}', 
                '{objeto.NumeroIdentificacionTributaria}', 
                {objeto.IdContacto}
            );
            """;
    }

    protected override string GenerarComandoEditar(Proveedor objeto) {
        return $"""
            UPDATE adv__proveedor 
            SET 
                razon_social = '{objeto.RazonSocial}', 
                nit = '{objeto.NumeroIdentificacionTributaria}', 
                id_contacto = {objeto.IdContacto} 
            WHERE id_proveedor = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__proveedor 
            WHERE id_proveedor = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaProveedor criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaProveedor.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__proveedor 
                    WHERE id_proveedor = {dato};
                    """;
                break;
            case FiltroBusquedaProveedor.RazonSocial:
                comando = $"""
                    SELECT * 
                    FROM adv__proveedor 
                    WHERE LOWER(razon_social) LIKE LOWER('%{dato}%');
                    """;
                break;
            case FiltroBusquedaProveedor.NIT:
                comando = $"""
                    SELECT * 
                    FROM adv__proveedor 
                    WHERE LOWER(nit) LIKE LOWER('%{dato}%');
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__proveedor;
                    """;
                break;
        }

        return comando;
    }

    protected override Proveedor MapearEntidad(MySqlDataReader lector) {
        return new Proveedor(
            id: Convert.ToInt64(lector["id_proveedor"]),
            razonSocial: Convert.ToString(lector["razon_social"]) ?? string.Empty,
            numeroIdentificacionTributaria: Convert.ToString(lector["nit"]) ?? string.Empty,
            idContacto: lector["id_contacto"] != DBNull.Value ? Convert.ToInt64(lector["id_contacto"]) : 0
        );
    }

    #region STATIC

    public static RepoProveedor Instancia { get; } = new RepoProveedor();

    #endregion
}