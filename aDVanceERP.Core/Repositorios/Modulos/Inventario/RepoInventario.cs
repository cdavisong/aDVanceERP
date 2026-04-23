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
                    { "@id", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) },
                },
                FiltroBusquedaInventario.IdProducto => new Dictionary<string, object> {
                    { "@id_producto", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) },
                },
                FiltroBusquedaInventario.IdAlmacen => new Dictionary<string, object> {
                    { "@id_almacen", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) }
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

        #region SINGLETON

        public static RepoInventario Instancia { get; } = new RepoInventario();

        #endregion

        #region UTILES  

        /// <summary>
        /// Si viene costo real (recepción de compra), usar costoUnitarioReal.
        /// Si no (venta, traslado), usar costo del catálogo de producto.
        /// </summary>
        public void ModificarInventario(Producto? producto, Almacen? almacenOrigen, Almacen? almacenDestino, decimal cantidadMovida) {
            var costoUnitario = producto!.Categoria == CategoriaProductoEnum.ProductoTerminado
                ? producto.CostoProduccionUnitario
                : producto.CostoAdquisicionUnitario;

            var consulta = string.Empty;
            var parametros = new Dictionary<string, object>();

            // Decrementar la cantidad en el almacen origen (el costo promedio NO cambia al sacar inventario)
            if (almacenOrigen != null) {
                consulta = """
                    UPDATE adv__inventario
                    SET 
                      valor_total = valor_total - (@Cantidad * costo_promedio),
                      cantidad = cantidad - @Cantidad
                    WHERE id_producto = @IdProducto AND id_almacen = @IdAlmacenOrigen;
                    """;

                parametros.Add("@Cantidad", cantidadMovida);
                parametros.Add("@IdProducto", producto?.Id ?? 0);
                parametros.Add("@IdAlmacenOrigen", almacenOrigen?.Id ?? 0);

                ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
                parametros.Clear();
            }

            // Aumentar la cantidad en el almacen destino
            if (almacenDestino != null) {
                // Verificar primeramente si el producto existe en el almacén de destino
                consulta = """
                    SELECT COUNT(*)
                    FROM adv__inventario
                    WHERE id_producto = @IdProducto AND id_almacen = @IdAlmacenDestino;
                    """;

                parametros.Add("@IdProducto", producto?.Id ?? 0);
                parametros.Add("@IdAlmacenDestino", almacenDestino?.Id ?? 0);

                var count = ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta, parametros);
                parametros.Clear();

                if (count > 0) {
                    consulta = """
                        UPDATE adv__inventario
                        SET 
                            cantidad = cantidad + @Cantidad,
                            costo_promedio = (valor_total + (@Cantidad * @CostoUnitario)) / (cantidad + @Cantidad),
                            valor_total = valor_total + (@Cantidad * @CostoUnitario)
                        WHERE id_producto = @IdProducto AND id_almacen = @IdAlmacenDestino
                        """;

                    parametros.Add("@Cantidad", cantidadMovida);
                    parametros.Add("@CostoUnitario", costoUnitario);
                    parametros.Add("@IdProducto", producto?.Id ?? 0);
                    parametros.Add("@IdAlmacenDestino", almacenDestino?.Id ?? 0);

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

                    parametros.Add("@IdProducto", producto?.Id ?? 0);
                    parametros.Add("@IdAlmacenDestino", almacenDestino?.Id ?? 0);
                    parametros.Add("@Cantidad", cantidadMovida);
                    parametros.Add("@CostoUnitario", costoUnitario);

                    ContextoBaseDatos.EjecutarComandoInsert(consulta, parametros);
                    parametros.Clear();
                }
            }
        }

        #region VALORES INVENTARIO

        /// <summary>
        /// Obtiene el valor total real del inventario (usando valor_total almacenado)
        /// </summary>
        /// <param name="idAlmacen">ID del almacén (0 = todos los almacenes)</param>
        /// <returns>Valor total del inventario en la moneda base</returns>
        public decimal ObtenerValorTotalInventarioReal(long idAlmacen = 0) {
            string consulta;
            Dictionary<string, object>? parametros = null;

            if (idAlmacen != 0) {
                consulta = "SELECT COALESCE(SUM(valor_total), 0) FROM adv__inventario WHERE id_almacen = @IdAlmacen";
                parametros = new Dictionary<string, object> { { "@IdAlmacen", idAlmacen } };
            } else {
                consulta = "SELECT COALESCE(SUM(valor_total), 0) FROM adv__inventario";
            }

            return ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta, parametros);
        }

        /// <summary>
        /// Obtiene el valor total del inventario filtrado por productos activos
        /// </summary>
        /// <param name="idAlmacen">ID del almacén (0 = todos los almacenes)</param>
        /// <returns>Valor total del inventario de productos activos</returns>
        public decimal ObtenerValorTotalInventarioRealSoloActivos(long idAlmacen = 0) {
            string consulta;
            Dictionary<string, object>? parametros = null;

            if (idAlmacen != 0) {
                consulta = @"
                    SELECT COALESCE(SUM(i.valor_total), 0)
                    FROM adv__inventario i
                    INNER JOIN adv__producto p ON p.id_producto = i.id_producto
                    WHERE p.activo = 1 AND i.id_almacen = @IdAlmacen";
                parametros = new Dictionary<string, object> { { "@IdAlmacen", idAlmacen } };
            } else {
                consulta = @"
                    SELECT COALESCE(SUM(i.valor_total), 0)
                    FROM adv__inventario i
                    INNER JOIN adv__producto p ON p.id_producto = i.id_producto
                    WHERE p.activo = 1";
            }

            return ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta, parametros);
        }

        /// <summary>
        /// Obtiene el valor total del inventario desglosado por almacén
        /// </summary>
        /// <returns>Diccionario con nombre del almacén y su valor total</returns>
        public Dictionary<string, decimal> ObtenerValorInventarioPorAlmacen() {
            var consulta = @"
                SELECT 
                    a.nombre AS Almacen,
                    COALESCE(SUM(i.valor_total), 0) AS ValorTotal
                FROM adv__inventario i
                INNER JOIN adv__almacen a ON a.id_almacen = i.id_almacen
                WHERE a.estado = 1
                GROUP BY a.id_almacen, a.nombre
                ORDER BY a.nombre";

            var resultado = new Dictionary<string, decimal>();

            // Usar EjecutarConsulta con un mapeador personalizado
            var datos = ContextoBaseDatos.EjecutarConsulta<Dictionary<string, decimal>>(
                consulta,
                null,
                reader => {
                    var fila = new Dictionary<string, decimal>();
                    var nombre = reader["Almacen"].ToString() ?? string.Empty;
                    var valor = Convert.ToDecimal(reader["ValorTotal"]);
                    return (fila, new List<IEntidadBaseDatos>());
                });

            foreach (var item in datos) {
                var kvp = item.entidadBase;
                foreach (var kv in kvp) {
                    resultado[kv.Key] = kv.Value;
                }
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene el valor total del inventario para un producto específico
        /// </summary>
        /// <param name="idProducto">ID del producto</param>
        /// <param name="idAlmacen">ID del almacén (0 = todos los almacenes)</param>
        /// <returns>Valor total del producto en inventario</returns>
        public decimal ObtenerValorTotalPorProducto(long idProducto, long idAlmacen = 0) {
            string consulta;
            Dictionary<string, object> parametros;

            if (idAlmacen != 0) {
                consulta = """
                    SELECT COALESCE(valor_total, 0) 
                    FROM adv__inventario 
                    WHERE id_producto = @IdProducto 
                        AND id_almacen = @IdAlmacen
                    """;
                    
                parametros = new Dictionary<string, object>                {
                    { "@IdProducto", idProducto },
                    { "@IdAlmacen", idAlmacen }
                };
            } else {
                consulta = """
                    SELECT COALESCE(SUM(valor_total), 0) 
                    FROM adv__inventario 
                    WHERE id_producto = @IdProducto
                    """;
                    
                parametros = new Dictionary<string, object> { 
                    { "@IdProducto", idProducto } 
                };
            }

            return ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta, parametros);
        }

        /// <summary>
        /// Obtiene estadísticas completas del inventario
        /// </summary>
        /// <param name="idAlmacen">ID del almacén (0 = todos los almacenes)</param>
        /// <returns>Tupla con estadísticas (TotalProductos, TotalCantidad, ValorTotal, CostoPromedioGeneral)</returns>
        public (int TotalProductos, decimal TotalCantidad, decimal ValorTotal, decimal CostoPromedioGeneral) ObtenerEstadisticasInventario(long idAlmacen = 0) {
            string consulta;
            Dictionary<string, object>? parametros = null;

            if (idAlmacen != 0) {
                consulta = """
                    SELECT 
                        COUNT(*) AS TotalProductos,
                        COALESCE(SUM(cantidad), 0) AS TotalCantidad,
                        COALESCE(SUM(valor_total), 0) AS ValorTotal,
                        CASE 
                            WHEN SUM(cantidad) > 0 THEN COALESCE(SUM(valor_total) / SUM(cantidad), 0)
                            ELSE 0
                        END AS CostoPromedioGeneral
                    FROM adv__inventario
                    WHERE id_almacen = @IdAlmacen
                    """;
                parametros = new Dictionary<string, object> { { "@IdAlmacen", idAlmacen } };
            } else {
                consulta = """
                    SELECT 
                        COUNT(*) AS TotalProductos,
                        COALESCE(SUM(cantidad), 0) AS TotalCantidad,
                        COALESCE(SUM(valor_total), 0) AS ValorTotal,
                        CASE 
                            WHEN SUM(cantidad) > 0 THEN COALESCE(SUM(valor_total) / SUM(cantidad), 0)
                            ELSE 0
                        END AS CostoPromedioGeneral
                    FROM adv__inventario
                    """;
            }

            // Como EjecutarConsultaEscalar no soporta múltiples valores, usamos EjecutarConsulta
            using var conexion = ContextoBaseDatos.ObtenerConexion();
            using var comando = ContextoBaseDatos.CrearComando(consulta, parametros, conexion);
            using var reader = comando.ExecuteReader();

            if (reader.Read()) {
                return (
                    TotalProductos: Convert.ToInt32(reader["TotalProductos"]),
                    TotalCantidad: Convert.ToDecimal(reader["TotalCantidad"]),
                    ValorTotal: Convert.ToDecimal(reader["ValorTotal"]),
                    CostoPromedioGeneral: Convert.ToDecimal(reader["CostoPromedioGeneral"])
                );
            }

            return (0, 0, 0, 0);
        }

        #endregion

        #endregion
    }
}