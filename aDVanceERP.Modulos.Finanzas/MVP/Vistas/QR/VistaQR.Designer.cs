using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.QR {
    partial class VistaQR {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaQR));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldIcono = new PictureBox();
            layoutTitulo = new TableLayoutPanel();
            btnCerrar = new Guna2Button();
            fieldTitulo = new Label();
            fieldPasosEscanearQr = new Guna2HtmlLabel();
            fieldSubtitulo = new Label();
            layoutQrDatos = new TableLayoutPanel();
            fieldCodigoQr = new PictureBox();
            layoutDatos = new TableLayoutPanel();
            fieldTituloMovil = new Label();
            fieldTituloAlias = new Label();
            fieldTituloCuenta = new Label();
            fieldAlias = new Label();
            fieldTarjeta = new Label();
            fieldMovil = new Label();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutTitulo.SuspendLayout();
            layoutQrDatos.SuspendLayout();
            ((ISupportInitialize) fieldCodigoQr).BeginInit();
            layoutDatos.SuspendLayout();
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
            layoutBase.Controls.Add(layoutVista, 1, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutBase.Size = new Size(500, 740);
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
            layoutVista.Controls.Add(fieldIcono, 1, 1);
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldPasosEscanearQr, 2, 5);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 2);
            layoutVista.Controls.Add(layoutQrDatos, 2, 3);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(13, 0);
            layoutVista.Margin = new Padding(3, 0, 0, 0);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 7;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 145F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(487, 740);
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
            btnCerrar.CustomizableEdges = customizableEdges1;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.White;
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
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
            fieldTitulo.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(3, 0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(361, 45);
            fieldTitulo.TabIndex = 0;
            fieldTitulo.Text = "QR";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldPasosEscanearQr
            // 
            fieldPasosEscanearQr.BackColor = Color.White;
            fieldPasosEscanearQr.Dock = DockStyle.Top;
            fieldPasosEscanearQr.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            fieldPasosEscanearQr.ForeColor = Color.Black;
            fieldPasosEscanearQr.Location = new Point(53, 258);
            fieldPasosEscanearQr.Name = "fieldPasosEscanearQr";
            fieldPasosEscanearQr.Size = new Size(412, 431);
            fieldPasosEscanearQr.TabIndex = 17;
            fieldPasosEscanearQr.Text = resources.GetString("fieldPasosEscanearQr.Text");
            // 
            // fieldSubtitulo
            // 
            fieldSubtitulo.Dock = DockStyle.Fill;
            fieldSubtitulo.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldSubtitulo.ForeColor = Color.DimGray;
            fieldSubtitulo.ImeMode = ImeMode.NoControl;
            fieldSubtitulo.Location = new Point(55, 70);
            fieldSubtitulo.Margin = new Padding(5, 5, 1, 1);
            fieldSubtitulo.Name = "fieldSubtitulo";
            fieldSubtitulo.Size = new Size(411, 29);
            fieldSubtitulo.TabIndex = 18;
            fieldSubtitulo.Text = "Código QR para la realización de transferencias";
            // 
            // layoutQrDatos
            // 
            layoutQrDatos.ColumnCount = 2;
            layoutQrDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
            layoutQrDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutQrDatos.Controls.Add(fieldCodigoQr, 0, 0);
            layoutQrDatos.Controls.Add(layoutDatos, 1, 0);
            layoutQrDatos.Dock = DockStyle.Fill;
            layoutQrDatos.Location = new Point(50, 100);
            layoutQrDatos.Margin = new Padding(0);
            layoutQrDatos.Name = "layoutQrDatos";
            layoutQrDatos.RowCount = 1;
            layoutQrDatos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutQrDatos.Size = new Size(417, 145);
            layoutQrDatos.TabIndex = 19;
            // 
            // fieldCodigoQr
            // 
            fieldCodigoQr.BackgroundImageLayout = ImageLayout.Center;
            fieldCodigoQr.Location = new Point(5, 5);
            fieldCodigoQr.Margin = new Padding(5);
            fieldCodigoQr.Name = "fieldCodigoQr";
            fieldCodigoQr.Size = new Size(135, 135);
            fieldCodigoQr.TabIndex = 15;
            fieldCodigoQr.TabStop = false;
            // 
            // layoutDatos
            // 
            layoutDatos.ColumnCount = 2;
            layoutDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 92F));
            layoutDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDatos.Controls.Add(fieldTituloMovil, 0, 3);
            layoutDatos.Controls.Add(fieldTituloAlias, 0, 1);
            layoutDatos.Controls.Add(fieldTituloCuenta, 0, 2);
            layoutDatos.Controls.Add(fieldAlias, 1, 1);
            layoutDatos.Controls.Add(fieldTarjeta, 1, 2);
            layoutDatos.Controls.Add(fieldMovil, 1, 3);
            layoutDatos.Dock = DockStyle.Fill;
            layoutDatos.Location = new Point(145, 0);
            layoutDatos.Margin = new Padding(0);
            layoutDatos.Name = "layoutDatos";
            layoutDatos.RowCount = 5;
            layoutDatos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutDatos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutDatos.Size = new Size(272, 145);
            layoutDatos.TabIndex = 16;
            // 
            // fieldTituloMovil
            // 
            fieldTituloMovil.Dock = DockStyle.Fill;
            fieldTituloMovil.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldTituloMovil.ForeColor = Color.DimGray;
            fieldTituloMovil.Image = (Image) resources.GetObject("fieldTituloMovil.Image");
            fieldTituloMovil.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloMovil.ImeMode = ImeMode.NoControl;
            fieldTituloMovil.Location = new Point(3, 92);
            fieldTituloMovil.Margin = new Padding(3, 5, 3, 3);
            fieldTituloMovil.Name = "fieldTituloMovil";
            fieldTituloMovil.Size = new Size(86, 22);
            fieldTituloMovil.TabIndex = 35;
            fieldTituloMovil.Text = "      Móvil :";
            fieldTituloMovil.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTituloAlias
            // 
            fieldTituloAlias.Dock = DockStyle.Fill;
            fieldTituloAlias.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldTituloAlias.ForeColor = Color.DimGray;
            fieldTituloAlias.Image = (Image) resources.GetObject("fieldTituloAlias.Image");
            fieldTituloAlias.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloAlias.ImeMode = ImeMode.NoControl;
            fieldTituloAlias.Location = new Point(3, 32);
            fieldTituloAlias.Margin = new Padding(3, 5, 3, 3);
            fieldTituloAlias.Name = "fieldTituloAlias";
            fieldTituloAlias.Size = new Size(86, 22);
            fieldTituloAlias.TabIndex = 34;
            fieldTituloAlias.Text = "      Alias :";
            fieldTituloAlias.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTituloCuenta
            // 
            fieldTituloCuenta.Dock = DockStyle.Fill;
            fieldTituloCuenta.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldTituloCuenta.ForeColor = Color.DimGray;
            fieldTituloCuenta.Image = (Image) resources.GetObject("fieldTituloCuenta.Image");
            fieldTituloCuenta.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloCuenta.ImeMode = ImeMode.NoControl;
            fieldTituloCuenta.Location = new Point(3, 62);
            fieldTituloCuenta.Margin = new Padding(3, 5, 3, 3);
            fieldTituloCuenta.Name = "fieldTituloCuenta";
            fieldTituloCuenta.Size = new Size(86, 22);
            fieldTituloCuenta.TabIndex = 36;
            fieldTituloCuenta.Text = "      Tarjeta :";
            fieldTituloCuenta.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldAlias
            // 
            fieldAlias.Dock = DockStyle.Fill;
            fieldAlias.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldAlias.ForeColor = Color.Black;
            fieldAlias.ImeMode = ImeMode.NoControl;
            fieldAlias.Location = new Point(95, 30);
            fieldAlias.Margin = new Padding(3);
            fieldAlias.Name = "fieldAlias";
            fieldAlias.Size = new Size(174, 24);
            fieldAlias.TabIndex = 37;
            fieldAlias.Text = "-";
            fieldAlias.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTarjeta
            // 
            fieldTarjeta.Dock = DockStyle.Fill;
            fieldTarjeta.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldTarjeta.ForeColor = Color.Black;
            fieldTarjeta.ImeMode = ImeMode.NoControl;
            fieldTarjeta.Location = new Point(95, 60);
            fieldTarjeta.Margin = new Padding(3);
            fieldTarjeta.Name = "fieldTarjeta";
            fieldTarjeta.Size = new Size(174, 24);
            fieldTarjeta.TabIndex = 38;
            fieldTarjeta.Text = "-";
            fieldTarjeta.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldMovil
            // 
            fieldMovil.Dock = DockStyle.Fill;
            fieldMovil.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldMovil.ForeColor = Color.Black;
            fieldMovil.ImeMode = ImeMode.NoControl;
            fieldMovil.Location = new Point(95, 90);
            fieldMovil.Margin = new Padding(3);
            fieldMovil.Name = "fieldMovil";
            fieldMovil.Size = new Size(174, 24);
            fieldMovil.TabIndex = 39;
            fieldMovil.Text = "-";
            fieldMovil.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaQR
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 740);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaQR";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaQR";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            layoutVista.PerformLayout();
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutTitulo.ResumeLayout(false);
            layoutQrDatos.ResumeLayout(false);
            ((ISupportInitialize) fieldCodigoQr).EndInit();
            layoutDatos.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private PictureBox fieldIcono;
        private TableLayoutPanel layoutTitulo;
        private Guna2Button btnCerrar;
        private Label fieldTitulo;
        private TableLayoutPanel layoutPrecios;
        private Guna2TextBox fieldPrecioCesion;
        private Guna2TextBox fieldPrecioAdquisicion;
        private Guna2TextBox fieldStock;
        private PictureBox fieldCodigoQr;
        private Guna2HtmlLabel fieldPasosEscanearQr;
        private Label fieldSubtitulo;
        private TableLayoutPanel layoutQrDatos;
        private TableLayoutPanel layoutDatos;
        private Label fieldTituloAlias;
        private Label fieldTituloMovil;
        private Label fieldTituloCuenta;
        private Label fieldAlias;
        private Label fieldTarjeta;
        private Label fieldMovil;
    }
}