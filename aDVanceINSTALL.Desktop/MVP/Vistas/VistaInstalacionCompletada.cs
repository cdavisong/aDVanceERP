namespace aDVanceINSTALL.Desktop.MVP.Vistas {
    public partial class VistaInstalacionCompletada : Form {
        public VistaInstalacionCompletada() {
            InitializeComponent();
        }

        public bool EjecutarAplicacionAlSalir => fieldEjecutarAplicacion.Checked;
    }
}
