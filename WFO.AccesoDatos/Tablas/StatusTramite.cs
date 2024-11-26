using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Tablas
{
    public class StatusTramite
    {
        ManejoDatos b = new ManejoDatos();

        public DataTable Seleccionar()
        {
            b.ExecuteCommandSP("StatusTramite_Seleccionar");
            return b.Select();
        }
    }
}
