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
    class clsNTraslados
    {
        public static OracleDataReader BusEtiqueta(string prmEstiba,string prmLinea, string prmPunto, OracleConnection prmConn)
        {
            clsDTraslados Obj = new clsDTraslados();
            Obj.EstibaV = Convert.ToInt32(prmEstiba);
            Obj.LineaV = Convert.ToInt32(prmLinea);
            Obj.PuntoV = Convert.ToInt32(prmPunto);
            Obj.PrmConn = prmConn;
            Obj.Condicion = "CuentaAtados";
            return Obj.Busquedas(Obj);
        }

        public static string Inserta(string prmEstiba, string prmLinea, string prmPunto, string prmEstibaN, string prmLineaN, string prmPuntoN, string prmUser, OracleConnection prmConn)
        {
            clsDTraslados Obj = new clsDTraslados();
            Obj.EstibaV = Convert.ToInt32(prmEstiba);
            Obj.LineaV = Convert.ToInt32(prmLinea);
            Obj.PuntoV = Convert.ToInt32(prmPunto);
            Obj.EstibaN = Convert.ToInt32(prmEstibaN);
            Obj.LineaN = Convert.ToInt32(prmLineaN);
            Obj.PuntoN = Convert.ToInt32(prmPuntoN);
            Obj.PrmConn = prmConn;
            return Obj.Insertar(Obj);
        }
    }
}
