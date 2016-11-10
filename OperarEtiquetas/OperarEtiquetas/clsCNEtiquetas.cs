using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace OperarEtiquetas
{
    class clsCNEtiquetas
    {
        clsCDEtiquetas CapaDatos = new clsCDEtiquetas();

        public static OracleDataReader BusEtiqueta(string prmEtiqueta, OracleConnection prmConn)
        {
            clsCDEtiquetas Obj = new clsCDEtiquetas();
            Obj.Buscar = prmEtiqueta;
            Obj.PrmConn = prmConn;
            Obj.Condicion = "LlenaEtiqueta";
            return Obj.Busquedas(Obj);
        }

        public static OracleDataReader BuscaEtiquetaP(string prmFecha, string prmAtado, OracleConnection prmConn)
        {
            string vCondicion = "";
            vCondicion = "SELECT numero_ticket('" + prmFecha + "'," + prmAtado + ") as ticket from dual";
            clsCDEtiquetas Obj = new clsCDEtiquetas();
            Obj.Buscar = vCondicion;
            Obj.PrmConn = prmConn;
            Obj.Condicion = "BuscaEtiquetaP";
            return Obj.Busquedas(Obj);
        }

        public static OracleDataReader BuscaAtado(string prmFecha, OracleConnection prmConn)
        {
            clsCDEtiquetas Obj = new clsCDEtiquetas();
            Obj.Buscar = prmFecha;
            Obj.PrmConn = prmConn;
            Obj.Condicion = "BuscaAtado";
            return Obj.Busquedas(Obj);
        }

        public static OracleDataReader BuscaNextEtiqueta(string prmFecha, OracleConnection prmConn)
        {
            clsCDEtiquetas Obj = new clsCDEtiquetas();
            Obj.Buscar = prmFecha;
            Obj.PrmConn = prmConn;
            Obj.Condicion = "NexEtiqueta";
            return Obj.Busquedas(Obj);
        }

        public static string EtiquetaSql()
        {
            string numero = "";
            DataTable dt = new DataTable();
            dt = new clsCDEtiquetas().Mostrar();
            if (dt.Rows.Count > 0)
            {
                numero = dt.Rows[0][0].ToString();
            }
            return numero;
        }

        public static string Insertar(string vEtiquetaV, string vEtiquetaN, string vPesoV, string vPesoN, string vPzV, string vPzN,string vUsers)
        {

            clsCDEtiquetas Obj = new clsCDEtiquetas();
            Obj.EtiquetaVieja = Convert.ToInt32(vEtiquetaV);
            Obj.EtiquetaNueva = Convert.ToInt32(vEtiquetaN);
            Obj.PesoViejo = Convert.ToDouble(vPesoV);
            Obj.PesoNuevo = Convert.ToDouble(vPesoN);
            Obj.PiezasViejas = Convert.ToInt32(vPzV);
            Obj.PiezasNuevas = Convert.ToInt32(vPzN);
            Obj.Usuario = vUsers;
            //Obj.PrmConn = prmConn;
            return Obj.Insertar(Obj);
        }

        
    }
}
