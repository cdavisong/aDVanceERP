using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.RolUsuario {
    partial class VistaRegistroRolUsuario {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaRegistroRolUsuario));
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
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldNombreModulo = new Guna2ComboBox();
            fieldIcono = new PictureBox();
            fieldTituloNombrePermiso = new Label();
            fieldSubtitulo = new Label();
            fieldNombreRolUsuario = new Guna2TextBox();
            layoutTitulo = new TableLayoutPanel();
            btnCerrar = new Guna2Button();
            fieldTitulo = new Label();
            fieldTituloGestionPermisos = new Label();
            layoutEncabezadosTabla = new TableLayoutPanel();
            fieldTituloProducto = new Label();
            contenedorVistas = new Panel();
            layoutPermiso = new TableLayoutPanel();
            fieldNombrePermiso = new Guna2ComboBox();
            btnAdicionarPermiso = new Guna2Button();
            fieldTituloNombreModulo = new Label();
            separador1 = new Guna2Separator();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna2Button();
            btnRegistrar = new Guna2Button();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutTitulo.SuspendLayout();
            layoutEncabezadosTabla.SuspendLayout();
            layoutPermiso.SuspendLayout();
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
            layoutVista.Controls.Add(fieldNombreModulo, 2, 9);
            layoutVista.Controls.Add(fieldIcono, 1, 1);
            layoutVista.Controls.Add(fieldTituloNombrePermiso, 2, 11);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 2);
            layoutVista.Controls.Add(fieldNombreRolUsuario, 2, 4);
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldTituloGestionPermisos, 2, 6);
            layoutVista.Controls.Add(layoutEncabezadosTabla, 2, 14);
            layoutVista.Controls.Add(contenedorVistas, 2, 15);
            layoutVista.Controls.Add(layoutPermiso, 2, 12);
            layoutVista.Controls.Add(fieldTituloNombreModulo, 2, 8);
            layoutVista.Controls.Add(separador1, 2, 7);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(13, 0);
            layoutVista.Margin = new Padding(3, 0, 0, 0);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 17;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(487, 620);
            layoutVista.TabIndex = 0;
            // 
            // fieldNombreModulo
            // 
            fieldNombreModulo.Animated = true;
            fieldNombreModulo.BackColor = Color.Transparent;
            fieldNombreModulo.BorderColor = Color.Gainsboro;
            fieldNombreModulo.BorderRadius = 16;
            fieldNombreModulo.CustomizableEdges = customizableEdges1;
            fieldNombreModulo.Dock = DockStyle.Fill;
            fieldNombreModulo.DrawMode = DrawMode.OwnerDrawFixed;
            fieldNombreModulo.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldNombreModulo.FocusedColor = Color.SandyBrown;
            fieldNombreModulo.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombreModulo.Font = new Font("Segoe UI", 11.25F);
            fieldNombreModulo.ForeColor = Color.Black;
            fieldNombreModulo.ItemHeight = 29;
            fieldNombreModulo.Location = new Point(55, 280);
            fieldNombreModulo.Margin = new Padding(5);
            fieldNombreModulo.Name = "fieldNombreModulo";
            fieldNombreModulo.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldNombreModulo.Size = new Size(407, 35);
            fieldNombreModulo.TabIndex = 0;
            fieldNombreModulo.TextOffset = new Point(10, 0);
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
            // fieldTituloNombrePermiso
            // 
            fieldTituloNombrePermiso.Dock = DockStyle.Fill;
            fieldTituloNombrePermiso.Font = new Font("Segoe UI", 11.25F);
            fieldTituloNombrePermiso.ForeColor = Color.DimGray;
            fieldTituloNombrePermiso.Image = (Image) resources.GetObject("fieldTituloNombrePermiso.Image");
            fieldTituloNombrePermiso.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloNombrePermiso.ImeMode = ImeMode.NoControl;
            fieldTituloNombrePermiso.Location = new Point(65, 335);
            fieldTituloNombrePermiso.Margin = new Padding(15, 5, 3, 3);
            fieldTituloNombrePermiso.Name = "fieldTituloNombrePermiso";
            fieldTituloNombrePermiso.Size = new Size(399, 27);
            fieldTituloNombrePermiso.TabIndex = 1;
            fieldTituloNombrePermiso.Text = "      Permiso :";
            fieldTituloNombrePermiso.TextAlign = ContentAlignment.MiddleLeft;
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
            // fieldNombreRolUsuario
            // 
            fieldNombreRolUsuario.Animated = true;
            fieldNombreRolUsuario.BorderColor = Color.Gainsboro;
            fieldNombreRolUsuario.BorderRadius = 16;
            fieldNombreRolUsuario.Cursor = Cursors.IBeam;
            fieldNombreRolUsuario.CustomizableEdges = customizableEdges3;
            fieldNombreRolUsuario.DefaultText = "";
            fieldNombreRolUsuario.DisabledState.BorderColor = Color.White;
            fieldNombreRolUsuario.DisabledState.ForeColor = Color.DimGray;
            fieldNombreRolUsuario.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNombreRolUsuario.Dock = DockStyle.Fill;
            fieldNombreRolUsuario.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombreRolUsuario.Font = new Font("Segoe UI", 11.25F);
            fieldNombreRolUsuario.ForeColor = Color.Black;
            fieldNombreRolUsuario.HoverState.BorderColor = Color.SandyBrown;
            fieldNombreRolUsuario.IconLeft = (Image) resources.GetObject("fieldNombreRolUsuario.IconLeft");
            fieldNombreRolUsuario.IconLeftOffset = new Point(10, 0);
            fieldNombreRolUsuario.Location = new Point(55, 135);
            fieldNombreRolUsuario.Margin = new Padding(5);
            fieldNombreRolUsuario.Name = "fieldNombreRolUsuario";
            fieldNombreRolUsuario.PasswordChar = '\0';
            fieldNombreRolUsuario.PlaceholderForeColor = Color.DimGray;
            fieldNombreRolUsuario.PlaceholderText = "Nombre del rol";
            fieldNombreRolUsuario.SelectedText = "";
            fieldNombreRolUsuario.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldNombreRolUsuario.Size = new Size(407, 35);
            fieldNombreRolUsuario.TabIndex = 1;
            fieldNombreRolUsuario.TextOffset = new Point(5, 0);
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
            layoutTitulo.TabIndex = 14;
            // 
            // btnCerrar
            // 
            btnCerrar.Animated = true;
            btnCerrar.AutoRoundedCorners = true;
            btnCerrar.BorderColor = Color.Gray;
            btnCerrar.BorderRadius = 18;
            btnCerrar.CustomizableEdges = customizableEdges5;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.White;
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.Black;
            btnCerrar.HoverState.FillColor = Color.White;
            btnCerrar.Image = (Image) resources.GetObject("btnCerrar.Image");
            btnCerrar.Location = new Point(370, 3);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges6;
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
            fieldTitulo.Text = "Rol de usuario";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloGestionPermisos
            // 
            fieldTituloGestionPermisos.Dock = DockStyle.Fill;
            fieldTituloGestionPermisos.Font = new Font("Segoe UI", 11.25F);
            fieldTituloGestionPermisos.ForeColor = Color.DimGray;
            fieldTituloGestionPermisos.Image = (Image) resources.GetObject("fieldTituloGestionPermisos.Image");
            fieldTituloGestionPermisos.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloGestionPermisos.ImeMode = ImeMode.NoControl;
            fieldTituloGestionPermisos.Location = new Point(65, 190);
            fieldTituloGestionPermisos.Margin = new Padding(15, 5, 3, 3);
            fieldTituloGestionPermisos.Name = "fieldTituloGestionPermisos";
            fieldTituloGestionPermisos.Size = new Size(399, 27);
            fieldTituloGestionPermisos.TabIndex = 15;
            fieldTituloGestionPermisos.Text = "      Gestión para los permisos de rol usuario";
            fieldTituloGestionPermisos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutEncabezadosTabla
            // 
            layoutEncabezadosTabla.BackColor = Color.WhiteSmoke;
            layoutEncabezadosTabla.ColumnCount = 3;
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutEncabezadosTabla.Controls.Add(fieldTituloProducto, 0, 0);
            layoutEncabezadosTabla.Dock = DockStyle.Fill;
            layoutEncabezadosTabla.Location = new Point(51, 421);
            layoutEncabezadosTabla.Margin = new Padding(1);
            layoutEncabezadosTabla.Name = "layoutEncabezadosTabla";
            layoutEncabezadosTabla.RowCount = 1;
            layoutEncabezadosTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.Size = new Size(415, 43);
            layoutEncabezadosTabla.TabIndex = 19;
            // 
            // fieldTituloProducto
            // 
            fieldTituloProducto.Dock = DockStyle.Fill;
            fieldTituloProducto.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloProducto.ForeColor = Color.Black;
            fieldTituloProducto.ImeMode = ImeMode.NoControl;
            fieldTituloProducto.Location = new Point(1, 1);
            fieldTituloProducto.Margin = new Padding(1);
            fieldTituloProducto.Name = "fieldTituloProducto";
            fieldTituloProducto.Size = new Size(353, 41);
            fieldTituloProducto.TabIndex = 0;
            fieldTituloProducto.Text = "Nombre o denominación";
            fieldTituloProducto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // contenedorVistas
            // 
            contenedorVistas.AutoScroll = true;
            contenedorVistas.Dock = DockStyle.Fill;
            contenedorVistas.Location = new Point(50, 465);
            contenedorVistas.Margin = new Padding(0);
            contenedorVistas.Name = "contenedorVistas";
            contenedorVistas.Size = new Size(417, 135);
            contenedorVistas.TabIndex = 20;
            // 
            // layoutPermiso
            // 
            layoutPermiso.ColumnCount = 2;
            layoutPermiso.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPermiso.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutPermiso.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutPermiso.Controls.Add(fieldNombrePermiso, 0, 0);
            layoutPermiso.Controls.Add(btnAdicionarPermiso, 1, 0);
            layoutPermiso.Dock = DockStyle.Fill;
            layoutPermiso.Location = new Point(50, 365);
            layoutPermiso.Margin = new Padding(0);
            layoutPermiso.Name = "layoutPermiso";
            layoutPermiso.RowCount = 1;
            layoutPermiso.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutPermiso.Size = new Size(417, 45);
            layoutPermiso.TabIndex = 18;
            // 
            // fieldNombrePermiso
            // 
            fieldNombrePermiso.Animated = true;
            fieldNombrePermiso.BackColor = Color.Transparent;
            fieldNombrePermiso.BorderColor = Color.Gainsboro;
            fieldNombrePermiso.BorderRadius = 16;
            fieldNombrePermiso.CustomizableEdges = customizableEdges7;
            fieldNombrePermiso.Dock = DockStyle.Fill;
            fieldNombrePermiso.DrawMode = DrawMode.OwnerDrawFixed;
            fieldNombrePermiso.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldNombrePermiso.FocusedColor = Color.SandyBrown;
            fieldNombrePermiso.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombrePermiso.Font = new Font("Segoe UI", 11.25F);
            fieldNombrePermiso.ForeColor = Color.Black;
            fieldNombrePermiso.ItemHeight = 29;
            fieldNombrePermiso.Location = new Point(5, 5);
            fieldNombrePermiso.Margin = new Padding(5);
            fieldNombrePermiso.Name = "fieldNombrePermiso";
            fieldNombrePermiso.ShadowDecoration.CustomizableEdges = customizableEdges8;
            fieldNombrePermiso.Size = new Size(357, 35);
            fieldNombrePermiso.TabIndex = 1;
            fieldNombrePermiso.TextOffset = new Point(10, 0);
            // 
            // btnAdicionarPermiso
            // 
            btnAdicionarPermiso.Animated = true;
            btnAdicionarPermiso.BorderRadius = 18;
            btnAdicionarPermiso.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnAdicionarPermiso.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnAdicionarPermiso.CustomizableEdges = customizableEdges9;
            btnAdicionarPermiso.DialogResult = DialogResult.Cancel;
            btnAdicionarPermiso.Dock = DockStyle.Fill;
            btnAdicionarPermiso.FillColor = Color.PeachPuff;
            btnAdicionarPermiso.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAdicionarPermiso.ForeColor = Color.White;
            btnAdicionarPermiso.Location = new Point(372, 5);
            btnAdicionarPermiso.Margin = new Padding(5);
            btnAdicionarPermiso.Name = "btnAdicionarPermiso";
            btnAdicionarPermiso.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnAdicionarPermiso.Size = new Size(40, 35);
            btnAdicionarPermiso.TabIndex = 3;
            // 
            // fieldTituloNombreModulo
            // 
            fieldTituloNombreModulo.Dock = DockStyle.Fill;
            fieldTituloNombreModulo.Font = new Font("Segoe UI", 11.25F);
            fieldTituloNombreModulo.ForeColor = Color.DimGray;
            fieldTituloNombreModulo.Image = (Image) resources.GetObject("fieldTituloNombreModulo.Image");
            fieldTituloNombreModulo.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloNombreModulo.ImeMode = ImeMode.NoControl;
            fieldTituloNombreModulo.Location = new Point(65, 245);
            fieldTituloNombreModulo.Margin = new Padding(15, 5, 3, 3);
            fieldTituloNombreModulo.Name = "fieldTituloNombreModulo";
            fieldTituloNombreModulo.Size = new Size(399, 27);
            fieldTituloNombreModulo.TabIndex = 0;
            fieldTituloNombreModulo.Text = "      Módulo :";
            fieldTituloNombreModulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // separador1
            // 
            separador1.Dock = DockStyle.Fill;
            separador1.FillColor = Color.FromArgb(  208,   197,   188);
            separador1.Location = new Point(53, 223);
            separador1.Name = "separador1";
            separador1.Size = new Size(411, 14);
            separador1.TabIndex = 16;
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
            btnSalir.CustomizableEdges = customizableEdges11;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.FillColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Gainsboro;
            btnSalir.HoverState.BorderColor = Color.PeachPuff;
            btnSalir.HoverState.FillColor = Color.PeachPuff;
            btnSalir.HoverState.ForeColor = Color.Black;
            btnSalir.Location = new Point(302, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnSalir.Size = new Size(160, 39);
            btnSalir.TabIndex = 14;
            btnSalir.Text = "Salir";
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.BorderRadius = 18;
            btnRegistrar.CustomizableEdges = customizableEdges13;
            btnRegistrar.Dock = DockStyle.Fill;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Location = new Point(53, 3);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnRegistrar.Size = new Size(243, 39);
            btnRegistrar.TabIndex = 15;
            btnRegistrar.Text = "Registrar rol";
            // 
            // VistaRegistroRolUsuario
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroRolUsuario";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroRolUsuario";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutTitulo.ResumeLayout(false);
            layoutEncabezadosTabla.ResumeLayout(false);
            layoutPermiso.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private Guna2TextBox fieldNombreRolUsuario;
        private TableLayoutPanel layoutTitulo;
        private Guna2Button btnCerrar;
        private Label fieldTitulo;
        private TableLayoutPanel layoutBotones;
        private Guna2Button btnSalir;
        private Guna2Button btnRegistrar;
        private Label fieldTituloGestionPermisos;
        private Guna2Separator separador1;
        private TableLayoutPanel layoutTituloModuloPermiso;
        private Label fieldTituloNombreModulo;
        private Label fieldTituloNombrePermiso;
        private TableLayoutPanel layoutPermiso;
        private Guna2ComboBox fieldNombreModulo;
        private Guna2ComboBox fieldNombrePermiso;
        private Guna2Button btnAdicionarPermiso;
        private TableLayoutPanel layoutEncabezadosTabla;
        private Label fieldTituloProducto;
        private Panel contenedorVistas;
    }
}