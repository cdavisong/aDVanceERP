using aDVanceERP.Core.Modelos.Modulos.Contactos;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Contactos;

public class RepoContacto : RepoEntidadBaseDatos<Contacto, FiltroBusquedaContacto> {
    public RepoContacto() : base("adv__contacto", "id_contacto") { }

    protected override string GenerarComandoAdicionar(Contacto objeto) {
        return $"""
                INSERT INTO adv__contacto (
                    nombre,
                    direccion_correo_electronico,
                    direccion,
                    notas
                )
                VALUES (
                    '{objeto.Nombre}',
                    '{objeto.DireccionCorreoElectronico}',
                    '{objeto.Direccion}',
                    '{objeto.Notas}'
                );
                """;
    }

    protected override string GenerarComandoEditar(Contacto objeto) {
        return $"""
                UPDATE adv__contacto
                SET
                    nombre='{objeto.Nombre}',
                    direccion_correo_electronico='{objeto.DireccionCorreoElectronico}',
                    direccion='{objeto.Direccion}',
                    notas='{objeto.Notas}'
                WHERE id_contacto='{objeto.Id}';
                """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
                DELETE FROM adv__contacto
                WHERE id_contacto = {id};

                DELETE FROM adv__telefono_contacto
                WHERE id_contacto = {id};

                UPDATE adv__proveedor
                SET
                    id_contacto = 0
                WHERE id_contacto = {id};

                UPDATE adv__mensajero
                SET
                    id_contacto=0
                WHERE id_contacto = {id};

                UPDATE adv__cliente
                SET
                    id_contacto = 0
                WHERE id_contacto = {id};

                UPDATE adv__empresa
                SET
                    id_contacto = 0
                WHERE id_contacto = {id};
                """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaContacto criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaContacto.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__contacto 
                    WHERE id_contacto = {dato};
                    """;
                break;
            case FiltroBusquedaContacto.Nombre:
                comando = $"""
                    SELECT * 
                    FROM adv__contacto 
                    WHERE LOWER(nombre) LIKE LOWER('%{dato}%');
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__contacto;
                    """;
                break;
        }

        return comando;
    }

    protected override Contacto MapearEntidad(MySqlDataReader lector) {
        return new Contacto(
            id: Convert.ToInt64(lector["id_contacto"]),
            nombre: lector["nombre"]?.ToString() ?? string.Empty,
            direccionCorreoElectronico: lector["direccion_correo_electronico"]?.ToString() ?? string.Empty,
            direccion: lector["direccion"]?.ToString() ?? "No disponible",
            notas: lector["notas"]?.ToString() ?? string.Empty
        );
    }
}