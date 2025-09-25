using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Desktop.Utiles;

public static class Extensiones {
    public static void EstablecerCoordenadasVistaRegistro(this IVistaBase vista, Size dimensionesContenedorVistas, bool centrar = false, bool dock = false) {
        vista.Coordenadas = new Point(
            centrar ? dimensionesContenedorVistas.Width / 2 - vista.Dimensiones.Width / 2 : dimensionesContenedorVistas.Width - vista.Dimensiones.Width,
            centrar ? (Screen.PrimaryScreen?.WorkingArea.Height ?? VariablesGlobales.AlturaBarraTituloPredeterminada) / 2 - vista.Dimensiones.Height / 2 : VariablesGlobales.AlturaBarraTituloPredeterminada
        );
    }

    public static void EstablecerDimensionesVistaRegistro(this IVistaBase vista, int alturaContenedorVistas, bool dimensionesOriginales = false) {
        vista.Dimensiones = dimensionesOriginales ? vista.Dimensiones : vista.Dimensiones with {
            Height = alturaContenedorVistas + VariablesGlobales.AlturaBarraPiePagina
        };
    }

    public static void EstablecerCoordenadasVistaRegistroFull(this IVistaBase vista) {
        vista.Coordenadas = new Point(Screen.PrimaryScreen.WorkingArea.Left, Screen.PrimaryScreen.WorkingArea.Top);
    }

    public static void EstablecerDimensionesVistaRegistroFull(this IVistaBase vista, Size dimensionesContenedorVistas) {
        vista.Dimensiones = dimensionesContenedorVistas;
    }
}