using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

namespace WFO.Negocio.Procesos.Supervision
{
    public class ReporteTramitesReingresosV2
    {
        AccesoDatos.Procesos.Tramite tramite = new AccesoDatos.Procesos.Tramite();

        public DataSet CargarGrafico(ref Chart grafico, string idusuario, string fechainicio, string fechatermino, string tipotramite)
        {
            //Carga gráfico
            DataSet ds = new DataSet();
            ds = tramite.ReporteTramitesReingresos(idusuario, fechainicio, fechatermino, tipotramite);
            grafico.DataSource = ds.Tables[1];

            Series serie01 = grafico.Series.Add("Procesos Realizados");
            serie01.ChartArea = "GrupoUno";
            serie01.Font = new Font("Arial", 6.5F);
            serie01.ChartType = SeriesChartType.Bar;
            serie01.Color = Color.SteelBlue;
            serie01.IsValueShownAsLabel = true;
            serie01.XValueMember = "Nombre";
            serie01.YValueMembers = "ProcesosRealizados";
            serie01.CustomProperties = "ShowMarkerLines=true";
            serie01.PostBackValue = "#VALX";
            serie01.IsValueShownAsLabel = true;

            Series serie02 = grafico.Series.Add("Total de Reingresos");
            serie02.ChartArea = "GrupoUno";
            serie02.Font = new Font("Arial", 6.5F);
            serie02.ChartType = SeriesChartType.Bar;
            serie02.Color = Color.Red;
            serie02.IsValueShownAsLabel = true;
            serie02.YValueMembers = "TotaldeReingresos";
            serie02.CustomProperties = "ShowMarkerLines=true";
            serie02.PostBackValue = "#VALX";
            serie02.IsValueShownAsLabel = true;

            grafico.DataBind();

            return ds;
        }
    }
}
