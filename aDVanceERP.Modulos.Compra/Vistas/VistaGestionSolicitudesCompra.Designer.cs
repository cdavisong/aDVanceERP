using System.ComponentModel;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Compra.Vistas {
    partial class VistaGestionSolicitudesCompra {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaGestionSolicitudesCompra));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            formatoBase = new Guna2BorderlessForm(components);
            layoutVista = new TableLayoutPanel();
            layoutHerramientas = new TableLayoutPanel();
            fieldDatoBusqueda = new Guna2TextBox();
            fieldFiltroBusqueda = new Guna2ComboBox();
            layoutTituloHerramientas = new TableLayoutPanel();
            fieldTituloFiltrosBusqueda = new Label();
            separador1 = new Guna2Separator();
            layoutTitulo = new TableLayoutPanel();
            fieldTitulo = new Label();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            contenedorVistas = new Panel();
            layoutControlesTabla = new TableLayoutPanel();
            btnPaginaAnterior = new Guna2Button();
            btnPrimeraPagina = new Guna2Button();
            btnPaginaSiguiente = new Guna2Button();
            btnUltimaPagina = new Guna2Button();
            btnSincronizarDatos = new Guna2Button();
            fieldPaginaActual = new Label();
            fieldPaginasTotales = new Label();
            panelBotonesGestion = new Panel();
            btnRegistrar = new Guna2Button();
            layoutEncabezadosTabla = new TableLayoutPanel();
            fieldTituloImporteTotal = new Label();
            fieldTituloObservaciones = new Label();
            fieldTituloFechaRequerida = new Label();
            fieldTituloEstado = new Label();
            fieldTituloFechaSolicitud = new Label();
            fieldTituloNombre = new Label();
            fieldTituloCodigo = new Label();
            layoutVista.SuspendLayout();
            layoutHerramientas.SuspendLayout();
            layoutTituloHerramientas.SuspendLayout();
            layoutTitulo.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutControlesTabla.SuspendLayout();
            panelBotonesGestion.SuspendLayout();
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
            layoutVista.Controls.Add(layoutHerramientas, 2, 5);
            layoutVista.Controls.Add(layoutTituloHerramientas, 2, 4);
            layoutVista.Controls.Add(separador1, 2, 6);
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldIcono, 1, 1);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 2);
            layoutVista.Controls.Add(contenedorVistas, 2, 11);
            layoutVista.Controls.Add(layoutControlesTabla, 2, 12);
            layoutVista.Controls.Add(panelBotonesGestion, 2, 7);
            layoutVista.Controls.Add(layoutEncabezadosTabla, 2, 9);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 14;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(1356, 608);
            layoutVista.TabIndex = 4;
            // 
            // layoutHerramientas
            // 
            layoutHerramientas.ColumnCount = 3;
            layoutHerramientas.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutHerramientas.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutHerramientas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutHerramientas.Controls.Add(fieldDatoBusqueda, 1, 0);
            layoutHerramientas.Controls.Add(fieldFiltroBusqueda, 0, 0);
            layoutHerramientas.Dock = DockStyle.Fill;
            layoutHerramientas.Location = new Point(50, 145);
            layoutHerramientas.Margin = new Padding(0);
            layoutHerramientas.Name = "layoutHerramientas";
            layoutHerramientas.RowCount = 1;
            layoutHerramientas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutHerramientas.Size = new Size(1286, 45);
            layoutHerramientas.TabIndex = 38;
            // 
            // fieldDatoBusqueda
            // 
            fieldDatoBusqueda.Animated = true;
            fieldDatoBusqueda.BackColor = Color.FromArgb(  254,   254,   253);
            fieldDatoBusqueda.BorderColor = Color.Gainsboro;
            fieldDatoBusqueda.BorderRadius = 18;
            fieldDatoBusqueda.Cursor = Cursors.IBeam;
            fieldDatoBusqueda.CustomizableEdges = customizableEdges1;
            fieldDatoBusqueda.DefaultText = "";
            fieldDatoBusqueda.DisabledState.BorderColor = Color.White;
            fieldDatoBusqueda.DisabledState.ForeColor = Color.DimGray;
            fieldDatoBusqueda.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldDatoBusqueda.Dock = DockStyle.Fill;
            fieldDatoBusqueda.FocusedState.BorderColor = Color.SandyBrown;
            fieldDatoBusqueda.Font = new Font("Segoe UI", 11.25F);
            fieldDatoBusqueda.ForeColor = Color.Black;
            fieldDatoBusqueda.HoverState.BorderColor = Color.SandyBrown;
            fieldDatoBusqueda.IconLeft = (Image) resources.GetObject("fieldDatoBusqueda.IconLeft");
            fieldDatoBusqueda.IconLeftOffset = new Point(10, 1);
            fieldDatoBusqueda.IconRightOffset = new Point(10, 0);
            fieldDatoBusqueda.Location = new Point(305, 5);
            fieldDatoBusqueda.Margin = new Padding(5);
            fieldDatoBusqueda.Name = "fieldDatoBusqueda";
            fieldDatoBusqueda.PasswordChar = '\0';
            fieldDatoBusqueda.PlaceholderForeColor = Color.DimGray;
            fieldDatoBusqueda.PlaceholderText = "Criterio de búsqueda";
            fieldDatoBusqueda.SelectedText = "";
            fieldDatoBusqueda.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldDatoBusqueda.Size = new Size(320, 35);
            fieldDatoBusqueda.TabIndex = 9;
            fieldDatoBusqueda.TextOffset = new Point(5, 0);
            fieldDatoBusqueda.Visible = false;
            // 
            // fieldFiltroBusqueda
            // 
            fieldFiltroBusqueda.Animated = true;
            fieldFiltroBusqueda.BackColor = Color.Transparent;
            fieldFiltroBusqueda.BorderColor = Color.Gainsboro;
            fieldFiltroBusqueda.BorderRadius = 16;
            fieldFiltroBusqueda.CustomizableEdges = customizableEdges3;
            fieldFiltroBusqueda.Dock = DockStyle.Fill;
            fieldFiltroBusqueda.DrawMode = DrawMode.OwnerDrawFixed;
            fieldFiltroBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldFiltroBusqueda.FocusedColor = Color.Gainsboro;
            fieldFiltroBusqueda.FocusedState.BorderColor = Color.Gainsboro;
            fieldFiltroBusqueda.Font = new Font("Segoe UI", 11.25F);
            fieldFiltroBusqueda.ForeColor = Color.Black;
            fieldFiltroBusqueda.ItemHeight = 29;
            fieldFiltroBusqueda.Location = new Point(5, 5);
            fieldFiltroBusqueda.Margin = new Padding(5);
            fieldFiltroBusqueda.Name = "fieldFiltroBusqueda";
            fieldFiltroBusqueda.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldFiltroBusqueda.Size = new Size(290, 35);
            fieldFiltroBusqueda.TabIndex = 27;
            fieldFiltroBusqueda.TextOffset = new Point(10, 0);
            // 
            // layoutTituloHerramientas
            // 
            layoutTituloHerramientas.ColumnCount = 3;
            layoutTituloHerramientas.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutTituloHerramientas.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutTituloHerramientas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTituloHerramientas.Controls.Add(fieldTituloFiltrosBusqueda, 0, 0);
            layoutTituloHerramientas.Dock = DockStyle.Fill;
            layoutTituloHerramientas.Location = new Point(50, 110);
            layoutTituloHerramientas.Margin = new Padding(0);
            layoutTituloHerramientas.Name = "layoutTituloHerramientas";
            layoutTituloHerramientas.RowCount = 1;
            layoutTituloHerramientas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTituloHerramientas.Size = new Size(1286, 35);
            layoutTituloHerramientas.TabIndex = 37;
            // 
            // fieldTituloFiltrosBusqueda
            // 
            fieldTituloFiltrosBusqueda.Dock = DockStyle.Fill;
            fieldTituloFiltrosBusqueda.Font = new Font("Segoe UI", 11.25F);
            fieldTituloFiltrosBusqueda.ForeColor = Color.DimGray;
            fieldTituloFiltrosBusqueda.Image = (Image) resources.GetObject("fieldTituloFiltrosBusqueda.Image");
            fieldTituloFiltrosBusqueda.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloFiltrosBusqueda.ImeMode = ImeMode.NoControl;
            fieldTituloFiltrosBusqueda.Location = new Point(15, 5);
            fieldTituloFiltrosBusqueda.Margin = new Padding(15, 5, 3, 3);
            fieldTituloFiltrosBusqueda.Name = "fieldTituloFiltrosBusqueda";
            fieldTituloFiltrosBusqueda.Size = new Size(282, 27);
            fieldTituloFiltrosBusqueda.TabIndex = 24;
            fieldTituloFiltrosBusqueda.Text = "      Filtro de búsqueda :";
            fieldTituloFiltrosBusqueda.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // separador1
            // 
            separador1.Dock = DockStyle.Fill;
            separador1.FillColor = Color.Gainsboro;
            separador1.Location = new Point(53, 193);
            separador1.Name = "separador1";
            separador1.Size = new Size(1280, 14);
            separador1.TabIndex = 35;
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
            fieldTitulo.Text = "Gestión de solicitudes de compra";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = Properties.Resources.buyingB_24px;
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
            fieldSubtitulo.Text = "Registro, edición, eliminación, búsqueda de solicitudes de compra.";
            // 
            // contenedorVistas
            // 
            contenedorVistas.Dock = DockStyle.Fill;
            contenedorVistas.Location = new Point(50, 335);
            contenedorVistas.Margin = new Padding(0);
            contenedorVistas.Name = "contenedorVistas";
            contenedorVistas.Size = new Size(1286, 218);
            contenedorVistas.TabIndex = 13;
            // 
            // layoutControlesTabla
            // 
            layoutControlesTabla.BackColor = Color.WhiteSmoke;
            layoutControlesTabla.ColumnCount = 13;
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutControlesTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutControlesTabla.Controls.Add(btnPaginaAnterior, 1, 0);
            layoutControlesTabla.Controls.Add(btnPrimeraPagina, 0, 0);
            layoutControlesTabla.Controls.Add(btnPaginaSiguiente, 6, 0);
            layoutControlesTabla.Controls.Add(btnUltimaPagina, 7, 0);
            layoutControlesTabla.Controls.Add(btnSincronizarDatos, 9, 0);
            layoutControlesTabla.Controls.Add(fieldPaginaActual, 3, 0);
            layoutControlesTabla.Controls.Add(fieldPaginasTotales, 4, 0);
            layoutControlesTabla.Dock = DockStyle.Fill;
            layoutControlesTabla.Location = new Point(50, 553);
            layoutControlesTabla.Margin = new Padding(0);
            layoutControlesTabla.Name = "layoutControlesTabla";
            layoutControlesTabla.RowCount = 1;
            layoutControlesTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutControlesTabla.Size = new Size(1286, 35);
            layoutControlesTabla.TabIndex = 17;
            // 
            // btnPaginaAnterior
            // 
            btnPaginaAnterior.Animated = true;
            btnPaginaAnterior.BackColor = Color.WhiteSmoke;
            btnPaginaAnterior.CheckedState.BorderColor = Color.WhiteSmoke;
            btnPaginaAnterior.CheckedState.FillColor = Color.WhiteSmoke;
            btnPaginaAnterior.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnPaginaAnterior.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnPaginaAnterior.CustomImages.ImageSize = new Size(24, 24);
            btnPaginaAnterior.CustomizableEdges = customizableEdges5;
            btnPaginaAnterior.Dock = DockStyle.Fill;
            btnPaginaAnterior.FillColor = Color.WhiteSmoke;
            btnPaginaAnterior.Font = new Font("Segoe UI", 9F);
            btnPaginaAnterior.ForeColor = Color.White;
            btnPaginaAnterior.HoverState.BorderColor = Color.WhiteSmoke;
            btnPaginaAnterior.HoverState.FillColor = Color.White;
            btnPaginaAnterior.ImageSize = new Size(24, 24);
            btnPaginaAnterior.Location = new Point(36, 1);
            btnPaginaAnterior.Margin = new Padding(1);
            btnPaginaAnterior.Name = "btnPaginaAnterior";
            btnPaginaAnterior.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnPaginaAnterior.Size = new Size(33, 33);
            btnPaginaAnterior.TabIndex = 1;
            // 
            // btnPrimeraPagina
            // 
            btnPrimeraPagina.Animated = true;
            btnPrimeraPagina.BackColor = Color.WhiteSmoke;
            btnPrimeraPagina.CheckedState.BorderColor = Color.WhiteSmoke;
            btnPrimeraPagina.CheckedState.FillColor = Color.WhiteSmoke;
            btnPrimeraPagina.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnPrimeraPagina.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnPrimeraPagina.CustomImages.ImageSize = new Size(24, 24);
            btnPrimeraPagina.CustomizableEdges = customizableEdges7;
            btnPrimeraPagina.Dock = DockStyle.Fill;
            btnPrimeraPagina.FillColor = Color.WhiteSmoke;
            btnPrimeraPagina.Font = new Font("Segoe UI", 9F);
            btnPrimeraPagina.ForeColor = Color.White;
            btnPrimeraPagina.HoverState.BorderColor = Color.WhiteSmoke;
            btnPrimeraPagina.HoverState.FillColor = Color.WhiteSmoke;
            btnPrimeraPagina.ImageSize = new Size(24, 24);
            btnPrimeraPagina.Location = new Point(1, 1);
            btnPrimeraPagina.Margin = new Padding(1);
            btnPrimeraPagina.Name = "btnPrimeraPagina";
            btnPrimeraPagina.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnPrimeraPagina.Size = new Size(33, 33);
            btnPrimeraPagina.TabIndex = 0;
            // 
            // btnPaginaSiguiente
            // 
            btnPaginaSiguiente.Animated = true;
            btnPaginaSiguiente.BackColor = Color.WhiteSmoke;
            btnPaginaSiguiente.CheckedState.BorderColor = Color.WhiteSmoke;
            btnPaginaSiguiente.CheckedState.FillColor = Color.WhiteSmoke;
            btnPaginaSiguiente.CustomImages.Image = (Image) resources.GetObject("resource.Image2");
            btnPaginaSiguiente.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnPaginaSiguiente.CustomImages.ImageSize = new Size(24, 24);
            btnPaginaSiguiente.CustomizableEdges = customizableEdges9;
            btnPaginaSiguiente.Dock = DockStyle.Fill;
            btnPaginaSiguiente.FillColor = Color.WhiteSmoke;
            btnPaginaSiguiente.Font = new Font("Segoe UI", 9F);
            btnPaginaSiguiente.ForeColor = Color.White;
            btnPaginaSiguiente.HoverState.BorderColor = Color.WhiteSmoke;
            btnPaginaSiguiente.HoverState.FillColor = Color.WhiteSmoke;
            btnPaginaSiguiente.ImageSize = new Size(24, 24);
            btnPaginaSiguiente.Location = new Point(311, 1);
            btnPaginaSiguiente.Margin = new Padding(1);
            btnPaginaSiguiente.Name = "btnPaginaSiguiente";
            btnPaginaSiguiente.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnPaginaSiguiente.Size = new Size(33, 33);
            btnPaginaSiguiente.TabIndex = 2;
            // 
            // btnUltimaPagina
            // 
            btnUltimaPagina.Animated = true;
            btnUltimaPagina.BackColor = Color.WhiteSmoke;
            btnUltimaPagina.CheckedState.BorderColor = Color.WhiteSmoke;
            btnUltimaPagina.CheckedState.FillColor = Color.WhiteSmoke;
            btnUltimaPagina.CustomImages.Image = (Image) resources.GetObject("resource.Image3");
            btnUltimaPagina.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnUltimaPagina.CustomImages.ImageSize = new Size(24, 24);
            btnUltimaPagina.CustomizableEdges = customizableEdges11;
            btnUltimaPagina.Dock = DockStyle.Fill;
            btnUltimaPagina.FillColor = Color.WhiteSmoke;
            btnUltimaPagina.Font = new Font("Segoe UI", 9F);
            btnUltimaPagina.ForeColor = Color.White;
            btnUltimaPagina.HoverState.BorderColor = Color.WhiteSmoke;
            btnUltimaPagina.HoverState.FillColor = Color.WhiteSmoke;
            btnUltimaPagina.ImageSize = new Size(24, 24);
            btnUltimaPagina.Location = new Point(346, 1);
            btnUltimaPagina.Margin = new Padding(1);
            btnUltimaPagina.Name = "btnUltimaPagina";
            btnUltimaPagina.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnUltimaPagina.Size = new Size(33, 33);
            btnUltimaPagina.TabIndex = 3;
            // 
            // btnSincronizarDatos
            // 
            btnSincronizarDatos.Animated = true;
            btnSincronizarDatos.BackColor = Color.WhiteSmoke;
            btnSincronizarDatos.CheckedState.BorderColor = Color.WhiteSmoke;
            btnSincronizarDatos.CheckedState.FillColor = Color.WhiteSmoke;
            btnSincronizarDatos.CustomImages.Image = (Image) resources.GetObject("resource.Image4");
            btnSincronizarDatos.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnSincronizarDatos.CustomImages.ImageSize = new Size(24, 24);
            btnSincronizarDatos.CustomizableEdges = customizableEdges13;
            btnSincronizarDatos.Dock = DockStyle.Fill;
            btnSincronizarDatos.FillColor = Color.WhiteSmoke;
            btnSincronizarDatos.Font = new Font("Segoe UI", 9F);
            btnSincronizarDatos.ForeColor = Color.White;
            btnSincronizarDatos.HoverState.BorderColor = Color.WhiteSmoke;
            btnSincronizarDatos.HoverState.FillColor = Color.WhiteSmoke;
            btnSincronizarDatos.ImageSize = new Size(24, 24);
            btnSincronizarDatos.Location = new Point(391, 1);
            btnSincronizarDatos.Margin = new Padding(1);
            btnSincronizarDatos.Name = "btnSincronizarDatos";
            btnSincronizarDatos.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnSincronizarDatos.Size = new Size(33, 33);
            btnSincronizarDatos.TabIndex = 4;
            // 
            // fieldPaginaActual
            // 
            fieldPaginaActual.Dock = DockStyle.Fill;
            fieldPaginaActual.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldPaginaActual.ForeColor = Color.Black;
            fieldPaginaActual.ImeMode = ImeMode.NoControl;
            fieldPaginaActual.Location = new Point(81, 1);
            fieldPaginaActual.Margin = new Padding(1, 1, 0, 1);
            fieldPaginaActual.Name = "fieldPaginaActual";
            fieldPaginaActual.Size = new Size(119, 33);
            fieldPaginaActual.TabIndex = 5;
            fieldPaginaActual.Text = "Página 1";
            fieldPaginaActual.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldPaginasTotales
            // 
            fieldPaginasTotales.Dock = DockStyle.Fill;
            fieldPaginasTotales.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldPaginasTotales.ForeColor = Color.Black;
            fieldPaginasTotales.ImeMode = ImeMode.NoControl;
            fieldPaginasTotales.Location = new Point(200, 1);
            fieldPaginasTotales.Margin = new Padding(0, 1, 1, 1);
            fieldPaginasTotales.Name = "fieldPaginasTotales";
            fieldPaginasTotales.Size = new Size(99, 33);
            fieldPaginasTotales.TabIndex = 6;
            fieldPaginasTotales.Text = "de 1";
            fieldPaginasTotales.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelBotonesGestion
            // 
            panelBotonesGestion.Controls.Add(btnRegistrar);
            panelBotonesGestion.Dock = DockStyle.Fill;
            panelBotonesGestion.Location = new Point(50, 210);
            panelBotonesGestion.Margin = new Padding(0);
            panelBotonesGestion.Name = "panelBotonesGestion";
            panelBotonesGestion.Padding = new Padding(3);
            panelBotonesGestion.Size = new Size(1286, 45);
            panelBotonesGestion.TabIndex = 18;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.BackColor = Color.White;
            btnRegistrar.BorderRadius = 18;
            btnRegistrar.CustomizableEdges = customizableEdges15;
            btnRegistrar.Dock = DockStyle.Left;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Image = (Image) resources.GetObject("btnRegistrar.Image");
            btnRegistrar.ImageOffset = new Point(-5, 0);
            btnRegistrar.Location = new Point(3, 3);
            btnRegistrar.Margin = new Padding(0);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnRegistrar.Size = new Size(320, 39);
            btnRegistrar.TabIndex = 7;
            btnRegistrar.Text = "Registrar nueva solicitud de compra";
            // 
            // layoutEncabezadosTabla
            // 
            layoutEncabezadosTabla.BackColor = Color.WhiteSmoke;
            layoutEncabezadosTabla.ColumnCount = 10;
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutEncabezadosTabla.Controls.Add(fieldTituloImporteTotal, 5, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloObservaciones, 4, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloFechaRequerida, 3, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloEstado, 6, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloFechaSolicitud, 2, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloNombre, 1, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloCodigo, 0, 0);
            layoutEncabezadosTabla.Dock = DockStyle.Fill;
            layoutEncabezadosTabla.Location = new Point(51, 266);
            layoutEncabezadosTabla.Margin = new Padding(1);
            layoutEncabezadosTabla.Name = "layoutEncabezadosTabla";
            layoutEncabezadosTabla.RowCount = 1;
            layoutEncabezadosTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.Size = new Size(1284, 58);
            layoutEncabezadosTabla.TabIndex = 40;
            // 
            // fieldTituloImporteTotal
            // 
            fieldTituloImporteTotal.Dock = DockStyle.Fill;
            fieldTituloImporteTotal.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloImporteTotal.ForeColor = Color.Black;
            fieldTituloImporteTotal.ImeMode = ImeMode.NoControl;
            fieldTituloImporteTotal.Location = new Point(844, 1);
            fieldTituloImporteTotal.Margin = new Padding(1);
            fieldTituloImporteTotal.Name = "fieldTituloImporteTotal";
            fieldTituloImporteTotal.Size = new Size(128, 56);
            fieldTituloImporteTotal.TabIndex = 24;
            fieldTituloImporteTotal.Text = "Importe total";
            fieldTituloImporteTotal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloObservaciones
            // 
            fieldTituloObservaciones.Dock = DockStyle.Fill;
            fieldTituloObservaciones.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloObservaciones.ForeColor = Color.Black;
            fieldTituloObservaciones.ImeMode = ImeMode.NoControl;
            fieldTituloObservaciones.Location = new Point(600, 1);
            fieldTituloObservaciones.Margin = new Padding(1);
            fieldTituloObservaciones.Name = "fieldTituloObservaciones";
            fieldTituloObservaciones.Size = new Size(242, 56);
            fieldTituloObservaciones.TabIndex = 23;
            fieldTituloObservaciones.Text = "Observaciones";
            fieldTituloObservaciones.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloFechaRequerida
            // 
            fieldTituloFechaRequerida.Dock = DockStyle.Fill;
            fieldTituloFechaRequerida.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloFechaRequerida.ForeColor = Color.Black;
            fieldTituloFechaRequerida.ImeMode = ImeMode.NoControl;
            fieldTituloFechaRequerida.Location = new Point(480, 1);
            fieldTituloFechaRequerida.Margin = new Padding(1);
            fieldTituloFechaRequerida.Name = "fieldTituloFechaRequerida";
            fieldTituloFechaRequerida.Size = new Size(118, 56);
            fieldTituloFechaRequerida.TabIndex = 22;
            fieldTituloFechaRequerida.Text = "Fecha requerida";
            fieldTituloFechaRequerida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloEstado
            // 
            fieldTituloEstado.Dock = DockStyle.Fill;
            fieldTituloEstado.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloEstado.ForeColor = Color.Black;
            fieldTituloEstado.ImeMode = ImeMode.NoControl;
            fieldTituloEstado.Location = new Point(974, 1);
            fieldTituloEstado.Margin = new Padding(1);
            fieldTituloEstado.Name = "fieldTituloEstado";
            fieldTituloEstado.Size = new Size(188, 56);
            fieldTituloEstado.TabIndex = 21;
            fieldTituloEstado.Text = "Estado";
            fieldTituloEstado.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloFechaSolicitud
            // 
            fieldTituloFechaSolicitud.Dock = DockStyle.Fill;
            fieldTituloFechaSolicitud.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloFechaSolicitud.ForeColor = Color.Black;
            fieldTituloFechaSolicitud.ImeMode = ImeMode.NoControl;
            fieldTituloFechaSolicitud.Location = new Point(360, 1);
            fieldTituloFechaSolicitud.Margin = new Padding(1);
            fieldTituloFechaSolicitud.Name = "fieldTituloFechaSolicitud";
            fieldTituloFechaSolicitud.Size = new Size(118, 56);
            fieldTituloFechaSolicitud.TabIndex = 20;
            fieldTituloFechaSolicitud.Text = "Fecha de solicitud";
            fieldTituloFechaSolicitud.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloNombre
            // 
            fieldTituloNombre.Dock = DockStyle.Fill;
            fieldTituloNombre.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloNombre.ForeColor = Color.Black;
            fieldTituloNombre.ImeMode = ImeMode.NoControl;
            fieldTituloNombre.Location = new Point(161, 1);
            fieldTituloNombre.Margin = new Padding(1);
            fieldTituloNombre.Name = "fieldTituloNombre";
            fieldTituloNombre.Size = new Size(197, 56);
            fieldTituloNombre.TabIndex = 18;
            fieldTituloNombre.Text = "Nombre del solicitante";
            fieldTituloNombre.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloCodigo
            // 
            fieldTituloCodigo.Dock = DockStyle.Fill;
            fieldTituloCodigo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloCodigo.ForeColor = Color.Black;
            fieldTituloCodigo.ImeMode = ImeMode.NoControl;
            fieldTituloCodigo.Location = new Point(1, 1);
            fieldTituloCodigo.Margin = new Padding(1);
            fieldTituloCodigo.Name = "fieldTituloCodigo";
            fieldTituloCodigo.Size = new Size(158, 56);
            fieldTituloCodigo.TabIndex = 15;
            fieldTituloCodigo.Text = "Código";
            fieldTituloCodigo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VistaGestionSolicitudesCompra
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1356, 608);
            Controls.Add(layoutVista);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaGestionSolicitudesCompra";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaGestionClientes";
            layoutVista.ResumeLayout(false);
            layoutHerramientas.ResumeLayout(false);
            layoutTituloHerramientas.ResumeLayout(false);
            layoutTitulo.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutControlesTabla.ResumeLayout(false);
            panelBotonesGestion.ResumeLayout(false);
            layoutEncabezadosTabla.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutVista;
        private TableLayoutPanel layoutTitulo;
        private Label fieldTitulo;
        private Guna2Button btnRegistrar;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private Panel contenedorVistas;
        private TableLayoutPanel layoutControlesTabla;
        private Guna2Button btnPaginaAnterior;
        private Guna2Button btnPrimeraPagina;
        private Guna2Button btnPaginaSiguiente;
        private Guna2Button btnUltimaPagina;
        private Guna2Button btnSincronizarDatos;
        private Label fieldPaginaActual;
        private Label fieldPaginasTotales;
        private Panel panelBotonesGestion;
        private TableLayoutPanel layoutHerramientas;
        private Guna2ComboBox fieldFiltroBusqueda;
        private Guna2TextBox fieldDatoBusqueda;
        private Guna2Separator separador1;
        private TableLayoutPanel layoutTituloHerramientas;
        private Label fieldTituloFiltrosBusqueda;
        private TableLayoutPanel layoutEncabezadosTabla;
        private Label fieldTituloEstado;
        private Label fieldTituloFechaSolicitud;
        private Label fieldTituloNombre;
        private Label fieldTituloCodigo;
        private Label fieldTituloFechaRequerida;
        private Label fieldTituloObservaciones;
        private Label fieldTituloImporteTotal;
    }
}