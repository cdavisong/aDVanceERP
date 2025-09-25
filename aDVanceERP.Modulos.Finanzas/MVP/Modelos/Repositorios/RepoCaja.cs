using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos.Repositorios.Plantillas;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Modulos.Finanzas.MVP.Modelos.Repositorios {
    public class RepoCaja : RepoEntidadBaseDatos<Caja, FiltroBusquedaCaja>, IRepoCaja {
        public RepoCaja() : base("adv__caja", "id_caja") { }

        protected override string GenerarComandoAdicionar(Caja objeto) {
            return $"INSERT INTO adv__caja (fecha_apertura, saldo_inicial, saldo_actual, fecha_cierre, estado, id_cuenta_usuario) VALUES ('{objeto.FechaApertura:yyyy-MM-dd HH:mm:ss}', {objeto.SaldoInicial.ToString(CultureInfo.InvariantCulture)}, {objeto.SaldoActual.ToString(CultureInfo.InvariantCulture)}, '{objeto.FechaCierre:yyyy-MM-dd HH:mm:ss}', '{objeto.Estado}', {objeto.IdCuentaUsuario});";
        }

        protected override string GenerarComandoEditar(Caja objeto) {
            return $"UPDATE adv__caja SET estado='{objeto.Estado}', saldo_actual={objeto.SaldoActual.ToString(CultureInfo.InvariantCulture)}, fecha_cierre='{objeto.FechaCierre:yyyy-MM-dd HH:mm:ss}' WHERE id_caja={objeto.Id};";
        }

        protected override string GenerarComandoEliminar(long id) {
            return $@"
                DELETE FROM adv__movimiento_caja WHERE id_caja={id};
                DELETE FROM adv__caja WHERE id_caja={id};
            ";
        }

        protected override string GenerarComandoObtener(FiltroBusquedaCaja criterio, string dato) {
            switch (criterio) {
                case FiltroBusquedaCaja.Id:
                    return $"SELECT * FROM adv__caja WHERE id_caja={dato};";
                case FiltroBusquedaCaja.FechaApertura:
                    return $"SELECT * FROM adv__caja WHERE DATE(fecha_apertura) = '{dato}';";
                case FiltroBusquedaCaja.Estado:
                    return $"SELECT * FROM adv__caja WHERE estado='{dato}';";
                case FiltroBusquedaCaja.FechaCierre:
                    return $"SELECT * FROM adv__caja WHERE DATE(fecha_cierre) = '{dato}';";
                default:
                    return "SELECT * FROM adv__caja;";
            }
        }

        protected override Caja MapearEntidad(MySqlDataReader lectorDatos) {
            return new Caja(
                lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_caja")),
                lectorDatos.GetDateTime(lectorDatos.GetOrdinal("fecha_apertura")),
                lectorDatos.GetDecimal(lectorDatos.GetOrdinal("saldo_inicial")),
                lectorDatos.GetDecimal(lectorDatos.GetOrdinal("saldo_actual")),
                lectorDatos.GetDateTime(lectorDatos.GetOrdinal("fecha_cierre")),
                lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_cuenta_usuario"))
            ) {
                Estado = (EstadoCaja) Enum.Parse(typeof(EstadoCaja),
                lectorDatos.GetString(lectorDatos.GetOrdinal("estado"))),
            };
        }
    }
}
