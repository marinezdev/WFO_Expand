using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos.Supervision
{
    public partial class buscarTramites : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string tramite = string.Empty;
            string rfc = string.Empty;
            string contratante = string.Empty;
            string asegurado = string.Empty;


            if (!string.IsNullOrEmpty(txtTramite.Text)) tramite = "%" + txtTramite.Text.Trim() + "%";
            else tramite = " ";
            if (!string.IsNullOrEmpty(txtRFC.Text)) rfc = "%" + txtRFC.Text.Trim() + "%";
            else rfc = " ";
            if (!string.IsNullOrEmpty(txtContratante.Text)) contratante = "%" + txtContratante.Text.Trim() + "%";
            else contratante = " ";
            if (!string.IsNullOrEmpty(txtAsegurado.Text)) asegurado = "%" + txtAsegurado.Text.Trim() + "%";
            else asegurado = " ";
            bt.MostrarDatos(ref dvgdTramites,tramite, rfc, contratante, asegurado);
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdTramites.ExportXlsToResponse();
        }
    }
}