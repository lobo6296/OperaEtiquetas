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
using Oracle.DataAccess.Client;

namespace OperarEtiquetas
{
    public partial class frmLogin : Form
    {
        clsConecta bd = new clsConecta();
        clsSeguridad seguridad = new clsSeguridad();
        clsUtilerias utilerias = new clsUtilerias();
        OracleConnection Conn = null;

        string Ip = "";//ip 

        public frmLogin()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            bd.Desconecta(Conn);
            this.Close();
            Application.Exit();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.lblhora.Text = DateTime.Now.ToLongTimeString();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Timer1.Enabled = true;
            string vUsuario = Environment.UserName;
            this.txtlogin.Text = vUsuario;
            this.txtpassword.Focus();
            //this.txtUser.Text = "admin";
            //this.txtPassword.Text = "corp@";
            Conn = new OracleConnection();

            bd.Conecta(Conn);
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            Acceder();
        }

        private void Acceder()
        {
            errorLogin.Clear();
            if (this.txtlogin.Text == "" || this.txtpassword.Text == "")
            {
                errorLogin.SetError(txtpassword, "Ingrese Contraseña");
                MessageBox.Show("Complete los campos", "Validación...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (ValidaUser(this.txtlogin.Text.Trim().ToUpper(), this.txtpassword.Text.Trim().ToUpper()))//VALIDA EN EL ESQUEMA SEGURIDAD
            {
                            this.Timer1.Enabled = false;
                            frmMenu mdi = new frmMenu();
                            Hide();
                            mdi.ShowDialog();
                            Close();
            }
        }

        private bool ValidaUser(string vUsuario, string vClave)
        {
            string query = "";
            try
            {
                if (Conn.State == System.Data.ConnectionState.Closed) { bd.Conecta(Conn); }
                if (vUsuario != "" && vClave != "")
                {
                    query = "SELECT USUARIO,CLAVE,NO_EMPLEADO " +
                         "FROM naf47.ARIMROLES_USUARIOS " +
                         "Where OperaEtiqueta = 'S'  AND USUARIO = '" + vUsuario + "' and CLAVE = '" + vClave + "' ";

                    OracleCommand cmdUsuario = new OracleCommand(query , Conn);

                    OracleDataReader drUsuario = cmdUsuario.ExecuteReader();

                    if (drUsuario.Read())
                    {
                        clsSeguridad.Usuario = drUsuario["USUARIO"].ToString(); 
                        clsSeguridad.Empleado = drUsuario["NO_EMPLEADO"].ToString();
                        clsSeguridad.Contrasenia = drUsuario["CLAVE"].ToString();

                    }
                    else
                    {
                        MessageBox.Show("Usuario no válido", "Validación...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        bd.Desconecta(Conn);
                        return false;
                    }
                    bd.Desconecta(Conn);
                    return true;
                }
                else
                {
                    MessageBox.Show("Escriba su nombre de Usuario y Contraseña", "Validación...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Valida Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor.Current = Cursors.Default;
                return false;
            }

        }

        private void txtlogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Acceder();
            }
        }

        private void txtpassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                Acceder();
            }
        }


    }
}
