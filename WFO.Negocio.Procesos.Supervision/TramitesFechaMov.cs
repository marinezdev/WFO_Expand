using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class TramitesFechaMov
    {
        AccesoDatos.Procesos.Tramite mt = new AccesoDatos.Procesos.Tramite();
        public void llenarDatos(ref DevExpress.Web.ASPxGridView aSPxGridView, string fechaDesde, string fechaHasta,string idFlujo)
        {
            DataTable dt = new DataTable();
            dt = mt.TotalTramiteFechaMov(fechaDesde, fechaHasta,idFlujo);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
        }
    }
}
