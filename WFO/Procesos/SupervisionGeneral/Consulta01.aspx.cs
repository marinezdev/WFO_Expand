using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;

namespace WFO.Procesos.SupervisionGeneral
{
    public partial class Consulta01 : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // supervisiongeneraltramites.TramiteConsultaX_LlenarGridView(ref GVReporte);
                // supervisiongeneraltramites.TramiteConsultaX_LlenarAspxGridView(ref GVXReporte);
            }
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(supervisiongeneraltramites.TramiteConsultaX());
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                wb.Worksheets.Worksheet(1).Name = "ASAE";

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
    }
}