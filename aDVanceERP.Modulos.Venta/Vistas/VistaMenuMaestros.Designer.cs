using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Venta.Vistas {
    partial class VistaMenuMaestros {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaMenuMaestros));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutDistribucion = new TableLayoutPanel();
            btnAtras = new Guna2Button();
            fieldTitulo = new Label();
            panelRelleno = new Panel();
            btnMensajeros = new Guna2Button();
            btnClientes = new Guna2Button();
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
            layoutDistribucion.ColumnCount = 3;
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion.Controls.Add(btnAtras, 0, 0);
            layoutDistribucion.Controls.Add(fieldTitulo, 0, 0);
            layoutDistribucion.Controls.Add(panelRelleno, 2, 0);
            layoutDistribucion.Dock = DockStyle.Fill;
            layoutDistribucion.Location = new Point(0, 0);
            layoutDistribucion.Margin = new Padding(0);
            layoutDistribucion.Name = "layoutDistribucion";
            layoutDistribucion.RowCount = 1;
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion.Size = new Size(994, 50);
            layoutDistribucion.TabIndex = 0;
            // 
            // btnAtras
            // 
            btnAtras.Animated = true;
            btnAtras.BackColor = Color.WhiteSmoke;
            btnAtras.BorderRadius = 12;
            btnAtras.CheckedState.FillColor = Color.WhiteSmoke;
            btnAtras.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnAtras.Cursor = Cursors.Hand;
            btnAtras.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnAtras.CustomImages.ImageAlign = HorizontalAlignment.Center;
            customizableEdges1.TopLeft = false;
            customizableEdges1.TopRight = false;
            btnAtras.CustomizableEdges = customizableEdges1;
            btnAtras.Dock = DockStyle.Left;
            btnAtras.FillColor = Color.WhiteSmoke;
            btnAtras.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            btnAtras.ForeColor = Color.Black;
            btnAtras.HoverState.FillColor = Color.PeachPuff;
            btnAtras.Location = new Point(160, 0);
            btnAtras.Margin = new Padding(0);
            btnAtras.Name = "btnAtras";
            btnAtras.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAtras.Size = new Size(35, 50);
            btnAtras.TabIndex = 14;
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
            fieldTitulo.Text = "Maestros";
            fieldTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelRelleno
            // 
            panelRelleno.BackColor = Color.WhiteSmoke;
            panelRelleno.Controls.Add(btnMensajeros);
            panelRelleno.Controls.Add(btnClientes);
            panelRelleno.Dock = DockStyle.Fill;
            panelRelleno.Font = new Font("Segoe UI", 11.25F);
            panelRelleno.Location = new Point(195, 0);
            panelRelleno.Margin = new Padding(0);
            panelRelleno.Name = "panelRelleno";
            panelRelleno.Size = new Size(799, 50);
            panelRelleno.TabIndex = 0;
            // 
            // btnMensajeros
            // 
            btnMensajeros.Animated = true;
            btnMensajeros.BackColor = Color.WhiteSmoke;
            btnMensajeros.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnMensajeros.CheckedState.FillColor = Color.WhiteSmoke;
            btnMensajeros.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnMensajeros.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage");
            btnMensajeros.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnMensajeros.CustomImages.ImageOffset = new Point(0, 32);
            btnMensajeros.CustomImages.ImageSize = new Size(131, 8);
            btnMensajeros.CustomizableEdges = customizableEdges3;
            btnMensajeros.Dock = DockStyle.Left;
            btnMensajeros.FillColor = Color.WhiteSmoke;
            btnMensajeros.Font = new Font("Segoe UI", 11.25F);
            btnMensajeros.ForeColor = Color.Black;
            btnMensajeros.Location = new Point(160, 0);
            btnMensajeros.Margin = new Padding(0);
            btnMensajeros.Name = "btnMensajeros";
            btnMensajeros.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnMensajeros.Size = new Size(160, 50);
            btnMensajeros.TabIndex = 11;
            btnMensajeros.Text = "Mensajeros";
            // 
            // btnClientes
            // 
            btnClientes.Animated = true;
            btnClientes.BackColor = Color.WhiteSmoke;
            btnClientes.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnClientes.CheckedState.FillColor = Color.WhiteSmoke;
            btnClientes.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnClientes.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage1");
            btnClientes.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnClientes.CustomImages.ImageOffset = new Point(0, 32);
            btnClientes.CustomImages.ImageSize = new Size(131, 8);
            btnClientes.CustomizableEdges = customizableEdges5;
            btnClientes.Dock = DockStyle.Left;
            btnClientes.FillColor = Color.WhiteSmoke;
            btnClientes.Font = new Font("Segoe UI", 11.25F);
            btnClientes.ForeColor = Color.Black;
            btnClientes.Location = new Point(0, 0);
            btnClientes.Margin = new Padding(0);
            btnClientes.Name = "btnClientes";
            btnClientes.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnClientes.Size = new Size(160, 50);
            btnClientes.TabIndex = 10;
            btnClientes.Text = "Clientes";
            // 
            // VistaMenuMaestros
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(  245,   245,   245);
            ClientSize = new Size(994, 50);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 11.25F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaMenuMaestros";
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
        private Guna2Button btnMensajeros;
        private Guna2Button btnClientes;
        private Guna2Button btnAtras;
    }
}