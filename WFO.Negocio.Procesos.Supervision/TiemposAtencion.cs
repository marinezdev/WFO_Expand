using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class TiemposAtencion
    {
        AccesoDatos.Procesos.Mesa m = new AccesoDatos.Procesos.Mesa();
        AccesoDatos.Procesos.TramiteMesa tm = new AccesoDatos.Procesos.TramiteMesa();

        public void DatosTiemposAtencion(ref DevExpress.Web.ASPxGridView aSPxGridView,DateTime fechaDesde,DateTime fechaHasta, string IdFlujo)
        {
            DataTable Mesas = new DataTable();
            DataTable Datos = new DataTable();
            Mesas = m.Mesas();

            Datos = tm.TiemposAtencion(fechaDesde, fechaHasta,IdFlujo);
            aSPxGridView.DataSource = Datos;
            aSPxGridView.DataBind();
            aSPxGridView.Columns.Clear();

            int index = 1;
            GridViewDataColumn Producto = AgregarColumna("TRAMITE", "FOLIOCOMPUESTO");
            aSPxGridView.Columns.Add(Producto);
            Producto.VisibleIndex = index;
            foreach (DataRow Registro in Mesas.Rows)
            {
                index++;
                foreach (DataColumn Campo in Mesas.Columns)
                {
                    GridViewBandColumn Col = new GridViewBandColumn(Registro[Campo].ToString());
                    aSPxGridView.Columns.Add(Col);
                    Col.Columns.Add(AgregarColumna("Espera", Registro[Campo].ToString() + "_E"));
                    Col.Columns.Add(AgregarColumna("Atención", Registro[Campo].ToString() + "_A"));
                    Col.VisibleIndex = index;
                }
            }
        }
        protected GridViewDataColumn AgregarColumna(string descripcion, string campo)
        {
            GridViewDataColumn subCol = new GridViewDataColumn();
            subCol.Caption = descripcion;
            subCol.FieldName = campo;
            if (string.Equals(descripcion, "TRAMITE"))
            {
                subCol.Width = 140;
            }
            return subCol;
        }
    }
}
