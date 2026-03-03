using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Compra.Vistas {
    partial class VistaMenuCompra {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaMenuCompra));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            layoutBase = new TableLayoutPanel();
            layoutDistribucion = new TableLayoutPanel();
            panelRelleno = new Panel();
            btnMaestros = new Guna2Button();
            btnPagos = new Guna2Button();
            btnCompras = new Guna2Button();
            btnSolicitudes = new Guna2Button();
            fieldTitulo = new Label();
            layoutBase.SuspendLayout();
            layoutDistribucion.SuspendLayout();
            panelRelleno.SuspendLayout();
            SuspendLayout();
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
            layoutDistribucion.Controls.Add(panelRelleno, 1, 0);
            layoutDistribucion.Controls.Add(fieldTitulo, 0, 0);
            layoutDistribucion.Dock = DockStyle.Fill;
            layoutDistribucion.Location = new Point(0, 0);
            layoutDistribucion.Margin = new Padding(0);
            layoutDistribucion.Name = "layoutDistribucion";
            layoutDistribucion.RowCount = 1;
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion.Size = new Size(1042, 50);
            layoutDistribucion.TabIndex = 0;
            // 
            // panelRelleno
            // 
            panelRelleno.BackColor = Color.WhiteSmoke;
            panelRelleno.Controls.Add(btnMaestros);
            panelRelleno.Controls.Add(btnPagos);
            panelRelleno.Controls.Add(btnCompras);
            panelRelleno.Controls.Add(btnSolicitudes);
            panelRelleno.Dock = DockStyle.Fill;
            panelRelleno.Font = new Font("Segoe UI", 11.25F);
            panelRelleno.Location = new Point(160, 0);
            panelRelleno.Margin = new Padding(0);
            panelRelleno.Name = "panelRelleno";
            panelRelleno.Size = new Size(802, 50);
            panelRelleno.TabIndex = 5;
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
            btnMaestros.Location = new Point(480, 0);
            btnMaestros.Margin = new Padding(0);
            btnMaestros.Name = "btnMaestros";
            btnMaestros.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnMaestros.Size = new Size(160, 50);
            btnMaestros.TabIndex = 20;
            btnMaestros.Text = "Maestros";
            btnMaestros.TextOffset = new Point(5, 0);
            // 
            // btnPagos
            // 
            btnPagos.Animated = true;
            btnPagos.BackColor = Color.WhiteSmoke;
            btnPagos.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnPagos.CheckedState.FillColor = Color.WhiteSmoke;
            btnPagos.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnPagos.CustomImages.CheckedImage = Properties.Resources.barra_seleccion;
            btnPagos.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnPagos.CustomImages.ImageOffset = new Point(0, 32);
            btnPagos.CustomImages.ImageSize = new Size(131, 8);
            btnPagos.CustomizableEdges = customizableEdges3;
            btnPagos.Dock = DockStyle.Left;
            btnPagos.FillColor = Color.WhiteSmoke;
            btnPagos.Font = new Font("Segoe UI", 11.25F);
            btnPagos.ForeColor = Color.Black;
            btnPagos.Location = new Point(320, 0);
            btnPagos.Margin = new Padding(0);
            btnPagos.Name = "btnPagos";
            btnPagos.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnPagos.Size = new Size(160, 50);
            btnPagos.TabIndex = 19;
            btnPagos.Text = "Pagos";
            // 
            // btnCompras
            // 
            btnCompras.Animated = true;
            btnCompras.BackColor = Color.WhiteSmoke;
            btnCompras.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnCompras.CheckedState.FillColor = Color.WhiteSmoke;
            btnCompras.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnCompras.CustomImages.CheckedImage = Properties.Resources.barra_seleccion;
            btnCompras.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnCompras.CustomImages.ImageOffset = new Point(0, 32);
            btnCompras.CustomImages.ImageSize = new Size(131, 8);
            btnCompras.CustomizableEdges = customizableEdges5;
            btnCompras.Dock = DockStyle.Left;
            btnCompras.FillColor = Color.WhiteSmoke;
            btnCompras.Font = new Font("Segoe UI", 11.25F);
            btnCompras.ForeColor = Color.Black;
            btnCompras.Location = new Point(160, 0);
            btnCompras.Margin = new Padding(0);
            btnCompras.Name = "btnCompras";
            btnCompras.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnCompras.Size = new Size(160, 50);
            btnCompras.TabIndex = 14;
            btnCompras.Text = "Compras";
            // 
            // btnSolicitudes
            // 
            btnSolicitudes.Animated = true;
            btnSolicitudes.BackColor = Color.WhiteSmoke;
            btnSolicitudes.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnSolicitudes.CheckedState.FillColor = Color.WhiteSmoke;
            btnSolicitudes.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnSolicitudes.CustomImages.CheckedImage = Properties.Resources.barra_seleccion;
            btnSolicitudes.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnSolicitudes.CustomImages.ImageOffset = new Point(0, 32);
            btnSolicitudes.CustomImages.ImageSize = new Size(131, 8);
            btnSolicitudes.CustomizableEdges = customizableEdges7;
            btnSolicitudes.Dock = DockStyle.Left;
            btnSolicitudes.FillColor = Color.WhiteSmoke;
            btnSolicitudes.Font = new Font("Segoe UI", 11.25F);
            btnSolicitudes.ForeColor = Color.Black;
            btnSolicitudes.Location = new Point(0, 0);
            btnSolicitudes.Margin = new Padding(0);
            btnSolicitudes.Name = "btnSolicitudes";
            btnSolicitudes.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnSolicitudes.Size = new Size(160, 50);
            btnSolicitudes.TabIndex = 13;
            btnSolicitudes.Text = "Solicitudes";
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
            fieldTitulo.Text = "Compra";
            fieldTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VistaMenuCompra
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(  245,   245,   245);
            ClientSize = new Size(1042, 50);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 11.25F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaMenuCompra";
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
        private Label fieldTitulo;
        private Panel panelRelleno;
        private Guna2Button btnCompras;
        private Guna2Button btnSolicitudes;
        private Guna2Button btnMaestros;
        private Guna2Button btnPagos;
    }
}