using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WFO.Negocio.Procesos.Supervision
{
    public class ReporteGeneralTotales
    {
        AccesoDatos.Procesos.Tramite mt= new AccesoDatos.Procesos.Tramite();
        AccesoDatos.Procesos.Flujo f = new AccesoDatos.Procesos.Flujo();
        public void llenarDatos(ref DevExpress.Web.ASPxGridView aSPxGridView, ref DevExpress.XtraCharts.Web.WebChartControl webChartControl, string fechaDesde, string fechaHasta, string idFlujo)
        {
            DataTable dt = new DataTable();
            dt = mt.SeleccionarReporteGeneralTotales(fechaDesde, fechaHasta,idFlujo);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind(); 
            aSPxGridView.Caption = "ACUMULADO MENSUAL";

            DataTable dtPorcentaje = new DataTable();
            dtPorcentaje.Columns.Add("Descripcion");
            dtPorcentaje.Columns.Add("Porcentaje", typeof(float));

            // MUESTRA DATOS EN LA GRAFICA
            foreach (DataRow tramite in dt.Rows)
            {
                dtPorcentaje.Rows.Add(new object[] { tramite["Descripcion"],tramite["Porcentaje"] });
            }

            webChartControl.DataSource = dtPorcentaje;
            webChartControl.SeriesDataMember = "Descripcion";
            webChartControl.SeriesTemplate.SetDataMembers("Descripcion", "Porcentaje");
            webChartControl.SeriesTemplate.ArgumentDataMember = "Descripcion";
            webChartControl.DataBind();
        }
        public DataTable DatosComboFlujo()
        {
            DataTable dt = new DataTable();
            dt = f.DatosFlujo();
            return dt;
        }
    }
}
