using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class Municipios
    {
        ManejoDatos b = new ManejoDatos();

        public DataTable SeleccionarPorEntidadMunicipio(string entidad, string municipio)
        {
            string consulta = "SELECT * FROM municipios WHERE estado=@entidad AND codigo=@municipio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@entidad", entidad, SqlDbType.NVarChar, 2);
            b.AddParameter("@municipio", municipio, SqlDbType.NVarChar, 3);
            return b.Select();
        }
    }
}
