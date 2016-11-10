namespace OperarEtiquetas
{
    partial class frmEstibaCompleta
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEstibaV = new System.Windows.Forms.TextBox();
            this.txtLineaV = new System.Windows.Forms.TextBox();
            this.txtPuntoV = new System.Windows.Forms.TextBox();
            this.grpVieja = new System.Windows.Forms.GroupBox();
            this.btnValida = new System.Windows.Forms.Button();
            this.grpNueva = new System.Windows.Forms.GroupBox();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.txtEstibaN = new System.Windows.Forms.TextBox();
            this.txtPuntoN = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLineaN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.errorIcon = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpVieja.SuspendLayout();
            this.grpNueva.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Estiba";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Linea";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(253, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Punto";
            // 
            // txtEstibaV
            // 
            this.txtEstibaV.Location = new System.Drawing.Point(6, 38);
            this.txtEstibaV.Name = "txtEstibaV";
            this.txtEstibaV.Size = new System.Drawing.Size(100, 20);
            this.txtEstibaV.TabIndex = 3;
            this.txtEstibaV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEstibaV_KeyPress);
            this.txtEstibaV.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEstibaV_KeyUp);
            // 
            // txtLineaV
            // 
            this.txtLineaV.Location = new System.Drawing.Point(112, 38);
            this.txtLineaV.Name = "txtLineaV";
            this.txtLineaV.Size = new System.Drawing.Size(100, 20);
            this.txtLineaV.TabIndex = 4;
            this.txtLineaV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLineaV_KeyPress);
            // 
            // txtPuntoV
            // 
            this.txtPuntoV.Location = new System.Drawing.Point(218, 38);
            this.txtPuntoV.Name = "txtPuntoV";
            this.txtPuntoV.Size = new System.Drawing.Size(100, 20);
            this.txtPuntoV.TabIndex = 5;
            this.txtPuntoV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPuntoV_KeyPress);
            // 
            // grpVieja
            // 
            this.grpVieja.Controls.Add(this.btnValida);
            this.grpVieja.Controls.Add(this.txtEstibaV);
            this.grpVieja.Controls.Add(this.txtPuntoV);
            this.grpVieja.Controls.Add(this.label1);
            this.grpVieja.Controls.Add(this.txtLineaV);
            this.grpVieja.Controls.Add(this.label2);
            this.grpVieja.Controls.Add(this.label3);
            this.grpVieja.Location = new System.Drawing.Point(12, 12);
            this.grpVieja.Name = "grpVieja";
            this.grpVieja.Size = new System.Drawing.Size(335, 100);
            this.grpVieja.TabIndex = 6;
            this.grpVieja.TabStop = false;
            this.grpVieja.Text = "Estiba Antigua";
            // 
            // btnValida
            // 
            this.btnValida.Location = new System.Drawing.Point(181, 71);
            this.btnValida.Name = "btnValida";
            this.btnValida.Size = new System.Drawing.Size(112, 23);
            this.btnValida.TabIndex = 8;
            this.btnValida.Text = "Valida Existencia";
            this.btnValida.UseVisualStyleBackColor = true;
            this.btnValida.Click += new System.EventHandler(this.btnValida_Click);
            // 
            // grpNueva
            // 
            this.grpNueva.Controls.Add(this.btnGrabar);
            this.grpNueva.Controls.Add(this.txtEstibaN);
            this.grpNueva.Controls.Add(this.txtPuntoN);
            this.grpNueva.Controls.Add(this.label4);
            this.grpNueva.Controls.Add(this.txtLineaN);
            this.grpNueva.Controls.Add(this.label5);
            this.grpNueva.Controls.Add(this.label6);
            this.grpNueva.Location = new System.Drawing.Point(12, 118);
            this.grpNueva.Name = "grpNueva";
            this.grpNueva.Size = new System.Drawing.Size(335, 100);
            this.grpNueva.TabIndex = 7;
            this.grpNueva.TabStop = false;
            this.grpNueva.Text = "Estiba Nueva";
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(243, 64);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(75, 23);
            this.btnGrabar.TabIndex = 6;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // txtEstibaN
            // 
            this.txtEstibaN.Location = new System.Drawing.Point(6, 38);
            this.txtEstibaN.Name = "txtEstibaN";
            this.txtEstibaN.Size = new System.Drawing.Size(100, 20);
            this.txtEstibaN.TabIndex = 3;
            this.txtEstibaN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEstibaN_KeyPress);
            // 
            // txtPuntoN
            // 
            this.txtPuntoN.Location = new System.Drawing.Point(218, 38);
            this.txtPuntoN.Name = "txtPuntoN";
            this.txtPuntoN.Size = new System.Drawing.Size(100, 20);
            this.txtPuntoN.TabIndex = 5;
            this.txtPuntoN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPuntoN_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Estiba";
            // 
            // txtLineaN
            // 
            this.txtLineaN.Location = new System.Drawing.Point(112, 38);
            this.txtLineaN.Name = "txtLineaN";
            this.txtLineaN.Size = new System.Drawing.Size(100, 20);
            this.txtLineaN.TabIndex = 4;
            this.txtLineaN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLineaN_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(148, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Linea";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(253, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Punto";
            // 
            // errorIcon
            // 
            this.errorIcon.ContainerControl = this;
            // 
            // frmEstibaCompleta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 304);
            this.Controls.Add(this.grpNueva);
            this.Controls.Add(this.grpVieja);
            this.Name = "frmEstibaCompleta";
            this.Text = "Traslado Estiba Completa";
            this.Load += new System.EventHandler(this.frmEstibaCompleta_Load);
            this.grpVieja.ResumeLayout(false);
            this.grpVieja.PerformLayout();
            this.grpNueva.ResumeLayout(false);
            this.grpNueva.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEstibaV;
        private System.Windows.Forms.TextBox txtLineaV;
        private System.Windows.Forms.TextBox txtPuntoV;
        private System.Windows.Forms.GroupBox grpVieja;
        private System.Windows.Forms.GroupBox grpNueva;
        private System.Windows.Forms.TextBox txtEstibaN;
        private System.Windows.Forms.TextBox txtPuntoN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLineaN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnValida;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.ErrorProvider errorIcon;
    }
}