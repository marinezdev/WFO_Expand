using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos.Supervision
{
    public partial class sprTiemposAtencion : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            CalDesde.EditFormatString = "dd/MM/yyyy";
            CalDesde.Date = DateTime.Today;
            CalHasta.EditFormatString = "dd/MM/yyyy";
            CalHasta.Date = DateTime.Today;
            cmbFlujo.DataSource = sup.DatosComboFlujo();
            cmbFlujo.DataTextField = "Nombre";
            cmbFlujo.DataValueField = "Id";
            cmbFlujo.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ta.DatosTiemposAtencion(ref dvgdMesa, CalDesde.Date, CalHasta.Date, cmbFlujo.SelectedValue.ToString() );
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdMesa.ExportXlsToResponse();
        }
    }
}