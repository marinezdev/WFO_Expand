using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class BuscarTramites
    {
        AccesoDatos.Procesos.TramiteMesa tm = new AccesoDatos.Procesos.TramiteMesa();
        public void MostrarDatos(ref DevExpress.Web.ASPxGridView aSPxGridView,string tramite, string rfc, string contratante, string asegurado)
        {
            DataTable dt = new DataTable();
            dt = tm.DatosTramite(tramite, rfc, contratante, asegurado);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
        }
    }
}
