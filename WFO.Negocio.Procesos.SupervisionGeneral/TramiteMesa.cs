using System.Web.UI.WebControls;
using WFO.Funciones;
using prop = WFO.Propiedades.Procesos.SupervisionGeneral;

namespace WFO.Negocio.Procesos.SupervisionGeneral
{
    public class TramiteMesa : SupervisionGeneral
    {
        public void TramiteMesaLLenarDetalle_GridView(ref GridView gridview, string idmesa, string statusmesa)
        {
            Funciones.LlenarControles.LlenarGridView(ref gridview, tramitemesa.SeleccionarDetalle(idmesa, statusmesa));
        }
    }
}
