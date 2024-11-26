using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.SupervisionGeneral;

namespace WFO.Negocio.Procesos.Supervision
{
    public class Sabana
    {
        AccesoDatos.Procesos.Mesa mM = new AccesoDatos.Procesos.Mesa();

        public DataSet Tramite_EstatusActualDS(DateTime Inicio, DateTime Fin, int IdFlujo)
        {
            return mM.Tramite_EstatusActualDS(Inicio, Fin, IdFlujo);
        }

        public List<prop.Tramites_Clean> Tramites_Clean(DateTime Inicio, DateTime Fin, int IdFlujo)
        {
            return mM.Tramites_Clean(Inicio, Fin, IdFlujo);
        }

        public List<prop.Tramites_SuspendidosTotales> Tramites_SuspendidosTotales(DateTime Inicio, DateTime Fin, int IdFlujo)
        {
            return mM.Tramites_SuspendidosTotales(Inicio, Fin, IdFlujo);
        }

        public List<prop.Tramites_Supendidos> Tramites_Suspendidos(DateTime Inicio, DateTime Fin, int IdFlujo, int IdPromotoria)
        {
            return mM.Tramites_Suspendidos(Inicio, Fin, IdFlujo, IdPromotoria);
        }

        public List<prop.Tramite_EstatusActual> Tramite_EstatusActual(DateTime Inicio, DateTime Fin, int IdFlujo)
        {
            return mM.Tramite_EstatusActual(Inicio, Fin, IdFlujo);
        }

        public List<prop.Tramite> Tramite_UltimoEstatusTramite(DateTime Inicio, DateTime Fin, int IdFlujo)
        {
            return mM.Tramite_UltimoEstatusTramite(Inicio, Fin, IdFlujo);
        }

        public List<prop.DetalleMesa> Tramite_InformacionBitacora(int IdTramite)
        {
            return mM.Tramite_InformacionBitacora(IdTramite);
        }

        public List<prop.BitacoraSabana> SabanaConsultaBitacoraDescarga()
        {
            return mM.SabanaConsultaBitacoraDescarga();
        }

        public DataTable SabanaReporte(DateTime fechaDesde, DateTime fechaHasta, int IdUsuario, int IdFlujo)
        {
            return mM.Sabana(fechaDesde, fechaHasta, IdUsuario, IdFlujo);
        }

        public void MostrarDatosMesa(ref DevExpress.Web.ASPxGridView aSPxGridView, string fechaDesde, string fechaHasta, string idFlujo)
        {
            DataTable datos = new DataTable();
            aSPxGridView.Columns.Clear();
            datos = mM.DatosReporteSabana(fechaDesde, fechaHasta, idFlujo);
            aSPxGridView.DataSource = datos;
            aSPxGridView.DataBind();
            int Index = 1;

            foreach (DataColumn Campo in datos.Columns)
            {
                GridViewDataColumn Col = new GridViewDataColumn();
                Col.VisibleIndex = Index;
                if (Index == 1) Col.Visible = false;
                Col.Width = 200;
                Col.Caption = Campo.ColumnName;
                Col.FieldName = Campo.ColumnName;
                aSPxGridView.Columns.Add(Col);
                Index++;
            }
        }

    }
}
