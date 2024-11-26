using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos.Supervision
{
    public partial class sprDetallePromotoria : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            CalDesde.EditFormatString = "dd/MM/yyyy";
            CalDesde.Date = DateTime.Today;
            CalHasta.EditFormatString = "dd/MM/yyyy";
            CalHasta.Date = DateTime.Today;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string modo = GridViewDetailExportMode.All.ToString();
            dvgdResumenPromotoria.SettingsDetail.ExportMode = (GridViewDetailExportMode)Enum.Parse(typeof(GridViewDetailExportMode), modo);
            dp.DatosResumenPromotoria(ref dvgdResumenPromotoria,  CalDesde.Date, CalHasta.Date);
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdResumenPromotoria.ExportXlsxToResponse("Promotoria.xlsx", new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }
        protected void dvgdDetallePromotoria_Init(object sender, EventArgs e)
        {
            ASPxGridView gridDetalle = (ASPxGridView)sender;
            string idPromotoria = gridDetalle.GetMasterRowFieldValues("idPromotoria").ToString();
            gridDetalle.DataSource = dp.DatosDetallePromotoria(CalDesde.Date.ToString(), CalHasta.Date.ToString(),idPromotoria);
        }
    }
}