using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Venta.Vistas {
    partial class VistaGestionVentas {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaGestionVentas));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            fieldTituloDesde = new Label();
            fieldFiltroBusquedaFechaDesde = new Guna2DateTimePicker();
            fieldTituloHasta = new Label();
            fieldFiltroBusquedaFechaHasta = new Guna2DateTimePicker();
            btnRegistrar = new Guna2Button();
            layoutTituloTotales = new TableLayoutPanel();
            fieldTituloTotalRecaudado = new Label();
            layoutTotales = new TableLayoutPanel();
            fieldTotalRecaudado = new Label();
            layoutContenedorVistas = new TableLayoutPanel();
            contenedorVistas = new Panel();
            panelEncabezadosTabla = new Guna2Panel();
            layoutEncabezadosTabla = new TableLayoutPanel();
            fieldTituloAcciones = new Label();
            label1 = new Label();
            fieldTituloTotalBruto = new Label();
            fieldTituloMetodoPagoPrincipal = new Label();
            fieldTituloId = new Label();
            fieldTituloFecha = new Label();
            fieldTituloDescuentoTotal = new Label();
            fieldTituloImpuestoTotal = new Label();
            fieldTituloImporteTotal = new Label();
            fieldTituloEstado = new Label();
            layoutPago = new TableLayoutPanel();
            symbolPeso = new Label();
            fieldTotalVenta = new Label();
            fieldPaginasTotales = new Label();
            fieldPaginaActual = new Label();
            btnSincronizarDatos = new Guna2Button();
            btnUltimaPagina = new Guna2Button();
            btnPaginaSiguiente = new Guna2Button();
            btnPrimeraPagina = new Guna2Button();
            btnPaginaAnterior = new Guna2Button();
            layoutControlesTabla = new TableLayoutPanel();
            panelControlesTabla = new Guna2Panel();
            layoutVista.SuspendLayout();
            layoutTitulo.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutFiltroHerramientas.SuspendLayout();
            layoutTituloTotales.SuspendLayout();
            layoutTotales.SuspendLayout();
            layoutContenedorVistas.SuspendLayout();
            panelEncabezadosTabla.SuspendLayout();
            layoutEncabezadosTabla.SuspendLayout();
            layoutPago.SuspendLayout();
            layoutControlesTabla.SuspendLayout();
            panelControlesTabla.SuspendLayout();
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
            layoutVista.Controls.Add(panelControlesTabla, 2, 8);
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldIcono, 1, 1);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 2);
            layoutVista.Controls.Add(layoutFiltroHerramientas, 2, 4);
            layoutVista.Controls.Add(layoutTituloTotales, 2, 10);
            layoutVista.Controls.Add(layoutTotales, 2, 11);
            layoutVista.Controls.Add(layoutContenedorVistas, 2, 7);
            layoutVista.Controls.Add(panelEncabezadosTabla, 2, 6);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 13;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
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
            fieldTitulo.Text = "Gestión de ventas";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = (Image) resources.GetObject("fieldIcono.BackgroundImage");
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
            fieldSubtitulo.Text = "Listado de transacciones, búsqueda, filtros y acceso al detalle";
            // 
            // layoutFiltroHerramientas
            // 
            layoutFiltroHerramientas.Controls.Add(fieldTituloFiltroBusqueda);
            layoutFiltroHerramientas.Controls.Add(fieldFiltroBusqueda);
            layoutFiltroHerramientas.Controls.Add(fieldCriterioBusqueda);
            layoutFiltroHerramientas.Controls.Add(fieldTituloDesde);
            layoutFiltroHerramientas.Controls.Add(fieldFiltroBusquedaFechaDesde);
            layoutFiltroHerramientas.Controls.Add(fieldTituloHasta);
            layoutFiltroHerramientas.Controls.Add(fieldFiltroBusquedaFechaHasta);
            layoutFiltroHerramientas.Controls.Add(btnRegistrar);
            layoutFiltroHerramientas.Dock = DockStyle.Fill;
            layoutFiltroHerramientas.Location = new Point(50, 100);
            layoutFiltroHerramientas.Margin = new Padding(0);
            layoutFiltroHerramientas.Name = "layoutFiltroHerramientas";
            layoutFiltroHerramientas.Size = new Size(1286, 45);
            layoutFiltroHerramientas.TabIndex = 83;
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
            fieldFiltroBusqueda.CustomizableEdges = customizableEdges13;
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
            fieldFiltroBusqueda.ShadowDecoration.CustomizableEdges = customizableEdges14;
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
            fieldCriterioBusqueda.CustomizableEdges = customizableEdges15;
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
            fieldCriterioBusqueda.ShadowDecoration.CustomizableEdges = customizableEdges16;
            fieldCriterioBusqueda.Size = new Size(220, 35);
            fieldCriterioBusqueda.TabIndex = 9;
            fieldCriterioBusqueda.TextOffset = new Point(5, 0);
            fieldCriterioBusqueda.Visible = false;
            // 
            // fieldTituloDesde
            // 
            fieldTituloDesde.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloDesde.ForeColor = Color.DimGray;
            fieldTituloDesde.ImeMode = ImeMode.NoControl;
            fieldTituloDesde.Location = new Point(551, 1);
            fieldTituloDesde.Margin = new Padding(1);
            fieldTituloDesde.Name = "fieldTituloDesde";
            fieldTituloDesde.Size = new Size(57, 40);
            fieldTituloDesde.TabIndex = 28;
            fieldTituloDesde.Text = "DESDE :";
            fieldTituloDesde.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldFiltroBusquedaFechaDesde
            // 
            fieldFiltroBusquedaFechaDesde.Animated = true;
            fieldFiltroBusquedaFechaDesde.AutoRoundedCorners = true;
            fieldFiltroBusquedaFechaDesde.BackColor = Color.White;
            fieldFiltroBusquedaFechaDesde.BorderColor = Color.Gainsboro;
            fieldFiltroBusquedaFechaDesde.BorderRadius = 16;
            fieldFiltroBusquedaFechaDesde.BorderThickness = 1;
            fieldFiltroBusquedaFechaDesde.Checked = true;
            fieldFiltroBusquedaFechaDesde.CheckedState.BorderColor = Color.Gainsboro;
            fieldFiltroBusquedaFechaDesde.CheckedState.FillColor = Color.White;
            fieldFiltroBusquedaFechaDesde.CheckedState.ForeColor = Color.Black;
            fieldFiltroBusquedaFechaDesde.CustomFormat = "yyyy-MM-dd";
            fieldFiltroBusquedaFechaDesde.CustomizableEdges = customizableEdges17;
            fieldFiltroBusquedaFechaDesde.FillColor = Color.White;
            fieldFiltroBusquedaFechaDesde.Font = new Font("Segoe UI", 11.25F);
            fieldFiltroBusquedaFechaDesde.ForeColor = Color.Black;
            fieldFiltroBusquedaFechaDesde.Format = DateTimePickerFormat.Custom;
            fieldFiltroBusquedaFechaDesde.Location = new Point(614, 5);
            fieldFiltroBusquedaFechaDesde.Margin = new Padding(5);
            fieldFiltroBusquedaFechaDesde.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            fieldFiltroBusquedaFechaDesde.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            fieldFiltroBusquedaFechaDesde.Name = "fieldFiltroBusquedaFechaDesde";
            fieldFiltroBusquedaFechaDesde.ShadowDecoration.CustomizableEdges = customizableEdges18;
            fieldFiltroBusquedaFechaDesde.Size = new Size(134, 35);
            fieldFiltroBusquedaFechaDesde.TabIndex = 29;
            fieldFiltroBusquedaFechaDesde.Value = new DateTime(2026, 2, 5, 17, 13, 37, 0);
            // 
            // fieldTituloHasta
            // 
            fieldTituloHasta.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloHasta.ForeColor = Color.DimGray;
            fieldTituloHasta.ImeMode = ImeMode.NoControl;
            fieldTituloHasta.Location = new Point(754, 1);
            fieldTituloHasta.Margin = new Padding(1);
            fieldTituloHasta.Name = "fieldTituloHasta";
            fieldTituloHasta.Size = new Size(58, 40);
            fieldTituloHasta.TabIndex = 30;
            fieldTituloHasta.Text = "HASTA :";
            fieldTituloHasta.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldFiltroBusquedaFechaHasta
            // 
            fieldFiltroBusquedaFechaHasta.Animated = true;
            fieldFiltroBusquedaFechaHasta.AutoRoundedCorners = true;
            fieldFiltroBusquedaFechaHasta.BackColor = Color.White;
            fieldFiltroBusquedaFechaHasta.BorderColor = Color.Gainsboro;
            fieldFiltroBusquedaFechaHasta.BorderRadius = 16;
            fieldFiltroBusquedaFechaHasta.BorderThickness = 1;
            fieldFiltroBusquedaFechaHasta.Checked = true;
            fieldFiltroBusquedaFechaHasta.CheckedState.BorderColor = Color.Gainsboro;
            fieldFiltroBusquedaFechaHasta.CheckedState.FillColor = Color.White;
            fieldFiltroBusquedaFechaHasta.CheckedState.ForeColor = Color.Black;
            fieldFiltroBusquedaFechaHasta.CustomFormat = "yyyy-MM-dd";
            fieldFiltroBusquedaFechaHasta.CustomizableEdges = customizableEdges19;
            fieldFiltroBusquedaFechaHasta.FillColor = Color.White;
            fieldFiltroBusquedaFechaHasta.Font = new Font("Segoe UI", 11.25F);
            fieldFiltroBusquedaFechaHasta.ForeColor = Color.Black;
            fieldFiltroBusquedaFechaHasta.Format = DateTimePickerFormat.Custom;
            fieldFiltroBusquedaFechaHasta.Location = new Point(818, 5);
            fieldFiltroBusquedaFechaHasta.Margin = new Padding(5);
            fieldFiltroBusquedaFechaHasta.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            fieldFiltroBusquedaFechaHasta.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            fieldFiltroBusquedaFechaHasta.Name = "fieldFiltroBusquedaFechaHasta";
            fieldFiltroBusquedaFechaHasta.ShadowDecoration.CustomizableEdges = customizableEdges20;
            fieldFiltroBusquedaFechaHasta.Size = new Size(134, 35);
            fieldFiltroBusquedaFechaHasta.TabIndex = 31;
            fieldFiltroBusquedaFechaHasta.Value = new DateTime(2026, 2, 5, 17, 13, 37, 0);
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.AutoRoundedCorners = true;
            btnRegistrar.BackColor = Color.White;
            btnRegistrar.BorderRadius = 16;
            btnRegistrar.CustomizableEdges = customizableEdges21;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Image = (Image) resources.GetObject("btnRegistrar.Image");
            btnRegistrar.Location = new Point(960, 5);
            btnRegistrar.Margin = new Padding(3, 5, 3, 5);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges22;
            btnRegistrar.Size = new Size(180, 35);
            btnRegistrar.TabIndex = 7;
            btnRegistrar.Text = "Nueva venta manual";
            // 
            // layoutTituloTotales
            // 
            layoutTituloTotales.ColumnCount = 4;
            layoutTituloTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTituloTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutTituloTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutTituloTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            layoutTituloTotales.Controls.Add(fieldTituloTotalRecaudado, 3, 0);
            layoutTituloTotales.Dock = DockStyle.Fill;
            layoutTituloTotales.Location = new Point(50, 528);
            layoutTituloTotales.Margin = new Padding(0);
            layoutTituloTotales.Name = "layoutTituloTotales";
            layoutTituloTotales.RowCount = 1;
            layoutTituloTotales.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTituloTotales.Size = new Size(1286, 25);
            layoutTituloTotales.TabIndex = 86;
            // 
            // fieldTituloTotalRecaudado
            // 
            fieldTituloTotalRecaudado.Dock = DockStyle.Fill;
            fieldTituloTotalRecaudado.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloTotalRecaudado.ForeColor = Color.DimGray;
            fieldTituloTotalRecaudado.ImeMode = ImeMode.NoControl;
            fieldTituloTotalRecaudado.Location = new Point(1087, 1);
            fieldTituloTotalRecaudado.Margin = new Padding(1);
            fieldTituloTotalRecaudado.Name = "fieldTituloTotalRecaudado";
            fieldTituloTotalRecaudado.Size = new Size(198, 23);
            fieldTituloTotalRecaudado.TabIndex = 28;
            fieldTituloTotalRecaudado.Text = "TOTAL RECAUDADO";
            fieldTituloTotalRecaudado.TextAlign = ContentAlignment.MiddleRight;
            // 
            // layoutTotales
            // 
            layoutTotales.ColumnCount = 4;
            layoutTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            layoutTotales.Controls.Add(fieldTotalRecaudado, 3, 0);
            layoutTotales.Dock = DockStyle.Fill;
            layoutTotales.Location = new Point(50, 553);
            layoutTotales.Margin = new Padding(0);
            layoutTotales.Name = "layoutTotales";
            layoutTotales.RowCount = 1;
            layoutTotales.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTotales.Size = new Size(1286, 35);
            layoutTotales.TabIndex = 87;
            // 
            // fieldTotalRecaudado
            // 
            fieldTotalRecaudado.Dock = DockStyle.Fill;
            fieldTotalRecaudado.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTotalRecaudado.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldTotalRecaudado.ImeMode = ImeMode.NoControl;
            fieldTotalRecaudado.Location = new Point(1087, 1);
            fieldTotalRecaudado.Margin = new Padding(1);
            fieldTotalRecaudado.Name = "fieldTotalRecaudado";
            fieldTotalRecaudado.Size = new Size(198, 33);
            fieldTotalRecaudado.TabIndex = 71;
            fieldTotalRecaudado.Text = "$ 0.00";
            fieldTotalRecaudado.TextAlign = ContentAlignment.MiddleRight;
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
            layoutContenedorVistas.Size = new Size(1286, 279);
            layoutContenedorVistas.TabIndex = 88;
            // 
            // contenedorVistas
            // 
            contenedorVistas.BackColor = Color.White;
            contenedorVistas.Dock = DockStyle.Fill;
            contenedorVistas.Location = new Point(1, 1);
            contenedorVistas.Margin = new Padding(1, 1, 1, 0);
            contenedorVistas.Name = "contenedorVistas";
            contenedorVistas.Size = new Size(1284, 278);
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
            customizableEdges23.BottomLeft = false;
            customizableEdges23.BottomRight = false;
            panelEncabezadosTabla.CustomizableEdges = customizableEdges23;
            panelEncabezadosTabla.Dock = DockStyle.Fill;
            panelEncabezadosTabla.FillColor = SystemColors.ButtonFace;
            panelEncabezadosTabla.Location = new Point(50, 155);
            panelEncabezadosTabla.Margin = new Padding(0);
            panelEncabezadosTabla.Name = "panelEncabezadosTabla";
            panelEncabezadosTabla.ShadowDecoration.BorderRadius = 8;
            panelEncabezadosTabla.ShadowDecoration.CustomizableEdges = customizableEdges24;
            panelEncabezadosTabla.ShadowDecoration.Depth = 10;
            panelEncabezadosTabla.Size = new Size(1286, 42);
            panelEncabezadosTabla.TabIndex = 89;
            // 
            // layoutEncabezadosTabla
            // 
            layoutEncabezadosTabla.ColumnCount = 10;
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 111F));
            layoutEncabezadosTabla.Controls.Add(fieldTituloAcciones, 9, 0);
            layoutEncabezadosTabla.Controls.Add(label1, 0, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloTotalBruto, 4, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloMetodoPagoPrincipal, 3, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloId, 1, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloFecha, 2, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloDescuentoTotal, 5, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloImpuestoTotal, 6, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloImporteTotal, 7, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloEstado, 8, 0);
            layoutEncabezadosTabla.Dock = DockStyle.Fill;
            layoutEncabezadosTabla.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            layoutEncabezadosTabla.ForeColor = Color.DimGray;
            layoutEncabezadosTabla.Location = new Point(0, 0);
            layoutEncabezadosTabla.Margin = new Padding(0, 0, 0, 2);
            layoutEncabezadosTabla.Name = "layoutEncabezadosTabla";
            layoutEncabezadosTabla.RowCount = 1;
            layoutEncabezadosTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.Size = new Size(1286, 42);
            layoutEncabezadosTabla.TabIndex = 19;
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
            fieldTituloAcciones.TabIndex = 22;
            fieldTituloAcciones.Text = "ACCIONES";
            fieldTituloAcciones.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label1.ForeColor = Color.DimGray;
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(1, 1);
            label1.Margin = new Padding(1);
            label1.Name = "label1";
            label1.Size = new Size(58, 40);
            label1.TabIndex = 21;
            label1.Text = "ID";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloTotalBruto
            // 
            fieldTituloTotalBruto.Dock = DockStyle.Fill;
            fieldTituloTotalBruto.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloTotalBruto.ForeColor = Color.DimGray;
            fieldTituloTotalBruto.ImeMode = ImeMode.NoControl;
            fieldTituloTotalBruto.Location = new Point(616, 1);
            fieldTituloTotalBruto.Margin = new Padding(1);
            fieldTituloTotalBruto.Name = "fieldTituloTotalBruto";
            fieldTituloTotalBruto.Size = new Size(108, 40);
            fieldTituloTotalBruto.TabIndex = 15;
            fieldTituloTotalBruto.Text = "TOTAL BRUTO";
            fieldTituloTotalBruto.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTituloMetodoPagoPrincipal
            // 
            fieldTituloMetodoPagoPrincipal.Dock = DockStyle.Fill;
            fieldTituloMetodoPagoPrincipal.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloMetodoPagoPrincipal.ForeColor = Color.DimGray;
            fieldTituloMetodoPagoPrincipal.ImeMode = ImeMode.NoControl;
            fieldTituloMetodoPagoPrincipal.Location = new Point(341, 1);
            fieldTituloMetodoPagoPrincipal.Margin = new Padding(1);
            fieldTituloMetodoPagoPrincipal.Name = "fieldTituloMetodoPagoPrincipal";
            fieldTituloMetodoPagoPrincipal.Size = new Size(273, 40);
            fieldTituloMetodoPagoPrincipal.TabIndex = 15;
            fieldTituloMetodoPagoPrincipal.Text = "CANAL DE PAGO PRINCIPAL";
            fieldTituloMetodoPagoPrincipal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloId
            // 
            fieldTituloId.Dock = DockStyle.Fill;
            fieldTituloId.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloId.ForeColor = Color.DimGray;
            fieldTituloId.ImeMode = ImeMode.NoControl;
            fieldTituloId.Location = new Point(61, 1);
            fieldTituloId.Margin = new Padding(1);
            fieldTituloId.Name = "fieldTituloId";
            fieldTituloId.Size = new Size(158, 40);
            fieldTituloId.TabIndex = 14;
            fieldTituloId.Text = "N° DE FACTURA";
            fieldTituloId.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloFecha
            // 
            fieldTituloFecha.Dock = DockStyle.Fill;
            fieldTituloFecha.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloFecha.ForeColor = Color.DimGray;
            fieldTituloFecha.ImeMode = ImeMode.NoControl;
            fieldTituloFecha.Location = new Point(221, 1);
            fieldTituloFecha.Margin = new Padding(1);
            fieldTituloFecha.Name = "fieldTituloFecha";
            fieldTituloFecha.Size = new Size(118, 40);
            fieldTituloFecha.TabIndex = 16;
            fieldTituloFecha.Text = "FECHA";
            fieldTituloFecha.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloDescuentoTotal
            // 
            fieldTituloDescuentoTotal.Dock = DockStyle.Fill;
            fieldTituloDescuentoTotal.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloDescuentoTotal.ForeColor = Color.DimGray;
            fieldTituloDescuentoTotal.ImeMode = ImeMode.NoControl;
            fieldTituloDescuentoTotal.Location = new Point(726, 1);
            fieldTituloDescuentoTotal.Margin = new Padding(1);
            fieldTituloDescuentoTotal.Name = "fieldTituloDescuentoTotal";
            fieldTituloDescuentoTotal.Size = new Size(108, 40);
            fieldTituloDescuentoTotal.TabIndex = 17;
            fieldTituloDescuentoTotal.Text = "DESCUENTO TOTAL";
            fieldTituloDescuentoTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTituloImpuestoTotal
            // 
            fieldTituloImpuestoTotal.Dock = DockStyle.Fill;
            fieldTituloImpuestoTotal.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloImpuestoTotal.ForeColor = Color.DimGray;
            fieldTituloImpuestoTotal.ImeMode = ImeMode.NoControl;
            fieldTituloImpuestoTotal.Location = new Point(836, 1);
            fieldTituloImpuestoTotal.Margin = new Padding(1);
            fieldTituloImpuestoTotal.Name = "fieldTituloImpuestoTotal";
            fieldTituloImpuestoTotal.Size = new Size(108, 40);
            fieldTituloImpuestoTotal.TabIndex = 18;
            fieldTituloImpuestoTotal.Text = "IMPUESTO TOTAL";
            fieldTituloImpuestoTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTituloImporteTotal
            // 
            fieldTituloImporteTotal.Dock = DockStyle.Fill;
            fieldTituloImporteTotal.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloImporteTotal.ForeColor = Color.DimGray;
            fieldTituloImporteTotal.ImeMode = ImeMode.NoControl;
            fieldTituloImporteTotal.Location = new Point(946, 1);
            fieldTituloImporteTotal.Margin = new Padding(1);
            fieldTituloImporteTotal.Name = "fieldTituloImporteTotal";
            fieldTituloImporteTotal.Size = new Size(108, 40);
            fieldTituloImporteTotal.TabIndex = 19;
            fieldTituloImporteTotal.Text = "IMPORTE TOTAL";
            fieldTituloImporteTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTituloEstado
            // 
            fieldTituloEstado.Dock = DockStyle.Fill;
            fieldTituloEstado.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloEstado.ForeColor = Color.DimGray;
            fieldTituloEstado.ImeMode = ImeMode.NoControl;
            fieldTituloEstado.Location = new Point(1056, 1);
            fieldTituloEstado.Margin = new Padding(1);
            fieldTituloEstado.Name = "fieldTituloEstado";
            fieldTituloEstado.Size = new Size(118, 40);
            fieldTituloEstado.TabIndex = 20;
            fieldTituloEstado.Text = "ESTADO";
            fieldTituloEstado.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutPago
            // 
            layoutPago.ColumnCount = 4;
            layoutPago.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPago.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutPago.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutPago.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutPago.Controls.Add(symbolPeso, 0, 0);
            layoutPago.Location = new Point(0, 0);
            layoutPago.Name = "layoutPago";
            layoutPago.RowCount = 1;
            layoutPago.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutPago.Size = new Size(200, 100);
            layoutPago.TabIndex = 0;
            // 
            // symbolPeso
            // 
            symbolPeso.Dock = DockStyle.Fill;
            symbolPeso.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            symbolPeso.ForeColor = Color.Black;
            symbolPeso.ImageAlign = ContentAlignment.MiddleLeft;
            symbolPeso.ImeMode = ImeMode.NoControl;
            symbolPeso.Location = new Point(3, 5);
            symbolPeso.Margin = new Padding(3, 5, 3, 3);
            symbolPeso.Name = "symbolPeso";
            symbolPeso.Size = new Size(1, 92);
            symbolPeso.TabIndex = 2;
            symbolPeso.Text = "$";
            symbolPeso.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTotalVenta
            // 
            fieldTotalVenta.Dock = DockStyle.Fill;
            fieldTotalVenta.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTotalVenta.ForeColor = Color.Black;
            fieldTotalVenta.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTotalVenta.ImeMode = ImeMode.NoControl;
            fieldTotalVenta.Location = new Point(15, 5);
            fieldTotalVenta.Margin = new Padding(15, 5, 3, 3);
            fieldTotalVenta.Name = "fieldTotalVenta";
            fieldTotalVenta.Size = new Size(92, 37);
            fieldTotalVenta.TabIndex = 1;
            fieldTotalVenta.Text = "0";
            fieldTotalVenta.TextAlign = ContentAlignment.MiddleRight;
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
            btnSincronizarDatos.CustomizableEdges = customizableEdges9;
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
            btnSincronizarDatos.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnSincronizarDatos.Size = new Size(33, 40);
            btnSincronizarDatos.TabIndex = 4;
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
            btnUltimaPagina.CustomizableEdges = customizableEdges7;
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
            btnUltimaPagina.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnUltimaPagina.Size = new Size(33, 40);
            btnUltimaPagina.TabIndex = 3;
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
            btnPaginaSiguiente.CustomizableEdges = customizableEdges5;
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
            btnPaginaSiguiente.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnPaginaSiguiente.Size = new Size(33, 40);
            btnPaginaSiguiente.TabIndex = 2;
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
            btnPrimeraPagina.CustomizableEdges = customizableEdges3;
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
            btnPrimeraPagina.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnPrimeraPagina.Size = new Size(33, 40);
            btnPrimeraPagina.TabIndex = 0;
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
            btnPaginaAnterior.CustomizableEdges = customizableEdges1;
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
            btnPaginaAnterior.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnPaginaAnterior.Size = new Size(33, 40);
            btnPaginaAnterior.TabIndex = 1;
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
            layoutControlesTabla.TabIndex = 84;
            // 
            // panelControlesTabla
            // 
            panelControlesTabla.BackColor = Color.Transparent;
            panelControlesTabla.BorderColor = Color.Gainsboro;
            panelControlesTabla.BorderRadius = 8;
            panelControlesTabla.BorderThickness = 1;
            panelControlesTabla.Controls.Add(layoutControlesTabla);
            customizableEdges11.TopLeft = false;
            customizableEdges11.TopRight = false;
            panelControlesTabla.CustomizableEdges = customizableEdges11;
            panelControlesTabla.Dock = DockStyle.Fill;
            panelControlesTabla.FillColor = Color.White;
            panelControlesTabla.Location = new Point(50, 476);
            panelControlesTabla.Margin = new Padding(0);
            panelControlesTabla.Name = "panelControlesTabla";
            panelControlesTabla.ShadowDecoration.BorderRadius = 8;
            panelControlesTabla.ShadowDecoration.CustomizableEdges = customizableEdges12;
            panelControlesTabla.ShadowDecoration.Depth = 10;
            panelControlesTabla.Size = new Size(1286, 42);
            panelControlesTabla.TabIndex = 90;
            // 
            // VistaGestionVentas
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1356, 608);
            Controls.Add(layoutVista);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaGestionVentas";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistGestionProveedor";
            layoutVista.ResumeLayout(false);
            layoutTitulo.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutFiltroHerramientas.ResumeLayout(false);
            layoutTituloTotales.ResumeLayout(false);
            layoutTotales.ResumeLayout(false);
            layoutContenedorVistas.ResumeLayout(false);
            panelEncabezadosTabla.ResumeLayout(false);
            layoutEncabezadosTabla.ResumeLayout(false);
            layoutPago.ResumeLayout(false);
            layoutControlesTabla.ResumeLayout(false);
            panelControlesTabla.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutVista;
        private TableLayoutPanel layoutTitulo;
        private Label fieldTitulo;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private Label fieldTituloId;
        private TableLayoutPanel layoutEncabezadosTabla;
        private Label fieldTituloTotalBruto;
        private Label fieldTituloMetodoPagoPrincipal;
        private Label fieldTituloFecha;
        private TableLayoutPanel layoutPago;
        private Label symbolPeso;
        private Label fieldTotalVenta;
        private Label fieldTituloDescuentoTotal;
        private Label fieldTituloImpuestoTotal;
        private Label fieldTituloImporteTotal;
        private Label fieldTituloEstado;
        private FlowLayoutPanel layoutFiltroHerramientas;
        private Label fieldTituloFiltroBusqueda;
        private Guna2ComboBox fieldFiltroBusqueda;
        private Guna2TextBox fieldCriterioBusqueda;
        private Label fieldTituloDesde;
        private Guna2DateTimePicker fieldFiltroBusquedaFechaDesde;
        private Label fieldTituloHasta;
        private Guna2DateTimePicker fieldFiltroBusquedaFechaHasta;
        private Guna2Button btnRegistrar;
        private TableLayoutPanel layoutTituloTotales;
        private Label fieldTituloTotalRecaudado;
        private TableLayoutPanel layoutTotales;
        private Label fieldTotalRecaudado;
        private TableLayoutPanel layoutContenedorVistas;
        private Panel contenedorVistas;
        private Guna2Panel panelEncabezadosTabla;
        private Label label1;
        private Label fieldTituloAcciones;
        private Guna2Panel panelControlesTabla;
        private TableLayoutPanel layoutControlesTabla;
        private Guna2Button btnPaginaAnterior;
        private Guna2Button btnPrimeraPagina;
        private Guna2Button btnPaginaSiguiente;
        private Guna2Button btnUltimaPagina;
        private Guna2Button btnSincronizarDatos;
        private Label fieldPaginaActual;
        private Label fieldPaginasTotales;
    }
}