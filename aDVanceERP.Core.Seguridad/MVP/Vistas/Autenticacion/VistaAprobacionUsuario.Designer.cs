using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion {
    partial class VistaAprobacionUsuario {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaAprobacionUsuario));
            formatoBase = new Guna2BorderlessForm(components);
            layoutVista = new TableLayoutPanel();
            fieldTitulo = new Label();
            fieldCopyright = new Label();
            btnCambiarUsuario = new Guna2Button();
            fieldImagen = new PictureBox();
            fieldMensaje = new Guna2HtmlLabel();
            layoutVista.SuspendLayout();
            ((ISupportInitialize) fieldImagen).BeginInit();
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
            layoutVista.Controls.Add(fieldCopyright, 1, 9);
            layoutVista.Controls.Add(btnCambiarUsuario, 1, 7);
            layoutVista.Controls.Add(fieldImagen, 1, 3);
            layoutVista.Controls.Add(fieldMensaje, 1, 5);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 11;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 200F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(500, 685);
            layoutVista.TabIndex = 1;
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
            fieldTitulo.Text = "¡Bienvenido a nuestro sistema!";
            fieldTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldCopyright
            // 
            fieldCopyright.Dock = DockStyle.Fill;
            fieldCopyright.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            fieldCopyright.ForeColor = Color.DarkGray;
            fieldCopyright.ImeMode = ImeMode.NoControl;
            fieldCopyright.Location = new Point(23, 585);
            fieldCopyright.Name = "fieldCopyright";
            fieldCopyright.Size = new Size(454, 80);
            fieldCopyright.TabIndex = 7;
            fieldCopyright.Text = "Copyright 2025© aDVance ERP®";
            fieldCopyright.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnCambiarUsuario
            // 
            btnCambiarUsuario.Animated = true;
            btnCambiarUsuario.BorderColor = Color.Gainsboro;
            btnCambiarUsuario.BorderRadius = 18;
            btnCambiarUsuario.BorderThickness = 1;
            btnCambiarUsuario.CustomizableEdges = customizableEdges1;
            btnCambiarUsuario.Dock = DockStyle.Fill;
            btnCambiarUsuario.FillColor = Color.White;
            btnCambiarUsuario.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnCambiarUsuario.ForeColor = Color.Gainsboro;
            btnCambiarUsuario.HoverState.BorderColor = Color.PeachPuff;
            btnCambiarUsuario.HoverState.FillColor = Color.PeachPuff;
            btnCambiarUsuario.HoverState.ForeColor = Color.Black;
            btnCambiarUsuario.Location = new Point(23, 483);
            btnCambiarUsuario.Name = "btnCambiarUsuario";
            btnCambiarUsuario.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCambiarUsuario.Size = new Size(454, 39);
            btnCambiarUsuario.TabIndex = 17;
            btnCambiarUsuario.Text = "Cambiar de usuario";
            // 
            // fieldImagen
            // 
            fieldImagen.BackgroundImage = (Image) resources.GetObject("fieldImagen.BackgroundImage");
            fieldImagen.BackgroundImageLayout = ImageLayout.Center;
            fieldImagen.Dock = DockStyle.Fill;
            fieldImagen.Location = new Point(21, 141);
            fieldImagen.Margin = new Padding(1);
            fieldImagen.Name = "fieldImagen";
            fieldImagen.Size = new Size(458, 98);
            fieldImagen.TabIndex = 19;
            fieldImagen.TabStop = false;
            // 
            // fieldMensaje
            // 
            fieldMensaje.AutoSize = false;
            fieldMensaje.BackColor = Color.FromArgb(  250,   250,   250);
            fieldMensaje.Dock = DockStyle.Fill;
            fieldMensaje.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldMensaje.Location = new Point(23, 263);
            fieldMensaje.Name = "fieldMensaje";
            fieldMensaje.Size = new Size(454, 194);
            fieldMensaje.TabIndex = 20;
            fieldMensaje.Text = resources.GetString("fieldMensaje.Text");
            // 
            // VistaAprobacionUsuario
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutVista);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaAprobacionUsuario";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaAprobacionUsuario";
            layoutVista.ResumeLayout(false);
            ((ISupportInitialize) fieldImagen).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutVista;
        private Label fieldTitulo;
        private Label fieldCopyright;
        private Guna2Button btnCambiarUsuario;
        private PictureBox fieldImagen;
        private Guna2HtmlLabel fieldMensaje;
    }
}