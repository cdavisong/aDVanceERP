using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Venta.Vistas {
    partial class VistaMenuVenta {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaMenuVenta));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutDistribucion = new TableLayoutPanel();
            fieldTitulo = new Label();
            panelRelleno = new Panel();
            btnMaestros = new Guna2Button();
            btnEnvios = new Guna2Button();
            btnPagos = new Guna2Button();
            btnVentas = new Guna2Button();
            btnPedidos = new Guna2Button();
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
            layoutBase.Size = new Size(1042, 50);
            layoutBase.TabIndex = 0;
            // 
            // layoutDistribucion
            // 
            layoutDistribucion.BackColor = Color.WhiteSmoke;
            layoutDistribucion.ColumnCount = 3;
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            layoutDistribucion.Controls.Add(fieldTitulo, 0, 0);
            layoutDistribucion.Controls.Add(panelRelleno, 1, 0);
            layoutDistribucion.Dock = DockStyle.Fill;
            layoutDistribucion.Location = new Point(0, 0);
            layoutDistribucion.Margin = new Padding(0);
            layoutDistribucion.Name = "layoutDistribucion";
            layoutDistribucion.RowCount = 1;
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion.Size = new Size(1042, 50);
            layoutDistribucion.TabIndex = 0;
            // 
            // fieldTitulo
            // 
            fieldTitulo.BackColor = Color.WhiteSmoke;
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(0, 0);
            fieldTitulo.Margin = new Padding(0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(160, 50);
            fieldTitulo.TabIndex = 4;
            fieldTitulo.Text = "Venta";
            fieldTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelRelleno
            // 
            panelRelleno.BackColor = Color.WhiteSmoke;
            panelRelleno.Controls.Add(btnMaestros);
            panelRelleno.Controls.Add(btnEnvios);
            panelRelleno.Controls.Add(btnPagos);
            panelRelleno.Controls.Add(btnVentas);
            panelRelleno.Controls.Add(btnPedidos);
            panelRelleno.Dock = DockStyle.Fill;
            panelRelleno.Font = new Font("Segoe UI", 11.25F);
            panelRelleno.Location = new Point(160, 0);
            panelRelleno.Margin = new Padding(0);
            panelRelleno.Name = "panelRelleno";
            panelRelleno.Size = new Size(802, 50);
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
            btnMaestros.CustomImages.Image = (Image)resources.GetObject("resource.Image");
            btnMaestros.CustomImages.ImageAlign = HorizontalAlignment.Left;
            btnMaestros.CustomImages.ImageOffset = new Point(10, 0);
            btnMaestros.CustomImages.ImageSize = new Size(24, 24);
            customizableEdges1.TopLeft = false;
            customizableEdges1.TopRight = false;
            btnMaestros.CustomizableEdges = customizableEdges1;
            btnMaestros.Dock = DockStyle.Left;
            btnMaestros.FillColor = Color.WhiteSmoke;
            btnMaestros.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMaestros.ForeColor = Color.Black;
            btnMaestros.HoverState.FillColor = Color.PeachPuff;
            btnMaestros.Location = new Point(640, 0);
            btnMaestros.Margin = new Padding(0);
            btnMaestros.Name = "btnMaestros";
            btnMaestros.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnMaestros.Size = new Size(160, 50);
            btnMaestros.TabIndex = 18;
            btnMaestros.Text = "Maestros";
            btnMaestros.TextOffset = new Point(5, 0);
            // 
            // btnEnvios
            // 
            btnEnvios.Animated = true;
            btnEnvios.BackColor = Color.WhiteSmoke;
            btnEnvios.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnEnvios.CheckedState.FillColor = Color.WhiteSmoke;
            btnEnvios.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnEnvios.CustomImages.CheckedImage = (Image)resources.GetObject("resource.CheckedImage");
            btnEnvios.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEnvios.CustomImages.ImageOffset = new Point(0, 32);
            btnEnvios.CustomImages.ImageSize = new Size(131, 8);
            btnEnvios.CustomizableEdges = customizableEdges3;
            btnEnvios.Dock = DockStyle.Left;
            btnEnvios.FillColor = Color.WhiteSmoke;
            btnEnvios.Font = new Font("Segoe UI", 11.25F);
            btnEnvios.ForeColor = Color.Black;
            btnEnvios.Location = new Point(480, 0);
            btnEnvios.Margin = new Padding(0);
            btnEnvios.Name = "btnEnvios";
            btnEnvios.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnEnvios.Size = new Size(160, 50);
            btnEnvios.TabIndex = 17;
            btnEnvios.Text = "Envíos";
            // 
            // btnPagos
            // 
            btnPagos.Animated = true;
            btnPagos.BackColor = Color.WhiteSmoke;
            btnPagos.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnPagos.CheckedState.FillColor = Color.WhiteSmoke;
            btnPagos.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnPagos.CustomImages.CheckedImage = (Image)resources.GetObject("resource.CheckedImage1");
            btnPagos.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnPagos.CustomImages.ImageOffset = new Point(0, 32);
            btnPagos.CustomImages.ImageSize = new Size(131, 8);
            btnPagos.CustomizableEdges = customizableEdges5;
            btnPagos.Dock = DockStyle.Left;
            btnPagos.FillColor = Color.WhiteSmoke;
            btnPagos.Font = new Font("Segoe UI", 11.25F);
            btnPagos.ForeColor = Color.Black;
            btnPagos.Location = new Point(320, 0);
            btnPagos.Margin = new Padding(0);
            btnPagos.Name = "btnPagos";
            btnPagos.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnPagos.Size = new Size(160, 50);
            btnPagos.TabIndex = 15;
            btnPagos.Text = "Pagos";
            // 
            // btnVentas
            // 
            btnVentas.Animated = true;
            btnVentas.BackColor = Color.WhiteSmoke;
            btnVentas.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnVentas.CheckedState.FillColor = Color.WhiteSmoke;
            btnVentas.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnVentas.CustomImages.CheckedImage = (Image)resources.GetObject("resource.CheckedImage2");
            btnVentas.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnVentas.CustomImages.ImageOffset = new Point(0, 32);
            btnVentas.CustomImages.ImageSize = new Size(131, 8);
            btnVentas.CustomizableEdges = customizableEdges7;
            btnVentas.Dock = DockStyle.Left;
            btnVentas.FillColor = Color.WhiteSmoke;
            btnVentas.Font = new Font("Segoe UI", 11.25F);
            btnVentas.ForeColor = Color.Black;
            btnVentas.Location = new Point(160, 0);
            btnVentas.Margin = new Padding(0);
            btnVentas.Name = "btnVentas";
            btnVentas.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnVentas.Size = new Size(160, 50);
            btnVentas.TabIndex = 14;
            btnVentas.Text = "Ventas";
            // 
            // btnPedidos
            // 
            btnPedidos.Animated = true;
            btnPedidos.BackColor = Color.WhiteSmoke;
            btnPedidos.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnPedidos.CheckedState.FillColor = Color.WhiteSmoke;
            btnPedidos.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnPedidos.CustomImages.CheckedImage = (Image)resources.GetObject("resource.CheckedImage3");
            btnPedidos.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnPedidos.CustomImages.ImageOffset = new Point(0, 32);
            btnPedidos.CustomImages.ImageSize = new Size(131, 8);
            btnPedidos.CustomizableEdges = customizableEdges9;
            btnPedidos.Dock = DockStyle.Left;
            btnPedidos.FillColor = Color.WhiteSmoke;
            btnPedidos.Font = new Font("Segoe UI", 11.25F);
            btnPedidos.ForeColor = Color.Black;
            btnPedidos.Location = new Point(0, 0);
            btnPedidos.Margin = new Padding(0);
            btnPedidos.Name = "btnPedidos";
            btnPedidos.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnPedidos.Size = new Size(160, 50);
            btnPedidos.TabIndex = 13;
            btnPedidos.Text = "Pedidos";
            // 
            // VistaMenuVenta
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1042, 50);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 11.25F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaMenuVenta";
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
        private Guna2Button btnPagos;
        private Guna2Button btnVentas;
        private Guna2Button btnPedidos;
        private Guna2Button btnMaestros;
        private Guna2Button btnEnvios;
    }
}