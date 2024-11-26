using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class Polizas
    {
        ManejoDatos b = new ManejoDatos();

        public string NombreCliente(string poliza)
        {
            string consulta = "SELECT COALESCE(nombrecliente, 'No existe') AS NombreCliente FROM polizas WHERE NoPoliza=@poliza";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@poliza", poliza, System.Data.SqlDbType.NVarChar, 50);
            return b.SelectString();
        }
    }
}
