using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario {
    public class RepoInventario : RepoEntidadBaseDatos<Modelos.Modulos.Inventario.Inventario, FiltroBusquedaInventario> {
        private Producto? _producto;

        public RepoInventario() : base("adv__inventario", "id_inventario") { }

        protected override string GenerarComandoAdicionar(Modelos.Modulos.Inventario.Inventario entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                INSERT INTO adv__inventario (
                id_producto, 
                id_almacen, 
                cantidad,
                costo_promedio,
                valor_total
            ) 
            VALUES (
                @id_producto,
                @id_almacen,
                @cantidad,
                @costo_promedio,
                @valor_total
            );
            """;

            parametros = new Dictionary<string, object> {
                { "@id_producto", entidad.IdProducto },
                { "@id_almacen", entidad.IdAlmacen },
                { "@cantidad", entidad.Cantidad },
                { "@costo_promedio", entidad.CostoPromedio },
                { "@valor_total", entidad.ValorTotal }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(Modelos.Modulos.Inventario.Inventario objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                UPDATE adv__inventario 
            SET 
                id_producto = @id_producto, 
                id_almacen = @id_almacen, 
                cantidad = @cantidad,
                costo_promedio = @costo_promedio,
                valor_total = @valor_total
            WHERE id_inventario = @id_inventario;
            """;

            parametros = new Dictionary<string, object> {
                { "@id_producto", objeto.IdProducto },
                { "@id_almacen", objeto.IdAlmacen },
                { "@cantidad", objeto.Cantidad },
                { "@costo_promedio", objeto.CostoPromedio },
                { "@valor_total", objeto.ValorTotal },
                { "@id_inventario", objeto.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__inventario 
            WHERE id_inventario = @id_inventario;
            """;

            parametros = new Dictionary<string, object> {
                { "@id_inventario", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaInventario filtroBusqueda, out Dictionary<string, object> parametros, string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaInventario.Id => $"""
                    SELECT * 
                    FROM adv__inventario 
                    WHERE id_inventario = @id;
                    """,
                FiltroBusquedaInventario.IdProducto => $"""
                    SELECT * 
                    FROM adv__inventario 
                    WHERE id_producto = @id_producto;
                    """,
                FiltroBusquedaInventario.IdAlmacen => $"""
                    SELECT * 
                    FROM adv__inventario 
                    WHERE id_almacen = @id_almacen;
                    """,
                _ => """
                    SELECT * 
                    FROM adv__inventario;
                    """
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaInventario.Id => new Dictionary<string, object> {
                    { "@id", Convert.ToInt64(criterio) }
                },
                FiltroBusquedaInventario.IdProducto => new Dictionary<string, object> {
                    { "@id_producto", Convert.ToInt64(criterio) }
                },
                FiltroBusquedaInventario.IdAlmacen => new Dictionary<string, object> {
                    { "@id_almacen", Convert.ToInt64(criterio) }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (Modelos.Modulos.Inventario.Inventario, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lectorDatos) {
            return (new Modelos.Modulos.Inventario.Inventario(
               id: Convert.ToInt64(lectorDatos["id_inventario"]),
               idProducto: Convert.ToInt64(lectorDatos["id_producto"]),
               idAlmacen: Convert.ToInt64(lectorDatos["id_almacen"]!),
               cantidad: Convert.ToDecimal(lectorDatos["cantidad"], CultureInfo.InvariantCulture),
               costoPromedio: Convert.ToDecimal(lectorDatos["costo_promedio"], CultureInfo.InvariantCulture),
               valorTotal: Convert.ToDecimal(lectorDatos["valor_total"], CultureInfo.InvariantCulture),
               ultimaActualizacion: Convert.ToDateTime(lectorDatos["ultima_actualizacion"], CultureInfo.InvariantCulture)
           ), new List<IEntidadBaseDatos>());
        }

        #region STATIC

        public static RepoInventario Instancia { get; } = new RepoInventario();

        #endregion

        #region UTILES  

        public void ModificarInventario(string nombreProducto, string nombreAlmacenOrigen, string nombreAlmacenDestino, decimal cantidad) {
            _producto = RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, nombreProducto).resultadosBusqueda.FirstOrDefault(p => p.entidadBase.Nombre.Equals(nombreProducto)).entidadBase;
        
            var almacenOrigen = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, nombreAlmacenOrigen).resultadosBusqueda.FirstOrDefault(a => a.entidadBase.Nombre.Equals(nombreAlmacenOrigen)).entidadBase;
            var almacenDestino = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, nombreAlmacenDestino).resultadosBusqueda.FirstOrDefault(a => a.entidadBase.Nombre.Equals(nombreAlmacenDestino)).entidadBase;

            ModificarInventario(_producto?.Id ?? 0, almacenOrigen?.Id ?? 0, almacenDestino?.Id ?? 0, cantidad);
        }

        public void ModificarInventario(long idProducto, long idAlmacenOrigen, long idAlmacenDestino, decimal cantidad) {
            var producto = _producto ?? RepoProducto.Instancia.ObtenerPorId(idProducto);

            var costoUnitario = (producto?.Categoria == CategoriaProducto.ProductoTerminado 
                ? (producto?.CostoProduccionUnitario ?? 0) 
                : (producto?.CostoAdquisicionUnitario ?? 0)).ToString(CultureInfo.InvariantCulture);

            var consulta = string.Empty;
            var parametros = new Dictionary<string, object>();

            // Decrementar la cantidad en el almacen origen
            if (idAlmacenOrigen > 0) {
                consulta = """
                UPDATE adv__inventario
                SET 
                  cantidad = cantidad - @Cantidad,
                  valor_total = valor_total - (@Cantidad * @CostoUnitario),
                  costo_promedio = valor_total / cantidad
                WHERE id_producto = @IdProducto AND id_almacen = @IdAlmacenOrigen;
                """;
                parametros.Add("@Cantidad", cantidad);
                parametros.Add("@CostoUnitario", costoUnitario);
                parametros.Add("@IdProducto", idProducto);
                parametros.Add("@IdAlmacenOrigen", idAlmacenOrigen);

                ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
                parametros.Clear();
            }

            // Aumentar la cantidad en el almacen destino
            if (idAlmacenDestino > 0) {
                // Verificar primeramente si el producto existe en el almacén de destino
                consulta = """
                SELECT COUNT(*)
                FROM adv__inventario
                WHERE id_producto = @IdProducto AND id_almacen = @IdAlmacenDestino;
                """;
                parametros.Add("@IdProducto", idProducto);
                parametros.Add("@IdAlmacenDestino", idAlmacenDestino);

                var count = ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta, parametros);
                parametros.Clear();

                if (count > 0) {
                    consulta = """
                    UPDATE adv__inventario
                    SET 
                      cantidad = cantidad + @Cantidad,
                      valor_total = valor_total + (@Cantidad * @CostoUnitario),
                      costo_promedio = valor_total / cantidad
                    WHERE id_producto = @IdProducto AND id_almacen = @IdAlmacenDestino;
                    """;
                    parametros.Add("@Cantidad", cantidad);
                    parametros.Add("@CostoUnitario", costoUnitario);
                    parametros.Add("@IdProducto", idProducto);
                    parametros.Add("@IdAlmacenDestino", idAlmacenDestino);

                    ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
                    parametros.Clear();
                } else {
                    consulta = $"""
                        INSERT INTO adv__inventario (
                        id_producto, 
                        id_almacen, 
                        cantidad,
                        costo_promedio,
                        valor_total
                    ) 
                    VALUES (
                        @IdProducto,
                        @IdAlmacenDestino,
                        @Cantidad,
                        @CostoUnitario,
                        @Cantidad * @CostoUnitario
                    );
                    """;
                    parametros.Add("@IdProducto", idProducto);
                    parametros.Add("@IdAlmacenDestino", idAlmacenDestino);
                    parametros.Add("@Cantidad", cantidad);
                    parametros.Add("@CostoUnitario", costoUnitario);

                    ContextoBaseDatos.EjecutarComandoInsert(consulta, parametros);
                    parametros.Clear();
                }
            }
        }

        #endregion
    }
}