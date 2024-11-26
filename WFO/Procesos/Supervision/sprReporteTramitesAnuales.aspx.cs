using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

namespace WFO.Procesos.Supervision
{
    public partial class sprReporteTramitesAnuales : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];

            if (!IsPostBack)
            {
                rta.ReportedeTramitesAnuales(ref dvgdEstatusTramite);
                rta.LlenadoGraficoTramitesAnuales(ref Chart1);
            }
        }




    }
}