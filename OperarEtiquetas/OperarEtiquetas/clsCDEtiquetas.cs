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
    class clsCDEtiquetas
    {

        private int _EtiquetaVieja;
        private int _EtiquetaNueva;
        private double _PesoViejo;
        private double _PesoNuevo;
        private int _PiezasViejas;
        private int _PiezasNuevas;
        private string _Condicion;
        private string _Buscar;
        private OracleConnection _prmConn;
        private string _Usuario;

        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }


        public int PiezasViejas
        {
            get { return _PiezasViejas; }
            set { _PiezasViejas = value; }
        }
        

        public int PiezasNuevas
        {
            get { return _PiezasNuevas; }
            set { _PiezasNuevas = value; }
        }

        public int EtiquetaVieja
        {
            get { return _EtiquetaVieja; }
            set { _EtiquetaVieja = value; }
        }


        public int EtiquetaNueva
        {
            get { return _EtiquetaNueva; }
            set { _EtiquetaNueva = value; }
        }

        

        public double PesoNuevo
        {
            get { return _PesoNuevo; }
            set { _PesoNuevo = value; }
        }

        public double PesoViejo
        {
            get { return _PesoViejo; }
            set { _PesoViejo = value; }
        }

        public string Buscar
        {
            get { return _Buscar; }
            set { _Buscar = value; }
        }

        public OracleConnection PrmConn
        {
            get { return _prmConn; }
            set { _prmConn = value; }
        }

        public string Condicion
        {
            get { return _Condicion; }
            set { _Condicion = value; }
        }

        public clsCDEtiquetas()
        {

        }

        public clsCDEtiquetas(int vEtiquetaV, int vEtiquetaN, double vPesoV, double vPesoN, int vPzV, int vPzN,string vCondicion,string vBuscar,OracleConnection vConn, string vUser)
        {
            this.EtiquetaVieja = vEtiquetaV;
            this.EtiquetaNueva = vEtiquetaN;
            this.PesoViejo = vPesoV;
            this.PesoNuevo = vPesoN;
            this.PiezasViejas = vPzV;
            this.PiezasNuevas = vPzN;
            this.Condicion = vCondicion;
            this.Buscar = vBuscar;
            this.PrmConn = vConn;
            this.Usuario = vUser;
        }

        public string Insertar(clsCDEtiquetas Etiqueta)
        {
            string rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon.ConnectionString  =  Conexion.Cn;
                SqlCon.Open();
                //Establecer el comando que permite ejecutar sentencias en base de datos
                OracleCommand SqlCmd = new OracleCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "Sp_DividePaquete";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("prmEtiquetaV", Etiqueta.EtiquetaVieja);
                SqlCmd.Parameters.Add("prmEtiquetaN", Etiqueta.EtiquetaNueva);
                SqlCmd.Parameters.Add("prmPesoV", Etiqueta.PesoViejo);
                SqlCmd.Parameters.Add("prmPesoN", Etiqueta.PesoNuevo);
                SqlCmd.Parameters.Add("prmPzV", Etiqueta.PiezasViejas);
                SqlCmd.Parameters.Add("prmPzN", Etiqueta.PiezasNuevas);
                SqlCmd.Parameters.Add("prmUsers", Etiqueta.Usuario);
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

        public OracleDataReader Busquedas(clsCDEtiquetas Parametro)
        {
            string query = "";

            OracleDataReader drQuery = null;
            try
            {
                switch (Parametro.Condicion)
                {
                    case "LlenaEtiqueta":
                        query = "select e.NO_ETIQUETA,e.NO_ARTI_PRODUCIDO,p.descripcion,e.CANTIDAD_PRODUCCION as piezas,e.PESO,nvl(e.SERIE_PRODUCCION,'No tiene') as serie,e.COLADA," +
                                   "nvl(p.NORMA,'No Tiene') as norma,to_char(e.FECHA_PRODUCCION,'dd/mm/yyyy') as FECHA_PRODUCCION ,p.es_perfil, e.IND_ANTIGUO " +
                         " from ARIN_ETIQUETAS e, ARINDA p  " +
                         " where (e.no_etiqueta = '" + Parametro.Buscar + "' or e.Etiqueta = '" + Parametro.Buscar + "') " +
                         " and e.estado = 'A' " +
                         " and e.no_cia = '10' " +
                         " and p.no_cia = e.no_cia " +
                         " and p.no_arti = e.no_arti_producido " +
                         " and e.origen = 'L' ";
                        break;
                    case "BuscaEtiquetaP":
                        query = Parametro.Buscar;
                        break;
                    case "BuscaAtado":
                        query = "SELECT COUNT(*) + 1 as atados FROM arin_etiquetas WHERE TRUNC(fecha_produccion) = TO_DATE('" + Parametro.Buscar + "','dd/mm/yyyy')";
                        break;
                    case "NexEtiqueta":
                        query = "SELECT MAX(NO_ETIQUETA) + 1 as ticket FROM arin_etiquetas WHERE TRUNC(fecha_produccion) = TO_DATE('" + Parametro.Buscar + "','dd/mm/yyyy')";
                        break;
                }

                OracleCommand OraCommand = new OracleCommand(query, Parametro.PrmConn);
                drQuery = OraCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                drQuery = null;
            }
            return drQuery;
        }

        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("etiquetas");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = ConexionSQL.Cn;
                SqlCon.Close();
                SqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "dbo.Sp_DividePaquete";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
                //SqlCon.Close();
            }
            catch (Exception ex)
            {
                DtResultado = null;
                SqlCon.Close();
            }
            return DtResultado;
        }
    }
}
