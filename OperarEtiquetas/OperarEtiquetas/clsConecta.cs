using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using System.Windows.Forms;

namespace OperarEtiquetas
{
    class clsConecta
    {
        public bool Conecta(OracleConnection Conn)
        {
            try
            {//hola
                string StringConexion = "Data Source = CORPACAM; User Id = NAF47; Password = NAF47;";

                if (Conn.State == System.Data.ConnectionState.Closed)
                {
                    Conn.ConnectionString = StringConexion;
                    Conn.Open();
                    return true;
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Error al intentar conectarse a la BD (método Conecta): " + ex.ErrorCode.ToString() + " - " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public void Desconecta(OracleConnection Conn)
        {
            Conn.Close();
            Conn = null;
        }
    }
}
