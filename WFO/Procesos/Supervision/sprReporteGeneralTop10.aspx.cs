using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos.Supervision
{
    public partial class sprReporteGeneralTop10 : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            CalDesdeTE.EditFormatString = "dd/MM/yyyy";
            CalDesdeTE.Date = DateTime.Today;
            CalHastaTE.EditFormatString = "dd/MM/yyyy";
            CalHastaTE.Date = DateTime.Today;
            CalDesdeTS.EditFormatString = "dd/MM/yyyy";
            CalDesdeTS.Date = DateTime.Today;
            CalHastaTS.EditFormatString = "dd/MM/yyyy";
            CalHastaTS.Date = DateTime.Today;
            cmbFlujoTE.DataSource = sup.DatosComboFlujo();
            cmbFlujoTE.DataTextField = "Nombre";
            cmbFlujoTE.DataValueField = "Id";
            cmbFlujoTE.DataBind();
            cmbFlujoTR.DataSource = sup.DatosComboFlujo();
            cmbFlujoTR.DataTextField = "Nombre";
            cmbFlujoTR.DataValueField = "Id";
            cmbFlujoTR.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var valor = Ancho.Value;
            dxChtTotales.Width = new Unit(Convert.ToInt32(Ancho.Value));
            dxChtSuspendidos.Width = new Unit(Convert.ToInt32(Ancho.Value));
            rgt10.MuestradatosRecepcion(ref dvgdPromotorias, ref dxChtTotales, CalDesdeTE.Date.ToString(), CalHastaTE.Date.ToString(), cmbFlujoTE.SelectedValue.ToString());
            rgt10.MuestradatosSuspendidos(ref dgvsuspendidos, ref dxChtSuspendidos, CalDesdeTS.Date.ToString(), CalHastaTS.Date.ToString(), cmbFlujoTR.SelectedValue.ToString());
            UPTopSus.Update();
        }
        protected void dxChtTotales_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            dxChtTotales.Width = new Unit(Convert.ToInt32(Ancho.Value));
        }
        protected void dxChtSuspendidos_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            dxChtSuspendidos.Width = new Unit(Convert.ToInt32(Ancho.Value));
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdPromotorias.ExportXlsToResponse();
        }
        protected void lnkExportSuspend_Click(object sender, EventArgs e)
        {
            dgvsuspendidos.ExportXlsToResponse();
        }
    }
}