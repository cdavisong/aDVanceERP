using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public sealed class Pedido : IEntidadBaseDatos {
        public Pedido() {
            Codigo = "00000000000000";
            FechaPedido = DateTime.UtcNow;
            TotalPedido = 0.0m;
            EstadoPedido = EstadoPedidoEnum.Pendiente;
            Activo = true;
        }

        public Pedido(long id, string codigo, long idCliente, long? idEmpleadoVendedor, DateTime fechaPedido, DateTime? fechaEntregaSolicitada, string direccionEntrega, decimal totalPedido, EstadoPedidoEnum estadoPedido, string observacionesPedido, bool activo) {
            Id = id;
            Codigo = codigo;
            IdCliente = idCliente;
            IdEmpleadoVendedor = idEmpleadoVendedor;
            FechaPedido = fechaPedido;
            FechaEntregaSolicitada = fechaEntregaSolicitada;
            DireccionEntrega = direccionEntrega;
            TotalPedido = totalPedido;
            EstadoPedido = estadoPedido;
            ObservacionesPedido = observacionesPedido;
            Activo = activo;
        }

        public long Id { get; set; }
        public string Codigo { get; set; }
        public long IdCliente { get; set; }
        public long? IdEmpleadoVendedor { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime? FechaEntregaSolicitada { get; set; }
        public string? DireccionEntrega { get; set; }
        public decimal TotalPedido { get; set; }
        public EstadoPedidoEnum EstadoPedido { get; set; }
        public string? ObservacionesPedido { get; set; }
        public bool Activo { get; set; }
    }

    public enum EstadoPedidoEnum {
        Pendiente,
        Confirmado,
        Preparando,
        [Display(Name = "Listo para Retirar")]
        ListoParaRetirar,
        Retirado,
        Cancelado
    }

    public enum FiltroBusquedaPedido {
        Todos,
        Id,
        Codigo,
        IdCliente,
        Estado,
        Inactivos
    }

    public static class UtilesBusquedaPedido {
        public static object[] FiltroBusquedaPedido = {
            "Todos los pedidos",
            "Identificador de BD",
            "Identificador del cliente",
            "Estado del pedido",
            "Pedidos inactivos"
        };
    }
}