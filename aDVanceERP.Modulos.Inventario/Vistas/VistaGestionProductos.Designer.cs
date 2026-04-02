using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    partial class VistaGestionProductos {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaGestionProductos));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutVista = new TableLayoutPanel();
            layoutTitulosTotales = new TableLayoutPanel();
            fieldTituloValorTotalInventario = new Label();
            panelEncabezadosTabla = new Guna2Panel();
            layoutEncabezadosTabla = new TableLayoutPanel();
            fieldTituloAcciones = new Label();
            fieldFechaUlimoMovimiento = new Label();
            fieldTituloId = new Label();
            fieldTituloDescripcion = new Label();
            fieldTituloPrecioCesion = new Label();
            fieldTituloCantidad = new Label();
            fieldTituloUnidadMedida = new Label();
            fieldTituloNombre = new Label();
            fieldTituloCodigo = new Label();
            fieldTituloPrecioAdquisicion = new Label();
            layoutTitulo = new TableLayoutPanel();
            btnGenerarCatalogo = new Guna2Button();
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
            layoutTotales = new TableLayoutPanel();
            fieldValorTotalInventario = new Label();
            layoutFiltroHerramientas = new FlowLayoutPanel();
            fieldTituloAlmacen = new Label();
            fieldFiltroAlmacen = new Guna2ComboBox();
            fieldTituloCategoria = new Label();
            fieldFiltroCategoriaProducto = new Guna2ComboBox();
            fieldTituloFiltroBusqueda = new Label();
            fieldFiltroBusqueda = new Guna2ComboBox();
            fieldCriterioBusqueda = new Guna2TextBox();
            btnRegistrar = new Guna2Button();
            layoutVista.SuspendLayout();
            layoutTitulosTotales.SuspendLayout();
            panelEncabezadosTabla.SuspendLayout();
            layoutEncabezadosTabla.SuspendLayout();
            layoutTitulo.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            panelControlesTabla.SuspendLayout();
            layoutControlesTabla.SuspendLayout();
            layoutContenedorVistas.SuspendLayout();
            layoutTotales.SuspendLayout();
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
            layoutVista.Controls.Add(layoutTitulosTotales, 2, 10);
            layoutVista.Controls.Add(panelEncabezadosTabla, 2, 6);
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldIcono, 1, 1);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 2);
            layoutVista.Controls.Add(panelControlesTabla, 2, 8);
            layoutVista.Controls.Add(layoutContenedorVistas, 2, 7);
            layoutVista.Controls.Add(layoutTotales, 2, 11);
            layoutVista.Controls.Add(layoutFiltroHerramientas, 2, 4);
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
            layoutVista.Size = new Size(1386, 608);
            layoutVista.TabIndex = 4;
            // 
            // layoutTitulosTotales
            // 
            layoutTitulosTotales.ColumnCount = 4;
            layoutTitulosTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulosTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutTitulosTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutTitulosTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            layoutTitulosTotales.Controls.Add(fieldTituloValorTotalInventario, 3, 0);
            layoutTitulosTotales.Dock = DockStyle.Fill;
            layoutTitulosTotales.Location = new Point(50, 528);
            layoutTitulosTotales.Margin = new Padding(0);
            layoutTitulosTotales.Name = "layoutTitulosTotales";
            layoutTitulosTotales.RowCount = 1;
            layoutTitulosTotales.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulosTotales.Size = new Size(1316, 25);
            layoutTitulosTotales.TabIndex = 78;
            // 
            // fieldTituloValorTotalInventario
            // 
            fieldTituloValorTotalInventario.Dock = DockStyle.Fill;
            fieldTituloValorTotalInventario.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloValorTotalInventario.ForeColor = Color.DimGray;
            fieldTituloValorTotalInventario.ImeMode = ImeMode.NoControl;
            fieldTituloValorTotalInventario.Location = new Point(1117, 1);
            fieldTituloValorTotalInventario.Margin = new Padding(1);
            fieldTituloValorTotalInventario.Name = "fieldTituloValorTotalInventario";
            fieldTituloValorTotalInventario.Size = new Size(198, 23);
            fieldTituloValorTotalInventario.TabIndex = 28;
            fieldTituloValorTotalInventario.Text = "VALOR TOTAL INVENTARIO";
            fieldTituloValorTotalInventario.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelEncabezadosTabla
            // 
            panelEncabezadosTabla.BackColor = Color.Transparent;
            panelEncabezadosTabla.BorderColor = Color.Gainsboro;
            panelEncabezadosTabla.BorderRadius = 8;
            panelEncabezadosTabla.BorderThickness = 1;
            panelEncabezadosTabla.Controls.Add(layoutEncabezadosTabla);
            panelEncabezadosTabla.CustomBorderThickness = new Padding(1, 1, 1, 3);
            customizableEdges1.BottomLeft = false;
            customizableEdges1.BottomRight = false;
            panelEncabezadosTabla.CustomizableEdges = customizableEdges1;
            panelEncabezadosTabla.Dock = DockStyle.Fill;
            panelEncabezadosTabla.FillColor = SystemColors.ButtonFace;
            panelEncabezadosTabla.Location = new Point(50, 155);
            panelEncabezadosTabla.Margin = new Padding(0);
            panelEncabezadosTabla.Name = "panelEncabezadosTabla";
            panelEncabezadosTabla.ShadowDecoration.BorderRadius = 8;
            panelEncabezadosTabla.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelEncabezadosTabla.ShadowDecoration.Depth = 10;
            panelEncabezadosTabla.Size = new Size(1316, 42);
            panelEncabezadosTabla.TabIndex = 71;
            // 
            // layoutEncabezadosTabla
            // 
            layoutEncabezadosTabla.BackColor = Color.Transparent;
            layoutEncabezadosTabla.ColumnCount = 10;
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 148F));
            layoutEncabezadosTabla.Controls.Add(fieldTituloAcciones, 9, 0);
            layoutEncabezadosTabla.Controls.Add(fieldFechaUlimoMovimiento, 2, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloId, 0, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloDescripcion, 4, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloPrecioCesion, 6, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloCantidad, 7, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloUnidadMedida, 8, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloNombre, 3, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloCodigo, 1, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloPrecioAdquisicion, 5, 0);
            layoutEncabezadosTabla.Dock = DockStyle.Fill;
            layoutEncabezadosTabla.Location = new Point(0, 0);
            layoutEncabezadosTabla.Margin = new Padding(1);
            layoutEncabezadosTabla.Name = "layoutEncabezadosTabla";
            layoutEncabezadosTabla.RowCount = 1;
            layoutEncabezadosTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.Size = new Size(1316, 42);
            layoutEncabezadosTabla.TabIndex = 11;
            // 
            // fieldTituloAcciones
            // 
            fieldTituloAcciones.Dock = DockStyle.Fill;
            fieldTituloAcciones.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloAcciones.ForeColor = Color.DimGray;
            fieldTituloAcciones.ImeMode = ImeMode.NoControl;
            fieldTituloAcciones.Location = new Point(1168, 1);
            fieldTituloAcciones.Margin = new Padding(1);
            fieldTituloAcciones.Name = "fieldTituloAcciones";
            fieldTituloAcciones.Size = new Size(147, 40);
            fieldTituloAcciones.TabIndex = 21;
            fieldTituloAcciones.Text = "ACCIONES";
            fieldTituloAcciones.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldFechaUlimoMovimiento
            // 
            fieldFechaUlimoMovimiento.Dock = DockStyle.Fill;
            fieldFechaUlimoMovimiento.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldFechaUlimoMovimiento.ForeColor = Color.DimGray;
            fieldFechaUlimoMovimiento.ImeMode = ImeMode.NoControl;
            fieldFechaUlimoMovimiento.Location = new Point(181, 1);
            fieldFechaUlimoMovimiento.Margin = new Padding(1);
            fieldFechaUlimoMovimiento.Name = "fieldFechaUlimoMovimiento";
            fieldFechaUlimoMovimiento.Size = new Size(118, 40);
            fieldFechaUlimoMovimiento.TabIndex = 16;
            fieldFechaUlimoMovimiento.Text = "ÚLTIMO MOV.";
            fieldFechaUlimoMovimiento.TextAlign = ContentAlignment.MiddleLeft;
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
            fieldTituloDescripcion.Location = new Point(484, 1);
            fieldTituloDescripcion.Margin = new Padding(1);
            fieldTituloDescripcion.Name = "fieldTituloDescripcion";
            fieldTituloDescripcion.Size = new Size(272, 40);
            fieldTituloDescripcion.TabIndex = 16;
            fieldTituloDescripcion.Text = "DESCRIPCIÓN";
            fieldTituloDescripcion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloPrecioCesion
            // 
            fieldTituloPrecioCesion.Dock = DockStyle.Fill;
            fieldTituloPrecioCesion.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloPrecioCesion.ForeColor = Color.DimGray;
            fieldTituloPrecioCesion.ImeMode = ImeMode.NoControl;
            fieldTituloPrecioCesion.Location = new Point(868, 1);
            fieldTituloPrecioCesion.Margin = new Padding(1);
            fieldTituloPrecioCesion.Name = "fieldTituloPrecioCesion";
            fieldTituloPrecioCesion.Size = new Size(108, 40);
            fieldTituloPrecioCesion.TabIndex = 18;
            fieldTituloPrecioCesion.Text = "PRECIO VENTA";
            fieldTituloPrecioCesion.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTituloCantidad
            // 
            fieldTituloCantidad.Dock = DockStyle.Fill;
            fieldTituloCantidad.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloCantidad.ForeColor = Color.DimGray;
            fieldTituloCantidad.ImeMode = ImeMode.NoControl;
            fieldTituloCantidad.Location = new Point(978, 1);
            fieldTituloCantidad.Margin = new Padding(1);
            fieldTituloCantidad.Name = "fieldTituloCantidad";
            fieldTituloCantidad.Size = new Size(108, 40);
            fieldTituloCantidad.TabIndex = 19;
            fieldTituloCantidad.Text = "CANTIDAD";
            fieldTituloCantidad.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTituloUnidadMedida
            // 
            fieldTituloUnidadMedida.Dock = DockStyle.Fill;
            fieldTituloUnidadMedida.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloUnidadMedida.ForeColor = Color.DimGray;
            fieldTituloUnidadMedida.ImeMode = ImeMode.NoControl;
            fieldTituloUnidadMedida.Location = new Point(1088, 1);
            fieldTituloUnidadMedida.Margin = new Padding(1);
            fieldTituloUnidadMedida.Name = "fieldTituloUnidadMedida";
            fieldTituloUnidadMedida.Size = new Size(78, 40);
            fieldTituloUnidadMedida.TabIndex = 20;
            fieldTituloUnidadMedida.Text = "U.M.";
            fieldTituloUnidadMedida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloNombre
            // 
            fieldTituloNombre.Dock = DockStyle.Fill;
            fieldTituloNombre.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloNombre.ForeColor = Color.DimGray;
            fieldTituloNombre.ImeMode = ImeMode.NoControl;
            fieldTituloNombre.Location = new Point(301, 1);
            fieldTituloNombre.Margin = new Padding(1);
            fieldTituloNombre.Name = "fieldTituloNombre";
            fieldTituloNombre.Size = new Size(181, 40);
            fieldTituloNombre.TabIndex = 4;
            fieldTituloNombre.Text = "NOMBRE";
            fieldTituloNombre.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloCodigo
            // 
            fieldTituloCodigo.Dock = DockStyle.Fill;
            fieldTituloCodigo.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloCodigo.ForeColor = Color.DimGray;
            fieldTituloCodigo.ImeMode = ImeMode.NoControl;
            fieldTituloCodigo.Location = new Point(61, 1);
            fieldTituloCodigo.Margin = new Padding(1);
            fieldTituloCodigo.Name = "fieldTituloCodigo";
            fieldTituloCodigo.Size = new Size(118, 40);
            fieldTituloCodigo.TabIndex = 15;
            fieldTituloCodigo.Text = "CÓDIGO";
            fieldTituloCodigo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloPrecioAdquisicion
            // 
            fieldTituloPrecioAdquisicion.Dock = DockStyle.Fill;
            fieldTituloPrecioAdquisicion.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloPrecioAdquisicion.ForeColor = Color.DimGray;
            fieldTituloPrecioAdquisicion.ImeMode = ImeMode.NoControl;
            fieldTituloPrecioAdquisicion.Location = new Point(758, 1);
            fieldTituloPrecioAdquisicion.Margin = new Padding(1);
            fieldTituloPrecioAdquisicion.Name = "fieldTituloPrecioAdquisicion";
            fieldTituloPrecioAdquisicion.Size = new Size(108, 40);
            fieldTituloPrecioAdquisicion.TabIndex = 17;
            fieldTituloPrecioAdquisicion.Text = "COSTO ";
            fieldTituloPrecioAdquisicion.TextAlign = ContentAlignment.MiddleRight;
            // 
            // layoutTitulo
            // 
            layoutTitulo.ColumnCount = 2;
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 188F));
            layoutTitulo.Controls.Add(btnGenerarCatalogo, 1, 0);
            layoutTitulo.Controls.Add(fieldTitulo, 0, 0);
            layoutTitulo.Dock = DockStyle.Fill;
            layoutTitulo.Location = new Point(50, 10);
            layoutTitulo.Margin = new Padding(0);
            layoutTitulo.Name = "layoutTitulo";
            layoutTitulo.RowCount = 1;
            layoutTitulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulo.Size = new Size(1316, 45);
            layoutTitulo.TabIndex = 14;
            // 
            // btnGenerarCatalogo
            // 
            btnGenerarCatalogo.Animated = true;
            btnGenerarCatalogo.AutoRoundedCorners = true;
            btnGenerarCatalogo.BackColor = Color.White;
            btnGenerarCatalogo.BorderColor = Color.Gray;
            btnGenerarCatalogo.BorderRadius = 18;
            btnGenerarCatalogo.BorderThickness = 1;
            btnGenerarCatalogo.CustomizableEdges = customizableEdges3;
            btnGenerarCatalogo.Dock = DockStyle.Fill;
            btnGenerarCatalogo.FillColor = Color.White;
            btnGenerarCatalogo.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnGenerarCatalogo.ForeColor = Color.Black;
            btnGenerarCatalogo.HoverState.BorderColor = Color.PeachPuff;
            btnGenerarCatalogo.HoverState.FillColor = Color.PeachPuff;
            btnGenerarCatalogo.HoverState.ForeColor = Color.Black;
            btnGenerarCatalogo.Image = (Image) resources.GetObject("btnGenerarCatalogo.Image");
            btnGenerarCatalogo.ImageOffset = new Point(-5, 0);
            btnGenerarCatalogo.Location = new Point(1131, 3);
            btnGenerarCatalogo.Name = "btnGenerarCatalogo";
            btnGenerarCatalogo.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnGenerarCatalogo.Size = new Size(182, 39);
            btnGenerarCatalogo.TabIndex = 17;
            btnGenerarCatalogo.Text = "Generar catálogo";
            // 
            // fieldTitulo
            // 
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 20.25F);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(3, 0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(1122, 45);
            fieldTitulo.TabIndex = 3;
            fieldTitulo.Text = "Gestión de productos";
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
            fieldSubtitulo.Size = new Size(1310, 29);
            fieldSubtitulo.TabIndex = 2;
            fieldSubtitulo.Text = "Registro, edición, eliminación, búsqueda de mercancías, productos terminados y materias primas.";
            // 
            // panelControlesTabla
            // 
            panelControlesTabla.BackColor = Color.Transparent;
            panelControlesTabla.BorderColor = Color.Gainsboro;
            panelControlesTabla.BorderRadius = 8;
            panelControlesTabla.BorderThickness = 1;
            panelControlesTabla.Controls.Add(layoutControlesTabla);
            customizableEdges15.TopLeft = false;
            customizableEdges15.TopRight = false;
            panelControlesTabla.CustomizableEdges = customizableEdges15;
            panelControlesTabla.Dock = DockStyle.Fill;
            panelControlesTabla.FillColor = Color.White;
            panelControlesTabla.Location = new Point(50, 476);
            panelControlesTabla.Margin = new Padding(0);
            panelControlesTabla.Name = "panelControlesTabla";
            panelControlesTabla.ShadowDecoration.BorderRadius = 8;
            panelControlesTabla.ShadowDecoration.CustomizableEdges = customizableEdges16;
            panelControlesTabla.ShadowDecoration.Depth = 10;
            panelControlesTabla.Size = new Size(1316, 42);
            panelControlesTabla.TabIndex = 76;
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
            layoutControlesTabla.Size = new Size(1316, 42);
            layoutControlesTabla.TabIndex = 17;
            // 
            // btnPaginaAnterior
            // 
            btnPaginaAnterior.Animated = true;
            btnPaginaAnterior.BackColor = Color.Transparent;
            btnPaginaAnterior.CheckedState.BorderColor = Color.WhiteSmoke;
            btnPaginaAnterior.CheckedState.FillColor = Color.WhiteSmoke;
            btnPaginaAnterior.Cursor = Cursors.Hand;
            btnPaginaAnterior.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnPaginaAnterior.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnPaginaAnterior.CustomImages.ImageSize = new Size(24, 24);
            btnPaginaAnterior.CustomizableEdges = customizableEdges5;
            btnPaginaAnterior.DisabledState.FillColor = Color.White;
            btnPaginaAnterior.Dock = DockStyle.Fill;
            btnPaginaAnterior.FillColor = Color.White;
            btnPaginaAnterior.Font = new Font("Segoe UI", 9F);
            btnPaginaAnterior.ForeColor = Color.White;
            btnPaginaAnterior.HoverState.BorderColor = Color.FromArgb(  245,   245,   245);
            btnPaginaAnterior.HoverState.FillColor = Color.WhiteSmoke;
            btnPaginaAnterior.ImageSize = new Size(24, 24);
            btnPaginaAnterior.Location = new Point(46, 1);
            btnPaginaAnterior.Margin = new Padding(1);
            btnPaginaAnterior.Name = "btnPaginaAnterior";
            btnPaginaAnterior.ShadowDecoration.CustomizableEdges = customizableEdges6;
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
            btnPrimeraPagina.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnPrimeraPagina.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnPrimeraPagina.CustomImages.ImageSize = new Size(24, 24);
            btnPrimeraPagina.CustomizableEdges = customizableEdges7;
            btnPrimeraPagina.DisabledState.FillColor = Color.White;
            btnPrimeraPagina.Dock = DockStyle.Fill;
            btnPrimeraPagina.FillColor = Color.White;
            btnPrimeraPagina.Font = new Font("Segoe UI", 9F);
            btnPrimeraPagina.ForeColor = Color.White;
            btnPrimeraPagina.HoverState.BorderColor = Color.FromArgb(  245,   245,   245);
            btnPrimeraPagina.HoverState.FillColor = Color.WhiteSmoke;
            btnPrimeraPagina.ImageSize = new Size(24, 24);
            btnPrimeraPagina.Location = new Point(11, 1);
            btnPrimeraPagina.Margin = new Padding(1);
            btnPrimeraPagina.Name = "btnPrimeraPagina";
            btnPrimeraPagina.ShadowDecoration.CustomizableEdges = customizableEdges8;
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
            btnPaginaSiguiente.CustomImages.Image = (Image) resources.GetObject("resource.Image2");
            btnPaginaSiguiente.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnPaginaSiguiente.CustomImages.ImageSize = new Size(24, 24);
            btnPaginaSiguiente.CustomizableEdges = customizableEdges9;
            btnPaginaSiguiente.DisabledState.FillColor = Color.White;
            btnPaginaSiguiente.Dock = DockStyle.Fill;
            btnPaginaSiguiente.FillColor = Color.White;
            btnPaginaSiguiente.Font = new Font("Segoe UI", 9F);
            btnPaginaSiguiente.ForeColor = Color.White;
            btnPaginaSiguiente.HoverState.BorderColor = Color.FromArgb(  245,   245,   245);
            btnPaginaSiguiente.HoverState.FillColor = Color.WhiteSmoke;
            btnPaginaSiguiente.ImageSize = new Size(24, 24);
            btnPaginaSiguiente.Location = new Point(321, 1);
            btnPaginaSiguiente.Margin = new Padding(1);
            btnPaginaSiguiente.Name = "btnPaginaSiguiente";
            btnPaginaSiguiente.ShadowDecoration.CustomizableEdges = customizableEdges10;
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
            btnUltimaPagina.CustomImages.Image = Properties.Resources.page_last_24px;
            btnUltimaPagina.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnUltimaPagina.CustomImages.ImageSize = new Size(24, 24);
            btnUltimaPagina.CustomizableEdges = customizableEdges11;
            btnUltimaPagina.DisabledState.FillColor = Color.White;
            btnUltimaPagina.Dock = DockStyle.Fill;
            btnUltimaPagina.FillColor = Color.White;
            btnUltimaPagina.Font = new Font("Segoe UI", 9F);
            btnUltimaPagina.ForeColor = Color.White;
            btnUltimaPagina.HoverState.BorderColor = Color.FromArgb(  245,   245,   245);
            btnUltimaPagina.HoverState.FillColor = Color.WhiteSmoke;
            btnUltimaPagina.ImageSize = new Size(24, 24);
            btnUltimaPagina.Location = new Point(356, 1);
            btnUltimaPagina.Margin = new Padding(1);
            btnUltimaPagina.Name = "btnUltimaPagina";
            btnUltimaPagina.ShadowDecoration.CustomizableEdges = customizableEdges12;
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
            btnSincronizarDatos.CustomImages.Image = (Image) resources.GetObject("resource.Image3");
            btnSincronizarDatos.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnSincronizarDatos.CustomImages.ImageSize = new Size(24, 24);
            btnSincronizarDatos.CustomizableEdges = customizableEdges13;
            btnSincronizarDatos.Dock = DockStyle.Fill;
            btnSincronizarDatos.FillColor = Color.White;
            btnSincronizarDatos.Font = new Font("Segoe UI", 9F);
            btnSincronizarDatos.ForeColor = Color.White;
            btnSincronizarDatos.HoverState.BorderColor = Color.FromArgb(  245,   245,   245);
            btnSincronizarDatos.HoverState.FillColor = Color.WhiteSmoke;
            btnSincronizarDatos.ImageSize = new Size(24, 24);
            btnSincronizarDatos.Location = new Point(1272, 1);
            btnSincronizarDatos.Margin = new Padding(1);
            btnSincronizarDatos.Name = "btnSincronizarDatos";
            btnSincronizarDatos.ShadowDecoration.CustomizableEdges = customizableEdges14;
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
            layoutContenedorVistas.Size = new Size(1316, 279);
            layoutContenedorVistas.TabIndex = 77;
            // 
            // contenedorVistas
            // 
            contenedorVistas.BackColor = Color.White;
            contenedorVistas.Dock = DockStyle.Fill;
            contenedorVistas.Location = new Point(1, 1);
            contenedorVistas.Margin = new Padding(1, 1, 1, 0);
            contenedorVistas.Name = "contenedorVistas";
            contenedorVistas.Size = new Size(1314, 278);
            contenedorVistas.TabIndex = 13;
            // 
            // layoutTotales
            // 
            layoutTotales.ColumnCount = 4;
            layoutTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            layoutTotales.Controls.Add(fieldValorTotalInventario, 3, 0);
            layoutTotales.Dock = DockStyle.Fill;
            layoutTotales.Location = new Point(50, 553);
            layoutTotales.Margin = new Padding(0);
            layoutTotales.Name = "layoutTotales";
            layoutTotales.RowCount = 1;
            layoutTotales.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTotales.Size = new Size(1316, 35);
            layoutTotales.TabIndex = 79;
            // 
            // fieldValorTotalInventario
            // 
            fieldValorTotalInventario.Dock = DockStyle.Fill;
            fieldValorTotalInventario.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldValorTotalInventario.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldValorTotalInventario.ImeMode = ImeMode.NoControl;
            fieldValorTotalInventario.Location = new Point(1117, 1);
            fieldValorTotalInventario.Margin = new Padding(1);
            fieldValorTotalInventario.Name = "fieldValorTotalInventario";
            fieldValorTotalInventario.Size = new Size(198, 33);
            fieldValorTotalInventario.TabIndex = 71;
            fieldValorTotalInventario.Text = "$ 0,00";
            fieldValorTotalInventario.TextAlign = ContentAlignment.MiddleRight;
            // 
            // layoutFiltroHerramientas
            // 
            layoutFiltroHerramientas.Controls.Add(fieldTituloAlmacen);
            layoutFiltroHerramientas.Controls.Add(fieldFiltroAlmacen);
            layoutFiltroHerramientas.Controls.Add(fieldTituloCategoria);
            layoutFiltroHerramientas.Controls.Add(fieldFiltroCategoriaProducto);
            layoutFiltroHerramientas.Controls.Add(fieldTituloFiltroBusqueda);
            layoutFiltroHerramientas.Controls.Add(fieldFiltroBusqueda);
            layoutFiltroHerramientas.Controls.Add(fieldCriterioBusqueda);
            layoutFiltroHerramientas.Controls.Add(btnRegistrar);
            layoutFiltroHerramientas.Dock = DockStyle.Fill;
            layoutFiltroHerramientas.Location = new Point(50, 100);
            layoutFiltroHerramientas.Margin = new Padding(0);
            layoutFiltroHerramientas.Name = "layoutFiltroHerramientas";
            layoutFiltroHerramientas.Size = new Size(1316, 45);
            layoutFiltroHerramientas.TabIndex = 80;
            // 
            // fieldTituloAlmacen
            // 
            fieldTituloAlmacen.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloAlmacen.ForeColor = Color.DimGray;
            fieldTituloAlmacen.ImeMode = ImeMode.NoControl;
            fieldTituloAlmacen.Location = new Point(1, 1);
            fieldTituloAlmacen.Margin = new Padding(1);
            fieldTituloAlmacen.Name = "fieldTituloAlmacen";
            fieldTituloAlmacen.Size = new Size(78, 40);
            fieldTituloAlmacen.TabIndex = 28;
            fieldTituloAlmacen.Text = "ALMACÉN :";
            fieldTituloAlmacen.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldFiltroAlmacen
            // 
            fieldFiltroAlmacen.Animated = true;
            fieldFiltroAlmacen.BackColor = Color.Transparent;
            fieldFiltroAlmacen.BorderColor = Color.Gainsboro;
            fieldFiltroAlmacen.BorderRadius = 16;
            fieldFiltroAlmacen.CustomizableEdges = customizableEdges17;
            fieldFiltroAlmacen.DrawMode = DrawMode.OwnerDrawFixed;
            fieldFiltroAlmacen.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldFiltroAlmacen.FocusedColor = Color.Gainsboro;
            fieldFiltroAlmacen.FocusedState.BorderColor = Color.Gainsboro;
            fieldFiltroAlmacen.Font = new Font("Segoe UI", 11.25F);
            fieldFiltroAlmacen.ForeColor = Color.Black;
            fieldFiltroAlmacen.ItemHeight = 29;
            fieldFiltroAlmacen.Location = new Point(83, 5);
            fieldFiltroAlmacen.Margin = new Padding(3, 5, 3, 5);
            fieldFiltroAlmacen.Name = "fieldFiltroAlmacen";
            fieldFiltroAlmacen.ShadowDecoration.CustomizableEdges = customizableEdges18;
            fieldFiltroAlmacen.Size = new Size(210, 35);
            fieldFiltroAlmacen.TabIndex = 28;
            fieldFiltroAlmacen.TextOffset = new Point(10, 0);
            // 
            // fieldTituloCategoria
            // 
            fieldTituloCategoria.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloCategoria.ForeColor = Color.DimGray;
            fieldTituloCategoria.ImeMode = ImeMode.NoControl;
            fieldTituloCategoria.Location = new Point(297, 1);
            fieldTituloCategoria.Margin = new Padding(1);
            fieldTituloCategoria.Name = "fieldTituloCategoria";
            fieldTituloCategoria.Size = new Size(92, 40);
            fieldTituloCategoria.TabIndex = 29;
            fieldTituloCategoria.Text = "CATEGORÍA :";
            fieldTituloCategoria.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldFiltroCategoriaProducto
            // 
            fieldFiltroCategoriaProducto.Animated = true;
            fieldFiltroCategoriaProducto.BackColor = Color.Transparent;
            fieldFiltroCategoriaProducto.BorderColor = Color.Gainsboro;
            fieldFiltroCategoriaProducto.BorderRadius = 16;
            fieldFiltroCategoriaProducto.CustomizableEdges = customizableEdges19;
            fieldFiltroCategoriaProducto.DrawMode = DrawMode.OwnerDrawFixed;
            fieldFiltroCategoriaProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldFiltroCategoriaProducto.FocusedColor = Color.SandyBrown;
            fieldFiltroCategoriaProducto.FocusedState.BorderColor = Color.SandyBrown;
            fieldFiltroCategoriaProducto.Font = new Font("Segoe UI", 11.25F);
            fieldFiltroCategoriaProducto.ForeColor = Color.Black;
            fieldFiltroCategoriaProducto.ItemHeight = 29;
            fieldFiltroCategoriaProducto.Items.AddRange(new object[] { "Todas las categorías", "Mercancía (Productos revendidos)", "Producto terminado", "Materia prima" });
            fieldFiltroCategoriaProducto.Location = new Point(393, 5);
            fieldFiltroCategoriaProducto.Margin = new Padding(3, 5, 3, 5);
            fieldFiltroCategoriaProducto.Name = "fieldFiltroCategoriaProducto";
            fieldFiltroCategoriaProducto.ShadowDecoration.CustomizableEdges = customizableEdges20;
            fieldFiltroCategoriaProducto.Size = new Size(240, 35);
            fieldFiltroCategoriaProducto.StartIndex = 0;
            fieldFiltroCategoriaProducto.TabIndex = 29;
            fieldFiltroCategoriaProducto.TextOffset = new Point(10, 0);
            // 
            // fieldTituloFiltroBusqueda
            // 
            fieldTituloFiltroBusqueda.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTituloFiltroBusqueda.ForeColor = Color.DimGray;
            fieldTituloFiltroBusqueda.ImeMode = ImeMode.NoControl;
            fieldTituloFiltroBusqueda.Location = new Point(637, 1);
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
            fieldFiltroBusqueda.CustomizableEdges = customizableEdges21;
            fieldFiltroBusqueda.DrawMode = DrawMode.OwnerDrawFixed;
            fieldFiltroBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldFiltroBusqueda.FocusedColor = Color.Gainsboro;
            fieldFiltroBusqueda.FocusedState.BorderColor = Color.Gainsboro;
            fieldFiltroBusqueda.Font = new Font("Segoe UI", 11.25F);
            fieldFiltroBusqueda.ForeColor = Color.Black;
            fieldFiltroBusqueda.ItemHeight = 29;
            fieldFiltroBusqueda.Location = new Point(737, 5);
            fieldFiltroBusqueda.Margin = new Padding(3, 5, 3, 5);
            fieldFiltroBusqueda.Name = "fieldFiltroBusqueda";
            fieldFiltroBusqueda.ShadowDecoration.CustomizableEdges = customizableEdges22;
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
            fieldCriterioBusqueda.CustomizableEdges = customizableEdges23;
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
            fieldCriterioBusqueda.Location = new Point(963, 5);
            fieldCriterioBusqueda.Margin = new Padding(3, 5, 3, 5);
            fieldCriterioBusqueda.Name = "fieldCriterioBusqueda";
            fieldCriterioBusqueda.PasswordChar = '\0';
            fieldCriterioBusqueda.PlaceholderForeColor = Color.DimGray;
            fieldCriterioBusqueda.PlaceholderText = "Criterio de búsqueda";
            fieldCriterioBusqueda.SelectedText = "";
            fieldCriterioBusqueda.ShadowDecoration.CustomizableEdges = customizableEdges24;
            fieldCriterioBusqueda.Size = new Size(220, 35);
            fieldCriterioBusqueda.TabIndex = 30;
            fieldCriterioBusqueda.TextOffset = new Point(5, 0);
            fieldCriterioBusqueda.Visible = false;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.BackColor = Color.White;
            btnRegistrar.BorderRadius = 18;
            btnRegistrar.CustomizableEdges = customizableEdges25;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Image = (Image) resources.GetObject("btnRegistrar.Image");
            btnRegistrar.ImageOffset = new Point(-5, 0);
            btnRegistrar.Location = new Point(3, 50);
            btnRegistrar.Margin = new Padding(3, 5, 3, 5);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges26;
            btnRegistrar.Size = new Size(176, 35);
            btnRegistrar.TabIndex = 7;
            btnRegistrar.Text = "Nuevo producto";
            // 
            // VistaGestionProductos
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1386, 608);
            Controls.Add(layoutVista);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaGestionProductos";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistGestionProveedor";
            layoutVista.ResumeLayout(false);
            layoutTitulosTotales.ResumeLayout(false);
            panelEncabezadosTabla.ResumeLayout(false);
            layoutEncabezadosTabla.ResumeLayout(false);
            layoutTitulo.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            panelControlesTabla.ResumeLayout(false);
            layoutControlesTabla.ResumeLayout(false);
            layoutContenedorVistas.ResumeLayout(false);
            layoutTotales.ResumeLayout(false);
            layoutFiltroHerramientas.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutVista;
        private TableLayoutPanel layoutTitulo;
        private Label fieldTitulo;
        private TableLayoutPanel layoutDistribucionMenu;
        private FlowLayoutPanel layoutBotones;
        private Guna2Button btnRegistrar;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private TableLayoutPanel layoutEncabezadosTabla;
        private Label fieldTituloCodigo;
        private Label fieldTituloId;
        private Label fieldTituloNombre;
        private Label fieldTituloDescripcion;
        private Label fieldTituloPrecioAdquisicion;
        private Label fieldTituloPrecioCesion;
        private Label fieldTituloCantidad;
        private Guna2ComboBox fieldFiltroAlmacen;
        private Guna2ComboBox fieldFiltroCategoriaProducto;
        private Label fieldTituloUnidadMedida;
        private Label fieldFechaUlimoMovimiento;
        private Guna2Button btnGenerarCatalogo;
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
        private Guna2Panel panelEncabezadosTabla;
        private TableLayoutPanel layoutContenedorVistas;
        private Panel contenedorVistas;
        private TableLayoutPanel layoutTitulosTotales;
        private Label fieldTituloValorTotalInventario;
        private Label fieldTituloTotalTransferencias;
        private Label fieldTituloTotalEfectivo;
        private TableLayoutPanel layoutTotales;
        private Label fieldValorTotalInventario;
        private FlowLayoutPanel layoutFiltroHerramientas;
        private Label fieldTituloAlmacen;
        private Label fieldTituloFiltroBusqueda;
        private Guna2ComboBox fieldFiltroBusqueda;
        private Label fieldTituloCategoria;
        private Label fieldTotalTransferencias;
        private Label fieldTotalEfectivo;
        private Guna2TextBox fieldCriterioBusqueda;
    }
}