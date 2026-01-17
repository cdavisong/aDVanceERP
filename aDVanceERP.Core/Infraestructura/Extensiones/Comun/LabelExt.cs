namespace aDVanceERP.Core.Infraestructura.Extensiones.Comun {
    public static class LabelExt {
        public static Padding AjusteAutomaticoMargenTexto(this Label label) {
            var dimensionesTexto = TextRenderer.MeasureText(label.Text, label.Font);

            return new Padding(1, dimensionesTexto.Width > label.Width ? 10 : 1, 1, 1);
        }
    }
}
