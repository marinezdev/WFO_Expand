using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Web;
using System.Web.UI.DataVisualization.Charting;

namespace WFO.Negocio.Procesos.Supervision
{
    public class ReporteTramitesAnuales
    {
        AccesoDatos.Procesos.Tramite tramite = new AccesoDatos.Procesos.Tramite();

        public void ReportedeTramitesAnuales(ref ASPxGridView gridview)
        {
            Funciones.LlenarControles.LlenaraAspxGridView(ref gridview, tramite.ReporteTramitesAnuales());
        }

        public void LlenadoGraficoTramitesAnuales(ref Chart grafico)
        {
            DataTable ChartData = tramite.ReporteTramitesAnualesGrafico().Tables[0];

            string[] XPointMember = new string[ChartData.Rows.Count];
            int[] YPointMember = new int[ChartData.Rows.Count];

            for (int count = 0; count < ChartData.Rows.Count; count++)
            {
                //storing Values for X axis  
                XPointMember[count] = ChartData.Rows[count]["Mes"].ToString();
                //storing values for Y Axis  
                YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["Tramites"]);
            }
            //binding chart control  
            grafico.Series[0].Points.DataBindXY(XPointMember, YPointMember);

            //Setting width of line  
            grafico.Series[0].BorderWidth = 1;
            //setting Chart type   
            grafico.Series[0].ChartType = SeriesChartType.Line;
            grafico.Series[0].Points[0].AxisLabel = "Ene";
            grafico.Series[0].Points[1].AxisLabel = "Feb";
            grafico.Series[0].Points[2].AxisLabel = "Mar";
            grafico.Series[0].Points[3].AxisLabel = "Abr";
            grafico.Series[0].Points[4].AxisLabel = "May";
            grafico.Series[0].Points[5].AxisLabel = "Jun";
            grafico.Series[0].Points[6].AxisLabel = "Jul";
            grafico.Series[0].Points[7].AxisLabel = "Ago";
            grafico.Series[0].Points[8].AxisLabel = "Sep";
            grafico.Series[0].Points[9].AxisLabel = "Oct";
            grafico.Series[0].Points[10].AxisLabel = "Nov";
            grafico.Series[0].Points[11].AxisLabel = "Dic";


            DataTable ChartData2 = tramite.ReporteTramitesAnualesGrafico().Tables[1];

            string[] XXPointMember = new string[ChartData2.Rows.Count];
            int[] YYPointMember = new int[ChartData2.Rows.Count];

            for (int count = 0; count < ChartData2.Rows.Count; count++)
            {
                //storing Values for X axis  
                XXPointMember[count] = ChartData2.Rows[count]["Mes"].ToString();
                //storing values for Y Axis  
                YYPointMember[count] = Convert.ToInt32(ChartData2.Rows[count]["Tramites"]);
            }
            //binding chart control  
            grafico.Series[1].Points.DataBindXY(XXPointMember, YYPointMember);

            //Setting width of line  
            grafico.Series[1].BorderWidth = 1;
            //setting Chart type   
            grafico.Series[1].ChartType = SeriesChartType.Line;

            grafico.Legends[0].Enabled = true;
        }
    }
}
