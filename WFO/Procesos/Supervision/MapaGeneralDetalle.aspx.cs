using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Procesos.Supervision
{
    public partial class MapaGeneralDetalle : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Operacion.MapaGeneral TableroControl = new Negocio.Procesos.Operacion.MapaGeneral();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
                hfIdMesa.Value = Request.QueryString["IdMesa"].ToString();
                hfIdFlujo.Value = Request.QueryString["IdFlujo"].ToString();
                Resumen();
                TramitesDetalle();
            }
        }

        protected void Resumen()
        {
            List<prop.MapaGeneral> WFODashboard = TableroControl.DashboardMesa(int.Parse(hfIdFlujo.Value), int.Parse(hfIdMesa.Value));
            lblTitulo.Text = "Mapa General. Mesas de " + WFODashboard[0].Mesa;
            MesaResumen.DataSource = WFODashboard;
            MesaResumen.DataBind();
            MesaResumen.Visible = true;
            string script2 = "";
            script2 = "$('#tMesaResumen').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); retirar();";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
        }

        private void TramitesDetalle()
        {
            List<prop.MapaGeneralMesaDetalleTramite> WFODashboard = TableroControl.getDashboardMesaDetalleTramite(int.Parse(hfIdFlujo.Value), int.Parse(hfIdMesa.Value));
            RepeaterFechas.DataSource = WFODashboard;
            RepeaterFechas.DataBind();
            RepeaterFechas.Visible = true;
            string script2 = "";
            script2 = "$('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); retirar();";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
        }

        protected void rptTramite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar"))
            {
                string IdTramite = e.CommandArgument.ToString();
                Response.Redirect("../SupervisionGeneral/ConsultaTramite.aspx?Procesable=" + IdTramite);
            }
        }
    }
}