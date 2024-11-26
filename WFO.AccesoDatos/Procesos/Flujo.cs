using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class Flujo
    {
        ManejoDatos b = new ManejoDatos();
        public DataTable DatosFlujo()
        {
            string consulta = "SELECT Id,Nombre FROM Flujo WHERE Activo=1";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }
    }
}
