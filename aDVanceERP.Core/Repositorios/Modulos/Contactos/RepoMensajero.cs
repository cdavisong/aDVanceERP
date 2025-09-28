using aDVanceERP.Core.Modelos.Modulos.Contactos;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Contactos;

public class RepoMensajero : RepoEntidadBaseDatos<Mensajero, FiltroBusquedaMensajero> {
    public RepoMensajero() : base("adv__mensajero", "id_mensajero") { }

    protected override string GenerarComandoAdicionar(Mensajero objeto) {
        return $"""
                INSERT INTO adv__mensajero (
                    nombre,
                    activo,
                    id_contacto
                )
                VALUES (
                    '{objeto.Nombre}',
                    '{(objeto.Activo ? 1 : 0)}',
                    {objeto.IdContacto}
                );
                """;
    }

    protected override string GenerarComandoEditar(Mensajero objeto) {
        return $"""
                UPDATE adv__mensajero
                SET
                    nombre = '{objeto.Nombre}',
                    activo = '{(objeto.Activo ? 1 : 0)}',
                    id_contacto = {objeto.IdContacto}
                WHERE id_mensajero = {objeto.Id};
                """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
                DELETE FROM adv__mensajero
                WHERE id_mensajero = {id};
                """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaMensajero criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaMensajero.Id:
                comando = $"""
                    SELECT *
                    FROM adv__mensajero
                    WHERE id_mensajero={dato};
                    """;
                break;
            case FiltroBusquedaMensajero.Nombre:
                comando = $"""
                    SELECT *
                    FROM adv__mensajero
                    WHERE LOWER(nombre) LIKE LOWER('%{dato}%');
                    """;
                break;
            default:
                comando = """
                    SELECT *
                    FROM adv__mensajero;
                    """;
                break;
        }

        return comando;
    }

    protected override Mensajero MapearEntidad(MySqlDataReader lectorDatos) {
        return new Mensajero(
            id: Convert.ToInt64(lectorDatos["id_mensajero"]),
            nombre: Convert.ToString(lectorDatos["nombre"]) ?? string.Empty,
            activo: Convert.ToBoolean(lectorDatos["activo"]),
            idContacto: lectorDatos["id_contacto"] != DBNull.Value ? Convert.ToInt64(lectorDatos["id_contacto"]) : 0
        );
    }
}