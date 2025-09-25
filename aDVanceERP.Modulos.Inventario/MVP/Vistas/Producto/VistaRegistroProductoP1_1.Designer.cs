namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto {
    partial class VistaRegistroProductoP1_1 {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaRegistroProductoP1_1));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            fieldTituloNombreProveedor = new Label();
            fieldNombreProveedor = new Guna.UI2.WinForms.Guna2ComboBox();
            layoutEsVendible = new TableLayoutPanel();
            fieldTextoEsVendible = new Label();
            fieldEsVendible = new Guna.UI2.WinForms.Guna2CheckBox();
            separador1 = new Guna.UI2.WinForms.Guna2Separator();
            layoutBase.SuspendLayout();
            layoutEsVendible.SuspendLayout();
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
            layoutBase.BackColor = Color.White;
            layoutBase.ColumnCount = 1;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBase.Controls.Add(separador1, 0, 2);
            layoutBase.Controls.Add(fieldTituloNombreProveedor, 0, 0);
            layoutBase.Controls.Add(fieldNombreProveedor, 0, 1);
            layoutBase.Controls.Add(layoutEsVendible, 0, 3);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 4;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBase.Size = new Size(417, 188);
            layoutBase.TabIndex = 0;
            // 
            // fieldTituloNombreProveedor
            // 
            fieldTituloNombreProveedor.Dock = DockStyle.Fill;
            fieldTituloNombreProveedor.Font = new Font("Segoe UI", 11.25F);
            fieldTituloNombreProveedor.ForeColor = Color.DimGray;
            fieldTituloNombreProveedor.Image = (Image) resources.GetObject("fieldTituloNombreProveedor.Image");
            fieldTituloNombreProveedor.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloNombreProveedor.ImeMode = ImeMode.NoControl;
            fieldTituloNombreProveedor.Location = new Point(15, 5);
            fieldTituloNombreProveedor.Margin = new Padding(15, 5, 3, 3);
            fieldTituloNombreProveedor.Name = "fieldTituloNombreProveedor";
            fieldTituloNombreProveedor.Size = new Size(399, 27);
            fieldTituloNombreProveedor.TabIndex = 11;
            fieldTituloNombreProveedor.Text = "      Proveedor :";
            fieldTituloNombreProveedor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldNombreProveedor
            // 
            fieldNombreProveedor.Animated = true;
            fieldNombreProveedor.BackColor = Color.Transparent;
            fieldNombreProveedor.BorderColor = Color.Gainsboro;
            fieldNombreProveedor.BorderRadius = 16;
            fieldNombreProveedor.CustomizableEdges = customizableEdges7;
            fieldNombreProveedor.Dock = DockStyle.Fill;
            fieldNombreProveedor.DrawMode = DrawMode.OwnerDrawFixed;
            fieldNombreProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldNombreProveedor.FocusedColor = Color.SandyBrown;
            fieldNombreProveedor.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombreProveedor.Font = new Font("Segoe UI", 11.25F);
            fieldNombreProveedor.ForeColor = Color.Black;
            fieldNombreProveedor.ItemHeight = 29;
            fieldNombreProveedor.Location = new Point(5, 40);
            fieldNombreProveedor.Margin = new Padding(5);
            fieldNombreProveedor.Name = "fieldNombreProveedor";
            fieldNombreProveedor.ShadowDecoration.CustomizableEdges = customizableEdges8;
            fieldNombreProveedor.Size = new Size(407, 35);
            fieldNombreProveedor.TabIndex = 12;
            fieldNombreProveedor.TextOffset = new Point(10, 0);
            // 
            // layoutTerminosServicio
            // 
            layoutEsVendible.ColumnCount = 2;
            layoutEsVendible.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 26F));
            layoutEsVendible.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEsVendible.Controls.Add(fieldTextoEsVendible, 1, 0);
            layoutEsVendible.Controls.Add(fieldEsVendible, 0, 0);
            layoutEsVendible.Dock = DockStyle.Fill;
            layoutEsVendible.Location = new Point(15, 100);
            layoutEsVendible.Margin = new Padding(15, 0, 0, 0);
            layoutEsVendible.Name = "layoutTerminosServicio";
            layoutEsVendible.RowCount = 1;
            layoutEsVendible.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEsVendible.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutEsVendible.Size = new Size(402, 88);
            layoutEsVendible.TabIndex = 13;
            // 
            // fieldTextoEsVendible
            // 
            fieldTextoEsVendible.Dock = DockStyle.Fill;
            fieldTextoEsVendible.Font = new Font("Segoe UI", 11.25F);
            fieldTextoEsVendible.ForeColor = Color.Black;
            fieldTextoEsVendible.ImeMode = ImeMode.NoControl;
            fieldTextoEsVendible.Location = new Point(31, 5);
            fieldTextoEsVendible.Margin = new Padding(5, 5, 1, 1);
            fieldTextoEsVendible.Name = "fieldTextoEsVendible";
            fieldTextoEsVendible.Size = new Size(370, 82);
            fieldTextoEsVendible.TabIndex = 1;
            fieldTextoEsVendible.Text = "Disponible para venta directa";
            // 
            // fieldEsVendible
            // 
            fieldEsVendible.BackColor = Color.White;
            fieldEsVendible.CheckedState.BorderColor = Color.Gainsboro;
            fieldEsVendible.CheckedState.BorderRadius = 4;
            fieldEsVendible.CheckedState.BorderThickness = 1;
            fieldEsVendible.CheckedState.FillColor = Color.WhiteSmoke;
            fieldEsVendible.CheckMarkColor = Color.Black;
            fieldEsVendible.Dock = DockStyle.Top;
            fieldEsVendible.Font = new Font("Segoe UI", 12F);
            fieldEsVendible.Location = new Point(5, 5);
            fieldEsVendible.Margin = new Padding(5, 5, 5, 15);
            fieldEsVendible.Name = "fieldEsVendible";
            fieldEsVendible.Size = new Size(16, 25);
            fieldEsVendible.TabIndex = 0;
            fieldEsVendible.UncheckedState.BorderColor = Color.Gainsboro;
            fieldEsVendible.UncheckedState.BorderRadius = 4;
            fieldEsVendible.UncheckedState.BorderThickness = 1;
            fieldEsVendible.UncheckedState.FillColor = Color.PeachPuff;
            fieldEsVendible.UseVisualStyleBackColor = false;
            // 
            // separador1
            // 
            separador1.Dock = DockStyle.Fill;
            separador1.FillColor = Color.FromArgb(  208,   197,   188);
            separador1.Location = new Point(3, 83);
            separador1.Name = "separador1";
            separador1.Size = new Size(411, 14);
            separador1.TabIndex = 14;
            // 
            // VistaRegistroProductoP1_1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(417, 188);
            Controls.Add(layoutBase);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VistaRegistroProductoP1_1";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroProductoP1_1";
            layoutBase.ResumeLayout(false);
            layoutEsVendible.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private Label fieldTituloNombreProveedor;
        private Guna.UI2.WinForms.Guna2ComboBox fieldNombreProveedor;
        private TableLayoutPanel layoutEsVendible;
        private Label fieldTextoEsVendible;
        private Guna.UI2.WinForms.Guna2CheckBox fieldEsVendible;
        private Guna.UI2.WinForms.Guna2Separator separador1;
    }
}