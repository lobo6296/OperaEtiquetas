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
using System.Data.SqlClient;

namespace OperarEtiquetas
{
    public partial class frmDividePaquetes : Form
    {
        string vPerfil = "", vAntiguo = "";
        string vAtado = "";

        clsCNEtiquetas CapaConsulta = new clsCNEtiquetas();
        clsConecta bd = new clsConecta();
        OracleConnection Conn = null;

        public frmDividePaquetes()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtEtiqueta.Text.Trim()))
            {
                this.BuscarEtiqueta(this.txtEtiqueta.Text.Trim());
            }
            
        }

        private void MensajeOK(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Divide Paquetes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Para mostrar mensaje de error
        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Divide Paquetes", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BuscaNextEtiqueta(string vFecha)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Conn = new OracleConnection();
                int vRen = 0;

                if (bd.Conecta(Conn))
                {
                    using (OracleDataReader rst = clsCNEtiquetas.BuscaNextEtiqueta(vFecha, Conn))
                    {
                        if (rst.HasRows)
                        {
                            while (rst.Read())
                            {
                                lblEtiquetaN.Text = rst["ticket"].ToString();
                                this.txtPiezas.Enabled = false;
                                this.btnGrabar.Enabled = true;
                                vRen++;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscaAtado(string vFecha)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Conn = new OracleConnection();
                int vRen = 0;

                if (bd.Conecta(Conn))
                {
                    using (OracleDataReader rst = clsCNEtiquetas.BuscaAtado(vFecha, Conn))
                    {
                        if (rst.HasRows)
                        {
                            while (rst.Read())
                            {
                                vAtado = rst["atados"].ToString();
                                vRen++;
                            }
                        }
                    }
                }
                bd.Desconecta(Conn);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarNoEtiquetaSql()
        {
            this.lblEtiquetaN.Text = clsCNEtiquetas.EtiquetaSql();
            this.txtPiezas.Enabled = false;
            this.btnGrabar.Enabled = true;
        }

        private void BuscarNoEtiqueta(string vFecha, string vAtados)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Conn = new OracleConnection();
                int vRen = 0;

                if (bd.Conecta(Conn))
                {
                     using (OracleDataReader rst = clsCNEtiquetas.BuscaEtiquetaP(vFecha,vAtados, Conn))
                    {
                        if (rst.HasRows)
                        {
                            while (rst.Read())
                            {
                                lblEtiquetaN.Text = rst["ticket"].ToString();
                                this.txtPiezas.Enabled = false;
                                this.btnGrabar.Enabled = true;
                                vRen++;
                            }
                        }
                    }
                }
                bd.Desconecta(Conn);
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarEtiqueta(string vNoEtiqueta)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                btnGrabar.Enabled = false;
                Conn = new OracleConnection();
                int vRen = 0;

                if (bd.Conecta(Conn))
                {
                    using (OracleDataReader drTipos = clsCNEtiquetas.BusEtiqueta(vNoEtiqueta, Conn))
                    {
                        if (drTipos.HasRows)
                        {
                            while (drTipos.Read())
                            {
                                lblEtiqueta.Text = drTipos["NO_ETIQUETA"].ToString();
                                lblCodProducto.Text = drTipos["NO_ARTI_PRODUCIDO"].ToString();
                                lblProducto.Text = drTipos["descripcion"].ToString();
                                lblPiezas.Text = drTipos["piezas"].ToString();
                                lblPeso.Text = drTipos["peso"].ToString();
                                lblSerie.Text = drTipos["serie"].ToString();
                                lblColada.Text = drTipos["COLADA"].ToString();
                                lblNorma.Text = drTipos["norma"].ToString();
                                lblFecha.Text = drTipos["FECHA_PRODUCCION"].ToString();
                                vPerfil = drTipos["es_perfil"].ToString();
                                vAntiguo = drTipos["IND_ANTIGUO"].ToString();
                                this.grpDividir.Enabled = true;
                                this.txtPiezas.Enabled = true;
                                vRen++;

                            }
                        }
                        else { limpiar(); }
                    }
                }
                bd.Desconecta(Conn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDividePaquetes_Load(object sender, EventArgs e)
        {
            this.limpiar();
            this.txtEtiqueta.Focus();
        }

        private void limpiar()
        {
            this.txtEtiqueta.Text = string.Empty;
            this.txtPiezas.Text = string.Empty;
            this.lblCodProducto.Text = string.Empty;
            this.lblColada.Text = string.Empty;
            this.lblEtiqueta.Text = string.Empty;
            this.lblEtiquetaN.Text = string.Empty;
            this.lblPeso.Text = string.Empty;
            this.lblPesoN.Text = string.Empty;
            this.lblPiezas.Text = string.Empty;
            this.lblPiezasN.Text = string.Empty;
            this.lblProducto.Text = string.Empty;
            this.lblSerie.Text = string.Empty;
            this.lblNorma.Text = string.Empty;
            this.lblFecha.Text = string.Empty;
            this.txtEtiqueta.Focus();
            this.grpDividir.Enabled = false;
        }

        private void txtEtiqueta_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(this.txtEtiqueta.Text.Trim()))
                {
                    this.BuscarEtiqueta(this.txtEtiqueta.Text.Trim());
                }
            }
        }

        private void btnLimpiar_Click(object sender, System.EventArgs e)
        {
            this.limpiar();
            this.txtEtiqueta.Focus();
        }

        private void txtPiezas_KeyPress(object sender, KeyPressEventArgs e)
        {
            erroricono.Clear();
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(this.txtPiezas.Text.Trim()))
                {
                    if (Convert.ToInt32(txtPiezas.Text.Trim()) < Convert.ToInt32(lblPiezas.Text.Trim()))
                    {
                        if (vPerfil.Equals("1"))
                        {
                            this.GeneraNuevaEtiqueta();
                        }
                        else
                        {
                            this.GeneraNuevaEtiquetaSql();
                        }
                    }
                    else
                    {
                        erroricono.SetError(txtPiezas, "Ingrese Piezas");
                        MessageBox.Show("Las piezas a Dividir deben ser menor a " + lblPiezas, "Valida Piezas", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void RecalculaPesoPiezas()
        {
            double vRstP = 0, vValorUnitario, vPzV = 0, vPesoV = 0;
            vValorUnitario = Convert.ToDouble(lblPeso.Text.Trim()) / Convert.ToDouble(lblPiezas.Text.Trim());
            vRstP = vValorUnitario * Convert.ToInt32(txtPiezas.Text.Trim());
            this.lblPesoN.Text = Convert.ToString(vRstP);
            this.lblPiezasN.Text = this.txtPiezas.Text.Trim();
            vPzV = Convert.ToInt32(lblPiezas.Text.Trim()) - Convert.ToInt32(txtPiezas.Text.Trim());
            vPesoV = vPzV * vValorUnitario;
            this.lblPiezas.Text = Convert.ToString(vPzV);
            this.lblPeso.Text = Convert.ToString(vPesoV);
        }

        private void GeneraNuevaEtiqueta()
        {
            if (vAntiguo.Equals("S"))
            {
                this.BuscaNextEtiqueta(lblFecha.Text.Trim());
            }
            else
            {
                this.BuscaAtado(lblFecha.Text.Trim());
                this.BuscarNoEtiqueta(lblFecha.Text.Trim(), vAtado);
            }
            this.RecalculaPesoPiezas();
       }


        private void GeneraNuevaEtiquetaSql()
        {
            this.BuscarNoEtiquetaSql();
            this.RecalculaPesoPiezas();
        }

        private void btnGrabar_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Esta Seguro de Guardar los Cambios?", "Eliminar registro",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (lblEtiquetaN.Text.Trim() != "")
                {
                    this.Ejecuta();
                }
                else
                {
                    MessageBox.Show("No se Genero etiqueta Nueva", "Valida Piezas", MessageBoxButtons.OK);
                }
            }
        }

        private void Ejecuta()
        {
            try
            {
                string Rpta = "";

                Rpta = clsCNEtiquetas.Insertar(this.lblEtiqueta.Text, this.lblEtiquetaN.Text, lblPeso.Text, lblPesoN.Text, lblPiezas.Text, lblPiezasN.Text,clsSeguridad.Usuario);

                if (Rpta.Equals("OK"))
                {
                        this.MensajeOK("Se insertó de forma correcta el registro");
                        this.btnGrabar.Enabled = false;
                        this.limpiar();
                }
                else
                {
                    this.MensajeError(Rpta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



    }
}
