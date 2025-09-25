using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Taller.Interfaces;
using aDVanceERP.Modulos.Taller.Modelos;
using aDVanceERP.Modulos.Taller.Repositorios;

using System.Globalization;

namespace aDVanceERP.Modulos.Taller.Presentadores.OrdenProduccion
{
    public class PresentadorRegistroOrdenProduccion : PresentadorVistaRegistro<IVistaRegistroOrdenProduccion, Modelos.OrdenProduccion, RepoOrdenProduccion, FiltroBusquedaOrdenProduccion> {
        public PresentadorRegistroOrdenProduccion(IVistaRegistroOrdenProduccion vista) : base(vista) {
        }

        public override void PopularVistaDesdeEntidad(Modelos.OrdenProduccion entidad) {
            Vista.ModoEdicion = true;
            Vista.Id = entidad.Id;
            Vista.NombreProductoTerminado = entidad.NombreProducto ?? string.Empty;
            Vista.NombreAlmacenDestino = UtilesAlmacen.ObtenerNombreAlmacen(entidad.IdAlmacen) ?? string.Empty;
            Vista.NumeroOrden = entidad.NumeroOrden;
            Vista.FechaApertura = entidad.FechaApertura;
            Vista.Cantidad = entidad.Cantidad;
            Vista.MargenGanancia = entidad.MargenGanancia;
            Vista.Observaciones = entidad.Observaciones;

            if (entidad.Estado == EstadoOrdenProduccion.Cerrada)
                Vista.Habilitada = false;

            // Popular materias primas
            using (var repoMateriaPrima = new RepoOrdenMateriaPrima()) {
                var materiasPrimas = repoMateriaPrima.Buscar(FiltroBusquedaOrdenMateriaPrima.OrdenProduccion, entidad.Id.ToString()).resultados;
                foreach (var materiaPrima in materiasPrimas) {
                    Vista.AdicionarMateriaPrima(
                        UtilesAlmacen.ObtenerNombreAlmacen(materiaPrima.IdAlmacen) ?? string.Empty,
                        UtilesProducto.ObtenerNombreProducto(materiaPrima.IdProducto).Result ?? string.Empty,
                        materiaPrima.Cantidad);
                }
            }
            // Popular actividades de producción
            using (var repoActividadProduccion = new RepoOrdenActividadProduccion()) {
                var actividadesProduccion = repoActividadProduccion.Buscar(FiltroBusquedaOrdenActividadProduccion.OrdenProduccion, entidad.Id.ToString()).resultados;
                foreach (var actividad in actividadesProduccion) {
                    Vista.AdicionarActividadProduccion(actividad.Nombre, actividad.Cantidad);
                }
            }
            //TODO: Popular gastos indirectos
            using (var repoGastoIndirecto = new RepoOrdenGastoIndirecto()) {
                using (var repoGastoDinamico = new RepoOrdenGastoDinamico()) {
                    var gastosIndirectos = repoGastoIndirecto.Buscar(FiltroBusquedaOrdenGastoIndirecto.OrdenProduccion, entidad.Id.ToString()).resultados;

                    foreach (var gasto in gastosIndirectos) {
                        var tuplaGasto = new string[] {
                        gasto.Concepto,
                        gasto.Cantidad.ToString(CultureInfo.InvariantCulture),
                        gasto.Monto.ToString(CultureInfo.InvariantCulture)
                    };

                    // Verificar si hay una fórmula asociada
                    var gastoDinamico = repoGastoDinamico.Buscar(FiltroBusquedaOrdenGastoDinamico.IdOrdenGastoIndirecto, gasto.Id.ToString()).resultados.FirstOrDefault();

                    if (gastoDinamico != null) {
                        tuplaGasto = tuplaGasto.Concat([gastoDinamico.Formula]).ToArray();

                        Vista.InsertarGastoIndirectoDinamico(tuplaGasto[0], decimal.Parse(tuplaGasto[1], CultureInfo.InvariantCulture), tuplaGasto[3]);
                    } else
                        Vista.InsertarGastoIndirectoNormal(tuplaGasto[0], decimal.Parse(tuplaGasto[1], CultureInfo.InvariantCulture));
                    }
                }
            }

            _entidad = entidad;
        }

        protected override void RegistroAuxiliar(RepoOrdenProduccion repoEntidad, long id) {
            RegistrarEditarMateriasPrimasOrden(id);
            RegistrarEditarActividadesProduccionOrden(id);
            RegistrarEditarGastosIndirectos(id);

            base.RegistroAuxiliar(repoEntidad, id);
        }

