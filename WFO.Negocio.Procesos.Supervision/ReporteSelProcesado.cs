using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class ReporteSelProcesado
    {
        AccesoDatos.Procesos.TramiteMesa tm = new AccesoDatos.Procesos.TramiteMesa();
        public void DatosReporteSelProcesado(ref DevExpress.Web.ASPxGridView aSPxGridView, string fechaDesde, string fechaHasta,string idFlujo)
        {
            DataTable dt = new DataTable();
            dt = tm.TramitesProcesables(fechaDesde, fechaHasta,idFlujo);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
            int Index = 1;
            aSPxGridView.Columns.Clear();
            foreach (DataColumn Campo in dt.Columns)
            {
                GridViewDataColumn Col = new GridViewDataColumn();
                Col.VisibleIndex = Index;
                Col.Caption = Campo.ColumnName;
                Col.FieldName = Campo.ColumnName;
                Col.Width = 200;
                aSPxGridView.Columns.Add(Col);
                Index++;
            }
        }
    }
}
