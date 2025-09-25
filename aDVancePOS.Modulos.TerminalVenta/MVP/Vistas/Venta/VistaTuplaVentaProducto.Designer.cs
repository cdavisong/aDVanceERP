namespace aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta {
    partial class VistaTuplaVentaProducto {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaTuplaVentaProducto));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldId = new Label();
            btnEliminar = new Guna.UI2.WinForms.Guna2Button();
            simboloPeso1 = new Label();
            fieldNombreProducto = new Label();
            fieldSubtotal = new Label();
            fieldPrecio = new Guna.UI2.WinForms.Guna2TextBox();
            fieldCantidad = new Guna.UI2.WinForms.Guna2TextBox();
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
            layoutBase.Margin = new Padding(0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBase.Size = new Size(810, 42);
            layoutBase.TabIndex = 0;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 7;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(btnEliminar, 6, 0);
            layoutVista.Controls.Add(simboloPeso1, 5, 0);
            layoutVista.Controls.Add(fieldNombreProducto, 1, 0);
            layoutVista.Controls.Add(fieldSubtotal, 4, 0);
            layoutVista.Controls.Add(fieldPrecio, 1, 0);
            layoutVista.Controls.Add(fieldCantidad, 2, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(810, 41);
            layoutVista.TabIndex = 2;
            // 
            // fieldId
            // 
            fieldId.Dock = DockStyle.Fill;
            fieldId.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldId.ForeColor = Color.DimGray;
            fieldId.ImeMode = ImeMode.NoControl;
            fieldId.Location = new Point(1, 1);
            fieldId.Margin = new Padding(1);
            fieldId.Name = "fieldId";
            fieldId.Size = new Size(58, 39);
            fieldId.TabIndex = 14;
            fieldId.Text = "id";
            fieldId.TextAlign = ContentAlignment.MiddleCenter;
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
            btnEliminar.CustomizableEdges = customizableEdges1;
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.FillColor = Color.White;
            btnEliminar.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.HoverState.BorderColor = Color.PeachPuff;
            btnEliminar.HoverState.FillColor = Color.PeachPuff;
            btnEliminar.HoverState.ForeColor = Color.White;
            btnEliminar.Location = new Point(773, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnEliminar.Size = new Size(34, 35);
            btnEliminar.TabIndex = 0;
            // 
            // simboloPeso1
            // 
            simboloPeso1.Dock = DockStyle.Fill;
            simboloPeso1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            simboloPeso1.ForeColor = Color.Black;
            simboloPeso1.ImageAlign = ContentAlignment.MiddleLeft;
            simboloPeso1.ImeMode = ImeMode.NoControl;
            simboloPeso1.Location = new Point(753, 5);
            simboloPeso1.Margin = new Padding(3, 5, 3, 3);
            simboloPeso1.Name = "simboloPeso1";
            simboloPeso1.Size = new Size(14, 33);
            simboloPeso1.TabIndex = 31;
            simboloPeso1.Text = "$";
            simboloPeso1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldNombreProducto
            // 
            fieldNombreProducto.Dock = DockStyle.Fill;
            fieldNombreProducto.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldNombreProducto.ForeColor = Color.DimGray;
            fieldNombreProducto.ImeMode = ImeMode.NoControl;
            fieldNombreProducto.Location = new Point(61, 1);
            fieldNombreProducto.Margin = new Padding(1);
            fieldNombreProducto.Name = "fieldNombreProducto";
            fieldNombreProducto.Size = new Size(318, 39);
            fieldNombreProducto.TabIndex = 1;
            fieldNombreProducto.Text = "nombreProducto";
            fieldNombreProducto.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldSubtotal
            // 
            fieldSubtotal.Dock = DockStyle.Fill;
            fieldSubtotal.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            fieldSubtotal.ForeColor = Color.Black;
            fieldSubtotal.ImeMode = ImeMode.NoControl;
            fieldSubtotal.Location = new Point(641, 1);
            fieldSubtotal.Margin = new Padding(1);
            fieldSubtotal.Name = "fieldSubtotal";
            fieldSubtotal.Size = new Size(108, 39);
            fieldSubtotal.TabIndex = 32;
            fieldSubtotal.Text = "subtotal";
            fieldSubtotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldPrecio
            // 
            fieldPrecio.Animated = true;
            fieldPrecio.BorderColor = Color.Gainsboro;
            fieldPrecio.BorderRadius = 16;
            fieldPrecio.Cursor = Cursors.IBeam;
            fieldPrecio.CustomizableEdges = customizableEdges3;
            fieldPrecio.DefaultText = "";
            fieldPrecio.DisabledState.BorderColor = Color.White;
            fieldPrecio.DisabledState.ForeColor = Color.DimGray;
            fieldPrecio.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldPrecio.Dock = DockStyle.Fill;
            fieldPrecio.FocusedState.BorderColor = Color.SandyBrown;
            fieldPrecio.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldPrecio.ForeColor = Color.Black;
            fieldPrecio.HoverState.BorderColor = Color.SandyBrown;
            fieldPrecio.IconLeftOffset = new Point(10, 0);
            fieldPrecio.IconRight = (Image) resources.GetObject("fieldPrecio.IconRight");
            fieldPrecio.IconRightOffset = new Point(6, 0);
            fieldPrecio.IconRightSize = new Size(12, 12);
            fieldPrecio.Location = new Point(383, 5);
            fieldPrecio.Margin = new Padding(3, 5, 3, 5);
            fieldPrecio.Name = "fieldPrecio";
            fieldPrecio.PasswordChar = '\0';
            fieldPrecio.PlaceholderForeColor = Color.DimGray;
            fieldPrecio.PlaceholderText = "Precio";
            fieldPrecio.SelectedText = "";
            fieldPrecio.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldPrecio.Size = new Size(124, 31);
            fieldPrecio.TabIndex = 5;
            fieldPrecio.TextAlign = HorizontalAlignment.Right;
            fieldPrecio.TextOffset = new Point(5, 0);
            // 
            // fieldCantidad
            // 
            fieldCantidad.Animated = true;
            fieldCantidad.BorderColor = Color.Gainsboro;
            fieldCantidad.BorderRadius = 16;
            fieldCantidad.Cursor = Cursors.IBeam;
            fieldCantidad.CustomizableEdges = customizableEdges5;
            fieldCantidad.DefaultText = "";
            fieldCantidad.DisabledState.BorderColor = Color.White;
            fieldCantidad.DisabledState.ForeColor = Color.DimGray;
            fieldCantidad.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldCantidad.Dock = DockStyle.Fill;
            fieldCantidad.FocusedState.BorderColor = Color.SandyBrown;
            fieldCantidad.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldCantidad.ForeColor = Color.Black;
            fieldCantidad.HoverState.BorderColor = Color.SandyBrown;
            fieldCantidad.IconLeftOffset = new Point(10, 0);
            fieldCantidad.IconRight = (Image) resources.GetObject("fieldCantidad.IconRight");
            fieldCantidad.IconRightOffset = new Point(6, 0);
            fieldCantidad.IconRightSize = new Size(12, 12);
            fieldCantidad.Location = new Point(513, 5);
            fieldCantidad.Margin = new Padding(3, 5, 3, 5);
            fieldCantidad.Name = "fieldCantidad";
            fieldCantidad.PasswordChar = '\0';
            fieldCantidad.PlaceholderForeColor = Color.DimGray;
            fieldCantidad.PlaceholderText = "Cant.";
            fieldCantidad.ReadOnly = true;
            fieldCantidad.SelectedText = "";
            fieldCantidad.ShadowDecoration.CustomizableEdges = customizableEdges6;
            fieldCantidad.Size = new Size(124, 31);
            fieldCantidad.TabIndex = 33;
            fieldCantidad.TextAlign = HorizontalAlignment.Right;
            fieldCantidad.TextOffset = new Point(5, 0);
            // 
            // VistaTuplaVentaProducto
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(810, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "VistaTuplaVentaProducto";
            Text = "VistaTuplaVenta";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Label fieldId;
        private Guna.UI2.WinForms.Guna2Button btnEliminar;
        private Label simboloPeso1;
        private Label fieldNombreProducto;
        private Label fieldSubtotal;
        private Guna.UI2.WinForms.Guna2TextBox fieldPrecio;
        private Guna.UI2.WinForms.Guna2TextBox fieldCantidad;
    }
}