using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces;

public interface IVistaRegistro : IVistaBase, IGestorEntidades {
    bool ModoEdicion { get; set; }
}