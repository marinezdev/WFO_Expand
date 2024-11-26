using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
   public class ReporteGeneralTop10
    {
        AccesoDatos.Procesos.Tramite mt = new AccesoDatos.Procesos.Tramite();
        public void MuestradatosRecepcion(ref DevExpress.Web.ASPxGridView aSPxGridView, ref DevExpress.XtraCharts.Web.WebChartControl webChartControl, string fechaDesde, string fechaHasta,string idFlujo)
        {
            DataTable dt = new DataTable();
            dt = mt.TopTenRecepcion(fechaDesde, fechaHasta,idFlujo);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
            webChartControl.Series.Clear();

            webChartControl.DataSource = dt;
            foreach (DataColumn Campo in dt.Columns)
            {
                if (!string.Equals("Promotoria", Campo.ColumnName) && !string.Equals("Nombre", Campo.ColumnName) && !string.Equals("Zona", Campo.ColumnName))
                {
                    Series SerieS = new Series(Campo.ColumnName, ViewType.Bar);
                    webChartControl.Series.Add(SerieS);
                    SerieS.ArgumentScaleType = ScaleType.Qualitative;
                    SerieS.ArgumentDataMember = "Promotoria";
                    SerieS.ValueScaleType = ScaleType.Numerical;
                    SerieS.ValueDataMembers.AddRange(new string[] { Campo.ColumnName });
                }

            }
            webChartControl.DataBind();

        }
        public void MuestradatosSuspendidos(ref DevExpress.Web.ASPxGridView aSPxGridView, ref DevExpress.XtraCharts.Web.WebChartControl webChartControl, string fechaDesde, string fechaHasta, string idFlujo)
        {
            DataTable dt = new DataTable();
            dt = mt.TopTenSuspendidos(fechaDesde, fechaHasta,idFlujo);

            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
            webChartControl.Series.Clear();
            aSPxGridView.DataSource = dt;
            webChartControl.DataSource = dt;
            foreach (DataColumn Campo in dt.Columns)
            {
                if (!string.Equals("Promotoria", Campo.ColumnName) && !string.Equals("Nombre", Campo.ColumnName) && !string.Equals("Zona", Campo.ColumnName))
                {
                    Series SerieE = new Series(Campo.ColumnName, ViewType.Bar);
                    webChartControl.Series.Add(SerieE);
                    SerieE.ArgumentScaleType = ScaleType.Qualitative;
                    SerieE.ArgumentDataMember = "Promotoria";
                    SerieE.ValueScaleType = ScaleType.Numerical;
                    SerieE.ValueDataMembers.AddRange(new string[] { Campo.ColumnName });
                }

            }
            webChartControl.DataBind();
        }
    }
}
