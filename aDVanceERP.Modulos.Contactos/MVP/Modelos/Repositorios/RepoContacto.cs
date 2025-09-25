using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.Contactos.MVP.Modelos.Repositorios.Plantillas;
using MySql.Data.MySqlClient;

namespace aDVanceERP.Modulos.Contactos.MVP.Modelos.Repositorios;

public class RepoContacto : RepoEntidadBaseDatos<Contacto, FiltroBusquedaContacto>, IRepoContacto {
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
                WHERE id_contacto='{id}';

                DELETE FROM adv__telefono_contacto
                WHERE id_contacto='{id}';

                UPDATE adv__proveedor
                SET
                    id_contacto=0
                WHERE id_contacto='{id}';

                UPDATE adv__mensajero
                SET
                    id_contacto=0
                WHERE id_contacto='{id}';

                UPDATE adv__cliente
                SET
                    id_contacto=0
                WHERE id_contacto='{id}';

                UPDATE adv__empresa
                SET
                    id_contacto=0
                WHERE id_contacto='{id}';
                """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaContacto criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaContacto.Id:
                comando = $"SELECT * FROM adv__contacto WHERE id_contacto='{dato}';";
                break;
            case FiltroBusquedaContacto.Nombre:
                comando = $"SELECT * FROM adv__contacto WHERE LOWER(nombre) LIKE LOWER('%{dato}%');";
                break;
            default:
                comando = "SELECT * FROM adv__contacto;";
                break;
        }

        return comando;
    }

    protected override Contacto MapearEntidad(MySqlDataReader lectorDatos) {
        return new Contacto(
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_contacto")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("nombre")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("direccion_correo_electronico")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("direccion")),
            lectorDatos.GetValue(lectorDatos.GetOrdinal("notas")).ToString()
        );
    }
}