using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.SupervisionGeneral;
using f = WFO.Funciones;
namespace WFO.AccesoDatos.SupervisionGeneral
{
    public class Productividad
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.ProductividadMesaFlujo> Pruductividad_Top3_IdFlujo(int IdFlujo, string FechaIn, string FechaFn)
        {
            b.ExecuteCommandSP("Pruductividad_Top3_IdFlujo");
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            b.AddParameter("@FECHAINI", FechaIn, SqlDbType.VarChar);
            b.AddParameter("@FECHAFN", FechaFn, SqlDbType.VarChar);
            List<prop.ProductividadMesaFlujo> resultado = new List<prop.ProductividadMesaFlujo>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.ProductividadMesaFlujo item = new prop.ProductividadMesaFlujo()
                {
                    Mesa = reader["MESA"].ToString(),
                    Abre = reader["ABRE"].ToString(),
                    Nombre1 = reader["NOMBRE1"].ToString(),
                    Total1 = reader["TOTAL1"].ToString(),
                    Reingresos1 = reader["REINGRESOS1"].ToString(),
                    Calidad1 = reader["CALIDAD1"].ToString(),
                    Nombre2 = reader["NOMBRE2"].ToString(),
                    Total2 = reader["TOTAL2"].ToString(),
                    Reingresos2 = reader["REINGRESOS2"].ToString(),
                    Calidad2 = reader["CALIDAD2"].ToString(),
                    Nombre3 = reader["NOMBRE3"].ToString(),
                    Total3 = reader["TOTAL3"].ToString(),
                    Reingresos3 = reader["REINGRESOS3"].ToString(),
                    Calidad3 = reader["CALIDAD3"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.ProductividadMesaDetalle> Pruductividad_Mesa_IdFlujo(int IdFlujo, string FechaIn, string FechaFn, int OrdenMesa)
        {
            b.ExecuteCommandSP("Pruductividad_Mesa_IdFlujo");
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            b.AddParameter("@FECHAINI", FechaIn, SqlDbType.VarChar);
            b.AddParameter("@FECHAFN", FechaFn, SqlDbType.VarChar);
            b.AddParameter("@OrdenMesa", OrdenMesa, SqlDbType.Int);
            List<prop.ProductividadMesaDetalle> resultado = new List<prop.ProductividadMesaDetalle>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.ProductividadMesaDetalle item = new prop.ProductividadMesaDetalle()
                {
                    Nombre = reader["NOMBRE"].ToString(),
                    Total =  reader["TOTAL"].ToString(),
                    Reingresos = reader["REINGRESOS"].ToString(),
                    Calidad = reader["CALIDAD"].ToString(),
                    Productividad =  reader["PRODUCTIVIDAD"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public DataTable Pruductividad_Mesa_Grafica_IdFlujo(int IdFlujo, DateTime fechaDesde, DateTime fechaHasta, int OrdenMesa)
        {
            DataTable dt = new DataTable();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;
            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);
            
            string consulta = "EXEC Pruductividad_Mesa_Grafica_IdFlujo " + IdFlujo + ",'" + fechaD + "', '" + fechaH + "','" + OrdenMesa + "'";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }
    }
}
