using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Repositorios.Comun.Interfaces;
using Microsoft.Extensions.Caching.Memory;

using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace aDVanceERP.Core.Repositorios.BD;

public abstract class RepoEntidadBaseDatos<En, Fb> : IRepoEntidadBaseDatos<En, Fb>
    where En : class, IEntidadBaseDatos, new()
    where Fb : Enum {
    private readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

    protected RepoEntidadBaseDatos(string nombreTabla, string columnaId) {
        NombreTabla = nombreTabla;
        ColumnaId = columnaId;
    }

    protected string NombreTabla { get; }

    protected string ColumnaId { get; }

    #region Obtención de datos y búsqueda de entidades

    public En? ObtenerPorId(object id) {
        var cacheKey = $"{NombreTabla}_Id_{id}";

        if (_cache.TryGetValue(cacheKey, out En? cachedEntity))
            return cachedEntity;

        var consulta = $"SELECT * FROM {NombreTabla} WHERE {ColumnaId} = @id LIMIT 1";
        var parametros = new Dictionary<string, object> { { "@id", id } };

        var entidad = ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearEntidad).FirstOrDefault();

        if (entidad != null)
            _cache.Set(cacheKey, entidad, _cacheDuration);

        return entidad;
    }

    public List<En> ObtenerTodos() {
        var consulta = $"SELECT * FROM {NombreTabla}";
        var resultados = ContextoBaseDatos.EjecutarConsulta(consulta, null, MapearEntidad).ToList();

        return resultados;
    }

    public (int cantidad, List<En> entidades) Buscar(string? consulta = "", int limite = 0, int desplazamiento = 0) {
        // Manejar consultas vacías o nulas
        if (string.IsNullOrEmpty(consulta))
            consulta = $"SELECT * FROM {NombreTabla}";

        var consultaCantidad = GenerarConsultaConteo(consulta);
        var consultaResultados = string.IsNullOrEmpty(consulta) ? GenerarComandoObtener(default, string.Empty) : consulta;

        var parametros = new Dictionary<string, object>();

        if (limite > 0) {
            consultaResultados = consultaResultados.TrimEnd(';');
            consultaResultados += " LIMIT @limite OFFSET @desplazamiento;";

            parametros.Add("@limite", limite);
            parametros.Add("@desplazamiento", desplazamiento);
        }

        int cantidad = 0;
        List<En> entidades = new List<En>();

        using (var conexion = ContextoBaseDatos.ObtenerConexionOptimizada()) {
            conexion.Open();

            cantidad = ContextoBaseDatos.EjecutarConsultaEscalar<int>(consultaCantidad, null, conexion);
            entidades.AddRange(ContextoBaseDatos.EjecutarConsulta(consultaResultados, parametros, MapearEntidad, conexion).ToList());

            conexion.Close();
        }         

        return (cantidad, entidades);
    }

    public (int cantidad, List<En> entidades) Buscar(Fb? filtroBusqueda, string? criterio, int limite = 0, int desplazamiento = 0) {
        var comando = GenerarComandoObtener(filtroBusqueda, criterio);

        return Buscar(comando, limite, desplazamiento);
    }

    #endregion

    #region CRUD

    public virtual long Adicionar(En objeto) {
        return ContextoBaseDatos.EjecutarComandoInsert(GenerarComandoAdicionar(objeto), new Dictionary<string, object>());
    }

    public virtual bool Editar(En objeto, long nuevoId = 0) {
        ContextoBaseDatos.EjecutarComandoNoQuery(GenerarComandoEditar(objeto), new Dictionary<string, object>());
        return true;
    }

    public virtual bool Eliminar(long id) {
        ContextoBaseDatos.EjecutarComandoNoQuery(GenerarComandoEliminar(id), new Dictionary<string, object>());
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

    protected abstract string GenerarComandoAdicionar(En objeto);
    protected abstract string GenerarComandoEditar(En objeto);
    protected abstract string GenerarComandoEliminar(long id);
    protected abstract string GenerarComandoObtener(Fb filtroBusqueda, string criterio);
    protected abstract En MapearEntidad(MySqlDataReader lectorDatos);

    #endregion

    #region Auxiliares

    private string GenerarConsultaConteo(string consultaOriginal) {
        // Regex para capturar toda la cláusula SELECT (desde SELECT hasta FROM)
        var regex = new Regex(@"SELECT\s+.*?(?=\s+FROM)", RegexOptions.IgnoreCase);

        // Reemplazar toda la cláusula SELECT por COUNT(*)
        var consultaModificada = regex.Replace(consultaOriginal, "SELECT COUNT(*) AS total_filas");

        return consultaModificada;
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