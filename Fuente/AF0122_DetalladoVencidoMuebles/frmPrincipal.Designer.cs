namespace AF0122_DetalladoVencidoMuebles
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.fbPrincipal = new System.Windows.Forms.GroupBox();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.picBoxPrincipal = new System.Windows.Forms.PictureBox();
            this.bIniciar = new System.Windows.Forms.Button();
            this.gbRendimiento = new System.Windows.Forms.GroupBox();
            this.lbTimeTranscurridoUsuario = new System.Windows.Forms.Label();
            this.lbTimeTranscurrido = new System.Windows.Forms.Label();
            this.lbHoraFinalUsuario = new System.Windows.Forms.Label();
            this.lbHoraInicialUsuario = new System.Windows.Forms.Label();
            this.lbHoraFinal = new System.Windows.Forms.Label();
            this.lbHoraInicial = new System.Windows.Forms.Label();
            this.ssBarraEstatus = new System.Windows.Forms.StatusStrip();
            this.tsslbEstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.fbPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrincipal)).BeginInit();
            this.gbRendimiento.SuspendLayout();
            this.ssBarraEstatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // fbPrincipal
            // 
            this.fbPrincipal.Controls.Add(this.lbTitulo);
            this.fbPrincipal.Controls.Add(this.picBoxPrincipal);
            this.fbPrincipal.Controls.Add(this.bIniciar);
            this.fbPrincipal.Controls.Add(this.gbRendimiento);
            this.fbPrincipal.Location = new System.Drawing.Point(12, 9);
            this.fbPrincipal.Name = "fbPrincipal";
            this.fbPrincipal.Size = new System.Drawing.Size(464, 201);
            this.fbPrincipal.TabIndex = 6;
            this.fbPrincipal.TabStop = false;
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(62, 16);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(341, 24);
            this.lbTitulo.TabIndex = 14;
            this.lbTitulo.Text = "Reporte detallado vencido Muebles";
            // 
            // picBoxPrincipal
            // 
            this.picBoxPrincipal.Image = global::AF0122_DetalladoVencidoMuebles.Properties.Resources._1388218536_Calculator;
            this.picBoxPrincipal.Location = new System.Drawing.Point(23, 47);
            this.picBoxPrincipal.Name = "picBoxPrincipal";
            this.picBoxPrincipal.Size = new System.Drawing.Size(128, 128);
            this.picBoxPrincipal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBoxPrincipal.TabIndex = 13;
            this.picBoxPrincipal.TabStop = false;
            // 
            // bIniciar
            // 
            this.bIniciar.Location = new System.Drawing.Point(156, 160);
            this.bIniciar.Name = "bIniciar";
            this.bIniciar.Size = new System.Drawing.Size(281, 23);
            this.bIniciar.TabIndex = 8;
            this.bIniciar.Text = "Iniciar";
            this.bIniciar.UseVisualStyleBackColor = true;
            this.bIniciar.Click += new System.EventHandler(this.bIniciar_Click);
            // 
            // gbRendimiento
            // 
            this.gbRendimiento.Controls.Add(this.lbTimeTranscurridoUsuario);
            this.gbRendimiento.Controls.Add(this.lbTimeTranscurrido);
            this.gbRendimiento.Controls.Add(this.lbHoraFinalUsuario);
            this.gbRendimiento.Controls.Add(this.lbHoraInicialUsuario);
            this.gbRendimiento.Controls.Add(this.lbHoraFinal);
            this.gbRendimiento.Controls.Add(this.lbHoraInicial);
            this.gbRendimiento.Location = new System.Drawing.Point(156, 47);
            this.gbRendimiento.Name = "gbRendimiento";
            this.gbRendimiento.Size = new System.Drawing.Size(281, 107);
            this.gbRendimiento.TabIndex = 4;
            this.gbRendimiento.TabStop = false;
            this.gbRendimiento.Text = "Rendimiento";
            // 
            // lbTimeTranscurridoUsuario
            // 
            this.lbTimeTranscurridoUsuario.AutoSize = true;
            this.lbTimeTranscurridoUsuario.Location = new System.Drawing.Point(141, 78);
            this.lbTimeTranscurridoUsuario.Name = "lbTimeTranscurridoUsuario";
            this.lbTimeTranscurridoUsuario.Size = new System.Drawing.Size(13, 13);
            this.lbTimeTranscurridoUsuario.TabIndex = 11;
            this.lbTimeTranscurridoUsuario.Text = "--";
            // 
            // lbTimeTranscurrido
            // 
            this.lbTimeTranscurrido.AutoSize = true;
            this.lbTimeTranscurrido.Location = new System.Drawing.Point(72, 78);
            this.lbTimeTranscurrido.Name = "lbTimeTranscurrido";
            this.lbTimeTranscurrido.Size = new System.Drawing.Size(45, 13);
            this.lbTimeTranscurrido.TabIndex = 12;
            this.lbTimeTranscurrido.Text = "Tiempo:";
            // 
            // lbHoraFinalUsuario
            // 
            this.lbHoraFinalUsuario.AutoSize = true;
            this.lbHoraFinalUsuario.Location = new System.Drawing.Point(141, 53);
            this.lbHoraFinalUsuario.Name = "lbHoraFinalUsuario";
            this.lbHoraFinalUsuario.Size = new System.Drawing.Size(13, 13);
            this.lbHoraFinalUsuario.TabIndex = 4;
            this.lbHoraFinalUsuario.Text = "--";
            // 
            // lbHoraInicialUsuario
            // 
            this.lbHoraInicialUsuario.AutoSize = true;
            this.lbHoraInicialUsuario.Location = new System.Drawing.Point(141, 29);
            this.lbHoraInicialUsuario.Name = "lbHoraInicialUsuario";
            this.lbHoraInicialUsuario.Size = new System.Drawing.Size(13, 13);
            this.lbHoraInicialUsuario.TabIndex = 3;
            this.lbHoraInicialUsuario.Text = "--";
            // 
            // lbHoraFinal
            // 
            this.lbHoraFinal.AutoSize = true;
            this.lbHoraFinal.Location = new System.Drawing.Point(72, 53);
            this.lbHoraFinal.Name = "lbHoraFinal";
            this.lbHoraFinal.Size = new System.Drawing.Size(58, 13);
            this.lbHoraFinal.TabIndex = 1;
            this.lbHoraFinal.Text = "Hora Final:";
            // 
            // lbHoraInicial
            // 
            this.lbHoraInicial.AutoSize = true;
            this.lbHoraInicial.Location = new System.Drawing.Point(72, 29);
            this.lbHoraInicial.Name = "lbHoraInicial";
            this.lbHoraInicial.Size = new System.Drawing.Size(63, 13);
            this.lbHoraInicial.TabIndex = 0;
            this.lbHoraInicial.Text = "Hora Inicial:";
            // 
            // ssBarraEstatus
            // 
            this.ssBarraEstatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ssBarraEstatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslbEstatus});
            this.ssBarraEstatus.Location = new System.Drawing.Point(0, 213);
            this.ssBarraEstatus.Name = "ssBarraEstatus";
            this.ssBarraEstatus.Size = new System.Drawing.Size(488, 22);
            this.ssBarraEstatus.TabIndex = 7;
            this.ssBarraEstatus.Text = "statusStrip1";
            // 
            // tsslbEstatus
            // 
            this.tsslbEstatus.Name = "tsslbEstatus";
            this.tsslbEstatus.Size = new System.Drawing.Size(142, 17);
            this.tsslbEstatus.Text = "Presiona ESC para salir";
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(488, 235);
            this.Controls.Add(this.ssBarraEstatus);
            this.Controls.Add(this.fbPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmPrincipal";
            this.Text = "AF0122_ReporteDetalladoVencidoMuebles";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmPrincipal_KeyUp);
            this.fbPrincipal.ResumeLayout(false);
            this.fbPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrincipal)).EndInit();
            this.gbRendimiento.ResumeLayout(false);
            this.gbRendimiento.PerformLayout();
            this.ssBarraEstatus.ResumeLayout(false);
            this.ssBarraEstatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox fbPrincipal;
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.PictureBox picBoxPrincipal;
        private System.Windows.Forms.Button bIniciar;
        private System.Windows.Forms.GroupBox gbRendimiento;
        private System.Windows.Forms.Label lbTimeTranscurridoUsuario;
        private System.Windows.Forms.Label lbTimeTranscurrido;
        private System.Windows.Forms.Label lbHoraFinalUsuario;
        private System.Windows.Forms.Label lbHoraInicialUsuario;
        private System.Windows.Forms.Label lbHoraFinal;
        private System.Windows.Forms.Label lbHoraInicial;
        private System.Windows.Forms.StatusStrip ssBarraEstatus;
        private System.Windows.Forms.ToolStripStatusLabel tsslbEstatus;
    }
}

