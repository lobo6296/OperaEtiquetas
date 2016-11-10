namespace OperarEtiquetas
{
    partial class frmMenu
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
            this.operarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dividirPaqueteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cortarPiezasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.trasladosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trasladoEstibaCompletaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblEstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // operarToolStripMenuItem
            // 
            this.operarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dividirPaqueteToolStripMenuItem,
            this.cortarPiezasToolStripMenuItem});
            this.operarToolStripMenuItem.Name = "operarToolStripMenuItem";
            this.operarToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.operarToolStripMenuItem.Text = "Operar";
            // 
            // dividirPaqueteToolStripMenuItem
            // 
            this.dividirPaqueteToolStripMenuItem.Name = "dividirPaqueteToolStripMenuItem";
            this.dividirPaqueteToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.dividirPaqueteToolStripMenuItem.Text = "Dividir Paquete";
            this.dividirPaqueteToolStripMenuItem.Click += new System.EventHandler(this.dividirPaqueteToolStripMenuItem_Click);
            // 
            // cortarPiezasToolStripMenuItem
            // 
            this.cortarPiezasToolStripMenuItem.Name = "cortarPiezasToolStripMenuItem";
            this.cortarPiezasToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.cortarPiezasToolStripMenuItem.Text = "Cortar Piezas";
            this.cortarPiezasToolStripMenuItem.Click += new System.EventHandler(this.cortarPiezasToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operarToolStripMenuItem,
            this.trasladosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1136, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // trasladosToolStripMenuItem
            // 
            this.trasladosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trasladoEstibaCompletaToolStripMenuItem});
            this.trasladosToolStripMenuItem.Name = "trasladosToolStripMenuItem";
            this.trasladosToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.trasladosToolStripMenuItem.Text = "Traslados";
            // 
            // trasladoEstibaCompletaToolStripMenuItem
            // 
            this.trasladoEstibaCompletaToolStripMenuItem.Name = "trasladoEstibaCompletaToolStripMenuItem";
            this.trasladoEstibaCompletaToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.trasladoEstibaCompletaToolStripMenuItem.Text = "Traslado estiba completa";
            this.trasladoEstibaCompletaToolStripMenuItem.Click += new System.EventHandler(this.trasladoEstibaCompletaToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEstatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 587);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1136, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblEstatus
            // 
            this.lblEstatus.Name = "lblEstatus";
            this.lblEstatus.Size = new System.Drawing.Size(118, 17);
            this.lblEstatus.Text = "toolStripStatusLabel1";
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 609);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMenu";
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.frmMenu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem operarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dividirPaqueteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cortarPiezasToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem trasladosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trasladoEstibaCompletaToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblEstatus;

    }
}

