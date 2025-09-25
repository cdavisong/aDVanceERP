using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.Contactos.MVP.Modelos.Repositorios.Plantillas;
using MySql.Data.MySqlClient;

namespace aDVanceERP.Modulos.Contactos.MVP.Modelos.Repositorios;

public class RepoCliente : RepoEntidadBaseDatos<Cliente, FiltroBusquedaCliente>, IRepoCliente {
    public RepoCliente() : base("adv__cliente", "id_cliente") { }

    protected override string GenerarComandoAdicionar(Cliente objeto) {
        return $"INSERT INTO adv__cliente (numero, razon_social, id_contacto) VALUES ('{objeto.Numero}', '{objeto.RazonSocial}', '{objeto.IdContacto}');";
    }

    protected override string GenerarComandoEditar(Cliente objeto) {
        return $"UPDATE adv__cliente SET numero='{objeto.Numero}', razon_social='{objeto.RazonSocial}', id_contacto='{objeto.IdContacto}' WHERE id_cliente='{objeto.Id}';";
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"DELETE FROM adv__cliente WHERE id_cliente={id};";
    }

    protected override string GenerarComandoObtener(FiltroBusquedaCliente criterio, string dato) {
        string? comando;

        switch (criterio) {
            case FiltroBusquedaCliente.Id:
                comando = $"SELECT * FROM adv__cliente WHERE id_cliente='{dato}';";
                break;
            case FiltroBusquedaCliente.RazonSocial:
                comando = $"SELECT * FROM adv__cliente WHERE LOWER(razon_social) LIKE LOWER('%{dato}%');";
                break;
            case FiltroBusquedaCliente.Numero:
                comando = $"SELECT * FROM adv__cliente WHERE LOWER(numero) LIKE LOWER('%{dato}%');";
                break;
            default:
                comando = "SELECT * FROM adv__cliente;";
                break;
        }

        return comando;
    }

    protected override Cliente MapearEntidad(MySqlDataReader lectorDatos) {
        return new Cliente(
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_cliente")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("numero")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("razon_social")),
            long.TryParse(lectorDatos.GetValue(lectorDatos.GetOrdinal("id_contacto")).ToString(), out var idContacto)
                ? idContacto
                : 0
        );
    }
}