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
    public partial class frmCortaPaquete : Form
    {
        int cont = 0;
        string vPerfil = "", vClase = "", vCategoria = "", vEspecificaciones = "", vAtado = "", vUnidadesAtado = "", vAntiguo = "";
        int posfin = 0, vPiezasDoble = 0;
        string codArticulo = "", vDesArticulo = "";
        clsConecta bd = new clsConecta();
        OracleConnection Conn = null;

        Dictionary<string, string> dicProducto =
            new Dictionary<string, string>();

        public frmCortaPaquete()
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
            MessageBox.Show(Mensaje, "Corta Paquetes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Divide Paquetes", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void limpiar()
        {
            this.txtEtiqueta.Text = string.Empty;
            this.txtPiezas.Text = string.Empty;
            this.lblCodProducto.Text = string.Empty;
            this.lblColada.Text = string.Empty;
            this.lblEtiqueta.Text = string.Empty;
            this.lblEtiquetaN1.Text = string.Empty;
            this.lblPeso.Text = string.Empty;
            this.lblPesoN1.Text = string.Empty;
            this.lblPiezas.Text = string.Empty;
            this.lblPiezasN1.Text = string.Empty;
            this.lblProducto.Text = string.Empty;
            this.lblSerie.Text = string.Empty;
            this.lblNorma.Text = string.Empty;
            this.lblFecha.Text = string.Empty;
            this.lblEtiquetaN2.Text = string.Empty;
            this.lblArticuloN1.Text = string.Empty;
            this.lblArticuloN2.Text = string.Empty;
            this.lblPesoN2.Text = string.Empty;
            this.lblPiezasN2.Text = string.Empty;
            this.lblEtiquetaN3.Text = string.Empty;
            this.lblArticuloN3.Text = string.Empty;
            this.lblPesoN3.Text = string.Empty;
            this.lblPiezasN3.Text = string.Empty;
            this.txtEtiqueta.Focus();
            this.grpDividir.Enabled = false;
            this.cboSupProducto.Enabled = false;
            this.txtPiezas.Enabled = false;
            this.cboSupProducto.Text = "";
            this.grpEtiqueta1.Visible = false;
            this.grpEtiqueta2.Visible = false;
            this.grpEtiqueta3.Visible = false;
        }

        private void frmCortaPaquete_Load(object sender, System.EventArgs e)
        {
            this.limpiar();
            this.txtEtiqueta.Focus();
        }

        private void llenacboSupProducto(string vParametro)
        {
            Cursor.Current = Cursors.WaitCursor;
            dicProducto.Clear();
            cboSupProducto.DataSource = null;
            cboSupProducto.Items.Clear();
            try
            {
                Conn = new OracleConnection();
                int vRen = 0;

                if(bd.Conecta(Conn))
                {
                    using (OracleDataReader drResul = clsNCorte.LlenaCboProducto(vParametro,Conn))
                    {
                        if(drResul.HasRows)
                        {
                            while (drResul.Read())
                            {
                                cboSupProducto.Items.Add(drResul["uniones"].ToString());
                                dicProducto.Add(drResul["NO_ARTI"].ToString(), drResul["DESCRIPCION"].ToString());
                                vRen++;
                            }
                        }
                        else
                        {
                            limpiar();
                        }

                    }
                }

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
                    using (OracleDataReader drTipos = clsNCorte.BusEtiqueta(vNoEtiqueta, Conn))
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
                                vUnidadesAtado = drTipos["UNIDADESLIO"].ToString();
                                vClase = drTipos["CLASE"].ToString();
                                vCategoria = drTipos["CATEGORIA"].ToString();
                                vEspecificaciones = drTipos["ESPECIFICACIONES"].ToString();
                                vAntiguo = drTipos["IND_ANTIGUO"].ToString();
                                fHabilita();
                                vRen++;

                            }
                        }
                        else
                        {
                            limpiar();
                            MessageBox.Show("Solo puede Ingresar etiquetas de longitud mayor a 30 pies", "Valida Etiquetas a Cortar", MessageBoxButtons.OK);
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

        private void fHabilita()
        {
            string vCondicion = "";
            vCondicion = " and CLASE = " + vClase + " " + vCondicion;
            vCondicion = " and CATEGORIA = '" + vCategoria + "' " + vCondicion;
            this.grpDividir.Enabled = true;
            this.txtPiezas.Enabled = true;
            this.cboSupProducto.Enabled = true;
            if (vEspecificaciones.Equals("NO"))
            {
                llenacboSupProducto(vCondicion);
            }
            else
            {
                llenacboSupProducto(vCondicion);
            }
            
        }

        

        public static string Right(string cadena, int length)
        {
            string result = cadena.Substring(cadena.Length - length, length);
            return result;
        }

        public static string Left(string buffer, int length)
        {
            string result = buffer.Substring(0, length);
            return result;
        }


        public static string Mid(string buffer, int startIndex, int length)
        {
            string result = buffer.Substring(startIndex, length);
            return result;
        }

        public static string Mid(string buffer, int startIndex)
        {
            string result = buffer.Substring(startIndex);
            return result;
        }

        private void txtPiezas_KeyPress(object sender, KeyPressEventArgs e)
        {
            erroricono.Clear();
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(this.txtPiezas.Text.Trim()))
                {
                    //if (Convert.ToInt32(txtPiezas.Text.Trim()) <= Convert.ToInt32(vUnidadesAtado.Trim()))
                    //{
                        if (Convert.ToInt32(txtPiezas.Text.Trim()) == Convert.ToInt32(lblPiezas.Text.Trim()))
                        {
                            if (vPerfil.Equals("1"))
                            {
                                vPiezasDoble = (Convert.ToInt32(txtPiezas.Text.Trim()) * 2);
                                if (vPiezasDoble > Convert.ToInt32(vUnidadesAtado.Trim()))
                                {
                                    this.GeneraEtiquetaDoble();
                                }
                                else
                                {
                                    this.GeneraNuevaEtiqueta();
                                }
                            }
                            else
                            {
                                //this.GeneraNuevaEtiquetaSql();
                            }
                        }
                        else
                        {
                            erroricono.SetError(txtPiezas, "Ingrese Piezas");
                            MessageBox.Show("Las piezas a Cortar deben ser Igual al total de piezas que tiene la etiqueta", "Valida Piezas");
                        }
                    //}
                    //else
                    //{
                    //    erroricono.SetError(txtPiezas, "Ingrese Piezas");
                    //    MessageBox.Show("Las piezas a Cortar deben ser menor a " + vUnidadesAtado, "Valida Piezas");
                    //}
                }
            }
        }

        private void cboSupProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            posfin = cboSupProducto.Text.IndexOf("]");
            if (cboSupProducto.Text != "") 
            {
                codArticulo = cboSupProducto.Text.Substring(1, posfin - 1);
                vDesArticulo = dicProducto[cboSupProducto.Text.Substring(1, posfin - 1)];
            }
        }

        private void GeneraEtiquetaDoble()
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
            this.RecalculaPesoPiezasDoble();
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

        private void RecalculaPesoPiezasDoble()
        {
            double vRstP = 0, vRstP2 = 0, vValorUnitario = 0, vValorUnitarioV = 0, vPzV = 0, vPesoV = 0, vEtiqueta3 = 0, vEtiqueta2 = 0, vResto = 0;
            vValorUnitarioV = (Convert.ToDouble(lblPeso.Text.Trim()) / Convert.ToDouble(lblPiezas.Text.Trim()));
            vValorUnitario = (Convert.ToDouble(lblPeso.Text.Trim()) / Convert.ToDouble(lblPiezas.Text.Trim()))/2;
            vRstP = vValorUnitario * Convert.ToInt32(vUnidadesAtado);
            vResto = (vPiezasDoble - Convert.ToInt32(vUnidadesAtado));
            vRstP2 = vValorUnitario * vResto;
            
            //etiqueta 1
            this.lblArticuloN1.Text = codArticulo;
            this.lblPesoN1.Text = Convert.ToString(vRstP);
            this.lblPiezasN1.Text = vUnidadesAtado;

            //etiqueta 2
            vEtiqueta2 = Convert.ToInt32(lblEtiquetaN1.Text.Trim()) + 1;
            this.lblEtiquetaN2.Text = Convert.ToString(vEtiqueta2);
            this.lblArticuloN2.Text = codArticulo;
            this.lblPesoN2.Text = Convert.ToString(vRstP2);
            this.lblPiezasN2.Text = Convert.ToString(vResto);


            //etiqueta 3
            vEtiqueta3 = Convert.ToInt32(lblEtiquetaN2.Text.Trim()) + 1;
            vPzV = Convert.ToInt32(lblPiezas.Text.Trim()) - Convert.ToInt32(txtPiezas.Text.Trim());
            vPesoV = vPzV * vValorUnitarioV;
            this.lblEtiquetaN3.Text = Convert.ToString(vEtiqueta3);
            this.lblArticuloN3.Text = lblCodProducto.Text;
            this.lblPesoN3.Text = Convert.ToString(vPesoV);
            this.lblPiezasN3.Text = Convert.ToString(vPzV);

            this.grpEtiqueta1.Visible = true;
            this.grpEtiqueta2.Visible = true;

            if (lblPiezasN3.Text == "0" && lblPesoN3.Text == "0")
            {
                this.grpEtiqueta3.Visible = false;
            }
            else
            {
                //this.grpEtiqueta3.Visible = true;
            }
        }

        private void RecalculaPesoPiezas()
        {
            double vRstP = 0, vValorUnitario, vPzV = 0, vPesoV = 0, vEtiqueta3 = 0;
            vValorUnitario = Convert.ToDouble(lblPeso.Text.Trim()) / Convert.ToDouble(lblPiezas.Text.Trim());
            vRstP = vValorUnitario * Convert.ToInt32(txtPiezas.Text.Trim());
            vPzV = Convert.ToInt32(lblPiezas.Text.Trim()) - Convert.ToInt32(txtPiezas.Text.Trim());
            vPesoV = vPzV * vValorUnitario;
            vEtiqueta3 = Convert.ToInt32(lblEtiquetaN1.Text.Trim()) + 1;

            // etiqueta 1
            this.lblPesoN1.Text = Convert.ToString(vRstP);
            this.lblPiezasN1.Text = Convert.ToString(vPiezasDoble);
            this.lblArticuloN1.Text = codArticulo;

            //etiqueta3
            this.lblEtiquetaN3.Text = Convert.ToString(vEtiqueta3);
            this.lblArticuloN3.Text = lblCodProducto.Text;
            this.lblPesoN3.Text = Convert.ToString(vPesoV);
            this.lblPiezasN3.Text = Convert.ToString(vPzV);

            this.grpEtiqueta1.Visible = true;
            //this.grpEtiqueta3.Visible = true;
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
            this.lblEtiquetaN1.Text = clsCNEtiquetas.EtiquetaSql();
            this.txtPiezas.Enabled = false;
            this.btnGrabar.Enabled = true;
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
                                lblEtiquetaN1.Text = rst["ticket"].ToString();
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

        private void BuscarNoEtiqueta(string vFecha, string vAtados)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Conn = new OracleConnection();
                int vRen = 0;

                if (bd.Conecta(Conn))
                {
                    using (OracleDataReader rst = clsCNEtiquetas.BuscaEtiquetaP(vFecha, vAtados, Conn))
                    {
                        if (rst.HasRows)
                        {
                            while (rst.Read())
                            {
                                lblEtiquetaN1.Text = rst["ticket"].ToString();
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            cont = 0;
            
            if (MessageBox.Show("Esta Seguro de Guardar los Cambios?", "Eliminar registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (lblEtiquetaN1.Text.Trim() != "" && Convert.ToDouble(lblPesoN1.Text) >= 1)
                {
                    this.Ejecuta(lblEtiqueta.Text,lblEtiquetaN1.Text,lblPesoN1.Text,lblPiezasN1.Text,lblArticuloN1.Text);
                }
                if (lblEtiquetaN2.Text.Trim() != "" && Convert.ToDouble(lblPesoN2.Text) >= 1)
                {
                    this.Ejecuta(lblEtiqueta.Text, lblEtiquetaN2.Text, lblPesoN2.Text, lblPiezasN2.Text, lblArticuloN2.Text);
                }
                //if (lblEtiquetaN3.Text.Trim() != "" && Convert.ToDouble(lblPesoN3.Text) >= 1)
                //{
                //    this.Ejecuta(lblEtiqueta.Text, lblEtiquetaN3.Text, lblPesoN3.Text, lblPiezasN3.Text, lblArticuloN3.Text);
                //}
                if (cont >= 1)
                {
                    this.ActualizaInventario("10", "SX", lblEtiqueta.Text, "C", "01");
                }
                else
                {
                    MessageBox.Show("No se Genero etiqueta Nueva", "Valida Piezas", MessageBoxButtons.OK);
                }
            this.limpiar();
            }
        }

        private void ActualizaInventario(string vCompania, string vDocu, string vEtiqueta, string vEstado, string vCentro)
        {
            try
            {
                string Rst = "";

                Rst = clsNCorte.UpdateInventario(vCompania, vDocu, vEtiqueta, vEstado, vCentro,clsSeguridad.Usuario);

                if (Rst.Equals("OK"))
                {
                    this.MensajeOK("Se insertó de forma correcta el registro");
                    this.btnGrabar.Enabled = false;
                }
                else
                {
                    this.MensajeError(Rst);
                    this.btnGrabar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Ejecuta(string vEtiV, string vEti, string vPeso, string vPiezas, string vCodArticulo)
        {
            try
            {
                string Rpta = "";

                Rpta = clsNCorte.Insertar(vEtiV,vEti,vPeso,vPiezas,vCodArticulo,clsSeguridad.Usuario);

                if (Rpta.Equals("OK"))
                {
                    cont++;
                    this.MensajeOK("Se insertó de forma correcta el registro");
                    this.btnGrabar.Enabled = false;
                    
                }
                else
                {
                    this.MensajeError(Rpta);
                    this.btnGrabar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


    }
}
