namespace aDVanceERP.Modulos.Inventario.Vistas {
    partial class VistaRegistroUnidadMedida {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaRegistroUnidadMedida));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            layoutVista = new TableLayoutPanel();
            layoutTitulos1 = new TableLayoutPanel();
            fieldTituloDatosGenerales = new Label();
            layoutTitulo = new TableLayoutPanel();
            fieldTitulo = new Label();
            layoutDatos1 = new TableLayoutPanel();
            fieldAbreviatura = new Guna.UI2.WinForms.Guna2TextBox();
            fieldDescripcion = new Guna.UI2.WinForms.Guna2TextBox();
            fieldNombre = new Guna.UI2.WinForms.Guna2TextBox();
            fieldSubtitulo = new Label();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna.UI2.WinForms.Guna2Button();
            btnRegistrarActualizar = new Guna.UI2.WinForms.Guna2Button();
            fieldIcono = new PictureBox();
            layoutVista.SuspendLayout();
            layoutTitulos1.SuspendLayout();
            layoutTitulo.SuspendLayout();
            layoutDatos1.SuspendLayout();
            layoutBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) fieldIcono).BeginInit();
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
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 4;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(layoutTitulos1, 2, 2);
            layoutVista.Controls.Add(layoutTitulo, 2, 0);
            layoutVista.Controls.Add(layoutDatos1, 2, 3);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 1);
            layoutVista.Controls.Add(layoutBotones, 2, 5);
            layoutVista.Controls.Add(fieldIcono, 1, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 7;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(1356, 685);
            layoutVista.TabIndex = 5;
            // 
            // layoutTitulos1
            // 
            layoutTitulos1.ColumnCount = 1;
            layoutTitulos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulos1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutTitulos1.Controls.Add(fieldTituloDatosGenerales, 0, 0);
            layoutTitulos1.Dock = DockStyle.Fill;
            layoutTitulos1.Location = new Point(50, 90);
            layoutTitulos1.Margin = new Padding(0);
            layoutTitulos1.Name = "layoutTitulos1";
            layoutTitulos1.RowCount = 1;
            layoutTitulos1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulos1.Size = new Size(1286, 35);
            layoutTitulos1.TabIndex = 47;
            // 
            // fieldTituloDatosGenerales
            // 
            fieldTituloDatosGenerales.Dock = DockStyle.Fill;
            fieldTituloDatosGenerales.Font = new Font("Segoe UI", 11.25F);
            fieldTituloDatosGenerales.ForeColor = Color.DimGray;
            fieldTituloDatosGenerales.Image = (Image) resources.GetObject("fieldTituloDatosGenerales.Image");
            fieldTituloDatosGenerales.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloDatosGenerales.ImeMode = ImeMode.NoControl;
            fieldTituloDatosGenerales.Location = new Point(15, 5);
            fieldTituloDatosGenerales.Margin = new Padding(15, 5, 3, 3);
            fieldTituloDatosGenerales.Name = "fieldTituloDatosGenerales";
            fieldTituloDatosGenerales.Size = new Size(1268, 27);
            fieldTituloDatosGenerales.TabIndex = 41;
            fieldTituloDatosGenerales.Text = "      Datos generales :";
            fieldTituloDatosGenerales.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutTitulo
            // 
            layoutTitulo.ColumnCount = 2;
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutTitulo.Controls.Add(fieldTitulo, 0, 0);
            layoutTitulo.Dock = DockStyle.Fill;
            layoutTitulo.Location = new Point(50, 0);
            layoutTitulo.Margin = new Padding(0);
            layoutTitulo.Name = "layoutTitulo";
            layoutTitulo.RowCount = 1;
            layoutTitulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulo.Size = new Size(1286, 45);
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
            fieldTitulo.Size = new Size(1230, 45);
            fieldTitulo.TabIndex = 3;
            fieldTitulo.Text = "Unidad de medida del producto";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutDatos1
            // 
            layoutDatos1.ColumnCount = 3;
            layoutDatos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            layoutDatos1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutDatos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            layoutDatos1.Controls.Add(fieldAbreviatura, 1, 0);
            layoutDatos1.Controls.Add(fieldDescripcion, 2, 0);
            layoutDatos1.Controls.Add(fieldNombre, 0, 0);
            layoutDatos1.Dock = DockStyle.Fill;
            layoutDatos1.Location = new Point(50, 125);
            layoutDatos1.Margin = new Padding(0);
            layoutDatos1.Name = "layoutDatos1";
            layoutDatos1.RowCount = 1;
            layoutDatos1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDatos1.Size = new Size(1286, 45);
            layoutDatos1.TabIndex = 40;
            // 
            // fieldAbreviatura
            // 
            fieldAbreviatura.Animated = true;
            fieldAbreviatura.BorderColor = Color.Gainsboro;
            fieldAbreviatura.BorderRadius = 16;
            fieldAbreviatura.Cursor = Cursors.IBeam;
            fieldAbreviatura.CustomizableEdges = customizableEdges11;
            fieldAbreviatura.DefaultText = "";
            fieldAbreviatura.DisabledState.BorderColor = Color.White;
            fieldAbreviatura.DisabledState.ForeColor = Color.DimGray;
            fieldAbreviatura.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldAbreviatura.Dock = DockStyle.Fill;
            fieldAbreviatura.FocusedState.BorderColor = Color.SandyBrown;
            fieldAbreviatura.Font = new Font("Segoe UI", 11.25F);
            fieldAbreviatura.ForeColor = Color.Black;
            fieldAbreviatura.HoverState.BorderColor = Color.SandyBrown;
            fieldAbreviatura.IconLeft = (Image) resources.GetObject("fieldAbreviatura.IconLeft");
            fieldAbreviatura.IconLeftOffset = new Point(10, 0);
            fieldAbreviatura.Location = new Point(342, 5);
            fieldAbreviatura.Margin = new Padding(5);
            fieldAbreviatura.Name = "fieldAbreviatura";
            fieldAbreviatura.PasswordChar = '\0';
            fieldAbreviatura.PlaceholderForeColor = Color.DimGray;
            fieldAbreviatura.PlaceholderText = "Abreviatura";
            fieldAbreviatura.SelectedText = "";
            fieldAbreviatura.ShadowDecoration.CustomizableEdges = customizableEdges12;
            fieldAbreviatura.Size = new Size(150, 35);
            fieldAbreviatura.TabIndex = 10;
            fieldAbreviatura.TextOffset = new Point(5, 0);
            // 
            // fieldDescripcion
            // 
            fieldDescripcion.Animated = true;
            fieldDescripcion.BorderColor = Color.Gainsboro;
            fieldDescripcion.BorderRadius = 16;
            fieldDescripcion.Cursor = Cursors.IBeam;
            fieldDescripcion.CustomizableEdges = customizableEdges13;
            fieldDescripcion.DefaultText = "";
            fieldDescripcion.DisabledState.BorderColor = Color.White;
            fieldDescripcion.DisabledState.ForeColor = Color.DimGray;
            fieldDescripcion.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldDescripcion.Dock = DockStyle.Fill;
            fieldDescripcion.FocusedState.BorderColor = Color.SandyBrown;
            fieldDescripcion.Font = new Font("Segoe UI", 11.25F);
            fieldDescripcion.ForeColor = Color.Black;
            fieldDescripcion.HoverState.BorderColor = Color.SandyBrown;
            fieldDescripcion.IconLeft = (Image) resources.GetObject("fieldDescripcion.IconLeft");
            fieldDescripcion.IconLeftOffset = new Point(10, 0);
            fieldDescripcion.Location = new Point(502, 5);
            fieldDescripcion.Margin = new Padding(5);
            fieldDescripcion.Name = "fieldDescripcion";
            fieldDescripcion.PasswordChar = '\0';
            fieldDescripcion.PlaceholderForeColor = Color.DimGray;
            fieldDescripcion.PlaceholderText = "Descripción ";
            fieldDescripcion.SelectedText = "";
            fieldDescripcion.ShadowDecoration.CustomizableEdges = customizableEdges14;
            fieldDescripcion.Size = new Size(779, 35);
            fieldDescripcion.TabIndex = 9;
            fieldDescripcion.TextOffset = new Point(5, 0);
            // 
            // fieldNombre
            // 
            fieldNombre.Animated = true;
            fieldNombre.BorderColor = Color.Gainsboro;
            fieldNombre.BorderRadius = 16;
            fieldNombre.Cursor = Cursors.IBeam;
            fieldNombre.CustomizableEdges = customizableEdges15;
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
            fieldNombre.Location = new Point(5, 5);
            fieldNombre.Margin = new Padding(5);
            fieldNombre.Name = "fieldNombre";
            fieldNombre.PasswordChar = '\0';
            fieldNombre.PlaceholderForeColor = Color.DimGray;
            fieldNombre.PlaceholderText = "Nombre o identificador";
            fieldNombre.SelectedText = "";
            fieldNombre.ShadowDecoration.CustomizableEdges = customizableEdges16;
            fieldNombre.Size = new Size(327, 35);
            fieldNombre.TabIndex = 8;
            fieldNombre.TextOffset = new Point(5, 0);
            // 
            // fieldSubtitulo
            // 
            fieldSubtitulo.Dock = DockStyle.Fill;
            fieldSubtitulo.Font = new Font("Segoe UI", 11.25F);
            fieldSubtitulo.ForeColor = Color.Gray;
            fieldSubtitulo.ImeMode = ImeMode.NoControl;
            fieldSubtitulo.Location = new Point(55, 50);
            fieldSubtitulo.Margin = new Padding(5, 5, 1, 1);
            fieldSubtitulo.Name = "fieldSubtitulo";
            fieldSubtitulo.Size = new Size(1280, 39);
            fieldSubtitulo.TabIndex = 2;
            fieldSubtitulo.Text = "Registro";
            // 
            // layoutBotones
            // 
            layoutBotones.BackColor = Color.White;
            layoutBotones.ColumnCount = 3;
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBotones.Controls.Add(btnSalir, 2, 0);
            layoutBotones.Controls.Add(btnRegistrarActualizar, 1, 0);
            layoutBotones.Dock = DockStyle.Fill;
            layoutBotones.Location = new Point(53, 620);
            layoutBotones.Margin = new Padding(3, 0, 0, 0);
            layoutBotones.Name = "layoutBotones";
            layoutBotones.RowCount = 1;
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBotones.Size = new Size(1283, 45);
            layoutBotones.TabIndex = 45;
            // 
            // btnSalir
            // 
            btnSalir.Animated = true;
            btnSalir.BorderColor = Color.Gainsboro;
            btnSalir.BorderRadius = 18;
            btnSalir.BorderThickness = 1;
            btnSalir.CustomizableEdges = customizableEdges17;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.FillColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Gainsboro;
            btnSalir.HoverState.BorderColor = Color.PeachPuff;
            btnSalir.HoverState.FillColor = Color.PeachPuff;
            btnSalir.HoverState.ForeColor = Color.Black;
            btnSalir.Location = new Point(1116, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnSalir.Size = new Size(164, 39);
            btnSalir.TabIndex = 14;
            btnSalir.Text = "Salir";
            // 
            // btnRegistrarActualizar
            // 
            btnRegistrarActualizar.Animated = true;
            btnRegistrarActualizar.BorderRadius = 18;
            btnRegistrarActualizar.CustomizableEdges = customizableEdges19;
            btnRegistrarActualizar.Dock = DockStyle.Fill;
            btnRegistrarActualizar.FillColor = Color.PeachPuff;
            btnRegistrarActualizar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrarActualizar.ForeColor = Color.Black;
            btnRegistrarActualizar.Location = new Point(886, 3);
            btnRegistrarActualizar.Name = "btnRegistrarActualizar";
            btnRegistrarActualizar.ShadowDecoration.CustomizableEdges = customizableEdges20;
            btnRegistrarActualizar.Size = new Size(224, 39);
            btnRegistrarActualizar.TabIndex = 15;
            btnRegistrarActualizar.Text = "Registrar unidad de medida";
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = Properties.Resources.inventory_24px;
            fieldIcono.BackgroundImageLayout = ImageLayout.Center;
            fieldIcono.Dock = DockStyle.Fill;
            fieldIcono.Location = new Point(20, 6);
            fieldIcono.Margin = new Padding(0, 6, 0, 0);
            fieldIcono.Name = "fieldIcono";
            fieldIcono.Size = new Size(30, 39);
            fieldIcono.TabIndex = 53;
            fieldIcono.TabStop = false;
            // 
            // VistaRegistroUnidadMedida
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1356, 685);
            Controls.Add(layoutVista);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "VistaRegistroUnidadMedida";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "VistaGestionCostosProduccion";
            layoutVista.ResumeLayout(false);
            layoutTitulos1.ResumeLayout(false);
            layoutTitulo.ResumeLayout(false);
            layoutDatos1.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) fieldIcono).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutVista;
        private TableLayoutPanel layoutTitulo;
        private Label fieldTitulo;
        private Label fieldSubtitulo;
        private TableLayoutPanel layoutBotones;
        private Guna.UI2.WinForms.Guna2Button btnSalir;
        private Guna.UI2.WinForms.Guna2Button btnRegistrarActualizar;
        private Guna.UI2.WinForms.Guna2ComboBox fieldClasificacionProducto;
        private TableLayoutPanel layoutTitulos1;
        private Label fieldTituloDatosGenerales;
        private TableLayoutPanel layoutDatos1;
        private Guna.UI2.WinForms.Guna2TextBox fieldNombre;
        private PictureBox fieldIcono;
        private Guna.UI2.WinForms.Guna2TextBox fieldDescripcion;
        private Guna.UI2.WinForms.Guna2TextBox fieldAbreviatura;
    }
}