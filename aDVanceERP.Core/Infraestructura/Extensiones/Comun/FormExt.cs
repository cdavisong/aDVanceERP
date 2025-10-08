namespace aDVanceERP.Core.Infraestructura.Extensiones.Comun;

public static class FormExt {
    public static IEnumerable<Control> GetAllControls(this Control container) {
        foreach (Control control in container.Controls) {
            yield return control;

            // Llamada recursiva para controles anidados
            foreach (Control childControl in GetAllControls(control)) {
                yield return childControl;
            }
        }
    }
}
