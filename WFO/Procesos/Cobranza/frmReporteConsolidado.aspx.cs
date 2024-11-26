using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos.Cobranza
{
    public partial class frmReporteConsolidado : WFO.Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["RegistrosTemporales"] = null;
                gvAgregado.DataSource = null;
                gvAgregado.DataBind();
            }
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void BtnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                BtnExportar.Visible = true;
                gvAgregado.DataSource = rcl.Seleccionar();
                gvAgregado.DataBind();
                ProcesarEncabezadoFijo(gvAgregado);
            }
            catch (Exception es)
            {
                mensajes.MostrarMensaje(this, "Error al tratar de ejecutar el reporte: " + es.Message);
            }
        }

        protected void ProcesarEncabezadoFijo(GridView grid)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Key", "<script>HacerEncabezadoEstatico('" + grid.ClientID + "', 500, 1170, 120, true); </script>", false);
        }

        protected void gvAgregado_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid1 = (GridView)sender;
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.ColumnSpan = 2;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                //-------------------------------------- 1er periodo vigencia

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Cobertura Básica 1er Periodo de Vigencia";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 19;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Cobertura Potenciada 1er Periodo de Vigencia";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 25;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                //-------------------------------------- 1er trimestre

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Cobertura Básica 1er Trimestre";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 19;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Cobertura Potenciada 1er Trimestre";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 25;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                //-------------------------------------- 2do trimestre

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Cobertura Básica 2do Trimestre";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 19;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Cobertura Potenciada 2do Trimestre";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 25;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                gvAgregado.Controls[0].Controls.AddAt(0, HeaderGridRow1);


                //-------------------------------------- Línea 2 1er periodo vigencia

                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell.ColumnSpan = 2;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Asegurados por parentesco";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 5;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Asegurados por tipo de suma";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 10;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Monto de la prima";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 4;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Asegurados por parentesco";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 5;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Asegurados por tipo de suma";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 16;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Monto de la prima";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 4;
                HeaderGridRow.Cells.Add(HeaderCell);

                //------------------------------------------- 1er trimestre

                HeaderCell = new TableCell();
                HeaderCell.Text = "Asegurados por parentesco";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 5;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Asegurados por tipo de suma";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 10;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Monto de la prima";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 4;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Asegurados por parentesco";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 5;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Asegurados por tipo de suma";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 16;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Monto de la prima";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 4;
                HeaderGridRow.Cells.Add(HeaderCell);

                //------------------------------------------- 2do trimestre

                HeaderCell = new TableCell();
                HeaderCell.Text = "Asegurados por parentesco";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 5;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Asegurados por tipo de suma";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 10;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Monto de la prima";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 4;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Asegurados por parentesco";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 5;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Asegurados por tipo de suma";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 16;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Monto de la prima";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 4;
                HeaderGridRow.Cells.Add(HeaderCell);



                gvAgregado.Controls[0].Controls.AddAt(1, HeaderGridRow);
            }
        }

        protected void BtnExportar_Click(object sender, EventArgs e)
        {
            
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ReporteDeColectividad.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);


                gvAgregado.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in gvAgregado.HeaderRow.Cells)
                {
                    cell.BackColor = gvAgregado.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvAgregado.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvAgregado.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvAgregado.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvAgregado.RenderControl(hw);

                //Estilo para formatear números a cadena
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifica que el control esté renderizado */
        }



    }
}