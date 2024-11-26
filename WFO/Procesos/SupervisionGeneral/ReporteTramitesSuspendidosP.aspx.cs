using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Operacion;
using prosu = WFO.Propiedades.Procesos.SupervisionGeneral;
using ClosedXML.Excel;
using System.IO;

namespace WFO.Procesos.SupervisionGeneral
{
    public partial class ReporteTramitesSuspendidosP : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Operacion.UsuariosFlujo usuariosFlujo = new Negocio.Procesos.Operacion.UsuariosFlujo();

        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];
            if (!IsPostBack)
            {
                CalDesde.EditFormatString = "yyyy-MM-dd";
                CalDesde.Date = DateTime.Now.AddDays(-1);
                CalDesde.MaxDate = DateTime.Today;
                CalHasta.EditFormatString = "yyyy-MM-dd";
                CalHasta.Date = DateTime.Today;
                CalHasta.MaxDate = DateTime.Today;
                CargaFlujos(manejo_sesion.Usuarios.IdUsuario);
            }
        }

        protected void CargaFlujos(int Id)
        {
            List<prop.UsuariosFlujo> Flujos = usuariosFlujo.SelecionarFlujoSabana(Id);
            cbFlujos.DataSource = Flujos;
            cbFlujos.DataBind();
            cbFlujos.TextField = "Nombre";
            cbFlujos.ValueField = "Id";
            cbFlujos.DataBind();
        }

        protected void btnFiltroMes_Click(object sender, EventArgs e)
        {
            String script;
            Mensaje.Text = "";
            if (CalDesde.Date <= CalHasta.Date)
            {
                int IdFlujo = Convert.ToInt32(cbFlujos.SelectedItem.Value.ToString());
                List<prosu.Tramites_SuspendidosTotales> tramites = rs.Tramites_SuspendidosTotales(CalDesde.Date, CalHasta.Date, IdFlujo);
                int num = tramites.Count;
                rptTramites.DataSource = tramites;
                rptTramites.DataBind();

                // Formato de tabla
                script = "";
                script = "$('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); retirar();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            else
            {
                Mensaje.Text = "La fecha 'Desde' debe ser menor a la fecha 'Hasta'";
                //rptTramitesEspera.Visible = false;
            }
        }


        [WebMethod]
        public static ConsultasMesas Busqueda(int Id)
        {
            WFO.Negocio.Procesos.Supervision.Sabana sabana = new Negocio.Procesos.Supervision.Sabana();

            List<prosu.DetalleMesa> tramites = sabana.Tramite_InformacionBitacora(Id);

            /* LLENAR JSON PARA RETORNAR */
            ConsultasMesas jsonObject = new ConsultasMesas();
            jsonObject.consulta = new List<Consulta>();

            foreach (prosu.DetalleMesa item in tramites)
            {
                jsonObject.consulta.Add(new Consulta()
                {
                    Orden = item.NORDENREPORTE.ToString(),
                    IdTramite = item.IdTramite.ToString(),
                    FechaRegistro = item.FechaRegistro.ToString(),
                    NMESA = item.NMESA.ToString(),
                    FechaInicio = item.FechaInicio.ToString(),
                    FechaTermino = item.FechaTermino.ToString(),
                    EstadoMesa = item.EstadoMesa.ToString(),
                    Observacion = item.Observacion.ToString(),
                    NombreUsuario = item.NombreUsuario.ToString(),
                });
            }
            return jsonObject;
        }


        protected void btnExportar_Click(object sender, EventArgs e)
        {
            Mensaje.Text = "";
            DataSet ds = new DataSet();

            if (CalDesde.Date <= CalHasta.Date)
            {
                int IdFlujo = Convert.ToInt32(cbFlujos.SelectedItem.Value.ToString());
                List<prosu.Tramite_EstatusActual> tramites = rs.Tramite_EstatusActual(CalDesde.Date, CalHasta.Date, IdFlujo);

                if (tramites.Count > 0)
                {
                    DataTable table = new DataTable();

                    System.Collections.IList list = tramites;
                    for (int i = 0; i < list.Count; i++)
                    {
                        string ColumnN = (string)list[i];
                        table.Columns.Add(ColumnN, typeof(string));
                    }

                    foreach (string str in list)
                    {
                        System.Collections.IList list2 = tramites;
                        for (int i = 0; i < list2.Count; i++)
                        {
                            string ColumnN = (string)list[i];
                            DataRow row = table.NewRow();
                            row[ColumnN] = str;
                            table.Rows.Add(row);
                        }
                    }
                    ds.Tables.Add(table);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    //wb.Worksheets.Add(supervisiongeneraltramites.TramiteConsultaX());

                    //wb.Worksheets.Add(supervisiongeneraltramites.Tramites_Totales(_IdFlujo, _FechaInicio, _FechaFinal));
                    wb.Worksheets.Add(ds);
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

                //if (this.cbFlujos.SelectedItem == null || this.cbFlujos.SelectedIndex == -1)
                //{
                //    string script = "BitacoraSabana()();";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                //}
                //else
                //{
                //    int IdFlujo = Convert.ToInt32(cbFlujos.SelectedItem.Value.ToString());
                //    string script = "window.open('sprSabanaDescarga.aspx?In=" + CalDesde.Date + "&Fn=" + CalHasta.Date + "&Flu=" + IdFlujo + "','Expediente', 'width = 800, height = 400'); ";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                //}
            }
            else
            {
                Mensaje.Text = "La fecha 'Desde' debe ser menor a la fecha 'Hasta'";
                //rptTramitesEspera.Visible = false;
            }
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            Mensaje.Text = "";

            if (CalDesde.Date <= CalHasta.Date)
            {
                int IdFlujo = Convert.ToInt32(cbFlujos.SelectedItem.Value.ToString());
                DataSet dsTramites = rs.Tramite_EstatusActualDS(CalDesde.Date, CalHasta.Date, IdFlujo);

                using (XLWorkbook wb = new XLWorkbook())
                {
                    //wb.Worksheets.Add(supervisiongeneraltramites.TramiteConsultaX());

                    //wb.Worksheets.Add(supervisiongeneraltramites.Tramites_Totales(_IdFlujo, _FechaInicio, _FechaFinal));
                    wb.Worksheets.Add(dsTramites);
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
            else
            {
                Mensaje.Text = "La fecha 'Desde' debe ser menor a la fecha 'Hasta'";
                //rptTramitesEspera.Visible = false;
            }
        }

        public static ConsultaBitacoraSabana BusquedaBitacoraDescraga()
        {
            WFO.Negocio.Procesos.Supervision.Sabana sabana = new Negocio.Procesos.Supervision.Sabana();
            List<prosu.BitacoraSabana> tramites = sabana.SabanaConsultaBitacoraDescarga();

            /* LLENAR JSON PARA RETORNAR */
            ConsultaBitacoraSabana jsonObject = new ConsultaBitacoraSabana();
            jsonObject.bitacoraSabanas = new List<BitacoraSabana>();

            foreach (prosu.BitacoraSabana item in tramites)
            {
                jsonObject.bitacoraSabanas.Add(new BitacoraSabana()
                {
                    FechaRegistro = item.FechaRegistro.ToString(),
                    FechaInicio = item.FechaInicio.ToString(),
                    FechaFin = item.FechaFin.ToString(),
                    NumRegistros = item.NumRegistros.ToString(),
                    Usuario = item.Usuario.ToString(),
                    NumSolicitudes = item.NumSolicitudes.ToString(),
                });
            }

            return jsonObject;
        }



    }
}