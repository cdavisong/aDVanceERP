using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    partial class VistaMenuInventario {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaMenuInventario));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutDistribucion = new TableLayoutPanel();
            fieldTitulo = new Label();
            panelRelleno = new Panel();
            btnMaestros = new Guna2Button();
            btnMovimientos = new Guna2Button();
            layoutBase.SuspendLayout();
            layoutDistribucion.SuspendLayout();
            panelRelleno.SuspendLayout();
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
            layoutBase.BackColor = Color.WhiteSmoke;
            layoutBase.ColumnCount = 1;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBase.Controls.Add(layoutDistribucion, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBase.Size = new Size(994, 50);
            layoutBase.TabIndex = 0;
            // 
            // layoutDistribucion
            // 
            layoutDistribucion.BackColor = Color.WhiteSmoke;
            layoutDistribucion.ColumnCount = 2;
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDistribucion.Controls.Add(fieldTitulo, 0, 0);
            layoutDistribucion.Controls.Add(panelRelleno, 1, 0);
            layoutDistribucion.Dock = DockStyle.Fill;
            layoutDistribucion.Location = new Point(0, 0);
            layoutDistribucion.Margin = new Padding(0);
            layoutDistribucion.Name = "layoutDistribucion";
            layoutDistribucion.RowCount = 1;
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion.Size = new Size(994, 50);
            layoutDistribucion.TabIndex = 0;
            // 
            // fieldTitulo
            // 
            fieldTitulo.BackColor = Color.WhiteSmoke;
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            fieldTitulo.ForeColor = Color.Firebrick;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(0, 0);
            fieldTitulo.Margin = new Padding(0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(160, 50);
            fieldTitulo.TabIndex = 4;
            fieldTitulo.Text = "Inventario";
            fieldTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelRelleno
            // 
            panelRelleno.BackColor = Color.WhiteSmoke;
            panelRelleno.Controls.Add(btnMaestros);
            panelRelleno.Controls.Add(btnMovimientos);
            panelRelleno.Dock = DockStyle.Fill;
            panelRelleno.Font = new Font("Segoe UI", 11.25F);
            panelRelleno.Location = new Point(160, 0);
            panelRelleno.Margin = new Padding(0);
            panelRelleno.Name = "panelRelleno";
            panelRelleno.Size = new Size(834, 50);
            panelRelleno.TabIndex = 0;
            // 
            // btnMaestros
            // 
            btnMaestros.Animated = true;
            btnMaestros.BackColor = Color.WhiteSmoke;
            btnMaestros.BorderRadius = 12;
            btnMaestros.CheckedState.FillColor = Color.WhiteSmoke;
            btnMaestros.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnMaestros.Cursor = Cursors.Hand;
            btnMaestros.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnMaestros.CustomImages.ImageAlign = HorizontalAlignment.Left;
            btnMaestros.CustomImages.ImageOffset = new Point(10, 0);
            btnMaestros.CustomImages.ImageSize = new Size(24, 24);
            customizableEdges1.TopLeft = false;
            customizableEdges1.TopRight = false;
            btnMaestros.CustomizableEdges = customizableEdges1;
            btnMaestros.Dock = DockStyle.Left;
            btnMaestros.FillColor = Color.WhiteSmoke;
            btnMaestros.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            btnMaestros.ForeColor = Color.Black;
            btnMaestros.HoverState.FillColor = Color.PeachPuff;
            btnMaestros.Location = new Point(160, 0);
            btnMaestros.Margin = new Padding(0);
            btnMaestros.Name = "btnMaestros";
            btnMaestros.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnMaestros.Size = new Size(160, 50);
            btnMaestros.TabIndex = 16;
            btnMaestros.Text = "Maestros";
            btnMaestros.TextOffset = new Point(5, 0);
            // 
            // btnMovimientos
            // 
            btnMovimientos.Animated = true;
            btnMovimientos.BackColor = Color.WhiteSmoke;
            btnMovimientos.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnMovimientos.CheckedState.FillColor = Color.WhiteSmoke;
            btnMovimientos.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnMovimientos.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage");
            btnMovimientos.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnMovimientos.CustomImages.ImageOffset = new Point(0, 32);
            btnMovimientos.CustomImages.ImageSize = new Size(131, 8);
            btnMovimientos.CustomizableEdges = customizableEdges3;
            btnMovimientos.Dock = DockStyle.Left;
            btnMovimientos.FillColor = Color.WhiteSmoke;
            btnMovimientos.Font = new Font("Segoe UI", 11.25F);
            btnMovimientos.ForeColor = Color.Black;
            btnMovimientos.Location = new Point(0, 0);
            btnMovimientos.Margin = new Padding(0);
            btnMovimientos.Name = "btnMovimientos";
            btnMovimientos.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnMovimientos.Size = new Size(160, 50);
            btnMovimientos.TabIndex = 11;
            btnMovimientos.Text = "Movimientos";
            // 
            // VistaMenuInventario
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(  245,   245,   245);
            ClientSize = new Size(994, 50);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 11.25F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaMenuInventario";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaMenu";
            layoutBase.ResumeLayout(false);
            layoutDistribucion.ResumeLayout(false);
            panelRelleno.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutDistribucion;
        private Panel panelRelleno;
        private Label fieldTitulo;
        private Guna2Button btnMovimientos;
        private Guna2Button btnMaestros;
    }
}