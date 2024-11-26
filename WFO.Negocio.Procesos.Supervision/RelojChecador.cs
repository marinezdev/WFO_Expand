using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class RelojChecador
    {
        AccesoDatos.Procesos.Usuarios u = new AccesoDatos.Procesos.Usuarios();
        public void DatosRelojChecador(ref DevExpress.Web.ASPxGridView aSPxGridView, string fechaDesde, string fechaHasta,string idFlujo)
        {
            DataTable Datos = new DataTable();
            Datos = u.DatosRelojChecador(fechaDesde, fechaHasta, idFlujo);
            aSPxGridView.DataSource = Datos;
            aSPxGridView.DataBind();
        }
    }
}
