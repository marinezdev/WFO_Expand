using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DevExpress.Web;

namespace WFO.Negocio.Procesos.Supervision
{
    public class ReporteProductividadPromotoria
    {
        AccesoDatos.Procesos.Tramite tramite = new AccesoDatos.Procesos.Tramite();

        public void ReportedeProductividaddePromotoria(ref ASPxGridView aspxgridview, string ann, string estado)
        {
            Funciones.LlenarControles.LlenaraAspxGridView(ref aspxgridview, tramite.ReporteProductividadPromotorias(ann, estado).Tables[0]);
        }
    }
}
