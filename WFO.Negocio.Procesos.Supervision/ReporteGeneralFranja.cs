using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WFO.Negocio.Procesos.Supervision
{
    public class ReporteGeneralFranja
    {
        AccesoDatos.Procesos.Tramite mt = new AccesoDatos.Procesos.Tramite();
        AccesoDatos.Procesos.BitacoraDos bdos = new AccesoDatos.Procesos.BitacoraDos();

        public void AcumuladoMensualFranja(ref Label Etiqueta,DateTime Fecha,string idFlujo)
        {
            int totalMes = mt.TotalMesFranja(Fecha,idFlujo); 
            Etiqueta.Text = totalMes.ToString();
        }
        public void MostrarDatosFranja(ref DevExpress.Web.ASPxGridView aSPxGridView, ref DevExpress.XtraCharts.Web.WebChartControl webChartControl,DateTime Fecha,string idFlujo)
        {
            webChartControl.Series.Clear();
            DataTable dt = new DataTable();
            dt = bdos.Franja(Fecha,idFlujo);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();

            Series sri = new Series("INGRESADOS", ViewType.Line);
            Series srt = new Series("PENDIENTES DE ATENCIÓN", ViewType.Line);
            Series sre = new Series("PROCESADOS", ViewType.Line);

            foreach (DataRow registro in dt.Rows)
            {
                sri.Points.Add(new SeriesPoint(registro["Franja"].ToString(), registro["ingresados"].ToString()));
                srt.Points.Add(new SeriesPoint(registro["Franja"].ToString(), registro["tocados"].ToString()));
                sre.Points.Add(new SeriesPoint(registro["Franja"].ToString(), registro["ejecutados"].ToString()));
            }

            ((LineSeriesView)sri.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
            ((LineSeriesView)sri.View).Color = System.Drawing.Color.DarkGreen;
            ((LineSeriesView)srt.View).LineMarkerOptions.Kind = MarkerKind.Circle;
            ((LineSeriesView)srt.View).Color = System.Drawing.Color.DarkBlue;
            ((LineSeriesView)sre.View).LineMarkerOptions.Kind = MarkerKind.Square;
            ((LineSeriesView)sre.View).Color = System.Drawing.Color.DarkGray;

            sri.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            srt.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            sre.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            webChartControl.Series.Add(sri);
            webChartControl.Series.Add(srt);
            webChartControl.Series.Add(sre);
        }
        public void LlenarDatosTS(ref DevExpress.Web.ASPxGridView aSPxGridView, ref DevExpress.XtraCharts.Web.WebChartControl webChartControl,string mes, string annio,string idFlujo)
        {
            DataTable dt = new DataTable();
            webChartControl.Series.Clear();
            dt = mt.TramiteSemana(mes, annio,idFlujo);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
            aSPxGridView.Caption = "TRÁMITES POR SEMANA";

            DataTable dtTotalSemana = new DataTable();
            DataTable dtTotales = new DataTable();
            dtTotales = mt.TotalTramiteSemana(mes, annio,idFlujo);
            dtTotalSemana.Columns.Add("SEMANA");
            dtTotalSemana.Columns.Add("TRAMITES", typeof(Int32));

            // MUESTRA DATOS EN LA GRAFICA
            foreach (DataRow tramite in dtTotales.Rows)
            {
                dtTotalSemana.Rows.Add(new object[] { tramite["SEMANA"], tramite["TRAMITES"] });
            }
            webChartControl.DataSource = dtTotalSemana;
            webChartControl.SeriesDataMember = "SEMANA";
            webChartControl.SeriesTemplate.SetDataMembers("SEMANA", "TRAMITES");
            webChartControl.SeriesTemplate.ArgumentDataMember = "SEMANA";
            webChartControl.DataBind();
        }
        public void MostrarDatosTendencia(ref DevExpress.Web.ASPxGridView aSPxGridView, ref DevExpress.XtraCharts.Web.WebChartControl webChartControl, string idFlujo)
        {
            DateTime fecha = DateTime.Now;
            webChartControl.Series.Clear();
            string  annioActual = fecha.Year.ToString();
            string annioAnterior = (fecha.Year - 1).ToString();
            DataTable dt = mt.Tendencia(annioActual,idFlujo);
            DataTable dt2 = mt.Tendencia(annioAnterior,idFlujo);
            dt.Merge(dt2);

            Series srI = new Series(annioActual.ToString() + "-INGRESADOS", ViewType.Line);
            Series srEA = new Series(annioActual.ToString() + "-EN ATENCIÓN", ViewType.Line);
            srI.Points.Add(new SeriesPoint("Enero", dt.Rows[0]["Enero"]));
            srEA.Points.Add(new SeriesPoint("Enero", dt.Rows[1]["Enero"]));
            srI.Points.Add(new SeriesPoint("Febrero", dt.Rows[0]["Febrero"]));
            srEA.Points.Add(new SeriesPoint("Febrero", dt.Rows[1]["Febrero"]));
            srI.Points.Add(new SeriesPoint("Marzo", dt.Rows[0]["Marzo"]));
            srEA.Points.Add(new SeriesPoint("Marzo", dt.Rows[1]["Marzo"]));
            srI.Points.Add(new SeriesPoint("Abril", dt.Rows[0]["Abril"]));
            srEA.Points.Add(new SeriesPoint("Abril", dt.Rows[1]["Abril"]));
            srI.Points.Add(new SeriesPoint("Mayo", dt.Rows[0]["Mayo"]));
            srEA.Points.Add(new SeriesPoint("Mayo", dt.Rows[1]["Mayo"]));
            srI.Points.Add(new SeriesPoint("Junio", dt.Rows[0]["Junio"]));
            srEA.Points.Add(new SeriesPoint("Junio", dt.Rows[1]["Junio"]));
            srI.Points.Add(new SeriesPoint("Julio", dt.Rows[0]["Julio"]));
            srEA.Points.Add(new SeriesPoint("Julio", dt.Rows[1]["Julio"]));
            srI.Points.Add(new SeriesPoint("Agosto", dt.Rows[0]["Agosto"]));
            srEA.Points.Add(new SeriesPoint("Agosto", dt.Rows[1]["Agosto"]));
            srI.Points.Add(new SeriesPoint("Septiembre", dt.Rows[0]["Septiembre"]));
            srEA.Points.Add(new SeriesPoint("Septiembre", dt.Rows[1]["Septiembre"]));
            srI.Points.Add(new SeriesPoint("Octubre", dt.Rows[0]["Octubre"]));
            srEA.Points.Add(new SeriesPoint("Octubre", dt.Rows[1]["Octubre"]));
            srI.Points.Add(new SeriesPoint("Noviembre", dt.Rows[0]["Noviembre"]));
            srEA.Points.Add(new SeriesPoint("Noviembre", dt.Rows[1]["Noviembre"]));
            srI.Points.Add(new SeriesPoint("Diciembre", dt.Rows[0]["Diciembre"]));
            srEA.Points.Add(new SeriesPoint("Diciembre", dt.Rows[1]["Diciembre"]));

            ((LineSeriesView)srI.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
            ((LineSeriesView)srI.View).Color = System.Drawing.Color.DarkBlue;
            srI.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            webChartControl.Series.Add(srI);

            ((LineSeriesView)srEA.View).LineMarkerOptions.Kind = MarkerKind.Square;
            ((LineSeriesView)srEA.View).Color = System.Drawing.Color.DarkCyan;
            srEA.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            webChartControl.Series.Add(srEA);

            Series srIAA = new Series(annioAnterior.ToString() + "-INGRESADOS", ViewType.Line);
            Series srEAAA = new Series(annioAnterior.ToString() + "-EN ATENCIÓN", ViewType.Line);

            srIAA.Points.Add(new SeriesPoint("Enero", dt.Rows[2]["Enero"]));
            srEAAA.Points.Add(new SeriesPoint("Enero", dt.Rows[3]["Enero"]));
            srIAA.Points.Add(new SeriesPoint("Febrero", dt.Rows[2]["Febrero"]));
            srEAAA.Points.Add(new SeriesPoint("Febrero", dt.Rows[3]["Febrero"]));
            srIAA.Points.Add(new SeriesPoint("Marzo", dt.Rows[2]["Marzo"]));
            srEAAA.Points.Add(new SeriesPoint("Marzo", dt.Rows[3]["Marzo"]));
            srIAA.Points.Add(new SeriesPoint("Abril", dt.Rows[2]["Abril"]));
            srEAAA.Points.Add(new SeriesPoint("Abril", dt.Rows[3]["Abril"]));
            srIAA.Points.Add(new SeriesPoint("Mayo", dt.Rows[2]["Mayo"]));
            srEAAA.Points.Add(new SeriesPoint("Mayo", dt.Rows[3]["Mayo"]));
            srIAA.Points.Add(new SeriesPoint("Junio", dt.Rows[2]["Junio"]));
            srEAAA.Points.Add(new SeriesPoint("Junio", dt.Rows[3]["Junio"]));
            srIAA.Points.Add(new SeriesPoint("Julio", dt.Rows[2]["Julio"]));
            srEAAA.Points.Add(new SeriesPoint("Julio", dt.Rows[3]["Julio"]));
            srIAA.Points.Add(new SeriesPoint("Agosto", dt.Rows[2]["Agosto"]));
            srEAAA.Points.Add(new SeriesPoint("Agosto", dt.Rows[3]["Agosto"]));
            srIAA.Points.Add(new SeriesPoint("Septiembre", dt.Rows[2]["Septiembre"]));
            srEAAA.Points.Add(new SeriesPoint("Septiembre", dt.Rows[3]["Septiembre"]));
            srIAA.Points.Add(new SeriesPoint("Octubre", dt.Rows[2]["Octubre"]));
            srEAAA.Points.Add(new SeriesPoint("Octubre", dt.Rows[3]["Octubre"]));
            srIAA.Points.Add(new SeriesPoint("Noviembre", dt.Rows[2]["Noviembre"]));
            srEAAA.Points.Add(new SeriesPoint("Noviembre", dt.Rows[3]["Noviembre"]));
            srIAA.Points.Add(new SeriesPoint("Diciembre", dt.Rows[2]["Diciembre"]));
            srEAAA.Points.Add(new SeriesPoint("Diciembre", dt.Rows[3]["Diciembre"]));

            ((LineSeriesView)srIAA.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
            ((LineSeriesView)srIAA.View).Color = System.Drawing.Color.DarkRed;
            srIAA.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            webChartControl.Series.Add(srIAA);

            ((LineSeriesView)srEAAA.View).LineMarkerOptions.Kind = MarkerKind.Square;
            ((LineSeriesView)srEAAA.View).Color = System.Drawing.Color.DarkOrange;
            srIAA.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            webChartControl.Series.Add(srEAAA);

            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
        }
    }
}
