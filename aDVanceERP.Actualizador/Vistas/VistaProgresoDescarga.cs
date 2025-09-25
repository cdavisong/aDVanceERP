using aDVanceERP.Actualizador.Componentes;

using System.Runtime.InteropServices;

namespace aDVanceERP.Actualizador.Vistas {
    public partial class VistaProgresoDescarga : Form {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);

        public VistaProgresoDescarga() {
            InitializeComponent();
            EstablecerColorBarraProgreso(fieldBarraProgreso, Color.Firebrick);
        }

        private void EstablecerColorBarraProgreso(CustomProgressBar pBar, Color newColor) {
            SendMessage(pBar.Handle, 1040, newColor.ToArgb(), IntPtr.Zero);
        }

        public void UpdateProgress(double percentage) {
            if (this.InvokeRequired) {
                this.Invoke(new Action<double>(UpdateProgress), percentage);
                return;
            }

            fieldBarraProgreso.Value = (int) percentage;
            fieldInfo.Text = $"Descargando archivos necesarios...{(int) percentage}%";
        }

        public void UpdateStatus(string status) {
            if (this.InvokeRequired) {
                this.Invoke(new Action<string>(UpdateStatus), status);
                return;
            }

            fieldInfo.Text = status;
        }
    }
}
