using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;
using prop = WFO.Propiedades.Procesos.SupervisionGeneral;

namespace WFO.Procesos.SupervisionGeneral
{
    public partial class ListadoTramitesOperacionN3 : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //supervisiongeneraltramites.ListadoTramitesOperacion(ref GVXReporte);
            }
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(supervisiongeneraltramites.qryListadoTramitesOperacionN3());
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                wb.Worksheets.Worksheet(1).Name = "Reporte";

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ASAE_Consultores.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void ListaTramitesIncompletos(object sender, EventArgs e)
        {
            List<prop.TramitesIncompletos> tramitesIncompletos = supervisiongeneraltramites.TramitesIncompletos();
            rptListadoTramites.DataSource = tramitesIncompletos;
            rptListadoTramites.DataBind();

            string script2 = "";
            script2 = "$('#datatable').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); retirar();";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);

        }

        protected void rptTramite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            btnReiniciarTramite.Visible = false;
            upPnlTraMesa.Visible = true;
            if (e.CommandName.Equals("Consultar"))
            {
                string IdTramite = e.CommandArgument.ToString();
                hfIdTramite.Value = IdTramite;
                btnReiniciarTramite.Visible = true;
                upPnlTraMesa.Visible = true;

                List<prop.TramiteMesa> ObservacionesMesas = supervisiongeneraltramites.tramiteMesas(Convert.ToInt32(IdTramite));
                rptObservacionesMesa.DataSource = ObservacionesMesas;
                rptObservacionesMesa.DataBind();

                Folio.Text = "Folio: " + ObservacionesMesas[0].FolioCompuesto;

                string script = "";
                script = "$('#datatable').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]});";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnReiniciarTramite_Click(object sender, EventArgs e)
        {
            upPnlTraMesa.Visible = false;
            LabelRespuestaReinicioTramite.Text = "";
            string NumeroPoliza = TextNumPolizaSisLegado.Text.Trim().ToString();
            int IdTramite = Convert.ToInt32(hfIdTramite.Value);

            if (supervisiongeneraltramites.EnviarTramiteMesaAdmisionNumeroPoliza(IdTramite, NumeroPoliza) == 1 )
            {
                upPnlTraMesa.Visible = false;
                TextNumPolizaSisLegado.Text = "";

                List<prop.TramitesIncompletos> tramitesIncompletos = supervisiongeneraltramites.TramitesIncompletos();
                rptListadoTramites.DataSource = tramitesIncompletos;
                rptListadoTramites.DataBind();

                string script2 = "";
                script2 = "$('#datatable').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); retirar();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
            }
            else
            {
                upPnlTraMesa.Visible = false;

                string script2 = "";
                script2 = "$('#datatable').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); retirar();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);

                LabelRespuestaReinicioTramite.Text = "Error de registro";
            }
        }
    }
}