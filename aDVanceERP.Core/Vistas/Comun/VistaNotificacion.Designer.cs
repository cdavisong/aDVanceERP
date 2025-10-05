namespace aDVanceERP.Core.Vistas.Comun {
    partial class VistaNotificacion {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaNotificacion));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            layoutDistribucion1 = new TableLayoutPanel();
            fieldIcono = new PictureBox();
            btnCerrar = new Guna.UI2.WinForms.Guna2Button();
            fieldMensaje = new Label();
            layoutDistribucion1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) fieldIcono).BeginInit();
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
            layoutDistribucion1.Dock = DockStyle.Fill;
            layoutDistribucion1.Location = new Point(0, 0);
            layoutDistribucion1.Name = "layoutDistribucion1";
            layoutDistribucion1.RowCount = 3;
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion1.Size = new Size(560, 160);
            layoutDistribucion1.TabIndex = 0;
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
            btnCerrar.Image = (Image) resources.GetObject("btnCerrar.Image");
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
            // VistaNotificacion
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(560, 160);
            Controls.Add(layoutDistribucion1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "VistaNotificacion";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaNotificacion";
            TopMost = true;
            layoutDistribucion1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) fieldIcono).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutDistribucion1;
        private PictureBox fieldIcono;
        private Guna.UI2.WinForms.Guna2Button btnCerrar;
        private Label fieldMensaje;
    }
}