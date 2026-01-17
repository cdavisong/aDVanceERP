using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;

public class RepoTelefonoContacto : RepoEntidadBaseDatos<TelefonoContacto, FiltroBusquedaTelefonoContacto> {
    public RepoTelefonoContacto() : base("adv__telefono_contacto", "id_telefono_contacto") { }

    protected override string GenerarComandoAdicionar(TelefonoContacto objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
        var consulta = $"""
            INSERT INTO adv__telefono_contacto (
                prefijo, 
                numero, 
                categoria, 
                id_persona
            ) VALUES (
                @prefijo,
                @numero,
                @categoria,
                @id_persona
            );
            """;

        parametros = new Dictionary<string, object> {
            { "@prefijo", objeto.PrefijoPais },
            { "@numero", objeto.NumeroTelefono },
            { "@categoria", objeto.Categoria.ToString() },
            { "@id_persona", objeto.IdPersona }
        };

        return consulta;
    }

    protected override string GenerarComandoEditar(TelefonoContacto objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
        var consulta = $"""
            UPDATE adv__telefono_contacto 
            SET 
                prefijo = @prefijo, 
                numero = @numero, 
                categoria = @categoria, 
                id_persona = @id_persona 
            WHERE id_telefono_contacto = @id_telefono_contacto;
            """;

        parametros = new Dictionary<string, object> {
            { "@prefijo", objeto.PrefijoPais },
            { "@numero", objeto.NumeroTelefono },
            { "@categoria", objeto.Categoria.ToString() },
            { "@id_persona", objeto.IdPersona },
            { "@id_telefono_contacto", objeto.Id }
        };

        return consulta;
    }

    protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
        var consulta = $"""
            DELETE FROM adv__telefono_contacto 
            WHERE id_telefono_contacto = @id_telefono_contacto;
            """;

        parametros = new Dictionary<string, object> {
            { "@id_telefono_contacto", id }
        };

        return consulta;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaTelefonoContacto filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
        var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
        var consulta = filtroBusqueda switch {
            FiltroBusquedaTelefonoContacto.Id => $"""
                SELECT * 
                FROM adv__telefono_contacto 
                WHERE id_telefono_contacto = @id_telefono_contacto;
                """,
            FiltroBusquedaTelefonoContacto.PrefijoPais => $"""
                SELECT * 
                FROM adv__telefono_contacto 
                WHERE prefijo = @prefijo;
                """,
            FiltroBusquedaTelefonoContacto.NumeroTelefono => $"""
                SELECT * 
                FROM adv__telefono_contacto 
                WHERE numero = @numero;
                """,
            FiltroBusquedaTelefonoContacto.Categoria => $"""
                SELECT * 
                FROM adv__telefono_contacto 
                WHERE categoria = @categoria;
                """,
            FiltroBusquedaTelefonoContacto.IdPersona => $"""
                SELECT * 
                FROM adv__telefono_contacto 
                WHERE id_persona = @id_persona;
                """,
            _ => """
                SELECT * 
                FROM adv__telefono_contacto;
                """
        };

        parametros = filtroBusqueda switch {
            FiltroBusquedaTelefonoContacto.Id => new Dictionary<string, object> {
                { "@id_telefono_contacto", Convert.ToInt64(criterio) }
            },
            FiltroBusquedaTelefonoContacto.PrefijoPais => new Dictionary<string, object> {
                { "@prefijo", criterio }
            },
            FiltroBusquedaTelefonoContacto.NumeroTelefono => new Dictionary<string, object> {
                { "@numero", criterio }
            },
            FiltroBusquedaTelefonoContacto.Categoria => new Dictionary<string, object> {
                { "@categoria", criterio }
            },
            FiltroBusquedaTelefonoContacto.IdPersona => new Dictionary<string, object> {
                { "@id_persona", Convert.ToInt64(criterio) }
            },
            _ => new Dictionary<string, object>()
        };

        return consulta;
    }

    protected override (TelefonoContacto, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
        return (new TelefonoContacto(
            id: Convert.ToInt64(lector["id_telefono_contacto"]),
            prefijoPais: Convert.ToString(lector["prefijo"]) ?? string.Empty,
            numeroTelefono: Convert.ToString(lector["numero"]) ?? string.Empty,
            categoria: Enum.TryParse<CategoriaTelefonoContacto>(Convert.ToString(lector["categoria"]) ?? string.Empty, out var categoria) ? categoria : CategoriaTelefonoContacto.Otro,
            idPersona: Convert.ToInt64(lector["id_persona"])
        ), new List<IEntidadBaseDatos>());
    }

    #region STATIC

    public static RepoTelefonoContacto Instancia { get; } = new RepoTelefonoContacto();

    #endregion
}