using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Finanzas;

public sealed class CentroCosto : IEntidadBaseDatos {
    public CentroCosto() {
        Codigo = "0";
        Nombre = "Genérico";
        Descripcion = "No hay descripción disponible.";
        Tipo = TipoCentroCosto.SinTipo;
    }

    public CentroCosto(long id, string codigo, string nombre, string? descripcion, long idResponsable, long idDepartamento, TipoCentroCosto tipo, bool activo, DateTime fechaCreacion, decimal presupuestoAnual, decimal presupuestoMensual) {
        Id = id;
        Codigo = codigo;
        Nombre = nombre;
        Descripcion = descripcion;
        IdResponsable = idResponsable;
        IdDepartamento = idDepartamento;
        Tipo = tipo;
        Activo = activo;
        FechaCreacion = fechaCreacion;
        PresupuestoAnual = presupuestoAnual;
        PresupuestoMensual = presupuestoMensual;
    }

    public long Id { get; set; }
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public long IdResponsable { get; set; }
    public long IdDepartamento { get; set; }
    public TipoCentroCosto Tipo { get; set; } // Administrativo, Producción, Ventas, etc.
    public bool Activo { get; set; }
    public DateTime FechaCreacion { get; set; }
    public decimal PresupuestoAnual { get; set; }
    public decimal PresupuestoMensual { get; set; }
}
