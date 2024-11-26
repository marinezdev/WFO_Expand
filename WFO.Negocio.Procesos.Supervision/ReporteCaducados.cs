using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class ReporteCaducados
    {
        AccesoDatos.Procesos.Tramite mt = new AccesoDatos.Procesos.Tramite();
        public void DatosReporteCaducados(ref DevExpress.Web.ASPxGridView aSPxGridView, string fechaDesde, string fechaHasta, string idFlujo)
        {
            DataTable dt = new DataTable();
            AccesoDatos.Procesos.Tramite mt = new AccesoDatos.Procesos.Tramite();
            string dEstatus = "'Caducado'";
            dt = mt.EstatusTramite(fechaDesde, fechaHasta, dEstatus, 3,idFlujo);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
        }
    }

}
