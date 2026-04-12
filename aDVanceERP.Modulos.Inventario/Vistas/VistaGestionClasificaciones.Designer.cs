using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    partial class VistaGestionClasificaciones {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaGestionClasificaciones));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutVista = new TableLayoutPanel();
            layoutTitulo = new TableLayoutPanel();
            fieldTitulo = new Label();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            layoutFiltroHerramientas = new FlowLayoutPanel();
            fieldTituloFiltroBusqueda = new Label();
            fieldFiltroBusqueda = new Guna2ComboBox();
            fieldCriterioBusqueda = new Guna2TextBox();
            btnRegistrar = new Guna2Button();
            panelControlesTabla = new Guna2Panel();
            layoutControlesTabla = new TableLayoutPanel();
            btnPaginaAnterior = new Guna2Button();
            btnPrimeraPagina = new Guna2Button();
            btnPaginaSiguiente = new Guna2Button();
            btnUltimaPagina = new Guna2Button();
            btnSincronizarDatos = new Guna2Button();
            fieldPaginaActual = new Label();
            fieldPaginasTotales = new Label();
            layoutContenedorVistas = new TableLayoutPanel();
            contenedorVistas = new Panel();
            panelEncabezadosTabla = new Guna2Panel();
            layoutEncabezadosTabla = new TableLayoutPanel();
            fieldTituloAcciones = new Label();
            fieldTituloNombre = new Label();
            fieldTituloId = new Label();
            fieldTituloDescripcion = new Label();
            layoutVista.SuspendLayout();
            layoutTitulo.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutFiltroHerramientas.SuspendLayout();
            panelControlesTabla.SuspendLayout();
            layoutControlesTabla.SuspendLayout();
            layoutContenedorVistas.SuspendLayout();
            panelEncabezadosTabla.SuspendLayout();
            layoutEncabezadosTabla.SuspendLayout();
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
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldIcono, 1, 1);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 2);
            layoutVista.Controls.Add(layoutFiltroHerramientas, 2, 4);
            layoutVista.Controls.Add(panelControlesTabla, 2, 8);
            layoutVista.Controls.Add(layoutContenedorVistas, 2, 7);
            layoutVista.Controls.Add(panelEncabezadosTabla, 2, 6);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 10;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(1356, 608);
            layoutVista.TabIndex = 4;
            // 
            // layoutTitulo
            // 
            layoutTitulo.ColumnCount = 2;
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutTitulo.Controls.Add(fieldTitulo, 0, 0);
            layoutTitulo.Dock = DockStyle.Fill;
            layoutTitulo.Location = new Point(50, 10);
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
            fieldTitulo.Text = "Gestión de clasificaciones";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = Properties.Resources.inventory_24px;
            fieldIcono.BackgroundImageLayout = ImageLayout.Center;
            fieldIcono.Dock = DockStyle.Fill;
            fieldIcono.Location = new Point(20, 16);
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
            fieldSubtitulo.Location = new Point(55, 60);
            fieldSubtitulo.Margin = new Padding(5, 5, 1, 1);
            fieldSubtitulo.Name = "fieldSubtitulo";
            fieldSubtitulo.Size = new Size(1280, 29);
            fieldSubtitulo.TabIndex = 2;
            fieldSubtitulo.Text = "Registro, edición, eliminación, búsqueda de clasificaciones o tipos de productos.";
            // 
            // layoutFiltroHerramientas
            // 
            layoutFiltroHerramientas.Controls.Add(fieldTituloFiltroBusqueda);
            layoutFiltroHerramientas.Controls.Add(fieldFiltroBusqueda);
            layoutFiltroHerramientas.Controls.Add(fieldCriterioBusqueda);
            layoutFiltroHerramientas.Controls.Add(btnRegistrar);
            layoutFiltroHerramientas.Dock = DockStyle.Fill;
            layoutFiltroHerramientas.Location = new Point(50, 100);
            layoutFiltroHerramientas.Margin = new Padding(0);
            layoutFiltroHerramientas.Name = "layoutFiltroHerramientas";
            layoutFiltroHerramientas.Size = new Size(1286, 45);
            layoutFiltroHerramientas.TabIndex = 77;
            // 
            // fieldTituloFiltroBusqueda
            // 
            fieldTituloFiltroBusqueda.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloFiltroBusqueda.ForeColor = Color.DimGray;
            fieldTituloFiltroBusqueda.ImeMode = ImeMode.NoControl;
            fieldTituloFiltroBusqueda.Location = new Point(1, 1);
            fieldTituloFiltroBusqueda.Margin = new Padding(1);
            fieldTituloFiltroBusqueda.Name = "fieldTituloFiltroBusqueda";
            fieldTituloFiltroBusqueda.Size = new Size(96, 40);
            fieldTituloFiltroBusqueda.TabIndex = 15;
            fieldTituloFiltroBusqueda.Text = "FILTRAR POR :";
            fieldTituloFiltroBusqueda.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldFiltroBusqueda
            // 
            fieldFiltroBusqueda.Animated = true;
            fieldFiltroBusqueda.BackColor = Color.Transparent;
            fieldFiltroBusqueda.BorderColor = Color.Gainsboro;
            fieldFiltroBusqueda.BorderRadius = 16;
            fieldFiltroBusqueda.CustomizableEdges = customizableEdges1;
            fieldFiltroBusqueda.DrawMode = DrawMode.OwnerDrawFixed;
            fieldFiltroBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldFiltroBusqueda.FocusedColor = Color.Gainsboro;
            fieldFiltroBusqueda.FocusedState.BorderColor = Color.Gainsboro;
            fieldFiltroBusqueda.Font = new Font("Segoe UI", 11.25F);
            fieldFiltroBusqueda.ForeColor = Color.Black;
            fieldFiltroBusqueda.ItemHeight = 29;
            fieldFiltroBusqueda.Location = new Point(101, 5);
            fieldFiltroBusqueda.Margin = new Padding(3, 5, 3, 5);
            fieldFiltroBusqueda.Name = "fieldFiltroBusqueda";
            fieldFiltroBusqueda.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldFiltroBusqueda.Size = new Size(220, 35);
            fieldFiltroBusqueda.TabIndex = 27;
            fieldFiltroBusqueda.TextOffset = new Point(10, 0);
            // 
            // fieldCriterioBusqueda
            // 
            fieldCriterioBusqueda.Animated = true;
            fieldCriterioBusqueda.BackColor = Color.FromArgb(  254,   254,   253);
            fieldCriterioBusqueda.BorderColor = Color.Gainsboro;
            fieldCriterioBusqueda.BorderRadius = 18;
            fieldCriterioBusqueda.Cursor = Cursors.IBeam;
            fieldCriterioBusqueda.CustomizableEdges = customizableEdges3;
            fieldCriterioBusqueda.DefaultText = "";
            fieldCriterioBusqueda.DisabledState.BorderColor = Color.White;
            fieldCriterioBusqueda.DisabledState.ForeColor = Color.DimGray;
            fieldCriterioBusqueda.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldCriterioBusqueda.FocusedState.BorderColor = Color.SandyBrown;
            fieldCriterioBusqueda.Font = new Font("Segoe UI", 11.25F);
            fieldCriterioBusqueda.ForeColor = Color.Black;
            fieldCriterioBusqueda.HoverState.BorderColor = Color.SandyBrown;
            fieldCriterioBusqueda.IconLeft = (Image) resources.GetObject("fieldCriterioBusqueda.IconLeft");
            fieldCriterioBusqueda.IconLeftOffset = new Point(10, 1);
            fieldCriterioBusqueda.IconRightOffset = new Point(10, 0);
            fieldCriterioBusqueda.Location = new Point(327, 5);
            fieldCriterioBusqueda.Margin = new Padding(3, 5, 3, 5);
            fieldCriterioBusqueda.Name = "fieldCriterioBusqueda";
            fieldCriterioBusqueda.PasswordChar = '\0';
            fieldCriterioBusqueda.PlaceholderForeColor = Color.DimGray;
            fieldCriterioBusqueda.PlaceholderText = "Criterio de búsqueda";
            fieldCriterioBusqueda.SelectedText = "";
            fieldCriterioBusqueda.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldCriterioBusqueda.Size = new Size(220, 35);
            fieldCriterioBusqueda.TabIndex = 9;
            fieldCriterioBusqueda.TextOffset = new Point(5, 0);
            fieldCriterioBusqueda.Visible = false;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.AutoRoundedCorners = true;
            btnRegistrar.BackColor = Color.White;
            btnRegistrar.BorderRadius = 16;
            btnRegistrar.CustomizableEdges = customizableEdges5;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Image = (Image) resources.GetObject("btnRegistrar.Image");
            btnRegistrar.ImageOffset = new Point(-5, 0);
            btnRegistrar.Location = new Point(553, 5);
            btnRegistrar.Margin = new Padding(3, 5, 3, 5);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnRegistrar.Size = new Size(191, 35);
            btnRegistrar.TabIndex = 7;
            btnRegistrar.Text = "Nueva clasificación";
            // 
            // panelControlesTabla
            // 
            panelControlesTabla.BackColor = Color.Transparent;
            panelControlesTabla.BorderColor = Color.Gainsboro;
            panelControlesTabla.BorderRadius = 8;
            panelControlesTabla.BorderThickness = 1;
            panelControlesTabla.Controls.Add(layoutControlesTabla);
            customizableEdges17.TopLeft = false;
            customizableEdges17.TopRight = false;
            panelControlesTabla.CustomizableEdges = customizableEdges17;
            panelControlesTabla.Dock = DockStyle.Fill;
            panelControlesTabla.FillColor = Color.White;
            panelControlesTabla.Location = new Point(50, 546);
            panelControlesTabla.Margin = new Padding(0);
            panelControlesTabla.Name = "panelControlesTabla";
            panelControlesTabla.ShadowDecoration.BorderRadius = 8;
            panelControlesTabla.ShadowDecoration.CustomizableEdges = customizableEdges18;
            panelControlesTabla.ShadowDecoration.Depth = 10;
            panelControlesTabla.Size = new Size(1286, 42);
            panelControlesTabla.TabIndex = 78;
            // 
            // layoutControlesTabla
            // 
            layoutControlesTabla.BackColor = Color.Transparent;
            layoutControlesTabla.ColumnCount = 12;
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            layoutControlesTabla.Controls.Add(btnPaginaAnterior, 2, 0);
            layoutControlesTabla.Controls.Add(btnPrimeraPagina, 1, 0);
            layoutControlesTabla.Controls.Add(btnPaginaSiguiente, 7, 0);
            layoutControlesTabla.Controls.Add(btnUltimaPagina, 8, 0);
            layoutControlesTabla.Controls.Add(btnSincronizarDatos, 10, 0);
            layoutControlesTabla.Controls.Add(fieldPaginaActual, 4, 0);
            layoutControlesTabla.Controls.Add(fieldPaginasTotales, 5, 0);
            layoutControlesTabla.Dock = DockStyle.Fill;
            layoutControlesTabla.Location = new Point(0, 0);
            layoutControlesTabla.Margin = new Padding(0);
            layoutControlesTabla.Name = "layoutControlesTabla";
            layoutControlesTabla.RowCount = 1;
            layoutControlesTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutControlesTabla.Size = new Size(1286, 42);
            layoutControlesTabla.TabIndex = 17;
            // 
            // btnPaginaAnterior
            // 
            btnPaginaAnterior.Animated = true;
            btnPaginaAnterior.BackColor = Color.Transparent;
            btnPaginaAnterior.CheckedState.BorderColor = Color.WhiteSmoke;
            btnPaginaAnterior.CheckedState.FillColor = Color.WhiteSmoke;
            btnPaginaAnterior.Cursor = Cursors.Hand;
            btnPaginaAnterior.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnPaginaAnterior.CustomImages.ImageSize = new Size(24, 24);
            btnPaginaAnterior.CustomizableEdges = customizableEdges7;
            btnPaginaAnterior.DisabledState.FillColor = Color.White;
            btnPaginaAnterior.DisabledState.Image = Properties.Resources.page_previous_disabled_24px;
            btnPaginaAnterior.Dock = DockStyle.Fill;
            btnPaginaAnterior.FillColor = Color.White;
            btnPaginaAnterior.Font = new Font("Segoe UI", 9F);
            btnPaginaAnterior.ForeColor = Color.White;
            btnPaginaAnterior.HoverState.BorderColor = Color.FromArgb(  245,   245,   245);
            btnPaginaAnterior.HoverState.FillColor = Color.WhiteSmoke;
            btnPaginaAnterior.Image = Properties.Resources.page_previous_24px;
            btnPaginaAnterior.ImageSize = new Size(24, 24);
            btnPaginaAnterior.Location = new Point(46, 1);
            btnPaginaAnterior.Margin = new Padding(1);
            btnPaginaAnterior.Name = "btnPaginaAnterior";
            btnPaginaAnterior.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnPaginaAnterior.Size = new Size(33, 40);
            btnPaginaAnterior.TabIndex = 1;
            // 
            // btnPrimeraPagina
            // 
            btnPrimeraPagina.Animated = true;
            btnPrimeraPagina.BackColor = Color.Transparent;
            btnPrimeraPagina.CheckedState.BorderColor = Color.WhiteSmoke;
            btnPrimeraPagina.CheckedState.FillColor = Color.WhiteSmoke;
            btnPrimeraPagina.Cursor = Cursors.Hand;
            btnPrimeraPagina.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnPrimeraPagina.CustomImages.ImageSize = new Size(24, 24);
            btnPrimeraPagina.CustomizableEdges = customizableEdges9;
            btnPrimeraPagina.DisabledState.FillColor = Color.White;
            btnPrimeraPagina.DisabledState.Image = Properties.Resources.page_first_disabled_24px;
            btnPrimeraPagina.Dock = DockStyle.Fill;
            btnPrimeraPagina.FillColor = Color.White;
            btnPrimeraPagina.Font = new Font("Segoe UI", 9F);
            btnPrimeraPagina.ForeColor = Color.White;
            btnPrimeraPagina.HoverState.BorderColor = Color.FromArgb(  245,   245,   245);
            btnPrimeraPagina.HoverState.FillColor = Color.WhiteSmoke;
            btnPrimeraPagina.Image = Properties.Resources.page_first_24px;
            btnPrimeraPagina.ImageSize = new Size(24, 24);
            btnPrimeraPagina.Location = new Point(11, 1);
            btnPrimeraPagina.Margin = new Padding(1);
            btnPrimeraPagina.Name = "btnPrimeraPagina";
            btnPrimeraPagina.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnPrimeraPagina.Size = new Size(33, 40);
            btnPrimeraPagina.TabIndex = 0;
            // 
            // btnPaginaSiguiente
            // 
            btnPaginaSiguiente.Animated = true;
            btnPaginaSiguiente.BackColor = Color.Transparent;
            btnPaginaSiguiente.CheckedState.BorderColor = Color.WhiteSmoke;
            btnPaginaSiguiente.CheckedState.FillColor = Color.WhiteSmoke;
            btnPaginaSiguiente.Cursor = Cursors.Hand;
            btnPaginaSiguiente.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnPaginaSiguiente.CustomImages.ImageSize = new Size(24, 24);
            btnPaginaSiguiente.CustomizableEdges = customizableEdges11;
            btnPaginaSiguiente.DisabledState.FillColor = Color.White;
            btnPaginaSiguiente.DisabledState.Image = Properties.Resources.page_next_disabled_24px;
            btnPaginaSiguiente.Dock = DockStyle.Fill;
            btnPaginaSiguiente.FillColor = Color.White;
            btnPaginaSiguiente.Font = new Font("Segoe UI", 9F);
            btnPaginaSiguiente.ForeColor = Color.White;
            btnPaginaSiguiente.HoverState.BorderColor = Color.FromArgb(  245,   245,   245);
            btnPaginaSiguiente.HoverState.FillColor = Color.WhiteSmoke;
            btnPaginaSiguiente.Image = Properties.Resources.page_next_24px;
            btnPaginaSiguiente.ImageSize = new Size(24, 24);
            btnPaginaSiguiente.Location = new Point(321, 1);
            btnPaginaSiguiente.Margin = new Padding(1);
            btnPaginaSiguiente.Name = "btnPaginaSiguiente";
            btnPaginaSiguiente.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnPaginaSiguiente.Size = new Size(33, 40);
            btnPaginaSiguiente.TabIndex = 2;
            // 
            // btnUltimaPagina
            // 
            btnUltimaPagina.Animated = true;
            btnUltimaPagina.BackColor = Color.Transparent;
            btnUltimaPagina.CheckedState.BorderColor = Color.WhiteSmoke;
            btnUltimaPagina.CheckedState.FillColor = Color.WhiteSmoke;
            btnUltimaPagina.Cursor = Cursors.Hand;
            btnUltimaPagina.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnUltimaPagina.CustomImages.ImageSize = new Size(24, 24);
            btnUltimaPagina.CustomizableEdges = customizableEdges13;
            btnUltimaPagina.DisabledState.FillColor = Color.White;
            btnUltimaPagina.DisabledState.Image = Properties.Resources.page_last_disabled_24px;
            btnUltimaPagina.Dock = DockStyle.Fill;
            btnUltimaPagina.FillColor = Color.White;
            btnUltimaPagina.Font = new Font("Segoe UI", 9F);
            btnUltimaPagina.ForeColor = Color.White;
            btnUltimaPagina.HoverState.BorderColor = Color.FromArgb(  245,   245,   245);
            btnUltimaPagina.HoverState.FillColor = Color.WhiteSmoke;
            btnUltimaPagina.Image = Properties.Resources.page_last_24px;
            btnUltimaPagina.ImageSize = new Size(24, 24);
            btnUltimaPagina.Location = new Point(356, 1);
            btnUltimaPagina.Margin = new Padding(1);
            btnUltimaPagina.Name = "btnUltimaPagina";
            btnUltimaPagina.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnUltimaPagina.Size = new Size(33, 40);
            btnUltimaPagina.TabIndex = 3;
            // 
            // btnSincronizarDatos
            // 
            btnSincronizarDatos.Animated = true;
            btnSincronizarDatos.BackColor = Color.Transparent;
            btnSincronizarDatos.CheckedState.BorderColor = Color.WhiteSmoke;
            btnSincronizarDatos.CheckedState.FillColor = Color.WhiteSmoke;
            btnSincronizarDatos.Cursor = Cursors.Hand;
            btnSincronizarDatos.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnSincronizarDatos.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnSincronizarDatos.CustomImages.ImageSize = new Size(24, 24);
            btnSincronizarDatos.CustomizableEdges = customizableEdges15;
            btnSincronizarDatos.Dock = DockStyle.Fill;
            btnSincronizarDatos.FillColor = Color.White;
            btnSincronizarDatos.Font = new Font("Segoe UI", 9F);
            btnSincronizarDatos.ForeColor = Color.White;
            btnSincronizarDatos.HoverState.BorderColor = Color.FromArgb(  245,   245,   245);
            btnSincronizarDatos.HoverState.FillColor = Color.WhiteSmoke;
            btnSincronizarDatos.ImageSize = new Size(24, 24);
            btnSincronizarDatos.Location = new Point(1242, 1);
            btnSincronizarDatos.Margin = new Padding(1);
            btnSincronizarDatos.Name = "btnSincronizarDatos";
            btnSincronizarDatos.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnSincronizarDatos.Size = new Size(33, 40);
            btnSincronizarDatos.TabIndex = 4;
            // 
            // fieldPaginaActual
            // 
            fieldPaginaActual.Dock = DockStyle.Fill;
            fieldPaginaActual.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldPaginaActual.ForeColor = Color.DimGray;
            fieldPaginaActual.ImeMode = ImeMode.NoControl;
            fieldPaginaActual.Location = new Point(91, 1);
            fieldPaginaActual.Margin = new Padding(1, 1, 0, 1);
            fieldPaginaActual.Name = "fieldPaginaActual";
            fieldPaginaActual.Size = new Size(119, 40);
            fieldPaginaActual.TabIndex = 5;
            fieldPaginaActual.Text = "PÁGINA 1";
            fieldPaginaActual.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldPaginasTotales
            // 
            fieldPaginasTotales.Dock = DockStyle.Fill;
            fieldPaginasTotales.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldPaginasTotales.ForeColor = Color.DimGray;
            fieldPaginasTotales.ImeMode = ImeMode.NoControl;
            fieldPaginasTotales.Location = new Point(210, 1);
            fieldPaginasTotales.Margin = new Padding(0, 1, 1, 1);
            fieldPaginasTotales.Name = "fieldPaginasTotales";
            fieldPaginasTotales.Size = new Size(99, 40);
            fieldPaginasTotales.TabIndex = 6;
            fieldPaginasTotales.Text = "DE 1";
            fieldPaginasTotales.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutContenedorVistas
            // 
            layoutContenedorVistas.BackColor = Color.Gainsboro;
            layoutContenedorVistas.ColumnCount = 1;
            layoutContenedorVistas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutContenedorVistas.Controls.Add(contenedorVistas, 0, 0);
            layoutContenedorVistas.Dock = DockStyle.Fill;
            layoutContenedorVistas.Location = new Point(50, 197);
            layoutContenedorVistas.Margin = new Padding(0);
            layoutContenedorVistas.Name = "layoutContenedorVistas";
            layoutContenedorVistas.RowCount = 1;
            layoutContenedorVistas.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutContenedorVistas.Size = new Size(1286, 349);
            layoutContenedorVistas.TabIndex = 79;
            // 
            // contenedorVistas
            // 
            contenedorVistas.BackColor = Color.White;
            contenedorVistas.Dock = DockStyle.Fill;
            contenedorVistas.Location = new Point(1, 1);
            contenedorVistas.Margin = new Padding(1, 1, 1, 0);
            contenedorVistas.Name = "contenedorVistas";
            contenedorVistas.Size = new Size(1284, 348);
            contenedorVistas.TabIndex = 13;
            // 
            // panelEncabezadosTabla
            // 
            panelEncabezadosTabla.BackColor = Color.Transparent;
            panelEncabezadosTabla.BorderColor = Color.Gainsboro;
            panelEncabezadosTabla.BorderRadius = 8;
            panelEncabezadosTabla.BorderThickness = 1;
            panelEncabezadosTabla.Controls.Add(layoutEncabezadosTabla);
            panelEncabezadosTabla.CustomBorderThickness = new Padding(1, 1, 1, 3);
            customizableEdges19.BottomLeft = false;
            customizableEdges19.BottomRight = false;
            panelEncabezadosTabla.CustomizableEdges = customizableEdges19;
            panelEncabezadosTabla.Dock = DockStyle.Fill;
            panelEncabezadosTabla.FillColor = SystemColors.ButtonFace;
            panelEncabezadosTabla.Location = new Point(50, 155);
            panelEncabezadosTabla.Margin = new Padding(0);
            panelEncabezadosTabla.Name = "panelEncabezadosTabla";
            panelEncabezadosTabla.ShadowDecoration.BorderRadius = 8;
            panelEncabezadosTabla.ShadowDecoration.CustomizableEdges = customizableEdges20;
            panelEncabezadosTabla.ShadowDecoration.Depth = 10;
            panelEncabezadosTabla.Size = new Size(1286, 42);
            panelEncabezadosTabla.TabIndex = 80;
            // 
            // layoutEncabezadosTabla
            // 
            layoutEncabezadosTabla.BackColor = Color.Transparent;
            layoutEncabezadosTabla.ColumnCount = 4;
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 111F));
            layoutEncabezadosTabla.Controls.Add(fieldTituloAcciones, 3, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloNombre, 0, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloId, 0, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloDescripcion, 2, 0);
            layoutEncabezadosTabla.Dock = DockStyle.Fill;
            layoutEncabezadosTabla.Location = new Point(0, 0);
            layoutEncabezadosTabla.Margin = new Padding(1);
            layoutEncabezadosTabla.Name = "layoutEncabezadosTabla";
            layoutEncabezadosTabla.RowCount = 1;
            layoutEncabezadosTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.Size = new Size(1286, 42);
            layoutEncabezadosTabla.TabIndex = 11;
            // 
            // fieldTituloAcciones
            // 
            fieldTituloAcciones.Dock = DockStyle.Fill;
            fieldTituloAcciones.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloAcciones.ForeColor = Color.DimGray;
            fieldTituloAcciones.ImeMode = ImeMode.NoControl;
            fieldTituloAcciones.Location = new Point(1176, 1);
            fieldTituloAcciones.Margin = new Padding(1);
            fieldTituloAcciones.Name = "fieldTituloAcciones";
            fieldTituloAcciones.Size = new Size(109, 40);
            fieldTituloAcciones.TabIndex = 19;
            fieldTituloAcciones.Text = "ACCIONES";
            fieldTituloAcciones.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloNombre
            // 
            fieldTituloNombre.Dock = DockStyle.Fill;
            fieldTituloNombre.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloNombre.ForeColor = Color.DimGray;
            fieldTituloNombre.ImeMode = ImeMode.NoControl;
            fieldTituloNombre.Location = new Point(61, 1);
            fieldTituloNombre.Margin = new Padding(1);
            fieldTituloNombre.Name = "fieldTituloNombre";
            fieldTituloNombre.Size = new Size(248, 40);
            fieldTituloNombre.TabIndex = 15;
            fieldTituloNombre.Text = "NOMBRE";
            fieldTituloNombre.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloId
            // 
            fieldTituloId.Dock = DockStyle.Fill;
            fieldTituloId.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloId.ForeColor = Color.DimGray;
            fieldTituloId.ImeMode = ImeMode.NoControl;
            fieldTituloId.Location = new Point(1, 1);
            fieldTituloId.Margin = new Padding(1);
            fieldTituloId.Name = "fieldTituloId";
            fieldTituloId.Size = new Size(58, 40);
            fieldTituloId.TabIndex = 14;
            fieldTituloId.Text = "ID";
            fieldTituloId.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloDescripcion
            // 
            fieldTituloDescripcion.Dock = DockStyle.Fill;
            fieldTituloDescripcion.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloDescripcion.ForeColor = Color.DimGray;
            fieldTituloDescripcion.ImeMode = ImeMode.NoControl;
            fieldTituloDescripcion.Location = new Point(311, 1);
            fieldTituloDescripcion.Margin = new Padding(1);
            fieldTituloDescripcion.Name = "fieldTituloDescripcion";
            fieldTituloDescripcion.Size = new Size(863, 40);
            fieldTituloDescripcion.TabIndex = 16;
            fieldTituloDescripcion.Text = "DESCRIPCIÓN";
            fieldTituloDescripcion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaGestionClasificaciones
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1356, 608);
            Controls.Add(layoutVista);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaGestionClasificaciones";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaGestionAlmacenes";
            layoutVista.ResumeLayout(false);
            layoutTitulo.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutFiltroHerramientas.ResumeLayout(false);
            panelControlesTabla.ResumeLayout(false);
            layoutControlesTabla.ResumeLayout(false);
            layoutContenedorVistas.ResumeLayout(false);
            panelEncabezadosTabla.ResumeLayout(false);
            layoutEncabezadosTabla.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutVista;
        private TableLayoutPanel layoutTitulo;
        private Label fieldTitulo;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private TableLayoutPanel layoutEncabezadosTabla;
        private Label fieldTituloNombre;
        private Label fieldTituloId;
        private Label fieldTituloDescripcion;
        private FlowLayoutPanel layoutFiltroHerramientas;
        private Label fieldTituloFiltroBusqueda;
        private Guna2ComboBox fieldFiltroBusqueda;
        private Guna2TextBox fieldCriterioBusqueda;
        private Guna2Button btnRegistrar;
        private Guna2Panel panelControlesTabla;
        private TableLayoutPanel layoutControlesTabla;
        private Guna2Button btnPaginaAnterior;
        private Guna2Button btnPrimeraPagina;
        private Guna2Button btnPaginaSiguiente;
        private Guna2Button btnUltimaPagina;
        private Guna2Button btnSincronizarDatos;
        private Label fieldPaginaActual;
        private Label fieldPaginasTotales;
        private TableLayoutPanel layoutContenedorVistas;
        private Panel contenedorVistas;
        private Guna2Panel panelEncabezadosTabla;
        private Label fieldTituloAcciones;
    }
}