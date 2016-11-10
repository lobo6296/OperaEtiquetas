using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Windows.Forms.Design;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;

namespace OperarEtiquetas
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void dividirPaqueteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmDividePaquetes ControlT = new frmDividePaquetes();
            if (bFormularioAbierto(ControlT) == true) { return; }
            ControlT.MdiParent = this;
            ControlT.Show();
            Cursor.Current = Cursors.Default;
        }

        private bool bFormularioAbierto(Form formulario)
        {
            bool cargado = false;

            foreach (Form frmBusca in this.MdiChildren)
            {
                if (frmBusca.Text == formulario.Text)
                {
                    frmBusca.Focus();
                    cargado = true;
                    break;
                }
            }
            Cursor.Current = Cursors.Default;
            return cargado;
        }

        private void cortarPiezasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmCortaPaquete Corte = new frmCortaPaquete();
            if (bFormularioAbierto(Corte) == true) { return; }
            Corte.MdiParent = this;
            Corte.Show();
            Cursor.Current = Cursors.Default;
        }

        private void trasladoEstibaCompletaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmEstibaCompleta Estiba = new frmEstibaCompleta();
            if (bFormularioAbierto(Estiba) == true) { return; }
            Estiba.MdiParent = this;
            Estiba.Show();
            Cursor.Current = Cursors.Default;
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            string vCondicion = "";
            vCondicion = "Usuario: " + clsSeguridad.Usuario;
            vCondicion = vCondicion + ", Codigo Usuario: " + clsSeguridad.Empleado;
            lblEstatus.Text = vCondicion;
        }
    }
}
