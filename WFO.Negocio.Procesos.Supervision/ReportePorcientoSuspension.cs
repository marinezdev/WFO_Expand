using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class ReportePorcientoSuspension
    {
        AccesoDatos.Procesos.Tramite mt = new AccesoDatos.Procesos.Tramite();
        AccesoDatos.Procesos.TramiteMesa tm = new AccesoDatos.Procesos.TramiteMesa();

        public void DatosPorcientoSuspension(ref DevExpress.Web.ASPxGridView dvgdPorcientoSuspension, ref DevExpress.Web.ASPxGridView dvgdMotivosSuspension, ref DevExpress.XtraCharts.Web.WebChartControl webChartControl,string fechaDesde, string fechaHasta,string idFlujo)
        {
            DataTable dt = new DataTable();
            dt = mt.PorcentajeSuspendidos(fechaDesde, fechaHasta,idFlujo);
            dvgdPorcientoSuspension.DataSource = dt;
            dvgdPorcientoSuspension.DataBind();
            dvgdPorcientoSuspension.Caption = "CIFRAS DE CONTROL";
            DataTable dtMotivos = new DataTable();

            dtMotivos = tm.PorcientoMotivosSuspension(fechaDesde, fechaHasta,idFlujo);
            dvgdMotivosSuspension.DataSource = dtMotivos;
            dvgdMotivosSuspension.DataBind();
            dvgdMotivosSuspension.Caption = "MOTIVOS DE SUSPENSIÓN (RESUMEN ANUAL)";

            DataTable dtPorcentaje = new DataTable();
            dtPorcentaje.Columns.Add("Promotoria", typeof(string));
            dtPorcentaje.Columns.Add("Suspendidos", typeof(Int32));

            // MUESTRA DATOS EN LA GRAFICA
            foreach (DataRow tramite in dt.Rows)
            {
                dtPorcentaje.Rows.Add(new object[] { tramite["PromotoriaClave"], tramite["suspendidos"] });
            }

            webChartControl.Series.Clear();
            webChartControl.DataSource = dtPorcentaje;
            Series SerieS = new Series("Suspendidos", ViewType.Bar);
            webChartControl.Series.Add(SerieS);
            SerieS.ArgumentScaleType = ScaleType.Qualitative;
            SerieS.ArgumentDataMember = "Promotoria";
            SerieS.ValueScaleType = ScaleType.Numerical;
            SerieS.ValueDataMembers.AddRange(new string[] { "Suspendidos" });
            webChartControl.DataBind();
        }
    }
}