        public void RegistrarEditarMateriasPrimasOrden(long idOrdenProduccion) {
            using (var datosObjeto = new RepoOrdenMateriaPrima()) {
                foreach (var tuplaMateriaPrima in Vista.MateriasPrimas) {
                    var criterioBusqueda = $"{idOrdenProduccion};{UtilesProducto.ObtenerIdProducto(tuplaMateriaPrima[1]).Result}";
                    var materiaPrimaExistente = datosObjeto.Buscar(FiltroBusquedaOrdenMateriaPrima.Producto, criterioBusqueda).resultados.FirstOrDefault();
                    var materiaPrima = materiaPrimaExistente ?? new OrdenMateriaPrima(0,
                        idOrdenProduccion,
                        UtilesAlmacen.ObtenerIdAlmacen(tuplaMateriaPrima[0]).Result,
                        UtilesProducto.ObtenerIdProducto(tuplaMateriaPrima[1]).Result,
                        decimal.TryParse(tuplaMateriaPrima[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad) ? cantidad : 0m,
                        decimal.TryParse(tuplaMateriaPrima[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var costoUnitario) ? costoUnitario : 0m,
                        costoUnitario * cantidad
                        );

                    if (materiaPrimaExistente != null) {
                        materiaPrima.Cantidad = decimal.TryParse(tuplaMateriaPrima[2], NumberStyles.Any, CultureInfo.InvariantCulture, out cantidad) ? cantidad : 0m;
                        materiaPrima.CostoUnitario = decimal.TryParse(tuplaMateriaPrima[3], NumberStyles.Any, CultureInfo.InvariantCulture, out costoUnitario) ? costoUnitario : 0m;
                        materiaPrima.Total = costoUnitario * cantidad;
                        datosObjeto.Editar(materiaPrima);
                    } else
                        datosObjeto.Adicionar(materiaPrima);
                }
            }
        }

        private void RegistrarEditarActividadesProduccionOrden(long idOrdenProduccion) {
            using (var datosObjeto = new RepoOrdenActividadProduccion()) {
                foreach (var tuplaActividadProduccion in Vista.ActividadesProduccion) {
                    var criterioBusqueda = $"{idOrdenProduccion};{tuplaActividadProduccion[0]}";
                    var actividadProduccionExistente = datosObjeto.Buscar(FiltroBusquedaOrdenActividadProduccion.Nombre, criterioBusqueda).resultados.FirstOrDefault();
                    var actividadProduccion = actividadProduccionExistente ?? new OrdenActividadProduccion(0,
                        idOrdenProduccion,
                        tuplaActividadProduccion[0],
                        decimal.TryParse(tuplaActividadProduccion[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad) ? cantidad : 0m,
                        decimal.TryParse(tuplaActividadProduccion[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var costo) ? costo : 0m,
                        costo * cantidad
                        );

                    if (actividadProduccionExistente != null) {
                        actividadProduccion.Cantidad = decimal.TryParse(tuplaActividadProduccion[1], NumberStyles.Any, CultureInfo.InvariantCulture, out cantidad) ? cantidad : 0m;
                        actividadProduccion.Costo = decimal.TryParse(tuplaActividadProduccion[2], NumberStyles.Any, CultureInfo.InvariantCulture, out costo) ? costo : 0m;
                        actividadProduccion.Total = costo * cantidad;
                        datosObjeto.Editar(actividadProduccion);
                    } else
                        datosObjeto.Adicionar(actividadProduccion);
                }
            }
        }

        private void RegistrarEditarGastosIndirectos(long idOrdenProduccion) {
            using (var entidadGastoIndirecto = new RepoOrdenGastoIndirecto()) {
                using (var entidadGastoDinamico = new RepoOrdenGastoDinamico()) {
                    foreach (var tuplaGastoIndirecto in Vista.GastosIndirectos) {
                        var criterioBusqueda = $"{idOrdenProduccion};{tuplaGastoIndirecto[0]}";
                        var gastoIndirectoExistente = entidadGastoIndirecto.Buscar(FiltroBusquedaOrdenGastoIndirecto.Concepto, criterioBusqueda).resultados.FirstOrDefault();
                        var gastoIndirecto = gastoIndirectoExistente ?? new OrdenGastoIndirecto(0,
                            idOrdenProduccion,
                            tuplaGastoIndirecto[0],
                            decimal.TryParse(tuplaGastoIndirecto[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad) ? cantidad : 0m,
                            decimal.TryParse(tuplaGastoIndirecto[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var monto) ? monto : 0m,
                            monto * cantidad
                            );

                        if (gastoIndirectoExistente != null) {
                            gastoIndirecto.Cantidad = decimal.TryParse(tuplaGastoIndirecto[1], NumberStyles.Any, CultureInfo.InvariantCulture, out cantidad) ? cantidad : 0m;
                            gastoIndirecto.Monto = decimal.TryParse(tuplaGastoIndirecto[2], NumberStyles.Any, CultureInfo.InvariantCulture, out monto) ? monto : 0m;
                            gastoIndirecto.Total = monto * cantidad;
                            entidadGastoIndirecto.Editar(gastoIndirecto);
                        } else
                            gastoIndirecto.Id = entidadGastoIndirecto.Adicionar(gastoIndirecto);

                        // Verificar e insertar tambien los gastos dinamicos
                        if (tuplaGastoIndirecto.Length > 3) {
                            var gastoDinamicoExistente = entidadGastoDinamico.Buscar(FiltroBusquedaOrdenGastoDinamico.IdOrdenGastoIndirecto, gastoIndirecto.Id.ToString()).resultados.FirstOrDefault();
                            var gastoDinamico = gastoDinamicoExistente ?? new OrdenGastoDinamico(0,
                                gastoIndirecto.Id,
                                tuplaGastoIndirecto[3]
                                );

                            if (gastoDinamicoExistente != null) {
                                gastoDinamico.Formula = tuplaGastoIndirecto[3];
                                entidadGastoDinamico.Editar(gastoDinamico);
                            } else {
                                gastoDinamico.Id = entidadGastoDinamico.Adicionar(gastoDinamico);
                            }
                        }
                    }
                }
            }
        }

        protected override Modelos.OrdenProduccion? ObtenerEntidadDesdeVista() {
            return new Modelos.OrdenProduccion(
                Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
                Vista.NumeroOrden,
                Vista.FechaApertura,
                DateTime.MinValue,
                UtilesAlmacen.ObtenerIdAlmacen(Vista.NombreAlmacenDestino).Result,
                Vista.NombreProductoTerminado,
                Vista.Cantidad,
                EstadoOrdenProduccion.Abierta,
                Vista.Observaciones,
                Vista.CostoTotal,
                Vista.PrecioUnitario,
                Vista.MargenGanancia);
        }
    }
}