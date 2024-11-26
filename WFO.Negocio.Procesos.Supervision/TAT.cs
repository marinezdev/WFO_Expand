using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class TAT
    {
        AccesoDatos.Procesos.Tramite T = new AccesoDatos.Procesos.Tramite();
        public void DatosReporteTAT(ref DevExpress.Web.ASPxGridView aSPxGridView, DateTime fechaDesde, DateTime fechaHasta, string idFlujo)
        {
            DataTable Datos = new DataTable();
            aSPxGridView.Columns.Clear();
            Datos = T.TAT(fechaDesde, fechaHasta,idFlujo);
            aSPxGridView.DataSource = Datos;
            aSPxGridView.DataBind();
            int Index = 1;
            foreach (DataColumn Campo in Datos.Columns)
            {
                GridViewDataColumn Col = new GridViewDataColumn();
                Col.VisibleIndex = Index;
                Col.Width = 250;
                Col.Caption = Campo.ColumnName;
                Col.FieldName = Campo.ColumnName;
                aSPxGridView.Columns.Add(Col);
                Index++;
            }
        }
    }
}
