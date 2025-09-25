using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Menu {
    partial class VistaMenuCompraventas {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaMenuCompraventas));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutDistribucion = new TableLayoutPanel();
            fieldTitulo = new Label();
            panelBotones = new Panel();
            btnVenta = new Guna2Button();
            btnCompra = new Guna2Button();
            layoutBase.SuspendLayout();
            layoutDistribucion.SuspendLayout();
            panelBotones.SuspendLayout();
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
            layoutDistribucion.Controls.Add(panelBotones, 1, 0);
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
            fieldTitulo.Text = "Compraventas";
            fieldTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelBotones
            // 
            panelBotones.BackColor = Color.WhiteSmoke;
            panelBotones.Controls.Add(btnCompra);
            panelBotones.Controls.Add(btnVenta);
            panelBotones.Dock = DockStyle.Fill;
            panelBotones.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            panelBotones.Location = new Point(160, 0);
            panelBotones.Margin = new Padding(0);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(834, 50);
            panelBotones.TabIndex = 0;
            // 
            // btnVenta
            // 
            btnVenta.Animated = true;
            btnVenta.BackColor = Color.WhiteSmoke;
            btnVenta.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnVenta.CheckedState.FillColor = Color.WhiteSmoke;
            btnVenta.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnVenta.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage1");
            btnVenta.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnVenta.CustomImages.ImageOffset = new Point(0, 32);
            btnVenta.CustomImages.ImageSize = new Size(131, 8);
            btnVenta.CustomizableEdges = customizableEdges3;
            btnVenta.Dock = DockStyle.Left;
            btnVenta.FillColor = Color.WhiteSmoke;
            btnVenta.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnVenta.ForeColor = Color.Black;
            btnVenta.Location = new Point(0, 0);
            btnVenta.Margin = new Padding(0);
            btnVenta.Name = "btnVenta";
            btnVenta.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnVenta.Size = new Size(160, 50);
            btnVenta.TabIndex = 12;
            btnVenta.Text = "Venta";
            // 
            // btnCompra
            // 
            btnCompra.Animated = true;
            btnCompra.BackColor = Color.WhiteSmoke;
            btnCompra.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnCompra.CheckedState.FillColor = Color.WhiteSmoke;
            btnCompra.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnCompra.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage");
            btnCompra.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnCompra.CustomImages.ImageOffset = new Point(0, 32);
            btnCompra.CustomImages.ImageSize = new Size(131, 8);
            btnCompra.CustomizableEdges = customizableEdges1;
            btnCompra.Dock = DockStyle.Left;
            btnCompra.FillColor = Color.WhiteSmoke;
            btnCompra.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnCompra.ForeColor = Color.Black;
            btnCompra.Location = new Point(160, 0);
            btnCompra.Margin = new Padding(0);
            btnCompra.Name = "btnCompra";
            btnCompra.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCompra.Size = new Size(160, 50);
            btnCompra.TabIndex = 13;
            btnCompra.Text = "Compra";
            // 
            // VistaMenuCompraventas
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(  245,   245,   245);
            ClientSize = new Size(994, 50);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaMenuCompraventas";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaMenu";
            layoutBase.ResumeLayout(false);
            layoutDistribucion.ResumeLayout(false);
            panelBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutDistribucion;
        private Panel panelBotones;
        private Label fieldTitulo;
        private Guna2Button btnVenta;
        private Guna2Button btnCompra;
    }
}