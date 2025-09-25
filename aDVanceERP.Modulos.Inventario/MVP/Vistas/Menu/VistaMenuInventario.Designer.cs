using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Menu {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaMenuInventario));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutDistribucion = new TableLayoutPanel();
            fieldTitulo = new Label();
            panelRelleno = new Panel();
            btnProductos = new Guna2Button();
            btnMovimientos = new Guna2Button();
            btnAlmacenes = new Guna2Button();
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
            fieldTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            fieldTitulo.ForeColor = Color.Black;
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
            panelRelleno.Controls.Add(btnAlmacenes);
            panelRelleno.Controls.Add(btnMovimientos);
            panelRelleno.Controls.Add(btnProductos);
            panelRelleno.Dock = DockStyle.Fill;
            panelRelleno.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            panelRelleno.Location = new Point(160, 0);
            panelRelleno.Margin = new Padding(0);
            panelRelleno.Name = "panelRelleno";
            panelRelleno.Size = new Size(834, 50);
            panelRelleno.TabIndex = 0;
            // 
            // btnProductos
            // 
            btnProductos.Animated = true;
            btnProductos.BackColor = Color.WhiteSmoke;
            btnProductos.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnProductos.CheckedState.FillColor = Color.WhiteSmoke;
            btnProductos.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnProductos.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage2");
            btnProductos.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnProductos.CustomImages.ImageOffset = new Point(0, 32);
            btnProductos.CustomImages.ImageSize = new Size(131, 8);
            btnProductos.CustomizableEdges = customizableEdges5;
            btnProductos.Dock = DockStyle.Left;
            btnProductos.FillColor = Color.WhiteSmoke;
            btnProductos.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnProductos.ForeColor = Color.Black;
            btnProductos.Location = new Point(0, 0);
            btnProductos.Margin = new Padding(0);
            btnProductos.Name = "btnProductos";
            btnProductos.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnProductos.Size = new Size(160, 50);
            btnProductos.TabIndex = 10;
            btnProductos.Text = "Productos";
            // 
            // btnMovimientos
            // 
            btnMovimientos.Animated = true;
            btnMovimientos.BackColor = Color.WhiteSmoke;
            btnMovimientos.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnMovimientos.CheckedState.FillColor = Color.WhiteSmoke;
            btnMovimientos.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnMovimientos.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage1");
            btnMovimientos.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnMovimientos.CustomImages.ImageOffset = new Point(0, 32);
            btnMovimientos.CustomImages.ImageSize = new Size(131, 8);
            btnMovimientos.CustomizableEdges = customizableEdges3;
            btnMovimientos.Dock = DockStyle.Left;
            btnMovimientos.FillColor = Color.WhiteSmoke;
            btnMovimientos.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnMovimientos.ForeColor = Color.Black;
            btnMovimientos.Location = new Point(160, 0);
            btnMovimientos.Margin = new Padding(0);
            btnMovimientos.Name = "btnMovimientos";
            btnMovimientos.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnMovimientos.Size = new Size(160, 50);
            btnMovimientos.TabIndex = 11;
            btnMovimientos.Text = "Movimientos";
            // 
            // btnAlmacenes
            // 
            btnAlmacenes.Animated = true;
            btnAlmacenes.BackColor = Color.WhiteSmoke;
            btnAlmacenes.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnAlmacenes.CheckedState.FillColor = Color.WhiteSmoke;
            btnAlmacenes.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnAlmacenes.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage");
            btnAlmacenes.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnAlmacenes.CustomImages.ImageOffset = new Point(0, 32);
            btnAlmacenes.CustomImages.ImageSize = new Size(131, 8);
            btnAlmacenes.CustomizableEdges = customizableEdges1;
            btnAlmacenes.Dock = DockStyle.Left;
            btnAlmacenes.FillColor = Color.WhiteSmoke;
            btnAlmacenes.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnAlmacenes.ForeColor = Color.Black;
            btnAlmacenes.Location = new Point(320, 0);
            btnAlmacenes.Margin = new Padding(0);
            btnAlmacenes.Name = "btnAlmacenes";
            btnAlmacenes.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAlmacenes.Size = new Size(160, 50);
            btnAlmacenes.TabIndex = 12;
            btnAlmacenes.Text = "Almacenes";
            // 
            // VistaMenuInventario
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(  245,   245,   245);
            ClientSize = new Size(994, 50);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
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
        private Guna2Button btnAlmacenes;
        private Guna2Button btnMovimientos;
        private Guna2Button btnProductos;
    }
}