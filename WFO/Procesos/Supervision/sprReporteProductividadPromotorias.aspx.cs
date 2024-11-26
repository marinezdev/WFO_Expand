using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

namespace WFO.Procesos.Supervision
{
    public partial class sprReporteProductividadPromotorias : Utilerias.Comun
    {
        public object LbxPromotorias { get; private set; }
        protected void LbxPromotorias_Init(object sender, EventArgs e)
        {
            Negocio.Procesos.SupervisionGeneral.cat_promotoria catpromotoria = new Negocio.Procesos.SupervisionGeneral.cat_promotoria();
            ASPxListBox listaUsuarios = (ASPxListBox)sender;
            foreach (var promotoria in catpromotoria.Listado())
            {
                listaUsuarios.Items.Add(promotoria.Nombre, promotoria.Id);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
            if (!IsPostBack)
            {
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            //Obtener los parámetros para llenar la tabla 
            string a = ASPxComboBoxAnn.SelectedItem.Value.ToString();
            string dEstatus = string.Empty;
            string estatus = cmbEstatus.Value.ToString();
            string[] listaEstatus = estatus.Split(';');

            string listaclaves = string.Empty;
            string[] nuevoestado = null;
            foreach (string estado in listaEstatus)
            {
                nuevoestado = estado.Split('-');
                listaclaves += nuevoestado[0] + ",";
            }

            listaclaves = listaclaves.Substring(0, listaclaves.Length - 1);
            rpp.ReportedeProductividaddePromotoria(ref dvgdEstatusTramite, a, listaclaves);
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
        }
    }
}