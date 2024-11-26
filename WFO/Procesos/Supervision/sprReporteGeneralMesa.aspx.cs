using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos.Supervision
{
    public partial class sprReporteGeneralMesa : Utilerias.Comun
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
            //var valor = Ancho.Value;
            //dxChtTotales.Width = new Unit(Convert.ToInt32(Ancho.Value));
            //rgm.MostrarDatosMesa(ref dvgdMesa, ref dxChtTotales, CalDesde.Date.ToString(), CalHasta.Date.ToString(), cmbFlujo.SelectedValue.ToString());
        }
        protected void dxChtTotales_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            dxChtTotales.Width = new Unit(Convert.ToInt32(Ancho.Value));
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
             dvgdMesa.ExportXlsToResponse(); 
        }
    }
}