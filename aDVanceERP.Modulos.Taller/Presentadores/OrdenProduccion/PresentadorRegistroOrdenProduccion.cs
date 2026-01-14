using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Taller;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Taller;
using aDVanceERP.Core.Utiles.Datos;

using aDVanceERP.Modulos.Taller.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Taller.Presentadores.OrdenProduccion {
    public class PresentadorRegistroOrdenProduccion : PresentadorVistaRegistro<IVistaRegistroOrdenProduccion, Core.Modelos.Modulos.Taller.OrdenProduccion, RepoOrdenProduccion, FiltroBusquedaOrdenProduccion> {
        public PresentadorRegistroOrdenProduccion(IVistaRegistroOrdenProduccion vista) : base(vista) {
        }

        public override void PopularVistaDesdeEntidad(Core.Modelos.Modulos.Taller.OrdenProduccion entidad) {
            Vista.ModoEdicion = true;
            Vista.Id = entidad.Id;
            Vista.NombreProductoTerminado = entidad.NombreProducto ?? string.Empty;
            Vista.NombreAlmacenDestino = RepoAlmacen.Instancia.ObtenerPorId(entidad.IdAlmacen)?.Nombre ?? string.Empty;
            Vista.NumeroOrden = entidad.NumeroOrden;
            Vista.FechaApertura = entidad.FechaApertura;
            Vista.Cantidad = entidad.Cantidad;
            Vista.MargenGanancia = entidad.MargenGanancia;
            Vista.Observaciones = entidad.Observaciones;

            if (entidad.Estado == EstadoOrdenProduccion.Cerrada)
                Vista.Habilitada = false;

            // Popular materias primas
            using (var repoMateriaPrima = new RepoOrdenMateriaPrima()) {
                var materiasPrimas = repoMateriaPrima.Buscar(FiltroBusquedaOrdenMateriaPrima.OrdenProduccion, entidad.Id.ToString()).entidades;
                foreach (var materiaPrima in materiasPrimas) {
                    Vista.AdicionarMateriaPrima(
                        RepoAlmacen.Instancia.ObtenerPorId(materiaPrima.IdAlmacen)?.Nombre ?? string.Empty,
                        RepoProducto.Instancia.ObtenerPorId(materiaPrima.IdProducto)?.Nombre ?? string.Empty,
                        materiaPrima.Cantidad);
                }
            }
            // Popular actividades de producción
            using (var repoActividadProduccion = new RepoOrdenActividadProduccion()) {
                var actividadesProduccion = repoActividadProduccion.Buscar(FiltroBusquedaOrdenActividadProduccion.OrdenProduccion, entidad.Id.ToString()).entidades;
                foreach (var actividad in actividadesProduccion) {
                    Vista.AdicionarActividadProduccion(actividad.Nombre, actividad.Cantidad);
                }
            }
            //TODO: Popular gastos indirectos
            using (var repoGastoIndirecto = new RepoOrdenGastoIndirecto()) {
                using (var repoGastoDinamico = new RepoOrdenGastoDinamico()) {
                    var gastosIndirectos = repoGastoIndirecto.Buscar(FiltroBusquedaOrdenGastoIndirecto.OrdenProduccion, entidad.Id.ToString()).entidades;

                    foreach (var gasto in gastosIndirectos) {
                        var tuplaGasto = new string[] {
                        gasto.Concepto,
                        gasto.Cantidad.ToString(CultureInfo.InvariantCulture),
                        gasto.Monto.ToString(CultureInfo.InvariantCulture)
                    };

                    // Verificar si hay una fórmula asociada
                    var gastoDinamico = repoGastoDinamico.Buscar(FiltroBusquedaOrdenGastoDinamico.IdOrdenGastoIndirecto, gasto.Id.ToString()).entidades.FirstOrDefault();

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

        protected override void RegistroEdicionAuxiliar(RepoOrdenProduccion repoEntidad, long id) {
            RegistrarEditarMateriasPrimasOrden(id);
            RegistrarEditarActividadesProduccionOrden(id);
            RegistrarEditarGastosIndirectos(id);

            base.RegistroEdicionAuxiliar(repoEntidad, id);
        }
        
        public void RegistrarEditarMateriasPrimasOrden(long idOrdenProduccion) {
            using (var datosObjeto = new RepoOrdenMateriaPrima()) {
                foreach (var tuplaMateriaPrima in Vista.MateriasPrimas) {
                    var criterioBusqueda = $"{idOrdenProduccion};{RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, tuplaMateriaPrima[1]).entidades.FirstOrDefault()?.Id ?? 0}";
                    var materiaPrimaExistente = datosObjeto.Buscar(FiltroBusquedaOrdenMateriaPrima.Producto, criterioBusqueda).entidades.FirstOrDefault();
                    var materiaPrima = materiaPrimaExistente ?? new OrdenMateriaPrima(0,
                        idOrdenProduccion,
                        RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, tuplaMateriaPrima[0]).entidades.FirstOrDefault()?.Id ?? 0,
                        RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, tuplaMateriaPrima[1]).entidades.FirstOrDefault()?.Id ?? 0,
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
                    var actividadProduccionExistente = datosObjeto.Buscar(FiltroBusquedaOrdenActividadProduccion.Nombre, criterioBusqueda).entidades.FirstOrDefault();
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
                        var gastoIndirectoExistente = entidadGastoIndirecto.Buscar(FiltroBusquedaOrdenGastoIndirecto.Concepto, criterioBusqueda).entidades.FirstOrDefault();
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
                            var gastoDinamicoExistente = entidadGastoDinamico.Buscar(FiltroBusquedaOrdenGastoDinamico.IdOrdenGastoIndirecto, gastoIndirecto.Id.ToString()).entidades.FirstOrDefault();
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

        protected override Core.Modelos.Modulos.Taller.OrdenProduccion? ObtenerEntidadDesdeVista() {
            return new Core.Modelos.Modulos.Taller.OrdenProduccion(
                Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
                Vista.NumeroOrden,
                Vista.FechaApertura,
                DateTime.MinValue,
                RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenDestino).entidades.FirstOrDefault()?.Id ?? 0,
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