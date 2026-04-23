using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Repositorios.Comun.Interfaces;

using Microsoft.Extensions.Caching.Memory;

using MySql.Data.MySqlClient;

using System.Text.RegularExpressions;

namespace aDVanceERP.Core.Repositorios.BD {
    public abstract class RepoEntidadBaseDatos<En, Fb> : IRepoEntidadBaseDatos<En, Fb>
        where En : class, IEntidadBaseDatos, new()
        where Fb : Enum {
        private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

        protected RepoEntidadBaseDatos(string nombreTabla, string columnaId) {
            NombreTabla = nombreTabla;
            ColumnaId = columnaId;
        }

        protected string NombreTabla { get; }

        protected string ColumnaId { get; }

        #region Obtención de datos y búsqueda de entidades

        public int Limite { get; set; }

        public int Desplazamiento { get; set; }

        public En? ObtenerPorId(object id) {
            var cacheKey = $"{NombreTabla}_Id_{id}";

            if (_cache.TryGetValue(cacheKey, out En? cachedEntity))
                return cachedEntity;

            var consulta = $"SELECT * FROM {NombreTabla} WHERE {ColumnaId} = @id LIMIT 1";
            var parametros = new Dictionary<string, object> {
                { "@id", id }
            };

            var entidad = ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearEntidad).FirstOrDefault().entidadBase;

            if (entidad != null)
                _cache.Set(cacheKey, entidad, _cacheDuration);

            return entidad;
        }

        List<(En entidadBase, List<IEntidadBase> entidadesExtra)> IRepoBase<En>.ObtenerTodos() {
            return new List<(En entidadBase, List<IEntidadBase> entidadesExtra)>();
        }

        public List<(En entidadBase, List<IEntidadBaseDatos> entidadesExtra)> ObtenerTodos() {
            var consulta = $"SELECT * FROM {NombreTabla}";
            var resultados = ContextoBaseDatos.EjecutarConsulta(consulta, null, MapearEntidad).ToList();

            return resultados;
        }

        public (int cantidad, List<(En entidadBase, List<IEntidadBaseDatos> entidadesExtra)> resultadosBusqueda) Buscar(Fb? filtroBusqueda, params string[] criteriosBusqueda) {
            var comando = GenerarComandoObtener(filtroBusqueda, out var parametros, criteriosBusqueda);

            return Buscar(comando, parametros);
        }

        private (int cantidad, List<(En entidadBase, List<IEntidadBaseDatos> entidadesExtra)> resultadosBusqueda) Buscar(string? consulta = "", Dictionary<string, object> parametros = null) {
            // Manejar consultas vacías o nulas
            if (string.IsNullOrEmpty(consulta))
                consulta = $"SELECT * FROM {NombreTabla}";

            var parametrosConsulta = parametros ?? new Dictionary<string, object>();
            var consultaCantidad = GenerarConsultaConteo(consulta);
            var consultaResultados = string.IsNullOrEmpty(consulta) ? GenerarComandoObtener(default, out parametrosConsulta, string.Empty) : consulta;

            if (Limite > 0) {
                consultaResultados = consultaResultados.TrimEnd(';');
                consultaResultados += "\nLIMIT @limite OFFSET @desplazamiento;";

                parametrosConsulta.Add("@limite", Limite);
                parametrosConsulta.Add("@desplazamiento", Desplazamiento);
            }

            int cantidad = 0;
            List<(En entidadBase, List<IEntidadBaseDatos> entidadesExtra)> entidades = new List<(En entidadBase, List<IEntidadBaseDatos> entidadesExtra)>();

            using (var conexion = ContextoBaseDatos.ObtenerConexionOptimizada()) {
                conexion.Open();

                cantidad = ContextoBaseDatos.EjecutarConsultaEscalar<int>(consultaCantidad, parametrosConsulta, conexion);
                entidades.AddRange(ContextoBaseDatos.EjecutarConsulta(consultaResultados, parametrosConsulta, MapearEntidad, conexion).ToList());

                conexion.Close();
            }

            return (cantidad, entidades);
        }

        #endregion

        #region CRUD

        public virtual long Adicionar(En objeto, params IEntidadBaseDatos[] entidadesExtra) {
            var id = ContextoBaseDatos.EjecutarComandoInsert(GenerarComandoAdicionar(objeto, out var parametros, entidadesExtra), parametros);
            _cache.Remove($"{NombreTabla}_Id_{id}"); // limpiar si por alguna razón existía
            return id;
        }

