namespace aDVanceERP.Modulos.Venta.Vistas {
    partial class VistaRegistroPagoEfectivo {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges49 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges50 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges51 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges52 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges53 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges54 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges55 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaRegistroPagoEfectivo));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges56 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            layoutDistribucion1 = new TableLayoutPanel();
            layoutBotones = new TableLayoutPanel();
            btnNoCancel = new Guna.UI2.WinForms.Guna2Button();
            btnRegistrarPago = new Guna.UI2.WinForms.Guna2Button();
            layoutBarraTitulo = new TableLayoutPanel();
            fieldTitulo = new Label();
            fieldIcono = new PictureBox();
            btnCerrar = new Guna.UI2.WinForms.Guna2ControlBox();
            layoutTitulos1 = new TableLayoutPanel();
            fieldTituloMonto = new Label();
            layoutDatos1 = new TableLayoutPanel();
            fieldMonto = new Guna.UI2.WinForms.Guna2TextBox();
            fieldEstadoPendiente = new Guna.UI2.WinForms.Guna2CheckBox();
            fieldTextoEstadoPago = new Label();
            layoutEstadoPago = new TableLayoutPanel();
            layoutDistribucion1.SuspendLayout();
            layoutBotones.SuspendLayout();
            layoutBarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fieldIcono).BeginInit();
            layoutTitulos1.SuspendLayout();
            layoutDatos1.SuspendLayout();
            layoutEstadoPago.SuspendLayout();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.AnimateWindow = true;
            formatoBase.BorderRadius = 16;
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
            formatoBase.DragForm = false;
            formatoBase.ResizeForm = false;
            formatoBase.TransparentWhileDrag = true;
            // 
            // layoutDistribucion1
            // 
            layoutDistribucion1.BackColor = Color.White;
            layoutDistribucion1.ColumnCount = 3;
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDistribucion1.Controls.Add(layoutBotones, 1, 8);
            layoutDistribucion1.Controls.Add(layoutBarraTitulo, 1, 0);
            layoutDistribucion1.Controls.Add(layoutTitulos1, 1, 2);
            layoutDistribucion1.Controls.Add(layoutDatos1, 1, 3);
            layoutDistribucion1.Controls.Add(layoutEstadoPago, 1, 5);
            layoutDistribucion1.Dock = DockStyle.Fill;
            layoutDistribucion1.Location = new Point(0, 0);
            layoutDistribucion1.Name = "layoutDistribucion1";
            layoutDistribucion1.RowCount = 10;
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion1.Size = new Size(577, 295);
            layoutDistribucion1.TabIndex = 2;
            // 
            // layoutBotones
            // 
            layoutBotones.BackColor = Color.White;
            layoutBotones.ColumnCount = 3;
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            layoutBotones.Controls.Add(btnNoCancel, 2, 0);
            layoutBotones.Controls.Add(btnRegistrarPago, 1, 0);
            layoutBotones.Dock = DockStyle.Fill;
            layoutBotones.Location = new Point(20, 230);
            layoutBotones.Margin = new Padding(0);
            layoutBotones.Name = "layoutBotones";
            layoutBotones.RowCount = 1;
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBotones.Size = new Size(537, 45);
            layoutBotones.TabIndex = 11;
            // 
            // btnNoCancel
            // 
            btnNoCancel.Animated = true;
            btnNoCancel.BorderColor = Color.Gainsboro;
            btnNoCancel.BorderRadius = 18;
            btnNoCancel.BorderThickness = 1;
            btnNoCancel.CustomizableEdges = customizableEdges49;
            btnNoCancel.Dock = DockStyle.Fill;
            btnNoCancel.FillColor = Color.White;
            btnNoCancel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnNoCancel.ForeColor = Color.Gainsboro;
            btnNoCancel.HoverState.BorderColor = Color.PeachPuff;
            btnNoCancel.HoverState.FillColor = Color.PeachPuff;
            btnNoCancel.HoverState.ForeColor = Color.Black;
            btnNoCancel.Location = new Point(370, 3);
            btnNoCancel.Name = "btnNoCancel";
            btnNoCancel.ShadowDecoration.CustomizableEdges = customizableEdges50;
            btnNoCancel.Size = new Size(164, 39);
            btnNoCancel.TabIndex = 14;
            btnNoCancel.Text = "Cancelar";
            // 
            // btnRegistrarPago
            // 
            btnRegistrarPago.Animated = true;
            btnRegistrarPago.BorderRadius = 18;
            btnRegistrarPago.CustomizableEdges = customizableEdges51;
            btnRegistrarPago.Dock = DockStyle.Fill;
            btnRegistrarPago.FillColor = Color.PeachPuff;
            btnRegistrarPago.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrarPago.ForeColor = Color.Black;
            btnRegistrarPago.Location = new Point(140, 3);
            btnRegistrarPago.Name = "btnRegistrarPago";
            btnRegistrarPago.ShadowDecoration.CustomizableEdges = customizableEdges52;
            btnRegistrarPago.Size = new Size(224, 39);
            btnRegistrarPago.TabIndex = 15;
            btnRegistrarPago.Text = "Registrar el pago";
            // 
            // layoutBarraTitulo
            // 
            layoutBarraTitulo.BackColor = Color.WhiteSmoke;
            layoutBarraTitulo.ColumnCount = 3;
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBarraTitulo.Controls.Add(fieldTitulo, 0, 0);
            layoutBarraTitulo.Controls.Add(fieldIcono, 0, 0);
            layoutBarraTitulo.Controls.Add(btnCerrar, 2, 0);
            layoutBarraTitulo.Dock = DockStyle.Fill;
            layoutBarraTitulo.Location = new Point(20, 0);
            layoutBarraTitulo.Margin = new Padding(0, 0, 0, 2);
            layoutBarraTitulo.Name = "layoutBarraTitulo";
            layoutBarraTitulo.RowCount = 1;
            layoutBarraTitulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBarraTitulo.Size = new Size(537, 43);
            layoutBarraTitulo.TabIndex = 12;
            // 
            // fieldTitulo
            // 
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(53, 0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(431, 43);
            fieldTitulo.TabIndex = 4;
            fieldTitulo.Text = "Pago de venta en efectivo";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = Properties.Resources.best_salesB_24px;
            fieldIcono.BackgroundImageLayout = ImageLayout.Center;
            fieldIcono.Dock = DockStyle.Fill;
            fieldIcono.Location = new Point(3, 3);
            fieldIcono.Margin = new Padding(3, 3, 0, 3);
            fieldIcono.Name = "fieldIcono";
            fieldIcono.Size = new Size(47, 37);
            fieldIcono.TabIndex = 0;
            fieldIcono.TabStop = false;
            // 
            // btnCerrar
            // 
            btnCerrar.BorderRadius = 5;
            btnCerrar.CustomizableEdges = customizableEdges53;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.WhiteSmoke;
            btnCerrar.HoverState.FillColor = Color.FromArgb(192, 0, 0);
            btnCerrar.HoverState.IconColor = Color.White;
            btnCerrar.IconColor = Color.Black;
            btnCerrar.Location = new Point(488, 1);
            btnCerrar.Margin = new Padding(1);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges54;
            btnCerrar.Size = new Size(48, 41);
            btnCerrar.TabIndex = 2;
            // 
            // layoutTitulos1
            // 
            layoutTitulos1.ColumnCount = 2;
            layoutTitulos1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175F));
            layoutTitulos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulos1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutTitulos1.Controls.Add(fieldTituloMonto, 0, 0);
            layoutTitulos1.Dock = DockStyle.Fill;
            layoutTitulos1.Location = new Point(20, 65);
            layoutTitulos1.Margin = new Padding(0);
            layoutTitulos1.Name = "layoutTitulos1";
            layoutTitulos1.RowCount = 1;
            layoutTitulos1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulos1.Size = new Size(537, 35);
            layoutTitulos1.TabIndex = 48;
            // 
            // fieldTituloMonto
            // 
            fieldTituloMonto.Dock = DockStyle.Fill;
            fieldTituloMonto.Font = new Font("Segoe UI", 11.25F);
            fieldTituloMonto.ForeColor = Color.DimGray;
            fieldTituloMonto.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloMonto.ImeMode = ImeMode.NoControl;
            fieldTituloMonto.Location = new Point(15, 5);
            fieldTituloMonto.Margin = new Padding(15, 5, 3, 3);
            fieldTituloMonto.Name = "fieldTituloMonto";
            fieldTituloMonto.Size = new Size(157, 27);
            fieldTituloMonto.TabIndex = 46;
            fieldTituloMonto.Text = " Monto pagado :";
            fieldTituloMonto.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutDatos1
            // 
            layoutDatos1.ColumnCount = 2;
            layoutDatos1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175F));
            layoutDatos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDatos1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDatos1.Controls.Add(fieldMonto, 0, 0);
            layoutDatos1.Dock = DockStyle.Fill;
            layoutDatos1.Location = new Point(20, 100);
            layoutDatos1.Margin = new Padding(0);
            layoutDatos1.Name = "layoutDatos1";
            layoutDatos1.RowCount = 1;
            layoutDatos1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDatos1.Size = new Size(537, 45);
            layoutDatos1.TabIndex = 49;
            // 
            // fieldMonto
            // 
            fieldMonto.Animated = true;
            fieldMonto.AutoRoundedCorners = true;
            fieldMonto.BorderColor = Color.Gainsboro;
            fieldMonto.BorderRadius = 16;
            fieldMonto.Cursor = Cursors.IBeam;
            fieldMonto.CustomizableEdges = customizableEdges55;
            fieldMonto.DefaultText = "";
            fieldMonto.DisabledState.BorderColor = Color.White;
            fieldMonto.DisabledState.ForeColor = Color.DimGray;
            fieldMonto.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldMonto.Dock = DockStyle.Fill;
            fieldMonto.FocusedState.BorderColor = Color.SandyBrown;
            fieldMonto.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldMonto.ForeColor = Color.Black;
            fieldMonto.HoverState.BorderColor = Color.SandyBrown;
            fieldMonto.IconLeftOffset = new Point(10, 0);
            fieldMonto.IconRight = (Image)resources.GetObject("fieldMonto.IconRight");
            fieldMonto.IconRightOffset = new Point(6, 0);
            fieldMonto.IconRightSize = new Size(12, 12);
            fieldMonto.Location = new Point(5, 5);
            fieldMonto.Margin = new Padding(5);
            fieldMonto.Name = "fieldMonto";
            fieldMonto.PasswordChar = '\0';
            fieldMonto.PlaceholderForeColor = Color.DimGray;
            fieldMonto.PlaceholderText = "0.00";
            fieldMonto.SelectedText = "";
            fieldMonto.ShadowDecoration.CustomizableEdges = customizableEdges56;
            fieldMonto.Size = new Size(165, 35);
            fieldMonto.TabIndex = 51;
            fieldMonto.TextAlign = HorizontalAlignment.Right;
            fieldMonto.TextOffset = new Point(5, 0);
            // 
            // fieldEstadoPendiente
            // 
            fieldEstadoPendiente.BackColor = Color.White;
            fieldEstadoPendiente.CheckedState.BorderColor = Color.Gainsboro;
            fieldEstadoPendiente.CheckedState.BorderRadius = 4;
            fieldEstadoPendiente.CheckedState.BorderThickness = 1;
            fieldEstadoPendiente.CheckedState.FillColor = Color.WhiteSmoke;
            fieldEstadoPendiente.CheckMarkColor = Color.Black;
            fieldEstadoPendiente.Dock = DockStyle.Top;
            fieldEstadoPendiente.Font = new Font("Segoe UI", 12F);
            fieldEstadoPendiente.Location = new Point(5, 13);
            fieldEstadoPendiente.Margin = new Padding(5, 13, 5, 5);
            fieldEstadoPendiente.Name = "fieldEstadoPendiente";
            fieldEstadoPendiente.Size = new Size(16, 25);
            fieldEstadoPendiente.TabIndex = 0;
            fieldEstadoPendiente.UncheckedState.BorderColor = Color.Gainsboro;
            fieldEstadoPendiente.UncheckedState.BorderRadius = 4;
            fieldEstadoPendiente.UncheckedState.BorderThickness = 1;
            fieldEstadoPendiente.UncheckedState.FillColor = Color.PeachPuff;
            fieldEstadoPendiente.UseVisualStyleBackColor = false;
            // 
            // fieldTextoEstadoPago
            // 
            fieldTextoEstadoPago.Dock = DockStyle.Fill;
            fieldTextoEstadoPago.Font = new Font("Segoe UI", 11.25F);
            fieldTextoEstadoPago.ForeColor = Color.Black;
            fieldTextoEstadoPago.ImeMode = ImeMode.NoControl;
            fieldTextoEstadoPago.Location = new Point(31, 5);
            fieldTextoEstadoPago.Margin = new Padding(5, 5, 1, 1);
            fieldTextoEstadoPago.Name = "fieldTextoEstadoPago";
            fieldTextoEstadoPago.Size = new Size(490, 39);
            fieldTextoEstadoPago.TabIndex = 1;
            fieldTextoEstadoPago.Text = "Marcar como pendiente (Para envíos sin fondo)";
            fieldTextoEstadoPago.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutEstadoPago
            // 
            layoutEstadoPago.ColumnCount = 2;
            layoutEstadoPago.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 26F));
            layoutEstadoPago.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEstadoPago.Controls.Add(fieldTextoEstadoPago, 1, 0);
            layoutEstadoPago.Controls.Add(fieldEstadoPendiente, 0, 0);
            layoutEstadoPago.Dock = DockStyle.Fill;
            layoutEstadoPago.Location = new Point(35, 155);
            layoutEstadoPago.Margin = new Padding(15, 0, 0, 0);
            layoutEstadoPago.Name = "layoutEstadoPago";
            layoutEstadoPago.RowCount = 1;
            layoutEstadoPago.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEstadoPago.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutEstadoPago.Size = new Size(522, 45);
            layoutEstadoPago.TabIndex = 52;
            // 
            // VistaRegistroPagoEfectivo
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(577, 295);
            Controls.Add(layoutDistribucion1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VistaRegistroPagoEfectivo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VistaRegistroPagoEfectivo";
            TopMost = true;
            layoutDistribucion1.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            layoutBarraTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)fieldIcono).EndInit();
            layoutTitulos1.ResumeLayout(false);
            layoutDatos1.ResumeLayout(false);
            layoutEstadoPago.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutDistribucion1;
        private TableLayoutPanel layoutBotones;
        private Guna.UI2.WinForms.Guna2Button btnNoCancel;
        private Guna.UI2.WinForms.Guna2Button btnRegistrarPago;
        private TableLayoutPanel layoutBarraTitulo;
        private PictureBox fieldIcono;
        private Guna.UI2.WinForms.Guna2ControlBox btnCerrar;
        private Label fieldTitulo;
        private TableLayoutPanel layoutTitulos1;
        private Label fieldTituloMonto;
        private TableLayoutPanel layoutDatos1;
        private Guna.UI2.WinForms.Guna2TextBox fieldMonto;
        private TableLayoutPanel layoutEstadoPago;
        private Label fieldTextoEstadoPago;
        private Guna.UI2.WinForms.Guna2CheckBox fieldEstadoPendiente;
    }
}