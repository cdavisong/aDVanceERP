using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Almacen {
    partial class VistaRegistroAlmacen {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaRegistroAlmacen));
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
            layoutVista = new TableLayoutPanel();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            fieldNombre = new Guna2TextBox();
            layoutTitulo = new TableLayoutPanel();
            fieldTitulo = new Label();
            fieldDireccion = new Guna2TextBox();
            fieldNotas = new Guna2TextBox();
            layoutAutorizoVenta = new TableLayoutPanel();
            fieldTituloAutorizoVentaProductos = new Label();
            fieldAutorizoVentaProductos = new Guna2CheckBox();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna2Button();
            btnRegistrar = new Guna2Button();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutTitulo.SuspendLayout();
            layoutAutorizoVenta.SuspendLayout();
            layoutBotones.SuspendLayout();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.AnimateWindow = true;
            formatoBase.AnimationType = Guna2BorderlessForm.AnimateWindowType.AW_HOR_NEGATIVE;
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
            formatoBase.DragForm = false;
            formatoBase.HasFormShadow = false;
            formatoBase.TransparentWhileDrag = true;
            // 
            // layoutBase
            // 
            layoutBase.BackColor = Color.Gainsboro;
            layoutBase.ColumnCount = 2;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBase.Controls.Add(layoutVista, 1, 0);
            layoutBase.Controls.Add(layoutBotones, 1, 1);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 2;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            layoutBase.Size = new Size(500, 685);
            layoutBase.TabIndex = 2;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 4;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(fieldIcono, 1, 1);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 2);
            layoutVista.Controls.Add(fieldNombre, 2, 4);
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldDireccion, 2, 6);
            layoutVista.Controls.Add(fieldNotas, 2, 10);
            layoutVista.Controls.Add(layoutAutorizoVenta, 2, 8);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(13, 0);
            layoutVista.Margin = new Padding(3, 0, 0, 0);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 13;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(487, 620);
            layoutVista.TabIndex = 0;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = (Image) resources.GetObject("fieldIcono.BackgroundImage");
            fieldIcono.BackgroundImageLayout = ImageLayout.Center;
            fieldIcono.Dock = DockStyle.Fill;
            fieldIcono.Location = new Point(20, 26);
            fieldIcono.Margin = new Padding(0, 6, 0, 0);
            fieldIcono.Name = "fieldIcono";
            fieldIcono.Size = new Size(30, 39);
            fieldIcono.TabIndex = 0;
            fieldIcono.TabStop = false;
            // 
            // fieldSubtitulo
            // 
            fieldSubtitulo.Dock = DockStyle.Fill;
            fieldSubtitulo.Font = new Font("Segoe UI", 11.25F);
            fieldSubtitulo.ForeColor = Color.Gray;
            fieldSubtitulo.ImeMode = ImeMode.NoControl;
            fieldSubtitulo.Location = new Point(55, 70);
            fieldSubtitulo.Margin = new Padding(5, 5, 1, 1);
            fieldSubtitulo.Name = "fieldSubtitulo";
            fieldSubtitulo.Size = new Size(411, 39);
            fieldSubtitulo.TabIndex = 0;
            fieldSubtitulo.Text = "Registro";
            // 
            // fieldNombre
            // 
            fieldNombre.Animated = true;
            fieldNombre.BorderColor = Color.Gainsboro;
            fieldNombre.BorderRadius = 16;
            fieldNombre.Cursor = Cursors.IBeam;
            fieldNombre.CustomizableEdges = customizableEdges1;
            fieldNombre.DefaultText = "";
            fieldNombre.DisabledState.BorderColor = Color.White;
            fieldNombre.DisabledState.ForeColor = Color.DimGray;
            fieldNombre.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNombre.Dock = DockStyle.Fill;
            fieldNombre.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombre.Font = new Font("Segoe UI", 11.25F);
            fieldNombre.ForeColor = Color.Black;
            fieldNombre.HoverState.BorderColor = Color.SandyBrown;
            fieldNombre.IconLeft = (Image) resources.GetObject("fieldNombre.IconLeft");
            fieldNombre.IconLeftOffset = new Point(10, 0);
            fieldNombre.Location = new Point(55, 135);
            fieldNombre.Margin = new Padding(5);
            fieldNombre.Name = "fieldNombre";
            fieldNombre.PasswordChar = '\0';
            fieldNombre.PlaceholderForeColor = Color.DimGray;
            fieldNombre.PlaceholderText = "Nombre";
            fieldNombre.SelectedText = "";
            fieldNombre.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldNombre.Size = new Size(407, 35);
            fieldNombre.TabIndex = 1;
            fieldNombre.TextOffset = new Point(5, 0);
            // 
            // layoutTitulo
            // 
            layoutTitulo.ColumnCount = 2;
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutTitulo.Controls.Add(fieldTitulo, 0, 0);
            layoutTitulo.Dock = DockStyle.Fill;
            layoutTitulo.Location = new Point(50, 20);
            layoutTitulo.Margin = new Padding(0);
            layoutTitulo.Name = "layoutTitulo";
            layoutTitulo.RowCount = 1;
            layoutTitulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulo.Size = new Size(417, 45);
            layoutTitulo.TabIndex = 14;
            // 
            // fieldTitulo
            // 
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 20.25F);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(3, 0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(361, 45);
            fieldTitulo.TabIndex = 0;
            fieldTitulo.Text = "Almacén";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldDireccion
            // 
            fieldDireccion.Animated = true;
            fieldDireccion.BorderColor = Color.Gainsboro;
            fieldDireccion.BorderRadius = 16;
            fieldDireccion.Cursor = Cursors.IBeam;
            fieldDireccion.CustomizableEdges = customizableEdges3;
            fieldDireccion.DefaultText = "";
            fieldDireccion.DisabledState.BorderColor = Color.White;
            fieldDireccion.DisabledState.ForeColor = Color.DimGray;
            fieldDireccion.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldDireccion.Dock = DockStyle.Fill;
            fieldDireccion.FocusedState.BorderColor = Color.SandyBrown;
            fieldDireccion.Font = new Font("Segoe UI", 11.25F);
            fieldDireccion.ForeColor = Color.Black;
            fieldDireccion.HoverState.BorderColor = Color.SandyBrown;
            fieldDireccion.IconLeft = (Image) resources.GetObject("fieldDireccion.IconLeft");
            fieldDireccion.IconLeftOffset = new Point(10, -11);
            fieldDireccion.Location = new Point(55, 190);
            fieldDireccion.Margin = new Padding(5);
            fieldDireccion.Multiline = true;
            fieldDireccion.Name = "fieldDireccion";
            fieldDireccion.PasswordChar = '\0';
            fieldDireccion.PlaceholderForeColor = Color.DimGray;
            fieldDireccion.PlaceholderText = "Dirección";
            fieldDireccion.SelectedText = "";
            fieldDireccion.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldDireccion.Size = new Size(407, 62);
            fieldDireccion.TabIndex = 22;
            fieldDireccion.TextOffset = new Point(5, 0);
            // 
            // fieldNotas
            // 
            fieldNotas.Animated = true;
            fieldNotas.BorderColor = Color.Gainsboro;
            fieldNotas.BorderRadius = 16;
            fieldNotas.Cursor = Cursors.IBeam;
            fieldNotas.CustomizableEdges = customizableEdges5;
            fieldNotas.DefaultText = "";
            fieldNotas.DisabledState.BorderColor = Color.White;
            fieldNotas.DisabledState.ForeColor = Color.DimGray;
            fieldNotas.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNotas.Dock = DockStyle.Fill;
            fieldNotas.FocusedState.BorderColor = Color.SandyBrown;
            fieldNotas.Font = new Font("Segoe UI", 11.25F);
            fieldNotas.ForeColor = Color.Black;
            fieldNotas.HoverState.BorderColor = Color.SandyBrown;
            fieldNotas.IconLeft = (Image) resources.GetObject("fieldNotas.IconLeft");
            fieldNotas.IconLeftOffset = new Point(10, -11);
            fieldNotas.Location = new Point(55, 327);
            fieldNotas.Margin = new Padding(5);
            fieldNotas.Multiline = true;
            fieldNotas.Name = "fieldNotas";
            fieldNotas.PasswordChar = '\0';
            fieldNotas.PlaceholderForeColor = Color.DimGray;
            fieldNotas.PlaceholderText = "Notas";
            fieldNotas.SelectedText = "";
            fieldNotas.ShadowDecoration.CustomizableEdges = customizableEdges6;
            fieldNotas.Size = new Size(407, 62);
            fieldNotas.TabIndex = 23;
            fieldNotas.TextOffset = new Point(5, 0);
            // 
            // layoutAutorizoVenta
            // 
            layoutAutorizoVenta.ColumnCount = 2;
            layoutAutorizoVenta.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 26F));
            layoutAutorizoVenta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutAutorizoVenta.Controls.Add(fieldTituloAutorizoVentaProductos, 1, 0);
            layoutAutorizoVenta.Controls.Add(fieldAutorizoVentaProductos, 0, 0);
            layoutAutorizoVenta.Dock = DockStyle.Fill;
            layoutAutorizoVenta.Location = new Point(65, 267);
            layoutAutorizoVenta.Margin = new Padding(15, 0, 0, 0);
            layoutAutorizoVenta.Name = "layoutAutorizoVenta";
            layoutAutorizoVenta.RowCount = 1;
            layoutAutorizoVenta.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutAutorizoVenta.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutAutorizoVenta.Size = new Size(402, 45);
            layoutAutorizoVenta.TabIndex = 24;
            // 
            // fieldTituloAutorizoVentaProductos
            // 
            fieldTituloAutorizoVentaProductos.Dock = DockStyle.Fill;
            fieldTituloAutorizoVentaProductos.Font = new Font("Segoe UI", 11.25F);
            fieldTituloAutorizoVentaProductos.ForeColor = Color.Black;
            fieldTituloAutorizoVentaProductos.ImeMode = ImeMode.NoControl;
            fieldTituloAutorizoVentaProductos.Location = new Point(31, 5);
            fieldTituloAutorizoVentaProductos.Margin = new Padding(5, 5, 1, 1);
            fieldTituloAutorizoVentaProductos.Name = "fieldTituloAutorizoVentaProductos";
            fieldTituloAutorizoVentaProductos.Size = new Size(370, 39);
            fieldTituloAutorizoVentaProductos.TabIndex = 1;
            fieldTituloAutorizoVentaProductos.Text = "Autorizar la venta de productos en el almacén actual.";
            // 
            // fieldAutorizoVentaProductos
            // 
            fieldAutorizoVentaProductos.BackColor = Color.White;
            fieldAutorizoVentaProductos.CheckedState.BorderColor = Color.Gainsboro;
            fieldAutorizoVentaProductos.CheckedState.BorderRadius = 4;
            fieldAutorizoVentaProductos.CheckedState.BorderThickness = 1;
            fieldAutorizoVentaProductos.CheckedState.FillColor = Color.WhiteSmoke;
            fieldAutorizoVentaProductos.CheckMarkColor = Color.Black;
            fieldAutorizoVentaProductos.Dock = DockStyle.Top;
            fieldAutorizoVentaProductos.Font = new Font("Segoe UI", 12F);
            fieldAutorizoVentaProductos.Location = new Point(5, 5);
            fieldAutorizoVentaProductos.Margin = new Padding(5, 5, 5, 15);
            fieldAutorizoVentaProductos.Name = "fieldAutorizoVentaProductos";
            fieldAutorizoVentaProductos.Size = new Size(16, 25);
            fieldAutorizoVentaProductos.TabIndex = 0;
            fieldAutorizoVentaProductos.UncheckedState.BorderColor = Color.Gainsboro;
            fieldAutorizoVentaProductos.UncheckedState.BorderRadius = 4;
            fieldAutorizoVentaProductos.UncheckedState.BorderThickness = 1;
            fieldAutorizoVentaProductos.UncheckedState.FillColor = Color.PeachPuff;
            fieldAutorizoVentaProductos.UseVisualStyleBackColor = false;
            // 
            // layoutBotones
            // 
            layoutBotones.BackColor = Color.White;
            layoutBotones.ColumnCount = 4;
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22F));
            layoutBotones.Controls.Add(btnSalir, 2, 0);
            layoutBotones.Controls.Add(btnRegistrar, 1, 0);
            layoutBotones.Dock = DockStyle.Fill;
            layoutBotones.Location = new Point(13, 620);
            layoutBotones.Margin = new Padding(3, 0, 0, 0);
            layoutBotones.Name = "layoutBotones";
            layoutBotones.RowCount = 2;
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutBotones.Size = new Size(487, 65);
            layoutBotones.TabIndex = 3;
            // 
            // btnSalir
            // 
            btnSalir.Animated = true;
            btnSalir.BorderColor = Color.Gainsboro;
            btnSalir.BorderRadius = 18;
            btnSalir.BorderThickness = 1;
            btnSalir.CustomizableEdges = customizableEdges7;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.FillColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Gainsboro;
            btnSalir.HoverState.BorderColor = Color.PeachPuff;
            btnSalir.HoverState.FillColor = Color.PeachPuff;
            btnSalir.HoverState.ForeColor = Color.Black;
            btnSalir.Location = new Point(302, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnSalir.Size = new Size(160, 39);
            btnSalir.TabIndex = 14;
            btnSalir.Text = "Salir";
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.BorderRadius = 18;
            btnRegistrar.CustomizableEdges = customizableEdges9;
            btnRegistrar.Dock = DockStyle.Fill;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Location = new Point(53, 3);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnRegistrar.Size = new Size(243, 39);
            btnRegistrar.TabIndex = 15;
            btnRegistrar.Text = "Registrar almacén";
            // 
            // VistaRegistroAlmacen
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroAlmacen";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroAlmacen";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutTitulo.ResumeLayout(false);
            layoutAutorizoVenta.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private Guna2TextBox fieldNombre;
        private TableLayoutPanel layoutTitulo;
        private Label fieldTitulo;
        private TableLayoutPanel layoutBotones;
        private Guna2Button btnSalir;
        private Guna2Button btnRegistrar;
        private Guna2TextBox fieldDireccion;
        private Guna2TextBox fieldNotas;
        private TableLayoutPanel layoutAutorizoVenta;
        private Label fieldTituloAutorizoVentaProductos;
        private Guna2CheckBox fieldAutorizoVentaProductos;
    }
}