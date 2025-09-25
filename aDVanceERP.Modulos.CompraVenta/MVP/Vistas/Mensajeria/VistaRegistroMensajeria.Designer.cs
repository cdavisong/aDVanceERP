using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Mensajeria {
    partial class VistaRegistroMensajeria {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaRegistroMensajeria));
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
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            layoutTitulo = new TableLayoutPanel();
            btnCerrar = new Guna2Button();
            fieldTitulo = new Label();
            fieldDireccion = new Guna2TextBox();
            fieldTituloNombreMensajero = new Label();
            fieldTituloTipoEntrega = new Label();
            fieldTipoEntrega = new Guna2ComboBox();
            fieldDescripcionTipoEntrega = new Label();
            fieldObservaciones = new Guna2TextBox();
            layoutGestionCliente = new TableLayoutPanel();
            btnAdicionarCliente = new Guna2Button();
            fieldRazonSocialCliente = new Guna2TextBox();
            layoutGestionMensajero = new TableLayoutPanel();
            btnAdicionarMensajero = new Guna2Button();
            fieldNombreMensajero = new Guna2ComboBox();
            separador1 = new Guna2Separator();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna2Button();
            btnRegistrar = new Guna2Button();
            panelResumenEntrega = new Panel();
            fieldResumenEntrega = new Guna2HtmlLabel();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutTitulo.SuspendLayout();
            layoutGestionCliente.SuspendLayout();
            layoutGestionMensajero.SuspendLayout();
            layoutBotones.SuspendLayout();
            panelResumenEntrega.SuspendLayout();
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
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 420F));
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBase.Controls.Add(layoutVista, 1, 0);
            layoutBase.Controls.Add(layoutBotones, 1, 1);
            layoutBase.Controls.Add(panelResumenEntrega, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 2;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            layoutBase.Size = new Size(910, 685);
            layoutBase.TabIndex = 0;
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
            layoutVista.Controls.Add(fieldDireccion, 2, 13);
            layoutVista.Controls.Add(fieldTituloNombreMensajero, 2, 4);
            layoutVista.Controls.Add(fieldTituloTipoEntrega, 2, 7);
            layoutVista.Controls.Add(fieldTipoEntrega, 2, 8);
            layoutVista.Controls.Add(fieldDescripcionTipoEntrega, 2, 9);
            layoutVista.Controls.Add(fieldObservaciones, 2, 15);
            layoutVista.Controls.Add(layoutGestionCliente, 2, 11);
            layoutVista.Controls.Add(layoutGestionMensajero, 2, 5);
            layoutVista.Controls.Add(separador1, 2, 10);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(423, 0);
            layoutVista.Margin = new Padding(3, 0, 0, 0);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 17;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
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
            fieldSubtitulo.TabIndex = 1;
            fieldSubtitulo.Text = "Registro";
            // 
            // layoutTitulo
            // 
            layoutTitulo.ColumnCount = 2;
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutTitulo.Controls.Add(btnCerrar, 1, 0);
            layoutTitulo.Controls.Add(fieldTitulo, 0, 0);
            layoutTitulo.Dock = DockStyle.Fill;
            layoutTitulo.Location = new Point(50, 20);
            layoutTitulo.Margin = new Padding(0);
            layoutTitulo.Name = "layoutTitulo";
            layoutTitulo.RowCount = 1;
            layoutTitulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulo.Size = new Size(417, 45);
            layoutTitulo.TabIndex = 0;
            // 
            // btnCerrar
            // 
            btnCerrar.Animated = true;
            btnCerrar.AutoRoundedCorners = true;
            btnCerrar.BorderColor = Color.Gray;
            btnCerrar.BorderRadius = 18;
            btnCerrar.CustomizableEdges = customizableEdges1;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.White;
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.Black;
            btnCerrar.HoverState.FillColor = Color.White;
            btnCerrar.Image = (Image) resources.GetObject("btnCerrar.Image");
            btnCerrar.Location = new Point(370, 3);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCerrar.Size = new Size(44, 39);
            btnCerrar.TabIndex = 1;
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
            fieldTitulo.Text = "Mensajería";
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
            fieldDireccion.Location = new Point(55, 416);
            fieldDireccion.Margin = new Padding(5);
            fieldDireccion.Multiline = true;
            fieldDireccion.Name = "fieldDireccion";
            fieldDireccion.PasswordChar = '\0';
            fieldDireccion.PlaceholderForeColor = Color.DimGray;
            fieldDireccion.PlaceholderText = "Dirección";
            fieldDireccion.SelectedText = "";
            fieldDireccion.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldDireccion.Size = new Size(407, 62);
            fieldDireccion.TabIndex = 9;
            fieldDireccion.TextOffset = new Point(5, 0);
            // 
            // fieldTituloNombreMensajero
            // 
            fieldTituloNombreMensajero.Dock = DockStyle.Fill;
            fieldTituloNombreMensajero.Font = new Font("Segoe UI", 11.25F);
            fieldTituloNombreMensajero.ForeColor = Color.DimGray;
            fieldTituloNombreMensajero.Image = (Image) resources.GetObject("fieldTituloNombreMensajero.Image");
            fieldTituloNombreMensajero.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloNombreMensajero.ImeMode = ImeMode.NoControl;
            fieldTituloNombreMensajero.Location = new Point(65, 135);
            fieldTituloNombreMensajero.Margin = new Padding(15, 5, 3, 3);
            fieldTituloNombreMensajero.Name = "fieldTituloNombreMensajero";
            fieldTituloNombreMensajero.Size = new Size(399, 27);
            fieldTituloNombreMensajero.TabIndex = 2;
            fieldTituloNombreMensajero.Text = "      Mensajero :";
            fieldTituloNombreMensajero.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloTipoEntrega
            // 
            fieldTituloTipoEntrega.Dock = DockStyle.Fill;
            fieldTituloTipoEntrega.Font = new Font("Segoe UI", 11.25F);
            fieldTituloTipoEntrega.ForeColor = Color.DimGray;
            fieldTituloTipoEntrega.Image = (Image) resources.GetObject("fieldTituloTipoEntrega.Image");
            fieldTituloTipoEntrega.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloTipoEntrega.ImeMode = ImeMode.NoControl;
            fieldTituloTipoEntrega.Location = new Point(65, 225);
            fieldTituloTipoEntrega.Margin = new Padding(15, 5, 3, 3);
            fieldTituloTipoEntrega.Name = "fieldTituloTipoEntrega";
            fieldTituloTipoEntrega.Size = new Size(399, 27);
            fieldTituloTipoEntrega.TabIndex = 4;
            fieldTituloTipoEntrega.Text = "      Tipo de entrega :";
            fieldTituloTipoEntrega.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTipoEntrega
            // 
            fieldTipoEntrega.Animated = true;
            fieldTipoEntrega.BackColor = Color.Transparent;
            fieldTipoEntrega.BorderColor = Color.Gainsboro;
            fieldTipoEntrega.BorderRadius = 16;
            fieldTipoEntrega.CustomizableEdges = customizableEdges5;
            fieldTipoEntrega.Dock = DockStyle.Fill;
            fieldTipoEntrega.DrawMode = DrawMode.OwnerDrawFixed;
            fieldTipoEntrega.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldTipoEntrega.FocusedColor = Color.SandyBrown;
            fieldTipoEntrega.FocusedState.BorderColor = Color.SandyBrown;
            fieldTipoEntrega.Font = new Font("Segoe UI", 11.25F);
            fieldTipoEntrega.ForeColor = Color.Black;
            fieldTipoEntrega.ItemHeight = 29;
            fieldTipoEntrega.Location = new Point(55, 260);
            fieldTipoEntrega.Margin = new Padding(5);
            fieldTipoEntrega.Name = "fieldTipoEntrega";
            fieldTipoEntrega.ShadowDecoration.CustomizableEdges = customizableEdges6;
            fieldTipoEntrega.Size = new Size(407, 35);
            fieldTipoEntrega.TabIndex = 5;
            fieldTipoEntrega.TextOffset = new Point(10, 0);
            // 
            // fieldDescripcionTipoEntrega
            // 
            fieldDescripcionTipoEntrega.Dock = DockStyle.Fill;
            fieldDescripcionTipoEntrega.Font = new Font("Segoe UI", 11.25F);
            fieldDescripcionTipoEntrega.ForeColor = Color.Black;
            fieldDescripcionTipoEntrega.ImeMode = ImeMode.NoControl;
            fieldDescripcionTipoEntrega.Location = new Point(60, 305);
            fieldDescripcionTipoEntrega.Margin = new Padding(10, 5, 10, 1);
            fieldDescripcionTipoEntrega.Name = "fieldDescripcionTipoEntrega";
            fieldDescripcionTipoEntrega.Size = new Size(397, 30);
            fieldDescripcionTipoEntrega.TabIndex = 6;
            fieldDescripcionTipoEntrega.Text = "Seleccione un tipo de entrega";
            // 
            // fieldObservaciones
            // 
            fieldObservaciones.Animated = true;
            fieldObservaciones.BorderColor = Color.Gainsboro;
            fieldObservaciones.BorderRadius = 16;
            fieldObservaciones.Cursor = Cursors.IBeam;
            fieldObservaciones.CustomizableEdges = customizableEdges7;
            fieldObservaciones.DefaultText = "";
            fieldObservaciones.DisabledState.BorderColor = Color.White;
            fieldObservaciones.DisabledState.ForeColor = Color.DimGray;
            fieldObservaciones.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldObservaciones.Dock = DockStyle.Fill;
            fieldObservaciones.FocusedState.BorderColor = Color.SandyBrown;
            fieldObservaciones.Font = new Font("Segoe UI", 11.25F);
            fieldObservaciones.ForeColor = Color.Black;
            fieldObservaciones.HoverState.BorderColor = Color.SandyBrown;
            fieldObservaciones.IconLeft = (Image) resources.GetObject("fieldObservaciones.IconLeft");
            fieldObservaciones.IconLeftOffset = new Point(10, -11);
            fieldObservaciones.Location = new Point(55, 498);
            fieldObservaciones.Margin = new Padding(5);
            fieldObservaciones.Multiline = true;
            fieldObservaciones.Name = "fieldObservaciones";
            fieldObservaciones.PasswordChar = '\0';
            fieldObservaciones.PlaceholderForeColor = Color.DimGray;
            fieldObservaciones.PlaceholderText = "Observaciones";
            fieldObservaciones.SelectedText = "";
            fieldObservaciones.ShadowDecoration.CustomizableEdges = customizableEdges8;
            fieldObservaciones.Size = new Size(407, 62);
            fieldObservaciones.TabIndex = 10;
            fieldObservaciones.TextOffset = new Point(5, 0);
            // 
            // layoutGestionCliente
            // 
            layoutGestionCliente.ColumnCount = 2;
            layoutGestionCliente.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutGestionCliente.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutGestionCliente.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutGestionCliente.Controls.Add(btnAdicionarCliente, 1, 0);
            layoutGestionCliente.Controls.Add(fieldRazonSocialCliente, 0, 0);
            layoutGestionCliente.Dock = DockStyle.Fill;
            layoutGestionCliente.Location = new Point(50, 356);
            layoutGestionCliente.Margin = new Padding(0);
            layoutGestionCliente.Name = "layoutGestionCliente";
            layoutGestionCliente.RowCount = 1;
            layoutGestionCliente.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutGestionCliente.Size = new Size(417, 45);
            layoutGestionCliente.TabIndex = 8;
            // 
            // btnAdicionarCliente
            // 
            btnAdicionarCliente.Animated = true;
            btnAdicionarCliente.BorderRadius = 18;
            btnAdicionarCliente.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnAdicionarCliente.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnAdicionarCliente.CustomizableEdges = customizableEdges9;
            btnAdicionarCliente.DialogResult = DialogResult.Cancel;
            btnAdicionarCliente.Dock = DockStyle.Fill;
            btnAdicionarCliente.FillColor = Color.PeachPuff;
            btnAdicionarCliente.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAdicionarCliente.ForeColor = Color.White;
            btnAdicionarCliente.Location = new Point(372, 5);
            btnAdicionarCliente.Margin = new Padding(5);
            btnAdicionarCliente.Name = "btnAdicionarCliente";
            btnAdicionarCliente.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnAdicionarCliente.Size = new Size(40, 35);
            btnAdicionarCliente.TabIndex = 1;
            // 
            // fieldRazonSocialCliente
            // 
            fieldRazonSocialCliente.Animated = true;
            fieldRazonSocialCliente.BorderColor = Color.Gainsboro;
            fieldRazonSocialCliente.BorderRadius = 16;
            fieldRazonSocialCliente.Cursor = Cursors.IBeam;
            fieldRazonSocialCliente.CustomizableEdges = customizableEdges11;
            fieldRazonSocialCliente.DefaultText = "";
            fieldRazonSocialCliente.DisabledState.BorderColor = Color.Gainsboro;
            fieldRazonSocialCliente.DisabledState.FillColor = Color.White;
            fieldRazonSocialCliente.DisabledState.ForeColor = Color.DimGray;
            fieldRazonSocialCliente.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldRazonSocialCliente.Dock = DockStyle.Fill;
            fieldRazonSocialCliente.FocusedState.BorderColor = Color.SandyBrown;
            fieldRazonSocialCliente.Font = new Font("Segoe UI", 11.25F);
            fieldRazonSocialCliente.ForeColor = Color.Black;
            fieldRazonSocialCliente.HoverState.BorderColor = Color.SandyBrown;
            fieldRazonSocialCliente.IconLeft = (Image) resources.GetObject("fieldRazonSocialCliente.IconLeft");
            fieldRazonSocialCliente.IconLeftOffset = new Point(10, 0);
            fieldRazonSocialCliente.Location = new Point(5, 5);
            fieldRazonSocialCliente.Margin = new Padding(5);
            fieldRazonSocialCliente.Name = "fieldRazonSocialCliente";
            fieldRazonSocialCliente.PasswordChar = '\0';
            fieldRazonSocialCliente.PlaceholderForeColor = Color.DimGray;
            fieldRazonSocialCliente.PlaceholderText = "Nombre del cliente";
            fieldRazonSocialCliente.SelectedText = "";
            fieldRazonSocialCliente.ShadowDecoration.CustomizableEdges = customizableEdges12;
            fieldRazonSocialCliente.Size = new Size(357, 35);
            fieldRazonSocialCliente.TabIndex = 0;
            fieldRazonSocialCliente.TextOffset = new Point(5, 0);
            // 
            // layoutGestionMensajero
            // 
            layoutGestionMensajero.ColumnCount = 2;
            layoutGestionMensajero.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutGestionMensajero.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutGestionMensajero.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutGestionMensajero.Controls.Add(btnAdicionarMensajero, 1, 0);
            layoutGestionMensajero.Controls.Add(fieldNombreMensajero, 0, 0);
            layoutGestionMensajero.Dock = DockStyle.Fill;
            layoutGestionMensajero.Location = new Point(50, 165);
            layoutGestionMensajero.Margin = new Padding(0);
            layoutGestionMensajero.Name = "layoutGestionMensajero";
            layoutGestionMensajero.RowCount = 1;
            layoutGestionMensajero.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutGestionMensajero.Size = new Size(417, 45);
            layoutGestionMensajero.TabIndex = 3;
            // 
            // btnAdicionarMensajero
            // 
            btnAdicionarMensajero.Animated = true;
            btnAdicionarMensajero.BorderRadius = 18;
            btnAdicionarMensajero.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnAdicionarMensajero.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnAdicionarMensajero.CustomizableEdges = customizableEdges13;
            btnAdicionarMensajero.DialogResult = DialogResult.Cancel;
            btnAdicionarMensajero.Dock = DockStyle.Fill;
            btnAdicionarMensajero.FillColor = Color.PeachPuff;
            btnAdicionarMensajero.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAdicionarMensajero.ForeColor = Color.White;
            btnAdicionarMensajero.Location = new Point(372, 5);
            btnAdicionarMensajero.Margin = new Padding(5);
            btnAdicionarMensajero.Name = "btnAdicionarMensajero";
            btnAdicionarMensajero.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnAdicionarMensajero.Size = new Size(40, 35);
            btnAdicionarMensajero.TabIndex = 1;
            // 
            // fieldNombreMensajero
            // 
            fieldNombreMensajero.Animated = true;
            fieldNombreMensajero.BackColor = Color.Transparent;
            fieldNombreMensajero.BorderColor = Color.Gainsboro;
            fieldNombreMensajero.BorderRadius = 16;
            fieldNombreMensajero.CustomizableEdges = customizableEdges15;
            fieldNombreMensajero.Dock = DockStyle.Fill;
            fieldNombreMensajero.DrawMode = DrawMode.OwnerDrawFixed;
            fieldNombreMensajero.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldNombreMensajero.FocusedColor = Color.SandyBrown;
            fieldNombreMensajero.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombreMensajero.Font = new Font("Segoe UI", 11.25F);
            fieldNombreMensajero.ForeColor = Color.Black;
            fieldNombreMensajero.ItemHeight = 29;
            fieldNombreMensajero.Location = new Point(5, 5);
            fieldNombreMensajero.Margin = new Padding(5);
            fieldNombreMensajero.Name = "fieldNombreMensajero";
            fieldNombreMensajero.ShadowDecoration.CustomizableEdges = customizableEdges16;
            fieldNombreMensajero.Size = new Size(357, 35);
            fieldNombreMensajero.TabIndex = 0;
            fieldNombreMensajero.TextOffset = new Point(10, 0);
            // 
            // separador1
            // 
            separador1.Dock = DockStyle.Fill;
            separador1.FillColor = Color.FromArgb(  208,   197,   188);
            separador1.Location = new Point(53, 339);
            separador1.Name = "separador1";
            separador1.Size = new Size(411, 14);
            separador1.TabIndex = 7;
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
            layoutBotones.Location = new Point(423, 620);
            layoutBotones.Margin = new Padding(3, 0, 0, 0);
            layoutBotones.Name = "layoutBotones";
            layoutBotones.RowCount = 2;
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutBotones.Size = new Size(487, 65);
            layoutBotones.TabIndex = 1;
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
            btnSalir.Location = new Point(302, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnSalir.Size = new Size(160, 39);
            btnSalir.TabIndex = 1;
            btnSalir.Text = "Salir";
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.BorderRadius = 18;
            btnRegistrar.CustomizableEdges = customizableEdges19;
            btnRegistrar.Dock = DockStyle.Fill;
            btnRegistrar.Enabled = false;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Location = new Point(53, 3);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges20;
            btnRegistrar.Size = new Size(243, 39);
            btnRegistrar.TabIndex = 0;
            btnRegistrar.Text = "Asignar mensajería";
            // 
            // panelResumenEntrega
            // 
            panelResumenEntrega.AutoScroll = true;
            panelResumenEntrega.Controls.Add(fieldResumenEntrega);
            panelResumenEntrega.Dock = DockStyle.Fill;
            panelResumenEntrega.Location = new Point(30, 20);
            panelResumenEntrega.Margin = new Padding(30, 20, 3, 3);
            panelResumenEntrega.Name = "panelResumenEntrega";
            panelResumenEntrega.Size = new Size(387, 597);
            panelResumenEntrega.TabIndex = 29;
            // 
            // fieldResumenEntrega
            // 
            fieldResumenEntrega.AutoSize = false;
            fieldResumenEntrega.AutoSizeHeightOnly = true;
            fieldResumenEntrega.BackColor = Color.Transparent;
            fieldResumenEntrega.Dock = DockStyle.Top;
            fieldResumenEntrega.Location = new Point(0, 0);
            fieldResumenEntrega.Margin = new Padding(10, 3, 10, 3);
            fieldResumenEntrega.Name = "fieldResumenEntrega";
            fieldResumenEntrega.Size = new Size(387, 1);
            fieldResumenEntrega.TabIndex = 0;
            fieldResumenEntrega.Text = null;
            // 
            // VistaRegistroMensajeria
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(910, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroMensajeria";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroMensajeria";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutTitulo.ResumeLayout(false);
            layoutGestionCliente.ResumeLayout(false);
            layoutGestionMensajero.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            panelResumenEntrega.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private TableLayoutPanel layoutTitulo;
        private Guna2Button btnCerrar;
        private Label fieldTitulo;
        private TableLayoutPanel layoutBotones;
        private Guna2Button btnSalir;
        private Guna2Button btnRegistrar;
        private Guna2TextBox fieldDireccion;
        private Label fieldTituloNombreMensajero;
        private Guna2ComboBox fieldNombreMensajero;
        private Label fieldTituloTipoEntrega;
        private Guna2ComboBox fieldTipoEntrega;
        private Label fieldDescripcionTipoEntrega;
        private Panel panelResumenEntrega;
        private Guna2HtmlLabel fieldResumenEntrega;
        private Guna2TextBox fieldObservaciones;
        private TableLayoutPanel layoutGestionCliente;
        private Guna2Button btnAdicionarCliente;
        private Guna2TextBox fieldRazonSocialCliente;
        private TableLayoutPanel layoutGestionMensajero;
        private Guna2Button btnAdicionarMensajero;
        private Guna2Separator separador1;
    }
}