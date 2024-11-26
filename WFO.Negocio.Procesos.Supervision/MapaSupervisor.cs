using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class MapaSupervisor
    {
        AccesoDatos.Procesos.MapaSupervisor mp = new AccesoDatos.Procesos.MapaSupervisor();
        public void MapaSupervisorMesa(ref DevExpress.Web.ASPxGridView aSPxGridView,string mesa, string idFlujo)
        {
            DataTable dt = new DataTable();
            dt = mp.DatosMapaSupervisorMesa(mesa,idFlujo);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
        }
        public DataTable MapaSupervisorDetalleMesa(string IdMesa)
        {
            DataTable dt = new DataTable();
            dt = mp.DatosSupervisorMesaDetalle(IdMesa);
            return dt;
        }
    }
}
