namespace aDVanceERP.Core.Vistas.Comun {
    partial class VistaMensaje {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaMensaje));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            layoutDistribucion1 = new TableLayoutPanel();
            fieldIcono = new PictureBox();
            btnCerrar = new Guna.UI2.WinForms.Guna2Button();
            fieldMensaje = new Label();
            layoutBotones = new TableLayoutPanel();
            btnNoCancel = new Guna.UI2.WinForms.Guna2Button();
            btnOkYes = new Guna.UI2.WinForms.Guna2Button();
            layoutDistribucion1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fieldIcono).BeginInit();
            layoutBotones.SuspendLayout();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.AnimationType = Guna.UI2.WinForms.Guna2BorderlessForm.AnimateWindowType.AW_HOR_NEGATIVE;
            formatoBase.BorderRadius = 26;
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
            formatoBase.DragForm = false;
            formatoBase.ResizeForm = false;
            formatoBase.TransparentWhileDrag = true;
            // 
            // layoutDistribucion1
            // 
            layoutDistribucion1.ColumnCount = 5;
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 116F));
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDistribucion1.Controls.Add(fieldIcono, 1, 1);
            layoutDistribucion1.Controls.Add(btnCerrar, 3, 1);
            layoutDistribucion1.Controls.Add(fieldMensaje, 2, 1);
            layoutDistribucion1.Controls.Add(layoutBotones, 2, 3);
            layoutDistribucion1.Dock = DockStyle.Fill;
            layoutDistribucion1.Location = new Point(0, 0);
            layoutDistribucion1.Name = "layoutDistribucion1";
            layoutDistribucion1.RowCount = 5;
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion1.Size = new Size(560, 225);
            layoutDistribucion1.TabIndex = 1;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = Properties.Resources.info_96px;
            fieldIcono.BackgroundImageLayout = ImageLayout.Center;
            fieldIcono.Dock = DockStyle.Fill;
            fieldIcono.Location = new Point(20, 20);
            fieldIcono.Margin = new Padding(0);
            fieldIcono.Name = "fieldIcono";
            fieldIcono.Size = new Size(116, 120);
            fieldIcono.TabIndex = 0;
            fieldIcono.TabStop = false;
            // 
            // btnCerrar
            // 
            btnCerrar.Animated = true;
            btnCerrar.AutoRoundedCorners = true;
            btnCerrar.BorderColor = Color.Gray;
            btnCerrar.BorderRadius = 18;
            btnCerrar.CustomizableEdges = customizableEdges1;
            btnCerrar.Dock = DockStyle.Top;
            btnCerrar.FillColor = Color.White;
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.Black;
            btnCerrar.HoverState.FillColor = Color.White;
            btnCerrar.Image = (Image)resources.GetObject("btnCerrar.Image");
            btnCerrar.Location = new Point(498, 23);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCerrar.Size = new Size(39, 39);
            btnCerrar.TabIndex = 9;
            // 
            // fieldMensaje
            // 
            fieldMensaje.Dock = DockStyle.Fill;
            fieldMensaje.Font = new Font("Segoe UI", 11.25F);
            fieldMensaje.ForeColor = Color.Gray;
            fieldMensaje.ImeMode = ImeMode.NoControl;
            fieldMensaje.Location = new Point(146, 25);
            fieldMensaje.Margin = new Padding(10, 5, 1, 1);
            fieldMensaje.Name = "fieldMensaje";
            fieldMensaje.Size = new Size(348, 114);
            fieldMensaje.TabIndex = 10;
            fieldMensaje.Text = "...";
            fieldMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutBotones
            // 
            layoutBotones.BackColor = Color.White;
            layoutBotones.ColumnCount = 2;
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBotones.Controls.Add(btnNoCancel, 1, 0);
            layoutBotones.Controls.Add(btnOkYes, 0, 0);
            layoutBotones.Dock = DockStyle.Fill;
            layoutBotones.Location = new Point(136, 160);
            layoutBotones.Margin = new Padding(0);
            layoutBotones.Name = "layoutBotones";
            layoutBotones.RowCount = 1;
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBotones.Size = new Size(359, 45);
            layoutBotones.TabIndex = 11;
            // 
            // btnNoCancel
            // 
            btnNoCancel.Animated = true;
            btnNoCancel.BorderColor = Color.Gainsboro;
            btnNoCancel.BorderRadius = 18;
            btnNoCancel.BorderThickness = 1;
            btnNoCancel.CustomizableEdges = customizableEdges3;
            btnNoCancel.Dock = DockStyle.Fill;
            btnNoCancel.FillColor = Color.White;
            btnNoCancel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnNoCancel.ForeColor = Color.Gainsboro;
            btnNoCancel.HoverState.BorderColor = Color.PeachPuff;
            btnNoCancel.HoverState.FillColor = Color.PeachPuff;
            btnNoCancel.HoverState.ForeColor = Color.Black;
            btnNoCancel.Location = new Point(242, 3);
            btnNoCancel.Name = "btnNoCancel";
            btnNoCancel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnNoCancel.Size = new Size(114, 39);
            btnNoCancel.TabIndex = 14;
            btnNoCancel.Text = "Cancelar";
            // 
            // btnOkYes
            // 
            btnOkYes.Animated = true;
            btnOkYes.BorderRadius = 18;
            btnOkYes.CustomizableEdges = customizableEdges5;
            btnOkYes.Dock = DockStyle.Fill;
            btnOkYes.FillColor = Color.PeachPuff;
            btnOkYes.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnOkYes.ForeColor = Color.Black;
            btnOkYes.Location = new Point(3, 3);
            btnOkYes.Name = "btnOkYes";
            btnOkYes.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnOkYes.Size = new Size(233, 39);
            btnOkYes.TabIndex = 15;
            btnOkYes.Text = "Aceptar";
            // 
            // VistaMensaje
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(560, 225);
            Controls.Add(layoutDistribucion1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VistaMensaje";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaMensaje";
            TopMost = true;
            layoutDistribucion1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)fieldIcono).EndInit();
            layoutBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutDistribucion1;
        private PictureBox fieldIcono;
        private Guna.UI2.WinForms.Guna2Button btnCerrar;
        private Label fieldMensaje;
        private TableLayoutPanel layoutBotones;
        private Guna.UI2.WinForms.Guna2Button btnNoCancel;
        private Guna.UI2.WinForms.Guna2Button btnOkYes;
    }
}