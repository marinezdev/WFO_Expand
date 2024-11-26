using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class ReporteGeneralMesa
    {
        AccesoDatos.Procesos.Mesa mM = new AccesoDatos.Procesos.Mesa();
        public void MostrarDatosMesa(ref DevExpress.Web.ASPxGridView aSPxGridView, ref DevExpress.XtraCharts.Web.WebChartControl webChartControl, string fechaDesde, string fechaHasta,string idFlujo)
        {
            DataTable datos = new DataTable();
            aSPxGridView.Columns.Clear();
            datos = mM.DatosReporteGeneralMesa(fechaDesde, fechaHasta,idFlujo);
            aSPxGridView.DataSource = datos;
            aSPxGridView.DataBind();
            int Index = 1;
            
            foreach (DataColumn Campo in datos.Columns)
            {
                GridViewDataColumn Col = new GridViewDataColumn();
                Col.VisibleIndex = Index;
                Col.Width = 150;
                Col.Caption = Campo.ColumnName;
                Col.FieldName = Campo.ColumnName;
                aSPxGridView.Columns.Add(Col);
                Index++;
            }
            webChartControl.Legend.Visibility = DefaultBoolean.False;
            webChartControl.Series.Clear();
            DataTable dtDatos = new DataTable();
            dtDatos = datos;
            webChartControl.DataSource = dtDatos;
            foreach (DataColumn Campo in dtDatos.Columns)
            {
                if (!string.Equals("ESTATUS", Campo.ColumnName) && !string.Equals("TOTAL", Campo.ColumnName))
                {
                    Series Serie = new Series(Campo.ColumnName, ViewType.Bar);
                    webChartControl.Series.Add(Serie);
                    Serie.ArgumentScaleType = ScaleType.Auto;
                    Serie.ArgumentDataMember = "ESTATUS";
                    Serie.ValueScaleType = ScaleType.Numerical;
                    Serie.ValueDataMembers.AddRange(new string[] { Campo.ColumnName });
                }
                webChartControl.DataBind();
            }
        }
    }
}
