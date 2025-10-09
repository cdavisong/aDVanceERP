﻿namespace aDVanceERP.Core.Infraestructura.Extensiones.Comun;

public static class FormExt {
    public static IEnumerable<Control> GetAllControls(this Control parent) {
        foreach (Control control in parent.Controls) {
            yield return control;

            foreach (Control descendant in control.GetAllControls()) {
                yield return descendant;
            }
        }
    }
}
