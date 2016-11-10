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
    class clsDCorte
    {
        private int _EtiquetaVieja;
        private int _Etiqueta1;
        private double _Peso1;
        private int _Pz1;
        private string articulo1;
        private string _Condicion;
        private string _Buscar;
        private OracleConnection _prmConn;
        private string _User;
        private string _Articulo;

        public string Articulo
        {
            get { return _Articulo; }
            set { _Articulo = value; }
        }

        public int EtiquetaVieja
        {
            get { return _EtiquetaVieja; }
            set { _EtiquetaVieja = value; }
        }

        public int Etiqueta1
        {
            get { return _Etiqueta1; }
            set { _Etiqueta1 = value; }
        }

        public double Peso1
        {
            get { return _Peso1; }
            set { _Peso1 = value; }
        }

        public int Pz1
        {
            get { return _Pz1; }
            set { _Pz1 = value; }
        }

        public string Articulo1
        {
            get { return articulo1; }
            set { articulo1 = value; }
        }

        public string Condicion
        {
            get { return _Condicion; }
            set { _Condicion = value; }
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

        public string User
        {
            get { return _User; }
            set { _User = value; }
        }
       
        //constructores
        public clsDCorte()
        {

        }

        public clsDCorte(int vEtiquetaV, int vEtiqueta1, double vPeso1, int vPz1, string vArti1, string vCondicion, string vBuscar, OracleConnection vCoon, string vUsuario, string vArticulo)
        {
            this.EtiquetaVieja = vEtiquetaV;
            this.Etiqueta1 = vEtiqueta1;
            this.Peso1 = vPeso1;
            this.Pz1 = vPz1;
            this.articulo1 = vArti1;
            this.Condicion = vCondicion;
            this.Buscar = vBuscar;
            this.PrmConn = vCoon;
            this.User = vUsuario;
            this.Articulo = vArticulo;
        }

        public OracleDataReader Busquedas(clsDCorte Parametro)
        {
            string query = "";

            OracleDataReader drQuery = null;
            try
            {
                switch (Parametro.Condicion)
                {
                    case "LlenaEtiqueta":
                        query = "select e.NO_ETIQUETA,e.NO_ARTI_PRODUCIDO,p.descripcion,e.CANTIDAD_PRODUCCION as piezas,e.PESO,nvl(e.SERIE_PRODUCCION,'No tiene') as serie,e.COLADA," +
                                   "nvl(p.NORMA,'No Tiene') as norma,to_char(e.FECHA_PRODUCCION,'dd/mm/yyyy') as FECHA_PRODUCCION ,p.es_perfil,p.UNIDADESLIO, " +
                                   " p.CLASE,p.CATEGORIA,nvl(p.ESPECIFICACIONES,'NO') AS ESPECIFICACIONES, e.IND_ANTIGUO  " +
                         " from ARIN_ETIQUETAS e, ARINDA p  " +
                         " where (e.no_etiqueta = '" + Parametro.Buscar + "' or e.Etiqueta = '" + Parametro.Buscar + "') " +
                         " and e.estado = 'A' " +
                         " and e.no_cia = '10' " +
                         " and p.no_cia = e.no_cia " +
                         " and p.no_arti = e.no_arti_producido " +
                         " and e.origen = 'L' " +
                         "  and (p.LONGITUD = 40 or p.LONGITUD = 30) ";
                        
                        break;
                    case "LlenacboProductos":
                        query = "select '['||NO_ARTI||']  '||DESCRIPCION as uniones, NO_ARTI, DESCRIPCION " +
                            "FROM ARINDA " +
                            "Where NO_CIA = '10' " +
                            "and LONGITUD = 20 " + Parametro.Buscar + 
                            " ORDER by NO_ARTI ASC";
                        break;
                    case "BuscaAtado":
                        query = "SELECT COUNT(*) + 1 as atados FROM arin_etiquetas WHERE TRUNC(fecha_produccion) = TO_DATE('" + Parametro.Buscar + "','dd/mm/yyyy')";
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

        public string UpInventario(clsDCorte paquete)
        {
            string rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                OracleCommand SqlCmd = new OracleCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "SP_REBAJA_ETIQUETA_CORATADA";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("pCia", paquete.Articulo1);
                SqlCmd.Parameters.Add("pTipodocr", paquete.Buscar);
                SqlCmd.Parameters.Add("pEtiqueta", paquete.EtiquetaVieja);
                SqlCmd.Parameters.Add("pEstado", paquete.Condicion);
                SqlCmd.Parameters.Add("pCentro", paquete.User);
                SqlCmd.Parameters.Add("pUsuario", paquete.Articulo);
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

        public string Insertar(clsDCorte Paquete)
        {
            string rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                //Establecer el comando que permite ejecutar sentencias en base de datos
                OracleCommand SqlCmd = new OracleCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "Sp_IngresaEtiquetaCortada";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("prmEtiquetaV", Paquete.EtiquetaVieja);
                SqlCmd.Parameters.Add("prmEtiquetaN", Paquete.Etiqueta1);
                SqlCmd.Parameters.Add("prmPeso", Paquete.Peso1);
                SqlCmd.Parameters.Add("prmPz", Paquete.Pz1);
                SqlCmd.Parameters.Add("prmArticulo", Paquete.articulo1);
                SqlCmd.Parameters.Add("prmUser", Paquete.User);
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
