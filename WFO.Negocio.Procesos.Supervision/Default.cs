using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WFO.Negocio.Procesos.Supervision
{
    public class Default
    {
        AccesoDatos.Procesos.MapaSupervisor mp = new AccesoDatos.Procesos.MapaSupervisor();
        AccesoDatos.Procesos.Tramite t = new AccesoDatos.Procesos.Tramite();
        AccesoDatos.Procesos.Flujo f = new AccesoDatos.Procesos.Flujo();

        public void MostrarMapa(ref Literal Indicadores,string idFlujo)
        {

            DataTable dt = new DataTable();
            dt = mp.DatosMapaSupervisor(idFlujo);
            int TotalRegistros = dt.Rows.Count;
            int registro = 0;
            int columna = 0;
            Indicadores.Text = string.Empty;
            while (registro <= TotalRegistros-1 )
            {
                Indicadores.Text += "<div class='row'>";
                while (columna <=3)
                {
                    if (registro <= TotalRegistros - 1)
                    {
                        Indicadores.Text += "<a href='sprMapaSupervisor.aspx?Id=" + dt.Rows[registro]["NombreMesa"].ToString() +"&idFlujo="+idFlujo+ "'>";
                        Indicadores.Text += "<div class='col-sm-3 col-md-3'>";
                        Indicadores.Text += "<img src='" + dt.Rows[registro]["Icono"].ToString() + "' class='img-responsive center-block img-thumbnail' /><br/>";
                        Indicadores.Text += "<table style='width:100%; border-collapse: separate; border-spacing: 0px;' border='0'> ";
                        Indicadores.Text += "<tr><td style='text-align:center'><label><b>" + dt.Rows[registro]["NombreMesa"].ToString() + "</b></label></td></tr>";
                        Indicadores.Text += "<tr><td style='text-align:center'><label>Usuarios conectados: </label><label>" + dt.Rows[registro]["usuarios"].ToString() + "</label></td></tr> ";
                        Indicadores.Text += "<tr><td style='text-align:center'><label>Tramites: </label><label>" + dt.Rows[registro]["Tramites"] + "</label></td></tr>";
                        Indicadores.Text += "</table><br/>";
                        Indicadores.Text += "</div>";
                        Indicadores.Text += "</a>";
                        columna = columna + 1;
                        registro = registro + 1; 
                    }
                    else {
                        columna = 4;
                    }
                }
                columna = 0;
                //registro = registro;
                Indicadores.Text += "</div>";
            }
        }
        public void MapaDetalle(ref DevExpress.Web.ASPxGridView aSPxGridView, ref DevExpress.XtraCharts.Web.WebChartControl webChartControl,string idFlujo)
        {
            DataTable dt = new DataTable();
            dt = t.MapaSupervisorDetalle(idFlujo);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
            aSPxGridView.Caption = "TOTALES POR ESTATUS";

            DataTable dtTotales = new DataTable();
            dtTotales.Columns.Add("ESTATUS");
            dtTotales.Columns.Add("TOTAL", typeof(Int32));

            // MUESTRA DATOS EN LA GRAFICA
            foreach (DataRow tramite in dt.Rows)
            {
                dtTotales.Rows.Add(new object[] { tramite["ESTATUS"], tramite["TOTAL"] });
            }

            webChartControl.DataSource = dtTotales;
            webChartControl.SeriesDataMember = "ESTATUS";
            webChartControl.SeriesTemplate.SetDataMembers("ESTATUS", "TOTAL");
            webChartControl.SeriesTemplate.ArgumentDataMember = "ESTATUS";
            webChartControl.DataBind();
        }
        public DataTable DatosResumenPromotoria(string estado, string idFlujo)
        {
            DataTable dt = new DataTable();
            dt = t.ResumenPromotoria(estado,idFlujo);
            return dt;
        }
        public DataTable DatosComboFlujo()
        {
            DataTable dt = new DataTable();
            dt = f.DatosFlujo();
            return dt;
        }
    }
}
