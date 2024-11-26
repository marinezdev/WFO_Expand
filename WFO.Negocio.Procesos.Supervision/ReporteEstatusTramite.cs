using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class ReporteEstatusTramite
    {
        AccesoDatos.Procesos.Tramite mt = new AccesoDatos.Procesos.Tramite();
        AccesoDatos.Procesos.Flujo f = new AccesoDatos.Procesos.Flujo();
        public void DatosEstatusTramite(ref DevExpress.Web.ASPxGridView aSPxGridView,ref DevExpress.Web.ASPxDropDownEdit DropdownEdit, string fechaDesde, string fechaHasta, string idFlujo)
        {
            DataTable dt = new DataTable();
            string dEstatus = string.Empty;
            string estatus = DropdownEdit.Text;
           if (string.IsNullOrEmpty(estatus))
            {
                dEstatus = "' '";
            }
            else
            {
                string[] listaEstatus = estatus.Split(';');
                foreach (string estado in listaEstatus)
                {
                    dEstatus += "'"+estado.Trim() + "',";

                }
                dEstatus = dEstatus.Trim(',');
            }
            dt = mt.EstatusTramite(fechaDesde, fechaHasta, dEstatus, 1, idFlujo);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
            aSPxGridView.Caption = "ESTATUS TRÁMITE";
        }
        public DataTable EstatusTramite()
        {
            DataTable dt = mt.EstatusTramite();
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
