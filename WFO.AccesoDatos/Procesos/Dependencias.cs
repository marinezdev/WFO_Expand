using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class Dependencias
    {
        ManejoDatos b = new ManejoDatos();

        public string SeleccionarPorNombre(string nombre)
        {
            string consulta = "SELECT nombre FROM dependencias WHERE nombre=@nombre";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", nombre, SqlDbType.NVarChar);
            return b.SelectString();
        }

        public string SeleccionarIdPorNombre(string nombre)
        {
            string consulta = "SELECT iddependencia FROM dependencias WHERE nombre=@nombre";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", nombre, SqlDbType.NVarChar);
            return b.SelectString();
        }

        public string SeleccionarRetenedorPorNombre(string nombre)
        {
            string consulta = "SELECT retenedor FROM dependencias WHERE nombre=@nombre";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", nombre, SqlDbType.NVarChar);
            return b.SelectString();
        }
    }
}
