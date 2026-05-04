using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Seguridad.Vistas {
    partial class VistaTuplaCuentaUsuario {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaCuentaUsuario));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            separador1 = new Guna2Separator();
            layoutVista = new TableLayoutPanel();
            btnAprobarCuentaUsuario = new Guna2Button();
            fieldEstado = new Guna2Button();
            fieldAprobado = new Label();
            fieldEsAdmin = new Label();
            fieldEmail = new Label();
            fieldRol = new Label();
            fieldNombre = new Label();
            btnEliminar = new Guna2Button();
            fieldId = new Label();
            btnEditar = new Guna2Button();
            fieldNombreUsuario = new Label();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
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
            // layoutBase
            // 
            layoutBase.BackColor = Color.White;
            layoutBase.ColumnCount = 1;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBase.Controls.Add(separador1, 0, 1);
            layoutBase.Controls.Add(layoutVista, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 2;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutBase.Size = new Size(1241, 42);
            layoutBase.TabIndex = 1;
            // 
            // separador1
            // 
            separador1.Dock = DockStyle.Fill;
            separador1.FillColor = Color.Gainsboro;
            separador1.Location = new Point(1, 38);
            separador1.Margin = new Padding(1);
            separador1.Name = "separador1";
            separador1.Size = new Size(1239, 3);
            separador1.TabIndex = 74;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 11;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.Controls.Add(btnAprobarCuentaUsuario, 8, 0);
            layoutVista.Controls.Add(fieldEstado, 7, 0);
            layoutVista.Controls.Add(fieldAprobado, 6, 0);
            layoutVista.Controls.Add(fieldEsAdmin, 5, 0);
            layoutVista.Controls.Add(fieldEmail, 4, 0);
            layoutVista.Controls.Add(fieldRol, 3, 0);
            layoutVista.Controls.Add(fieldNombre, 1, 0);
            layoutVista.Controls.Add(btnEliminar, 10, 0);
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(btnEditar, 9, 0);
            layoutVista.Controls.Add(fieldNombreUsuario, 2, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 36);
            layoutVista.TabIndex = 18;
            // 
            // btnAprobarCuentaUsuario
            // 
            btnAprobarCuentaUsuario.Animated = true;
            btnAprobarCuentaUsuario.AutoRoundedCorners = true;
            btnAprobarCuentaUsuario.BorderColor = Color.Gainsboro;
            btnAprobarCuentaUsuario.BorderRadius = 14;
            btnAprobarCuentaUsuario.BorderThickness = 1;
            btnAprobarCuentaUsuario.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage");
            btnAprobarCuentaUsuario.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnAprobarCuentaUsuario.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnAprobarCuentaUsuario.CustomizableEdges = customizableEdges17;
            btnAprobarCuentaUsuario.Dock = DockStyle.Fill;
            btnAprobarCuentaUsuario.FillColor = Color.White;
            btnAprobarCuentaUsuario.Font = new Font("Segoe UI", 9.75F);
            btnAprobarCuentaUsuario.ForeColor = Color.White;
            btnAprobarCuentaUsuario.HoverState.BorderColor = Color.PeachPuff;
            btnAprobarCuentaUsuario.HoverState.FillColor = Color.PeachPuff;
            btnAprobarCuentaUsuario.Location = new Point(1133, 3);
            btnAprobarCuentaUsuario.Name = "btnAprobarCuentaUsuario";
            btnAprobarCuentaUsuario.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnAprobarCuentaUsuario.Size = new Size(31, 30);
            btnAprobarCuentaUsuario.TabIndex = 45;
            // 
            // fieldEstado
            // 
            fieldEstado.AutoRoundedCorners = true;
            fieldEstado.BorderColor = Color.Gainsboro;
            fieldEstado.BorderRadius = 11;
            fieldEstado.BorderThickness = 1;
            fieldEstado.CustomizableEdges = customizableEdges19;
            fieldEstado.DisabledState.BorderColor = Color.Gainsboro;
            fieldEstado.DisabledState.CustomBorderColor = Color.Gainsboro;
            fieldEstado.DisabledState.FillColor = Color.Gainsboro;
            fieldEstado.DisabledState.ForeColor = Color.DimGray;
            fieldEstado.Dock = DockStyle.Left;
            fieldEstado.Enabled = false;
            fieldEstado.FillColor = Color.Gainsboro;
            fieldEstado.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldEstado.ForeColor = Color.DimGray;
            fieldEstado.HoverState.BorderColor = Color.PeachPuff;
            fieldEstado.HoverState.FillColor = Color.PeachPuff;
            fieldEstado.HoverState.ForeColor = Color.Black;
            fieldEstado.Location = new Point(1016, 6);
            fieldEstado.Margin = new Padding(6);
            fieldEstado.Name = "fieldEstado";
            fieldEstado.ShadowDecoration.CustomizableEdges = customizableEdges20;
            fieldEstado.Size = new Size(108, 24);
            fieldEstado.TabIndex = 44;
            fieldEstado.Text = "● estado";
            fieldEstado.TextOffset = new Point(0, -1);
            // 
            // fieldAprobado
            // 
            fieldAprobado.Dock = DockStyle.Fill;
            fieldAprobado.Font = new Font("Segoe UI", 11.25F);
            fieldAprobado.ForeColor = Color.DimGray;
            fieldAprobado.ImeMode = ImeMode.NoControl;
            fieldAprobado.Location = new Point(911, 1);
            fieldAprobado.Margin = new Padding(1);
            fieldAprobado.Name = "fieldAprobado";
            fieldAprobado.Size = new Size(98, 34);
            fieldAprobado.TabIndex = 19;
            fieldAprobado.Text = "No";
            fieldAprobado.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldEsAdmin
            // 
            fieldEsAdmin.Dock = DockStyle.Fill;
            fieldEsAdmin.Font = new Font("Segoe UI", 11.25F);
            fieldEsAdmin.ForeColor = Color.DimGray;
            fieldEsAdmin.ImeMode = ImeMode.NoControl;
            fieldEsAdmin.Location = new Point(811, 1);
            fieldEsAdmin.Margin = new Padding(1);
            fieldEsAdmin.Name = "fieldEsAdmin";
            fieldEsAdmin.Size = new Size(98, 34);
            fieldEsAdmin.TabIndex = 18;
            fieldEsAdmin.Text = "No";
            fieldEsAdmin.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldEmail
            // 
            fieldEmail.AutoEllipsis = true;
            fieldEmail.Dock = DockStyle.Fill;
            fieldEmail.Font = new Font("Segoe UI", 11.25F);
            fieldEmail.ForeColor = Color.DimGray;
            fieldEmail.ImeMode = ImeMode.NoControl;
            fieldEmail.Location = new Point(651, 1);
            fieldEmail.Margin = new Padding(1);
            fieldEmail.Name = "fieldEmail";
            fieldEmail.Size = new Size(158, 34);
            fieldEmail.TabIndex = 16;
            fieldEmail.Text = "email";
            fieldEmail.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldRol
            // 
            fieldRol.Dock = DockStyle.Fill;
            fieldRol.Font = new Font("Segoe UI", 11.25F);
            fieldRol.ForeColor = Color.DimGray;
            fieldRol.ImeMode = ImeMode.NoControl;
            fieldRol.Location = new Point(451, 1);
            fieldRol.Margin = new Padding(1);
            fieldRol.Name = "fieldRol";
            fieldRol.Size = new Size(198, 34);
            fieldRol.TabIndex = 15;
            fieldRol.Text = "rol";
            fieldRol.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldNombre
            // 
            fieldNombre.AutoEllipsis = true;
            fieldNombre.Dock = DockStyle.Fill;
            fieldNombre.Font = new Font("Segoe UI", 11.25F);
            fieldNombre.ForeColor = Color.Black;
            fieldNombre.ImeMode = ImeMode.NoControl;
            fieldNombre.Location = new Point(61, 1);
            fieldNombre.Margin = new Padding(1);
            fieldNombre.Name = "fieldNombre";
            fieldNombre.Size = new Size(238, 34);
            fieldNombre.TabIndex = 14;
            fieldNombre.Text = "nombre";
            fieldNombre.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnEliminar
            // 
            btnEliminar.Animated = true;
            btnEliminar.AutoRoundedCorners = true;
            btnEliminar.BorderColor = Color.Gainsboro;
            btnEliminar.BorderRadius = 14;
            btnEliminar.BorderThickness = 1;
            btnEliminar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage1");
            btnEliminar.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnEliminar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEliminar.CustomizableEdges = customizableEdges21;
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.FillColor = Color.White;
            btnEliminar.Font = new Font("Segoe UI", 9.75F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.HoverState.BorderColor = Color.PeachPuff;
            btnEliminar.HoverState.FillColor = Color.PeachPuff;
            btnEliminar.HoverState.ForeColor = Color.White;
            btnEliminar.Location = new Point(1207, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges22;
            btnEliminar.Size = new Size(31, 30);
            btnEliminar.TabIndex = 11;
            // 
            // fieldId
            // 
            fieldId.Dock = DockStyle.Left;
            fieldId.Font = new Font("Segoe UI", 11.25F);
            fieldId.ForeColor = Color.DimGray;
            fieldId.ImeMode = ImeMode.NoControl;
            fieldId.Location = new Point(1, 1);
            fieldId.Margin = new Padding(1);
            fieldId.Name = "fieldId";
            fieldId.Size = new Size(58, 34);
            fieldId.TabIndex = 13;
            fieldId.Text = "id";
            fieldId.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnEditar
            // 
            btnEditar.Animated = true;
            btnEditar.AutoRoundedCorners = true;
            btnEditar.BorderColor = Color.Gainsboro;
            btnEditar.BorderRadius = 14;
            btnEditar.BorderThickness = 1;
            btnEditar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage2");
            btnEditar.CustomImages.Image = (Image) resources.GetObject("resource.Image2");
            btnEditar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEditar.CustomizableEdges = customizableEdges23;
            btnEditar.Dock = DockStyle.Fill;
            btnEditar.FillColor = Color.White;
            btnEditar.Font = new Font("Segoe UI", 9.75F);
            btnEditar.ForeColor = Color.White;
            btnEditar.HoverState.BorderColor = Color.PeachPuff;
            btnEditar.HoverState.FillColor = Color.PeachPuff;
            btnEditar.Location = new Point(1170, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges24;
            btnEditar.Size = new Size(31, 30);
            btnEditar.TabIndex = 9;
            // 
            // fieldNombreUsuario
            // 
            fieldNombreUsuario.Dock = DockStyle.Left;
            fieldNombreUsuario.Font = new Font("Segoe UI", 11.25F);
            fieldNombreUsuario.ForeColor = Color.DimGray;
            fieldNombreUsuario.ImeMode = ImeMode.NoControl;
            fieldNombreUsuario.Location = new Point(301, 1);
            fieldNombreUsuario.Margin = new Padding(1);
            fieldNombreUsuario.Name = "fieldNombreUsuario";
            fieldNombreUsuario.Size = new Size(148, 34);
            fieldNombreUsuario.TabIndex = 4;
            fieldNombreUsuario.Text = "nombreUsuario";
            fieldNombreUsuario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaTuplaCuentaUsuario
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaCuentaUsuario";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaCuentaUsuario";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Guna2Button btnEliminar;
        private Label fieldNombreUsuario;
        private Label fieldId;
        private Guna2Button btnEditar;
        private Guna2Separator separador1;
        private Label fieldNombre;
        private Label fieldEmail;
        private Label fieldRol;
        private Label fieldAprobado;
        private Label fieldEsAdmin;
        private Guna2Button fieldEstado;
        private Guna2Button btnAprobarCuentaUsuario;
    }
}