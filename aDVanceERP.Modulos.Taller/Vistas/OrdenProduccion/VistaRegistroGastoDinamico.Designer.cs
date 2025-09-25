using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Taller.Vistas.OrdenProduccion {
    partial class VistaRegistroGastoDinamico {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaRegistroGastoDinamico));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges31 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges32 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges33 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges34 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges35 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges36 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges37 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges38 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges39 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges40 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges41 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges42 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges43 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges44 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            layoutTitulo = new TableLayoutPanel();
            fieldTitulo = new Label();
            fieldEcuacion = new Guna2TextBox();
            fieldTituloConceptosDisponibles = new Label();
            fieldTituloOperaciones = new Label();
            _fieldConceptosDisponibles = new ListBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnDivision = new Guna2Button();
            btnMultiplicacion = new Guna2Button();
            btnResta = new Guna2Button();
            btnSuma = new Guna2Button();
            layoutSimbolosConstantes = new TableLayoutPanel();
            fieldConstante = new Guna2TextBox();
            btnParentesisDerecho = new Guna2Button();
            btnParentesisIzquierdo = new Guna2Button();
            btnInsertarConstante = new Guna2Button();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna2Button();
            btnRegistrar = new Guna2Button();
            separador1 = new Guna2Separator();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutTitulo.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            layoutSimbolosConstantes.SuspendLayout();
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
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldEcuacion, 2, 11);
            layoutVista.Controls.Add(fieldTituloConceptosDisponibles, 2, 4);
            layoutVista.Controls.Add(fieldTituloOperaciones, 2, 7);
            layoutVista.Controls.Add(_fieldConceptosDisponibles, 2, 5);
            layoutVista.Controls.Add(tableLayoutPanel1, 2, 8);
            layoutVista.Controls.Add(layoutSimbolosConstantes, 2, 9);
            layoutVista.Controls.Add(separador1, 2, 10);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(13, 0);
            layoutVista.Margin = new Padding(3, 0, 0, 0);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 14;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
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
            fieldSubtitulo.Text = "Diseño de ecuación e inserción";
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
            fieldTitulo.Text = "Gasto dinámico";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldEcuacion
            // 
            fieldEcuacion.Animated = true;
            fieldEcuacion.BorderColor = Color.Gainsboro;
            fieldEcuacion.BorderRadius = 16;
            fieldEcuacion.Cursor = Cursors.IBeam;
            fieldEcuacion.CustomizableEdges = customizableEdges23;
            fieldEcuacion.DefaultText = "";
            fieldEcuacion.DisabledState.BorderColor = Color.White;
            fieldEcuacion.DisabledState.ForeColor = Color.DimGray;
            fieldEcuacion.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldEcuacion.Dock = DockStyle.Fill;
            fieldEcuacion.FocusedState.BorderColor = Color.SandyBrown;
            fieldEcuacion.Font = new Font("Segoe UI", 11.25F);
            fieldEcuacion.ForeColor = Color.Black;
            fieldEcuacion.HoverState.BorderColor = Color.SandyBrown;
            fieldEcuacion.IconLeft = (Image) resources.GetObject("fieldEcuacion.IconLeft");
            fieldEcuacion.IconLeftOffset = new Point(10, -11);
            fieldEcuacion.Location = new Point(55, 415);
            fieldEcuacion.Margin = new Padding(5);
            fieldEcuacion.Multiline = true;
            fieldEcuacion.Name = "fieldEcuacion";
            fieldEcuacion.PasswordChar = '\0';
            fieldEcuacion.PlaceholderForeColor = Color.DimGray;
            fieldEcuacion.PlaceholderText = "Ecuación";
            fieldEcuacion.ReadOnly = true;
            fieldEcuacion.SelectedText = "";
            fieldEcuacion.ShadowDecoration.CustomizableEdges = customizableEdges24;
            fieldEcuacion.Size = new Size(407, 62);
            fieldEcuacion.TabIndex = 22;
            fieldEcuacion.TextOffset = new Point(5, 0);
            // 
            // fieldTituloConceptosDisponibles
            // 
            fieldTituloConceptosDisponibles.Dock = DockStyle.Fill;
            fieldTituloConceptosDisponibles.Font = new Font("Segoe UI", 11.25F);
            fieldTituloConceptosDisponibles.ForeColor = Color.DimGray;
            fieldTituloConceptosDisponibles.Image = (Image) resources.GetObject("fieldTituloConceptosDisponibles.Image");
            fieldTituloConceptosDisponibles.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloConceptosDisponibles.ImeMode = ImeMode.NoControl;
            fieldTituloConceptosDisponibles.Location = new Point(65, 135);
            fieldTituloConceptosDisponibles.Margin = new Padding(15, 5, 3, 3);
            fieldTituloConceptosDisponibles.Name = "fieldTituloConceptosDisponibles";
            fieldTituloConceptosDisponibles.Size = new Size(399, 27);
            fieldTituloConceptosDisponibles.TabIndex = 23;
            fieldTituloConceptosDisponibles.Text = "      Conceptos disponibles :";
            fieldTituloConceptosDisponibles.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloOperaciones
            // 
            fieldTituloOperaciones.Dock = DockStyle.Fill;
            fieldTituloOperaciones.Font = new Font("Segoe UI", 11.25F);
            fieldTituloOperaciones.ForeColor = Color.DimGray;
            fieldTituloOperaciones.Image = (Image) resources.GetObject("fieldTituloOperaciones.Image");
            fieldTituloOperaciones.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloOperaciones.ImeMode = ImeMode.NoControl;
            fieldTituloOperaciones.Location = new Point(65, 270);
            fieldTituloOperaciones.Margin = new Padding(15, 5, 3, 3);
            fieldTituloOperaciones.Name = "fieldTituloOperaciones";
            fieldTituloOperaciones.Size = new Size(399, 27);
            fieldTituloOperaciones.TabIndex = 24;
            fieldTituloOperaciones.Text = "      Operadores y constantes :";
            fieldTituloOperaciones.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _fieldConceptosDisponibles
            // 
            _fieldConceptosDisponibles.BackColor = Color.White;
            _fieldConceptosDisponibles.BorderStyle = BorderStyle.None;
            _fieldConceptosDisponibles.Dock = DockStyle.Fill;
            _fieldConceptosDisponibles.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point,  0);
            _fieldConceptosDisponibles.FormattingEnabled = true;
            _fieldConceptosDisponibles.ItemHeight = 20;
            _fieldConceptosDisponibles.Items.AddRange(new object[] { "Costo total en materiales", "Costo total en actividades" });
            _fieldConceptosDisponibles.Location = new Point(60, 170);
            _fieldConceptosDisponibles.Margin = new Padding(10, 5, 10, 5);
            _fieldConceptosDisponibles.Name = "_fieldConceptosDisponibles";
            _fieldConceptosDisponibles.Size = new Size(397, 80);
            _fieldConceptosDisponibles.TabIndex = 25;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(btnDivision, 3, 0);
            tableLayoutPanel1.Controls.Add(btnMultiplicacion, 2, 0);
            tableLayoutPanel1.Controls.Add(btnResta, 1, 0);
            tableLayoutPanel1.Controls.Add(btnSuma, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(50, 300);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(417, 45);
            tableLayoutPanel1.TabIndex = 26;
            // 
            // btnDivision
            // 
            btnDivision.Animated = true;
            btnDivision.AutoRoundedCorners = true;
            btnDivision.BorderColor = Color.Gainsboro;
            btnDivision.BorderRadius = 18;
            btnDivision.BorderThickness = 1;
            btnDivision.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage");
            btnDivision.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnDivision.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnDivision.CustomImages.ImageSize = new Size(24, 24);
            btnDivision.CustomizableEdges = customizableEdges25;
            btnDivision.Dock = DockStyle.Fill;
            btnDivision.FillColor = Color.White;
            btnDivision.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnDivision.ForeColor = Color.Gainsboro;
            btnDivision.HoverState.BorderColor = Color.PeachPuff;
            btnDivision.HoverState.FillColor = Color.PeachPuff;
            btnDivision.HoverState.ForeColor = Color.Black;
            btnDivision.ImageSize = new Size(24, 24);
            btnDivision.Location = new Point(315, 3);
            btnDivision.Name = "btnDivision";
            btnDivision.ShadowDecoration.CustomizableEdges = customizableEdges26;
            btnDivision.Size = new Size(99, 39);
            btnDivision.TabIndex = 18;
            // 
            // btnMultiplicacion
            // 
            btnMultiplicacion.Animated = true;
            btnMultiplicacion.AutoRoundedCorners = true;
            btnMultiplicacion.BorderColor = Color.Gainsboro;
            btnMultiplicacion.BorderRadius = 18;
            btnMultiplicacion.BorderThickness = 1;
            btnMultiplicacion.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage1");
            btnMultiplicacion.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnMultiplicacion.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnMultiplicacion.CustomImages.ImageSize = new Size(24, 24);
            btnMultiplicacion.CustomizableEdges = customizableEdges27;
            btnMultiplicacion.Dock = DockStyle.Fill;
            btnMultiplicacion.FillColor = Color.White;
            btnMultiplicacion.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnMultiplicacion.ForeColor = Color.Gainsboro;
            btnMultiplicacion.HoverState.BorderColor = Color.PeachPuff;
            btnMultiplicacion.HoverState.FillColor = Color.PeachPuff;
            btnMultiplicacion.HoverState.ForeColor = Color.Black;
            btnMultiplicacion.ImageSize = new Size(24, 24);
            btnMultiplicacion.Location = new Point(211, 3);
            btnMultiplicacion.Name = "btnMultiplicacion";
            btnMultiplicacion.ShadowDecoration.CustomizableEdges = customizableEdges28;
            btnMultiplicacion.Size = new Size(98, 39);
            btnMultiplicacion.TabIndex = 17;
            // 
            // btnResta
            // 
            btnResta.Animated = true;
            btnResta.AutoRoundedCorners = true;
            btnResta.BorderColor = Color.Gainsboro;
            btnResta.BorderRadius = 18;
            btnResta.BorderThickness = 1;
            btnResta.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage2");
            btnResta.CustomImages.Image = (Image) resources.GetObject("resource.Image2");
            btnResta.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnResta.CustomImages.ImageSize = new Size(24, 24);
            btnResta.CustomizableEdges = customizableEdges29;
            btnResta.Dock = DockStyle.Fill;
            btnResta.FillColor = Color.White;
            btnResta.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnResta.ForeColor = Color.Gainsboro;
            btnResta.HoverState.BorderColor = Color.PeachPuff;
            btnResta.HoverState.FillColor = Color.PeachPuff;
            btnResta.HoverState.ForeColor = Color.Black;
            btnResta.ImageSize = new Size(24, 24);
            btnResta.Location = new Point(107, 3);
            btnResta.Name = "btnResta";
            btnResta.ShadowDecoration.CustomizableEdges = customizableEdges30;
            btnResta.Size = new Size(98, 39);
            btnResta.TabIndex = 16;
            // 
            // btnSuma
            // 
            btnSuma.Animated = true;
            btnSuma.AutoRoundedCorners = true;
            btnSuma.BorderColor = Color.Gainsboro;
            btnSuma.BorderRadius = 18;
            btnSuma.BorderThickness = 1;
            btnSuma.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage3");
            btnSuma.CustomImages.Image = (Image) resources.GetObject("resource.Image3");
            btnSuma.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnSuma.CustomImages.ImageSize = new Size(24, 24);
            btnSuma.CustomizableEdges = customizableEdges31;
            btnSuma.Dock = DockStyle.Fill;
            btnSuma.FillColor = Color.White;
            btnSuma.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSuma.ForeColor = Color.Gainsboro;
            btnSuma.HoverState.BorderColor = Color.PeachPuff;
            btnSuma.HoverState.FillColor = Color.PeachPuff;
            btnSuma.HoverState.ForeColor = Color.Black;
            btnSuma.ImageSize = new Size(24, 24);
            btnSuma.Location = new Point(3, 3);
            btnSuma.Name = "btnSuma";
            btnSuma.ShadowDecoration.CustomizableEdges = customizableEdges32;
            btnSuma.Size = new Size(98, 39);
            btnSuma.TabIndex = 15;
            // 
            // layoutSimbolosConstantes
            // 
            layoutSimbolosConstantes.ColumnCount = 4;
            layoutSimbolosConstantes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            layoutSimbolosConstantes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            layoutSimbolosConstantes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            layoutSimbolosConstantes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            layoutSimbolosConstantes.Controls.Add(fieldConstante, 2, 0);
            layoutSimbolosConstantes.Controls.Add(btnParentesisDerecho, 1, 0);
            layoutSimbolosConstantes.Controls.Add(btnParentesisIzquierdo, 0, 0);
            layoutSimbolosConstantes.Controls.Add(btnInsertarConstante, 3, 0);
            layoutSimbolosConstantes.Dock = DockStyle.Fill;
            layoutSimbolosConstantes.Location = new Point(50, 345);
            layoutSimbolosConstantes.Margin = new Padding(0);
            layoutSimbolosConstantes.Name = "layoutSimbolosConstantes";
            layoutSimbolosConstantes.RowCount = 1;
            layoutSimbolosConstantes.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutSimbolosConstantes.Size = new Size(417, 45);
            layoutSimbolosConstantes.TabIndex = 27;
            // 
            // fieldConstante
            // 
            fieldConstante.Animated = true;
            fieldConstante.AutoRoundedCorners = true;
            fieldConstante.BorderColor = Color.Gainsboro;
            fieldConstante.BorderRadius = 18;
            fieldConstante.Cursor = Cursors.IBeam;
            customizableEdges33.BottomRight = false;
            customizableEdges33.TopRight = false;
            fieldConstante.CustomizableEdges = customizableEdges33;
            fieldConstante.DefaultText = "0";
            fieldConstante.DisabledState.BorderColor = Color.White;
            fieldConstante.DisabledState.ForeColor = Color.DimGray;
            fieldConstante.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldConstante.Dock = DockStyle.Fill;
            fieldConstante.FocusedState.BorderColor = Color.SandyBrown;
            fieldConstante.Font = new Font("Segoe UI", 11.25F);
            fieldConstante.ForeColor = Color.Black;
            fieldConstante.HoverState.BorderColor = Color.SandyBrown;
            fieldConstante.IconLeftOffset = new Point(10, 0);
            fieldConstante.Location = new Point(211, 3);
            fieldConstante.Margin = new Padding(3, 3, 0, 3);
            fieldConstante.Name = "fieldConstante";
            fieldConstante.PasswordChar = '\0';
            fieldConstante.PlaceholderForeColor = Color.DimGray;
            fieldConstante.PlaceholderText = "Nombre o identificador";
            fieldConstante.SelectedText = "";
            fieldConstante.ShadowDecoration.CustomizableEdges = customizableEdges34;
            fieldConstante.Size = new Size(101, 39);
            fieldConstante.TabIndex = 20;
            fieldConstante.TextAlign = HorizontalAlignment.Right;
            fieldConstante.TextOffset = new Point(5, 0);
            // 
            // btnParentesisDerecho
            // 
            btnParentesisDerecho.Animated = true;
            btnParentesisDerecho.AutoRoundedCorners = true;
            btnParentesisDerecho.BorderColor = Color.Gainsboro;
            btnParentesisDerecho.BorderRadius = 18;
            btnParentesisDerecho.BorderThickness = 1;
            btnParentesisDerecho.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnParentesisDerecho.CustomImages.ImageSize = new Size(24, 24);
            btnParentesisDerecho.CustomizableEdges = customizableEdges35;
            btnParentesisDerecho.Dock = DockStyle.Fill;
            btnParentesisDerecho.FillColor = Color.White;
            btnParentesisDerecho.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            btnParentesisDerecho.ForeColor = Color.Gainsboro;
            btnParentesisDerecho.HoverState.BorderColor = Color.PeachPuff;
            btnParentesisDerecho.HoverState.FillColor = Color.PeachPuff;
            btnParentesisDerecho.HoverState.ForeColor = Color.Black;
            btnParentesisDerecho.ImageSize = new Size(24, 24);
            btnParentesisDerecho.Location = new Point(107, 3);
            btnParentesisDerecho.Name = "btnParentesisDerecho";
            btnParentesisDerecho.ShadowDecoration.CustomizableEdges = customizableEdges36;
            btnParentesisDerecho.Size = new Size(98, 39);
            btnParentesisDerecho.TabIndex = 16;
            btnParentesisDerecho.Text = ")";
            btnParentesisDerecho.TextOffset = new Point(0, -2);
            // 
            // btnParentesisIzquierdo
            // 
            btnParentesisIzquierdo.Animated = true;
            btnParentesisIzquierdo.AutoRoundedCorners = true;
            btnParentesisIzquierdo.BorderColor = Color.Gainsboro;
            btnParentesisIzquierdo.BorderRadius = 18;
            btnParentesisIzquierdo.BorderThickness = 1;
            btnParentesisIzquierdo.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnParentesisIzquierdo.CustomImages.ImageSize = new Size(24, 24);
            btnParentesisIzquierdo.CustomizableEdges = customizableEdges37;
            btnParentesisIzquierdo.Dock = DockStyle.Fill;
            btnParentesisIzquierdo.FillColor = Color.White;
            btnParentesisIzquierdo.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            btnParentesisIzquierdo.ForeColor = Color.Gainsboro;
            btnParentesisIzquierdo.HoverState.BorderColor = Color.PeachPuff;
            btnParentesisIzquierdo.HoverState.FillColor = Color.PeachPuff;
            btnParentesisIzquierdo.HoverState.ForeColor = Color.Black;
            btnParentesisIzquierdo.ImageSize = new Size(24, 24);
            btnParentesisIzquierdo.Location = new Point(3, 3);
            btnParentesisIzquierdo.Name = "btnParentesisIzquierdo";
            btnParentesisIzquierdo.ShadowDecoration.CustomizableEdges = customizableEdges38;
            btnParentesisIzquierdo.Size = new Size(98, 39);
            btnParentesisIzquierdo.TabIndex = 15;
            btnParentesisIzquierdo.Text = "(";
            btnParentesisIzquierdo.TextOffset = new Point(0, -2);
            // 
            // btnInsertarConstante
            // 
            btnInsertarConstante.Animated = true;
            btnInsertarConstante.AutoRoundedCorners = true;
            btnInsertarConstante.BorderColor = Color.Gainsboro;
            btnInsertarConstante.BorderRadius = 18;
            btnInsertarConstante.BorderThickness = 1;
            btnInsertarConstante.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage4");
            btnInsertarConstante.CustomImages.Image = (Image) resources.GetObject("resource.Image4");
            btnInsertarConstante.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnInsertarConstante.CustomImages.ImageSize = new Size(24, 24);
            customizableEdges39.BottomLeft = false;
            customizableEdges39.TopLeft = false;
            btnInsertarConstante.CustomizableEdges = customizableEdges39;
            btnInsertarConstante.Dock = DockStyle.Fill;
            btnInsertarConstante.FillColor = Color.White;
            btnInsertarConstante.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnInsertarConstante.ForeColor = Color.Gainsboro;
            btnInsertarConstante.HoverState.BorderColor = Color.PeachPuff;
            btnInsertarConstante.HoverState.FillColor = Color.PeachPuff;
            btnInsertarConstante.HoverState.ForeColor = Color.Black;
            btnInsertarConstante.ImageSize = new Size(24, 24);
            btnInsertarConstante.Location = new Point(312, 3);
            btnInsertarConstante.Margin = new Padding(0, 3, 3, 3);
            btnInsertarConstante.Name = "btnInsertarConstante";
            btnInsertarConstante.ShadowDecoration.CustomizableEdges = customizableEdges40;
            btnInsertarConstante.Size = new Size(102, 39);
            btnInsertarConstante.TabIndex = 19;
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
            layoutBotones.TabIndex = 2;
            // 
            // btnSalir
            // 
            btnSalir.Animated = true;
            btnSalir.BorderColor = Color.Gainsboro;
            btnSalir.BorderRadius = 18;
            btnSalir.BorderThickness = 1;
            btnSalir.CustomizableEdges = customizableEdges41;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.FillColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Gainsboro;
            btnSalir.HoverState.BorderColor = Color.PeachPuff;
            btnSalir.HoverState.FillColor = Color.PeachPuff;
            btnSalir.HoverState.ForeColor = Color.Black;
            btnSalir.Location = new Point(302, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges42;
            btnSalir.Size = new Size(160, 39);
            btnSalir.TabIndex = 14;
            btnSalir.Text = "Salir";
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.BorderRadius = 18;
            btnRegistrar.CustomizableEdges = customizableEdges43;
            btnRegistrar.Dock = DockStyle.Fill;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Location = new Point(53, 3);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges44;
            btnRegistrar.Size = new Size(243, 39);
            btnRegistrar.TabIndex = 15;
            btnRegistrar.Text = "Insertar gasto";
            // 
            // separador1
            // 
            separador1.Dock = DockStyle.Fill;
            separador1.FillColor = Color.FromArgb(  208,   197,   188);
            separador1.Location = new Point(53, 393);
            separador1.Name = "separador1";
            separador1.Size = new Size(411, 14);
            separador1.TabIndex = 44;
            // 
            // VistaRegistroGastoDinamico
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroGastoDinamico";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroGastoIndirectoDinamico";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutTitulo.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            layoutSimbolosConstantes.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private TableLayoutPanel layoutTitulo;
        private Label fieldTitulo;
        private TableLayoutPanel layoutBotones;
        private Guna2Button btnSalir;
        private Guna2Button btnRegistrar;
        private Guna2TextBox fieldEcuacion;
        private Label fieldTituloConceptosDisponibles;
        private Label fieldTituloOperaciones;
        private ListBox _fieldConceptosDisponibles;
        private TableLayoutPanel tableLayoutPanel1;
        private Guna2Button btnResta;
        private Guna2Button btnSuma;
        private Guna2Button btnDivision;
        private Guna2Button btnMultiplicacion;
        private Guna2Button btnInsertarConstante;
        private Guna2TextBox fieldConstante;
        private TableLayoutPanel layoutSimbolosConstantes;
        private Guna2Button btnParentesisDerecho;
        private Guna2Button btnParentesisIzquierdo;
        private Guna2Separator separador1;
    }
}