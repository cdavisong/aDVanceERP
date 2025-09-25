namespace aDVanceERP.Core.Vistas.Comun {
    partial class VistaCargaDatos {
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
            layoutBase = new TableLayoutPanel();
            layoutDistribucion = new TableLayoutPanel();
            fieldIconoCarga = new PictureBox();
            fieldTextoCarga = new Label();
            layoutBase.SuspendLayout();
            layoutDistribucion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) fieldIconoCarga).BeginInit();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.BorderRadius = 16;
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
            formatoBase.DragForm = false;
            formatoBase.ResizeForm = false;
            formatoBase.TransparentWhileDrag = true;
            // 
            // layoutBase
            // 
            layoutBase.BackColor = Color.Gainsboro;
            layoutBase.ColumnCount = 1;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBase.Controls.Add(layoutDistribucion, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Margin = new Padding(0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBase.Size = new Size(446, 92);
            layoutBase.TabIndex = 0;
            // 
            // layoutDistribucion
            // 
            layoutDistribucion.BackColor = Color.White;
            layoutDistribucion.ColumnCount = 4;
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDistribucion.Controls.Add(fieldIconoCarga, 1, 1);
            layoutDistribucion.Controls.Add(fieldTextoCarga, 2, 1);
            layoutDistribucion.Dock = DockStyle.Fill;
            layoutDistribucion.Location = new Point(1, 1);
            layoutDistribucion.Margin = new Padding(1);
            layoutDistribucion.Name = "layoutDistribucion";
            layoutDistribucion.RowCount = 3;
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion.Size = new Size(444, 90);
            layoutDistribucion.TabIndex = 0;
            // 
            // fieldIconoCarga
            // 
            fieldIconoCarga.BackgroundImage = Properties.Resources.p1_48px;
            fieldIconoCarga.BackgroundImageLayout = ImageLayout.Center;
            fieldIconoCarga.Dock = DockStyle.Fill;
            fieldIconoCarga.Location = new Point(20, 20);
            fieldIconoCarga.Margin = new Padding(0);
            fieldIconoCarga.Name = "fieldIconoCarga";
            fieldIconoCarga.Size = new Size(50, 50);
            fieldIconoCarga.TabIndex = 0;
            fieldIconoCarga.TabStop = false;
            // 
            // fieldTextoCarga
            // 
            fieldTextoCarga.Dock = DockStyle.Fill;
            fieldTextoCarga.Font = new Font("Segoe UI", 11.25F);
            fieldTextoCarga.ForeColor = Color.Gray;
            fieldTextoCarga.ImeMode = ImeMode.NoControl;
            fieldTextoCarga.Location = new Point(75, 25);
            fieldTextoCarga.Margin = new Padding(5, 5, 1, 1);
            fieldTextoCarga.Name = "fieldTextoCarga";
            fieldTextoCarga.Size = new Size(348, 44);
            fieldTextoCarga.TabIndex = 1;
            fieldTextoCarga.Text = "Filtrando resultados de búsqueda";
            fieldTextoCarga.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaCargaDatos
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(446, 92);
            Controls.Add(layoutBase);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VistaCargaDatos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VistaCargaDatos";
            layoutBase.ResumeLayout(false);
            layoutDistribucion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) fieldIconoCarga).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutDistribucion;
        private PictureBox fieldIconoCarga;
        private Label fieldTextoCarga;
    }
}