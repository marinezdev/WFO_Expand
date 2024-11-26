using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos.Supervision
{
    public partial class Default : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            
            cmbFlujoM.DataSource = sup.DatosComboFlujo();
            cmbFlujoM.DataTextField = "Nombre";
            cmbFlujoM.DataValueField = "Id";
            cmbFlujoM.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            sup.MostrarMapa(ref IndicadoresMapa, cmbFlujoM.SelectedValue.ToString());
            sup.MapaDetalle(ref dvgdEstatusTramite, ref dxChtTotales,cmbFlujoM.SelectedValue.ToString());
            var valor = Ancho.Value;
            dxChtTotales.Width = new Unit(Convert.ToInt32(Ancho.Value));
            
        }
        protected void dxChtTotales_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            dxChtTotales.Width = new Unit(Convert.ToInt32(Ancho.Value));
        }
        protected void dvgdDetallePromotoria_Init(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView gridDetalle = (ASPxGridView)sender;
                string estado = gridDetalle.GetMasterRowFieldValues("ESTADO").ToString();
                DataTable dtD = sup.DatosResumenPromotoria(estado, cmbFlujoM.SelectedValue.ToString());
                gridDetalle.DataSource = dtD;
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }
        }
    }
}