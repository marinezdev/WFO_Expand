using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class DetallePromotoria
    {
        AccesoDatos.Procesos.Tramite mt = new AccesoDatos.Procesos.Tramite();
        AccesoDatos.Procesos.TramiteMesa tm = new AccesoDatos.Procesos.TramiteMesa();

        public void DatosResumenPromotoria(ref DevExpress.Web.ASPxGridView aSPxGridView, DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dt = new DataTable();
            dt = mt.ResumenPromotoria(fechaDesde, fechaHasta);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
        }
        public DataTable DatosDetallePromotoria(string fechaDesde, string fechaHasta,string idPromotoria)
        {
            DataTable dt = new DataTable();
            dt = tm.DetallePromotoria(fechaDesde, fechaHasta, idPromotoria);
            return dt;
        }
    }
}
