using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Timers;

namespace WFO.Procesos.Supervision
{
    public partial class sprSabanaDescargaV2 : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["In"]) && !String.IsNullOrEmpty(Request.QueryString["Fn"]) && !String.IsNullOrEmpty(Request.QueryString["Flu"]))
            {
                DateTime CalDesde = Convert.ToDateTime(Request.QueryString["In"].ToString());
                DateTime CalHasta = Convert.ToDateTime(Request.QueryString["Fn"].ToString());

                LabelFechaInicio.Text = CalDesde.Date.ToShortDateString();
                LabelFechaFin.Text = CalHasta.Date.ToShortDateString();
            }

            manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnDescargar_Click(object sender, EventArgs e)
        {
            DateTime CalDesde = Convert.ToDateTime(LabelFechaInicio.Text.ToString());
            DateTime CalHasta = Convert.ToDateTime(LabelFechaFin.Text.ToString());

            int IdFlujo = Convert.ToInt32(Request.QueryString["Flu"].ToString());

            WFO.Negocio.Procesos.Supervision.Sabana sabana = new Negocio.Procesos.Supervision.Sabana();

            DataTable dt = sabana.SabanaReporte(CalDesde, CalHasta, manejo_sesion.Usuarios.IdUsuario, IdFlujo);

            Informacion.Visible = false;
            InformacionFin.Visible = true;

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            Descarga(ds);
        }

        protected void Descarga(DataSet dt)
        {
            var wb = new XLWorkbook();
            // Add all DataTables in the DataSet as a worksheets
            wb.Worksheets.Add(dt);

            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"Sabana.xlsx\"");


            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }
    }
}