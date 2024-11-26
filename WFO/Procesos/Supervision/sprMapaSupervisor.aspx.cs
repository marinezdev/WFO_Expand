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
    public partial class sprMapaSupervisor :Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string modo = GridViewDetailExportMode.All.ToString();
            string Mesa = Request.Params["Id"];
            string IdFlujo = Request.Params["idFlujo"];
            lblMesa.Text = "MESA: " + Mesa;
            msup.MapaSupervisorMesa(ref dvgdTramites, Mesa,IdFlujo);
            dvgdTramites.SettingsDetail.ExportMode = (GridViewDetailExportMode)Enum.Parse(typeof(GridViewDetailExportMode), modo);
        }
        protected void dvgdDetalleTramite_Init(object sender, EventArgs e)
        {
            ASPxGridView gridDetalle = (ASPxGridView)sender;
            string idMesa = gridDetalle.GetMasterRowFieldValues("idMesa").ToString();
            DataTable dtD = msup.MapaSupervisorDetalleMesa(idMesa);
            gridDetalle.DataSource = dtD;
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdTramites.ExportXlsxToResponse("MapaSupervisor.xlsx", new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }
    }
}