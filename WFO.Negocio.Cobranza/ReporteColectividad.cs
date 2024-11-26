using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Cobranza
{
    public class ReporteColectividad
    {
        AccesoDatos.Procesos.PolizasDetalle pde = new AccesoDatos.Procesos.PolizasDetalle();

        public DataTable SeleccionarPolizasColectividadExcel()
        {
            return pde.SeleccionarPolizasColectividadExcel();
        }
    }
}
