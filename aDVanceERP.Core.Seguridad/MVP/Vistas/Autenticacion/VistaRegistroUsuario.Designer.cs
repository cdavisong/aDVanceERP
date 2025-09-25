using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion {
    partial class VistaRegistroUsuario {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaRegistroUsuario));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutVista = new TableLayoutPanel();
            fieldTitulo = new Label();
            fieldCopyright = new Label();
            fieldNombreUsuario = new Guna2TextBox();
            fieldPassword = new Guna2TextBox();
            btnRegistrarCuentaUsuario = new Guna2Button();
            btnRegresarAutenticar = new Guna2Button();
            fieldConfirmarPassword = new Guna2TextBox();
            layoutTerminosServicio = new TableLayoutPanel();
            fieldTextoAceptacionTerminosServicio = new Label();
            fieldAceptacionTerminosServicio = new Guna2CheckBox();
            layoutVista.SuspendLayout();
            layoutTerminosServicio.SuspendLayout();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.AnimationType = Guna2BorderlessForm.AnimateWindowType.AW_HOR_NEGATIVE;
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
            formatoBase.DragForm = false;
            formatoBase.HasFormShadow = false;
            formatoBase.TransparentWhileDrag = true;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.FromArgb(  250,   250,   250);
            layoutVista.ColumnCount = 3;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(fieldTitulo, 1, 1);
            layoutVista.Controls.Add(fieldCopyright, 1, 15);
            layoutVista.Controls.Add(fieldNombreUsuario, 1, 3);
            layoutVista.Controls.Add(fieldPassword, 1, 5);
            layoutVista.Controls.Add(btnRegistrarCuentaUsuario, 1, 11);
            layoutVista.Controls.Add(btnRegresarAutenticar, 1, 13);
            layoutVista.Controls.Add(fieldConfirmarPassword, 1, 7);
            layoutVista.Controls.Add(layoutTerminosServicio, 1, 9);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 17;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 78F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(500, 685);
            layoutVista.TabIndex = 0;
            // 
            // fieldTitulo
            // 
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(23, 20);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(454, 80);
            fieldTitulo.TabIndex = 0;
            fieldTitulo.Text = "Crea una cuenta de usuario";
            fieldTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldCopyright
            // 
            fieldCopyright.Dock = DockStyle.Fill;
            fieldCopyright.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            fieldCopyright.ForeColor = Color.DarkGray;
            fieldCopyright.ImeMode = ImeMode.NoControl;
            fieldCopyright.Location = new Point(23, 584);
            fieldCopyright.Name = "fieldCopyright";
            fieldCopyright.Size = new Size(454, 80);
            fieldCopyright.TabIndex = 7;
            fieldCopyright.Text = "Copyright 2025© aDVance ERP®";
            fieldCopyright.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldNombreUsuario
            // 
            fieldNombreUsuario.Animated = true;
            fieldNombreUsuario.BorderColor = Color.Gainsboro;
            fieldNombreUsuario.BorderRadius = 16;
            fieldNombreUsuario.Cursor = Cursors.IBeam;
            fieldNombreUsuario.CustomizableEdges = customizableEdges1;
            fieldNombreUsuario.DefaultText = "";
            fieldNombreUsuario.DisabledState.BorderColor = Color.White;
            fieldNombreUsuario.DisabledState.ForeColor = Color.DimGray;
            fieldNombreUsuario.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNombreUsuario.Dock = DockStyle.Fill;
            fieldNombreUsuario.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombreUsuario.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldNombreUsuario.ForeColor = Color.Black;
            fieldNombreUsuario.HoverState.BorderColor = Color.SandyBrown;
            fieldNombreUsuario.IconLeft = (Image) resources.GetObject("fieldNombreUsuario.IconLeft");
            fieldNombreUsuario.IconLeftOffset = new Point(10, 0);
            fieldNombreUsuario.Location = new Point(25, 125);
            fieldNombreUsuario.Margin = new Padding(5);
            fieldNombreUsuario.Name = "fieldNombreUsuario";
            fieldNombreUsuario.PasswordChar = '\0';
            fieldNombreUsuario.PlaceholderForeColor = Color.DimGray;
            fieldNombreUsuario.PlaceholderText = "Nombre de usuario";
            fieldNombreUsuario.SelectedText = "";
            fieldNombreUsuario.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldNombreUsuario.Size = new Size(450, 35);
            fieldNombreUsuario.TabIndex = 1;
            fieldNombreUsuario.TextOffset = new Point(5, 0);
            // 
            // fieldPassword
            // 
            fieldPassword.Animated = true;
            fieldPassword.BorderColor = Color.Gainsboro;
            fieldPassword.BorderRadius = 16;
            fieldPassword.Cursor = Cursors.IBeam;
            fieldPassword.CustomizableEdges = customizableEdges3;
            fieldPassword.DefaultText = "";
            fieldPassword.DisabledState.BorderColor = Color.White;
            fieldPassword.DisabledState.ForeColor = Color.DimGray;
            fieldPassword.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldPassword.Dock = DockStyle.Fill;
            fieldPassword.FocusedState.BorderColor = Color.SandyBrown;
            fieldPassword.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldPassword.ForeColor = Color.Black;
            fieldPassword.HoverState.BorderColor = Color.SandyBrown;
            fieldPassword.IconLeft = (Image) resources.GetObject("fieldPassword.IconLeft");
            fieldPassword.IconLeftOffset = new Point(10, 0);
            fieldPassword.IconRight = Properties.Resources.closed_eye_20px;
            fieldPassword.IconRightOffset = new Point(10, 0);
            fieldPassword.Location = new Point(25, 180);
            fieldPassword.Margin = new Padding(5);
            fieldPassword.Name = "fieldPassword";
            fieldPassword.PasswordChar = '●';
            fieldPassword.PlaceholderForeColor = Color.DimGray;
            fieldPassword.PlaceholderText = "Contraseña";
            fieldPassword.SelectedText = "";
            fieldPassword.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldPassword.Size = new Size(450, 35);
            fieldPassword.TabIndex = 2;
            fieldPassword.TextOffset = new Point(5, 0);
            fieldPassword.UseSystemPasswordChar = true;
            // 
            // btnRegistrarCuentaUsuario
            // 
            btnRegistrarCuentaUsuario.Animated = true;
            btnRegistrarCuentaUsuario.BorderRadius = 18;
            btnRegistrarCuentaUsuario.CustomizableEdges = customizableEdges5;
            btnRegistrarCuentaUsuario.Dock = DockStyle.Fill;
            btnRegistrarCuentaUsuario.FillColor = Color.PeachPuff;
            btnRegistrarCuentaUsuario.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnRegistrarCuentaUsuario.ForeColor = Color.Black;
            btnRegistrarCuentaUsuario.Location = new Point(23, 402);
            btnRegistrarCuentaUsuario.Name = "btnRegistrarCuentaUsuario";
            btnRegistrarCuentaUsuario.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnRegistrarCuentaUsuario.Size = new Size(454, 39);
            btnRegistrarCuentaUsuario.TabIndex = 5;
            btnRegistrarCuentaUsuario.Text = "Registrar mi cuenta";
            // 
            // btnRegresarAutenticar
            // 
            btnRegresarAutenticar.Animated = true;
            btnRegresarAutenticar.BorderColor = Color.Gainsboro;
            btnRegresarAutenticar.BorderRadius = 18;
            btnRegresarAutenticar.BorderThickness = 1;
            btnRegresarAutenticar.CustomizableEdges = customizableEdges7;
            btnRegresarAutenticar.Dock = DockStyle.Fill;
            btnRegresarAutenticar.FillColor = Color.White;
            btnRegresarAutenticar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnRegresarAutenticar.ForeColor = Color.Gainsboro;
            btnRegresarAutenticar.HoverState.BorderColor = Color.PeachPuff;
            btnRegresarAutenticar.HoverState.FillColor = Color.PeachPuff;
            btnRegresarAutenticar.HoverState.ForeColor = Color.Black;
            btnRegresarAutenticar.Location = new Point(23, 457);
            btnRegresarAutenticar.Name = "btnRegresarAutenticar";
            btnRegresarAutenticar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnRegresarAutenticar.Size = new Size(454, 39);
            btnRegresarAutenticar.TabIndex = 6;
            btnRegresarAutenticar.Text = "Regresar y autenticarme";
            // 
            // fieldConfirmarPassword
            // 
            fieldConfirmarPassword.Animated = true;
            fieldConfirmarPassword.BorderColor = Color.Gainsboro;
            fieldConfirmarPassword.BorderRadius = 16;
            fieldConfirmarPassword.Cursor = Cursors.IBeam;
            fieldConfirmarPassword.CustomizableEdges = customizableEdges9;
            fieldConfirmarPassword.DefaultText = "";
            fieldConfirmarPassword.DisabledState.BorderColor = Color.White;
            fieldConfirmarPassword.DisabledState.ForeColor = Color.DimGray;
            fieldConfirmarPassword.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldConfirmarPassword.Dock = DockStyle.Fill;
            fieldConfirmarPassword.FocusedState.BorderColor = Color.SandyBrown;
            fieldConfirmarPassword.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldConfirmarPassword.ForeColor = Color.Black;
            fieldConfirmarPassword.HoverState.BorderColor = Color.SandyBrown;
            fieldConfirmarPassword.IconLeft = (Image) resources.GetObject("fieldConfirmarPassword.IconLeft");
            fieldConfirmarPassword.IconLeftOffset = new Point(10, 0);
            fieldConfirmarPassword.IconRightOffset = new Point(10, 0);
            fieldConfirmarPassword.Location = new Point(25, 235);
            fieldConfirmarPassword.Margin = new Padding(5);
            fieldConfirmarPassword.Name = "fieldConfirmarPassword";
            fieldConfirmarPassword.PasswordChar = '●';
            fieldConfirmarPassword.PlaceholderForeColor = Color.DimGray;
            fieldConfirmarPassword.PlaceholderText = "Confirme la contraseña";
            fieldConfirmarPassword.SelectedText = "";
            fieldConfirmarPassword.ShadowDecoration.CustomizableEdges = customizableEdges10;
            fieldConfirmarPassword.Size = new Size(450, 35);
            fieldConfirmarPassword.TabIndex = 3;
            fieldConfirmarPassword.TextOffset = new Point(5, 0);
            fieldConfirmarPassword.UseSystemPasswordChar = true;
            // 
            // layoutTerminosServicio
            // 
            layoutTerminosServicio.ColumnCount = 2;
            layoutTerminosServicio.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 26F));
            layoutTerminosServicio.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTerminosServicio.Controls.Add(fieldTextoAceptacionTerminosServicio, 1, 0);
            layoutTerminosServicio.Controls.Add(fieldAceptacionTerminosServicio, 0, 0);
            layoutTerminosServicio.Dock = DockStyle.Fill;
            layoutTerminosServicio.Location = new Point(35, 285);
            layoutTerminosServicio.Margin = new Padding(15, 0, 0, 0);
            layoutTerminosServicio.Name = "layoutTerminosServicio";
            layoutTerminosServicio.RowCount = 1;
            layoutTerminosServicio.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTerminosServicio.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutTerminosServicio.Size = new Size(445, 90);
            layoutTerminosServicio.TabIndex = 4;
            // 
            // fieldTextoAceptacionTerminosServicio
            // 
            fieldTextoAceptacionTerminosServicio.Dock = DockStyle.Fill;
            fieldTextoAceptacionTerminosServicio.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldTextoAceptacionTerminosServicio.ForeColor = Color.Black;
            fieldTextoAceptacionTerminosServicio.ImeMode = ImeMode.NoControl;
            fieldTextoAceptacionTerminosServicio.Location = new Point(31, 5);
            fieldTextoAceptacionTerminosServicio.Margin = new Padding(5, 5, 1, 1);
            fieldTextoAceptacionTerminosServicio.Name = "fieldTextoAceptacionTerminosServicio";
            fieldTextoAceptacionTerminosServicio.Size = new Size(413, 84);
            fieldTextoAceptacionTerminosServicio.TabIndex = 1;
            fieldTextoAceptacionTerminosServicio.Text = "Al crear una cuenta usted está de acuerdo con los Términos de Servicio. Para más información acerca de las prácticas de privacidad de aDVance ERP, vea los Estatutos de Privacidad de aDVance ERP.";
            // 
            // fieldAceptacionTerminosServicio
            // 
            fieldAceptacionTerminosServicio.BackColor = Color.White;
            fieldAceptacionTerminosServicio.CheckedState.BorderColor = Color.Gainsboro;
            fieldAceptacionTerminosServicio.CheckedState.BorderRadius = 4;
            fieldAceptacionTerminosServicio.CheckedState.BorderThickness = 1;
            fieldAceptacionTerminosServicio.CheckedState.FillColor = Color.WhiteSmoke;
            fieldAceptacionTerminosServicio.CheckMarkColor = Color.Black;
            fieldAceptacionTerminosServicio.Dock = DockStyle.Top;
            fieldAceptacionTerminosServicio.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            fieldAceptacionTerminosServicio.Location = new Point(5, 5);
            fieldAceptacionTerminosServicio.Margin = new Padding(5, 5, 5, 15);
            fieldAceptacionTerminosServicio.Name = "fieldAceptacionTerminosServicio";
            fieldAceptacionTerminosServicio.Size = new Size(16, 25);
            fieldAceptacionTerminosServicio.TabIndex = 0;
            fieldAceptacionTerminosServicio.UncheckedState.BorderColor = Color.Gainsboro;
            fieldAceptacionTerminosServicio.UncheckedState.BorderRadius = 4;
            fieldAceptacionTerminosServicio.UncheckedState.BorderThickness = 1;
            fieldAceptacionTerminosServicio.UncheckedState.FillColor = Color.PeachPuff;
            fieldAceptacionTerminosServicio.UseVisualStyleBackColor = false;
            // 
            // VistaRegistroUsuario
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutVista);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroUsuario";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroUsuario";
            layoutVista.ResumeLayout(false);
            layoutTerminosServicio.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutVista;
        private Label fieldTitulo;
        private Label fieldCopyright;
        private Guna2TextBox fieldNombreUsuario;
        private Guna2TextBox fieldPassword;
        private Guna2Button btnRegistrarCuentaUsuario;
        private Guna2Button btnRegresarAutenticar;
        private Guna2TextBox fieldConfirmarPassword;
        private TableLayoutPanel layoutTerminosServicio;
        private Label fieldTextoAceptacionTerminosServicio;
        private Guna2CheckBox fieldAceptacionTerminosServicio;
    }
}