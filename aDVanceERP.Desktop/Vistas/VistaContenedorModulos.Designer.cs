using Guna.UI2.WinForms;

namespace aDVanceERP.Desktop.Vistas {
    partial class VistaContenedorModulos {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaContenedorModulos));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutDistribucion = new TableLayoutPanel();
            layoutMenuLateral = new TableLayoutPanel();
            btnGestorModulos = new Guna2CircleButton();
            btnConfiguracionGeneral = new Guna2CircleButton();
            layoutModulos = new FlowLayoutPanel();
            btnInicio = new Guna2CircleButton();
            panelCentral = new Panel();
            fieldNombreModulo = new Label();
            layoutBase.SuspendLayout();
            layoutDistribucion.SuspendLayout();
            layoutMenuLateral.SuspendLayout();
            layoutModulos.SuspendLayout();
            panelCentral.SuspendLayout();
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
            // layoutBase
            // 
            layoutBase.BackColor = Color.White;
            layoutBase.ColumnCount = 1;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBase.Controls.Add(layoutDistribucion, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBase.Size = new Size(1356, 608);
            layoutBase.TabIndex = 1;
            // 
            // layoutDistribucion
            // 
            layoutDistribucion.BackColor = Color.White;
            layoutDistribucion.ColumnCount = 2;
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion.Controls.Add(layoutMenuLateral, 0, 0);
            layoutDistribucion.Controls.Add(panelCentral, 1, 0);
            layoutDistribucion.Dock = DockStyle.Fill;
            layoutDistribucion.Location = new Point(0, 0);
            layoutDistribucion.Margin = new Padding(0);
            layoutDistribucion.Name = "layoutDistribucion";
            layoutDistribucion.RowCount = 1;
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion.Size = new Size(1356, 608);
            layoutDistribucion.TabIndex = 0;
            // 
            // layoutMenuLateral
            // 
            layoutMenuLateral.BackColor = Color.White;
            layoutMenuLateral.ColumnCount = 1;
            layoutMenuLateral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMenuLateral.Controls.Add(btnGestorModulos, 0, 1);
            layoutMenuLateral.Controls.Add(btnConfiguracionGeneral, 0, 2);
            layoutMenuLateral.Controls.Add(layoutModulos, 0, 0);
            layoutMenuLateral.Dock = DockStyle.Fill;
            layoutMenuLateral.Location = new Point(0, 10);
            layoutMenuLateral.Margin = new Padding(0, 10, 0, 10);
            layoutMenuLateral.Name = "layoutMenuLateral";
            layoutMenuLateral.RowCount = 3;
            layoutMenuLateral.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutMenuLateral.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            layoutMenuLateral.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            layoutMenuLateral.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutMenuLateral.Size = new Size(50, 588);
            layoutMenuLateral.TabIndex = 0;
            // 
            // btnGestorModulos
            // 
            btnGestorModulos.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnGestorModulos.CheckedState.FillColor = Color.PeachPuff;
            btnGestorModulos.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnGestorModulos.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnGestorModulos.CustomImages.ImageSize = new Size(24, 24);
            btnGestorModulos.Dock = DockStyle.Fill;
            btnGestorModulos.FillColor = Color.White;
            btnGestorModulos.Font = new Font("Segoe UI", 9F);
            btnGestorModulos.ForeColor = Color.White;
            btnGestorModulos.ImageSize = new Size(24, 24);
            btnGestorModulos.Location = new Point(3, 491);
            btnGestorModulos.Name = "btnGestorModulos";
            btnGestorModulos.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btnGestorModulos.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnGestorModulos.Size = new Size(44, 44);
            btnGestorModulos.TabIndex = 2;
            btnGestorModulos.Visible = false;
            // 
            // btnConfiguracionGeneral
            // 
            btnConfiguracionGeneral.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnConfiguracionGeneral.CheckedState.FillColor = Color.PeachPuff;
            btnConfiguracionGeneral.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnConfiguracionGeneral.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnConfiguracionGeneral.CustomImages.ImageSize = new Size(24, 24);
            btnConfiguracionGeneral.Dock = DockStyle.Fill;
            btnConfiguracionGeneral.FillColor = Color.White;
            btnConfiguracionGeneral.Font = new Font("Segoe UI", 9F);
            btnConfiguracionGeneral.ForeColor = Color.White;
            btnConfiguracionGeneral.ImageSize = new Size(24, 24);
            btnConfiguracionGeneral.Location = new Point(3, 541);
            btnConfiguracionGeneral.Name = "btnConfiguracionGeneral";
            btnConfiguracionGeneral.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnConfiguracionGeneral.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnConfiguracionGeneral.Size = new Size(44, 44);
            btnConfiguracionGeneral.TabIndex = 1;
            btnConfiguracionGeneral.Visible = false;
            // 
            // layoutModulos
            // 
            layoutModulos.BackColor = Color.White;
            layoutModulos.Controls.Add(btnInicio);
            layoutModulos.Dock = DockStyle.Fill;
            layoutModulos.FlowDirection = FlowDirection.TopDown;
            layoutModulos.Location = new Point(0, 0);
            layoutModulos.Margin = new Padding(0);
            layoutModulos.Name = "layoutModulos";
            layoutModulos.Size = new Size(50, 488);
            layoutModulos.TabIndex = 0;
            // 
            // btnInicio
            // 
            btnInicio.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnInicio.CheckedState.FillColor = Color.PeachPuff;
            btnInicio.CustomImages.Image = (Image) resources.GetObject("resource.Image2");
            btnInicio.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnInicio.CustomImages.ImageSize = new Size(24, 24);
            btnInicio.FillColor = Color.White;
            btnInicio.Font = new Font("Segoe UI", 9F);
            btnInicio.ForeColor = Color.White;
            btnInicio.ImageSize = new Size(24, 24);
            btnInicio.Location = new Point(3, 3);
            btnInicio.Name = "btnInicio";
            btnInicio.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnInicio.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnInicio.Size = new Size(44, 44);
            btnInicio.TabIndex = 0;
            // 
            // panelCentral
            // 
            panelCentral.Controls.Add(fieldNombreModulo);
            panelCentral.Dock = DockStyle.Fill;
            panelCentral.Location = new Point(50, 0);
            panelCentral.Margin = new Padding(0);
            panelCentral.Name = "panelCentral";
            panelCentral.Size = new Size(1306, 608);
            panelCentral.TabIndex = 1;
            // 
            // fieldNombreModulo
            // 
            fieldNombreModulo.AutoSize = true;
            fieldNombreModulo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldNombreModulo.Location = new Point(7, 16);
            fieldNombreModulo.Name = "fieldNombreModulo";
            fieldNombreModulo.Size = new Size(47, 20);
            fieldNombreModulo.TabIndex = 2;
            fieldNombreModulo.Text = "Inicio";
            fieldNombreModulo.Visible = false;
            // 
            // VistaContenedorModulos
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1356, 608);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 11.25F);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaContenedorModulos";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "FormularioFormatoBase1";
            layoutBase.ResumeLayout(false);
            layoutDistribucion.ResumeLayout(false);
            layoutMenuLateral.ResumeLayout(false);
            layoutModulos.ResumeLayout(false);
            panelCentral.ResumeLayout(false);
            panelCentral.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutDistribucion;
        private TableLayoutPanel layoutMenuLateral;
        private FlowLayoutPanel layoutModulos;
        private Panel panelCentral;
        private Guna2CircleButton btnInicio;
        private Guna2CircleButton btnGestorModulos;
        private Guna2CircleButton btnConfiguracionGeneral;
        private Label fieldNombreModulo;
    }
}