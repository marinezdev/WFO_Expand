using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WFO.Negocio.Procesos.Supervision
{
    public class AlmacenamientoTramites
    {
        AccesoDatos.Procesos.Tramite tramite = new AccesoDatos.Procesos.Tramite();

        public void AlmacenamientodeTramitesPromedio(ref GridView gridview)
        {
            Funciones.LlenarControles.LlenarGridView(ref gridview, tramite.ReporteAlmacenamientoTramites());
        }

        public void AlmacenamientodeTramites(ref Repeater TablaInformativa, string opcion)
        {
                Funciones.LlenarControles.LlenarRepeater(ref TablaInformativa, tramite.ReporteAlmacenamientoTramitesDetalle(opcion));
        }
    }
}