        public virtual bool Editar(En objeto, params IEntidadBaseDatos[] entidadesExtra) {
            ContextoBaseDatos.EjecutarComandoNoQuery(GenerarComandoEditar(objeto, out var parametros, entidadesExtra), parametros);
            _cache.Remove($"{NombreTabla}_Id_{objeto.Id}"); // invalidar entrada editada
            return true;
        }

        public virtual bool Eliminar(long id) {
            ContextoBaseDatos.EjecutarComandoNoQuery(GenerarComandoEliminar(id, out var parametros), parametros);
            _cache.Remove($"{NombreTabla}_Id_{id}");
            return true;
        }

        #endregion

        #region Utilidades

        public long Cantidad() {
            var consulta = $"SELECT COUNT(*) FROM {NombreTabla}";

            return ContextoBaseDatos.EjecutarConsultaEscalar<long>(consulta, new Dictionary<string, object>());
        }

        public bool Existe(long id) {
            var consulta = $"SELECT COUNT(*) FROM {NombreTabla} WHERE {ColumnaId} = @id";
            var parametros = new Dictionary<string, object> {
                { "@id", id }
            };
            var cantidad = ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta, parametros);

            return cantidad > 0;
        }

        #endregion

        #region Métodos abstractos para heredar

        protected abstract string GenerarComandoAdicionar(En entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra);
        protected abstract string GenerarComandoEditar(En entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra);
        protected abstract string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros);
        protected abstract string GenerarComandoObtener(Fb filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda);
        protected abstract (En, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector);

        #endregion

        #region Auxiliares

        private string GenerarConsultaConteo(string consultaOriginal) {
            // Eliminar punto y coma final si existe
            string consultaLimpia = consultaOriginal.TrimEnd();
            if (consultaLimpia.EndsWith(";"))
                consultaLimpia = consultaLimpia.Substring(0, consultaLimpia.Length - 1);

            // Eliminar ORDER BY (no afecta el conteo)
            var orderByMatch = Regex.Match(consultaLimpia, @"\s+ORDER\s+BY\s+.+$", RegexOptions.IgnoreCase);
            if (orderByMatch.Success) {
                consultaLimpia = consultaLimpia.Substring(0, orderByMatch.Index);
            }

            // Si la consulta tiene GROUP BY, DISTINCT, UNION, etc., envolver como subconsulta
            bool necesitaSubconsulta = Regex.IsMatch(consultaLimpia, @"\bGROUP\s+BY\b", RegexOptions.IgnoreCase)
                                    || Regex.IsMatch(consultaLimpia, @"\bDISTINCT\b", RegexOptions.IgnoreCase)
                                    || Regex.IsMatch(consultaLimpia, @"\bUNION\b", RegexOptions.IgnoreCase);

            if (necesitaSubconsulta) {
                return $"SELECT COUNT(*) AS total_filas FROM ({consultaLimpia}) AS subconsulta";
            }

            // Método original para consultas simples
            var fromIndex = FindFromPosition(consultaLimpia);
            if (fromIndex >= 0) {
                return "SELECT COUNT(*) AS total_filas " + consultaLimpia.Substring(fromIndex);
            }

            return $"SELECT COUNT(*) AS total_filas FROM ({consultaOriginal}) AS subconsulta";
        }

        private int FindFromPosition(string consulta) {
            int nivelParentesis = 0;

            for (int i = 0; i < consulta.Length; i++) {
                char c = consulta[i];

                if (c == '(') nivelParentesis++;
                else if (c == ')') nivelParentesis--;

                // Buscar "FROM" solo cuando no estamos dentro de paréntesis
                if (nivelParentesis == 0 &&
                    i + 4 < consulta.Length &&
                    consulta.Substring(i, 4).Equals("FROM", StringComparison.OrdinalIgnoreCase)) {

                    // Verificar que no sea parte de una palabra más larga
                    if (i > 0 && char.IsLetterOrDigit(consulta[i - 1])) continue;

                    return i;
                }
            }

            return -1;
        }

        #endregion

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
        }

        ~RepoEntidadBaseDatos() {
            Dispose(false);
        }
    }
}