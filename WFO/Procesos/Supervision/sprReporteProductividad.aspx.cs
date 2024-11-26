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
    public partial class sprReporteProductividad : Utilerias.Comun
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
            string modo = GridViewDetailExportMode.All.ToString();
            dvgdProductividad.SettingsDetail.ExportMode = (GridViewDetailExportMode)Enum.Parse(typeof(GridViewDetailExportMode), modo);
            rep.DatosProductividad(ref dvgdProductividad, ref cmbUsuarios, CalDesde.Date.ToString(), CalHasta.Date.ToString(), cmbFlujo.SelectedValue.ToString());
        }
        protected void listUsuario_Init(object sender, EventArgs e)
        {
            ASPxListBox listaUsuarios = (ASPxListBox)sender;
            DataTable dtD = rep.DatosUsuarios(cmbFlujo.SelectedValue.ToString());
            foreach (DataRow usuario in dtD.Rows)
            {
                listaUsuarios.Items.Add(usuario["Nombre"].ToString());
            }
        }
        protected void dvgdDetalleProducividad_Init(object sender, EventArgs e)
        {
            ASPxGridView gridDetalle = (ASPxGridView)sender;
            string mesaNombre = gridDetalle.GetMasterRowFieldValues("mesaNombre").ToString();
            string usuario = gridDetalle.GetMasterRowFieldValues("operador").ToString();
            gridDetalle.DataSource = rep.DatosDetalleProductividad(CalDesde.Date.ToString(), CalHasta.Date.ToString(), usuario, mesaNombre, cmbFlujo.SelectedValue.ToString());
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdProductividad.ExportXlsxToResponse("Productividad.xlsx", new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }
    }
}