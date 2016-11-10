using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using System.Windows.Forms;

namespace OperarEtiquetas
{
    class clsUtilerias
    {
        //función para validar que sean números los que están ingresando
        public bool IsNumeric(string num)
        {
            try
            {
                double x = Convert.ToDouble(num);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    class clsSeguridad
    {
        static clsConecta bd = new clsConecta();

        private static string _Usuario;
        private static string _Contrasenia;
        private static string _Empleado;

        public static string Empleado
        {
            get { return clsSeguridad._Empleado; }
            set { clsSeguridad._Empleado = value; }
        }
        //private static string[,] arrpermisos;

        public static string Contrasenia
        {
            get { return clsSeguridad._Contrasenia; }
            set { clsSeguridad._Contrasenia = value; }
        }

        public static string Usuario
        {
            get { return clsSeguridad._Usuario; }
            set { clsSeguridad._Usuario = value; }
        }

        //public void BuscaPermisos(string Login, OracleConnection Conn)
        //{
        //    int i = 0;
        //    try
        //    {
        //        bd.Conecta(Conn);
        //        DataSet dsSeg = new DataSet();
        //        OracleDataAdapter daSeg = new OracleDataAdapter(
        //            "Select NUMCOMPONENTE, NUMFUNCION " +
        //                "From SEGURIDAD.FUNCUSUARIO " +
        //                "Where CVEAPP = 'CTRLDECRETO' AND LOGIN = '" + Login + "' " +
        //                "Order By NUMCOMPONENTE, NUMFUNCION ", Conn);
        //        daSeg.Fill(dsSeg);

        //        if (dsSeg.Tables[0].Rows.Count > 0)
        //        {
        //            arrpermisos = new string[dsSeg.Tables[0].Rows.Count, 2];
        //            foreach (DataRow r in dsSeg.Tables[0].Rows)
        //            {
        //                arrpermisos[i, 0] = r["NUMCOMPONENTE"].ToString();
        //                arrpermisos[i, 1] = r["NUMFUNCION"].ToString();
        //                i++;
        //            }
        //        }
        //        bd.Desconecta(Conn);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "BuscaPermisos", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Cursor.Current = Cursors.Default;
        //    }
        //}

        //public static bool ValidaPermiso(int prmNumComponente, int prmNumFuncion)
        //{
        //    if (arrpermisos != null)
        //    {
        //        int MaxDim0 = arrpermisos.GetUpperBound(0);
        //        for (int i = 0; i <= MaxDim0; i++)
        //        {
        //            if (arrpermisos[i, 0] == prmNumComponente.ToString())
        //            {
        //                if (arrpermisos[i, 1] == prmNumFuncion.ToString()) { return true; }
        //            }
        //        }
        //    }
        //    return false;
        //}
    }
}
