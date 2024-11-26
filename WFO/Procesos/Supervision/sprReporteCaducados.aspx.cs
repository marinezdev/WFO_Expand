using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos.Supervision
{
    public partial class sprReporteCaducados : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e)
        {
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
            string modo = GridViewDetailExportMode.All.ToString();
            dvgdEstatusTramite.SettingsDetail.ExportMode = (GridViewDetailExportMode)Enum.Parse(typeof(GridViewDetailExportMode), modo);
            rec.DatosReporteCaducados(ref dvgdEstatusTramite, CalDesde.Date.ToString(), CalHasta.Date.ToString(), cmbFlujo.SelectedValue.ToString());
        }
        protected void dvgdDetalleCaducados_Init(object sender, EventArgs e)
        {
            ASPxGridView gridDetalle = (ASPxGridView)sender;
            int IdTramite = int.Parse(gridDetalle.GetMasterRowFieldValues("Id").ToString());
            DataTable dtD = new DataTable();
            gridDetalle.DataSource = dtD;
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdEstatusTramite.ExportXlsxToResponse("Caducados.xlsx", new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }
    }
}