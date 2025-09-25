using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Menu {
    partial class VistaMenuContacto {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaMenuContacto));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutDistribucion = new TableLayoutPanel();
            fieldTitulo = new Label();
            panelRelleno = new Panel();
            btnProveedores = new Guna2Button();
            btnMensajeros = new Guna2Button();
            btnClientes = new Guna2Button();
            btnContactos = new Guna2Button();
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
            fieldTitulo.Text = "Contacto";
            fieldTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelRelleno
            // 
            panelRelleno.BackColor = Color.WhiteSmoke;
            panelRelleno.Controls.Add(btnContactos);
            panelRelleno.Controls.Add(btnClientes);
            panelRelleno.Controls.Add(btnMensajeros);
            panelRelleno.Controls.Add(btnProveedores);
            panelRelleno.Dock = DockStyle.Fill;
            panelRelleno.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            panelRelleno.Location = new Point(160, 0);
            panelRelleno.Margin = new Padding(0);
            panelRelleno.Name = "panelRelleno";
            panelRelleno.Size = new Size(834, 50);
            panelRelleno.TabIndex = 0;
            // 
            // btnProveedores
            // 
            btnProveedores.Animated = true;
            btnProveedores.BackColor = Color.WhiteSmoke;
            btnProveedores.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnProveedores.CheckedState.FillColor = Color.WhiteSmoke;
            btnProveedores.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnProveedores.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage3");
            btnProveedores.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnProveedores.CustomImages.ImageOffset = new Point(0, 32);
            btnProveedores.CustomImages.ImageSize = new Size(131, 8);
            btnProveedores.CustomizableEdges = customizableEdges7;
            btnProveedores.Dock = DockStyle.Left;
            btnProveedores.FillColor = Color.WhiteSmoke;
            btnProveedores.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnProveedores.ForeColor = Color.Black;
            btnProveedores.Location = new Point(0, 0);
            btnProveedores.Margin = new Padding(0);
            btnProveedores.Name = "btnProveedores";
            btnProveedores.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnProveedores.Size = new Size(160, 50);
            btnProveedores.TabIndex = 10;
            btnProveedores.Text = "Proveedores";
            // 
            // btnMensajeros
            // 
            btnMensajeros.Animated = true;
            btnMensajeros.BackColor = Color.WhiteSmoke;
            btnMensajeros.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnMensajeros.CheckedState.FillColor = Color.WhiteSmoke;
            btnMensajeros.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnMensajeros.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage2");
            btnMensajeros.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnMensajeros.CustomImages.ImageOffset = new Point(0, 32);
            btnMensajeros.CustomImages.ImageSize = new Size(131, 8);
            btnMensajeros.CustomizableEdges = customizableEdges5;
            btnMensajeros.Dock = DockStyle.Left;
            btnMensajeros.FillColor = Color.WhiteSmoke;
            btnMensajeros.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnMensajeros.ForeColor = Color.Black;
            btnMensajeros.Location = new Point(160, 0);
            btnMensajeros.Margin = new Padding(0);
            btnMensajeros.Name = "btnMensajeros";
            btnMensajeros.ShadowDecoration.CustomizableEdges = customizableEdges6;
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
            btnClientes.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnClientes.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage1");
            btnClientes.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnClientes.CustomImages.ImageOffset = new Point(0, 32);
            btnClientes.CustomImages.ImageSize = new Size(131, 8);
            btnClientes.CustomizableEdges = customizableEdges3;
            btnClientes.Dock = DockStyle.Left;
            btnClientes.FillColor = Color.WhiteSmoke;
            btnClientes.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClientes.ForeColor = Color.Black;
            btnClientes.Location = new Point(320, 0);
            btnClientes.Margin = new Padding(0);
            btnClientes.Name = "btnClientes";
            btnClientes.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnClientes.Size = new Size(160, 50);
            btnClientes.TabIndex = 12;
            btnClientes.Text = "Clientes";
            // 
            // btnContactos
            // 
            btnContactos.Animated = true;
            btnContactos.BackColor = Color.WhiteSmoke;
            btnContactos.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnContactos.CheckedState.FillColor = Color.WhiteSmoke;
            btnContactos.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnContactos.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage");
            btnContactos.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnContactos.CustomImages.ImageOffset = new Point(0, 32);
            btnContactos.CustomImages.ImageSize = new Size(131, 8);
            btnContactos.CustomizableEdges = customizableEdges1;
            btnContactos.Dock = DockStyle.Left;
            btnContactos.FillColor = Color.WhiteSmoke;
            btnContactos.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnContactos.ForeColor = Color.Black;
            btnContactos.Location = new Point(480, 0);
            btnContactos.Margin = new Padding(0);
            btnContactos.Name = "btnContactos";
            btnContactos.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnContactos.Size = new Size(160, 50);
            btnContactos.TabIndex = 13;
            btnContactos.Text = "Contactos";
            // 
            // VistaMenuContacto
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(  245,   245,   245);
            ClientSize = new Size(994, 50);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaMenuContacto";
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
        private Guna2Button btnContactos;
        private Guna2Button btnClientes;
        private Guna2Button btnMensajeros;
        private Guna2Button btnProveedores;
    }
}