namespace aDVanceERP.Modulos.Venta.Vistas {
    partial class VistaProductoCarritoCard {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            panelBase = new Guna.UI2.WinForms.Guna2Panel();
            layoutBase = new TableLayoutPanel();
            fieldPresentaciones = new Guna.UI2.WinForms.Guna2ComboBox();
            fieldNombreProducto = new Label();
            fieldClasificacion = new Label();
            fieldCodigo = new Label();
            btnAgregar = new Guna.UI2.WinForms.Guna2Button();
            fieldPrecioVenta = new Label();
            panelBase.SuspendLayout();
            layoutBase.SuspendLayout();
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
            // panelBase
            // 
            panelBase.BackColor = Color.Transparent;
            panelBase.BorderColor = Color.Gainsboro;
            panelBase.BorderRadius = 16;
            panelBase.BorderThickness = 1;
            panelBase.Controls.Add(layoutBase);
            panelBase.CustomBorderThickness = new Padding(1, 1, 1, 3);
            panelBase.CustomizableEdges = customizableEdges5;
            panelBase.Dock = DockStyle.Fill;
            panelBase.FillColor = Color.White;
            panelBase.Location = new Point(0, 0);
            panelBase.Margin = new Padding(0);
            panelBase.Name = "panelBase";
            panelBase.ShadowDecoration.BorderRadius = 8;
            panelBase.ShadowDecoration.CustomizableEdges = customizableEdges6;
            panelBase.ShadowDecoration.Depth = 10;
            panelBase.Size = new Size(157, 208);
            panelBase.TabIndex = 74;
            // 
            // layoutBase
            // 
            layoutBase.ColumnCount = 1;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBase.Controls.Add(fieldPresentaciones, 0, 5);
            layoutBase.Controls.Add(fieldNombreProducto, 0, 1);
            layoutBase.Controls.Add(fieldClasificacion, 0, 2);
            layoutBase.Controls.Add(fieldCodigo, 0, 3);
            layoutBase.Controls.Add(btnAgregar, 0, 6);
            layoutBase.Controls.Add(fieldPrecioVenta, 0, 7);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 9;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutBase.Size = new Size(157, 208);
            layoutBase.TabIndex = 0;
            // 
            // fieldPresentaciones
            // 
            fieldPresentaciones.Animated = true;
            fieldPresentaciones.BackColor = Color.Transparent;
            fieldPresentaciones.BorderColor = Color.Gainsboro;
            fieldPresentaciones.BorderRadius = 16;
            customizableEdges1.BottomLeft = false;
            customizableEdges1.BottomRight = false;
            fieldPresentaciones.CustomizableEdges = customizableEdges1;
            fieldPresentaciones.Dock = DockStyle.Fill;
            fieldPresentaciones.DrawMode = DrawMode.OwnerDrawFixed;
            fieldPresentaciones.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldPresentaciones.FocusedColor = Color.SandyBrown;
            fieldPresentaciones.FocusedState.BorderColor = Color.SandyBrown;
            fieldPresentaciones.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point,  0);
            fieldPresentaciones.ForeColor = Color.Black;
            fieldPresentaciones.ItemHeight = 29;
            fieldPresentaciones.Items.AddRange(new object[] { "Unidad (u)" });
            fieldPresentaciones.Location = new Point(10, 84);
            fieldPresentaciones.Margin = new Padding(10, 5, 10, 0);
            fieldPresentaciones.Name = "fieldPresentaciones";
            fieldPresentaciones.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldPresentaciones.Size = new Size(137, 35);
            fieldPresentaciones.StartIndex = 0;
            fieldPresentaciones.TabIndex = 62;
            fieldPresentaciones.TextOffset = new Point(10, 0);
            // 
            // fieldNombreProducto
            // 
            fieldNombreProducto.Dock = DockStyle.Fill;
            fieldNombreProducto.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldNombreProducto.ForeColor = Color.Black;
            fieldNombreProducto.ImeMode = ImeMode.NoControl;
            fieldNombreProducto.Location = new Point(10, 11);
            fieldNombreProducto.Margin = new Padding(10, 1, 10, 1);
            fieldNombreProducto.Name = "fieldNombreProducto";
            fieldNombreProducto.Size = new Size(137, 22);
            fieldNombreProducto.TabIndex = 94;
            fieldNombreProducto.Text = "Producto";
            fieldNombreProducto.TextAlign = ContentAlignment.BottomLeft;
            // 
            // fieldClasificacion
            // 
            fieldClasificacion.Dock = DockStyle.Fill;
            fieldClasificacion.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point,  0);
            fieldClasificacion.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldClasificacion.ImeMode = ImeMode.NoControl;
            fieldClasificacion.Location = new Point(10, 35);
            fieldClasificacion.Margin = new Padding(10, 1, 10, 1);
            fieldClasificacion.Name = "fieldClasificacion";
            fieldClasificacion.Size = new Size(137, 18);
            fieldClasificacion.TabIndex = 95;
            fieldClasificacion.Text = "General";
            // 
            // fieldCodigo
            // 
            fieldCodigo.Dock = DockStyle.Fill;
            fieldCodigo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point,  0);
            fieldCodigo.ForeColor = Color.Gray;
            fieldCodigo.ImeMode = ImeMode.NoControl;
            fieldCodigo.Location = new Point(10, 55);
            fieldCodigo.Margin = new Padding(10, 1, 10, 1);
            fieldCodigo.Name = "fieldCodigo";
            fieldCodigo.Size = new Size(137, 18);
            fieldCodigo.TabIndex = 96;
            fieldCodigo.Text = "COD : 0000000000000";
            fieldCodigo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnAgregar
            // 
            btnAgregar.Animated = true;
            btnAgregar.AutoRoundedCorners = true;
            btnAgregar.BorderColor = Color.Gainsboro;
            btnAgregar.BorderRadius = 16;
            btnAgregar.BorderThickness = 1;
            customizableEdges3.TopLeft = false;
            customizableEdges3.TopRight = false;
            btnAgregar.CustomizableEdges = customizableEdges3;
            btnAgregar.Dock = DockStyle.Fill;
            btnAgregar.FillColor = Color.Gainsboro;
            btnAgregar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnAgregar.ForeColor = Color.Black;
            btnAgregar.Location = new Point(10, 119);
            btnAgregar.Margin = new Padding(10, 0, 10, 0);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnAgregar.Size = new Size(137, 35);
            btnAgregar.TabIndex = 99;
            btnAgregar.Text = "Agregar";
            // 
            // fieldPrecioVenta
            // 
            fieldPrecioVenta.Dock = DockStyle.Fill;
            fieldPrecioVenta.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldPrecioVenta.ForeColor = Color.Firebrick;
            fieldPrecioVenta.ImeMode = ImeMode.NoControl;
            fieldPrecioVenta.Location = new Point(10, 155);
            fieldPrecioVenta.Margin = new Padding(10, 1, 10, 1);
            fieldPrecioVenta.Name = "fieldPrecioVenta";
            fieldPrecioVenta.Size = new Size(137, 42);
            fieldPrecioVenta.TabIndex = 100;
            fieldPrecioVenta.Text = "$ 0.00";
            fieldPrecioVenta.TextAlign = ContentAlignment.BottomRight;
            // 
            // VistaProductoCarritoCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(157, 208);
            Controls.Add(panelBase);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VistaProductoCarritoCard";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaProductoCarrito";
            panelBase.ResumeLayout(false);
            layoutBase.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private Guna.UI2.WinForms.Guna2Panel panelBase;
        private TableLayoutPanel layoutBase;
        private Guna.UI2.WinForms.Guna2ComboBox fieldPresentaciones;
        private Label fieldNombreProducto;
        private Label fieldClasificacion;
        private Label fieldCodigo;
        private Guna.UI2.WinForms.Guna2Button btnAgregar;
        private Label fieldPrecioVenta;
    }
}