namespace aDVancePOS.Desktop.MVP.Vistas.ContenedorSeguridad {
    partial class VistaContenedorSeguridad {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            formatoBase = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            layoutDistribucion = new TableLayoutPanel();
            contenedorVistas = new Panel();
            layoutDistribucion.SuspendLayout();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
            formatoBase.DragForm = false;
            formatoBase.HasFormShadow = false;
            formatoBase.TransparentWhileDrag = true;
            // 
            // layoutDistribucion
            // 
            layoutDistribucion.BackColor = Color.Gainsboro;
            layoutDistribucion.ColumnCount = 3;
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutDistribucion.Controls.Add(contenedorVistas, 1, 0);
            layoutDistribucion.Dock = DockStyle.Fill;
            layoutDistribucion.Location = new Point(0, 0);
            layoutDistribucion.Name = "layoutDistribucion";
            layoutDistribucion.RowCount = 1;
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion.Size = new Size(1386, 788);
            layoutDistribucion.TabIndex = 3;
            // 
            // contenedorVistas
            // 
            contenedorVistas.Dock = DockStyle.Fill;
            contenedorVistas.Location = new Point(443, 20);
            contenedorVistas.Margin = new Padding(0, 20, 0, 20);
            contenedorVistas.Name = "contenedorVistas";
            contenedorVistas.Size = new Size(500, 748);
            contenedorVistas.TabIndex = 0;
            // 
            // VistaContenedorSeguridad
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1386, 788);
            Controls.Add(layoutDistribucion);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "VistaContenedorSeguridad";
            StartPosition = FormStartPosition.Manual;
            Text = "VistaContenedorSeguridad";
            layoutDistribucion.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutDistribucion;
        private Panel contenedorVistas;
    }
}