using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;

namespace OperarEtiquetas
{
    class clsDTraslados
    {
        private int _EstibaV;
        private int _LineaV;
        private int _PuntoV;
        private int _EstibaN;
        private int _LineaN;
        private int _PuntoN;
        private string _Busca;
        private string _Condicion;
        private string _User;
        private OracleConnection _prmConn;

        public OracleConnection PrmConn
        {
            get { return _prmConn; }
            set { _prmConn = value; }
        }

        public string User
        {
            get { return _User; }
            set { _User = value; }
        }
        private OracleConnection _Conn;

        public string Condicion
        {
            get { return _Condicion; }
            set { _Condicion = value; }
        }
        

        public string Busca
        {
            get { return _Busca; }
            set { _Busca = value; }
        }
        

        public int PuntoN
        {
            get { return _PuntoN; }
            set { _PuntoN = value; }
        }
        

        public int LineaN
        {
            get { return _LineaN; }
            set { _LineaN = value; }
        }
        

        public int EstibaN
        {
            get { return _EstibaN; }
            set { _EstibaN = value; }
        }
        

        public int PuntoV
        {
            get { return _PuntoV; }
            set { _PuntoV = value; }
        }
        

        public int LineaV
        {
            get { return _LineaV; }
            set { _LineaV = value; }
        }
        

        public int EstibaV
        {
            get { return _EstibaV; }
            set { _EstibaV = value; }
        }

        public clsDTraslados()
        {

        }

        public clsDTraslados(int vEstibaV, int vLineaV, int vPuntoV, int vEstibaN, int vLineaN, int vPuntoN, string vBuscar,
            string vCondicion, string vUser, OracleConnection prmConn)
        {
            this.EstibaV = vEstibaV;
            this.LineaV = vLineaV;
            this.PuntoV = vPuntoV;
            this.EstibaN = vEstibaN;
            this.LineaN = vLineaN;
            this.PuntoN = vPuntoN;
            this.Busca = vBuscar;
            this.Condicion = vCondicion;
            this.User = vUser;
            this.PrmConn = PrmConn;
        }

        public OracleDataReader Busquedas(clsDTraslados Parameter)
        {
            string query = "";
            OracleDataReader drQuery = null;
            try
            {
                switch (Parameter.Condicion)
                {
                    case "CuentaAtados":
                        query = "SELECT nvl(COUNT(*),0) Atados FROM ARIN_ETIQUETAS " +
                                 "Where RECIBIDO = 'S' and ESTADO = 'A' and ESTIBA = " + Parameter.EstibaV + " and LINEA = " + Parameter.LineaV + " and PUNTO = " + Parameter.PuntoV + "";

                        break;
                    
                }

                OracleCommand OraCommand = new OracleCommand(query, Parameter.PrmConn);
                drQuery = OraCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                drQuery = null;
            }
            return drQuery;
        }

        public string Insertar(clsDTraslados Parameters)
        {
            string rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                //SqlCon.ConnectionString = Conexion.Cn;
                //SqlCon.Open();
                //Establecer el comando que permite ejecutar sentencias en base de datos
                OracleCommand SqlCmd = new OracleCommand();
                //SqlCmd.Connection = SqlCon;
                SqlCmd.Connection = Parameters.PrmConn;
                SqlCmd.CommandText = "Sp_Trasladaestiba";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("prmEstibaV", Parameters.EstibaV);
                SqlCmd.Parameters.Add("prmLineaV", Parameters.LineaV);
                SqlCmd.Parameters.Add("prmPuntoV", Parameters.PuntoV);
                SqlCmd.Parameters.Add("prmEstibaN", Parameters.EstibaN);
                SqlCmd.Parameters.Add("prmLineaN", Parameters.LineaN);
                SqlCmd.Parameters.Add("prmPuntoN", Parameters.PuntoN);
                //SqlCmd.Parameters.Add("prmUser", Parameters.User);
                rpta = SqlCmd.ExecuteNonQuery() == -1 ? "OK" : "No se Pudo Almacenar";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;
        }
        
    }
}
