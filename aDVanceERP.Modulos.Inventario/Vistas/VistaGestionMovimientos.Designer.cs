using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    partial class VistaGestionMovimientos {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaGestionMovimientos));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            formatoBase = new Guna2BorderlessForm(components);
            layoutVista = new TableLayoutPanel();
            layoutTitulo = new TableLayoutPanel();
            fieldTitulo = new Label();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
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
            fieldTituloNombreProducto = new Label();
            fieldTituloFecha = new Label();
            fieldTituloMotivo = new Label();
            fieldTitulaCantidadMovida = new Label();
            fieldTituloAlmacenDestino = new Label();
            fieldTituloAlmacenOrigen = new Label();
            fieldTituloId = new Label();
            fieldTituloSaldoInicial = new Label();
            fieldTituloSaldoFinal = new Label();
            fieldTituloAcciones = new Label();
            layoutFiltroHerramientas = new FlowLayoutPanel();
            fieldTituloFiltroBusqueda = new Label();
            fieldFiltroBusqueda = new Guna2ComboBox();
            fieldCriterioBusqueda = new Guna2TextBox();
            fieldTituloDesde = new Label();
            fieldFiltroBusquedaFechaDesde = new Guna2DateTimePicker();
            fieldTituloHasta = new Label();
            fieldFiltroBusquedaFechaHasta = new Guna2DateTimePicker();
            btnRegistrar = new Guna2Button();
            layoutVista.SuspendLayout();
            layoutTitulo.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            panelControlesTabla.SuspendLayout();
            layoutControlesTabla.SuspendLayout();
            layoutContenedorVistas.SuspendLayout();
            panelEncabezadosTabla.SuspendLayout();
            layoutEncabezadosTabla.SuspendLayout();
            layoutFiltroHerramientas.SuspendLayout();
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
            layoutVista.Controls.Add(panelControlesTabla, 2, 8);
            layoutVista.Controls.Add(layoutContenedorVistas, 2, 7);
            layoutVista.Controls.Add(panelEncabezadosTabla, 2, 6);
            layoutVista.Controls.Add(layoutFiltroHerramientas, 2, 4);
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
            fieldTitulo.Text = "Gestión de movimientos";
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
            fieldSubtitulo.Text = "Control de entradas, salidas y traslados entre almacenes.";
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
            panelControlesTabla.Location = new Point(50, 546);
            panelControlesTabla.Margin = new Padding(0);
            panelControlesTabla.Name = "panelControlesTabla";
            panelControlesTabla.ShadowDecoration.BorderRadius = 8;
            panelControlesTabla.ShadowDecoration.CustomizableEdges = customizableEdges12;
            panelControlesTabla.ShadowDecoration.Depth = 10;
            panelControlesTabla.Size = new Size(1286, 42);
            panelControlesTabla.TabIndex = 79;
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
            layoutContenedorVistas.TabIndex = 80;
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
            customizableEdges13.BottomLeft = false;
            customizableEdges13.BottomRight = false;
            panelEncabezadosTabla.CustomizableEdges = customizableEdges13;
            panelEncabezadosTabla.Dock = DockStyle.Fill;
            panelEncabezadosTabla.FillColor = SystemColors.ButtonFace;
            panelEncabezadosTabla.Location = new Point(50, 155);
            panelEncabezadosTabla.Margin = new Padding(0);
            panelEncabezadosTabla.Name = "panelEncabezadosTabla";
            panelEncabezadosTabla.ShadowDecoration.BorderRadius = 8;
            panelEncabezadosTabla.ShadowDecoration.CustomizableEdges = customizableEdges14;
            panelEncabezadosTabla.ShadowDecoration.Depth = 10;
            panelEncabezadosTabla.Size = new Size(1286, 42);
            panelEncabezadosTabla.TabIndex = 81;
            // 
            // layoutEncabezadosTabla
            // 
            layoutEncabezadosTabla.BackColor = Color.Transparent;
            layoutEncabezadosTabla.ColumnCount = 11;
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 111F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutEncabezadosTabla.Controls.Add(fieldTituloNombreProducto, 0, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloFecha, 9, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloMotivo, 8, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTitulaCantidadMovida, 6, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloAlmacenDestino, 4, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloAlmacenOrigen, 2, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloId, 0, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloSaldoInicial, 5, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloSaldoFinal, 7, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloAcciones, 10, 0);
            layoutEncabezadosTabla.Dock = DockStyle.Fill;
            layoutEncabezadosTabla.Location = new Point(0, 0);
            layoutEncabezadosTabla.Margin = new Padding(0, 0, 0, 2);
            layoutEncabezadosTabla.Name = "layoutEncabezadosTabla";
            layoutEncabezadosTabla.RowCount = 1;
            layoutEncabezadosTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.Size = new Size(1286, 42);
            layoutEncabezadosTabla.TabIndex = 19;
            // 
            // fieldTituloNombreProducto
            // 
            fieldTituloNombreProducto.Dock = DockStyle.Fill;
            fieldTituloNombreProducto.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloNombreProducto.ForeColor = Color.DimGray;
            fieldTituloNombreProducto.ImeMode = ImeMode.NoControl;
            fieldTituloNombreProducto.Location = new Point(61, 1);
            fieldTituloNombreProducto.Margin = new Padding(1);
            fieldTituloNombreProducto.Name = "fieldTituloNombreProducto";
            fieldTituloNombreProducto.Size = new Size(223, 40);
            fieldTituloNombreProducto.TabIndex = 16;
            fieldTituloNombreProducto.Text = "PRODUCTO";
            fieldTituloNombreProducto.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloFecha
            // 
            fieldTituloFecha.Dock = DockStyle.Fill;
            fieldTituloFecha.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloFecha.ForeColor = Color.DimGray;
            fieldTituloFecha.ImeMode = ImeMode.NoControl;
            fieldTituloFecha.Location = new Point(1056, 1);
            fieldTituloFecha.Margin = new Padding(1);
            fieldTituloFecha.Name = "fieldTituloFecha";
            fieldTituloFecha.Size = new Size(118, 40);
            fieldTituloFecha.TabIndex = 15;
            fieldTituloFecha.Text = "FECHA";
            fieldTituloFecha.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloMotivo
            // 
            fieldTituloMotivo.Dock = DockStyle.Fill;
            fieldTituloMotivo.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloMotivo.ForeColor = Color.DimGray;
            fieldTituloMotivo.ImeMode = ImeMode.NoControl;
            fieldTituloMotivo.Location = new Point(896, 1);
            fieldTituloMotivo.Margin = new Padding(1);
            fieldTituloMotivo.Name = "fieldTituloMotivo";
            fieldTituloMotivo.Size = new Size(158, 40);
            fieldTituloMotivo.TabIndex = 15;
            fieldTituloMotivo.Text = "TIPO DE MOV.";
            fieldTituloMotivo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTitulaCantidadMovida
            // 
            fieldTitulaCantidadMovida.Dock = DockStyle.Fill;
            fieldTitulaCantidadMovida.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTitulaCantidadMovida.ForeColor = Color.DimGray;
            fieldTitulaCantidadMovida.ImeMode = ImeMode.NoControl;
            fieldTitulaCantidadMovida.Location = new Point(676, 1);
            fieldTitulaCantidadMovida.Margin = new Padding(1);
            fieldTitulaCantidadMovida.Name = "fieldTitulaCantidadMovida";
            fieldTitulaCantidadMovida.Size = new Size(108, 40);
            fieldTitulaCantidadMovida.TabIndex = 15;
            fieldTitulaCantidadMovida.Text = "CANT. MOVIDA";
            fieldTitulaCantidadMovida.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloAlmacenDestino
            // 
            fieldTituloAlmacenDestino.Dock = DockStyle.Fill;
            fieldTituloAlmacenDestino.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloAlmacenDestino.ForeColor = Color.DimGray;
            fieldTituloAlmacenDestino.ImeMode = ImeMode.NoControl;
            fieldTituloAlmacenDestino.Location = new Point(446, 1);
            fieldTituloAlmacenDestino.Margin = new Padding(1);
            fieldTituloAlmacenDestino.Name = "fieldTituloAlmacenDestino";
            fieldTituloAlmacenDestino.Size = new Size(118, 40);
            fieldTituloAlmacenDestino.TabIndex = 15;
            fieldTituloAlmacenDestino.Text = "DESTINO";
            fieldTituloAlmacenDestino.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloAlmacenOrigen
            // 
            fieldTituloAlmacenOrigen.Dock = DockStyle.Fill;
            fieldTituloAlmacenOrigen.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloAlmacenOrigen.ForeColor = Color.DimGray;
            fieldTituloAlmacenOrigen.ImeMode = ImeMode.NoControl;
            fieldTituloAlmacenOrigen.Location = new Point(286, 1);
            fieldTituloAlmacenOrigen.Margin = new Padding(1);
            fieldTituloAlmacenOrigen.Name = "fieldTituloAlmacenOrigen";
            fieldTituloAlmacenOrigen.Size = new Size(118, 40);
            fieldTituloAlmacenOrigen.TabIndex = 15;
            fieldTituloAlmacenOrigen.Text = "ORIGEN";
            fieldTituloAlmacenOrigen.TextAlign = ContentAlignment.MiddleRight;
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
            // fieldTituloSaldoInicial
            // 
            fieldTituloSaldoInicial.Dock = DockStyle.Fill;
            fieldTituloSaldoInicial.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloSaldoInicial.ForeColor = Color.DimGray;
            fieldTituloSaldoInicial.ImeMode = ImeMode.NoControl;
            fieldTituloSaldoInicial.Location = new Point(566, 1);
            fieldTituloSaldoInicial.Margin = new Padding(1);
            fieldTituloSaldoInicial.Name = "fieldTituloSaldoInicial";
            fieldTituloSaldoInicial.Size = new Size(108, 40);
            fieldTituloSaldoInicial.TabIndex = 17;
            fieldTituloSaldoInicial.Text = "SALDO INICIAL";
            fieldTituloSaldoInicial.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloSaldoFinal
            // 
            fieldTituloSaldoFinal.Dock = DockStyle.Fill;
            fieldTituloSaldoFinal.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloSaldoFinal.ForeColor = Color.DimGray;
            fieldTituloSaldoFinal.ImeMode = ImeMode.NoControl;
            fieldTituloSaldoFinal.Location = new Point(786, 1);
            fieldTituloSaldoFinal.Margin = new Padding(1);
            fieldTituloSaldoFinal.Name = "fieldTituloSaldoFinal";
            fieldTituloSaldoFinal.Size = new Size(108, 40);
            fieldTituloSaldoFinal.TabIndex = 18;
            fieldTituloSaldoFinal.Text = "SALDO FINAL";
            fieldTituloSaldoFinal.TextAlign = ContentAlignment.MiddleLeft;
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
            fieldTituloAcciones.TabIndex = 20;
            fieldTituloAcciones.Text = "ACCIONES";
            fieldTituloAcciones.TextAlign = ContentAlignment.MiddleLeft;
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
            layoutFiltroHerramientas.TabIndex = 82;
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
            fieldFiltroBusqueda.CustomizableEdges = customizableEdges15;
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
            fieldFiltroBusqueda.ShadowDecoration.CustomizableEdges = customizableEdges16;
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
            fieldCriterioBusqueda.CustomizableEdges = customizableEdges17;
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
            fieldCriterioBusqueda.ShadowDecoration.CustomizableEdges = customizableEdges18;
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
            fieldFiltroBusquedaFechaDesde.CustomizableEdges = customizableEdges19;
            fieldFiltroBusquedaFechaDesde.FillColor = Color.White;
            fieldFiltroBusquedaFechaDesde.Font = new Font("Segoe UI", 11.25F);
            fieldFiltroBusquedaFechaDesde.ForeColor = Color.Black;
            fieldFiltroBusquedaFechaDesde.Format = DateTimePickerFormat.Custom;
            fieldFiltroBusquedaFechaDesde.Location = new Point(614, 5);
            fieldFiltroBusquedaFechaDesde.Margin = new Padding(5);
            fieldFiltroBusquedaFechaDesde.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            fieldFiltroBusquedaFechaDesde.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            fieldFiltroBusquedaFechaDesde.Name = "fieldFiltroBusquedaFechaDesde";
            fieldFiltroBusquedaFechaDesde.ShadowDecoration.CustomizableEdges = customizableEdges20;
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
            fieldFiltroBusquedaFechaHasta.CustomizableEdges = customizableEdges21;
            fieldFiltroBusquedaFechaHasta.FillColor = Color.White;
            fieldFiltroBusquedaFechaHasta.Font = new Font("Segoe UI", 11.25F);
            fieldFiltroBusquedaFechaHasta.ForeColor = Color.Black;
            fieldFiltroBusquedaFechaHasta.Format = DateTimePickerFormat.Custom;
            fieldFiltroBusquedaFechaHasta.Location = new Point(818, 5);
            fieldFiltroBusquedaFechaHasta.Margin = new Padding(5);
            fieldFiltroBusquedaFechaHasta.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            fieldFiltroBusquedaFechaHasta.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            fieldFiltroBusquedaFechaHasta.Name = "fieldFiltroBusquedaFechaHasta";
            fieldFiltroBusquedaFechaHasta.ShadowDecoration.CustomizableEdges = customizableEdges22;
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
            btnRegistrar.CustomizableEdges = customizableEdges23;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Image = (Image) resources.GetObject("btnRegistrar.Image");
            btnRegistrar.ImageOffset = new Point(-5, 0);
            btnRegistrar.Location = new Point(960, 5);
            btnRegistrar.Margin = new Padding(3, 5, 3, 5);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges24;
            btnRegistrar.Size = new Size(192, 35);
            btnRegistrar.TabIndex = 7;
            btnRegistrar.Text = "Nuevo movimiento";
            // 
            // VistaGestionMovimientos
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1356, 608);
            Controls.Add(layoutVista);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaGestionMovimientos";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistGestionProveedor";
            layoutVista.ResumeLayout(false);
            layoutTitulo.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            panelControlesTabla.ResumeLayout(false);
            layoutControlesTabla.ResumeLayout(false);
            layoutContenedorVistas.ResumeLayout(false);
            panelEncabezadosTabla.ResumeLayout(false);
            layoutEncabezadosTabla.ResumeLayout(false);
            layoutFiltroHerramientas.ResumeLayout(false);
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
        private Label fieldTituloAlmacenOrigen;
        private Label fieldTituloFecha;
        private Label fieldTituloMotivo;
        private Label fieldTitulaCantidadMovida;
        private Label fieldTituloAlmacenDestino;
        private Label fieldTituloNombreProducto;
        private Label fieldTituloSaldoInicial;
        private Label fieldTituloSaldoFinal;
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
        private FlowLayoutPanel layoutFiltroHerramientas;
        private Label fieldTituloFiltroBusqueda;
        private Guna2ComboBox fieldFiltroBusqueda;
        private Guna2TextBox fieldCriterioBusqueda;
        private Guna2Button btnRegistrar;
        private Label fieldTituloDesde;
        private Guna2DateTimePicker fieldFiltroBusquedaFechaDesde;
        private Label fieldTituloHasta;
        private Guna2DateTimePicker fieldFiltroBusquedaFechaHasta;
        private Label fieldTituloAcciones;
    }
}