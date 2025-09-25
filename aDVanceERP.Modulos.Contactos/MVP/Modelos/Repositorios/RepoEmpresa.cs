using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.Contactos.MVP.Modelos.Repositorios.Plantillas;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Modulos.Contactos.MVP.Modelos.Repositorios
{
    public class RepoEmpresa : RepoEntidadBaseDatos<Empresa, FiltroBusquedaEmpresa>, IRepoEmpresa {
        public RepoEmpresa() : base("adv__empresa", "id_empresa") { }

        protected override string GenerarComandoAdicionar(Empresa objeto) {
            return $"""
                INSERT INTO adv__empresa (
                    nombre,
                    logotipo,
                    id_contacto) 
                VALUES (
                    @nombre,
                    @logotipo,
                    @idContacto);
                """;
        }

        public override long Adicionar(Empresa objeto) {
            var logoBytes = objeto.ObtenerDatosDbLogotipo();

            using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();

                using (var comando = new MySqlCommand(GenerarComandoAdicionar(objeto), conexion)) {
                    comando.Parameters.AddWithValue("@nombre", objeto.Nombre);
                    comando.Parameters.Add("@logotipo", MySqlDbType.LongBlob).Value = logoBytes.Length > 0 ? logoBytes : DBNull.Value;
                    comando.Parameters.AddWithValue("@idContacto", objeto.IdContacto);

                    comando.ExecuteNonQuery();

                    return comando.LastInsertedId;
                }
            }
        }

        protected override string GenerarComandoEditar(Empresa objeto) {
            return $"""
                UPDATE adv__empresa
                SET
                    nombre = @nombre,
                    logotipo = @logotipo,
                    id_contacto = @idContacto
                WHERE id_empresa = {objeto.Id};
                """;
        }

        public override bool Editar(Empresa objeto, long nuevoId = 0) {
            var logoBytes = objeto.ObtenerDatosDbLogotipo();

            using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();

                using (var comando = new MySqlCommand(GenerarComandoEditar(objeto), conexion)) {
                    comando.Parameters.AddWithValue("@nombre", objeto.Nombre);
                    comando.Parameters.Add("@logotipo", MySqlDbType.LongBlob).Value = logoBytes.Length > 0 ? logoBytes : DBNull.Value;
                    comando.Parameters.AddWithValue("@idContacto", objeto.IdContacto);

                    comando.ExecuteNonQuery();

                    return true;
                }
            }
        }

        protected override string GenerarComandoEliminar(long id) {
            return $"""
                DELETE FROM adv__empresa 
                WHERE id_empresa = {id};
                """;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaEmpresa criterio, string dato) {
            string? comando;

            switch (criterio) {
                case FiltroBusquedaEmpresa.Id:
                    comando = $"SELECT * FROM adv__empresa WHERE id_empresa='{dato}';";
                    break;
                case FiltroBusquedaEmpresa.Nombre:
                    comando = $"SELECT * FROM adv__empresa WHERE LOWER(nombre) LIKE LOWER('%{dato}%');";
                    break;
                default:
                    comando = "SELECT * FROM adv__empresa;";
                    break;
            }

            return comando;
        }

        protected override Empresa MapearEntidad(MySqlDataReader lectorDatos) {
            var empresa = new Empresa(
                lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_empresa")),
                null,
                lectorDatos.GetString(lectorDatos.GetOrdinal("nombre")),
                long.TryParse(lectorDatos.GetValue(lectorDatos.GetOrdinal("id_contacto")).ToString(), out var idContacto)
                    ? idContacto
                    : 0
            );

            if (!lectorDatos.IsDBNull(lectorDatos.GetOrdinal("logotipo"))) {
                var bytesImagen = (byte[]) lectorDatos["logotipo"];

                if (!EsImagenValida(bytesImagen)) {
                    System.Diagnostics.Debug.WriteLine("Advertencia: Datos de imagen no válidos en BD");
                    bytesImagen = Array.Empty<byte>();
                }

                empresa.EstablecerLogotipoDesdeBytes(bytesImagen);
            }

            return empresa;
        }

        private bool EsImagenValida(byte[] bytes) {
            try {
                using (var ms = new MemoryStream(bytes)) {
                    // Intenta leer solo los primeros bytes para verificar el formato
                    var header = new byte[8];
                    ms.Read(header, 0, 8);

                    // Verificar firmas de formatos comunes
                    if (header.Take(8).SequenceEqual(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A })) // PNG
                        return true;
                    if (header.Take(2).SequenceEqual(new byte[] { 0xFF, 0xD8 })) // JPEG
                        return true;
                }
                return false;
            } catch {
                return false;
            }
        }
    }
}
