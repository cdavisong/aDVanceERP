using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using GMap.NET.WindowsForms;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces {
    public interface IVistaGestionUbicaciones : IVistaContenedor, IGestorEntidades {
        Image Icono { get; set; }
        GMapControl Mapa { get; }
        CoordenadasGeograficas Ubicacion { get; set; }

        event EventHandler? AlturaContenedorTuplasModificada;
        event EventHandler? SincronizarDatos;
    }
}
