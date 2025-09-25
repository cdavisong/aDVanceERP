namespace aDVanceERP.Actualizador.Vistas {
    partial class VistaProgresoDescarga {
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
            fieldInfo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            fieldTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            fieldBarraProgreso = new Componentes.CustomProgressBar();
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
            layoutDistribucion.ColumnCount = 1;
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion.Controls.Add(fieldInfo, 0, 2);
            layoutDistribucion.Controls.Add(fieldTitulo, 0, 0);
            layoutDistribucion.Controls.Add(fieldBarraProgreso, 0, 1);
            layoutDistribucion.Dock = DockStyle.Fill;
            layoutDistribucion.Location = new Point(0, 0);
            layoutDistribucion.Margin = new Padding(0);
            layoutDistribucion.Name = "layoutDistribucion";
            layoutDistribucion.RowCount = 3;
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            layoutDistribucion.Size = new Size(599, 365);
            layoutDistribucion.TabIndex = 3;
            // 
            // fieldInfo
            // 
            fieldInfo.AutoSize = false;
            fieldInfo.BackColor = Color.White;
            fieldInfo.Dock = DockStyle.Fill;
            fieldInfo.Font = new Font("Segoe UI", 11.25F);
            fieldInfo.Location = new Point(10, 151);
            fieldInfo.Margin = new Padding(10, 10, 30, 10);
            fieldInfo.Name = "fieldInfo";
            fieldInfo.Size = new Size(559, 204);
            fieldInfo.TabIndex = 4;
            fieldInfo.Text = "Preparando la descarga...";
            fieldInfo.UseGdiPlusTextRendering = true;
            // 
            // fieldTitulo
            // 
            fieldTitulo.AutoSize = false;
            fieldTitulo.BackColor = Color.White;
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 11.25F);
            fieldTitulo.Location = new Point(10, 10);
            fieldTitulo.Margin = new Padding(10, 10, 30, 10);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(559, 76);
            fieldTitulo.TabIndex = 3;
            fieldTitulo.Text = "<h3>Descargando aDVance ERP</h3>";
            fieldTitulo.UseGdiPlusTextRendering = true;
            // 
            // fieldBarraProgreso
            // 
            fieldBarraProgreso.Dock = DockStyle.Fill;
            fieldBarraProgreso.Location = new Point(10, 96);
            fieldBarraProgreso.Margin = new Padding(10, 0, 30, 0);
            fieldBarraProgreso.Name = "fieldBarraProgreso";
            fieldBarraProgreso.Size = new Size(559, 45);
            fieldBarraProgreso.TabIndex = 5;
            // 
            // VistaProgresoDescarga
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(599, 365);
            Controls.Add(layoutDistribucion);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VistaProgresoDescarga";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaProgresoDescargaActualizacion";
            layoutDistribucion.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutDistribucion;
        private Guna.UI2.WinForms.Guna2HtmlLabel fieldInfo;
        private Guna.UI2.WinForms.Guna2HtmlLabel fieldTitulo;
        private Componentes.CustomProgressBar fieldBarraProgreso;
    }
}