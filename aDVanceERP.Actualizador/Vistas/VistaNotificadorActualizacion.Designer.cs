namespace aDVanceERP.Actualizador.Vistas {
    partial class VistaNotificadorActualizacion {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaNotificadorActualizacion));
            formatoBase = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            layoutDistribucion = new TableLayoutPanel();
            panelCentral = new Panel();
            fieldTexto = new Guna.UI2.WinForms.Guna2HtmlLabel();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna.UI2.WinForms.Guna2Button();
            btnActualizar = new Guna.UI2.WinForms.Guna2Button();
            layoutDistribucion.SuspendLayout();
            panelCentral.SuspendLayout();
            layoutBotones.SuspendLayout();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.AnimationType = Guna.UI2.WinForms.Guna2BorderlessForm.AnimateWindowType.AW_HOR_NEGATIVE;
            formatoBase.BorderRadius = 26;
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
            formatoBase.DragForm = false;
            formatoBase.ResizeForm = false;
            formatoBase.TransparentWhileDrag = true;
            // 
            // layoutDistribucion
            // 
            layoutDistribucion.BackColor = Color.White;
            layoutDistribucion.ColumnCount = 3;
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDistribucion.Controls.Add(panelCentral, 1, 1);
            layoutDistribucion.Controls.Add(layoutBotones, 1, 3);
            layoutDistribucion.Dock = DockStyle.Fill;
            layoutDistribucion.Location = new Point(0, 0);
            layoutDistribucion.Name = "layoutDistribucion";
            layoutDistribucion.RowCount = 5;
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion.Size = new Size(800, 460);
            layoutDistribucion.TabIndex = 1;
            // 
            // panelCentral
            // 
            panelCentral.AutoScroll = true;
            panelCentral.Controls.Add(fieldTexto);
            panelCentral.Dock = DockStyle.Fill;
            panelCentral.Location = new Point(20, 20);
            panelCentral.Margin = new Padding(0);
            panelCentral.Name = "panelCentral";
            panelCentral.Size = new Size(760, 355);
            panelCentral.TabIndex = 12;
            // 
            // fieldTexto
            // 
            fieldTexto.AutoSize = false;
            fieldTexto.AutoSizeHeightOnly = true;
            fieldTexto.BackColor = Color.White;
            fieldTexto.Dock = DockStyle.Top;
            fieldTexto.Location = new Point(0, 0);
            fieldTexto.Name = "fieldTexto";
            fieldTexto.Size = new Size(760, 1);
            fieldTexto.TabIndex = 2;
            fieldTexto.Text = null;
            // 
            // layoutBotones
            // 
            layoutBotones.BackColor = Color.White;
            layoutBotones.ColumnCount = 3;
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            layoutBotones.Controls.Add(btnSalir, 2, 0);
            layoutBotones.Controls.Add(btnActualizar, 1, 0);
            layoutBotones.Dock = DockStyle.Fill;
            layoutBotones.Location = new Point(23, 395);
            layoutBotones.Margin = new Padding(3, 0, 0, 0);
            layoutBotones.Name = "layoutBotones";
            layoutBotones.RowCount = 1;
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutBotones.Size = new Size(757, 45);
            layoutBotones.TabIndex = 11;
            // 
            // btnSalir
            // 
            btnSalir.Animated = true;
            btnSalir.BorderColor = Color.Gainsboro;
            btnSalir.BorderRadius = 18;
            btnSalir.BorderThickness = 1;
            btnSalir.CustomizableEdges = customizableEdges1;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.FillColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Gainsboro;
            btnSalir.HoverState.BorderColor = Color.PeachPuff;
            btnSalir.HoverState.FillColor = Color.PeachPuff;
            btnSalir.HoverState.ForeColor = Color.Black;
            btnSalir.Location = new Point(560, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnSalir.Size = new Size(194, 39);
            btnSalir.TabIndex = 14;
            btnSalir.Text = "Salir";
            // 
            // btnActualizar
            // 
            btnActualizar.Animated = true;
            btnActualizar.BorderRadius = 18;
            btnActualizar.CustomizableEdges = customizableEdges3;
            btnActualizar.Dock = DockStyle.Fill;
            btnActualizar.FillColor = Color.PeachPuff;
            btnActualizar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnActualizar.ForeColor = Color.Black;
            btnActualizar.Location = new Point(260, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnActualizar.Size = new Size(294, 39);
            btnActualizar.TabIndex = 15;
            btnActualizar.Text = "Actualizar ahora";
            // 
            // VistaNotificadorActualizacion
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(800, 460);
            Controls.Add(layoutDistribucion);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon) resources.GetObject("$this.Icon");
            Name = "VistaNotificadorActualizacion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VistaNotificadorActualizacion";
            layoutDistribucion.ResumeLayout(false);
            panelCentral.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutDistribucion;
        private TableLayoutPanel layoutBotones;
        private Guna.UI2.WinForms.Guna2Button btnActualizar;
        private Panel panelCentral;
        private Guna.UI2.WinForms.Guna2HtmlLabel fieldTexto;
        private Guna.UI2.WinForms.Guna2Button btnSalir;
    }
}