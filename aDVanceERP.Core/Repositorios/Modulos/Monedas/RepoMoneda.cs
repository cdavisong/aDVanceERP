using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Monedas;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Monedas {
    public class RepoMoneda : RepoEntidadBaseDatos<Moneda, FiltroBusquedaMoneda> {
        public RepoMoneda() : base("adv__moneda", "id_moneda") { }

        // ── CRUD ───────────────────────────────────────────────────────────────

        protected override string GenerarComandoAdicionar(
                Moneda objeto, out Dictionary<string, object> parametros,
                params IEntidadBaseDatos[] entidadesExtra) {
            parametros = new Dictionary<string, object> {
                    { "@codigo",            objeto.Codigo },
                    { "@nombre",            objeto.Nombre },
                    { "@simbolo",           objeto.Simbolo },
                    { "@precision_decimal", objeto.PrecisionDecimal },
                    { "@es_base",           objeto.EsBase ? 1 : 0 },
                    { "@activa",            objeto.Activa ? 1 : 0 }
                };

            return """
                INSERT INTO adv__moneda (
                    codigo, 
                    nombre, 
                    simbolo, 
                    precision_decimal, 
                    es_base, 
                    activa
                ) VALUES (
                    @codigo, 
                    @nombre, 
                    @simbolo, 
                    @precision_decimal, 
                    @es_base, 
                    @activa
                );
                """;
        }

        protected override string GenerarComandoEditar(
                Moneda objeto, out Dictionary<string, object> parametros,
                params IEntidadBaseDatos[] entidadesExtra) {
            parametros = new Dictionary<string, object> {
                { "@id",                objeto.Id },
                { "@codigo",            objeto.Codigo },
                { "@nombre",            objeto.Nombre },
                { "@simbolo",           objeto.Simbolo },
                { "@precision_decimal", objeto.PrecisionDecimal },
                { "@es_base",           objeto.EsBase ? 1 : 0 },
                { "@activa",            objeto.Activa ? 1 : 0 }
            };

            return """
                UPDATE adv__moneda
                SET codigo = @codigo, nombre = @nombre, simbolo = @simbolo,
                    precision_decimal = @precision_decimal, es_base = @es_base, activa = @activa
                WHERE id_moneda = @id;
                """;
        }

        protected override string GenerarComandoEliminar(
                long id, out Dictionary<string, object> parametros) {
            parametros = new Dictionary<string, object> { { "@id", id } };
            return "DELETE FROM adv__moneda WHERE id_moneda = @id;";
        }

        protected override string GenerarComandoObtener(
                FiltroBusquedaMoneda filtroBusqueda,
                out Dictionary<string, object> parametros,
                params string[] criterios) {
            var criterio = criterios.Length > 0 ? criterios[0] : string.Empty;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaMoneda.Id =>
                    "SELECT * FROM adv__moneda WHERE id_moneda = @id;",
                FiltroBusquedaMoneda.Codigo =>
                    "SELECT * FROM adv__moneda WHERE codigo = @codigo;",
                FiltroBusquedaMoneda.SoloActivas =>
                    "SELECT * FROM adv__moneda WHERE activa = 1 ORDER BY es_base DESC, codigo;",
                FiltroBusquedaMoneda.SoloBase =>
                    "SELECT * FROM adv__moneda WHERE es_base = 1 AND activa = 1 LIMIT 1;",
                _ =>
                    "SELECT * FROM adv__moneda ORDER BY es_base DESC, codigo;"
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaMoneda.Id => new() { { "@id", Convert.ToInt64(criterio.Length > 0 ? criterio : "0") } },
                FiltroBusquedaMoneda.Codigo => new() { { "@codigo", criterio } },
                _ => new()
            };

            return consulta;
        }

        protected override (Moneda, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader r) =>
            (new Moneda(
                id: Convert.ToInt64(r["id_moneda"]),
                codigo: Convert.ToString(r["codigo"]) ?? string.Empty,
                nombre: Convert.ToString(r["nombre"]) ?? string.Empty,
                simbolo: Convert.ToString(r["simbolo"]) ?? string.Empty,
                precisionDecimal: Convert.ToInt32(r["precision_decimal"]),
                esBase: Convert.ToBoolean(r["es_base"]),
                activa: Convert.ToBoolean(r["activa"])
            ), new List<IEntidadBaseDatos>());

        // ── SINGLETON ──────────────────────────────────────────────────────────
        public static RepoMoneda Instancia { get; } = new RepoMoneda();

        // ── UTILIDADES ─────────────────────────────────────────────────────────

        /// <summary>
        /// Devuelve la moneda marcada como base (es_base = 1).
        /// Solo debe existir una. Lanza excepción si no está configurada.
        /// </summary>
        public Moneda ObtenerMonedaBase() {
            var monedaBase = ObtenerTodos()
                .Select(r => r.entidadBase)
                .FirstOrDefault(m => m.EsBase && m.Activa)
                ?? throw new InvalidOperationException(
                    "No hay ninguna moneda configurada como base en adv__moneda. " +
                    "Asegúrese de que exactamente una fila tenga es_base = 1.");

            return monedaBase;
        }

        /// <summary>
        /// Devuelve solo las monedas activas ordenadas: base primero, luego alfabético.
        /// </summary>
        public List<Moneda> ObtenerActivas() =>
            ObtenerTodos()
                .Select(r => r.entidadBase)
                .Where(m => m.Activa)
                .OrderByDescending(m => m.EsBase)
                .ThenBy(m => m.Codigo)
                .ToList();
    }
}
