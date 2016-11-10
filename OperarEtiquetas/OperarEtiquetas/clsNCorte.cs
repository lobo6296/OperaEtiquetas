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
    class clsNCorte
    {
        public static OracleDataReader BusEtiqueta(string prmEtiqueta, OracleConnection prmConn)
        {
            clsDCorte Obj = new clsDCorte();
            Obj.Buscar = prmEtiqueta;
            Obj.PrmConn = prmConn;
            Obj.Condicion = "LlenaEtiqueta";
            return Obj.Busquedas(Obj);
        }

        public static OracleDataReader LlenaCboProducto(string prmBuscar, OracleConnection prmConn)
        {
            clsDCorte Obj = new clsDCorte();
            Obj.Buscar = prmBuscar;
            Obj.PrmConn = prmConn;
            Obj.Condicion = "LlenacboProductos";
            return Obj.Busquedas(Obj);
        }

        public static string UpdateInventario(string vCompania, string vDocu, string vEtiqueta, string vEstado, string vCentro, string vArticulo)
        {
            clsDCorte Obj = new clsDCorte();
            Obj.Articulo1 = vCompania;
            Obj.Buscar = vDocu;
            Obj.EtiquetaVieja = Convert.ToInt32(vEtiqueta); ;
            Obj.Condicion = vEstado;
            Obj.User = vCentro;
            Obj.Articulo = vArticulo;
            return Obj.UpInventario(Obj);
        }

        public static string Insertar(string vEtiquetaV, string vEtiquetaN, string vPeso, string vPz, string vArticulo, string vUser)
        {

            clsDCorte Obj = new clsDCorte();
            Obj.EtiquetaVieja = Convert.ToInt32(vEtiquetaV);
            Obj.Etiqueta1 = Convert.ToInt32(vEtiquetaN);
            Obj.Peso1 = Convert.ToDouble(vPeso);
            Obj.Pz1 = Convert.ToInt32(vPz);
            Obj.Articulo1 = vArticulo;
            Obj.User = vUser;
            return Obj.Insertar(Obj);
        }

    }
}
