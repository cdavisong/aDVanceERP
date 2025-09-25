using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetalleCompraventaProducto {
    partial class VistaTuplaDetalleCompraventaProducto {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            components = new Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaDetalleCompraventaProducto));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldPrecioCompraventa = new Guna2TextBox();
            btnEliminar = new Guna2Button();
            fieldNombreProducto = new Label();
            fieldCantidad = new Label();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
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
            layoutBase.BackColor = Color.Gainsboro;
            layoutBase.ColumnCount = 1;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBase.Controls.Add(layoutVista, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBase.Size = new Size(417, 42);
            layoutBase.TabIndex = 0;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 4;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(fieldPrecioCompraventa, 0, 0);
            layoutVista.Controls.Add(btnEliminar, 3, 0);
            layoutVista.Controls.Add(fieldNombreProducto, 0, 0);
            layoutVista.Controls.Add(fieldCantidad, 2, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(417, 41);
            layoutVista.TabIndex = 0;
            // 
            // fieldPrecioCompraventa
            // 
            fieldPrecioCompraventa.Animated = true;
            fieldPrecioCompraventa.AutoRoundedCorners = true;
            fieldPrecioCompraventa.BorderColor = Color.Gainsboro;
            fieldPrecioCompraventa.BorderRadius = 16;
            fieldPrecioCompraventa.Cursor = Cursors.IBeam;
            fieldPrecioCompraventa.CustomizableEdges = customizableEdges1;
            fieldPrecioCompraventa.DefaultText = "";
            fieldPrecioCompraventa.DisabledState.BorderColor = Color.White;
            fieldPrecioCompraventa.DisabledState.ForeColor = Color.DimGray;
            fieldPrecioCompraventa.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldPrecioCompraventa.Dock = DockStyle.Fill;
            fieldPrecioCompraventa.FocusedState.BorderColor = Color.SandyBrown;
            fieldPrecioCompraventa.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldPrecioCompraventa.ForeColor = Color.Black;
            fieldPrecioCompraventa.HoverState.BorderColor = Color.SandyBrown;
            fieldPrecioCompraventa.IconLeftOffset = new Point(10, 0);
            fieldPrecioCompraventa.IconRight = (Image) resources.GetObject("fieldPrecioCompraventa.IconRight");
            fieldPrecioCompraventa.IconRightOffset = new Point(6, 0);
            fieldPrecioCompraventa.IconRightSize = new Size(12, 12);
            fieldPrecioCompraventa.Location = new Point(200, 3);
            fieldPrecioCompraventa.Name = "fieldPrecioCompraventa";
            fieldPrecioCompraventa.PasswordChar = '\0';
            fieldPrecioCompraventa.PlaceholderForeColor = Color.DimGray;
            fieldPrecioCompraventa.PlaceholderText = "Precio";
            fieldPrecioCompraventa.SelectedText = "";
            fieldPrecioCompraventa.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldPrecioCompraventa.Size = new Size(124, 35);
            fieldPrecioCompraventa.TabIndex = 5;
            fieldPrecioCompraventa.TextAlign = HorizontalAlignment.Right;
            fieldPrecioCompraventa.TextOffset = new Point(5, 0);
            // 
            // btnEliminar
            // 
            btnEliminar.Animated = true;
            btnEliminar.BorderColor = Color.Gainsboro;
            btnEliminar.BorderRadius = 16;
            btnEliminar.BorderThickness = 1;
            btnEliminar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage");
            btnEliminar.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnEliminar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEliminar.CustomizableEdges = customizableEdges3;
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.FillColor = Color.White;
            btnEliminar.Font = new Font("Segoe UI", 9.75F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.HoverState.BorderColor = Color.PeachPuff;
            btnEliminar.HoverState.FillColor = Color.PeachPuff;
            btnEliminar.HoverState.ForeColor = Color.White;
            btnEliminar.Location = new Point(380, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnEliminar.Size = new Size(34, 35);
            btnEliminar.TabIndex = 0;
            // 
            // fieldNombreProducto
            // 
            fieldNombreProducto.Dock = DockStyle.Fill;
            fieldNombreProducto.Font = new Font("Segoe UI", 11.25F);
            fieldNombreProducto.ForeColor = Color.DimGray;
            fieldNombreProducto.ImeMode = ImeMode.NoControl;
            fieldNombreProducto.Location = new Point(1, 1);
            fieldNombreProducto.Margin = new Padding(1);
            fieldNombreProducto.Name = "fieldNombreProducto";
            fieldNombreProducto.Size = new Size(195, 39);
            fieldNombreProducto.TabIndex = 1;
            fieldNombreProducto.Text = "nombreProducto";
            fieldNombreProducto.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldCantidad
            // 
            fieldCantidad.Dock = DockStyle.Fill;
            fieldCantidad.Font = new Font("Segoe UI", 11.25F);
            fieldCantidad.ForeColor = Color.DimGray;
            fieldCantidad.ImeMode = ImeMode.NoControl;
            fieldCantidad.Location = new Point(328, 1);
            fieldCantidad.Margin = new Padding(1);
            fieldCantidad.Name = "fieldCantidad";
            fieldCantidad.Size = new Size(48, 39);
            fieldCantidad.TabIndex = 4;
            fieldCantidad.Text = "cant.";
            fieldCantidad.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VistaTuplaDetalleCompraventaProducto
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(417, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaDetalleCompraventaProducto";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaDetalleVentaProducto";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Guna2Button btnEliminar;
        private Label fieldNombreProducto;
        private Label fieldCantidad;
        private Guna2TextBox fieldPrecioCompraventa;
    }
}