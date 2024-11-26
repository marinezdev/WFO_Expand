using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Cobranza
{
    public class ReporteConsolidado
    {
        AccesoDatos.Procesos.PolizasDetalleConsolidado pdc = new AccesoDatos.Procesos.PolizasDetalleConsolidado();

        public DataTable Seleccionar()
        {
            return pdc.Seleccionar();
        }
    }
}
