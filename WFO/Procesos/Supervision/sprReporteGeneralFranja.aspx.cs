using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos.Supervision
{
    public partial class sprReporteGeneralFranja : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            CalFranja.EditFormatString = "dd/MM/yyyy";
            CalFranja.Date = DateTime.Now;
            int annio = DateTime.Now.Year;
            int mes = DateTime.Now.Month;
            cmbFlujoF.DataSource = sup.DatosComboFlujo();
            cmbFlujoF.DataTextField = "Nombre";
            cmbFlujoF.DataValueField = "Id";
            cmbFlujoF.DataBind();
            cmbFlujoTS.DataSource = sup.DatosComboFlujo();
            cmbFlujoTS.DataTextField = "Nombre";
            cmbFlujoTS.DataValueField = "Id";
            cmbFlujoTS.DataBind();
            cmbFlujoT.DataSource = sup.DatosComboFlujo();
            cmbFlujoT.DataTextField = "Nombre";
            cmbFlujoT.DataValueField = "Id";
            cmbFlujoT.DataBind();

            AnnioTS.Items.Add(new ListItem((DateTime.Now.Year).ToString(), (DateTime.Now.Year).ToString()));
            AnnioTS.Items.Add(new ListItem((DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 1).ToString()));
            AnnioTS.Items.Add(new ListItem((DateTime.Now.Year - 2).ToString(), (DateTime.Now.Year - 2).ToString()));
            MesTS.Items.FindByValue(mes.ToString()).Selected = true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var valor = Ancho.Value;
            dvchtFranja.Width = new Unit(Convert.ToInt32(Ancho.Value));
            dxChtTotalesTS.Width = new Unit(Convert.ToInt32(Ancho.Value));
            dvchtTendencia.Width = new Unit(Convert.ToInt32(Ancho.Value));
            rgf.MostrarDatosFranja(ref dvgdFranja,ref dvchtFranja,CalFranja.Date, cmbFlujoF.SelectedValue.ToString());
            rgf.AcumuladoMensualFranja(ref lblAcumulado,CalFranja.Date, cmbFlujoF.SelectedValue.ToString());
            string annio = AnnioTS.SelectedValue;
            string mes = MesTS.SelectedValue;
            rgf.LlenarDatosTS(ref dvgdTramiteSemana,ref dxChtTotalesTS, mes, annio, cmbFlujoTS.SelectedValue.ToString());
            rgf.MostrarDatosTendencia(ref dvgdTendencia,ref dvchtTendencia, cmbFlujoT.SelectedValue.ToString());
            DetalleReporteTS.Update();

        }
        protected void dvchtFranja_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            dvchtFranja.Width = new Unit(Convert.ToInt32(Ancho.Value));
        }
        protected void dxChtTotalesTS_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            dxChtTotalesTS.Width = new Unit(Convert.ToInt32(Ancho.Value));
        }
        protected void dvchtTendencia_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            dvchtTendencia.Width = new Unit(Convert.ToInt32(Ancho.Value));
        }
        protected void lnkExportarFranja_Click(object sender, EventArgs e)
        {
            grdExportFranja.WriteXlsToResponse();
        }
        protected void lnkExportarTS_Click(object sender, EventArgs e)
        {

            dvgdTramiteSemana.ExportXlsToResponse();
        }
        protected void lnkExportarTendencia_Click(object sender, EventArgs e)
        {
            grdExportT.WriteXlsToResponse();
        }
    }
}