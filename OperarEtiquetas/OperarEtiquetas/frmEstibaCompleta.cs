using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.Globalization;
using Oracle.DataAccess.Client;
using System.IO;
using System.Collections;

namespace OperarEtiquetas
{
    public partial class frmEstibaCompleta : Form
    {
        clsUtilerias utilerias = new clsUtilerias();
        clsConecta bd = new clsConecta();
        OracleConnection Conn = null;
        int vAtados = 0;

        public frmEstibaCompleta()
        {
            InitializeComponent();
        }

        #region Mensajes
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema Bascula", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Mostrar Mensaje de Confirmacion
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema Bascula", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        private void frmEstibaCompleta_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.txtEstibaV, "Ingrese Estiba");
            toolTip1.SetToolTip(this.txtLineaV, "Ingrese Linea");
            toolTip1.SetToolTip(this.txtPuntoV, "Ingrese Punto");
            toolTip1.SetToolTip(this.txtEstibaN, "Ingrese Estiba Nueva");
            toolTip1.SetToolTip(this.txtLineaN, "Ingrese Linea Nueva");
            toolTip1.SetToolTip(this.txtPuntoN, "Ingrese Punto Nuevo");
            Limpiar();
        }

        private void Limpiar()
        {
            this.txtEstibaV.Text = string.Empty;
            this.txtLineaV.Text = string.Empty;
            this.txtPuntoV.Text = string.Empty;
            this.txtEstibaN.Text = string.Empty;
            this.txtLineaN.Text = string.Empty;
            this.txtPuntoN.Text = string.Empty;
            grpNueva.Enabled = false;
            grpVieja.Enabled = true;
        }

        private void btnValida_Click(object sender, EventArgs e)
        {
            if (Valida())
            {
                if (CuentaPaquetes(txtEstibaV.Text.Trim(), txtLineaV.Text.Trim(), txtPuntoV.Text.Trim()))
                {
                    grpNueva.Enabled = true;
                    MessageBox.Show("La estiba cuenta con " + vAtados + " Atados.", "Valida Atados");
                    grpVieja.Enabled = false;
                }
                else
                {
                    MessageBox.Show("La estiba no existe.", "Valida Atados");
                }
            }
        }

        private bool CuentaPaquetes(string vEstiba, string vLinea, string vPunto)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Conn = new OracleConnection();
                int vRen = 0;

                if (bd.Conecta(Conn))
                {
                    using (OracleDataReader rst = clsNTraslados.BusEtiqueta(vEstiba,vLinea,vPunto, Conn))
                    {
                        if (rst.HasRows)
                        {
                            while (rst.Read())
                            {
                                vAtados = Convert.ToInt32(rst["atados"].ToString());
                                vRen++;   
                            }
                        }
                    }
                }
                bd.Desconecta(Conn);

                if (vAtados >= 1)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                return false;
                MessageBox.Show(e.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Valida()
        {
            errorIcon.Clear();
            string campo_falta = "";

            if (this.txtEstibaV.Text == string.Empty)
            {
                campo_falta = " Falta de Estiba ";
                errorIcon.SetError(txtEstibaV, "Ingrese la Estiba");
            }
            else errorIcon.Clear();
            if (this.txtLineaV.Text == string.Empty)
            {
                campo_falta = " Falta Linea";
                errorIcon.SetError(txtLineaV, "Ingrese la Linea");
            }
            else errorIcon.Clear();
            if (this.txtPuntoV.Text == string.Empty)
            {
                campo_falta = " Falta Punto";
                errorIcon.SetError(txtPuntoV, "Ingrese el Punto");
            }
            else errorIcon.Clear();
            if (campo_falta != "")
            {
                //MensajeError("Falta ingresar algunos datos, serán remarcados");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void txtEstibaV_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || Convert.ToInt32(e.KeyChar) == 8 || (int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Solo se permiten números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void txtLineaV_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || Convert.ToInt32(e.KeyChar) == 8 || (int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Solo se permiten números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void txtPuntoV_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || Convert.ToInt32(e.KeyChar) == 8 || (int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Solo se permiten números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void txtEstibaV_KeyUp(object sender, KeyEventArgs e)
        {
            if (utilerias.IsNumeric(this.txtEstibaV.Text.Trim()))
            {
            }
            else
            {
                MessageBox.Show("Solo se permiten números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtEstibaV.Text = string.Empty;
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (ValidaMaestro())
            {
                this.Ejecuta(txtEstibaV.Text.Trim(),txtLineaV.Text.Trim(),txtPuntoV.Text.Trim(),txtEstibaN.Text.Trim(),txtLineaN.Text.Trim(),txtPuntoN.Text.Trim(),clsSeguridad.Usuario);
            }
        }

        private void MensajeOK(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Corta Paquetes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //private void MensajeError(string Mensaje)
        //{
        //    MessageBox.Show(Mensaje, "Divide Paquetes", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}


        private void Ejecuta(string vEstibaV,string vLineaV, string vPuntoV, string vEstibaN, string vLineaN, string vPuntoN, string vUsuario)
        {
            try
            {
                Conn = new OracleConnection();

                if (bd.Conecta(Conn))
                {
                    string Resul = "";
                    Resul = clsNTraslados.Inserta(vEstibaV, vLineaV, vPuntoV, vEstibaN, vLineaN, vPuntoN, clsSeguridad.Usuario, Conn);

                    if (Resul.Equals("OK"))
                    {
                       
                        this.MensajeOK("Se insertó de forma correcta el registro");
                        this.btnGrabar.Enabled = false;
                        this.Limpiar();
                    }
                    else
                    {
                        this.MensajeError(Resul);
                        this.btnGrabar.Enabled = false;
                    }
                }
                bd.Desconecta(Conn);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidaMaestro()
        {
            errorIcon.Clear();
            string campo_falta = "";

            if (this.txtEstibaV.Text == string.Empty)
            {
                campo_falta = " Falta de Estiba ";
                errorIcon.SetError(txtEstibaN, "Ingrese la Estiba Nueva");
            }
            else errorIcon.Clear();
            if (this.txtLineaV.Text == string.Empty)
            {
                campo_falta = " Falta Linea";
                errorIcon.SetError(txtLineaN, "Ingrese la Linea Nueva");
            }
            else errorIcon.Clear();
            if (this.txtPuntoV.Text == string.Empty)
            {
                campo_falta = " Falta Punto";
                errorIcon.SetError(txtPuntoN, "Ingrese el Punto Nuevo");
            }
            else errorIcon.Clear();
            if (campo_falta != "")
            {
                //MensajeError("Falta ingresar algunos datos, serán remarcados");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void txtEstibaN_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || Convert.ToInt32(e.KeyChar) == 8 || (int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Solo se permiten números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void txtLineaN_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || Convert.ToInt32(e.KeyChar) == 8 || (int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Solo se permiten números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void txtPuntoN_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || Convert.ToInt32(e.KeyChar) == 8 || (int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Solo se permiten números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }
    }
}
