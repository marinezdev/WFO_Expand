using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos.Supervision
{
    public partial class sprAlmacenamientoTramites : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];
            if (!IsPostBack)
            {
                atr.AlmacenamientodeTramitesPromedio(ref GVPromedio);
                if (Request["a"] != null)
                {
                    atr.AlmacenamientodeTramites(ref rptTramitesAlmacen, Request["a"]);

                    string script2 = "";
                    script2 = "$('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); retirar();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
                }
            }
        }

        protected void rptTramitesAlmacen_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar"))
            {
            //    string IdTramite = e.CommandArgument.ToString();
            //    Response.Redirect("ConsultaTramite.aspx?Procesable=" + IdTramite);
            }
        }
    }
}