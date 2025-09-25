namespace aDVanceERP.Actualizador.Componentes {
    public partial class CustomProgressBar : ProgressBar {
        public CustomProgressBar() {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e) {
            Rectangle rect = this.ClientRectangle;
            Graphics g = e.Graphics;

            // Fondo
            using (var brush = new SolidBrush(this.BackColor)) {
                g.FillRectangle(brush, rect);
            }

            // Progreso
            Rectangle progressRect = new Rectangle(
                rect.X, rect.Y,
                (int) (rect.Width * ((double) Value / Maximum)),
                rect.Height);

            Color progressColor = Color.Firebrick; 

            using (var brush = new SolidBrush(progressColor)) {
                g.FillRectangle(brush, progressRect);
            }

            // Borde
            using (var pen = new Pen(Color.WhiteSmoke, 1)) {
                g.DrawRectangle(pen, new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1));
            }
        }
    }
}
